namespace StrongAppData.Extensions
{
    using System;

    public class WeekNumber
    {
        public int Year { get; set; }
        public int Week { get; set; }
    }

    public static class DateTimeExtensions
    {
        public static WeekNumber GetWeekNumber(this DateTime d)
        {
            DateTime dt = new DateTime(d.Year, 1, 1);
            DateTime dt2 = dt;
            while (dt2.DayOfWeek != DayOfWeek.Monday)
            {
                dt2 = dt2.AddDays(1);
            }
            int weekOffset = dt2.DayOfYear - dt.DayOfYear;

            int week = (
                (d.DayOfYear
                - (weekOffset >= 4 ? weekOffset - 7 : weekOffset)
                + 6) / 7);

            if (week == 0)
            {
                return GetWeekNumber(new DateTime(d.Year - 1, 12, 31));
            }

            return new WeekNumber
            {
                Year = d.Year,
                Week = week
            };
        }
    }
}
