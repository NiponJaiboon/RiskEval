using System;
using System.Collections;
using System.Collections.Generic;
using imSabaya.MutualFundSystem;
using iSabaya;
using NHibernate;
using WebHelper;

public partial class MFAccountGridControl : iSabayaControl
{
    public event EventHandler TextLostFocus;
    public String GridName;
    private int partyId = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initControl("th", PersistenceLayer.WebSessionManager.PersistenceSession);
            ComboTLNCardType.ParentNode = TreeListNode.FindByCode(iSabayaContext, "cardtype");
            ComboTLNCardType.DataBind();

            ASPxPopupControl1.ClientInstanceName = ASPxPopupControl1.ClientID;

            LabelCustomerName.ClientInstanceName = LabelCustomerName.ClientID;
            cbpGridAccountOutput.ClientInstanceName = cbpGridAccountOutput.ClientID;

            CallbackSearchByCard.ClientInstanceName = CallbackSearchByCard.ClientID;

            CallbacklikeCustomerName.ClientInstanceName = CallbacklikeCustomerName.ClientID;
            Callback1FromPopup.ClientInstanceName = Callback1FromPopup.ClientID;
            GridCustomer.ClientInstanceName = GridCustomer.ClientID;
            TextSearchString.ClientInstanceName = TextSearchString.ClientID;
            ButtonSearch.ClientInstanceName = ButtonSearch.ClientID;
            buttonApplyAndClose.ClientInstanceName = buttonApplyAndClose.ClientID;
            buttonClose.ClientInstanceName = buttonClose.ClientID;
            /*LostFocus*/
            TextCardCode.ClientSideEvents.LostFocus = @"function(s, e) {";

            TextCardCode.ClientSideEvents.LostFocus += CallbackSearchByCard.ClientInstanceName + ".SendCallback(s.GetValue());      }";

            /*Search by card complete*/
            CallbackSearchByCard.ClientSideEvents.CallbackComplete =
                @"function(s, e) {
                    document.getElementById('" + LabelCustomerName.ClientInstanceName + @"').innerHTML=e.result;
            " + cbpGridAccountOutput.ClientInstanceName + @".PerformCallback();
            setTimeout('" + GridName + ".PerformCallback()', 5000); ";
            CallbackSearchByCard.ClientSideEvents.CallbackComplete +=
            @"
                    var params = new Array(1);
                    params['source']='completeSearch';
                    //alert(eventCallback);
                    //if(eventCallback!= undefined){
                    //   eventCallback(params);
                    //}
            }";

            /*Click search from name*/
            String script = "function(s, e) { var likeCustomerName = " + TextSearchString.ClientInstanceName + ".GetValue();";
            script += " " + CallbacklikeCustomerName.ClientInstanceName + ".SendCallback(likeCustomerName);";
            script += "var win = " + ASPxPopupControl1.ClientInstanceName + ".GetWindow(0);";
            script += ASPxPopupControl1.ClientInstanceName + ".ShowWindow(win);";
            script += "}";
            ButtonSearch.ClientSideEvents.Click = script;

            /*show grid name like criteria*/
            CallbacklikeCustomerName.ClientSideEvents.CallbackComplete =
              @"function(s, e) {
            " + cbpGridAccountOutput.ClientInstanceName + @".PerformCallback();
             " + GridCustomer.ClientInstanceName + ".PerformCallback();  " +
               GridCustomer.ClientInstanceName + ".SetFocusedRowIndex(0); }";

            /*grid FocusedRowChanged*/
            GridCustomer.ClientSideEvents.FocusedRowChanged = @"function(s, e) {
                     if(" + GridCustomer.ClientInstanceName + ".GetFocusedRowIndex()!=-1){";
            GridCustomer.ClientSideEvents.FocusedRowChanged += CallbackTest.ClientInstanceName + ".SendCallback(" + GridCustomer.ClientInstanceName + ".GetFocusedRowIndex());} }";

            /*click select from popup*/
            String scriptString1 = "function(s, e) { ";
            scriptString1 += Callback1FromPopup.ClientInstanceName + ".SendCallback();";
            scriptString1 += cbpGridAccountOutput.ClientInstanceName + ".PerformCallback();";
            scriptString1 += ASPxPopupControl1.ClientInstanceName + ".Hide();";
            scriptString1 += " }";
            buttonApplyAndClose.ClientSideEvents.Click = scriptString1;

            String scriptStringClose = "function(s, e) { ";
            scriptString1 += ASPxPopupControl1.ClientInstanceName + ".Hide();";
            scriptString1 += " }";
            buttonClose.ClientSideEvents.Click = scriptStringClose;

            /*Search by name complete after selected from grid*/
            Callback1FromPopup.ClientSideEvents.CallbackComplete =
             @"function(s, e) {
                    document.getElementById('" + LabelCustomerName.ClientInstanceName + "').innerHTML=e.result; setTimeout('" + GridName + ".PerformCallback()', 5000);  ";
            Callback1FromPopup.ClientSideEvents.CallbackComplete +=
            @"
                    var params = new Array(1);
                    params['source']='completeSearch';
                    if(typeof(eventCallback)!='undefined'){
                       eventCallback(params);
                    }
            }";

            gridAccountOutput.ClientInstanceName = gridAccountOutput.ClientID;
            cbSelectedIndexChnage.ClientInstanceName = cbSelectedIndexChnage.ClientID;
            gridAccountOutput.ClientSideEvents.FocusedRowChanged = @"function(s, e) {
                    /*alert('hi');*/
                    var rowIndex = " + gridAccountOutput.ClientInstanceName + @".GetFocusedRowIndex();
                " + cbSelectedIndexChnage.ClientInstanceName + @".SendCallback(rowIndex);
            }";
        }

        if (Session[this.ID + "cbpGridAccountOutput"] != null)
        {
            IList<MFAccount> accounts = (IList<MFAccount>)Session[this.ID + "cbpGridAccountOutput"];

            gridAccountOutput.DataSource = accounts;

            gridAccountOutput.DataBind();
        }
        if (Session["GridCustomerVOS"] != null)
        {
            GridCustomer.DataSource = (IList<MFCustomer>)Session["GridCustomerVOS"];
            GridCustomer.DataBind();
        }
        if (Session[this.ID + "ctrls_CustomerControl_CurrentParty"] != null)
        {
            partyId = (int)Session[this.ID + "ctrls_CustomerControl_CurrentParty"];
        }
    }

    public MFAccountGridControl()
    {
    }

    public void InitializeControls(String currentLanguage, ISession session)
    {
    }

    private void initControl(String currentLanguage, ISession session)
    {
        //ComboTLNCardType.InitializeControls(session, currentLanguage);

        //LabelCardType.Text = MultilingualString.ValueInCurrentLanguage(((int)EnumMLS.CardType).ToString(), currentLanguage, session);
        //LabelCardCode.Text = MultilingualString.ValueInCurrentLanguage(((int)EnumMLS.CardCode).ToString(), currentLanguage, session);
    }

    public MFCustomer MFCustomer
    {
        get
        {
            if (Session[this.ID + "ctrls_CustomerControl_CurrentParty"] != null)
            {
                partyId = (int)Session[this.ID + "ctrls_CustomerControl_CurrentParty"];
            }
            else
            {
                return null;
            }
            /*if (partyId == -1)
                {
                return null;
            }
            */
            Party party = (Party)iSabayaContext.PersistencySession.Get<Party>(partyId);
            MFCustomer currentCustomer =
                MFCustomer.FindByParty(iSabayaContext, party);
            return currentCustomer;
        }
        set { }
    }

    /*Callback from card search*/

    protected void ASPxCallback1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        ISession session = PersistenceLayer.WebSessionManager.PersistenceSession;
        TreeListNode identityCategory = ComboTLNCardType.SelectedNode;
        String identityNo = TextCardCode.Text;

        String type = (String)ComboIPartyType.SelectedItem.Value;

        if (type.Equals("O"))
        {
        }
        else if (type.Equals("P"))
        {
            Person person = Person.FindByPartyIdentity(iSabayaContext, identityCategory, identityNo);

            if (person != null)
            {
                Session[this.ID + "ctrls_CustomerControl_CurrentParty"] = person.PersonID;

                e.Result = person.FullName;
            }
            else
            {
                Session[this.ID + "ctrls_CustomerControl_CurrentParty"] = null;
                e.Result = "";
            }
        }

        if (TextLostFocus != null)
        {
            TextLostFocus(this, EventArgs.Empty);
        }
    }

    /*Callback from name search*/

    protected void likeCustomerNameCallback_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        String likeCustomerName = e.Parameter;

        String type = (String)ComboIPartyType.SelectedItem.Value;

        IList<MFCustomer> customers = MFCustomer.FindLikeByName(iSabayaContext, likeCustomerName, type);
        GridCustomer.DataSource = customers;
        GridCustomer.DataBind();
        Session["GridCustomerVOS"] = customers;
        // Session["CurrentPersonID"]=null;
    }

    protected void CallbackTest_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        String index = e.Parameter;
        MFCustomer cus = (MFCustomer)GridCustomer.GetRow(int.Parse(index));
        Session["CurrentParty"] = cus.Party.PartyID;
    }

    protected void Callback1FromPopup_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        ISession session = PersistenceLayer.WebSessionManager.PersistenceSession;
        int partyID = (Int32)Session["CurrentParty"];

        Party party = (Party)session.Get(typeof(Party), partyID);

        Session[this.ID + "ctrls_CustomerControl_CurrentParty"] = party.PartyID;
        e.Result = party.FullName;
        if (TextLostFocus != null)
        {
            TextLostFocus(this, EventArgs.Empty);
        }
    }

    protected void cbpGridAccountOutput_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        MFCustomer customer = this.MFCustomer;
        String currentLanguage = (String)Session["CurrentLanguage"];
        IList<MFAccount> accounts = MFAccount.FindByMFCustomer(iSabayaContext, customer);
        gridAccountOutput.DataSource = accounts;
        gridAccountOutput.DataBind();
        Session[this.ID + "cbpGridAccountOutput"] = accounts;
    }

    protected void cbSelectedIndexChnage_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {//var rowIndex = gridPassword.GetFocusedRowIndex();
        if (e.Parameter.Length != 0)
        {
            int index = int.Parse(e.Parameter);
            MFAccount account = (MFAccount)gridAccountOutput.GetRow(index);

            Session[this.ID + "SelectedAccount"] = account;
        }
    }

    public MFAccount MFAccount
    {
        get { return (MFAccount)Session[this.ID + "SelectedAccount"]; }
    }
}