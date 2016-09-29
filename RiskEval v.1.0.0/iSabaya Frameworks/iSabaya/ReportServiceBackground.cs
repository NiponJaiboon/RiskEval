using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class ReportServiceBackground
    {
        private int serviceId;
        public virtual int ServiceId
        {
            get { return serviceId; }
            set { this.serviceId = value; }
        }

        private String rptReportPath;
        public virtual String RptReportPath
        {
            get { return rptReportPath; }
            set { this.rptReportPath = value; }
        }
        private String pdfTargetPath;
        public virtual String PdfTargetPath
        {
            get { return pdfTargetPath; }
            set { this.pdfTargetPath = value; }
        }
        private DateTime requestTS;
        public virtual DateTime RequestTS
        {
            get { return requestTS; }
            set { this.requestTS = value; }
        }
        private DateTime startExecuteTS;
        public virtual DateTime StartExecuteTS
        {
            get { return startExecuteTS; }
            set { this.startExecuteTS = value; }
        }
        private DateTime successTS;
        public virtual DateTime SuccessTS
        {
            get { return successTS; }
            set { this.successTS = value; }
        }
        private String requestUser;
        public virtual String RequestUser
        {
            get { return requestUser; }
            set { this.requestUser = value; }
        }

        private IList<ReportServiceParameters> parameters;
        public virtual IList<ReportServiceParameters> Params
        {
            get { return parameters; }
            set { this.parameters = value; }
        }
    }
}
