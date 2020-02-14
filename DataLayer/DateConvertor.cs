using System;
using System.Globalization;
using System.Text;

namespace DataLayer
{
    public class DateConvertor
    {
        public static string M2SH(DateTime d)
        {
            PersianCalendar pc = new PersianCalendar();
            StringBuilder sb = new StringBuilder();
            sb.Append(pc.GetYear(d).ToString("0000"));
            sb.Append("/");
            sb.Append(pc.GetMonth(d).ToString("00"));
            sb.Append("/");
            sb.Append(pc.GetDayOfMonth(d).ToString("00"));
            return sb.ToString();
        }

        public static DateTime StartDayOfPersianMonth()
        {
            try
            {
                var temp = M2SH(DateTime.Now);
                var arr = temp.Split('/');
                return Sh2M($"{arr[0]}/{arr[1]}/01");
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static DateTime EndDayOfPersianMonth()
        {
            try
            {
                var temp = M2SH(DateTime.Now);
                var arr = temp.Split('/');
                return Sh2M($"{arr[0]}/{arr[1]}/30");
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static DateTime Sh2M(string ShamsiDate)
        {
            try
            {
                var pc = new PersianCalendar();
                var year = int.Parse(ShamsiDate.Substring(0, 4));
                var mounth = int.Parse(ShamsiDate.Substring(5, 2));
                var day = int.Parse(ShamsiDate.Substring(8, 2));
                var dt = new DateTime(year, mounth, day, pc);
                return dt;
            }
            catch
            {
                return DateTime.Now;
            }
        }
    }
}
