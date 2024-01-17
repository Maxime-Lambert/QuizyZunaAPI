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
        var questionId = QuestionId.Create(Guid.NewGuid());
        var wrongAnswersId = WrongAnswersId.Create(Guid.NewGuid());
        var topicsId = TopicsId.Create(Guid.NewGuid());
        var title = QuestionTitle.Create("Is this a question ?");
        var correctAnswer = CorrectAnswer.Create("Yes");
        var wrongAnswersList = WrongAnswersList.Create(["No", "Maybe", "Impossible"]);
        var wrongAnswers = WrongAnswers.Create(wrongAnswersId, questionId, wrongAnswersList);
        var answers = Answers.Create(correctAnswer, wrongAnswers);
        var topicsList = TopicsList.Create([Topic.Literature]);
        var topics = Topics.Create(topicsId, questionId, topicsList);
        var difficulty = Difficulty.Beginner;
        var questionTags = QuestionTags.Create(topics, difficulty);

        //Act
        var result = Question.Create(questionId, title, answers, questionTags);

        //Assert
        result.Id.Value.Should().Be(questionId.Value);
        result.Title.Value.Should().Be(title.Value);
        result.Answers.CorrectAnswer.Value.Should().Be(correctAnswer.Value);
        result.Answers.WrongAnswers.List.Value.Should().BeEquivalentTo(wrongAnswersList.Value);
        result.Tags.Topics.List.Value.Should().BeEquivalentTo(topicsList.Value);
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
        static WrongAnswersList Action() => WrongAnswersList.Create(null);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<WrongAnswersIsNullDomainException>().Which.Message.Should().Be("wrongAnswersList can't be null");
    }

    [Theory]
    [MemberData(nameof(invalidWrongAnswerValues))]
    public void WrongAnswersList_Create_Should_ThrowException_WhenValueIsInvalid(Collection<string>? wrongAnswers)
    {
        //Arrange
        WrongAnswersList Action() => WrongAnswersList.Create(wrongAnswers);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<WrongAnswersDoesNotContainThreeElementsDomainException>().Which.Message.Should().Be("wrongAnswersList must contain 3 elements");
    }

    [Fact]
    public void Answers_Create_Should_ThrowException_WhenValueIsInvalid()
    {
        //Arrange
        var correctAnswer = CorrectAnswer.Create("Yes");
        var wrongAnswersList = WrongAnswersList.Create(["Yes", "Maybe", "Impossible"]);
        var questionId = QuestionId.Create(Guid.NewGuid());
        var wrongAnswersId = WrongAnswersId.Create(Guid.NewGuid());
        var wrongAnswers = WrongAnswers.Create(wrongAnswersId, questionId, wrongAnswersList);
        Answers Action() => Answers.Create(correctAnswer, wrongAnswers);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<WrongAnswersContainsCorrectAnswerDomainException>().Which.Message.Should().Be("wrongAnswers can't contain correctAnswer");
    }

    [Fact]
    public void TopicsList_Create_Should_ThrowException_WhenValueIsNull()
    {
        //Arrange
        static TopicsList Action() => TopicsList.Create(null);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<TopicsIsNullDomainException>().Which.Message.Should().Be("topicsList can't be null");
    }

    [Fact]
    public void TopicsId_Create_Should_ThrowException_WhenValueIsNull()
    {
        //Arrange
        static TopicsId Action() => TopicsId.Create(null);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<TopicsIdIsNullDomainException>().Which.Message.Should().Be("topicsId can't be null");
    }

    [Fact]
    public void WrongAnswersId_Create_Should_ThrowException_WhenValueIsNull()
    {
        //Arrange
        static WrongAnswersId Action() => WrongAnswersId.Create(null);

        //Act
        var result = FluentActions.Invoking(Action);

        //Assert
        result.Should().Throw<WrongAnswersIdIsNullDomainException>().Which.Message.Should().Be("wrongAnswersId can't be null");
    }
}
