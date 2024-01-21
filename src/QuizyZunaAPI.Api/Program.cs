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

builder.Services.AddPersistence()
                .AddApplication()
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

    return Question.Create(questionId, title, answers, questionTags);
})
.WithName("GetQuestion")
.WithOpenApi();

app.Run();