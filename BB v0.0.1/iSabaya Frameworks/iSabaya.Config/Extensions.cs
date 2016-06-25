using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public static class Extensions
    {

        public static MultilingualString Clone(this MultilingualString original)
        {
            if (MultilingualString.IsNullOrEmpty(original))
                return null;
            else
                return new MultilingualString(original);
        }
    }
}
