using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaMaster.PowerShell
{
    public static class DateExtensions
    {
        public static string ToRelativeString(this DateTimeOffset offset)
        {
            var days = (DateTime.Today - offset.Date).Days;

            if (days < 1)
            {
                return "heute";
            }

            if (days < 2)
            {
                return "gestern";
            }

            if (days < 3)
            {
                return "vorgestern";
            }

            if (days < 7)
            {
                return $"vor {days} Tagen";
            }

            if (days < 14)
            {
                return "vor einer Woche";
            }

            return $"vor {days / 7} Wochen";
        }
    }
}