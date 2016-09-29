using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxPopupControl;

namespace WebHelper
{
    public class PopupTransitionTemplate : ITemplate
    {
        protected PopupControlTemplateContainer parent;
        private object stateID;
        protected ASPxComboBox cboTransition;
        protected ASPxButton btnTransit;
        protected SqlDataSource sqlDataSource;
        protected ASPxTextBox txtDescription;
        private string callbackClientInstanceName;
        private string lblDescription;
        private string lblTransition;
        private string btnText;
        private string btnImgUrl;
        private bool cancelOnly;

        public PopupTransitionTemplate(string callbackName, int stateID, bool cancelOnly, string descriptionStr, string transitionStr, string btnText, string btnImgUrl)
        {
            this.callbackClientInstanceName = callbackName;
            this.stateID = stateID;
            this.cancelOnly = cancelOnly;
            this.lblDescription = descriptionStr;
            this.lblTransition = transitionStr;
            this.btnText = btnText;
            this.btnImgUrl = btnImgUrl;
        }

        public void InstantiateIn(Control container)
        {
            parent = (PopupControlTemplateContainer)container;
            CreateSQLDataSource();
            CreateTextBox();
            CreateComboBox();
            CreateButtonTransit();
            Table table = new Table();

            TableRow tr = new TableRow();
            TableCell td = new TableCell();
            td.Controls.Add(new Literal() { Text = this.lblDescription });
            tr.Cells.Add(td);
            td = new TableCell();
            td.Controls.Add(txtDescription);
            tr.Cells.Add(td);
            table.Rows.Add(tr);

            tr = new TableRow();
            td = new TableCell();
            td.Controls.Add(new Literal() { Text = this.lblTransition });
            tr.Cells.Add(td);
            td = new TableCell();
            td.Controls.Add(cboTransition);
            tr.Cells.Add(td);
            table.Rows.Add(tr);

            tr = new TableRow();
            td = new TableCell();
            td.Controls.Add(new Literal() { Text = "&nbsp;" });
            tr.Cells.Add(td);
            td = new TableCell();
            td.Controls.Add(btnTransit);
            tr.Cells.Add(td);
            table.Rows.Add(tr);

            parent.Controls.Add(table);
            cboTransition.DataSourceID = sqlDataSource.ID;
            cboTransition.DataBind();
            cboTransition.SelectedIndex = 0;
        }

        private void CreateTextBox()
        {
            txtDescription = new ASPxTextBox()
            {
                ID = "txtDescription",
                Width = Unit.Pixel(170),
            };
            txtDescription.SetValidation_Border("validate");
            txtDescription.ClientInstanceName = string.Format("{0}_{1}", parent.ClientID, txtDescription.ID);
        }

        private void CreateComboBox()
        {
            cboTransition = new ASPxComboBox()
            {
                ID = "cboTransition",
                TextField = "sText",
                ValueField = "sValue",
                DataSourceID = sqlDataSource.ID,
                AnimationType = DevExpress.Web.ASPxClasses.AnimationType.None,
                Width = Unit.Pixel(170),
            };
            cboTransition.ClientInstanceName = string.Format("{0}_{1}", parent.ClientID, cboTransition.ID);
        }

        private void CreateButtonTransit()
        {
            btnTransit = new ASPxButton()
            {
                ID = "btnTransit",
                Text = this.btnText,
                AutoPostBack = false,
                ImageUrl = btnImgUrl,
                Width = Unit.Pixel(170)
            };
            btnTransit.ClientSideEvents.Click = @"
                function(s,e)
                {
                    var stateID = " + cboTransition.ClientInstanceName + @".GetValue();
                    if(ASPxClientEdit.ValidateGroup('validate')){
                        " + callbackClientInstanceName + @".SendCallback(stateID + ';' +" + txtDescription.ClientInstanceName + @".GetText());
                    }
                }
                ";
        }

        private void CreateSQLDataSource()
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append(@"select dbo.f_mls(TitleMLSID, @languageCode) as sText,
                    st.StateTransitionID as sValue
                    from StateTransition st
                    inner join [State] s on st.ToState = s.StateID
                    where st.FromState = @currStateID");
            if (this.cancelOnly)
                sqlCommand.Append(" and dbo.f_mls(TitleMLSID, 'th-TH') like('%ปฏิเสธ%')");

            sqlDataSource = new SqlDataSource()
            {
                ID = "sdsTransition",
                ConnectionString = System.Web.Configuration.WebConfigurationManager
                                                    .ConnectionStrings["imSabayaConnectionString"].ToString(),
                SelectCommandType = SqlDataSourceCommandType.Text,
                SelectCommand = sqlCommand.ToString(),
            };
            sqlDataSource.SelectParameters.Add("languageCode", (String)HttpContext.Current.Session["LanguageCode"]);
            sqlDataSource.SelectParameters.Add("currStateID", stateID.ToString());
            parent.Controls.Add(sqlDataSource);
        }
    }
}