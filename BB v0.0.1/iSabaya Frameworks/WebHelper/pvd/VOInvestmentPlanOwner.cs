using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;
using iSabaya;

namespace WebHelper.pvd
{
	[Serializable]
	public class VOInvestmentPlanOwner
	{
		public InvestmentPlanOwner instance;
		public VOInvestmentPlanOwner(InvestmentPlanOwner instance)
		{
			this.instance = instance;
		}

		//public InvestmentPlanOwner investmentPlanOwner
		//{
		//    get { return instance; }
		//}

		public int InvestmentPlanOwnerID
		{
			get { return instance.InvestmentPlanOwnerID; }
		}

		public int OwnerAccountID
		{
			get 
			{
				Member employee = (Member)instance.Owner;
				return employee.AccountID;
			}
		}

		public string InvestmentPlan
		{
			get
			{
				if (instance.InvestmentPlan == null)
				{
					return "";
				}
				else
				{
					return instance.InvestmentPlan.ToString();
				}
			}
		}

		public string Owner
		{
			get
			{
				if (instance.Owner == null)
				{
					return "";
				}
				else
				{
					return instance.Owner.ToString();
				}
			}
		}

		public string Reference
		{
			get { return instance.Reference; }
		}

		public string Remark
		{
			get { return instance.Remark; }
		}

		public TimeInterval EffectivePeriod
		{
			get
			{
				if (instance.EffectivePeriod == null)
				{
                    return null;
				}
				else
				{
					return instance.EffectivePeriod;
				}
			}
		}

		public bool MustReallocateExistingInvestments
		{
			get { return instance.MustReallocateExistingInvestments; }
		}

		public DateTime OrderedDate
		{
			get { return instance.OrderedDate; }
		}

		public bool IsEffectivePeriod
		{
			get
			{
				return instance.EffectivePeriod.Includes(DateTime.Today);
			}
		}
	}
}
