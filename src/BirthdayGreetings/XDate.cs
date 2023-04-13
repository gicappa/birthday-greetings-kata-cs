namespace BirthdayGreetings;

internal record XDate(int Day, int Month)
{
    internal XDate() : this(DateTime.Today.Day, DateTime.Today.Month)
    {
    }

    internal XDate(string yyyyMMdd) : this(int.Parse(yyyyMMdd[8..]), int.Parse(yyyyMMdd.Substring(5, 2)))
    {
    }

    internal bool IsSameDay(XDate anotherDate)
        => anotherDate.Day == Day && anotherDate.Month == Month;
}