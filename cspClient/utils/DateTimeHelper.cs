using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cspClient.utils
{
    class DateTimeHelper
    {
        public static String ConvertStringToDateTime(string timeStamp)
        {
            try {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(timeStamp + "0000");
                TimeSpan toNow = new TimeSpan(lTime);
                return dtStart.Add(toNow).ToString();
            }
            catch (Exception e) {
                return "";
            }
            
        }
    }
}
