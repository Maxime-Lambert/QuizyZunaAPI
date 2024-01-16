using Serilog;

using QuizyZunaAPI.Presentation;
using QuizyZunaAPI.Infrastructure;
using QuizyZunaAPI.Application;
using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Enumerations;
using QuizyZunaAPI.Domain.Questions.ValueObjects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure()
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
    var id = QuestionId.Create(Guid.NewGuid());
    var title = Title.Create("Is this a question ?");
    var correctAnswer = CorrectAnswer.Create("Yes");
    var wrongAnswers = WrongAnswers.Create(["No", "Maybe", "Impossible"]);
    var answers = Answers.Create(correctAnswer, wrongAnswers);
    var topics = Topics.Create([Topic.Literature]);
    var difficulty = Difficulty.Beginner;

    return Question.Create(id, title, answers, topics, difficulty);
})
.WithName("GetQuestion")
.WithOpenApi();

app.Run();