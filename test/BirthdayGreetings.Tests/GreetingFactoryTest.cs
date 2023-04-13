using NUnit.Framework;

namespace BirthdayGreetings.Tests;

public class GreetingFactoryTest
{
    private IGreetingsFactory _greetingsFactory;

    [SetUp]
    public void SetUp()
    {
        _greetingsFactory = new GreetingsFactory();
    }

    [Test]
    public void salutation_is_hardcoded()
    {
        var greetings = _greetingsFactory.MakeFor(new Employee("John", "Doh", "2022-11-11", "some-email@email.com"));
        
        Assert.That(greetings.Salutation, Is.EqualTo("Happy Birthday!"));
    }
    
    [Test]
    public void message_mentions_employee_name()
    {
        var greetings = _greetingsFactory.MakeFor(new Employee("John", "Doh", "2022-11-11", "some-email@email.com"));
        
        Assert.That(greetings.Message, Contains.Substring("John"));
    }
}