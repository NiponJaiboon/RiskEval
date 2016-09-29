using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public class LS
    {
        public LS(string languageCode, string stringValue)
        {
            this.LanguageCode = languageCode;
            this.LanguageString = stringValue;
        }

        public string LanguageCode;
        public string LanguageString;
    }
}
