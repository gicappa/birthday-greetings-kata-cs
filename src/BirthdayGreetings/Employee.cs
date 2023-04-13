namespace BirthdayGreetings;

public record Employee(string FirstName, string LastName, XDate BirthDate, string Email)
{
    public Employee(string firstName, string lastName, string birthDate, string email) :
        this(firstName, lastName, new XDate(birthDate), email)
    {
    }

    public bool IsBirthday(XDate today)
        => BirthDate.IsSameDay(today);
}