namespace QuizyZunaAPI.Domain.Questions.ValueObjects;

public sealed record TimesAnswered
{
    public int? Value { get; private set; }

    private TimesAnswered() { }

    public void AddOne()
    {
        Value += 1;
    }

    public TimesAnswered(int? timesAnswered)
    {
        Value = timesAnswered;
    }
}
