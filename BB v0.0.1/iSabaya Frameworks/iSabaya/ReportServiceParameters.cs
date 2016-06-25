using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class ReportServiceParameters
    {
        public ReportServiceParameters()
        {

        }
        public ReportServiceParameters(String param, String paramValue, int paramType, ReportServiceBackground report)
        {
            
            this.param = param;
            this.paramValue = paramValue;
            this.paramType = paramType;
            this.Report = report;
        }
        private ReportServiceBackground report;
        public virtual ReportServiceBackground Report
        {
            get { return report; }
            set { this.report = value; }
        }

        private int paramId;
        public virtual int ParamId
        {
            get { return paramId; }
            set { this.paramId = value; }
        }
        private String param;
        public virtual String Param
        {
            get { return param; }
            set { this.param = value; }
        }

        private String paramValue;
        public virtual String ParamValue
        {
            get { return paramValue; }
            set { this.paramValue = value; }
        }
        //'1=String;2=datetime;3=number'
        private int paramType;
        public virtual int ParamType
        {
            get { return paramType; }
            set { this.paramType = value; }
        }

     
    }
}
