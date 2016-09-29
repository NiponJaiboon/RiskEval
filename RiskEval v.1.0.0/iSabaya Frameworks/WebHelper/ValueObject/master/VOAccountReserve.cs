using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOAccountReserve
    {
        AccountReserve accountReserve;
        public VOAccountReserve(AccountReserve accountReserve)
        {
            this.accountReserve = accountReserve;
        }
        private int accountReserveID;
        public int AccountReserveID
        {
            get { return accountReserveID; }
            set { accountReserveID = value; }
        }

        private string reservationNo;

        public string ReservationNo
        {
            get { return reservationNo; }
            set { reservationNo = value; }
        }
        private double reservedUnits;

        public double ReservedUnits
        {
            get { return reservedUnits; }
            set { reservedUnits = value; }
        }

        public String PledgedPeriod
        {
            get { return accountReserve.PledgedPeriod.ToString(); }
            
        }
        public String ReservationPeriod
        {
            get { return accountReserve.ReservationPeriod.ToString(); }

        }
        public string CurrentStatus
        {
            get { 
                
                bool reserveStatus =  accountReserve.ReservationPeriod.Contains(DateTime.Now);
                bool pladStatus = accountReserve.PledgedPeriod.Contains(DateTime.Now);
                if (!reserveStatus)
                {
                    return "ยกเลิกแล้ว";
                }
                else
                {
                    if (!pladStatus)
                    {
                        return "ใบหน่วยปรกติ";
                    }
                    else
                    {
                        return "ถูกจำนำ";
                    }
                }
            }
         
        }

    }
}
