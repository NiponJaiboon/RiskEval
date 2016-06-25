using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Web.ASPxGridView;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web;
using System.Web.UI;

namespace WebHelper
{
    public abstract class ChildTransactionsGridView : ITemplate
    {
        protected GridViewDetailRowTemplateContainer parent;
        protected object masterKey;
        protected ASPxGridView detailGrid;
        protected SqlDataSource sqlDataSource;
        protected String transactionTypeCode;
     
        #region ITemplate Members
        public void InstantiateIn(Control container)
        {
            transactionTypeCode = "";
            parent = (GridViewDetailRowTemplateContainer)container;
            masterKey = parent.KeyValue;
            object ttCode = (object)parent.Grid.GetRowValuesByKeyValue(masterKey, "TransactionTypeCode");
            if (ttCode != null)
                transactionTypeCode = ttCode.ToString();
            CreateSQLDataSource();
            CreateDetailGridView();
            detailGrid.ClientSideEvents.CustomButtonClick = parent.Grid.ClientSideEvents.CustomButtonClick;
        }
        #endregion



        private void CreateDetailGridView()
        {
            detailGrid = new ASPxGridView()
                {
                    ID = "gdvChildren" + masterKey.ToString(),
                    KeyFieldName = "TransactionID",
                    AutoGenerateColumns = false,
                    DataSourceID = sqlDataSource.ID,
                    Width = Unit.Percentage(100),
                };
            detailGrid.SettingsDetail.ShowDetailRow = true;
            detailGrid.DetailRowGetButtonVisibility += new ASPxGridViewDetailRowButtonEventHandler(detailGrid_DetailRowGetButtonVisibility);
            //detailGrid.BeforePerformDataSelect += new EventHandler(detailGrid_BeforePerformDataSelect);
            //detailGrid.DetailRowExpandedChanged += new ASPxGridViewDetailRowEventHandler(detailGrid_DetailRowExpandedChanged);
            detailGrid.Load += new EventHandler(detailGrid_Load);
            detailGrid.SettingsPager.PageSize = 15;
            this.SetDetailColumn();
            parent.Controls.Add(detailGrid);
            detailGrid.DataBind();
        }

        private void CreateSQLDataSource()
        {
            sqlDataSource = new SqlDataSource()
            {
                ID = "sdsChildren",
                ConnectionString = System.Web.Configuration.WebConfigurationManager
                                                    .ConnectionStrings["imSabayaConnectionString"].ToString(),
                SelectCommandType = SqlDataSourceCommandType.StoredProcedure,
                SelectCommand = "usp_GetChildTransactions"
            };
            sqlDataSource.SelectParameters.Add("languageCode", (String)HttpContext.Current.Session["LanguageCode"]);
            sqlDataSource.SelectParameters.Add("parentID", masterKey.ToString());
            parent.Controls.Add(sqlDataSource);
        }
        protected abstract void SetDetailColumn();
        protected void detailGrid_DetailRowGetButtonVisibility(object source, ASPxGridViewDetailRowButtonEventArgs e)
        {
            ASPxGridView gdv = source as ASPxGridView;
            int childrenCount = (int)gdv.GetRowValues(e.VisibleIndex, "ChildrenCount");
            e.ButtonState = childrenCount > 0 ? GridViewDetailRowButtonState.Visible : 
                                                GridViewDetailRowButtonState.Hidden;
        }
        protected abstract void LoadDetailGridView();
        protected void detailGrid_Load(object sender, EventArgs e)
        {
            LoadDetailGridView();
        }
        
    }
}