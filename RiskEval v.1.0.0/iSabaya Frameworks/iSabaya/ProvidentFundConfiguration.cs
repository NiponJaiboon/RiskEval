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
    public class ProvidentFundConfiguration : Configuration
    {
        public static new ProvidentFundConfiguration CurrentConfiguration
        {
            get { return (ProvidentFundConfiguration)Configuration.CurrentConfiguration; }
        }

        public static new ISessionFactory SessionFactory
        {
            get { return Configuration.SessionFactory; }
            set
            {
                //if (null == Configuration.CurrentConfiguration)
                //    Configuration.CurrentConfiguration = value.OpenSession().Get<ProvidentFundConfiguration>(1);
                //Configuration.SessionFactory = value;
            }
        }

        #region persistent

        public virtual TreeListNode AccountReserveCategoryRootNode { get; set; }
        public virtual int AllowedContributionLateDays { get; set; }
        public virtual Rule GenPFEmployeeAccountNoRule { get; set; }
        public virtual float LateContributionFineInMonthlyPercentage { get; set; }
        public virtual TreeListNode PFAmountTypeRootNode { get; set; }
        public virtual TreeListNode PFAttributeKeyRootNode { get; set; }
        public virtual TreeListNode PFCommitteeRoleParentNode { get; set; }
        public virtual string PFDateInputFormat { get; set; }
        public virtual string PFDateOutputFormat { get; set; }
        public virtual TreeListNode PFDistributionMethodParentNode { get; set; }
        public virtual Rule PFFeesAndCommissionsRule { get; set; }
        public virtual TreeListNode PFFundFeeCategoryRootNode { get; set; }
        public virtual TreeListNode PFInternalCategoryParentNode { get; set; }
        public virtual TreeListNode PFInvestmentCategoryRootNode { get; set; }
        public virtual TreeListNode PFSECCategoryRootNode { get; set; }
        public virtual TreeListNode PFSECPolicyParentNode { get; set; }
        public virtual TreeListNode PFTerminationCategoryRootNode { get; set; }
        public virtual TreeListNode PFReceiptItemCategoryRootNode { get; set; }
        public virtual TreeListNode RegulatorFundCategoryRootNode { get; set; }
        public virtual TreeListNode ReserveCategoryRootNode { get; set; }
        public virtual TreeListNode SECMFCategoriesRootNode { get; set; }

        #endregion persistent


        //public virtual void Save(Context context)
        //{
        //    if (this.Person != null) this.Person.Save(context);
        //    if (this.Organization != null) this.Organization.Save(context);
        //    context.PersistenceSession.SaveOrUpdate(this);
        //}

    }
}
