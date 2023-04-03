using NUnit.Framework;
using System;
using BirthdayGreetings;

namespace BirthdayGreetingsTests
{
  [TestFixture]
  public class XDateTest
  {
    [Test]
    public void Getters()
    {
      XDate date = new("1789/01/24");
      Assert.Multiple(() =>
      {
        Assert.That(date.GetDay(), Is.EqualTo(24));
        Assert.That(date.GetMonth(), Is.EqualTo(1));
      });
    }

    [Test]
    public void IsSameDate()
    {
      XDate date = new("1789/01/24");
      XDate sameDay = new("2001/01/24");
      XDate notSameDay = new("1789/01/25");
      XDate notSameMonth = new("1789/02/25");
      Assert.Multiple(() =>
      {
        Assert.That(date.IsSameDay(sameDay), Is.True, "same");
        Assert.That(date.IsSameDay(notSameDay), Is.False, "not same day");
        Assert.That(date.IsSameDay(notSameMonth), Is.False, "not same month");
      });
    }

    [Test]
    public void EqualityTest()
    {
      XDate date = new("2000/01/02");
      XDate same = new("2000/01/02");
      XDate different = new("2000/01/04");
      Assert.Multiple(() =>
      {
        Assert.That(date, Is.Not.EqualTo(null));
        // Assert.That(date, Is.Not.EqualTo(""));
        Assert.That(date, Is.EqualTo(date));
        Assert.That(date, Is.EqualTo(same));
        Assert.That(date, Is.Not.EqualTo(different));
      });
    }

    [Test]
    public void TodaysDate()
    {
      XDate date = new();
      Assert.Multiple(() =>
      {
        Assert.That(date.GetDay(), Is.EqualTo(DateTime.Today.Day));
        Assert.That(date.GetMonth(), Is.EqualTo(DateTime.Today.Month));
      });
    }
  }
}
