using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHelper.Security
{
    public class ExceptionMessage
    {
        public ExceptionMessage()
        {
        }

        public static Exception LastException
        {
            get
            {
                return _lastException;
            }
            set
            {
                if (value != _lastException)
                {
                    _lastException = value;
                }
            }
        }

        private static Exception _lastException;
    }
}