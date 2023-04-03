using System;

namespace BirthdayGreetings
{
  public class Employee
  {
    private readonly XDate birthDate;
    private readonly String lastName;
    private readonly String firstName;
    private readonly String email;

    public Employee(String firstName, String lastName, String birthDate, String email)
    {
      this.firstName = firstName;
      this.lastName = lastName;
      this.birthDate = new XDate(birthDate);
      this.email = email;
    }

    public bool IsBirthday(XDate today)
    {
      return today.IsSameDay(birthDate);
    }

    public String GetEmail()
    {
      return email;
    }

    public String GetFirstName()
    {
      return firstName;
    }

    public override bool Equals(object obj)
    {
      if (obj is not Employee other)
        return false;

      return this.birthDate.Equals(other.birthDate)
          && this.firstName == other.firstName
          && this.email == other.email
          && this.birthDate.Equals(other.birthDate);
    }

    public override int GetHashCode()
    {
      return email.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format("[Employee {0} {1} {2} {3}]", firstName, lastName, email, birthDate);
    }
  }
}

