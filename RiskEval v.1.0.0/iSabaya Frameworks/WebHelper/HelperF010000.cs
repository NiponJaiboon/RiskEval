using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using WebHelper.ValueObject;
using NHibernate;
using imSabaya;

namespace WebHelper
{
	[Serializable]
	public class HelperF010000
	{
		private List<PartyAttribute> fundAttributes;
		private List<InstrumentOrganization> fundRelatedOrgs;
		private List<PartyBankAccount> fundBankAccounts;
		private List<FundRelation> fundRelations;

		public List<PartyAttribute> FundAttributes
		{
			get
			{
				if (fundAttributes == null)
				{
					fundAttributes = new List<PartyAttribute>();
				}
				return fundAttributes;

			}
			set { this.fundAttributes = value; }
		}
        public List<InstrumentOrganization> FundRelatedOrgs
		{
			get
			{
				if (fundRelatedOrgs == null)
				{
                    fundRelatedOrgs = new List<InstrumentOrganization>();
				}
				return fundRelatedOrgs;
			}
			set { this.fundRelatedOrgs = value; }
		}
		public List<PartyBankAccount> FundBankAccounts
		{
			get
			{
				if (fundBankAccounts == null)
				{
					fundBankAccounts = new List<PartyBankAccount>();
				}
				return fundBankAccounts;
			}
			set { this.fundBankAccounts = value; }
		}
		public List<FundRelation> FundRelations
		{
			get
			{
				if (fundRelations == null)
				{
					fundRelations = new List<FundRelation>();
				}
				return fundRelations;
			}
			set { this.fundRelations = value; }
		}

		private MultilingualString title;
		public MultilingualString Title
		{
			get { return title; }
			set { this.title = value; }
		}
		private String code;
		public String Code
		{
			get { return code; }
			set { this.code = value; }
		}
		private String description1;
		public String Description1
		{
			get { return description1; }
			set { this.description1 = value; }
		}
		private String description2;
		public String Description2
		{
			get { return description2; }
			set { this.description2 = value; }
		}
		private String description3;
		public String Description3
		{
			get { return description3; }
			set { this.description3 = value; }
		}
		private String description4;
		public String Description4
		{
			get { return description4; }
			set { this.description4 = value; }
		}
		private String description5;
		public String Description5
		{
			get { return description5; }
			set { this.description5 = value; }
		}
		private Organization fundRelateOrg;
		public Organization FundRelateOrg
		{
			get { return fundRelateOrg; }
			set { this.fundRelateOrg = value; }
		}
		private DateTime purchaseHours;
		public DateTime PurchaseHours
		{
			get { return purchaseHours; }
			set { this.purchaseHours = value; }
		}
		private DateTime redeemHours;
		public DateTime RedeemHours
		{
			get { return redeemHours; }
			set { this.redeemHours = value; }
		}
		private double par;
		public double Par
		{
			get { return par; }
			set { this.par = value; }
		}
		private Currency baseCurrency;
		public Currency BaseCurrency
		{
			get { return baseCurrency; }
			set { this.baseCurrency = value; }
		}
		private DateTime registerDate;
		public DateTime RegisterDate
		{
			get { return registerDate; }
			set { this.registerDate = value; }
		}
		private double registerSize;
		public double RegisterSize
		{
			get { return registerSize; }
			set { this.registerSize = value; }
		}
		private double targetSize;
		public double TargetSize
		{
			get { return targetSize; }
			set { this.targetSize = value; }
		}


		public void RemoveFundAttributes(int FundAttributeID)
		{
			int i = 0;
			int index = 0;
			foreach (PartyAttribute fa in FundAttributes)
			{
                if (fa.PartyAttributeID == FundAttributeID)
				{
					index = i;
					break;
				}
				i++;
			}
			FundAttributes.RemoveAt(index);
		}

		public void RemoveFundRelatedOrg(int FundRelatedOrgID)
		{
			int i = 0;
			int index = 0;
            foreach (InstrumentOrganization fro in fundRelatedOrgs)
			{
				if (fro.InstrumentOrganizationID == FundRelatedOrgID)
				{
					index = i;
					break;
				}
				i++;
			}
			fundRelatedOrgs.RemoveAt(index);
		}

		public void RemoveFundBankAccount(int fundBankAccountID)
		{
			int i = 0;
			int index = 0;
			foreach (PartyBankAccount bfa in fundBankAccounts)
			{
				if (bfa.ID == fundBankAccountID)
				{
					index = i;
					break;
				}
				i++;
			}
			fundBankAccounts.RemoveAt(index);
		}

		public void RemoveFundRelation(int FundRelationID)
		{
			int i = 0;
			int index = 0;
			foreach (FundRelation fr in fundRelations)
			{
				if (fr.FundRelationID == FundRelationID)
				{
					index = i;
					break;
				}
				i++;
			}
			fundRelations.RemoveAt(index);
		}
	}
}
