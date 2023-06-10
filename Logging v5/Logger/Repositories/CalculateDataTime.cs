using Logging_v5.Logger.Services;
using System;
using System.Globalization;

namespace Logging_v5.Logger.Repositories
{
    public class CalculateDataTime : ICalculateDataTime
    {

        public int CalculateGregorianDay(DateTime date)
        {
            return date.DayOfYear;
        }

        public DateTime ToPersian(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            var m = pc.GetMonth(date).ToString().Length < 2 ? "0" + pc.GetMonth(date).ToString() : pc.GetMonth(date).ToString();
            var d = pc.GetDayOfMonth(date).ToString().Length < 2 ? "0" + pc.GetDayOfMonth(date).ToString() : pc.GetDayOfMonth(date).ToString();
            var y = pc.GetYear(date).ToString();
            var time =  $"{y}/{m}/{d}";
            return DateTime.Parse(time);
        }
    }
}
