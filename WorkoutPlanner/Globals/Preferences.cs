using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace WorkoutPlanner.Globals
{
    public static class Preferences
    {
        public static string DateFormat
        {
            get { return Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern; }
        }

        public static string Language
        {
            get { return Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName; }
        }
    }
}