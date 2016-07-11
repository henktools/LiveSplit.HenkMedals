using System;

namespace LiveSplit.TimeFormatters
{
    class RegularHenkMedalsTimeFormatter : ITimeFormatter
    {
        public string Format(TimeSpan? time)
        {
            var formatter = new RegularTimeFormatter(TimeAccuracy.Hundredths);
            if (time == null)
                return "-";
            else
                return formatter.Format(time);
        }
    }
}
