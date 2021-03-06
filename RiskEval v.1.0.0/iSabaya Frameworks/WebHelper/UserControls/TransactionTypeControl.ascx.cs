using System;
using System.Collections.Generic;
using System.Data;
using DevExpress.Web.ASPxEditors;
using imSabaya;
using iSabaya;
using NHibernate;
using WebHelper;

public partial class ctrls_TransactionTypeControl : iSabayaControl
{
    #region Validation Section

    private bool isRequiredField = false;

    public bool IsRequiredField
    {
        get { return isRequiredField; }
        set { this.isRequiredField = value; }
    }

    public String ValidationGroup { get; set; }

    #endregion Validation Section

    private ASPxListBox LBTTYPES;
    private ListEditSelectionMode selectMethod = ListEditSelectionMode.Single;

    public ListEditSelectionMode SelectMethod
    {
        get { return selectMethod; }
        set { selectMethod = value; }
    }

    private int selectIndex;

    public int SelectIndex
    {
        get { return selectIndex; }
        set { selectIndex = value; }
    }

    protected override void OnInit(EventArgs e)
    {
        //ASPxListBox lbTransactionTypes = (ASPxListBox)ComboTransactionType.FindControl("lbTransactionTypes");
        LBTTYPES = (ASPxListBox)ComboTransactionType.FindControl("lbTransactionTypes");
        if (IsRequiredField)
            ComboTransactionType.SetValidation(this.ValidationGroup);
        ComboTransactionType.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //ASPxListBox lbTransactionTypes = (ASPxListBox)ComboTransactionType.FindControl("lbTransactionTypes");
        //LBTTYPES = (ASPxListBox)ComboTransactionType.FindControl("lbTransactionTypes");
        if (!IsPostBack)
        {
            Session[this.ClientID + "trantypes"] = null;
            LBTTYPES.SelectionMode = this.selectMethod;
            if (this.selectMethod != ListEditSelectionMode.CheckColumn)
                LBTTYPES.Items.RemoveAt(0);
            //else
            //    lbTransactionTypes.Items[0].Selected = true;
        }
    }

    private DataTable TransactionTypeLists()
    {
        iSystem mySystem;
        DataTable ds = null;
        if (CommonConstants.Systems.TryGetValue(int.Parse(System.Configuration.ConfigurationManager.AppSettings["ApplicationID"]), out mySystem))
        {
            IList<InvestmentTransactionType> transtype = InvestmentTransactionType.ListPrincipalTypes(iSabayaContext, mySystem.ApplicationID);
            ds = new DataTable();
            ds.Columns.Add(new DataColumn("title", System.Type.GetType("System.String")));
            ds.Columns.Add(new DataColumn("value", System.Type.GetType("System.Int32")));
            ds.Rows.Add("ทุกประเภท");
            foreach (InvestmentTransactionType item in transtype)
                ds.Rows.Add(item.Title.ToString(), item.TransactionTypeID);

            Session[this.ClientID + "trantypes"] = ds;
        }
        return ds;
    }

    private void BindData(ASPxListBox lb)
    {
        lb.DataBind();
    }

    protected void ComboTransactionType_DataBinding(object sender, EventArgs e)
    {
        this.BindData(LBTTYPES);
    }

    protected void lbTransactionTypes_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        //ASPxListBox lbTransactionTypes = (ASPxListBox)sender;
        LBTTYPES = (ASPxListBox)sender;
        LBTTYPES.DataBind();
    }

    protected void lbTransactionTypes_DataBinding(object sender, EventArgs e)
    {
        //ASPxListBox lbTransactionTypes = (ASPxListBox)sender;
        LBTTYPES = (ASPxListBox)sender;

        if (null != Session[this.ClientID + "trantypes"])
            LBTTYPES.DataSource = Session[this.ClientID + "trantypes"];
        else
            LBTTYPES.DataSource = this.TransactionTypeLists();
    }

    public InvestmentTransactionType InvestmentTransactionType
    {
        get
        {
            //please make sure this.SelectionMethod is "Single".
            ASPxListBox lbTransactionTypes = (ASPxListBox)ComboTransactionType.FindControl("lbTransactionTypes");
            if (lbTransactionTypes.SelectedItem == null) return null;
            String id = lbTransactionTypes.SelectedItem.Value.ToString();
            InvestmentTransactionType curr = InvestmentTransactionType.Find(iSabayaContext, int.Parse(id));
            return curr;
        }
        set
        {
            ASPxListBox lbTransactionTypes = (ASPxListBox)ComboTransactionType.FindControl("lbTransactionTypes");
            if (value != null)
            {
                //lbTransactionTypes.SelectionMode = this.SelectMethod;
                foreach (ListEditItem item in lbTransactionTypes.Items)
                {
                    if (item.Value.Equals(value.TransactionTypeID))
                    {
                        lbTransactionTypes.SelectedItem = item;
                        break;
                    }
                }
            }
        }
    }

    public IList<InvestmentTransactionType> TransactionTypes
    {
        get
        {
            //ASPxListBox lbTransactionTypes = (ASPxListBox)ComboTransactionType.FindControl("lbTransactionTypes");
            ASPxListBox lbTransactionTypes = LBTTYPES;
            IList<InvestmentTransactionType> transtype = new List<InvestmentTransactionType>();
            String id = null;
            InvestmentTransactionType curr = null;
            if (lbTransactionTypes.SelectedItems == null) return null;
            int i = 0;
            if (lbTransactionTypes.SelectedItems.Count == lbTransactionTypes.Items.Count)
                i = 1; //i = 1 to filter out all transaction check
            for (; i < lbTransactionTypes.SelectedItems.Count; i++)
            {
                id = lbTransactionTypes.SelectedItems[i].Value.ToString();
                curr = InvestmentTransactionType.Find(iSabayaContext, int.Parse(id));
                transtype.Add(curr);
            }
            return transtype;
        }
    }

    public void InitializeControls(String currentLanguage, ISession session)
    {
    }
}