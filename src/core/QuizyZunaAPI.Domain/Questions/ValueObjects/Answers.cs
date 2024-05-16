namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record Answers
{
    public CorrectAnswer CorrectAnswer { get; private init; } = null!;

    public WrongAnswers WrongAnswers { get; private init; } = null!;

    private Answers() { }

    public Answers(CorrectAnswer correctAnswer, WrongAnswers wrongAnswers)
    {
        ArgumentNullException.ThrowIfNull(correctAnswer);
        ArgumentNullException.ThrowIfNull(wrongAnswers);

        wrongAnswers.ThrowExceptionIfCorrectAnswerIsPresent(correctAnswer);

        CorrectAnswer = correctAnswer;
        WrongAnswers = wrongAnswers;
    }
};