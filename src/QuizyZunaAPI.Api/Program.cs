using Serilog;

using QuizyZunaAPI.Presentation;
using QuizyZunaAPI.Application;
using QuizyZunaAPI.Persistence;
using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddPersistence()
    .AddPresentation();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.MapGet("/questions", () =>
{
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

    return Question.Create(questionId, title, answers, questionTags);
})
.WithName("GetQuestion")
.WithOpenApi();

app.Run();