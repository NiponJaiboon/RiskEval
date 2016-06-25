using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using imSabaya;
using System.Web.UI.HtmlControls;
using NHibernate.Criterion;
using NHibernate;
using System.Text;
using System.Collections;
using System.Data;
using WebHelper;
using imSabaya.ProvidentFundSystem;

public partial class ctrls_EmployeesControl : iSabayaControl
{
    public enum TerminationState
    {
        NotTerminated,
        Terminated,
        All,
    };
    public enum EffectiveState
    {
        Effective,
        Expire,
        All,
    }
    public enum RedemptionOption
    {
        All,
        Entire,
        Installment
    }

    public ctrls_EmployerControl EmployerControl { get; set; }

    private const string FIELDNAME_IS_TERMINATED = "IsTerminated";
    private const string FIELDNAME_SERVICE_YEARS = "ServiceYears";
    private const string FIELDNAME_EMPLOYEENO = "EmployeeNo";
    private const string FIELDNAME_ACCOUNTID = "AccountID";
    private const string FIELDNAME_EMPLOYEENAME = "EmployeeName";
    private const string FIELDNAME_DIVISIONCODE = "DivisionCode";
    private const string FIELDNAME_DIFF_EFFECTIVE_FROM = "diffEffectiveFrom";
    private const string FIELDNAME_DIFF_EFFECTIVE_TO = "diffEffectiveTo";
    private const string FIELDNAME_REDEMPTION_OPTION = "RedemptionOption";

    private const string JS_CLASS_NAME = "EmployeesControl";

    private const string GRID_BINDING = "gdvBidding";
    private const string GRID_INVERSE = "gdvInverseSelected";
    private const string GRID_SELECT_BY_VALUE = "gdvSelectedByValue";
    private const string GRID_SELECTED = "gdvSelected";
    private const string GRID_UNDO_SELECTED = "gdvUndoSelected";

    private const string JSPROPERTY_SELECTED_ACCOUNTID = "cpSelectedAccountID";
    private const string JSPROPERTY_SELECTED_EMPLOYEENO = "cpSelectedEmployeeNo";
    private const string JSPROPERTY_CALLBACK_TYPE = "cpCallbackSelect";
    private const string JSPROPERTY_LBL_SELECTED = "cpLblSelected";
    private const string JSPROPERTY_TEXT_POPUP_SELECTED = "cpTextSelected";

    private const string HDDKEY_EMPLOYEES_SELECTED = "employees";
    private const string HDDKEY_PREVIOUS_EMPLOYEENO = "previousEmployeesNo";
    private const string HDDKEY_SELECTED_INDEX = "selectedIndex";

    //private const string TEXT_EMPLOYEES = "[ + ]";

    private TerminationState terminationStatus = TerminationState.All;
    public TerminationState TerminationStatus
    {
        get { return this.terminationStatus; }
        set { this.terminationStatus = value; }
    }

    private EffectiveState effectiveStatus = EffectiveState.Effective;
    public EffectiveState EffectiveStatus
    {
        get { return this.effectiveStatus; }
        set { this.effectiveStatus = value; }
    }

    private RedemptionOption terminationRedeptionOption = RedemptionOption.All;
    public RedemptionOption TerminationRedeptionOption
    {
        get { return this.terminationRedeptionOption; }
        set { this.terminationRedeptionOption = value; }
    }

    private bool multiSelection = true;
    public bool MultiSelection
    {
        get { return this.multiSelection; }
        set { this.multiSelection = value; }
    }
    public string ServiceYearsExpression { get; set; }  // example : <5, >=5

    private bool enabled = true;
    public bool Enabled
    {
        get { return this.enabled; }
        set { bteEmployees.Enabled = this.enabled = value; }
    }
    private bool useSessionEmployer = false;
    public bool UseSessionEmployer
    {
        get { return this.useSessionEmployer; }
        set { this.useSessionEmployer = value; }
    }
    private bool isRequiredField = false;
    public bool IsRequiredField
    {
        get { return this.isRequiredField; }
        set { this.isRequiredField = value; }
    }

    public string ValidationGroup { get; set; }

    public string HeaderText
    {
        get { return popupEmployees.HeaderText; }
        set { popupEmployees.HeaderText = value; }
    }
    public string OnAfterChangeEmployees { set; get; }

    public Member Member
    {
        get
        {
            IList<Member> employees = Employees;
            if (employees == null) return null;
            return employees.Count > 0 ? employees[0] : null;
        }
    }
    public IList<object> AccountIDs
    {
        get
        {
            return gridEmployees.GetSelectedFieldValues(FIELDNAME_ACCOUNTID);
        }
    }
    public IList<Member> Employees
    {
        get
        {
            IList<object> selectedEmployees = AccountIDs;
            if (selectedEmployees.Count == 0) return null;
            ICriteria cri = iSabayaContext.PersistencySession.CreateCriteria<Member>()
                                .Add(Expression.In("AccountID", selectedEmployees.ToArray<object>()));
            return cri.List<Member>();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        bteEmployees.Enabled = this.enabled;
        if (!Enabled) return;
        if (!Page.IsCallback)
        {
            //gdvEmployees = ddeEmployees.FindControl("gridEmployees") as ASPxGridView;
            //btnSelectAll = ddeEmployees.FindControl("btnSelectAll") as ASPxButton;
            //btnClear = ddeEmployees.FindControl("btnClear") as ASPxButton;
            //btnInverse = ddeEmployees.FindControl("btnInverse") as ASPxButton;
            //btnSelect = ddeEmployees.FindControl("btnSelect") as ASPxButton;
            //sdsEmployees = ddeEmployees.FindControl("sdsEmployees") as SqlDataSource;

            gridEmployees.ClientInstanceName = this.ClientID + gridEmployees.ID; // GRID_EMPLOYEES_NAME;
            popupEmployees.ClientInstanceName = this.ClientID + popupEmployees.ID; // POPUP_EMPLOYEES_NAME;
            hddEmployeesSelected.ClientInstanceName = this.ClientID + hddEmployeesSelected.ID; // HIDDENFIELD_EMPLOYEES_NAME;
            bteEmployees.ClientInstanceName = this.ClientID + bteEmployees.ID; // BTE_EMPLOYEES_NAME;
            lblEmployee.ClientInstanceName = this.ClientID + lblEmployee.ID; // LBL_EMPLOYEE_NAME;
            popupSelectedEmployees.ClientInstanceName = this.ClientID + popupSelectedEmployees.ID; // POPUP_SEL_EMPLOYEES_NAME;
            imgDetail.ClientInstanceName = this.ClientID + imgDetail.ID;

            if (IsRequiredField)
                bteEmployees.SetValidation(ValidationGroup, true);
            gridEmployees.SettingsBehavior.AllowMultiSelection = multiSelection;
            if (multiSelection)
            {
                gridEmployees.ClientSideEvents.EndCallback = @"function(s,e)
                        {
                            if(s." + JSPROPERTY_CALLBACK_TYPE + " == '" + GRID_SELECT_BY_VALUE + @"' || 
                                s." + JSPROPERTY_CALLBACK_TYPE + " == '" + GRID_SELECTED + @"')
                            {
                                var hdd = " + hddEmployeesSelected.ClientInstanceName + @";
                                var pop = " + popupSelectedEmployees.ClientInstanceName + @";
                                var bte = " + bteEmployees.ClientInstanceName + @";
                                hdd.Set('" + HDDKEY_PREVIOUS_EMPLOYEENO + @"', s." + JSPROPERTY_SELECTED_EMPLOYEENO + @");
                                bte.SetText(s." + JSPROPERTY_SELECTED_EMPLOYEENO + @");
                                pop.SetContentHtml(s." + JSPROPERTY_TEXT_POPUP_SELECTED + @");
                                " + OnAfterChangeEmployees + @"
                            }
                            
                            document.getElementById('" + imgLoader.ClientID + @"').style.visibility='hidden';
                        }";
                popupEmployees.ClientSideEvents.CloseButtonClick = @"function(s,e)
                        {
                            var gdv = " + gridEmployees.ClientInstanceName + @";
                            if(typeof gdv." + JSPROPERTY_SELECTED_ACCOUNTID + @" != 'undefined')
                            {
                                " + hddEmployeesSelected.ClientInstanceName + @".Set('" + HDDKEY_EMPLOYEES_SELECTED
                                  + @"', gdv." + JSPROPERTY_SELECTED_ACCOUNTID + @");
                                gdv.PerformCallback('" + GRID_UNDO_SELECTED + @"');
                            }
                        }";
                imgDetail.ClientSideEvents.Click = @"function(s,e)
                        {
                            var win = " + popupSelectedEmployees.ClientInstanceName + @".GetWindow(0); 
                            " + popupSelectedEmployees.ClientInstanceName + @".ShowWindow(win); 
                        }";
                btnSelectAll.ClientSideEvents.Click = @"function(s,e)
                        {
                            " + gridEmployees.ClientInstanceName + @".SelectRows();
                        }";
                btnClear.ClientSideEvents.Click = @"function(s,e)
                        {
                            " + gridEmployees.ClientInstanceName + @".UnselectRows();
                        }";
                btnInverse.ClientSideEvents.Click = @"function(s,e)
                        {
                            " + gridEmployees.ClientInstanceName + @".PerformCallback('" + GRID_INVERSE + @"');
                        }";
                btnSelect.ClientSideEvents.Click = @"function(s,e)
                        {
                            " + gridEmployees.ClientInstanceName + @".PerformCallback('" + GRID_SELECTED + @"');
                            " + popupEmployees.ClientInstanceName + @".Hide();
                        }";
                lblEmployee.Visible = false;
                popupSelectedEmployees.PopupElementID = imgDetail.ClientID;
                gridEmployees.Columns[1].Visible = false;
                gridEmployees.Columns[0].Visible = true;
            }
            else
            {
                gridEmployees.ClientSideEvents.EndCallback = @"function(s,e)
                        {
                            if(s." + JSPROPERTY_CALLBACK_TYPE + " == '" + GRID_SELECT_BY_VALUE + @"' || 
                                s." + JSPROPERTY_CALLBACK_TYPE + " == '" + GRID_SELECTED + @"')
                            {
                                var lbl  = " + lblEmployee.ClientInstanceName + @";
                                var hdd = " + hddEmployeesSelected.ClientInstanceName + @";
                                var bte = " + bteEmployees.ClientInstanceName + @";
                                lbl.SetText(s." + JSPROPERTY_LBL_SELECTED + @");
                                hdd.Set('" + HDDKEY_PREVIOUS_EMPLOYEENO + @"', s." + JSPROPERTY_SELECTED_EMPLOYEENO + @");
                                bte.SetText(s." + JSPROPERTY_SELECTED_EMPLOYEENO + @");
                                " + OnAfterChangeEmployees + @"
                            }
                            document.getElementById('" + imgLoader.ClientID + @"').style.visibility='hidden';
                        }";

                gridEmployees.ClientSideEvents.CustomButtonClick = @"function(s,e)
                        {
                            if(e.buttonID == 'btnGridSelect')
                            {
                                " + hddEmployeesSelected.ClientInstanceName + @".Set('" + HDDKEY_SELECTED_INDEX + @"',e.visibleIndex);
                                s.PerformCallback('" + GRID_SELECTED + @"');
                                " + popupEmployees.ClientInstanceName + @".Hide();
                            }
                        }";
                gridEmployees.ClientSideEvents.RowDblClick = @"function(s,e)
                        {
                            " + hddEmployeesSelected.ClientInstanceName + @".Set('" + HDDKEY_SELECTED_INDEX + @"',e.visibleIndex);
                                s.PerformCallback('" + GRID_SELECTED + @"');
                            " + popupEmployees.ClientInstanceName + @".Hide();
                        }";
                imgDetail.Visible = false;
                gridEmployees.Columns[0].Visible = false;
                gridEmployees.Columns[1].Visible = true;
                btnSelectAll.Visible = false;
                btnClear.Visible = false;
                btnInverse.Visible = false;
                btnSelect.Visible = false;
            }
            gridEmployees.ClientSideEvents.BeginCallback = @"function(s,e)
                        {
                            document.getElementById('" + imgLoader.ClientID + @"').style.visibility='visible';
                        }";
            btnReflesh.ClientSideEvents.Click = @"function(s,e)
                        {
                            " + gridEmployees.ClientInstanceName + @".PerformCallback('" + GRID_BINDING + @"');
                        }";
            bteEmployees.ClientSideEvents.LostFocus = @"function(s,e)
                        {
                            var hdd  = " + hddEmployeesSelected.ClientInstanceName + @";
                            var text = s.GetText();
                            if(text != hdd.Get('" + HDDKEY_PREVIOUS_EMPLOYEENO + @"'))
                            {
                                hdd.Set('" + HDDKEY_PREVIOUS_EMPLOYEENO + @"', text);
                            " + gridEmployees.ClientInstanceName + @".PerformCallback('" + GRID_SELECT_BY_VALUE + @"');
                            }
                        }";
            bteEmployees.ClientSideEvents.Init = @"function(s,e)
                        {
                            s.SetText('');
                            " + hddEmployeesSelected.ClientInstanceName + @".Set('" + HDDKEY_PREVIOUS_EMPLOYEENO + @"',s.GetText());
                            s.SetIsValid(true);
                        }";
            bteEmployees.ClientSideEvents.ButtonClick = @"function(s,e)
                        {
                            var hdd  = " + hddEmployeesSelected.ClientInstanceName + @";
                            var pop = " + popupEmployees.ClientInstanceName + @";
                            var text = s.GetText();
                            if(text != hdd.Get('" + HDDKEY_PREVIOUS_EMPLOYEENO + @"'))
                            {
                                hdd.Set('" + HDDKEY_PREVIOUS_EMPLOYEENO + @"', text);
                            " + gridEmployees.ClientInstanceName + @".PerformCallback('" + GRID_SELECT_BY_VALUE + @"');
                            }
                            var win = pop.GetWindow(0); 
                            pop.ShowWindow(win); 
                        }";

            string scriptStr = "<script type=\"text/javascript\">";
            scriptStr += @"
                function " + JS_CLASS_NAME + @"(ctrlName,gdvName)
                {
                    this.control = document.getElementsByTagName(ctrlName);
                    this.gridEmployees = gdvName;
                }
                " + JS_CLASS_NAME + @".prototype.GridPerformCallback = function()
                {
                    this.GetGridEmployees().PerformCallback('" + GRID_BINDING + @"');
                }
                " + JS_CLASS_NAME + @".prototype.GetGridEmployees = function()
                {
                    return window[this.gridEmployees];
                }"
            ;
            scriptStr += "</script>\n";
            if (!Page.ClientScript.IsClientScriptBlockRegistered(JS_CLASS_NAME))
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), JS_CLASS_NAME, scriptStr);

            string scriptInstance = "<script type=\"text/javascript\">";
            scriptInstance += @"
                var " + this.ID + " = new " + JS_CLASS_NAME + @"('" + this.ClientID + @"', '" + gridEmployees.ClientInstanceName + @"');
                window['" + this.ID + @"'] = " + this.ID + @";"
            ;
            scriptInstance += "\n</script>\n";
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.ID))
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ID, scriptInstance);

            popupEmployees.PopupElementID = bteEmployees.ClientID;

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsCallback)
        //{
        //    bteEmployees.Value = null;
        //}
    }

    //private void EmployeesBindGrid()
    //{
    //    sdsEmployees.Select(new DataSourceSelectArguments());
    //    gridEmployees.DataBind();
    //}
    private void SetParameters()
    {
        Int32 employerID = 0;
        if (this.useSessionEmployer)
        {
            Employer employer = Session["SessionEmployer"] as Employer;
            if (employer != null)
                employerID = employer.EmployerID;
        }
        else
        {
            if (EmployerControl != null)
                employerID = EmployerControl.EmployerID;
        }
        if (employerID == 0) return;
        sdsEmployees.SelectParameters["employerID"].DefaultValue = employerID.ToString();
        sdsEmployees.SelectParameters["lang"].DefaultValue = base.iSabayaContext.CurrentLanguage.Code;
        switch (TerminationStatus)
        {
            case TerminationState.NotTerminated:
                sdsEmployees.FilterExpression = String.Format("{0} = 0", FIELDNAME_IS_TERMINATED);
                break;
            case TerminationState.Terminated:
                sdsEmployees.FilterExpression = String.Format("{0} = 1", FIELDNAME_IS_TERMINATED);
                if (!String.IsNullOrEmpty(ServiceYearsExpression))
                    sdsEmployees.FilterExpression += String.Format(" AND {0} {1}", FIELDNAME_SERVICE_YEARS, ServiceYearsExpression);

                if (TerminationRedeptionOption == RedemptionOption.Installment)
                    sdsEmployees.FilterExpression += String.Format(" AND {0} > 0", FIELDNAME_REDEMPTION_OPTION);
                else if (TerminationRedeptionOption == RedemptionOption.Entire)
                    sdsEmployees.FilterExpression += String.Format(" AND {0} = 0 ", FIELDNAME_REDEMPTION_OPTION);
                break;
            default: break;
        }
        StringBuilder condition;
        switch (EffectiveStatus)
        {
            case EffectiveState.Effective:
                condition = new StringBuilder(sdsEmployees.FilterExpression);
                if (!string.IsNullOrEmpty(sdsEmployees.FilterExpression))
                    condition.Append(" AND ");
                condition.Append(String.Format("({0} <= 0 AND {1} >= 0)",
                        FIELDNAME_DIFF_EFFECTIVE_FROM, FIELDNAME_DIFF_EFFECTIVE_TO));
                sdsEmployees.FilterExpression = condition.ToString();
                break;
            case EffectiveState.Expire:
                condition = new StringBuilder(sdsEmployees.FilterExpression);
                if (!string.IsNullOrEmpty(sdsEmployees.FilterExpression))
                    condition.Append(" AND ");
                condition.Append(String.Format("({0} > 0 OR {1} < 0)",
                        FIELDNAME_DIFF_EFFECTIVE_FROM, FIELDNAME_DIFF_EFFECTIVE_TO));
                sdsEmployees.FilterExpression = condition.ToString();
                break;
            default: break;
        }
    }
    public void LoadEmployees()
    {
        //this.SetParameters();
        //this.EmployeesBindGrid();
        gridEmployees.DataBind();
    }
    protected void gridEmployees_BeforePerformDataSelect(object sender, EventArgs e)
    {
        SetParameters();
    }
    protected void gridEmployees_CustomCallback(object source, ASPxGridViewCustomCallbackEventArgs e)
    {
        string param = e.Parameters.ToString();
        ASPxGridView gdv = source as ASPxGridView;
        gdv.JSProperties[JSPROPERTY_CALLBACK_TYPE] = param;
        switch (param)
        {
            case GRID_INVERSE:
                if (gdv.VisibleRowCount > 0)
                    for (int i = 0; i < gdv.VisibleRowCount; i++)
                        gdv.Selection.SetSelection(i, !gdv.Selection.IsRowSelected(i));
                break;
            case GRID_BINDING:
                gdv.DataBind();
                break;
            case GRID_SELECT_BY_VALUE:
                if (gdv.VisibleRowCount > 0 && bteEmployees.Text.Length > 0)
                {
                    DataRow dr;
                    if (multiSelection)
                    {
                        string[] employeesNos = bteEmployees.Text.Split(',');
                        if (employeesNos.Length > 0)
                        {
                            bool isContain;
                            StringBuilder accounts = new StringBuilder();
                            StringBuilder selectedText = new StringBuilder();
                            StringBuilder employeeNos = new StringBuilder();
                            for (int i = 0; i < gdv.VisibleRowCount; i++)
                            {
                                dr = gdv.GetDataRow(i);
                                isContain = employeesNos.Contains<string>(dr[FIELDNAME_EMPLOYEENO].ToString());
                                gdv.Selection.SetSelection(i, isContain);
                                if (isContain)
                                {
                                    accounts.AppendFormat(",{0}", dr[FIELDNAME_ACCOUNTID]);
                                    employeeNos.AppendFormat(",{0}", dr[FIELDNAME_EMPLOYEENO]);
                                    selectedText.AppendFormat("<br>{0} - {1} - {2}", dr[FIELDNAME_DIVISIONCODE],
                                        dr[FIELDNAME_EMPLOYEENO], dr[FIELDNAME_EMPLOYEENAME]);

                                }
                            }
                            if (accounts.Length > 0)
                            {
                                //gdv.JSProperties[JSPROPERTY_LBL_SELECTED] = TEXT_EMPLOYEES;
                                gdv.JSProperties[JSPROPERTY_TEXT_POPUP_SELECTED] = selectedText.ToString();
                                gdv.JSProperties[JSPROPERTY_SELECTED_ACCOUNTID] = accounts.ToString().Substring(1);
                                gdv.JSProperties[JSPROPERTY_SELECTED_EMPLOYEENO] = employeeNos.ToString().Substring(1);
                                return;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < gdv.VisibleRowCount; i++)
                        {
                            dr = gdv.GetDataRow(i);
                            gdv.Selection.SetSelection(i, bteEmployees.Text == dr[FIELDNAME_EMPLOYEENO].ToString());
                            if (gdv.Selection.IsRowSelected(i))
                            {
                                gdv.JSProperties[JSPROPERTY_LBL_SELECTED] = dr[FIELDNAME_EMPLOYEENAME];
                                gdv.JSProperties[JSPROPERTY_SELECTED_ACCOUNTID] = dr[FIELDNAME_ACCOUNTID];
                                gdv.JSProperties[JSPROPERTY_SELECTED_EMPLOYEENO] = dr[FIELDNAME_EMPLOYEENO];
                                return;
                            }
                        }
                    }
                }
                gdv.Selection.UnselectAll();
                gdv.JSProperties[JSPROPERTY_LBL_SELECTED] = "";
                gdv.JSProperties[JSPROPERTY_TEXT_POPUP_SELECTED] = "";
                gdv.JSProperties[JSPROPERTY_SELECTED_ACCOUNTID] = "";
                gdv.JSProperties[JSPROPERTY_SELECTED_EMPLOYEENO] = "";

                break;
            case GRID_SELECTED:
                if (gdv.VisibleRowCount > 0)
                {
                    if (multiSelection)
                    {
                        StringBuilder accounts = new StringBuilder();
                        StringBuilder selectedText = new StringBuilder();
                        StringBuilder employeeNos = new StringBuilder();
                        List<object> selectedList = gdv.GetSelectedFieldValues(FIELDNAME_ACCOUNTID, FIELDNAME_DIVISIONCODE
                                                                        , FIELDNAME_EMPLOYEENO, FIELDNAME_EMPLOYEENAME);
                        foreach (object obj in selectedList)
                        {
                            object[] data = obj as object[];
                            accounts.AppendFormat(",{0}", data[0]);
                            employeeNos.AppendFormat(",{0}", data[2]);
                            selectedText.AppendFormat("<br>{0} - {1} - {2}", data[1], data[2], data[3]);

                        }
                        if (accounts.Length > 0)
                        {
                            //gdv.JSProperties[JSPROPERTY_LBL_SELECTED] = TEXT_EMPLOYEES;
                            gdv.JSProperties[JSPROPERTY_TEXT_POPUP_SELECTED] = selectedText.ToString();
                            gdv.JSProperties[JSPROPERTY_SELECTED_ACCOUNTID] = accounts.ToString().Substring(1);
                            gdv.JSProperties[JSPROPERTY_SELECTED_EMPLOYEENO] = employeeNos.ToString().Substring(1);
                        }
                        else
                        {
                            //gdv.JSProperties[JSPROPERTY_LBL_SELECTED] = "";
                            gdv.JSProperties[JSPROPERTY_TEXT_POPUP_SELECTED] = "";
                            gdv.JSProperties[JSPROPERTY_SELECTED_ACCOUNTID] = "";
                            gdv.JSProperties[JSPROPERTY_SELECTED_EMPLOYEENO] = "";
                        }
                    }
                    else
                    {
                        int index = Int32.Parse(hddEmployeesSelected[HDDKEY_SELECTED_INDEX].ToString());
                        object[] data = gdv.GetRowValues(index, FIELDNAME_ACCOUNTID, FIELDNAME_EMPLOYEENO
                                                        , FIELDNAME_EMPLOYEENAME) as object[];
                        gdv.JSProperties[JSPROPERTY_SELECTED_ACCOUNTID] = data[0];
                        gdv.JSProperties[JSPROPERTY_SELECTED_EMPLOYEENO] = data[1];
                        gdv.JSProperties[JSPROPERTY_LBL_SELECTED] = data[2];
                        gdv.Selection.UnselectAll();
                        gdv.Selection.SetSelection(index, true);
                    }
                }
                break;
            case GRID_UNDO_SELECTED:
                gdv.Selection.UnselectAll();
                if (!hddEmployeesSelected.Contains(HDDKEY_EMPLOYEES_SELECTED) ||
                    String.IsNullOrEmpty(hddEmployeesSelected[HDDKEY_EMPLOYEES_SELECTED].ToString())) return;
                string[] keys = hddEmployeesSelected[HDDKEY_EMPLOYEES_SELECTED].ToString().Split(',');
                foreach (string key in keys)
                    gdv.Selection.SetSelectionByKey(Int32.Parse(key), true);
                break;
            default: break;
        }
    }
}
