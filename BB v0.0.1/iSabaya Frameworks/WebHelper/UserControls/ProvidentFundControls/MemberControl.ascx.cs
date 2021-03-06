using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using NHibernate;
using imSabaya;
using iSabaya;
using DevExpress.Web.ASPxEditors;
using System.Collections.Generic;
using NHibernate.Criterion;
using System.Net.Json;
using System.Data.SqlClient;
using WebHelper.pvd;
using imSabaya.ProvidentFundSystem;
using WebHelper;

public partial class ctrls_EmployeeControl : iSabayaControl
{
    public enum EmployeeTerminatedStatus
    {
        All,
        NotTerminated,
        Terminated,
    }

    private String name = "";
    public String Name
    {
        get { return this.name; }
        set { this.name = value; }
    }
    private String departmentCtrlName = "";
    public String DepartmentCtrlName
    {
        get { return this.departmentCtrlName; }
        set { this.departmentCtrlName = value; }
    }

    private bool isUseOnComplete = false;
    public bool IsUseOnComplete
    {
        get { return this.isUseOnComplete; }
        set { this.isUseOnComplete = value; }
    }

    private bool enabled = true;
    public bool Enabled
    {
        get { return this.enabled; }
        set { this.enabled = value; }
    }
    private EmployeeTerminatedStatus terminationStatus = EmployeeTerminatedStatus.All;
    public EmployeeTerminatedStatus TerminationStatus
    {
        get { return this.terminationStatus; }
        set { this.terminationStatus = value; }
    }
    //wichan 08032010 10:03 AM
    #region Validation Section
    private bool isRequiredField = false;

    public bool IsRequiredField
    {
        get { return isRequiredField; }
        set { this.isRequiredField = value; }
    }
    private String controlName;
    public String ControlName
    {
        get { return controlName; }
        set { this.controlName = value; }
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
    protected void Page_Load(object sender, EventArgs e)
    {

        btnEmployeeBrowse.Enabled = Enabled;

        #region coke 14072009 hh:mm
        if (IsRequiredField)
        {

            btnEmployeeBrowse.ValidationSettings.ValidationGroup = ValidationGroup;

            btnEmployeeBrowse.ValidationSettings.SetFocusOnError = true;
            btnEmployeeBrowse.ValidationSettings.ErrorText = "ErrorText";
            btnEmployeeBrowse.ValidationSettings.ValidateOnLeave = true;
            btnEmployeeBrowse.ValidationSettings.ErrorImage.Height = Unit.Pixel(16);
            btnEmployeeBrowse.ValidationSettings.ErrorImage.Width = Unit.Pixel(16);
            btnEmployeeBrowse.ValidationSettings.ErrorImage.AlternateText = "Error";
            btnEmployeeBrowse.ValidationSettings.ErrorImage.Url = "~/Images/iconError.png";
            btnEmployeeBrowse.ValidationSettings.RequiredField.IsRequired = true;
            btnEmployeeBrowse.ValidationSettings.RequiredField.ErrorText = "กรุณากรอกข้อมูล";
            btnEmployeeBrowse.ValidationSettings.ErrorFrameStyle.ForeColor = System.Drawing.Color.Red;
            btnEmployeeBrowse.ValidationSettings.ErrorFrameStyle.Paddings.Padding = Unit.Pixel(0);
            btnEmployeeBrowse.ValidationSettings.ErrorFrameStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            btnEmployeeBrowse.ValidationSettings.ErrorFrameStyle.BackgroundImage.ImageUrl = "~/Images/bgError.png";
            btnEmployeeBrowse.ValidationSettings.ErrorFrameStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#FD4D3E");
            btnEmployeeBrowse.ValidationSettings.ErrorFrameStyle.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            btnEmployeeBrowse.ValidationSettings.ErrorFrameStyle.Border.BorderWidth = Unit.Pixel(0);
            btnEmployeeBrowse.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingRight = Unit.Pixel(0);
			btnEmployeeBrowse.ValidationSettings.Display = Display.Dynamic;
        }
        #endregion

        #region first load
        if (IsPostBack == false)
        {
            //if (Name == "")
            //{
            //    Name = this.ClientID;
            //}
            //load employer to combo
            //IList<Member> employers = loadEmployee();
            Session[this.GetType().ToString() + "GridEmployee"] = null;

            cbpTxtEmployerNo.ClientInstanceName = this.ClientID + cbpTxtEmployerNo.ClientID;

            GridCustomer.ClientInstanceName = this.ClientID + GridCustomer.ClientID;
            txtFirstName.ClientInstanceName = this.ClientID + txtFirstName.ClientID;
            btnFindName.ClientInstanceName = this.ClientID + btnFindName.ClientID;
            CallbacklikeCustomerName.ClientInstanceName = this.ClientID + CallbacklikeCustomerName.ClientID;
            //btnFindName.ClientInstanceName = this.ClientID + btnFindName.ClientID;
            labelTitle.ClientInstanceName = this.ClientID + labelTitle.ClientID;
            popupAccount.ClientInstanceName = this.ClientID + popupAccount.ClientID;
            //cbChangeEmployer.ClientInstanceName = this.ClientID + cbChangeEmployer.ClientID;
            cbChangeEmployer.ClientInstanceName = "employeeControl_cbChangeEmployer";
            lblEmployerID.ClientInstanceName = ControlName != null ? ControlName : this.ClientID + lblEmployerID.ClientID;
            cbFindEmployer.ClientInstanceName = this.ClientID + cbFindEmployer.ClientID;

            cbGetAccountNo.ClientInstanceName = this.ClientID + cbGetAccountNo.ClientID;
            btnAddAll.ClientInstanceName = this.ClientID + btnAddAll.ClientID;

            if (Name != "")
            {
                btnEmployeeBrowse.ClientInstanceName = "btnEmployeeBrowse" + Name;
            }
            else
            {
                btnEmployeeBrowse.ClientInstanceName = this.ClientID + btnEmployeeBrowse.ClientID;
            }
            ddDepartment.ClientInstanceName = "ddDepartment" + departmentCtrlName;


            //Employer employer = getPageEmployer();
            //if (employer != null)
            //{
            //    IList<OrgUnit> units = employer.EmployerOrg.OrgUnits;
            //    ddDepartment.DataSource = units;
            //    ddDepartment.TextField = "FullName";
            //    ddDepartment.ValueField = "ID";
            //    ddDepartment.DataBind();
            //}

            btnEmployeeBrowse.ClientSideEvents.ButtonClick = @"function(s,e){ 
					var id = " + lblEmployerID.ClientInstanceName + @".GetText();
					if(typeof id != 'undefined' && id !== null && id !== ''){
						" + cbFindEmployer.ClientInstanceName + @".SendCallback(id);
                    }
                    if(typeof(onLoadEmployee) != 'undefined'){
                       onLoadEmployee.SendCallback();
                    }
                    var win = " + popupAccount.ClientInstanceName + @".GetWindow(0); 
                    " + popupAccount.ClientInstanceName + @".ShowWindow(win);                                    
                    }";

            btnEmployeeBrowse.ClientSideEvents.LostFocus = @"function(s,e){ 
                " + cbGetAccountNo.ClientInstanceName + @".SendCallback(" + btnEmployeeBrowse.ClientInstanceName + @".GetText());                    
            }";

            cbGetAccountNo.ClientSideEvents.CallbackComplete = @"function(s,e){ 
                var obj = eval('('+e.result+')');
                if(obj.success){
                 " + labelTitle.ClientInstanceName + @".SetText(obj.name);                     
                }else{
                    " + labelTitle.ClientInstanceName + @".SetText(''); 
                    " + btnEmployeeBrowse.ClientInstanceName + @".SetText(''); 
                }
            }";

            String ScriptAppend = "";
            if (IsUseOnComplete)
            {
                ScriptAppend = @"if(typeof(onCompleteLoadEmployee) != 'undefined'){
                                    onCompleteLoadEmployee();
                               }";
            }
            GridCustomer.ClientSideEvents.CustomButtonClick = @"function(s,e){
                var buttonID = e.buttonID;               
                var visibleIndex = e.visibleIndex;
                  if(buttonID = 'buttonSelect')
                  {
                        " + GridCustomer.ClientInstanceName + @".GetRowValues(visibleIndex,'AccountID;EmployeeNo;FullName',
                           function (values){  
                               " + btnEmployeeBrowse.ClientInstanceName + @".SetText(values[1]);
                               " + labelTitle.ClientInstanceName + @".SetText(values[2]);                                                           
                               " + popupAccount.ClientInstanceName + @".Hide();
                                if(typeof(cbSelectEmployee) != 'undefined'){
                                    //alert(values[0]);
                                    cbSelectEmployee.PerformCallback(values[0]);
                                }
                               " + ScriptAppend + @"
                            }
                        );
                  }//end if
            }";

            btnFindName.ClientSideEvents.Click = @"function(s,e){
                  " + CallbacklikeCustomerName.ClientInstanceName + @".SendCallback();
            }";

            CallbacklikeCustomerName.ClientSideEvents.CallbackComplete = @"function(s,e){
                  " + GridCustomer.ClientInstanceName + @".PerformCallback();
            }";

            cbChangeEmployer.ClientSideEvents.CallbackComplete = @"function(s,e){
                  //alert(e.result);
                  //" + lblEmployerID.ClientInstanceName + @".Set('empId',e.result);
                  " + lblEmployerID.ClientInstanceName + @".SetText(e.result);
                  //alert(" + lblEmployerID.ClientInstanceName + @".Get('empId'));
                  //alert(" + lblEmployerID.ClientInstanceName + @".GetText());
                  " + lblEmployerID.ClientInstanceName + @".SetVisible(false);
            }";

            cbFindEmployer.ClientSideEvents.CallbackComplete = @"function(s,e){
                  " + GridCustomer.ClientInstanceName + @".PerformCallback();
                  }";

            btnAddAll.ClientSideEvents.Click = @"function(s,e){
                var grid = " + GridCustomer.ClientInstanceName + @";
                grid.GetSelectedFieldValues('EmployeeNo',
                    function (values){
                                //alert('select'+values.length);
                                var ids='';      
                                for(var i = 0;i < values.length;i++){
                                    ids+='\''+values[i]+'\',';
                                }
                                ids=ids.substring(0, ids.length-1)
                               " + btnEmployeeBrowse.ClientInstanceName + @".SetText(ids);
                                                                           
                               " + popupAccount.ClientInstanceName + @".Hide();
                               
                    }
                );
            }";
        }//end postback false
        #endregion

        if (Session[this.GetType().ToString() + "GridEmployee"] != null)
        {
            GridCustomer.DataSource = (IList<Member>)Session[this.GetType().ToString() + "GridEmployee"];
            GridCustomer.DataBind();
        }
        String[] divs = GetDivisionCodes();
        if (divs != null)
        {
            List<VODivisionCode> vos = new List<VODivisionCode>();
            foreach (String s in divs)
            {
                VODivisionCode vo = new VODivisionCode();
                vo.DivisionCode = s;
                vos.Add(vo);
            }

            ddDepartment.DataSource = vos;
            ddDepartment.ValueField = "DivisionCode";
            ddDepartment.TextField = "DivisionCode";
            ddDepartment.DataBind();
            ddDepartment.SelectedIndex = 0;
        }
    }

    private IList<Member> loadEmployee()
    {
        return iSabayaContext.PersistencySession.CreateCriteria<Member>().List<Member>();
    }

    public Member Member
    {
        get
        {
            String employeeNo = (String)btnEmployeeBrowse.Text;
            Member employee = Member.FindByAccountNo(iSabayaContext, employeeNo);
            //Member employee = new Member();
            return employee;

        }
        set
        {
            if (value != null)
            {
                btnEmployeeBrowse.Text = value.AccountNo;
            }
        }
    }
    public IList<Member> Employees
    {
        get
        {
            String empNo = btnEmployeeBrowse.Text;
            String[] employeeNo = btnEmployeeBrowse.Text.Split(new char[] { ',' });
            if (employeeNo.Length == 1)
            {
                empNo = "'" + btnEmployeeBrowse.Text + "'";
            }

            Employer employer = getPageEmployer();
            String query = @"
		    select * from Account
		    inner join dbo.Employer on dbo.Employer.EmployerID = dbo.Account.EmployerID
		    where  dbo.Account.AccountDiscriminator = 2 and dbo.Employer.EmployerID =" + employer.EmployerID + @"
		    AND (dbo.Account.EffectiveFrom <= GETDATE())  
		    AND (dbo.Account.EffectiveTo >= GETDATE())
            AND AccountNo in (" + empNo + ")";

            IList<Member> employee =
                this.iSabayaContext.PersistencySession.CreateSQLQuery(query).AddEntity("employee",
                typeof(Member)).List<Member>();

            return employee;

        }
        set
        {
            if (value != null)
            {
                String s = "";
                foreach (Member e in value)
                {
                    s += e.AccountNo + ",";
                }
                btnEmployeeBrowse.Text = s.Substring(0, s.Length - 1);
            }
        }
    }
    /*Callback from name search*/
    protected void likeCustomerNameCallback_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        IList<Member> employees = this.FindLikeByName(ASPxRadioButtonList1.SelectedIndex,
            txtFirstName.Text);
        GridCustomer.DataSource = employees;
        GridCustomer.DataBind();
        Session[this.GetType().ToString() + "GridEmployee"] = employees;

    }

    private IList<Member> FindLikeByName(int findType, string keyword)
    {
        String divisionCode = null;
        divisionCode = ddDepartment.SelectedItem != null ? ddDepartment.SelectedItem.Value.ToString() : null;
        Employer employer = getPageEmployer();
        String query = "";
        if (findType == 1)
        {

            if (divisionCode == null || divisionCode.Equals("-All Division-"))
            {
                query = @"select * from Account where AccountID in(
                        SELECT     dbo.Account.AccountID
                        FROM         dbo.Person INNER JOIN
                      dbo.Account ON dbo.Person.PersonID = dbo.Account.EmployeeID INNER JOIN
                      dbo.PersonName ON dbo.Person.CurrentNameID = dbo.PersonName.PersonNameID INNER JOIN
                      dbo.MLSValue ON dbo.PersonName.FirstNameID = dbo.MLSValue.MLSID
                 
                      where  dbo.MLSValue.Value like N'" + keyword + @"%' and Account.AccountDiscriminator=2                     
                        and EmployerID=" + employer.EmployerID + @"
                       ";
            }
            else
            {
                query = @"select * from Account where AccountID in(
                        SELECT     dbo.Account.AccountID
                        FROM         dbo.Person INNER JOIN
                      dbo.Account ON dbo.Person.PersonID = dbo.Account.EmployeeID INNER JOIN
                      dbo.PersonName ON dbo.Person.CurrentNameID = dbo.PersonName.PersonNameID INNER JOIN
                      dbo.MLSValue ON dbo.PersonName.FirstNameID = dbo.MLSValue.MLSID
                    inner join EmployeeInfo  on Account.AccountID=EmployeeInfo.AccountID
                      where  dbo.MLSValue.Value like N'" + keyword + @"%' and Account.AccountDiscriminator=2                     
                        and EmployerID=" + employer.EmployerID + @"
                        and EmployeeInfo.DivisionCode='" + divisionCode + @"')
                       ";
            }
        }
        else if (findType == 2)
        {
            if (divisionCode == null || divisionCode.Equals("-All Division-"))
            {
                query = @"select * from Account where AccountID in(
                        SELECT     dbo.Account.AccountID
                        FROM         dbo.Person INNER JOIN
                      dbo.Account ON dbo.Person.PersonID = dbo.Account.EmployeeID INNER JOIN
                      dbo.PersonName ON dbo.Person.CurrentNameID = dbo.PersonName.PersonNameID INNER JOIN
                      dbo.MLSValue ON dbo.PersonName.LastNameID = dbo.MLSValue.MLSID
                      where  dbo.MLSValue.Value like N'" + keyword + @"%' and Account.AccountDiscriminator=2
                      and EmployerID=" + employer.EmployerID + @"                     
                        )
                       ";
            }
            else
            {
                query = @"select * from Account where AccountID in(
                        SELECT     dbo.Account.AccountID
                        FROM         dbo.Person INNER JOIN
                      dbo.Account ON dbo.Person.PersonID = dbo.Account.EmployeeID INNER JOIN
                      dbo.PersonName ON dbo.Person.CurrentNameID = dbo.PersonName.PersonNameID INNER JOIN
                      dbo.MLSValue ON dbo.PersonName.LastNameID = dbo.MLSValue.MLSID
                    inner join EmployeeInfo  on Account.AccountID=EmployeeInfo.AccountID
                      where  dbo.MLSValue.Value like N'" + keyword + @"%' and Account.AccountDiscriminator=2
                      and EmployerID=" + employer.EmployerID + @"  
                      and EmployeeInfo.DivisionCode='" + divisionCode + @"'         
                        )
                       ";
            }
        }
        else
        {
            if (divisionCode == null || divisionCode.Equals("-All Division-"))
            {
                query = @"  select * from Account inner join EmployeeInfo on EmployeeInfo.EmployeeInfoID = Account.CurrentEmployeeInfoID
                        where Account.AccountDiscriminator = 2 and EmployeeInfo.EmployeeNo like N'" + keyword + @"%'
                         and EmployerID=" + employer.EmployerID + @"
                        ";
            }
            else
            {
                query = @"   select Account.* 
                      from Account              
                      inner join EmployeeInfo  on Account.AccountID=EmployeeInfo.AccountID   
                      and  EmployeeInfo.EmployeeInfoID = Account.CurrentEmployeeInfoID  
                        where Account.AccountDiscriminator = 2 and EmployeeInfo.EmployeeNo like N'" + keyword + @"%'
                         and EmployerID=" + employer.EmployerID + @"
                            and EmployeeInfo.DivisionCode='" + divisionCode + @"'   
                        ";
            }
        }
        string terminationCondition = " and dbo.Account.TerminationInfoID is not null ";
        if (TerminationStatus.Equals(EmployeeTerminatedStatus.Terminated))
            query += terminationCondition;
        IList<Member> results =
            iSabayaContext.PersistencySession.CreateSQLQuery(query).AddEntity("employee",
            typeof(Member)).List<Member>();

        return results;
    }

    private IList<Member> FindByEmployer(ISession session, int EmployerID)
    {
        String query = "";
        query = @"
		select * from Account
					inner join dbo.Employer on dbo.Employer.EmployerID = dbo.Account.EmployerID
					where  dbo.Account.AccountDiscriminator = 2 and dbo.Employer.EmployerID =" + EmployerID + @"
					AND (dbo.Account.EffectiveFrom <= GETDATE())  
					AND (dbo.Account.EffectiveTo >= GETDATE())";
        string terminationCondition = " and dbo.Account.TerminationInfoID is not null ";
        if (TerminationStatus.Equals(EmployeeTerminatedStatus.Terminated))
            query += terminationCondition;
        IList<Member> results =
            session.CreateSQLQuery(query).AddEntity("employee",
            typeof(Member)).List<Member>();

        return results;
    }

    protected void cbpTxtEmployerNo_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {

    }

    protected void cbSendAcc_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        //String code = e.Parameter;
        //Member customer = Member.FindByEmployeeNo(iSabayaContext.PersistencySession, code);
        //if (customer != null)
        //{
        //    e.Result = customer.Name.ToString("th");
        //}
    }

    public Employer getPageEmployer()
    {
        if (Session["SessionEmployer"] == null) return null;
        Employer employer = (Employer)Session["SessionEmployer"];
        return employer;
    }

    protected void ddDepartment_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        Employer employer = getPageEmployer();
        if (employer != null)
        {
            IList<OrgUnit> units = employer.EmployerOrg.OrgUnits;
            ddDepartment.DataSource = units;
            ddDepartment.TextField = "FullName";
            ddDepartment.ValueField = "ID";
            ddDepartment.DataBind();
        }
    }
    protected void cbChangeEmployer_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        //int id = int.Parse(e.Parameter);
        //IList<Member> employees = this.FindByEmployer(iSabayaContext.PersistencySession, id);

        //GridCustomer.DataSource = employees;
        //GridCustomer.DataBind();
        //Session[this.GetType().ToString() + "GridEmployee"] = employees;
        e.Result = e.Parameter;
    }
    protected void cbFindEmployer_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        int id = int.Parse(e.Parameter);
        IList<Member> employees = this.FindByEmployer(iSabayaContext.PersistencySession, id);

        GridCustomer.DataSource = employees;
        GridCustomer.DataBind();
        Session[this.GetType().ToString() + "GridEmployee"] = employees;
    }
    protected void cbGetAccountNo_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        Employer employer = getPageEmployer();
        String query = "";

        String empNo = e.Parameter;
        String[] employeeNo = e.Parameter.Split(new char[] { ',' });
        if (employeeNo.Length == 1)
        {
            empNo = "'" + e.Parameter + "'";
        }

        query = @"
		    select * from Account
		    inner join dbo.Employer on dbo.Employer.EmployerID = dbo.Account.EmployerID
		    where  dbo.Account.AccountDiscriminator = 2 and dbo.Employer.EmployerID =" + employer.EmployerID + @"
		    AND (dbo.Account.EffectiveFrom <= GETDATE())  
		    AND (dbo.Account.EffectiveTo >= GETDATE())
            AND AccountNo in (" + empNo + ")";

        IList<Member> employee =
            this.iSabayaContext.PersistencySession.CreateSQLQuery(query).AddEntity("employee",
            typeof(Member)).List<Member>();

        JsonObjectCollection json = new JsonObjectCollection();
        if (employee.Count > 0)
        {


            json.Add(new JsonBooleanValue("success", true));
            String names = "";
            foreach (Member n in employee)
            {
                names += n.FullName + " ";
            }
            json.Add(new JsonStringValue("name", names));

        }
        else
        {
            json.Add(new JsonBooleanValue("success", false));
        }
        e.Result = json.ToString();

    }

    private String[] GetDivisionCodes()
    {
        Employer employer = getPageEmployer();
        if (employer == null)
            return null;
        List<String> divisionCodesL = new List<String>();
        divisionCodesL.Add("-All Division-");
        if (employer == null) { return null; }
        String connectionString = ConfigurationManager.ConnectionStrings["imSabayaConnectionString"].ToString();
        using (SqlConnection objConn = new SqlConnection(connectionString))
        {

            objConn.Open();

            SqlCommand objComm = objConn.CreateCommand();
            objComm.CommandText = @"
                                    select distinct DivisionCode from EmployeeInfo  inner join
                                    Account a on a.AccountID=EmployeeInfo.AccountID
                                    where a.EmployerID=" + employer.EmployerID;

            SqlDataReader objReader = objComm.ExecuteReader();

            while (objReader.Read())
            {
                if (objReader["DivisionCode"].Equals(null))
                {
                    String divisionCode = (String)objReader["DivisionCode"];
                    divisionCodesL.Add(divisionCode);
                }
            }//end loop
        }//end using
        String[] divisionCodes = divisionCodesL.ToArray();
        Array.Sort(divisionCodes);
        return divisionCodes;
    }

}
