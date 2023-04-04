namespace BirthdayGreetings;

public class XDate
{
  public XDate()
  {
    var today = DateTime.Today;
    Day = today.Day;
    Month = today.Month;
  }

  public XDate(string yyyyMMdd)
  {
    Day = int.Parse(yyyyMMdd[8..]);
    Month = int.Parse(yyyyMMdd.Substring(5, 2));
  }

  public int Day { get; }

  public int Month { get; }

  public bool IsSameDay(XDate anotherDate)
    => anotherDate.Day == this.Day && anotherDate.Month == this.Month;

  public override int GetHashCode() => Day;

  public override bool Equals(Object? obj)
  {
    if (obj is not XDate other)
      return false;

    return this.Day == other.Day && this.Month == other.Month;
  }

  public override string ToString() => $"{Day}/{Month}";
}
