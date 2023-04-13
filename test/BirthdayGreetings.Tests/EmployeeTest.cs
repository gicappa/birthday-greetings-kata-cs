using NUnit.Framework;
using System;
using BirthdayGreetings;

namespace BirthdayGreetings.Tests;

[TestFixture]
public class EmployeeTest
{
    [Test]
    public void TestBirthday()
    {
        Employee employee = new("foo", "bar", "1990/01/31", "a@b.c");
        Assert.Multiple(() =>
        {
            Assert.That(employee.IsBirthday(new XDate("2008/01/30")), Is.False, "not his birthday");
            Assert.That(employee.IsBirthday(new XDate("2008/01/31")), Is.True, "his birthday");
        });
    }

    [Test]
    public void Equality()
    {
        Employee employee = new("First", "Last", "1999/09/01", "first@last.com");
        Employee same = new("First", "Last", "1999/09/01", "first@last.com");
        Employee differentEmail = new("First", "Last", "1999/09/01", "boom@boom.com");

        Assert.Multiple(() =>
        {
            Assert.That(employee, Is.Not.Null);
            Assert.That(same, Is.EqualTo(employee));
            Assert.That(differentEmail, Is.Not.EqualTo(employee));
        });
    }
}