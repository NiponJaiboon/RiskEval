using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;
namespace WebHelper.pvd
{
	[Serializable]
	public class VOInvestment
	{
		private InvestmentPlan instance;
		public VOInvestment(InvestmentPlan instance)
		{
			this.instance = instance;
		}

		public int InvestmentPlanID
		{
			get { return instance.InvestmentPlanID; }
		}

		public String MasterFund
		{
			get { return instance.MasterFund.ToString(); }
		}

		public string Title
		{
			get
			{
				if (instance.Title == null)
					return "-";
				else
					return instance.Title.ToString();
			}
		}

		public string ShortTitle
		{
			get
			{
				if (instance.ShortTitle == null)
					return "-";
				else
					return instance.ShortTitle.ToString();
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
		public string EffectivePeriod
		{
			get
			{
				if (instance.EffectivePeriod == null)
					return "-";
				else
					return instance.EffectivePeriod.ToString();
			}
		}

		public bool IsEffectivePeriod
		{
			get
			{
                return instance.EffectivePeriod.Includes(DateTime.Now);
			}
		}
	}
}
