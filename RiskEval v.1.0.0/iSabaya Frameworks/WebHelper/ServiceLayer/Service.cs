using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Json;
using System.Text;
using BizPortal;
using DevExpress.Web.ASPxCallback;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using iSabaya;

namespace WebHelper.ServiceLayer
{
    public enum SystemId
    {
        AdminiBank = 41,
        ClientiBank = 42,
        AdminCeft = 53,
        ClientCeft = 54,
    }

    public enum FunctionWorkflowType
    {
        Mainternance,
        Service
    }

    public class MessageRespone
    {
        public string Message { get; set; }
        public string TransactionNo { get; set; }
        public bool IsSuccess { get; set; }
        //public string Icon
        //{
        //    get
        //    {
        //        return IsSuccess ? "Images/led_icon/_success.png" : "Images/led_icon/_warning.png";
        //    }
        //}
    }

    public abstract class Service
    {
        public static string newLineHTML = "</br>";
        private const string Bath = "บาท";
        private const string Dolla = "ดอลล่า";
        public const string DateFormat = "dd/MM/yyyy";
        public const string DateTimeFormat = "dd/MM/yyyy HH:mm";
        public const string TimeFormat = "HH:mm";
        public const string MoneyFormat = "#,##0.#0";

        public static string Currency(string isoCode)
        {
            string currency = "";
            switch (isoCode)
            {
                case "THB":
                    currency = Bath;
                    break;
                case "US":
                    currency = Dolla;
                    break;
            }
            return currency;
        }

        public static IList<T> GetSelectedOnGridView<T>(ASPxGridView gv)
        {
            IList<T> listId = new List<T>();
            for (int i = 0; i < gv.VisibleRowCount; i++)
            {
                if (gv.Selection.IsRowSelected(i))
                    listId.Add((T)gv.GetRow(i));
            }
            return listId;
        }

        /// <summary>
        /// Method GetSelecttedOnGridView
        /// </summary>
        /// <param name="gv">gridview</param>
        /// <param name="keyFiled">keyFileName</param>
        /// <returns></returns>
        public static List<long> GetSelectedOnGridView(ASPxGridView gv, string keyFiled)
        {
            var listId = new List<long>();
            for (int i = 0; i < gv.VisibleRowCount; i++)
            {
                if (gv.Selection.IsRowSelected(i))
                    listId.Add((long)gv.GetRowValues(i, keyFiled));
            }
            return listId;
        }

        public static List<int> GetSelectedFieldValues(ASPxGridView gv, string keyFiled)
        {
            return gv.GetSelectedFieldValues(keyFiled).Cast<int>().ToList();
            //var listId = new List<int>();
            //foreach (int item in gv.GetSelectedFieldValues(keyFiled))
            //    listId.Add(item);
            //return listId;
        }

        public static List<long> GetSelectedOnGridViewLong(ASPxGridView gv, string keyFiled)
        {
            var listId = new List<long>();
            for (var i = 0; i < gv.VisibleRowCount; i++)
            {
                if (gv.Selection.IsRowSelected(i))
                    listId.Add((long)gv.GetRowValues(i, keyFiled));
            }
            return listId;
        }

        public static T GetRowObject<T>(ASPxGridView gv, long visibleIndex)
        {
            if (gv == null) return default(T);

            return (T)gv.GetRow(int.Parse(visibleIndex.ToString(CultureInfo.InvariantCulture)));
        }

        public static MaintenanceWorkflow GetFunctionMaintenanceWorkflow(MemberUser memberUser, int functionId)
        {
            try
            {
                var fwf = new MaintenanceWorkflow();
                foreach (var fw in memberUser.GetEffectiveCreatorMaintenanceWorkflows().Where(fw => fw.MemberFunction.FunctionID == functionId))
                {
                    fwf = fw;
                    break;
                }
                return fwf;
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("function work flow is null");
            }
        }

        public static ServiceWorkflow GetFunctionServiceWorkflow(MemberUser memberUser,int functionId)
        {
            try
            {
                var fwf = new ServiceWorkflow();
                foreach (var fw in memberUser.GetEffectiveCreatorServiceWorkflows().Where(fw => fw.MemberFunction.FunctionID == functionId))
                {
                    fwf = fw;
                    break;
                }
                return fwf;
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("function work flow is null");
            }
        }

        public static IList<ServiceWorkflow> GetFunctionServiceWorkflows(MemberUser memberUser, int functionId,BankService bankService)
        {
            try
            {
                var fwf = new List<ServiceWorkflow>();
                foreach (var fw in memberUser.GetEffectiveCreatorServiceWorkflows().Where(fw => fw.MemberFunction.FunctionID == functionId 
                    && fw.ContainsService(bankService)))
                {
                    fwf.Add(fw);
                }
                return fwf;
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("function work flow is null");
            }
        }

        private static bool ContainsService(IEnumerable<ServiceWorkflowService> sws, string serviceCode)
        {
            foreach (ServiceWorkflowService s in sws)
            {
                if (s.MemberService.Service.ServiceCode == serviceCode) return true;
            }
            return false;
        }
        public static IList<ServiceWorkflow> GetFunctionServiceWorkflows(MemberUser memberUser, int functionId, string serviceCode)
        {
            try
            {
                var fwf = new List<ServiceWorkflow>();
                foreach (var fw in memberUser.GetEffectiveCreatorServiceWorkflows().Where(fw => fw.MemberFunction.FunctionID == functionId
                    && ContainsService(fw.WorkflowServices, serviceCode)))
                {
                    fwf.Add(fw);
                }
                return fwf;
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("function work flow is null");
            }
        }
        /// <summary>
        /// if use method
        /// JsonBooleanValue = result
        /// JsonStringValue = message
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        /// <param name="warningCount"></param>
        public static void JsonMessage(JsonObjectCollection obj, string message, int warningCount)
        {
            try
            {
                if (warningCount == 0)
                {
                    obj.Add(new JsonBooleanValue("result", true));
                    obj.Add(new JsonStringValue("message", message));
                }
                else
                {
                    obj.Add(new JsonBooleanValue("result", false));
                    obj.Add(new JsonStringValue("message", message));
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public static void JsonMessage(JsonObjectCollection obj, string message1, bool isConfirm, int warningCount)
        {
            try
            {
                if (warningCount == 0)
                {
                    obj.Add(new JsonBooleanValue("result", true));
                    obj.Add(new JsonStringValue("message", message1));
                    obj.Add(new JsonBooleanValue("Confirm", isConfirm));
                }
                else
                {
                    obj.Add(new JsonBooleanValue("result", false));
                    obj.Add(new JsonStringValue("message", message1));
                    obj.Add(new JsonBooleanValue("Confirm", isConfirm));
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static void JsonMessage(JsonObjectCollection obj, string message1, string message2, int warningCount)
        {
            try
            {
                if (warningCount == 0)
                {
                    obj.Add(new JsonBooleanValue("result", true));
                    obj.Add(new JsonStringValue("message1", message1));
                    obj.Add(new JsonStringValue("message2", message2));
                }
                else
                {
                    obj.Add(new JsonBooleanValue("result", false));
                    obj.Add(new JsonStringValue("message1", message1));
                    obj.Add(new JsonStringValue("message2", message2));
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public static void JsMessage(ASPxCallbackPanel panel, StringBuilder message, int warningCount)
        {
            panel.JSProperties["cpIsError"] = warningCount > 0;
            panel.JSProperties["cpErrorMessage"] = message.ToString();
        }
        public static void JsMessage(ASPxCallback panel, StringBuilder message, int warningCount)
        {
            panel.JSProperties["cpIsError"] = warningCount > 0;
            panel.JSProperties["cpErrorMessage"] = message.ToString();
        }

        public static string ValidateComment(string comment)
        {
            return comment.Length >= 400 ? comment.Substring(0, 399) : comment;
        }

        public static void SetText(ASPxLabel label, string text)
        {
            label.Text = text != "" ? text : "-";
        }

        public static void SetText(ASPxTextBox textBox, string text)
        {
            if (text != "")
                textBox.Text = text;
        }

        public static bool IsNotFinalized<T>(T target, ref string message, ref int warningCount, string code)
            where T : PersistentEntity
        {
            if (!target.IsNotFinalized) return false;
            warningCount++;
            message = Messages.Genaral.TransactionApproved.Format(code);
            return true;
        }

        public static bool IsNotPermistion<T>(T functionWorkflow, ref string message, ref int warningCount, string code)
        where T : FunctionWorkflow
        {
            if (functionWorkflow.MemberFunction != null) return false;
            warningCount++;
            message = Messages.Genaral.IsNotMemberFunction.Format(code);
            return true;
        }

        public static bool OpenTransactionsUsingWorkflow(int countOfOpenTransactionsUsingWorkflow, ref string message, ref int warningCount, string code)
        {
            if (countOfOpenTransactionsUsingWorkflow <= 0) return false;
            warningCount++;
            message = Messages.FunctionWorkFlow.OpenTransactionsUsingWorkflow.Format(code, countOfOpenTransactionsUsingWorkflow);
            return true;
        }

        public static bool StringSerializer(bool first, ref StringBuilder sb, string item, string symbol)
        {
            if (first)
            {
                sb.Append(item);
            }
            else
            {
                sb.Append(symbol);
                sb.Append(item);
            }
            return false;
        }

        public static string ConvertBizPortalStateCategory(StateCode stateCategory)
        {
            string stateCategotyTh = "";
            switch (stateCategory)
            {
                case StateCode.Initial:
                case StateCode.Draft:
                case StateCode.Canceled:
                case StateCode.AwaitAmendment:
                case StateCode.PendingMoreApproval:
                case StateCode.Rejected:
                    stateCategotyTh = "";
                    break;
                case StateCode.PartialSuccess:
                    stateCategotyTh = "PendingProcessing";
                    break;
                //case StateCode.Processing:
                //    stateCategotyTh = "Processing";
                //    break;
                case StateCode.Submitted:
                    stateCategotyTh = "PendingApproval";
                    break;
                //case StateCode.Failed:
                //    stateCategotyTh = "Failed";
                //    break;
                case StateCode.Success:
                    stateCategotyTh = "Success";
                    break;
                case StateCode.Approved:
                    stateCategotyTh = "Approved";
                    break;
                //case StateCode.M_ManualProcessing:
                //    stateCategotyTh = "PendingProcessing";
                //    break;
            }
            return stateCategotyTh;
        }

        public static string GetEffectivePeriod(TimeInterval timeInterval)
        {
            return timeInterval != null ? (timeInterval.IsEffective()
                ? "<span style='color:Green'>" + "ใช้งานได้" + "</span>"
                : "<span style='color:Red'>" + "ใช้งานไม่ได้" + "</span>") : "";
        }

        protected static string CreateTraceInfo(Exception exception, string messageCode = "")
        {
            var st = new StackTrace(exception);
            var stackIndent = new StringBuilder();
            for (int i = 0; i < st.FrameCount; i++)
            {
                StackFrame sf = st.GetFrame(i);
                //stackIndent.AppendLine(string.Format(" No : {0}", i.ToString(CultureInfo.InvariantCulture)));
                stackIndent.AppendLine(string.Format(" Method : {0} -> ", sf.GetMethod().Name));
                //stackIndent.AppendLine(string.Format(" Message : {0}:{1}", messageCode, message));
                //stackIndent.AppendLine(string.Format(" File : {0}", sf.GetFileName()));
                //stackIndent.AppendLine(string.Format(" Line Number : {0}", sf.GetFileLineNumber()));
            }
            stackIndent.AppendLine(string.Format(" Message : {0}:{1}", messageCode, exception.Message));
            return ((stackIndent.ToString().Length <= 1950) ? stackIndent.ToString() : stackIndent.ToString().Substring(0, 1950) + "...");
        }
    }
}