using System;
using System.Globalization;

namespace SegamApp
{
    public static class PersianConvertor
    {
        public static string[] ToShamsi(this DateTime value)
        {
            string[] datetime = new string[2];
            PersianCalendar pc = new PersianCalendar();

            datetime[0] = pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" + pc.GetDayOfMonth(value).ToString("00");
            datetime[1] = pc.GetHour(value).ToString("00") + ":" + pc.GetMinute(value).ToString("00");
            return datetime;
        }
    }
}