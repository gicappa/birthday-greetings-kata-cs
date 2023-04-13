namespace BirthdayGreetings;

internal record Employee(string FirstName, string LastName, XDate BirthDate, string Email)
{
    internal Employee(string firstName, string lastName, string birthDate, string email) :
        this(firstName, lastName, new XDate(birthDate), email)
    {
    }

    internal bool IsBirthday(XDate today)
        => BirthDate.IsSameDay(today);
}