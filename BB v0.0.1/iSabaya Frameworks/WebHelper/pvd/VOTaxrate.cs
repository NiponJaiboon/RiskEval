using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using iSabaya;

namespace WebHelper.pvd
{
	[Serializable]
	public class VOTaxrate
	{
		private TaxRate instance;
		public VOTaxrate(TaxRate instance)
		{
			this.instance = instance;
		}

		public int TaxRateID
		{
			get { return instance.TaxRateID; }
		}

		public TimeInterval EffectivePeriod
		{
			get
			{
				return instance.EffectivePeriod;
			}
		}

		public String EffectivePeriodFrom
		{
			get
			{
				if (instance.EffectivePeriod == null)
				{
					return "-";
				}
				return instance.EffectivePeriod.From.ToString("yyyy/MM/dd");
			}
		}

		public float Rate
		{
			get
			{
				return instance.Rate;
			}
		}

		public String UpdatedTSString
		{
			get { return instance.UpdatedTS.ToString("yyyy/MM/dd HH:mm:ss"); }
		}

		public String UpdatedBy
		{
			get
			{
				return instance.UpdatedBy.ToString();
			}
		}

		public bool IsEffectivePeriod
		{
			get
			{
				if (DateTime.Now > instance.EffectivePeriod.To)
					return true;
				else
					return false;
			}
		}
	}
}
