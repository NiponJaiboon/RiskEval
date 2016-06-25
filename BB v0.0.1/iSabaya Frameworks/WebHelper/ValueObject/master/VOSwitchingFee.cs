using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using imSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOSwitchingFee
    {
        public int SwitchingFeeID { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }

        public int FromFundID { get; set; }

        public string FromFundCode { get; set; }

        public InvestmentInstrument FromFund
        {
            set
            {
                if (value != null)
                {
                    FromFundID = value.InvestmentInstrumentID;
                    FromFundCode = value.Code;
                }
                else
                {
                    FromFundID = 0;
                    FromFundCode = "NULL";
                }
            }
        }

        public int ToFundID { get; set; }

        public string ToFundCode { get; set; }

        public InvestmentInstrument ToFund
        {
            set
            {
                if (value != null)
                {
                    ToFundID = value.InvestmentInstrumentID;
                    ToFundCode = value.Code;
                }
                else
                {
                    ToFundID = 0;
                    ToFundCode = "NULL";
                }
            }
        }

        public DateTime UpdatedTS { get; set; }

        public int UpdatedBy { get; set; }

        public int FeeScheduleID { get; set; }

        public string FeeSchedule { get; set; }
    }
}