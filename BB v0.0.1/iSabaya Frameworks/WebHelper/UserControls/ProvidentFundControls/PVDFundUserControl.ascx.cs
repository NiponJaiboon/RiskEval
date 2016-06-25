using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using imSabaya;
using imSabaya.ProvidentFundSystem;
using WebHelper;

public partial class PVDFundUserControl : iSabayaControl
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}

	protected void GridView_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
	{
		ASPxGridView grid = (ASPxGridView)DropDownEdit.FindControl("GridView");
		object[] employeeNames = new object[grid.VisibleRowCount];
		object[] keyValues = new object[grid.VisibleRowCount];
		for (int i = 0; i < grid.VisibleRowCount; i++)
		{
			employeeNames[i] = grid.GetRowValues(i, "EmployerNo") + "/ " + grid.GetRowValues(i, "FundName");
			keyValues[i] = grid.GetRowValues(i, "EmployeeIDFundID");
		}
		e.Properties["cpEmployeeNames"] = employeeNames;
		e.Properties["cpKeyValues"] = keyValues;
	}

	public ProvidentFund ProvidentFund
	{
		get
		{
			String[] ids = getId();
			if (ids == null)
			{
				return null;
			}
			int fundId = int.Parse(ids[1]);
			ProvidentFund fund = ProvidentFund.Find(iSabayaContext, fundId);
			return fund;
		}
	}
	public Employer Employer
	{
		get
		{
			String[] ids = getId();
			int employerId = int.Parse(ids[0]);
			Employer employer = Employer.Find(iSabayaContext, employerId);

			return employer;
		}
	}
	private String[] getId()
	{
		String EmployeeIDFundID = (String)DropDownEdit.KeyValue;
		if (EmployeeIDFundID == null)
		{
			return null;
		}
		String[] ids = EmployeeIDFundID.Split(new char[] { '-' });
		return ids;
	}

}
