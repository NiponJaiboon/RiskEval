using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizPortal;
using iSabaya;

namespace WebHelper.ServiceLayer
{
    public class TransactionFinance
    {
        public int Record { get; set; }

        public int ID { get; set; }

        public string CIF { get; set; }

        public string CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string DocummentCode { get; set; }

        public DateTime MakerDate { get; set; }

        public string MakerName { get; set; }

        public string AccountOwer { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal FeeAmount { get; set; }

        public decimal TotalSummary { get; set; }

        public int Transaction { get; set; }

        public DateTime ApproverDate { get; set; }

        public string ApproveName { get; set; }

        public IList<DetailsTransactionFinanceLog> Details { get; set; }
        public DetailsTransactionFinanceLog Detail { get; set; }
        public DetailsTransactionFinanceLog DetailNew { get; set; }

        public string Status { get; set; }

        public static DateTime now = DateTime.Now;

        public static IList<TransactionFinance> GetTransactionFinance(BizPortalSessionContext context)
        {
            return new List<TransactionFinance>
            {
               new TransactionFinance
               {
                   AccountOwer = "2521212332",
                   ApproveName = context.PersistenceSession.QueryOver<iSabaya.User>().Where(u => u.LoginName == "approver@prumatub").SingleOrDefault().Name.NameWithoutAffixes,
                   ApproverDate = now,
                   CIF = context.PersistenceSession.QueryOver<Member>().Where(m => m.ID == 139).SingleOrDefault().MainCIF,
                   CompanyID = context.PersistenceSession.QueryOver<Member>().Where(m => m.ID == 139).SingleOrDefault().MemberNo,
                   CompanyName = "บริษัท ไทยโรตารี่พลาสติก จำกัด ",
                   Detail = new DetailsTransactionFinanceLog
                   {
                       Service = context.PersistenceSession.QueryOver<BizPortal.FundsTransferService>().Where(s => s.ServiceCode ==  "DC2").SingleOrDefault().Title.ToString(),
                   },
                   DetailNew = new DetailsTransactionFinanceLog
                   {
                       Service = "",
                   },
                   DocummentCode = "02323136581212",
                   FeeAmount = 35m,
                   ID = 1,
                   MakerDate = now,
                   MakerName = context.PersistenceSession.QueryOver<iSabaya.User>().Where(u => u.LoginName == "maker2@prumatub").SingleOrDefault().Name.NameWithoutAffixes,
                   Record = 2,
                   Status = BizPortal.BizPortalTransactionStatus.AwaitApproval.ToString(),
                   TotalAmount = 1000m,
                   TotalSummary = 1035m,
                   Transaction = 1,      
               },
               new TransactionFinance
               {
                   AccountOwer = "2521212332",
                   ApproveName = context.PersistenceSession.QueryOver<iSabaya.User>().Where(u => u.LoginName == "approver@prumatub").SingleOrDefault().Name.NameWithoutAffixes,
                   ApproverDate = now,
                   CIF = context.PersistenceSession.QueryOver<Member>().Where(m => m.ID == 139).SingleOrDefault().MainCIF,
                   CompanyID = context.PersistenceSession.QueryOver<Member>().Where(m => m.ID == 139).SingleOrDefault().MemberNo,
                   CompanyName = "บริษัท ไทยโรตารี่พลาสติก จำกัด ",
                   Detail = new DetailsTransactionFinanceLog
                   {
                       Service = context.PersistenceSession.QueryOver<BizPortal.FundsTransferService>().Where(s => s.ServiceCode ==  "DC3").SingleOrDefault().Title.ToString(),
                   },
                   DetailNew = new DetailsTransactionFinanceLog
                   {
                       Service = "",
                   },
                   DocummentCode = "02323145451456",
                   FeeAmount = 25m,
                   ID = 2,
                   MakerDate = now,
                   MakerName = context.PersistenceSession.QueryOver<iSabaya.User>().Where(u => u.LoginName == "maker2@prumatub").SingleOrDefault().Name.NameWithoutAffixes,
                   Record = 2,
                   Status = BizPortal.BizPortalTransactionStatus.AwaitApproval.ToString(),
                   TotalAmount = 55000m,
                   TotalSummary = 55025m,
                   Transaction = 1,      
               },
               new TransactionFinance
               {
                   AccountOwer = "2521212332",
                   ApproveName = context.PersistenceSession.QueryOver<iSabaya.User>().Where(u => u.LoginName == "approver@prumatub").SingleOrDefault().Name.NameWithoutAffixes,
                   ApproverDate = now,
                   CIF = context.PersistenceSession.QueryOver<Member>().Where(m => m.ID == 139).SingleOrDefault().MainCIF,
                   CompanyID = context.PersistenceSession.QueryOver<Member>().Where(m => m.ID == 139).SingleOrDefault().MemberNo,
                   CompanyName = "บริษัท ไทยโรตารี่พลาสติก จำกัด ",
                   Detail = new DetailsTransactionFinanceLog
                   {
                       Service = context.PersistenceSession.QueryOver<BizPortal.FundsTransferService>().Where(s => s.ServiceCode ==  "BND").SingleOrDefault().Title.ToString(),
                   },
                   DetailNew = new DetailsTransactionFinanceLog
                   {
                       Service = context.PersistenceSession.QueryOver<BizPortal.FundsTransferService>().Where(s => s.ServiceCode ==  "DC2").SingleOrDefault().Title.ToString(),
                   },
                   DocummentCode = "02323145478844",
                   FeeAmount = 100m,
                   ID = 3,
                   MakerDate = now,
                   MakerName = context.PersistenceSession.QueryOver<iSabaya.User>().Where(u => u.LoginName == "maker2@prumatub").SingleOrDefault().Name.NameWithoutAffixes,
                   Record = 2,
                   Status = BizPortal.BizPortalTransactionStatus.Closed.ToString(),
                   TotalAmount = 63000m,
                   TotalSummary = 63100m,
                   Transaction = 3,      
               },
               new TransactionFinance
               {
                   AccountOwer = "2521212332",
                   ApproveName = context.PersistenceSession.QueryOver<iSabaya.User>().Where(u => u.LoginName == "approver@prumatub").SingleOrDefault().Name.NameWithoutAffixes,
                   ApproverDate = now,
                   CIF = context.PersistenceSession.QueryOver<Member>().Where(m => m.ID == 139).SingleOrDefault().MainCIF,
                   CompanyID = context.PersistenceSession.QueryOver<Member>().Where(m => m.ID == 139).SingleOrDefault().MemberNo,
                   CompanyName = "บริษัท ไทยโรตารี่พลาสติก จำกัด ",
                   Detail = new DetailsTransactionFinanceLog
                   {
                       Service = context.PersistenceSession.QueryOver<BizPortal.FundsTransferService>().Where(s => s.ServiceCode ==  "BND").SingleOrDefault().Title.ToString(),
                   },
                   DetailNew = new DetailsTransactionFinanceLog
                   {
                       Service = "",
                   },
                   DocummentCode = "02323145432658",
                   FeeAmount = 350m,
                   ID = 4,
                   MakerDate = now,
                   MakerName = context.PersistenceSession.QueryOver<iSabaya.User>().Where(u => u.LoginName == "maker2@prumatub").SingleOrDefault().Name.NameWithoutAffixes,
                   Record = 2,
                   Status = BizPortal.BizPortalTransactionStatus.AwaitApproval.ToString(),
                   TotalAmount = 352000m,
                   TotalSummary = 352350m,
                   Transaction = 1,      
               },
               new TransactionFinance
               {
                   AccountOwer = "2521212332",
                   ApproveName = context.PersistenceSession.QueryOver<iSabaya.User>().Where(u => u.LoginName == "approver@prumatub").SingleOrDefault().Name.NameWithoutAffixes,
                   ApproverDate = now,
                   CIF = context.PersistenceSession.QueryOver<Member>().Where(m => m.ID == 139).SingleOrDefault().MainCIF,
                   CompanyID = context.PersistenceSession.QueryOver<Member>().Where(m => m.ID == 139).SingleOrDefault().MemberNo,
                   CompanyName = "บริษัท ไทยโรตารี่พลาสติก จำกัด ",
                   Detail = new DetailsTransactionFinanceLog
                   {
                       Service = context.PersistenceSession.QueryOver<BizPortal.FundsTransferService>().Where(s => s.ServiceCode ==  "BND").SingleOrDefault().Title.ToString(),
                   },
                   DetailNew = new DetailsTransactionFinanceLog
                   {
                       Service = "",
                   },
                   DocummentCode = "02323145214587",
                   FeeAmount = 35m,
                   ID = 5,
                   MakerDate = now,
                   MakerName = context.PersistenceSession.QueryOver<iSabaya.User>().Where(u => u.LoginName == "maker2@prumatub").SingleOrDefault().Name.NameWithoutAffixes,
                   Record = 2,
                   Status = BizPortal.BizPortalTransactionStatus.AwaitApproval.ToString(),
                   TotalAmount = 45000m,
                   TotalSummary = 45035m,
                   Transaction = 1,      
                   },
                new TransactionFinance
                {
                   AccountOwer = "2521212332",
                   ApproveName = context.PersistenceSession.QueryOver<iSabaya.User>().Where(u => u.LoginName == "approver@prumatub").SingleOrDefault().Name.NameWithoutAffixes,
                   ApproverDate = now,
                   CIF = context.PersistenceSession.QueryOver<Member>().Where(m => m.ID == 139).SingleOrDefault().MainCIF,
                   CompanyID = context.PersistenceSession.QueryOver<Member>().Where(m => m.ID == 139).SingleOrDefault().MemberNo,
                   CompanyName = "บริษัท ไทยโรตารี่พลาสติก จำกัด ",
                   Detail = new DetailsTransactionFinanceLog
                   {
                       Service = context.PersistenceSession.QueryOver<BizPortal.FundsTransferService>().Where(s => s.ServiceCode ==  "BND").SingleOrDefault().Title.ToString(),
                   },
                   DetailNew = new DetailsTransactionFinanceLog
                   {
                       Service = "",
                   },
                   DocummentCode = "02323145436955",
                   FeeAmount = 0m,
                   ID = 6,
                   MakerDate = now,
                   MakerName = context.PersistenceSession.QueryOver<iSabaya.User>().Where(u => u.LoginName == "maker2@prumatub").SingleOrDefault().Name.NameWithoutAffixes,
                   Record = 2,
                   Status = BizPortal.BizPortalTransactionStatus.Closed.ToString(),
                   TotalAmount = 35000,
                   TotalSummary = 35000m,
                   Transaction = 1,      
               },

              
            };
        }

    }

    public class DetailsTransactionFinanceLog
    {
        public int Record { get; set; }

        public string AccountRecirpt { get; set; }

        public decimal Amount { get; set; }

        public string Service { get; set; }

        public decimal Fee { get; set; }

        public string FeeCondition { get; set; }

        public string Notify { get; set; }

        public string Ref1 { get; set; }

        public string Ref2 { get; set; }

        public string Status { get; set; }

        public string Remark { get; set; }
    }
}