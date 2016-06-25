using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iSabaya;
using imSabaya;
using imSabaya.ProvidentFundSystem;
using NHibernate.Criterion;
using DevExpress.Web.ASPxEditors;
using WebHelper;

public partial class Controls_NewPersonControl : iSabayaControl
{
    private string validationGroup;
    public string ValidationGroup
    {
        get { return this.validationGroup; }
        set { this.validationGroup = value; }
    }

    public Person Person
    {
        get
        {
            //Response.Write(@"<script language='javascript'> alert(" + cbxPerson.Value + ");</script>");
            string a = (cbxPerson.SelectedItem != null ? cbxPerson.SelectedItem.Value.ToString() : null);
            if (a == null)
                return null;
            int personID = int.Parse(a);
            Person p = Person.Find(iSabayaContext, personID);
            return Person.Find(iSabayaContext, Convert.ToInt32(cbxPerson.Value));
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            cbxPerson.DataSource = sdcPerson;
            cbxPerson.ValueField = "PersonID";
            cbxPerson.DataBind();
            initializeControls();
        }
        else
        {
            if (Page.IsCallback)
            {
                cbxPerson.DataSource = sdcPerson;
                cbxPerson.ValueField = "PersonID";
                cbxPerson.DataBind();
            }
        }
    }

    private void initializeControls()
    {
        cbxPerson.SetValidation(ValidationGroup);
        ListBoxColumn lbc;
        //cbxPerson.DropDownStyle = DropDownStyle.DropDownList;
        //cbxPerson.DropDownHeight = Unit.Pixel(200);
        cbxPerson.EnableAnimation = false;
        cbxPerson.EnableSynchronization =   DevExpress.Web.ASPxClasses.DefaultBoolean.False;
        cbxPerson.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains;
        cbxPerson.EnableCallbackMode = true;
        cbxPerson.CallbackPageSize = 20;
        lbc = new ListBoxColumn("PersonID");
        lbc.Caption = "รหัส";
        cbxPerson.Columns.Add(lbc);
        lbc = new ListBoxColumn("FirstName");
        lbc.Caption = "ชื่อ";
        cbxPerson.Columns.Add(lbc);
        lbc = new ListBoxColumn("LastName");
        lbc.Caption = "นามสกุล";
        cbxPerson.Columns.Add(lbc); 
        //lbc = new ListBoxColumn("CurrentName");
        //lbc.Caption = "ชื่อ-นามสกุล";
        //cbxPerson.Columns.Add(lbc);

        //lbc = new ListBoxColumn("OfficialIDNo");
        //lbc.Caption = "เลขบัตรประจำตัว";
        //cbxPerson.Columns.Add(lbc);

        cbxPerson.TextFormatString = "{0} {1} {2}";
        //cbxPerson.DropDownWidth = Unit.Pixel(400);

        cbxPerson.DataSource = sdcPerson;
        //cbxPerson.DataSource = GetPersons();
        cbxPerson.ValueField = "PersonID";
        cbxPerson.DataBind();

    }

    private IList<Person> GetPersons()
    {
        return iSabayaContext.PersistencySession
                                .CreateCriteria<Person>()
                                .List<Person>();
    }
}