using System;
using System.Collections.Generic;
using System.Linq;
using BizPortal;
using iSabaya;

namespace BizPortal//AdminWeb.Management.Corporate
{
    [Serializable]
    public class ClientFunction
    {
        private enum ClientFunctionStatus
        {
            Unsubscribed,
            Subscribed,
            BeingTerminated,
            BeingSubscribed,
        }

        public ClientFunction(string languageCode, BizPortalFunction function, Member member)
        {
            if (languageCode == null) throw new ArgumentNullException("languageCode");
            LanguageCode = languageCode;
            Function = function;
            Member = member;
            FunctionID = function.ID;
            FunctionTitle = function.Title.ToString(languageCode);
            _status = ClientFunctionStatus.Unsubscribed;

            //this.MemberFunction = subscribedFunction;
        }

        #region Grid columns

        public int FunctionID { get; set; }
        public string FunctionTitle { get; set; }
        public TimeInterval EffectivePeriod { get; set; }
        public long MemberFunctionID { get; set; }

        public string CommandButton
        {
            get
            {
                string command = null;
                switch (_status)
                {
                    case ClientFunctionStatus.Subscribed:
                        command = String.Format("<a href=\"javascript:void(0);\" title=\"คลิกเพื่อ {0}\" onclick=\"OnMoreInfoClick('{0}', 'keyFieldName')\"> {0}</a>", "ยกเลิก");
                        command += String.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"javascript:void(0);\" title=\"คลิกเพื่อ {0}\" onclick=\"AddWorkFlow('{0}', 'keyFieldName')\">{0}</a><br/>", "เพิ่มสิทธิ์");
                        break;

                    case ClientFunctionStatus.BeingTerminated:
                        command = "อยู่ในระหว่างการยกเลิก";
                        break;

                    case ClientFunctionStatus.Unsubscribed:
                        command = String.Format("<a href=\"javascript:void(0);\" title=\"คลิกเพื่อ {0}\" onclick=\"OnMoreInfoClick('{0}', 'keyFieldName')\"> {0}</a><br/>", "สมัคร");
                        break;

                    case ClientFunctionStatus.BeingSubscribed:
                        command = "อยู่ในระหว่างการสมัคร";
                        break;
                }
                return command;
            }
        }

        public string CommandButtonClient
        {
            get
            {
                string command = null;
                switch (_status)
                {
                    case ClientFunctionStatus.Subscribed:
                        command = String.Format("<a href=\"javascript:void(0);\" title=\"คลิกเพื่อ {0}\" onclick=\"AddWorkFlow('{0}', 'keyFieldName')\">{0}</a><br/>", "เพิ่มสิทธิ์");
                        break;

                    case ClientFunctionStatus.BeingTerminated:
                        command = String.Format("<a href=\"javascript:void(0);\" title=\"คลิกเพื่อ {0}\" onclick=\"AddWorkFlow('{0}', 'keyFieldName')\">{0}</a><br/>", "เพิ่มสิทธิ์");
                        break;

                    case ClientFunctionStatus.Unsubscribed:
                        command = "";
                        break;

                    case ClientFunctionStatus.BeingSubscribed:
                        command = "";
                        break;
                }
                return command;
            }
        }

        #endregion Grid columns

        private Member Member { get; set; }
        private BizPortalFunction Function { get; set; }
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        private string LanguageCode { get; set; }

        // ReSharper restore UnusedAutoPropertyAccessor.Local
        private ClientFunctionStatus _status;

        private MemberFunction _subscribedFunction;
        public MemberFunction SubscribedFunction
        {
            get { return _subscribedFunction; }
            set
            {
                _subscribedFunction = value;
                if (value.IsNotFinalized)
                {
                    _status = TimeInterval.IsNullOrEmpty(value.EffectivePeriod) ? ClientFunctionStatus.BeingSubscribed : ClientFunctionStatus.BeingTerminated;
                }
                else if (!value.IsEffective)
                {
                    _status = ClientFunctionStatus.Unsubscribed;
                }
                else
                    _status = ClientFunctionStatus.Subscribed;

                EffectivePeriod = value.EffectivePeriod;
            }
        }

        /// <summary>
        /// สมัครใช้บริการ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="workflow"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        public AddMemberFunctionTransaction Apply(BizPortalSessionContext context, MaintenanceWorkflow workflow, DateTime when)
        {
            if (_status != ClientFunctionStatus.Unsubscribed)
                throw new Exception("This function cannot be Apply.");
            _status = ClientFunctionStatus.BeingSubscribed;
            return new AddMemberFunctionTransaction(context, workflow, when, Member, new MemberFunction(Function, Member));
        }

        /// <summary>
        /// ยกเลิกการใช้บริการ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="workflow"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        public TerminateMemberFunctionTransaction Terminate(BizPortalSessionContext context, MaintenanceWorkflow workflow, DateTime when)
        {
            if (_status != ClientFunctionStatus.Subscribed)
                throw new Exception("This function cannot be terminated.");
            _status = ClientFunctionStatus.BeingTerminated;
            return new TerminateMemberFunctionTransaction(context, workflow, when, Member, SubscribedFunction);
        }

        public static IList<ClientFunction> CreateFunctionList(BizPortalSessionContext context, Member member, bool isFinancialFunction)
        {
            if (context == null) throw new ArgumentNullException("context");
            string languageCode = context.CurrentLanguage.Code;
            IList<ClientFunction> functions;

            if (isFinancialFunction)
                functions = ClientMaker.Functions.Select(mf => new ClientFunction(languageCode, mf, member)).ToList();
            else
                functions = ClientAdmin.Functions.Select(f => new ClientFunction(languageCode, f, member)).ToList();

            foreach (MemberFunction mf in member.SubscribedFunctions)
            {
                //Set memberFunctionID
                foreach (ClientFunction cf in functions)
                {
                    if (mf.FunctionID == cf.FunctionID)
                    {
                        cf.MemberFunctionID = mf.ID;
                    }
                }

                //Other Set ClientFunction
                if (!TimeInterval.IsNullOrEmpty(mf.EffectivePeriod) && !mf.IsEffectiveOn(DateTime.Now))
                    continue;
                foreach (ClientFunction cf in functions)
                    if (mf.FunctionID == cf.FunctionID)
                    {
                        cf.SubscribedFunction = mf;
                        break;
                    }
            }
            return functions;
        }

        public static IList<ClientFunction> CreateFunctionList(BizPortalSessionContext context, Member member)
        {
            if (context == null) throw new ArgumentNullException("context");
            string languageCode = context.CurrentLanguage.Code;
            IList<ClientFunction> functions = ClientAdmin.Functions.Select(f => new ClientFunction(languageCode, f, member)).ToList();
            foreach (BizPortalFunction f in ClientMaker.Functions)
                functions.Add(new ClientFunction(languageCode, f, member));

            foreach (MemberFunction mf in member.SubscribedFunctions)
            {
                if (!TimeInterval.IsNullOrEmpty(mf.EffectivePeriod) && !mf.IsEffectiveOn(DateTime.Now))
                    continue;
                foreach (ClientFunction cf in functions)
                    if (mf.FunctionID == cf.FunctionID)
                    {
                        cf.SubscribedFunction = mf;
                        break;
                    }
            }
            return functions;
        }
    }
}