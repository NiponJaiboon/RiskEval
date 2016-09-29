using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace WebHelper
{
    //Obsolete
    public static class WebConstants
    {
        //  <Theme Text="Default" Value="Default" />
        //<Theme Text="Glass" Value="Glass" />
        //<Theme Text="Black Glass" Value="BlackGlass" />
        //<Theme Text="Office2003 Blue" Value="Office2003 Blue" />
        //<Theme Text="Office2003 Olive" Value="Office2003 Olive" />
        //<Theme Text="Office2003 Silver" Value="Office2003 Silver" />
        //<Theme Text="Plastic Blue" Value="Plastic Blue" />
        //<Theme Text="Red Wine" Value="Red Wine" />
        //<Theme Text="Soft Orange" Value="Soft Orange" />
        //<Theme Text="Youthful" Value="Youthful" />
        private static readonly Pair[] CssPostfixs = new Pair[]
        {
            new Pair("Default", "Default"),
            new Pair("Aqua", "Aqua"),
            new Pair("Glass", "Glass"),
            new Pair("BlackGlass", "BlackGlass"),
            new Pair("Office2003Blue", "Office2003_Blue"),
            new Pair("Office2003Olive", "Office2003_Olive"),
            new Pair("Office2003Silver", "Office2003_Silver"),
            new Pair("PlasticBlue", "PlasticBlue"),
            new Pair("RedWine", "RedWine"),
            new Pair("SoftOrange", "Soft_Orange"),
            new Pair("Youthful", "Youthful"),
        };
        public static string GetCssPostfix(string themeName)
        {
            if (!string.IsNullOrEmpty(themeName))
            {
                for (int i = 0; i < CssPostfixs.Length; i++)
                {
                    if (CssPostfixs[i].First.Equals(themeName))
                        return CssPostfixs[i].Second.ToString();
                }
            }
            return string.Empty;
        }
        public static string[] BooleanValues =
        {
            true.ToString(), false.ToString(),
        };
        public static string DateTimeCulture = "th-TH";

        //public static String Langguage = "th";
        public static String Thai = "th-TH";
        public static String English = "en-US";
        //public static String CompanyName = "ISABAYA ASSET MANAGEMENT";
    }
}
