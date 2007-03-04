using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.LastFM
{
    public static class DateTimeUtil
    {
		#region Public Static Fields
        public static readonly DateTime LocalUnixEpoch = new DateTime(1970, 1, 1).ToLocalTime();
        #endregion

		#region Public Static Methods
        public static DateTime ToDateTime(long time)
        {
            return FromTimeT(time);
        }

        public static long FromDateTime(DateTime time)
        {
            return ToTimeT(time);
        }

        public static DateTime FromTimeT(long time)
        {
            return LocalUnixEpoch.AddSeconds(time);
        }

        public static long ToTimeT(DateTime time)
        {
            return (long)time.Subtract(LocalUnixEpoch).TotalSeconds;
        }

        public static string FormatDuration(long time)
        {
            return (time > 3600 ?
                    String.Format("{0}:{1:00}:{2:00}", time / 3600, (time / 60) % 60, time % 60) :
                    String.Format("{0}:{1:00}", time / 60, time % 60));
        }
        #endregion
    }
}
