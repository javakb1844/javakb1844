using System;
using System.Collections.Generic;

public static class ExDateTime
{
    public static string ToRelativeDate(this DateTime theDate)
    {
        Dictionary<long, string> thresholds = new Dictionary<long, string>();
        int minute = 60;
        int hour = 60 * minute;
        int day = 24 * hour;
        thresholds.Add(60, "{0} seconds ago");
        thresholds.Add(minute * 2, "a minute ago");
        thresholds.Add(45 * minute, "{0} minutes ago");
        thresholds.Add(120 * minute, "an hour ago");
        thresholds.Add(day, "{0} hours ago");
        thresholds.Add(day * 2, "yesterday");
        thresholds.Add(day * 30, "{0} days ago");
        thresholds.Add(day * 365, "{0} months ago");
        thresholds.Add(long.MaxValue, "{0} years ago");

        long since = (DateTime.UtcNow.Ticks - theDate.Ticks) / 10000000;
        foreach (long threshold in thresholds.Keys)
        {
            if (since < threshold)
            {
                TimeSpan t = new TimeSpan((DateTime.UtcNow.Ticks - theDate.Ticks));
                return string.Format(thresholds[threshold], (t.Days > 365 ? t.Days / 365 : (t.Days > 0 ? t.Days : (t.Hours > 0 ? t.Hours : (t.Minutes > 0 ? t.Minutes : (t.Seconds > 0 ? t.Seconds : 0))))).ToString());
            }
        }
        return "";
    }

    public static string ToSlashForamt_dd_mm_yyyy(this DateTime o)
    {
        return string.Format("{0}/{1}/{2}",
            o.Day.ts().ToTwoDigits(),
            o.Month.ts().ToTwoDigits(),
            o.Year.ts());
    }

    public static string ToSlashForamt_mm_dd_yyyy(this DateTime o)
    {
        return string.Format("{0}/{1}/{2}",
            o.Month.ts().ToTwoDigits(),
            o.Day.ts().ToTwoDigits(),
            o.Year.ts());
    }//2010-04-18 13:03:44.957

    public static string ToDirNameFormat(this DateTime o)
    {
        return string.Format("{0}-{1}-{2}",
            o.Day.ts().ToTwoDigits(),
            o.Month.ts().ToTwoDigits(),
            o.Year.ts());
    }// Dir name complaint function

    public static string ToSqlQueryFormat(this DateTime dt)
    {
        return string.Format("{0}-{1}-{2} {3}:{4}:{5}.{6}", dt.Year, dt.Month.ToTwoDigits(), dt.Day.ToTwoDigits(),
            dt.Hour.ToTwoDigits(), dt.Minute.ToTwoDigits(), dt.Second.ToTwoDigits(), dt.Millisecond);
    }

    public static DateTime ToAustralianDateTime(this DateTime utcDateTime)
    {
        TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
        DateTime utcToLocal = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, tzi);
        return utcToLocal;
    }

    public static DateTime ToLocalTimeFromUtcDateTime(this DateTime utcDateTime)
    {
        DateTime convertedDate = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
        return convertedDate.ToLocalTime();
    }

    public static DateTime ToLocalTimeFromUtcDateTime(this DateTime utcDateTime, string browserTimeZoneId)
    {
        DateTime convertedDate = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
        TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(browserTimeZoneId);
        DateTime utcToLocal = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, tzi);
        return utcToLocal;
    }

    public static DateTime FromAustralianToUtcDateTime(this DateTime dateTime)
    {
        TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
        DateTime utcToLocal = TimeZoneInfo.ConvertTimeToUtc(dateTime, tzi);
        return utcToLocal;
    }

    public static string ToTimeAgo(this DateTime userDate)
    {
        try
        {
            const int SECOND = 1;
            const int MINUTE = 60*SECOND;
            const int HOUR = 60*MINUTE;
            const int DAY = 24*HOUR;
            const int MONTH = 30*DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - userDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 0)
            {
                return "";
            }
            //if (DateTime.UtcNow.Date == userDate.Date)
            //{
            //    return userDate.ToString("hh:mm tt");
            //}
            if (delta < 1*MINUTE)
            {
                //return ts.Seconds == 1 ? "1 second ago" : ts.Seconds + " seconds ago";
                return ts.Seconds == 1 ? "1 second ago" : ts.Seconds < 2 ? "2 seconds ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 2*MINUTE)
            {
                return "1 minute ago";
            }
            if (delta < 45*MINUTE)
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 120*MINUTE)
            {
                return "an hour ago";
            }
            if (delta < 24*HOUR)
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 48*HOUR)
            {
                return "yesterday";
            }
            if (delta < 30*DAY)
            {
                if (delta < 7*DAY)
                {
                    return ts.Days + " days ago";
                }
                else
                {
                    int week = 1;
                    if (ts.Days < 14)
                    {
                        week = 1;
                    }
                    else if (ts.Days < 21)
                    {
                        week = 2;
                    }
                    else if (ts.Days < 28)
                    {
                        week = 3;
                    }
                    else if (ts.Days < 30)
                    {
                        week = 4;
                    }

                    if (week == 1)
                    {
                        return week + " week ago";
                    }
                    else
                    {
                        return week + " weeks ago";
                    }
                }
            }
            if (delta < 12*MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double) ts.Days/30));
                return months <= 1 ? "1 month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double) ts.Days/365));
                return years <= 1 ? "1 year ago" : years + " years ago";
            }
        }
        catch
        {
            return "";
        }
    }

    public static string FriendlyDate(this DateTime theDate)
    {
        string friendlyDate;
        var number = theDate.Day; 
        if (number < 0) return number.ToString();
        switch (number % 100)
        {
            case 11:
            case 12:
            case 13:
            friendlyDate = number + "th";
                 break;

        }
        switch (number % 10)
        {
            case 1:
                friendlyDate = number +"st";
                break;
            case 2:
                friendlyDate = number + "nd";
                break;
            case 3:
                friendlyDate = number + "rd";
                break;
            default:
                friendlyDate = number + "th";
                break;
        }

        return theDate.ToString("ddd") + ", " + friendlyDate + " " + theDate.ToString("MMMM");
    }

    //TODO: TimeAgo Remove or Comment method and where its being used, need to comment that.
    public static double ToUnixTimestamp(this DateTime dateTime)
    {
        return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
    }

    public static string ToFullDateWithMilliSeconds(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy/MM/dd hh.mm.ss.fff tt");
    }


}

