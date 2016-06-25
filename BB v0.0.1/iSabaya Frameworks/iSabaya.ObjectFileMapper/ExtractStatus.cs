using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace iSabaya.ObjectFileMapper
{
    public class ExtractStatus
    {
        public ExtractStatus(string status)
        {
            this.Status = status;
        }
        public string Status;
        public string Message;

        public override string ToString()
        {
            return this.Status;
        }

        public static ExtractStatus EndOfFile = new ExtractStatus("EndOfFile");
        public static ExtractStatus Success = new ExtractStatus("Success");
        public static ExtractStatus SkipTheRest = new ExtractStatus("SkipTheRest");
        public static ExtractStatus ValueMismatched = new ExtractStatus("SignatureValueMismatched");
        public static ExtractStatus Failed = new ExtractStatus("Failed");
        //Success
        //SkipTheRest,
        ////EndOfStream,
        ////Exhausted,
        //ValueMismatched, //Signature value
        //Failed,
    }
}
