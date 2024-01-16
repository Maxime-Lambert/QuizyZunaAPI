using System.Collections.ObjectModel;

using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.Exceptions;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

namespace QuizyZunaAPI.Domain.UnitTests.Questions;

public class QuestionTests
{
    public static TheoryData<Collection<string>> invalidWrongAnswerValues =>
        new()
        {
            new Collection<string>(),
            new Collection<string>() { "Answer 1" },
            new Collection<string>() { "Answer 1", "Answer 2" },
            new Collection<string>() { "Answer 1", "Answer 2", "Answer 3", "Answer 4" }
        };

    [Fact]
    public void Question_Create_Should_Suceed_WhenValueIsValid()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var text = "Is this a question ?";
        var correctAnswer = "Yes";
        List<string> wrongAnswers = ["No", "Maybe", "Impossible"];
        List<Topic> topics = [Topic.Literature];
        var difficulty = Difficulty.Beginner;

        //Act
        var result = Question.Create(new QuestionId(guid), 
            new Title(text), 
            Answers.Create(new CorrectAnswer(correctAnswer), 
                            WrongAnswers.Create(wrongAnswers)),
            Topics.Create(topics),
            difficulty);

        //Assert
        result.QuestionId.Value.Should().Be(guid);
        result.Text.Value.Should().Be(text);
        result.Answers.CorrectAnswer.Value.Should().Be(correctAnswer);
        result.Answers.WrongAnswers.Value.Should().BeEquivalentTo(wrongAnswers);
        result.Topics.Value.Should().BeEquivalentTo(topics);
        result.Difficulty.Should().Be(difficulty);
        result.Era.Should().Be(Era.None);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void CorrectAnswer_Create_Should_ThrowException_WhenValueIsNullOrEmpty(string? correctAnswer)
    {
        //Arrange
        CorrectAnswer Action() => new(correctAnswer);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<CorrectAnswerIsNullDomainException>().Which.Message.Should().Be("correctAnswer can't be null");
    }

    [Theory]
    [MemberData(nameof(invalidWrongAnswerValues))]
    public void WrongAnswers_Create_Should_ThrowException_WhenValueIsInvalid(Collection<string>? wrongAnswers)
    {
        //Arrange
        WrongAnswers Action() => WrongAnswers.Create(wrongAnswers);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<WrongAnswersDoesNotContainThreeElementsDomainException>().Which.Message.Should().Be("wrongAnswers must contain 3 elements");
    }

    [Fact]
    public void WrongAnswers_Create_Should_ThrowException_WhenValueIsNull()
    {
        //Arrange
        static WrongAnswers Action() => WrongAnswers.Create(null);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<WrongAnswersIsNullDomainException>().Which.Message.Should().Be("wrongAnswers can't be null");
    }

    [Fact]
    public void Topics_Create_Should_ThrowException_WhenValueIsNull()
    {
        //Arrange
        static Topics Action() => Topics.Create(null);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<TopicsIsNullDomainException>().Which.Message.Should().Be("topics can't be null");
    }

    [Fact]
    public void Answers_Create_Should_ThrowException_WhenValueIsInvalid()
    {
        //Arrange
        var correctAnswer = "Yes";
        List<string> wrongAnswers = ["Yes", "Maybe", "Impossible"];
        Answers Action() => Answers.Create(new CorrectAnswer(correctAnswer), WrongAnswers.Create(wrongAnswers));

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<WrongAnswersContainsCorrectAnswerDomainException>().Which.Message.Should().Be("wrongAnswers can't contain correctAnswer");
    }
}
