using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using imSabaya;
using System.Collections.Generic;
using NHibernate;
using iSabaya;
using DevExpress.Web.ASPxClasses;
using NHibernate.Criterion;
using imSabaya.ProvidentFundSystem;
using WebHelper;

public partial class ctrls_PVDSingleFundControl : iSabayaControl
{

    public ctrls_PVDSingleFundControl()
    {
        dic = new Dictionary<int, string>();
    }
    
    public event EventHandler SelectedFund;
	private String gridOutput = "nogrid";
	public String GridOutput
	{
		get { return gridOutput; }
		set { this.gridOutput = value; }
	}

	private bool includeAllFundItem = false;
	public bool IncludeAllFundItem
	{
		get { return includeAllFundItem; }
		set { includeAllFundItem = value; }
	}

	private bool isMasterFund = false;
	public bool IsMasterfund
	{
		get { return this.isMasterFund; }
		set { this.isMasterFund = value; }
	}

	private bool isNotMasterFund = false;
	public bool IsNotMasterFund
	{
		get { return this.isNotMasterFund; }
		set { this.isNotMasterFund = value; }
	}

	private String transactionTypeCode = "";
	public String TransactionTypeCode
	{
		get { return transactionTypeCode; }
		set { this.transactionTypeCode = value; }
	}

	private Dictionary<int, String> dic;

	public void Enable(bool isEnable)
	{
		if (isEnable)
		{
			cboFundCode.Enabled = true;
		}
		else
		{
			cboFundCode.Enabled = false;
		}
	}

	private void refreshControl()
	{
		cboFundCode.Items.Clear();

		foreach (int k in dic.Keys)
		{
			String title = dic[k];
			cboFundCode.Items.Add(title, k);
		}
	}

	#region Validation Section
	private bool isRequiredField = false;

	// cha 20-07-09
	public bool IsRequiredField
	{
		get { return isRequiredField; }
		set { this.isRequiredField = value; }
	}

	private String validationGroup;
	/// <summary>
	/// Get or sets the group of controls for which the editor forces validation when it posts back to the server.
	/// </summary>
	public String ValidationGroup
	{
		get { return validationGroup; }
		set { this.validationGroup = value; }
	}
	#endregion

	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		if (!IsPostBack)
		{
			InitialControl();
			cboFundCode.SelectedIndex = 0;

			cboFundCode.ClientInstanceName = cboFundCode.ClientID;
			lblFundName.ClientInstanceName = lblFundName.ClientID;
			cbLoadFund.ClientInstanceName = cbLoadFund.ClientID;

			/*Combo change*/
			cboFundCode.ClientSideEvents.SelectedIndexChanged = @"
                function(s, e) { "
				+ cbLoadFund.ClientInstanceName + @".SendCallback(); 
                }";

			/*Callback complete*/
			cbLoadFund.ClientSideEvents.CallbackComplete =
			@"function(s, e) {"
				+ lblFundName.ClientInstanceName + @".SetValue(e.result);
                if(typeof(" + GridOutput + @") != 'undefined')
                    setTimeout('" + GridOutput + @".PerformCallback()', 5000);
                if(typeof(oncompleteLoadMFFund) != 'undefined')
                    oncompleteLoadMFFund();
           }";
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (IsRequiredField)
            cboFundCode.SetValidation(validationGroup);

		if (ViewState["listFunds"] != null)
		{
			dic = (Dictionary<int, String>)ViewState["listFunds"];
			refreshControl();
		}
		if (IsPostBack == false)
		{
			//            cboFundCode.ClientInstanceName = cboFundCode.ClientID;
			//            lblFundName.ClientInstanceName = lblFundName.ClientID;
			//            cbLoadFund.ClientInstanceName = cbLoadFund.ClientID;

			//            /*Combo change*/
			//            cboFundCode.ClientSideEvents.SelectedIndexChanged = @"
			//                function(s, e) { "
			//                + cbLoadFund.ClientInstanceName + @".SendCallback(); 
			//                }";

			//            /*Callback complete*/
			//            cbLoadFund.ClientSideEvents.CallbackComplete =
			//            @"function(s, e) {" 
			//                + lblFundName.ClientInstanceName + @".SetValue(e.result);
			//                if(typeof(" + GridOutput + @") != 'undefined')
			//                    setTimeout('" + GridOutput + @".PerformCallback()', 5000);
			//                if(typeof(oncompleteLoadMFFund) != 'undefined')
			//                    oncompleteLoadMFFund();
			//           }";
		}
	}

	public void InitialControl()
	{
		ISession session = iSabayaContext.PersistencySession;
		IList<ProvidentFund> listFunds = null;
		//        if (isMasterFund)
		//        {
		//            // do notthing
		//        }
		//        else if (isNotMasterFund)
		//        {
		//            String sqlquery = @"select * from fund
		//                                where IsMasterFund = 0 and FundDiscriminator = 2";
		//            ISQLQuery query = session.CreateSQLQuery(sqlquery).AddEntity(typeof(ProvidentFund)); ;
		//            listFunds = query.List<ProvidentFund>();
		//        }
		//        else
		//        {
		//            //listFunds = ProvidentFund.List(iSabayaContext);
		//        }
        listFunds = ProvidentFund.ListSingleFunds(iSabayaContext);
		List<ProvidentFund> listFundFilters = new List<ProvidentFund>();
		if (!transactionTypeCode.Equals(""))
		{
			TreeListNode channel = TreeListNode.FindByCode(iSabayaContext, "WEB");
            InvestmentTransactionType tranType = InvestmentTransactionType.FindByCode(iSabayaContext, TransactionTypeCode);
			if (listFunds != null)
			{
				foreach (ProvidentFund fund in listFunds)
				{
					//bool isHave = InstrumentTransactionType.IsHave(session, fund, channel, tranType);
                    ICriteria crit = iSabayaContext.PersistencySession.CreateCriteria(typeof(InstrumentTransactionType));
					crit.Add(Expression.Eq("Fund", fund));
					bool isHave = crit.List<InstrumentTransactionType>().Count > 0;
					if (isHave)
					{
						listFundFilters.Add(fund);
					}
				}
			}
		}
		else
		{
			if (listFunds != null)
			{
				foreach (ProvidentFund fund in listFunds)
				{
					listFundFilters.Add(fund);
				}
			}
		}

		dic.Clear();

		if (includeAllFundItem)
		{
			dic.Add(0, "-All Funds-");
		}
		if (listFunds != null)
		{
			foreach (ProvidentFund fund in listFundFilters)
			{
				//dic.Add(fund.FundID, "[" + fund.Code + "]" + fund.ShortTitle.ToString());
				dic.Add(fund.FundID, fund.Code);
			}
			ViewState["listFunds"] = dic;
		}
		refreshControl();
	}

	//public void InitialControl(string currentLanguage, ISession session)
	//{
	//}

	//private void initControl() //string currentLanguage, ISession session)
	//{
	//}

	public Fund Fund
	{
		get
		{
			if (cboFundCode.SelectedItem == null)
			{
				return null;
			}
			if (((int)cboFundCode.SelectedItem.Value) == 0)
			{
				return null;
			}
			if (cboFundCode.SelectedItem == null)
			{
				return null;
			}
			int fId = (int)cboFundCode.SelectedItem.Value;
			if (fId != -1)
			{
				ProvidentFund fund = iSabayaContext.PersistencySession.Get<ProvidentFund>(fId);
				return fund;
			}
			else
			{
				return null;
			}
		}
		set
		{
			Fund fund = value;
			if (fund != null)
			{
				foreach (ListEditItem item in cboFundCode.Items)
				{
					if (item.Value.ToString().Equals(fund.FundID.ToString()))
					{
						cboFundCode.SelectedItem = item;
						break;
					}
				}
			}
		}
	}

	protected void cbLoadFund_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
	{
		if (this.Fund == null)
		{
			e.Result = "";
			return;
		}
		e.Result = this.Fund.Title.GetValue(iSabayaContext.CurrentLanguage.Code);
		if (SelectedFund != null)
		{
			SelectedFund(this, EventArgs.Empty);
		}
	}
}
