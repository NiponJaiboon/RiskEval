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
    public class Configuration : PersistentTemporalEntity
    {
        public static ISessionFactory SessionFactory { get; protected set; }
#if Debug
        public static Configuration CurrentConfiguration { get; protected set; }
#else
        public static Configuration CurrentConfiguration { get; set; }
#endif

        private static CultureInfo thaiCulture;
        public static CultureInfo ThaiCulture
        {
            get
            {
                if (null == thaiCulture)
                    thaiCulture = CultureInfo.GetCultureInfo("th-TH");
                return thaiCulture;
            }
        }

        #region persistent

        //public virtual SystemEnum SystemID { get; set; }

        /// <summary>
        /// The default number of fraction digits is 4.
        /// This property defines extra digits over the defualt.
        /// The number of fraction digits is 4 + this value.
        /// </summary>
        public virtual int NumberOfExtraFractionDigitsOfMoney { get; set; }

        public virtual OrganizationConfig Organization { get; set; }
        public virtual PersonConfig Person { get; set; }
        public virtual SecurityConfig Security { get; set; }

        //public virtual TimeInterval EffectivePeriod { get; set; }
        public virtual TreeListNode BankAccountCategoryRootNode { get; set; }
        public virtual TreeListNode BankOrgCategoryNode { get; set; }
        //public virtual MultiMoneyBracketPercentageOfMoneyRateSchedule CapitalGainTaxSchedule { get; set; }
        public virtual String ChequeNoToStringFormat { get; set; }
        public virtual TreeListNode ContactCategoryRootNode { get; set; }
        public virtual TreeListNode CountryParentNode { get; set; }

        public virtual Country DefaultCountry { get; set; }
        public virtual Currency DefaultCurrency { get; set; }
        public virtual Language DefaultLanguage { get; set; }
        public virtual TreeListNode DefaultNationality { get; set; }

        public virtual MultiMoneyBracketPercentageOfMoneyRateSchedule IncomeTaxSchedule { get; set; }
        public virtual TreeListNode NationalityParentNode { get; set; }
        public virtual TreeListNode GeographicAddressCategoryRootNode { get; set; }
        public virtual TreeListNode PartyContactCategoryRootNode { get; set; }
        public virtual TreeListNode PartyIdentityCategoryRootNode { get; set; }
        public virtual TreeListNode IdentityCategoryRootNode { get; set; }
        public virtual TreeListNode RelationshipCategoryParentNode { get; set; }
        /// <summary>
        /// Percentage
        /// </summary>
        public virtual double SalesTaxRate { get; set; }
        public virtual MultiMoneyBracketPercentageOfMoneyRateSchedule SalesTaxSchedule { get; set; }
        public virtual TreeListNode ScheduleCategoryParentNode { get; set; }
        public virtual Rule SequenceNumberGeneratingRule { get; set; }
        /// <summary>
        /// Percentage
        /// </summary>
        public virtual double ServiceTaxRate { get; set; }
        public virtual Organization SystemOwnerOrg { get; set; }

        public virtual TreeListNode TaxScheduleCategoryRootNode { get; set; }
        /// <summary>
        /// Percentage
        /// </summary>
        public virtual double WithholdDividendTaxRate { get; set; }
        /// <summary>
        /// Percentage
        /// </summary>
        public virtual double WithholdSalesTaxRate { get; set; }
        /// <summary>
        /// Percentage
        /// </summary>
        public virtual double WithholdServiceTaxRate { get; set; }
        public virtual TimeSchedule WorkCalendar { get; set; }
        public virtual TimeSchedule NonworkCalendar { get; set; }

        #endregion persistent

        //public virtual Configuration ShallowClone()
        //{
        //    Configuration clone = new Configuration();

        //    if (null != this.Person) clone.Person = this.Person.ShallowClone();
        //    if (null != this.Organization) this.Organization.ShallowClone();
        //    if (null != this.Security) clone.Security = this.Security.ShallowClone();
        //    if (null != this.GeographicAddressCategoryRootNode) this.GeographicAddressCategoryRootNode.ID;
        //    if (null != BankAccountCategoryRootNode) BankAccountCategoryRootNode.ID;
        //    if (null != BankOrgCategoryNode) BankOrgCategoryNode.ID;
        //    if (null != CapitalGainTaxSchedule) CapitalGainTaxSchedule.Persist(context);
        //    if (null != ContactCategoryRootNode) ContactCategoryRootNode.ID;
        //    if (null != CountryParentNode) CountryParentNode.ID;
        //    if (null != DefaultCountry) DefaultCountry.ID;
        //    if (null != DefaultCurrency) DefaultCurrency.ID;
        //    if (null != DefaultLanguage) DefaultLanguage.ID;
        //    if (null != DefaultNationality) DefaultNationality.ID;
        //    if (null != IncomeTaxSchedule) IncomeTaxSchedule.Persist(context);
        //    if (null != NationalityParentNode) NationalityParentNode.ID;
        //    if (null != GeographicAddressCategoryRootNode) GeographicAddressCategoryRootNode.ID;
        //    if (null != PartyIdentityCategoryRootNode) PartyIdentityCategoryRootNode.ID;
        //    if (null != PartyContactCategoryRootNode) PartyContactCategoryRootNode.ID;
        //    if (null != IdentityCategoryRootNode) IdentityCategoryRootNode.ID;
        //    if (null != RelationshipCategoryParentNode) RelationshipCategoryParentNode.ID;
        //    if (null != SalesTaxSchedule) SalesTaxSchedule.Persist(context);
        //    if (null != ScheduleCategoryParentNode) ScheduleCategoryParentNode.ID;
        //    if (null != SequenceNumberGeneratingRule) SequenceNumberGeneratingRule.ID;
        //    if (null != SystemOwnerOrg) SystemOwnerOrg.Persist(context);
        //    if (null != WorkCalendar) WorkCalendar.ID;
        //    if (null != NonworkCalendar) NonworkCalendar.ID;
        //    if (null != TaxScheduleCategoryRootNode) TaxScheduleCategoryRootNode.ID;
        //}

        public virtual ISession Session { get; set; }

        public override void Persist(Context context)
        {
            //if (null != this.Person) this.Person.Save(context);
            //if (null != this.Organization) this.Organization.Save(context);
            //if (null != this.Security) this.Security.Save(context);
            if (null != this.GeographicAddressCategoryRootNode && this.GeographicAddressCategoryRootNode.NodeID == 0) this.GeographicAddressCategoryRootNode.Save(context);
            if (null != BankAccountCategoryRootNode && this.BankAccountCategoryRootNode.NodeID == 0) BankAccountCategoryRootNode.Save(context);
            if (null != BankOrgCategoryNode && this.BankOrgCategoryNode.NodeID == 0) BankOrgCategoryNode.Save(context);
            //if (null != CapitalGainTaxSchedule && CapitalGainTaxSchedule.ID == 0) CapitalGainTaxSchedule.Persist(context);
            if (null != ContactCategoryRootNode && ContactCategoryRootNode.NodeID == 0) ContactCategoryRootNode.Save(context);
            if (null != CountryParentNode && CountryParentNode.NodeID == 0) CountryParentNode.Save(context);
            if (null != DefaultCountry) DefaultCountry.Save(context);
            if (null != DefaultCurrency) DefaultCurrency.Save(context);
            if (null != DefaultLanguage) DefaultLanguage.Save(context);
            if (null != DefaultNationality) DefaultNationality.Save(context);
            if (null != IncomeTaxSchedule) IncomeTaxSchedule.Persist(context);
            if (null != NationalityParentNode && NationalityParentNode.NodeID == 0) NationalityParentNode.Save(context);
            if (null != GeographicAddressCategoryRootNode && GeographicAddressCategoryRootNode.NodeID == 0) GeographicAddressCategoryRootNode.Save(context);
            if (null != PartyIdentityCategoryRootNode && PartyIdentityCategoryRootNode.NodeID == 0) PartyIdentityCategoryRootNode.Save(context);
            if (null != PartyContactCategoryRootNode && PartyContactCategoryRootNode.NodeID == 0) PartyContactCategoryRootNode.Save(context);
            if (null != IdentityCategoryRootNode && IdentityCategoryRootNode.NodeID == 0) IdentityCategoryRootNode.Save(context);
            if (null != RelationshipCategoryParentNode && RelationshipCategoryParentNode.NodeID == 0) RelationshipCategoryParentNode.Save(context);
            if (null != SalesTaxSchedule && SalesTaxSchedule.ID == 0) SalesTaxSchedule.Persist(context);
            if (null != ScheduleCategoryParentNode && ScheduleCategoryParentNode.NodeID == 0) ScheduleCategoryParentNode.Save(context);
            if (null != SequenceNumberGeneratingRule && SequenceNumberGeneratingRule.ID == 0) SequenceNumberGeneratingRule.Save(context);
            if (null != SystemOwnerOrg && SystemOwnerOrg.ID == 0) SystemOwnerOrg.Persist(context);
            if (null != WorkCalendar && WorkCalendar.ID == 0) WorkCalendar.Save(context);
            if (null != NonworkCalendar && NonworkCalendar.ID == 0) NonworkCalendar.Save(context);
            if (null != TaxScheduleCategoryRootNode && TaxScheduleCategoryRootNode.NodeID == 0) TaxScheduleCategoryRootNode.Save(context);

            base.Persist(context);
        }
    }
}