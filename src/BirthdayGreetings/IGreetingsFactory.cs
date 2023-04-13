namespace BirthdayGreetings;

interface IGreetingsFactory
{
    Greetings MakeFor(Employee employee);
}