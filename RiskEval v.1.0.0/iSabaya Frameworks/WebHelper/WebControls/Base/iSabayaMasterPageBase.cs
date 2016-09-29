using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iSabaya;

namespace WebHelper.WebControls.Base
{
    public abstract class iSabayaMasterPageBase : System.Web.UI.MasterPage
    {
        protected Context SessionContext
        {
            get { return ((iSabayaWebPageBase)base.Page).SessionContext; }
        }
    }
}