using System.Collections.ObjectModel;

using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Entities;
using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.Exceptions;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.UnitTests.Questions;

public class QuestionTests
{
    public static TheoryData<Collection<WrongAnswer>> InvalidWrongAnswerValues =>
        new()
        {
            new Collection<WrongAnswer>(),
            new Collection<WrongAnswer>() { WrongAnswer.Create(new QuestionId(Guid.NewGuid()), "Answer 1", new TimesAnswered(0))},
            new Collection<WrongAnswer>() { WrongAnswer.Create(new QuestionId(Guid.NewGuid()), "Answer 1", new TimesAnswered(0)),
                                            WrongAnswer.Create(new QuestionId(Guid.NewGuid()), "Answer 2", new TimesAnswered(0))},
            new Collection<WrongAnswer>() { WrongAnswer.Create(new QuestionId(Guid.NewGuid()), "Answer 1", new TimesAnswered(0)),
                                            WrongAnswer.Create(new QuestionId(Guid.NewGuid()), "Answer 2", new TimesAnswered(0)),
                                            WrongAnswer.Create(new QuestionId(Guid.NewGuid()), "Answer 3", new TimesAnswered(0)),
                                            WrongAnswer.Create(new QuestionId(Guid.NewGuid()), "Answer 4", new TimesAnswered(0))}
        };

    [Fact]
    public void Create_Should_Suceed_WhenValueIsValid()
    {
        //Arrange
        QuestionId questionId = new(Guid.NewGuid());
        QuestionTitle title = new("Is this a question ?");
        CorrectAnswer correctAnswer = new("Yes", new TimesAnswered(0));
        ICollection<WrongAnswer> wrongAnswersList = 
            [WrongAnswer.Create(questionId, "No", new TimesAnswered(0)),
            WrongAnswer.Create(questionId, "Maybe", new TimesAnswered(0)),
            WrongAnswer.Create(questionId, "Impossible", new TimesAnswered(0))];
        WrongAnswers wrongAnswers = new(wrongAnswersList);
        Answers answers = new(correctAnswer, wrongAnswers);
        ICollection<Theme> themesList = [Theme.Create(questionId, Topic.Literature)];
        Themes themes = new(themesList);
        var difficulty = Difficulty.Beginner;
        QuestionYear date = new("");
        QuestionTags questionTags = new(themes, difficulty, date);
        QuestionLastModifiedAt questionLastModifiedAt = new(DateTime.UtcNow);

        //Act
        var result = Question.Create(questionId, title, answers, questionTags, questionLastModifiedAt);

        //Assert
        result.Id.Value.Should().Be(questionId.Value);
        result.Title.Value.Should().Be(title.Value);
        result.LastModifiedAt.Value.Should().Be(questionLastModifiedAt.Value);
        result.Answers.CorrectAnswer.Value.Should().Be(correctAnswer.Value);
        result.Answers.WrongAnswers.Value.Should().BeEquivalentTo(wrongAnswersList);
        result.Tags.Themes.Value.Should().BeEquivalentTo(themesList);
        result.Tags.Difficulty.Should().Be(difficulty);
        result.Tags.Year.Should().Be(date);
    }

    [Fact]
    public void QuestionTitle_Create_Should_ThrowException_WhenValueIsEmpty()
    {
        //Arrange
        static QuestionTitle Action() => new("");

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().ThrowExactly<ArgumentException>();
    }

    [Theory]
    [MemberData(nameof(InvalidWrongAnswerValues))]
    public void WrongAnswersList_Create_Should_ThrowException_WhenValueIsInvalid(Collection<WrongAnswer> wrongAnswers)
    {
        //Arrange
        WrongAnswers Action() => new(wrongAnswers);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().ThrowExactly<WrongAnswersDoesNotContainThreeElementsDomainException>().WithMessage($"{nameof(wrongAnswers)} must contain 3 elements");
    }

    [Fact]
    public void Answers_Create_Should_ThrowException_WhenValueIsInvalid()
    {
        //Arrange
        var questionId = new QuestionId(Guid.NewGuid());
        var correctAnswer = new CorrectAnswer("Yes", new TimesAnswered(0));
        ICollection<WrongAnswer> wrongAnswersList =
            [WrongAnswer.Create(questionId, "Yes", new TimesAnswered(0)),
                WrongAnswer.Create(questionId, "Maybe", new TimesAnswered(0)),
                WrongAnswer.Create(questionId, "Impossible", new TimesAnswered(0))];
        var wrongAnswers = new WrongAnswers(wrongAnswersList);

        Answers Action() => new(correctAnswer, wrongAnswers);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().ThrowExactly<WrongAnswersContainsCorrectAnswerDomainException>().WithMessage($"{nameof(correctAnswer)} can't be contained by {nameof(WrongAnswers)}");
    }

    [Fact]
    public void QuestionYear_Create_Should_ThrowException_WhenValueIsInvalid()
    {
        //Arrange
        QuestionYear Action() => new("2024");

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().ThrowExactly<QuestionYearIsNotConformDomainException>().WithMessage($"{nameof(QuestionYear)} must be in the format from -99999999999 to +99999999999 with exactly 11 decimals");
    }

    [Fact]
    public void QuestionYear_Create_Should_Success_WhenValueIsValid()
    {
        //Arrange
        string validYear = "+00000002024";

        //Act
        QuestionYear result = new(validYear);

        //Assert
        result.Value.Should().Be(validYear);
    }
}
