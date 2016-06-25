using System;
using System.Collections.Generic;
using DevExpress.Web.ASPxEditors;
using iSabaya;
using WebHelper;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
namespace WebHelper.UserControls
{
    public partial class PersonControl : iSabayaControl
    {
        private string validationGroup;

        public string ValidationGroup
        {
            get { return this.validationGroup; }
            set { this.validationGroup = value; }
        }

        private bool enabled = true;


        public bool Enabled
        {
            get { return this.enabled; }
            set { this.enabled = value; }
        }

        public Person Person
        {
            get
            {
                string a = (cbxPerson.SelectedItem != null ? cbxPerson.SelectedItem.Value.ToString() : null);
                if (a == null)
                    return null;
                int personID = int.Parse(a);
                Person p = iSabaya.Person.Find(iSabayaContext, personID);
                return iSabaya.Person.Find(iSabayaContext, Convert.ToInt32(cbxPerson.Value));
            }
            set
            {
                this.EnabledControls(this.enabled);
                if (value != null)
                {
                    ListEditItem item = cbxPerson.Items.FindByValue(value.PersonID);
                    if (item != null)
                        cbxPerson.SelectedItem = item;
                }
                else
                    cbxPerson.SelectedIndex = -1;
            }
        }

        public override string Text
        {
            get
            {
                return cbxPerson.Text;
            }
        }

        public override object Value
        {
            get { return Person; }
            set { Person = (Person)value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.EnabledControls(this.enabled);
                initializeControls();
            }
        }

        private void initializeControls()
        {
            cbxPerson.SetValidation(ValidationGroup);
        }

        private void EnabledControls(bool enabled)
        {
            cbxPerson.ClientEnabled = enabled;
        }

        private IList<Person> GetPersons()
        {
            return iSabayaContext.PersistenceSession
                                    .CreateCriteria<Person>()
                                    .List<Person>();
        }

        /////////////////// Edit by Nice Create Client Event ///////////////////
        public string ClientInstanceName
        {
            get { return cbxPerson.ClientInstanceName; }
            set { cbxPerson.ClientInstanceName = value; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ComboBoxClientSideEvents ClientSideEvents
        {
            get { return cbxPerson.ClientSideEvents; }
        }

        public ListEditItem SelectedItem
        {
            get { return cbxPerson.SelectedItem; }
            set { cbxPerson.SelectedItem = value; }
        }

        public int SelectedIndex
        {
            get { return cbxPerson.SelectedIndex; }
            set { cbxPerson.SelectedIndex = value; }
        }

        public Unit Width
        {
            get { return cbxPerson.Width; }
            set { cbxPerson.Width = value; }
        }
        ////////////////////////////////////////////////////////////////////////
    }
}