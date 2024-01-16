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
    var guid = Guid.NewGuid();
    var text = "Is this a question ?";
    var correctAnswer = "Yes";
    List<string> wrongAnswers = ["No", "Maybe", "Impossible"];
    List<Topic> topics = [Topic.Literature];
    var difficulty = Difficulty.Beginner;
    var question = Question.Create(new QuestionId(guid),
            new Title(text),
            Answers.Create(new CorrectAnswer(correctAnswer),
                            WrongAnswers.Create(wrongAnswers)),
            Topics.Create(topics),
            difficulty);
    return question;
})
.WithName("GetQuestion")
.WithOpenApi();

app.Run();