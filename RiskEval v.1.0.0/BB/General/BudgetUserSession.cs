using iSabaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budget.General
{
    public class BudgetUserSession : UserSession
    {
        public BudgetUserSession()
        {

        }

        public BudgetUserSession(iSystem iSystem, SelfAuthenticatedUser user, string fromIPAddress)
            : base(iSystem, user, fromIPAddress)
        {

        }

        public virtual new SelfAuthenticatedUser User
        {
            get { return (SelfAuthenticatedUser)base.User; }
            set { base.User = value; }
        }
    }
}
