using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace iSabaya
{
    public class iSabayaException : Exception
    {
        public iSabayaException()
            : base()
        {
        }

        public iSabayaException(String message)
            : base(message, new Exception(CreateTraceInfo()))
        {
        }
        
        public iSabayaException(String message, Exception innerException)
            : base(message, new Exception(CreateTraceInfo(), innerException))
        {
        }

        private static string CreateTraceInfo()
        {
            StackTrace st = new StackTrace(true);
            StackFrame sf = st.GetFrame(2);
            MethodBase methodInfo = sf.GetMethod();
            return "";
            //return sf.GetFileName() + "/" + methodInfo.DeclaringType + "." + methodInfo.Name + ": line " + sf.GetFileLineNumber();
        }
    }
}
