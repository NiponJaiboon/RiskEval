using System;
using System.Collections.Generic;
using System.Linq;
using WebHelper;
using DevExpress.Web.ASPxGridView;
using WebHelper.Util;


public partial class ServiceFeeScheduleControl : iSabayaControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            grid.DataBind();
        }
    }

    protected void gridServiceFeeShedule_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        grid.DataBind();
    }

    protected void gridServiceFeeShedule_DataBinding(object sender, EventArgs e)
    {
        grid.DataSource = TransactionHelper.GetFundsTransferServices(iSabayaContext);
    }

}
