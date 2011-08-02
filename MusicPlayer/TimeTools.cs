using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Music
{
    static class TimeTools
    {
        public static string SecondsToString(int seconds)
        {
            string timeString ="";

            //int seconds2 = seconds;
            int minutes = seconds / 60;
            int remSeconds = seconds - (minutes * 60);
            if (remSeconds < 10)
            {
                timeString = minutes + ":0" + remSeconds;
            }
            else
            {
                timeString = minutes + ":" + remSeconds;
            }
            Console.WriteLine(timeString);
            return timeString;
        }
    }
}
