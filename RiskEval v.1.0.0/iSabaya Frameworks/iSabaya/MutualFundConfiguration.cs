using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Text;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public class MutualFundConfiguration : Configuration
    {
        public static new MutualFundConfiguration CurrentConfiguration
        {
            get { return (MutualFundConfiguration)Configuration.CurrentConfiguration; }
        }

        public static new ISessionFactory SessionFactory
        {
            get { return Configuration.SessionFactory; }
            set
            {
                //if (null == CurrentConfiguration)
                //    Configuration.CurrentConfiguration = value.OpenSession().Get<MutualFundConfiguration>(1);
                //Configuration.SessionFactory = value;
            }
        }

        #region persistent

        protected TreeListNode accountReserveCategoryRootNode;
        public virtual TreeListNode AccountReserveCategoryRootNode
        {
            get { return accountReserveCategoryRootNode; }
            set { accountReserveCategoryRootNode = value; }
        }
        protected TreeListNode brokerOrgCategoryNode;
        public virtual TreeListNode BrokerOrgCategoryNode
        {
            get { return brokerOrgCategoryNode; }
            set { brokerOrgCategoryNode = value; }
        }

        public virtual TreeListNode InvestmentAdviceSourceParentNode { get; set; }

        protected TreeListNode complianceRelatedPartyRootNode;
        public virtual TreeListNode ComplianceRelatedPartyRootNode
        {
            get { return complianceRelatedPartyRootNode; }
            set { complianceRelatedPartyRootNode = value; }
        }

        protected TreeListNode complianceSanctionActionRootNode;
        public virtual TreeListNode ComplianceSanctionActionRootNode
        {
            get { return complianceSanctionActionRootNode; }
            set { complianceSanctionActionRootNode = value; }
        }

        protected TreeListNode fundManagerCategoryRootNode;
        public virtual TreeListNode FundManagerCategoryRootNode
        {
            get { return fundManagerCategoryRootNode; }
            set { fundManagerCategoryRootNode = value; }
        }

        protected TreeListNode fundOrgRoleRootNode;
        public virtual TreeListNode FundOrgRoleRootNode
        {
            get { return fundOrgRoleRootNode; }
            set { fundOrgRoleRootNode = value; }
        }

        protected TreeListNode ipCategoryRootNode;
        public virtual TreeListNode IPCategoryRootNode
        {
            get { return ipCategoryRootNode; }
            set { ipCategoryRootNode = value; }
        }

        protected TreeListNode mfAccountBalanceCategoryRootNode;
        public virtual TreeListNode MFAccountBalanceCategoryRootNode
        {
            get { return mfAccountBalanceCategoryRootNode; }
            set { mfAccountBalanceCategoryRootNode = value; }
        }

        protected TreeListNode mFAmountCategoryRootNode;
        public virtual TreeListNode MFAmountCategoryRootNode
        {
            get { return mFAmountCategoryRootNode; }
            set { mFAmountCategoryRootNode = value; }
        }

        protected TreeListNode mFAmountTypeRootNode;
        public virtual TreeListNode MFAmountTypeRootNode
        {
            get { return mFAmountTypeRootNode; }
            set { mFAmountTypeRootNode = value; }
        }

        protected TreeListNode mFCategoryRootNode;
        public virtual TreeListNode MFCategoryRootNode
        {
            get { return mFCategoryRootNode; }
            set { mFCategoryRootNode = value; }
        }

        protected TreeListNode mFCustomerCategoryRootNode;
        public virtual TreeListNode MFCustomerCategoryRootNode
        {
            get { return mFCustomerCategoryRootNode; }
            set { mFCustomerCategoryRootNode = value; }

        }

        protected TreeListNode mFDividendPolicyRootNode;
        public virtual TreeListNode MFDividendPolicyRootNode
        {
            get { return mFDividendPolicyRootNode; }
            set { mFDividendPolicyRootNode = value; }
        }

        protected TreeListNode mFIPOAllocationPolicyRootNode;
        public virtual TreeListNode MFIPOAllocationPolicyRootNode
        {
            get { return mFIPOAllocationPolicyRootNode; }
            set { mFIPOAllocationPolicyRootNode = value; }
        }

        protected TreeListNode mFTransactionChannelRootNode;
        public virtual TreeListNode MFTransactionChannelRootNode
        {
            get { return mFTransactionChannelRootNode; }
            set { mFTransactionChannelRootNode = value; }
        }

        protected TreeListNode mFTransactionFeeCategoriesRootNode;
        public virtual TreeListNode MFTransactionFeeCategoriesRootNode
        {
            get { return mFTransactionFeeCategoriesRootNode; }
            set { mFTransactionFeeCategoriesRootNode = value; }
        }

        protected bool oneFundPerAccount;
        public virtual bool OneFundPerAccount
        {
            get { return oneFundPerAccount; }
            set { oneFundPerAccount = value; }
        }

        protected TreeListNode regulatorFundCategoryRootNode;
        public virtual TreeListNode RegulatorFundCategoryRootNode
        {
            get { return regulatorFundCategoryRootNode; }
            set { regulatorFundCategoryRootNode = value; }
        }

        protected TreeListNode reserveCategoryRootNode;
        public virtual TreeListNode ReserveCategoryRootNode
        {
            get { return reserveCategoryRootNode; }
            set { reserveCategoryRootNode = value; }
        }

        protected TreeListNode sECMFCategoriesRootNode;
        public virtual TreeListNode SECMFCategoriesRootNode
        {
            get { return sECMFCategoriesRootNode; }
            set { sECMFCategoriesRootNode = value; }
        }

        protected TreeListNode transactionFeeCategoryRootNode;
        public virtual TreeListNode TransactionFeeCategoryRootNode
        {
            get { return transactionFeeCategoryRootNode; }
            set { transactionFeeCategoryRootNode = value; }
        }

        #endregion persistent

        //public virtual void Save(Context context)
        //{
        //    if (this.Person != null) this.Person.Save(context);
        //    if (this.Organization != null) this.Organization.Save(context);
        //    context.PersistenceSession.SaveOrUpdate(this);
        //}
    }
}
