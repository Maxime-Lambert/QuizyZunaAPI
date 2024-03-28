namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record Answers
{
    public CorrectAnswer CorrectAnswer { get; private init; }

    public WrongAnswers WrongAnswers { get; private init; }

    private Answers() { }

    public Answers(CorrectAnswer correctAnswer, WrongAnswers wrongAnswers)
    {
        wrongAnswers.ThrowExceptionIfCorrectAnswerIsPresent(correctAnswer);

        CorrectAnswer = correctAnswer;
        WrongAnswers = wrongAnswers;
    }
};