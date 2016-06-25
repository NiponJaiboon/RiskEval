using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using imSabaya.MutualFundSystem;
using iSabaya;
using imSabaya;
using NHibernate;
using NHibernate.Criterion;
using DevExpress.Web.ASPxCallback;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebHelper.Controls
{
    public class MutualFundControl : FundControl
    {
        private bool isFillterSellingAgent = false;
        public bool IsFillterSellingAgent
        {
            get { return this.isFillterSellingAgent; }
            set { this.isFillterSellingAgent = value; }
        }

        private bool isRMFFund = false;
        public bool IsRMFFund
        {
            get { return this.isRMFFund; }
            set { this.isRMFFund = value; }
        }

        private bool isLTFFund = false;
        public bool IsLTFFund
        {
            get { return this.isLTFFund; }
            set { this.isLTFFund = value; }
        }

        private bool isFIFO = false;
        public bool IsFIFO
        {
            get { return this.isFIFO; }
            set { this.isFIFO = value; }
        }

        private bool isFilterOutFIFO = false;
        public bool IsFilterOutFIFO
        {
            get { return this.isFilterOutFIFO; }
            set { this.isFilterOutFIFO = value; }
        }

        private bool isIncludeIPOPeriod = false;
        public bool IsIncludeIPOPeriod
        {
            get { return this.isIncludeIPOPeriod; }
            set { this.isIncludeIPOPeriod = value; }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            cbFund.Callback += new CallbackEventHandler(cbFund_Callback);

            ObjectDataSource obd = new ObjectDataSource();
            obd.ID = "ObjectDataSource1";
            obd.TypeName = "imSabaya.MutualFundSystem.MutualFund";
            obd.SelectMethod = "List";
            this.Controls.Add(obd);
            cbxFund.DataSourceID = obd.ID;
            cbxFund.DataBind();

        }

        protected  void cbFund_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {

        }

        public IList<MutualFund> getListFund()
        {
            return MutualFund.List(iSabayaContext);
            //List<MutualFund> listFunds = new List<MutualFund>();
            //IList<MutualFund> Funds = new List<MutualFund>();
            //if (IsFillterSellingAgent)
            //{
            //    foreach (PersonOrgRelation employer in this.User.Person.FindCurrentEmployments(iSabaya.Context))
            //    {
            //        if (employer.Organization == iSabayaContext.SystemOwnerOrg)
            //        {
            //            Funds = ComplicatedListFunds();
            //            break;
            //        }
            //        else
            //        {
            //            Funds = FundSellingAgent.ListFunds(iSabaya.Context, employer.Organization);
            //        }
            //    }
            //}
            //else
            //{
            //    Funds = ComplicatedListFunds();
            //}
            //if (TransactionTypeCode != "")
            //{
            //    if (Funds.Count > 0)
            //    {
            //        foreach (MutualFund item in Funds)
            //        {
            //            if (item.GetTransactionCalendar(TransactionType.FindByCode(iSabaya.Context, TransactionTypeCode), DateTime.Now) != null)
            //                listFunds.Add(item);
            //        }
            //    }
            //}
            //else
            //{
            //    if (Funds.Count > 0)
            //    {
            //        listFunds = new List<MutualFund>(Funds);
            //    }
            //}
            //if (listFunds.Count > 0)
            //{
            //    listFunds.Sort((x, y) => string.Compare(x.Code, y.Code));
            //}
            //return listFunds;
        }

        private IList<MutualFund> ComplicatedListFunds()
        {
            ICriteria crit = iSabayaContext.PersistencySession.CreateCriteria(typeof(MutualFund));
            crit.Add(Expression.Le("EffectivePeriod.From", DateTime.Now));
            crit.Add(Expression.Ge("EffectivePeriod.To", DateTime.Now));

            if (isIncludeIPOPeriod)
            {
                crit.Add(Expression.Le("IPOPeriod.From", DateTime.Now));
                crit.Add(Expression.Ge("IPOPeriod.To", DateTime.Now));
            }
            if (isRMFFund)
            {
                crit.Add(Expression.Eq("FundCategory", iSabayaContext.imSabayaConfig.MF.SECFundCategoryParentNode.GetChild(MFConstants.FundCategoryRMF)));
            }
            if (isLTFFund)
            {
                crit.Add(Expression.Eq("FundCategory", iSabayaContext.imSabayaConfig.MF.SECFundCategoryParentNode.GetChild(MFConstants.FundCategoryLTF)));
            }
            if (isFIFO)
            {
                crit.Add(Expression.Eq("RedemptionMethod", RedemptionMethod.FIFO));
            }
            if (isFilterOutFIFO)
            {
                crit.Add(!Expression.Eq("RedemptionMethod", RedemptionMethod.FIFO));
            }
            return crit.List<MutualFund>();
        }
    }
}
