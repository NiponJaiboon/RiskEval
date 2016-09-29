using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public class Message
    {
        private LS[] LanguageStrings;

        public Message(params LS[] languageStrings)
        {
            this.LanguageStrings = languageStrings;
        }

        public string Format(string languageCode, params object[] formattingValues)
        {
            if (LanguageStrings.Length == 0)
                return null;

            if (LanguageStrings.Length == 1)
                return String.Format(LanguageStrings[0].LanguageString, formattingValues);

            foreach (LS ls in LanguageStrings)
            {
                if (ls.LanguageCode == languageCode)
                    return String.Format(ls.LanguageString, formattingValues);
            }

            return null;
        }
    }
}
