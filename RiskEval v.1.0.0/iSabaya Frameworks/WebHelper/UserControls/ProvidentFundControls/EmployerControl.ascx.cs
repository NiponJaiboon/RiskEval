using System;
using System.Data;
using System.Collections;
using System.Web;
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
using WebHelper.ValueObject.master;
using imSabaya.ProvidentFundSystem;
using WebHelper;

public partial class ctrls_EmployerControl : iSabayaControl
{
    public string JSEmployerChangedFName
    {
        get { return this.ID + "_EmployerChanged"; }
    }

    private String name = "";
    public String Name
    {
        get { return this.name; }
        set { this.name = value; }
    }
    //wichan 17022010
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        cboAccountNo.DataSource = Employer.ListAll(iSabayaContext);
        cboAccountNo.ValueField = "EmployerID";
        cboAccountNo.TextField = "OrganizationCode";
        cboAccountNo.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        #region first load
        if (IsPostBack == false)
        {
            if (Name == "")
            {
                Name = this.ClientID;
            }
            //load employer to combo
            IList<Employer> employers = Employer.ListAll(iSabayaContext);

            cboAccountNo.ClientInstanceName = this.ClientID + cboAccountNo.ClientID;
            cbpTxtEmployerNo.ClientInstanceName = this.ClientID + cbpTxtEmployerNo.ClientID;
            btnBrowse.ClientInstanceName = this.ClientID + btnBrowse.ClientID;
            GridCustomer.ClientInstanceName = this.ClientID + GridCustomer.ClientID;
            txtFirstName.ClientInstanceName = this.ClientID + txtFirstName.ClientID;
            btnFindName.ClientInstanceName = this.ClientID + btnFindName.ClientID;
            CallbacklikeCustomerName.ClientInstanceName = this.ClientID + CallbacklikeCustomerName.ClientID;
            btnFindName.ClientInstanceName = this.ClientID + btnFindName.ClientID;
            labelTitle.ClientInstanceName = this.ClientID + labelTitle.ClientID;
            popupAccount.ClientInstanceName = this.ClientID + popupAccount.ClientID;


            /*Create json for store MAP<EmployerID, Name>*/
            JsonObjectCollection json = null;
            if (Session[this.ClientID + "ctrls_MFAccountControlNew_employers"] == null)
            {
                json = new JsonObjectCollection();
                foreach (Employer employer in employers)
                {
                    json.Add(new JsonStringValue(employer.EmployerID + "", employer.Name.ToString(iSabayaContext.CurrentCurrency.Code)));
                }
                Session[this.ClientID + "ctrls_MFAccountControlNew_employers"] = json.ToString();
            }
            /*Create json for store MAP<EmployerID, Name>*/
            String mfaccountMap = (String)Session[this.ClientID + "ctrls_MFAccountControlNew_employers"];

            cboAccountNo.ClientSideEvents.Init = @"
            function(s,e){
                document.emploterMap = eval(" + mfaccountMap + @"); 
            }
            ";

            cboAccountNo.ClientSideEvents.SelectedIndexChanged = @"function(s,e){       
              
                " + labelTitle.ClientInstanceName + @".SetText(document.emploterMap[s.GetValue()]);
                if(typeof(" + JSEmployerChangedFName + @") != 'undefined')
                {
                    " + JSEmployerChangedFName + @"(s.GetValue());
                }else
                {
                    if(typeof(oncompleteLoadEmployer) != 'undefined'){
                       oncompleteLoadEmployer();
                    }
                    if(typeof(employeeControl_cbChangeEmployer) != 'undefined'){
					    employeeControl_cbChangeEmployer.SendCallback(s.GetValue());
				    }
				    if(typeof(selectreport2_cbpChangeEmployer) != 'undefined'){
					    //alert(s.GetValue());
					    //selectreport2_cbpChangeEmployer.PerformCallback();
				    }
                }
            }
            ";


            CallbacklikeCustomerName.ClientSideEvents.CallbackComplete = @"function(s, e) {                
                
                " + GridCustomer.ClientInstanceName + @".PerformCallback();                
            }";

            btnFindName.ClientSideEvents.Click = @"function(s, e) {
            " + CallbacklikeCustomerName.ClientInstanceName + @".SendCallback();
            }";


            btnBrowse.ClientSideEvents.Click = @"function(s,e){ 
                    var win = " + popupAccount.ClientInstanceName + @".GetWindow(0); 
                    " + popupAccount.ClientInstanceName + @".ShowWindow(win);                                    
                    }";


            GridCustomer.ClientSideEvents.CustomButtonClick = @"function(s,e){
                var buttonID = e.buttonID;               
                var visibleIndex = e.visibleIndex;
                  if(buttonID = 'buttonSelect')
                  {
                        
                        " + GridCustomer.ClientInstanceName + @".GetRowValues(visibleIndex,'EmployerID;EmployerNo',
                           function (values){                        
                               " + labelTitle.ClientInstanceName + @".SetText(document.emploterMap[values[0]]);
                               for(var i = 0;i<" + employers.Count + @";i++){
                                    var id = " + cboAccountNo.ClientInstanceName + @".GetItem(i).value;
                                    if(parseInt(id)==parseInt(values[0])){
                                        " + cboAccountNo.ClientInstanceName + @".SetSelectedIndex(i);
                                         break;       
                                    }
                                    
                                }
                              
                                " + popupAccount.ClientInstanceName + @".Hide();
                            }
                        );

                  }//end if
            }";




        }//end postback false
        #endregion

        if (Session[this.GetType().ToString() + "GridEmployer"] != null)
        {
            GridCustomer.DataSource = Session[this.GetType().ToString() + "GridEmployer"];
            GridCustomer.DataBind();
        }
    }
    //    private IList<Employer> loadEmployerToCombo()
    //    {
    //        ISession session = iSabayaContext.PersistencySession;
    //        IQuery query = session.CreateQuery(
    //                @"from Employer where EffectiveFrom <= getdate() and 
    //                (EffectiveTo >= getdate())");
    //        IList<Employer> employers = query.List<Employer>();
    //        //cboAccountNo.DataSource = employers;
    //        //cboAccountNo.ValueField = "EmployerID";
    //        //cboAccountNo.TextField = "EmployerNo";
    //        //cboAccountNo.DataBind();
    //        return employers;
    //    }
    //Dictionary<String, String> EmployerNoEmployeeNameDic;
    public int EmployerID
    {
        get
        {
            if (cboAccountNo.SelectedItem == null)
                return 0;
            int employerID = int.Parse(cboAccountNo.SelectedItem.Value.ToString());
            if (employerID == -1)
                return 0;
            return employerID;
        }
        set
        {
            foreach (ListEditItem item in cboAccountNo.Items)
            {
                if (item.Value.Equals(value))
                {
                    cboAccountNo.SelectedItem = item;
                    break;
                }
            }
        }
    }
    public Employer Employer
    {
        get
        {
            //ISession session = iSabayaContext;
            if (cboAccountNo.SelectedItem == null) { return null; }
            int accountId = int.Parse(cboAccountNo.SelectedItem.Value.ToString());
            if (accountId == -1) { return null; }
            Employer account = Employer.Find(iSabayaContext, accountId);
            return account;

        }
        set
        {
            foreach (ListEditItem item in cboAccountNo.Items)
            {
                if (item.Value.Equals(value.EmployerID.ToString()))
                {
                    cboAccountNo.SelectedItem = item;
                    break;
                }
            }

        }
    }
    /*Callback from name search*/
    protected void likeCustomerNameCallback_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        IList<Employer> accounts = this.FindLikeByName(iSabayaContext.PersistencySession, txtFirstName.Text);
        Session[this.GetType().ToString() + "GridEmployer"] = GetVOEmployerList(accounts);
        GridCustomer.DataSource = Session[this.GetType().ToString() + "GridEmployer"];
        GridCustomer.DataBind();
    }
    private IList<Employer> FindLikeByName(ISession session, string keyword)
    {
        String query = @"select * from Employer where EmployerID in(
                        SELECT   Employer.EmployerID 
                        FROM         
	                        Employer INNER JOIN
                            Organization ON Employer.EmployerOrgID = Organization.OrgID INNER JOIN
                            OrgName ON Organization.CurrentNameID = OrgName.OrgNameID INNER JOIN
                            MLSValue ON OrgName.NameMLSID = MLSValue.MLSID
                            where  MLSValue.Value like'" + keyword + @"%')
                       ";
        IList<Employer> results =
            session.CreateSQLQuery(query).AddEntity("employer",
            typeof(Employer)).List<Employer>();

        return results;
    }
    protected void cbpTxtEmployerNo_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {

    }
    protected void cbSendAcc_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        String code = e.Parameter;
        Employer customer = Employer.FindByEmployerNo(iSabayaContext, code);
        if (customer != null)
        {
            e.Result = customer.Name.ToString(iSabayaContext.CurrentLanguage.Code);
        }
    }
    private List<VOEmployer> GetVOEmployerList(IList<Employer> employers)
    {
        List<VOEmployer> list = new List<VOEmployer>();
        foreach ( Employer item in employers)
        {
            VOEmployer vo = new VOEmployer(iSabayaContext, item);
            list.Add(vo);
        }
        return list;
    }
}
