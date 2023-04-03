namespace BirthdayGreetings;

	public class XDate
	{
		readonly int day;
		readonly int month;

		public XDate()
		{
			var today = DateTime.Today;
			day = today.Day;
			month = today.Month;
		}

		public XDate(String yyyyMMdd)
		{
			day = int.Parse(yyyyMMdd[8..]);
			month = int.Parse (yyyyMMdd.Substring(5, 2));
		}

		public int GetDay()
		{
			return day;
		}

		public int GetMonth()
		{
			return month;
		}

		public bool IsSameDay(XDate anotherDate) 
		{
			return anotherDate.GetDay() == this.GetDay() && anotherDate.GetMonth() == this.GetMonth();
		}

		public override int GetHashCode ()
		{
			return day;
		}

		public override bool Equals(Object obj)
		{
        if (obj is not XDate other)
            return false;

        return this.day == other.day && this.month == other.month;
		}

		public override string ToString ()
		{
			return string.Format ("{0}/{1}", day, month);
		}
	}
