using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DevExpress.Web.ASPxClasses;
using iSabaya;
using NHibernate;
using BizPortal;
using NHibernate.Criterion;

namespace WebHelper
{
    public class iSabayaControl : System.Web.UI.UserControl
    {
        public Context iSabayaContext
        {
            get { return ((iSabayaWebPageBase)base.Page).SessionContext; }
        }

        public long UserSessionID
        {
            //get { return ((iSabayaWebPageBase)base.Page).UserSessionID; }
            get { return iSabayaContext.UserSessionID; }
        }

        public User User
        {
            get { return ((iSabayaWebPageBase)base.Page).User; }  // Edit by Watchara 
            //set { ((iSabayaWebPageBase)base.Page).User = value; }
        }

        public MemberUser MemberUser
        {
            get 
            {
                //return iSabayaContext.PersistenceSession.CreateCriteria<MemberUser>().Add(Expression.Eq("User", this.User)).UniqueResult<MemberUser>();
                //edit by Itsada 03022014
                return (MemberUser)iSabayaContext.PersistenceSession.GetSessionImplementation().PersistenceContext.Unproxy(this.User);

            }  // Edit by Watchara 
            //set { ((iSabayaWebPageBase)base.Page).User = value; }
        }

        public Currency Currency
        {
            get { return ((iSabayaWebPageBase)base.Page).Currency; }
            set { ((iSabayaWebPageBase)base.Page).Currency = value; }
        }

        public Language Language
        {
            get { return ((iSabayaWebPageBase)base.Page).Language; }
            set { ((iSabayaWebPageBase)base.Page).Language = value; }
        }

        public String LanguageCode
        {
            get { return ((iSabayaWebPageBase)base.Page).LanguageCode; }
            set { ((iSabayaWebPageBase)base.Page).LanguageCode = value; }
        }

        public Country Country
        {
            get { return ((iSabayaWebPageBase)base.Page).Country; }
            set { ((iSabayaWebPageBase)base.Page).Country = value; }
        }

        public String DateOutputFormat
        {
            get { return ((iSabayaWebPageBase)base.Page).DateOutputFormat; }
        }

        public String DateTimeOutputFormat
        {
            get { return ((iSabayaWebPageBase)base.Page).DateTimeOutputFormat; }
        }

        public String DateInputFormat
        {
            get { return ((iSabayaWebPageBase)base.Page).DateInputFormat; }
        }

        public String CurrencyFormat
        {
            get { return ((iSabayaWebPageBase)base.Page).CurrencyFormat; }
        }

        public String UnitsFormat
        {
            get { return ((iSabayaWebPageBase)base.Page).UnitsFormat; }
        }

        protected Unit EditorWidth
        {
            get { return ((iSabayaWebPageBase)base.Page).EditorWidth; }
        }

        protected string EmailExpression
        {
            get { return ((iSabayaWebPageBase)base.Page).EmailExpression; }
        }

        public int PrivilegeLevel;

        public void SetVisible(int privilegeLevel)
        {
            this.Visible = (this.PrivilegeLevel <= privilegeLevel);
        }

        public virtual String Text
        {
            get { return null; }
            set { }
        }

        public virtual object Value
        {
            get { return null; }
            set { }
        }

        /// <summary>
        /// Memthod UserCanAccess
        /// </summary>
        /// <param name="memberUser"></param>
        /// <param name="functionIds"></param>
        /// <returns>Access right of each MemberUser</returns>
        public bool UserCanAccess(MemberUser memberUser, IList<int> functionIds)
        {
            foreach (var wf in memberUser.GetEffectiveCreatorMaintenanceWorkflows())
            {
                foreach (var functionId in functionIds)
                {
                    if (wf.MemberFunction.FunctionID == functionId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}