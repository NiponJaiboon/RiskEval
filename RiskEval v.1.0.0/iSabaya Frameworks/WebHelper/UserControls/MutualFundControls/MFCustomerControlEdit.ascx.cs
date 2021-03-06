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
using NHibernate;
using imSabaya;
using imSabaya.MutualFundSystem;
using iSabaya;
using DevExpress.Web.ASPxEditors;
using System.Collections.Generic;
using System.ComponentModel;
using WebHelper;

public partial class ctrls_CustomerControlNew : iSabayaControl
{
    public event EventHandler TextLostFocus;
    public String GridName;

    private bool isRequiredField = false;
    public bool IsRequiredField
    {
        get { return isRequiredField; }
        set { this.isRequiredField = value; }
    }

    private string validationGroup;
    public string ValidationGroup
    {
        get { return validationGroup; }
        set { this.validationGroup = value; }
    }
    public AdditionClientScript clientSideEvents = null;
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public AdditionClientScript ClientSideEvents
    {
        get
        {
            if (clientSideEvents == null)
                clientSideEvents = new AdditionClientScript();
            return clientSideEvents;
        }
        set { this.clientSideEvents = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboTLNCardType.ParentNode = TreeListNode.FindByCode(iSabayaContext,"cardtype");
            ComboTLNCardType.DataBind();

            CallbackSearchByCard.ClientInstanceName = this.ClientID + CallbackSearchByCard.ClientID;
            CallbacklikeCustomerName.ClientInstanceName = this.ClientID + CallbacklikeCustomerName.ClientID;
            cbSelect.ClientInstanceName = this.ClientID + cbSelect.ClientID;
            cbLostFocus.ClientInstanceName = this.ClientID + cbLostFocus.ClientID;
            GridCustomer.ClientInstanceName = this.ClientID + GridCustomer.ClientID;
            txtFirstName.ClientInstanceName = this.ClientID + txtFirstName.ClientID;
            btnFindName.ClientInstanceName = this.ClientID + btnFindName.ClientID;
            btnFindCard.ClientInstanceName = this.ClientID + btnFindCard.ClientID;
            btnFindName.ClientInstanceName = this.ClientID + btnFindName.ClientID;
            GridCustomer.ClientSideEvents.Init = @"function(s, e) {
                  s.SetFocusedRowIndex(-1);
            }";

            CallbackSearchByCard.ClientSideEvents.CallbackComplete = @"function(s, e) {
                " + GridCustomer.ClientInstanceName + @".SetFocusedRowIndex(-1);
                " + GridCustomer.ClientInstanceName + @".PerformCallback();
            }";

            CallbacklikeCustomerName.ClientSideEvents.CallbackComplete = @"function(s, e) {
                " + GridCustomer.ClientInstanceName + @".SetFocusedRowIndex(-1);
                " + GridCustomer.ClientInstanceName + @".PerformCallback();
            }";

            btnFindCard.ClientSideEvents.Click = @"function(s, e) {
            " + CallbackSearchByCard.ClientInstanceName + @".SendCallback();
            }";

            btnFindName.ClientSideEvents.Click = @"function(s, e) {
            " + CallbacklikeCustomerName.ClientInstanceName + @".SendCallback();
            }";

            //--------------------------------------------------------
            lblMFCustomerName.ClientInstanceName = this.ClientID + lblMFCustomerName.ClientID;
            txtMFCustomerNo.ClientInstanceName = this.ClientID + txtMFCustomerNo.ClientID;

            popupAccount.ClientInstanceName = this.ClientID + popupAccount.ClientID;
            cbpTxtMFAccountNo.ClientInstanceName = this.ClientID + cbpTxtMFAccountNo.ClientID;

            String lostFocus = @"function(s,e){
       
            " + cbLostFocus.ClientInstanceName + ".SendCallback(" + txtMFCustomerNo.ClientInstanceName + @".GetValue()); 
            }";
            txtMFCustomerNo.ClientSideEvents.LostFocus = lostFocus;

            String buttonClick = @"function(s,e){ 
                    var win = " + popupAccount.ClientInstanceName + @".GetWindow(0); 
                    " + popupAccount.ClientInstanceName + @".ShowWindow(win);
                    }";

            txtMFCustomerNo.ClientSideEvents.ButtonClick = buttonClick;
            //+ cbpTxtMFAccountNo.ClientInstanceName + @".PerformCallback();
            cbSelect.ClientSideEvents.CallbackComplete = @"function(s,e){
                " + txtMFCustomerNo.ClientInstanceName + @".SetValue(e.result);
                if(typeof(oncompleteLoadCustomer)!='undefined'){                   
                       oncompleteLoadCustomer();
                }
                var value = e.result;
                " + ClientSideEvents.AfterSelectedChanged + @"
            }";

            cbLostFocus.ClientSideEvents.CallbackComplete = @"function(s,e){ 
                if(e.result=='hit'){
                    " + cbpTxtMFAccountNo.ClientInstanceName + @".PerformCallback(); 
                    if(typeof(oncompleteLoadCustomer)!='undefined'){                   
                           oncompleteLoadCustomer();                       
                    }
                    var value = " + txtMFCustomerNo.ClientInstanceName + @".GetText();
                    " + ClientSideEvents.AfterSelectedChanged + @"
                }else{
                    " + txtMFCustomerNo.ClientInstanceName + @".SetValue('');
                    " + lblMFCustomerName.ClientInstanceName + @".SetValue('');
                    var value = '';
                    " + ClientSideEvents.AfterSelectedChanged + @"
                }
            }";

            GridCustomer.ClientSideEvents.CustomButtonClick = @"function(s,e){
                var buttonID = e.buttonID;               
                var visibleIndex = e.visibleIndex;
                  if(buttonID = 'buttonSelect')
                  {                         
                     " + cbSelect.ClientInstanceName + @".SendCallback(visibleIndex);         
                     " + popupAccount.ClientInstanceName + @".Hide();
                  }
            }";
        }
        if (IsRequiredField)
            txtMFCustomerNo.SetValidation(ValidationGroup);

        if (Session[this.ID + this.GetType().ToString() + "GridCustomerVOS"] != null)
        {
            GridCustomer.DataSource = (IList<MFCustomer>)Session[this.ID + this.GetType().ToString() + "GridCustomerVOS"];
            GridCustomer.DataBind();
        }
    }


    public MFCustomer MFCustomer
    {
        get
        {
            int customerId = 0;
            if (Session[this.ID + this.GetType().ToString() + "SelectedRow"] != null)
            {
                customerId = (int)Session[this.ID + this.GetType().ToString() + "SelectedRow"];
            }
            else
            {
                return null;
            }
            MFCustomer currentCustomer =
                MFCustomer.Find(iSabayaContext, customerId);
            return currentCustomer;
        }
        set
        {
            if (value != null)
            {
                txtMFCustomerNo.Text = value.CustomerNo;
                Session[this.ID + this.GetType().ToString() + "SelectedRow"] = value.CustomerID;
            }
            else
            {
                txtMFCustomerNo.Text = "";
                Session[this.ID + this.GetType().ToString() + "SelectedRow"] = null;
            }
        }
    }

    /*Callback from card search*/
    protected void ASPxCallback1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        TreeListNode identityCategory = ComboTLNCardType.SelectedNode;
        String identityNo = TextCardCode.Text;
        String type = (String)ComboIPartyType.SelectedItem.Value;

        IList<Party> partys = this.FindByIdentity(identityCategory, identityNo);
        if (partys != null)
        {
            IList<MFCustomer> customers = new List<MFCustomer>();
            foreach (Party party in partys)
            {
                MFCustomer cus = MFCustomer.FindByParty(iSabayaContext, party);
                if (cus != null)
                {
                    customers.Add(cus);
                }
                if (party != null)
                {
                    if (type.Equals("A"))
                    {
                        Session[this.ID + this.GetType().ToString() + "ctrls_CustomerControl_CurrentParty"] = party.PartyID;
                    }
                    else if (type.Equals("O"))
                    {
                        if (party is Organization || party is OrgUnit)
                        {
                            Session[this.ID + this.GetType().ToString() + "ctrls_CustomerControl_CurrentParty"] = party.PartyID;
                        }
                    }
                    else if (type.Equals("P"))
                    {
                        if (party is Person)
                        {
                            Session[this.ID + this.GetType().ToString() + "ctrls_CustomerControl_CurrentParty"] = party.PartyID;
                        }

                    }
                    Session[this.ID + this.GetType().ToString() + "GridCustomerVOS"] = customers;
                }
                GridCustomer.DataSource = customers;
                GridCustomer.DataBind();
            }

            if (TextLostFocus != null)
            {
                TextLostFocus(this, EventArgs.Empty);
            }
        }
    }


    /*Callback from name search*/
    protected void likeCustomerNameCallback_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        bool isFName = rdoFname.Checked;
        String likeCustomerName = "";
        if (isFName)
        {
            likeCustomerName = txtFirstName.Text;
        }
        else
        {
            likeCustomerName = txtLastName.Text;
        }

        String type = (String)ComboIPartyType.SelectedItem.Value;
        IList<MFCustomer> customers = this.FindLikeByName(isFName, likeCustomerName, type);
        GridCustomer.DataSource = customers;
        GridCustomer.DataBind();
        Session[this.ID + this.GetType().ToString() + "GridCustomerVOS"] = customers;
    }

    public IList<Party> FindByIdentity(TreeListNode identityCategory, string identityNo)
    {
        if (identityCategory == null)
        {
            return null;
        }
        IQuery query = iSabayaContext.PersistencySession.CreateQuery(
                        @"from Party p left join fetch p.Identities ids
                            where ids.IdentityNo like :IdentityNo
                            and ids.Category = :Category");

        query.SetString("IdentityNo", identityNo + "%");
        query.SetInt32("Category", identityCategory.NodeID);
        return query.List<Party>();
    }

    private IList<MFCustomer> FindLikeByName(
           bool isFName,
           string likeCustomerName,
           String type)
    {
        IQuery q = null;
        //named parameter list
        ArrayList partyIds = new ArrayList();
        if (type.Equals("A"))
        {
            IList<Person> persons = Person.FindLikeByName(iSabayaContext, isFName, likeCustomerName);
            IList<Organization> orgs = Organization.FindByNamePrefix(iSabayaContext, likeCustomerName);

            foreach (Person p in persons)
            {
                partyIds.Add(p.PersonID);
            }

            foreach (Organization o in orgs)
            {
                partyIds.Add(o.OrganizationID);
            }
            q = iSabayaContext.PersistencySession.CreateQuery(@"from MFCustomer cus 
                                      where PartyID in (:personList)
                                        and PartyDiscriminator in(10,20)");

        }
        else
            if (type.Equals("P"))
            {
                IList<Person> persons = Person.FindLikeByName(iSabayaContext, true, likeCustomerName);
                if (persons.Count < 1)
                {
                    return new List<MFCustomer>();
                }


                foreach (Person p in persons)
                {
                    partyIds.Add(p.PersonID);
                }
                q = iSabayaContext.PersistencySession.CreateQuery(@"from MFCustomer cus 
                                      where PartyID in (:personList)
                                        and PartyDiscriminator in(20)");
            }
            else if (type.Equals("O"))
            {
                IList<Organization> orgs = Organization.FindByNamePrefix(iSabayaContext, likeCustomerName);
                if (orgs.Count < 1)
                {
                    return new List<MFCustomer>();
                }


                foreach (Organization o in orgs)
                {
                    partyIds.Add(o.OrganizationID);
                }
                q = iSabayaContext.PersistencySession.CreateQuery(@"from MFCustomer cus 
                                      where PartyID in (:personList)
                                        and PartyDiscriminator in(10)");
            }
        if (partyIds.Count == 0)
        {
            IList<MFCustomer> list = new List<MFCustomer>();
            return list;
        }

        q.SetParameterList("personList", partyIds);
        IList<MFCustomer> customers = q.List<MFCustomer>();
        return customers;
    }


    protected void cbSelect_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        int rowIndex = int.Parse(e.Parameter);
        MFCustomer customer = (MFCustomer)GridCustomer.GetRow(rowIndex);

        if (customer != null)
        {
            Session[this.ID + this.GetType().ToString() + "SelectedRow"] = customer.CustomerID;
            txtMFCustomerNo.Text = customer.CustomerNo;
            e.Result = customer.CustomerNo;
        }
    }
    protected void cbpTxtMFAccountNo_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {

        if (MFCustomer != null)
        {
            lblMFCustomerName.Text = this.MFCustomer.FullName;
        }
        else { lblMFCustomerName.Text = ""; }
    }

    protected void cbSendAcc_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        String code = e.Parameter;
        MFCustomer customer = MFCustomer.FindByCustomerNo(iSabayaContext, code);
        if (customer != null)
        {
            e.Result = customer.FullName;
        }
    }

    protected void cbLostFocus_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        MFCustomer customer = MFCustomer.FindByCustomerNo(iSabayaContext, e.Parameter);
        if (customer != null)
        {
            Session[this.ID + this.GetType().ToString() + "SelectedRow"] = customer.CustomerID;
            e.Result = "hit";
        }
        else
        {
            Session[this.ID + this.GetType().ToString() + "SelectedRow"] = null;
            e.Result = "nohit";
        }
    }
    public class AdditionClientScript
    {
        public string AfterSelectedChanged { get; set; }
    }
}
