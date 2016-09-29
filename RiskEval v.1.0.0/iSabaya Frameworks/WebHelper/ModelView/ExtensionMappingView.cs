using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using BizPortal;
using Controller;
using iSabaya;

namespace WebHelper.ModelView
{
    public class ExtensionMappingView
    {
        public static IList<FundsTransferServiceSystemView> View(IList<FundsTransferService> fundsTransferServices)
        {
            return fundsTransferServices.Select(service => 
                new FundsTransferServiceSystemView
                {
                    ID = service.ID, 
                    Title = service.Title.ToString(), 
                    EffectivePeriod = service.EffectivePeriod.ToString(), 
                    Code = service.ServiceCode, MaxAmountPerDay = "", 
                    MaxAmountPerTransaction = "",
                    CreateBy = service.CreateAction.UserActionLoginName(),
                    CreateTimeStamp = service.CreateAction.UserActionTimeStamp(),
                    ApproveBy = service.ApproveAction.UserActionLoginName(),
                    ApproveTimeStamp = service.ApproveAction.UserActionTimeStamp(),
                    DataEntryDay = "",
                    DataEntryTime = "",
                    DebitDay = service.CurrentProfile.RelativeExportWindow.LeadDays.ToString(CultureInfo.InvariantCulture),
                    DebitTime = service.CurrentProfile.RelativeDebitWindow.To.DateTimeFormat(),
                    Status = service.EffectivePeriod.IsEffective() ? "ใช้งานได้" : "ใช้งานไม่ได้",
                }).ToList();
        }
    }
}