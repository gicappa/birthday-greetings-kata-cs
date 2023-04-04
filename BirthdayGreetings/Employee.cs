namespace BirthdayGreetings
{
  public record Employee(string firstName, string lastName, XDate birthDate, string email)
  {
    public Employee(string firstName, string lastName, string birthDate, string email) : this(
      firstName, lastName, new XDate(birthDate), email) { }

    public bool IsBirthday(XDate today)
      => birthDate.IsSameDay(today);
  }
}