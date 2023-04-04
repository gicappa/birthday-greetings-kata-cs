namespace BirthdayGreetings
{
  public class Employee
  {
    private readonly XDate _birthDate;
    private readonly string _lastName;

    public Employee(string firstName, string lastName, string birthDate, string email)
    {
      FirstName = firstName;
      _lastName = lastName;
      _birthDate = new XDate(birthDate);
      Email = email;
    }

    public bool IsBirthday(XDate today)
      => _birthDate.IsSameDay(today);

    public string Email { get; }

    public string FirstName { get; }

    public override bool Equals(object? obj)
    {
      if (obj is not Employee other)
        return false;

      return _birthDate.Equals(other._birthDate)
          && FirstName == other.FirstName
          && Email == other.Email
          && _birthDate.Equals(other._birthDate);
    }

    public override int GetHashCode()
    {
      return Email.GetHashCode();
    }

    public override string ToString()
    {
      return $"[Employee {FirstName} {_lastName} {Email} {_birthDate}]";
    }
  }
}
