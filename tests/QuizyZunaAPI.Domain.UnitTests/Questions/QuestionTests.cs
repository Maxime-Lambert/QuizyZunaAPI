using System.Collections.ObjectModel;

using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.Exceptions;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.UnitTests.Questions;

public class QuestionTests
{

    public static TheoryData<Collection<WrongAnswer>> invalidWrongAnswerValues =>
        new()
        {
            new Collection<WrongAnswer>(),
            new Collection<WrongAnswer>() { WrongAnswer.Create(QuestionId.Create(Guid.NewGuid()), "Answer 1") },
            new Collection<WrongAnswer>() { WrongAnswer.Create(QuestionId.Create(Guid.NewGuid()), "Answer 1"),
                                            WrongAnswer.Create(QuestionId.Create(Guid.NewGuid()), "Answer 2")},
            new Collection<WrongAnswer>() { WrongAnswer.Create(QuestionId.Create(Guid.NewGuid()), "Answer 1"),
                                            WrongAnswer.Create(QuestionId.Create(Guid.NewGuid()), "Answer 2"),
                                            WrongAnswer.Create(QuestionId.Create(Guid.NewGuid()), "Answer 3"),
                                            WrongAnswer.Create(QuestionId.Create(Guid.NewGuid()), "Answer 4")}
        };

    [Fact]
    public void Question_Create_Should_Suceed_WhenValueIsValid()
    {
        //Arrange
        var questionId = QuestionId.Create(Guid.NewGuid());
        var title = QuestionTitle.Create("Is this a question ?");
        var correctAnswer = CorrectAnswer.Create("Yes");
        ICollection<WrongAnswer> wrongAnswersList = 
            [WrongAnswer.Create(questionId, "No"),
            WrongAnswer.Create(questionId, "Maybe"),
            WrongAnswer.Create(questionId, "Impossible")];
        var wrongAnswers = WrongAnswers.Create(wrongAnswersList);
        var answers = Answers.Create(correctAnswer, wrongAnswers);
        ICollection<Theme> themesList = [Theme.Create(questionId, Topic.Literature)];
        var themes = Themes.Create(themesList);
        var difficulty = Difficulty.Beginner;
        var questionTags = QuestionTags.Create(themes, difficulty);

        //Act
        var result = Question.Create(questionId, title, answers, questionTags);

        //Assert
        result.Id.Value.Should().Be(questionId.Value);
        result.Title.Value.Should().Be(title.Value);
        result.Answers.CorrectAnswer.Value.Should().Be(correctAnswer.Value);
        result.Answers.WrongAnswers.Value.Should().BeEquivalentTo(wrongAnswersList);
        result.Tags.Themes.Value.Should().BeEquivalentTo(themesList);
        result.Tags.Difficulty.Should().Be(difficulty);
        result.Tags.Era.Should().Be(Era.None);
    }

    [Fact]
    public void QuestionId_Create_Should_ThrowException_WhenValueIsNull()
    {
        //Arrange
        static QuestionId Action() => QuestionId.Create(null);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<QuestionIdIsNullDomainException>().Which.Message.Should().Be("questionId can't be null");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void QuestionTitle_Create_Should_ThrowException_WhenValueIsNullOrEmpty(string? title)
    {
        //Arrange
        QuestionTitle Action() => QuestionTitle.Create(title);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<TitleIsNullDomainException>().Which.Message.Should().Be("questionTitle can't be null");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void CorrectAnswer_Create_Should_ThrowException_WhenValueIsNullOrEmpty(string? correctAnswer)
    {
        //Arrange
        CorrectAnswer Action() => CorrectAnswer.Create(correctAnswer);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<CorrectAnswerIsNullDomainException>().Which.Message.Should().Be("correctAnswer can't be null");
    }

    [Fact]
    public void WrongAnswersList_Create_Should_ThrowException_WhenValueIsNull()
    {
        //Arrange
        static WrongAnswers Action() => WrongAnswers.Create(null);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<WrongAnswersIsNullDomainException>().Which.Message.Should().Be("wrongAnswers can't be null");
    }

    [Theory]
    [MemberData(nameof(invalidWrongAnswerValues))]
    public void WrongAnswersList_Create_Should_ThrowException_WhenValueIsInvalid(Collection<WrongAnswer>? wrongAnswers)
    {
        //Arrange
        WrongAnswers Action() => WrongAnswers.Create(wrongAnswers);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<WrongAnswersDoesNotContainThreeElementsDomainException>().Which.Message.Should().Be("wrongAnswers must contain 3 elements");
    }

    [Fact]
    public void Answers_Create_Should_ThrowException_WhenValueIsInvalid()
    {
        //Arrange
        var questionId = QuestionId.Create(Guid.NewGuid());
        var correctAnswer = CorrectAnswer.Create("Yes");
        ICollection<WrongAnswer> wrongAnswersList =
            [WrongAnswer.Create(questionId, "Yes"),
                WrongAnswer.Create(questionId, "Maybe"),
                WrongAnswer.Create(questionId, "Impossible")];
        var wrongAnswers = WrongAnswers.Create(wrongAnswersList);
        Answers Action() => Answers.Create(correctAnswer, wrongAnswers);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<WrongAnswersContainsCorrectAnswerDomainException>().Which.Message.Should().Be("wrongAnswers can't contain correctAnswer");
    }

    [Fact]
    public void Themes_Create_Should_ThrowException_WhenValueIsNull()
    {
        //Arrange
        static Themes Action() => Themes.Create(null);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<TopicsIsNullDomainException>().Which.Message.Should().Be("themes can't be null");
    }
}
