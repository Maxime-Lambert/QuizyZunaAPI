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
            new Collection<WrongAnswer>() { WrongAnswer.Create(new QuestionId(Guid.NewGuid()), "Answer 1")},
            new Collection<WrongAnswer>() { WrongAnswer.Create(new QuestionId(Guid.NewGuid()), "Answer 1"),
                                            WrongAnswer.Create(new QuestionId(Guid.NewGuid()), "Answer 2")},
            new Collection<WrongAnswer>() { WrongAnswer.Create(new QuestionId(Guid.NewGuid()), "Answer 1"),
                                            WrongAnswer.Create(new QuestionId(Guid.NewGuid()), "Answer 2"),
                                            WrongAnswer.Create(new QuestionId(Guid.NewGuid()), "Answer 3"),
                                            WrongAnswer.Create(new QuestionId(Guid.NewGuid()), "Answer 4")}
        };

    [Fact]
    public void Create_Should_Suceed_WhenValueIsValid()
    {
        //Arrange
        QuestionId questionId = new(Guid.NewGuid());
        QuestionTitle title = new("Is this a question ?");
        CorrectAnswer correctAnswer = new("Yes");
        ICollection<WrongAnswer> wrongAnswersList = 
            [WrongAnswer.Create(questionId, "No"),
            WrongAnswer.Create(questionId, "Maybe"),
            WrongAnswer.Create(questionId, "Impossible")];
        WrongAnswers wrongAnswers = new(wrongAnswersList);
        Answers answers = new(correctAnswer, wrongAnswers);
        ICollection<Theme> themesList = [Theme.Create(questionId, Topic.Literature)];
        Themes themes = new(themesList);
        var difficulty = Difficulty.Beginner;
        var era = Era.None;
        QuestionTags questionTags = new(themes, difficulty, era);

        //Act
        var result = Question.Create(questionId, title, answers, questionTags);

        //Assert
        result.Id.Value.Should().Be(questionId.Value);
        result.Title.Value.Should().Be(title.Value);
        result.Answers.CorrectAnswer.Value.Should().Be(correctAnswer.Value);
        result.Answers.WrongAnswers.Value.Should().BeEquivalentTo(wrongAnswersList);
        result.Tags.Themes.Value.Should().BeEquivalentTo(themesList);
        result.Tags.Difficulty.Should().Be(difficulty);
        result.Tags.Era.Should().Be(era);
    }

    [Fact]
    public void QuestionTitle_Create_Should_ThrowException_WhenValueIsEmpty()
    {
        //Arrange
        static QuestionTitle Action() => new("");

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().ThrowExactly<TitleIsEmptyDomainException>().WithMessage($"questionTitle can't be null");
    }

    [Fact]
    public void CorrectAnswer_Create_Should_ThrowException_WhenValueIsEmpty()
    {
        //Arrange
        static CorrectAnswer Action() => new("");

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().ThrowExactly<CorrectAnswerIsEmptyDomainException>().WithMessage($"correctAnswer can't be null");
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
        var correctAnswer = new CorrectAnswer("Yes");
        ICollection<WrongAnswer> wrongAnswersList =
            [WrongAnswer.Create(questionId, "Yes"),
                WrongAnswer.Create(questionId, "Maybe"),
                WrongAnswer.Create(questionId, "Impossible")];
        var wrongAnswers = new WrongAnswers(wrongAnswersList);

        Answers Action() => new(correctAnswer, wrongAnswers);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().ThrowExactly<WrongAnswersContainsCorrectAnswerDomainException>().WithMessage($"{nameof(correctAnswer)} can't be contained by {nameof(WrongAnswers)}");
    }

    [Fact]
    public void Themes_Create_Should_ThrowException_WhenThemesIsNull()
    {
        //Arrange
        static Themes Action() => new([]);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().ThrowExactly<ThemesIsEmptyDomainException>().WithMessage($"Themes can't be null");
    }
}
