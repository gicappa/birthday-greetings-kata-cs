namespace BirthdayGreetings;

internal class Employee
{
    internal Employee(string firstName, string lastName, string birthDate, string email) :
        this(firstName, lastName, new XDate(birthDate), email)
    {
    }

    private Employee(string FirstName, string LastName, XDate BirthDate, string Email)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.BirthDate = BirthDate;
        this.Email = Email;
    }

    public string FirstName { get; init; }
    public string LastName { get; init; }
    private XDate BirthDate { get; init; }
    public string Email { get; init; }

    internal bool IsBirthday(XDate today)
        => BirthDate.IsSameDay(today);

    public override bool Equals(object obj)
    {
        if (obj is not Employee other)
            return false;

        return
            BirthDate.Equals(other.BirthDate)
            && FirstName == other.FirstName
            && Email == other.Email
            && BirthDate.Equals(other.BirthDate);
    }

    public override int GetHashCode()
    {
        return Email.GetHashCode();
    }
}