using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Common.Helpers
{
    public static class DateHelper
    {
        public static DateTime StartOfMonth(int year, int month) =>
            new DateTime(year, month, 1);

        public static DateTime EndOfMonth(int year, int month) =>
            new DateTime(year, month, DateTime.DaysInMonth(year, month));

        public static DateTime StartOfYear(int year) =>
            new DateTime(year, 1, 1);

        public static DateTime EndOfYear(int year) =>
            new DateTime(year, 12, 31);

        public static bool IsSameMonth(DateTime a, DateTime b) =>
            a.Year == b.Year && a.Month == b.Month;

        public static int DaysInRange(DateTime start, DateTime end) =>
            (end.Date - start.Date).Days + 1;
    }
}
