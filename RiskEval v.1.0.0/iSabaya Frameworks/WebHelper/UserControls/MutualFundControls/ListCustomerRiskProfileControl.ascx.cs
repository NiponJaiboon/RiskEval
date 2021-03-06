using System;
using System.Collections.Generic;
using imSabaya;
using imSabaya.MutualFundSystem;
using iSabaya;
using NHibernate.Criterion;
using WebHelper;

public partial class ListCustomerRiskProfileControl : iSabayaControl
{
    public IList<Person> PersonList { get; set; }

    public MultiOwnerPortfolio MFAccount
    {
        get
        {
            if (String.IsNullOrEmpty(hddMfAccountID.Value))
                return null;
            int id = int.Parse(hddMfAccountID.Value);
            if (id == 0)
                return null;
            return iSabayaContext.PersistencySession.Get<MFAccount>(id);
        }
        set
        {
            PersonList = new List<Person>();
            if (value != null)
            {
                hddMfAccountID.Value = value.InvestmentPortfolioID.ToString();
                Party owner;
                int count = value.Owners.Count;
                if (count <= 0)
                    return;
                if (value.OwnerConnective != Connective.And)
                    count = 1;
                for (int i = 0; i < count; i++)
                {
                    owner = value.Owners[i].Owner;
                    if (owner is Person)
                    {
                        PersonList.Add((Person)owner);
                    }
                    else if (owner is Organization)
                    {
                        Organization org = (Organization)owner;
                        foreach (PersonOrgRelation por in org.ListAuthorizedDirectors(iSabayaContext))
                            PersonList.Add(por.Person);
                    }
                }
                Session[this.ClientID + "listIPerson"] = PersonList;
            }
            else
                hddMfAccountID.Value = "0";
        }
    }

    public Party Party
    {
        set
        {
            PersonList = new List<Person>();
            if (null != value)
            {
                PersonList.Add((Person)value);
                Session[this.ClientID + "listIPerson"] = PersonList;
            }
        }
    }
    protected override void OnInit(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session[this.ClientID + "listIPerson"] = null;
            Session[this.ClientID + "CustomerProfileList"] = null;
            Session[this.ClientID + "RiskProfileList"] = null;
            InitializeControls();
            BindCombo();

            comboCustomerProfile.SelectedIndex = comboRiskProfile.SelectedIndex = 0;
            comboRiskProfile.SelectedIndex = comboRiskProfile.SelectedIndex = 0;
        }
        if (Page.IsCallback)
        {
            BindCombo();
            //gridCustomerProfile.DataBind();
            //gridRiskProfile.DataBind();
        }

        gridRiskProfile.CustomButtonInitialize += HandlerMethod.gdvGeneral_CustomButtonInitialize;
        gridCustomerProfile.CustomButtonInitialize += HandlerMethod.gdvGeneral_CustomButtonInitialize;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void InitializeControls()
    {
        //string CssPostFix = "";
        btnClosePopQuestion.SetCancelButton();
        lblSelectCustomerProfile.Text = lblSelectRiskProfile.Text = "กรุณาเลือกแบบสอบถาม:";
        btnClosePopQuestion.SetCloseButton();

        comboCustomerProfile.ClientInstanceName = this.ClientID + comboCustomerProfile.ClientID;
        comboRiskProfile.ClientInstanceName = this.ClientID + comboRiskProfile.ClientID;
        gridCustomerProfile.ClientInstanceName = this.ClientID + gridCustomerProfile.ClientID;
        gridRiskProfile.ClientInstanceName = this.ClientID + gridRiskProfile.ClientID;
        cpbViewCustomerRiskProfile.ClientInstanceName = this.ClientID + cpbViewCustomerRiskProfile.ClientID;
        popQuestionniare.ClientInstanceName = this.ClientID + popQuestionniare.ClientID;
        btnClosePopQuestion.ClientInstanceName = this.ClientID + btnClosePopQuestion.ClientID;
        popRiskProfileDescription.ClientInstanceName = this.ClientID + popRiskProfileDescription.ClientID;

        comboCustomerProfile.ClientSideEvents.SelectedIndexChanged = @"function(s, e)
        {
            " + gridCustomerProfile.ClientInstanceName + @".PerformCallback();
        }";

        comboRiskProfile.ClientSideEvents.SelectedIndexChanged = @"function(s, e){
            " + gridRiskProfile.ClientInstanceName + @".PerformCallback();
        }";

        gridCustomerProfile.ClientSideEvents.CustomButtonClick = @"function(s, e){
            var buttonID = e.buttonID;
            var key = " + gridCustomerProfile.ClientInstanceName + @".GetRowKey(e.visibleIndex);
            if(buttonID == 'btnViewDetailCustomerProfile')
            {
                loading.Show();
                " + cpbViewCustomerRiskProfile.ClientInstanceName + @".PerformCallback(key);
            }
        }";

        gridRiskProfile.ClientSideEvents.CustomButtonClick = @"function(s, e){
            var buttonID = e.buttonID;
            var key = " + gridRiskProfile.ClientInstanceName + @".GetRowKey(e.visibleIndex);
            if(buttonID == 'btnViewDetailRiskProfile')
            {
                loading.Show();
                " + cpbViewCustomerRiskProfile.ClientInstanceName + @".PerformCallback(key);
            }
//            else if(buttonID == 'btnHelpRiskProfile')
//            {
//                " + popRiskProfileDescription.ClientInstanceName + @".Show();
//                " + cpbViewCustomerRiskProfile.ClientInstanceName + @".PerformCallback(key);
//            }
        }";

        btnClosePopQuestion.ClientSideEvents.Click = @"function(s, e){
            " + popQuestionniare.ClientInstanceName + @".Hide();
        }";

        //        popQuestionniare.ClientSideEvents.Closing = @"function(s, e){
        //            ASPxClientEdit.ClearEditorsInContainerById('" + popQContent1.ID + @"');
        //        }";
        cpbViewCustomerRiskProfile.ClientSideEvents.EndCallback = @"function(s, e){
            loading.Hide();
            " + popQuestionniare.ClientInstanceName + @".Show();
        }";
    }

    private IList<Questionnaire> QuestionniareList(String code)
    {
        return iSabayaContext.PersistencySession.CreateCriteria<Questionnaire>()
                            .Add(Expression.Like("Code", code, MatchMode.Start))
                            .List<Questionnaire>();
    }

    private void BindCombo()
    {
        comboCustomerProfile.DataSource = QuestionniareList("C");
        comboCustomerProfile.DataBind();

        comboRiskProfile.DataSource = QuestionniareList("R");
        comboRiskProfile.DataBind();
    }

    private String FindQuestionniareCode(int ID)
    {
        Questionnaire questionniare = Questionnaire.Find(iSabayaContext, ID);
        if (null == questionniare)
            throw new Exception("Can not find the questionniare." + ID);

        return questionniare.Code;
    }


    public void ShowCustomerRiskProFile()
    {
        if (PersonList.Count == 0)
            return;
        Person respondent = PersonList[0];
        if (null != respondent)
        {
            if (comboCustomerProfile.SelectedItem != null)
            {
                Session[this.ClientID + "CustomerProfileList"] = iSabaya.Response.List(iSabayaContext,
                                                                FindQuestionniareCode((Convert.ToInt32(comboCustomerProfile.SelectedItem.Value))),
                                                                respondent);
            }
            gridCustomerProfile.DataBind();

            if (comboRiskProfile.SelectedItem != null)
            {
                IList<iSabaya.Response> responseLists = iSabaya.Response.List(iSabayaContext,
                                                                FindQuestionniareCode((Convert.ToInt32(comboRiskProfile.SelectedItem.Value))),
                                                                respondent);
                Session[this.ClientID + "RiskProfileList"] = responseLists;

                foreach (Response item in responseLists)
                {
                    if (item.RespondedDate.CompareTo(DateTime.Today) <= 0)
                    {
                        ctrlRiskProfileDescription.CustomerRiskScore = item.Score;
                        ctrlRiskProfileDescription.ShowCustomerRiskDescription();
                        break;
                    }
                }
            }
            gridRiskProfile.DataBind();
        }
    }

    protected void cpbViewCustomerRiskProfile_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (String.IsNullOrEmpty(e.Parameter))
            throw new Exception("Response ID is null." + e.Parameter);
        int responseID = Convert.ToInt32(e.Parameter);
        ctrlCustomerRiskProfileviewer.ResponseForm = iSabaya.Response.Find(iSabayaContext, responseID);
        ctrlCustomerRiskProfileviewer.ShowCustomerRiskProfile();
    }

    protected void gridCustomerProfile_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        List<Person> p = (Session[this.ClientID + "listIPerson"] as List<Person>);
        if (p != null && p.Count > 0)
        {
            Person respondent = p[0];
            if (null != respondent)
            {
                Session[this.ClientID + "CustomerProfileList"] = iSabaya.Response.List(iSabayaContext, FindQuestionniareCode((Convert.ToInt32(comboCustomerProfile.SelectedItem.Value))), respondent);
                gridCustomerProfile.DataBind();
            }
        }
    }

    protected void gridCustomerProfile_DataBinding(object sender, EventArgs e)
    {
        gridCustomerProfile.DataSource = Session[this.ClientID + "CustomerProfileList"];
    }

    protected void gridRiskProfile_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        List<Person> p = (Session[this.ClientID + "listIPerson"] as List<Person>);
        if (p != null && p.Count > 0)
        {
            Person respondent = p[0];

            IList<iSabaya.Response> responseLists = iSabaya.Response.List(iSabayaContext,
                                                                FindQuestionniareCode((Convert.ToInt32(comboRiskProfile.SelectedItem.Value))),
                                                                respondent);
            if (null != respondent)
            {
                Session[this.ClientID + "RiskProfileList"] = responseLists;
                gridRiskProfile.DataBind();
            }
        }
    }

    protected void gridRiskProfile_DataBinding(object sender, EventArgs e)
    {
        gridRiskProfile.DataSource = Session[this.ClientID + "RiskProfileList"];
    }

    public void ShowGridCustomerRiskProfile()
    { }
}