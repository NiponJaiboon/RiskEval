using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iSabaya;

namespace WebHelper.ServiceLayer
{
    public class ChequesService :Service
    {
        public static ChequeStateCategory GetChequeStatus(int index)
        {
            //Value="0" Text="Unpaid"
            //Value="1" Text="Damaged"
            //Value="2" Text="Lost" 
            //Value="3" Text="Expired" 
            //Value="4" Text="Other" 
            ChequeStateCategory status;
            switch (index)
            {
                case 0:
                    status = ChequeStateCategory.Unpaid;
                    break;
                case 1:
                    status = ChequeStateCategory.Damaged;
                    break;
                case 2:
                    status = ChequeStateCategory.Lost;
                    break;
                case 3:
                    status = ChequeStateCategory.Expired;
                    break;
                default:
                    status = ChequeStateCategory.Other;
                    break;
            }
            return status;
        }

        public static string ConvertStatusThai(ChequeStateCategory chequeStateCategory)
        {
            string status = "";
            switch (chequeStateCategory)
            {
                case ChequeStateCategory.New:
                    status = "ใหม่";
                    break;
                case ChequeStateCategory.Outstanding:
                    status = "ออกเช็ค";
                    break;                
                case ChequeStateCategory.Lost:
                    status = "หาย";
                    break;
                case ChequeStateCategory.Damaged:
                    status = "เสีย";
                    break;
                case ChequeStateCategory.Expired:
                    status = "หมดอายุ";
                    break;
                case ChequeStateCategory.Cancelled:
                    status = "ยกเลิก";
                    break;
                case ChequeStateCategory.Other:
                    status = "ออกเช็คทดแทน";
                    break;                    
                case ChequeStateCategory.Unpaid:
                default:
                    status = "";
                    break;
            }
            return status;
        }


        public static string ConvertChequePaymentStatusThai(ChequeStateCategory chequeStateCategory)
        {
            string status = "";
            switch (chequeStateCategory)
            {
                case ChequeStateCategory.New:
                    status = "ใหม่";
                    break;
                case ChequeStateCategory.Outstanding:
                    status = "ส่งแล้ว";
                    break;
                case ChequeStateCategory.Cleared:
                    status = "รับเงินแล้ว";
                    break;
                case ChequeStateCategory.Rejected:
                    status = "";
                    break;
                case ChequeStateCategory.Lost:
                    status = "หาย";
                    break;
                case ChequeStateCategory.Damaged:
                    status = "เสีย";
                    break;
                case ChequeStateCategory.Expired:
                    status = "หมดอายุ";
                    break;
                case ChequeStateCategory.Cancelled:
                    status = "ยกเลิก";
                    break;
                case ChequeStateCategory.Other:
                    status = "รับแล้ว";
                    break;
                case ChequeStateCategory.Reinvested:
                case ChequeStateCategory.Unpaid:
                default:
                    status = "";
                    break;
            }
            return status;
        }
    }
}