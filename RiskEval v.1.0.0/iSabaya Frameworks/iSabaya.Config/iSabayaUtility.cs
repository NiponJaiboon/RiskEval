using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public class iSabayaUtility
    {
        public static readonly String[] OrdinalIndicator = 
        { 
            "th", //0
            "st", //1
            "nd", //2
            "rd", //3
            "th", //4
            "th", //5
            "th", //6
            "th", //7
            "th", //8
            "th", //9
        };

        public static String ConvertToOrdinalNumberString(int i)
        {
            i = Math.Abs(i) % 100;
            if (10 < i && i < 21)
                return i.ToString() + "th"; //Teen numbers
            else
                return i.ToString() + OrdinalIndicator[i % 10];
        }

        public static DateTime EOM(int year, int month)
        {
            if (month == 12)
                return new DateTime(year, 12, 31);
            else
                return new DateTime(year, month + 1, 1).AddDays(-1);
        }
    }
}
