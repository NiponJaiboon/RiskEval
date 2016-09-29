using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using imSabaya;
using imSabaya.ProvidentFundSystem;
using WebHelper;

public partial class ctrls_Viewer_MemberTerminationsViewer : iSabayaControl
{
    public PFTransaction Transaction { get; set; }
    public string CssPostfix { get; set; }
    private DataTable dataTable;
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Member", typeof(string)));
            dataTable.Columns.Add(new DataColumn("TerminationType", typeof(string)));
            ViewState[this.ClientID + "TerminationInfo"] = dataTable;
            InitialControl();
        }

    }
    private void InitialControl()
    {
        cbpTerminationInfoViewer.ClientInstanceName = this.ClientID + cbpTerminationInfoViewer.ID;
        gdvTerminationInfos.ClientSideEvents.FocusedRowChanged = @"function(s,e)
        {
            " + cbpTerminationInfoViewer.ClientInstanceName + @".PerformCallback(s.GetFocusedRowIndex());
        }";
        gdvTerminationInfos.GroupBy(gdvTerminationInfos.Columns["TerminationType"]);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (Transaction != null)
            {
                InitialTerminationInfos(Transaction);
                ViewState[this.ClientID + "TerminationInfo"] = gdvTerminationInfos.DataSource = dataTable;
                gdvTerminationInfos.DataBind();
            }
            else
            {
                gdvTerminationInfos.DataSource = ViewState[this.ClientID + "TerminationInfo"];
            }
        }
        else
        {
            gdvTerminationInfos.DataSource = ViewState[this.ClientID + "TerminationInfo"];
        }
    }
    private void InitialTerminationInfos(FundTransaction transaction)
    {
        PFTransaction pfTransaction = transaction as PFTransaction;
        if (pfTransaction == null) return;
        TerminationInfo info = pfTransaction.TerminationInfo;
        if( pfTransaction.Type.Code ==  PFConstants.PFTTCodeMemberTermination )
        {
            if (info != null && info.Member != null)
            {
                DataRow dr = dataTable.NewRow();
                dr["ID"] = info.TerminationInfoID;
                dr["Member"] = string.Format("{0}/{1}/{2}", info.Member.DivisionCode, info.Member.EmployeeNo, info.Member.Name.ToString(base.LanguageCode));
                dr["TerminationType"] = info.TerminationCategory.ToString(base.LanguageCode);
                dataTable.Rows.Add(dr);
            }
        }

        for (int j = 0; j < transaction.Children.Count; j++)
            InitialTerminationInfos(transaction.Children[j].Child as FundTransaction);
    }
    protected void cbpTerminationInfoViewer_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        int terminationInfoID = (int)gdvTerminationInfos.GetRowValues(Int32.Parse(e.Parameter), "ID");
        if (terminationInfoID > 0)
        {
            ctrlTerminationInfoViewer.MemberTerminationInfo = iSabayaContext.PersistencySession.Get<TerminationInfo>(terminationInfoID);
            ctrlTerminationInfoViewer.CssPostfix = gdvTerminationInfos.CssPostfix;
            ctrlTerminationInfoViewer.SetDataTable();
        }
    }
}
