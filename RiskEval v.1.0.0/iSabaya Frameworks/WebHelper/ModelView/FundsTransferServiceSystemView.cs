using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHelper.ModelView
{
    public class FundsTransferServiceSystemView
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string EffectivePeriod { get; set; }
        public string Code { get; set; }
        public string MaxAmountPerDay { get; set; }
        public string MaxAmountPerTransaction { get; set; }
        public string CreateBy { get; set; }
        public string CreateTimeStamp { get; set; }
        public string ApproveBy { get; set; }
        public string ApproveTimeStamp { get; set; }
        public string DataEntryDay { get; set; }
        public string DataEntryTime { get; set; }
        public string DebitDay { get; set; }
        public string DebitTime { get; set; }
        public string Status { get; set; }
    }
}