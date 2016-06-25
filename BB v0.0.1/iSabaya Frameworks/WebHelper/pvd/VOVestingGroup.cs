using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;
using iSabaya;

namespace WebHelper.pvd
{
	public class VOVestingGroup : IComparable
	{
		private VestingGroup instance;
		private imSabayaContext context;
		public VOVestingGroup(imSabayaContext context, VestingGroup instance)
		{
			this.context = context;
			this.instance = instance;
		}

		public int VestingGroupID
		{
			get { return instance.VestingGroupID; }
		}

		public String Code
		{
			get { return instance.Code; }
		}

		public String Unvestpolixy
		{
			get { return instance.UnvestPolicy.ToString(); }
		}

		public String PercentageOf
		{
			get { return instance.PercentageOf.ToString(); }
		}

		public String IsLower
		{
			get
			{
				if (instance.IsLowerBoundInclusive == true)
				{
					return "ขั้นต่ำ<= อายุ <ขั้นสูง ";
				}
				else
				{
					return "ขั้นต่ำ< อายุ <=ขั้นสูง";
				}
			}
		}

		public String BasedOn
		{
			get
			{
				if (instance.BasedOnMembershipDuration == true)
				{
					return "อายุงาน";
				}
				else
				{
					return "อายุสมาชิก";
				}
			}
		}

		public String InvestmentCategoryCode
		{
			get { return instance.InvestmentCategory.ToString(); }
		}

		public TreeListNode InvestmentCategory
		{
			get { return instance.InvestmentCategory; }
		}

		public int Compare(object a, object b)
		{
			VOVestingGroup aa = (VOVestingGroup)a;
			VOVestingGroup bb = (VOVestingGroup)b;

			if (aa.InvestmentCategory.NodeID > bb.InvestmentCategory.NodeID)
			{
				return 1;
			}
			else if (aa.InvestmentCategory.NodeID < bb.InvestmentCategory.NodeID)
			{
				return -1;
			}
			else
			{
				return 0;
			}
		}

		int IComparable.CompareTo(object obj)
		{
			return this.Compare(this, obj);
		}
	}
}
