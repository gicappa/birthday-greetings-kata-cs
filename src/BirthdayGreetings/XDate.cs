namespace BirthdayGreetings;

public record XDate(int Day, int Month)
{
    public XDate() : this(DateTime.Today.Day, DateTime.Today.Month)
    {
    }

    public XDate(string yyyyMMdd) : this(int.Parse(yyyyMMdd[8..]), int.Parse(yyyyMMdd.Substring(5, 2)))
    {
    }

    public bool IsSameDay(XDate anotherDate)
        => anotherDate.Day == Day && anotherDate.Month == Month;
}