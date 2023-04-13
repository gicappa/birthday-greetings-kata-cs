namespace BirthdayGreetings;

class GreetingsFactory : IGreetingsFactory
{
    Greetings IGreetingsFactory.MakeFor(Employee employee)
    {
        return new Greetings
        {
            Salutation = "Happy Birthday!",
            Message = $"Happy Birthday, dear {employee.FirstName}!",
        };
    }
}