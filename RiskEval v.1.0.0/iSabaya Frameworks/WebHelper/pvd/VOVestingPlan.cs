using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;
using iSabaya;
using System.Collections;

namespace WebHelper.pvd
{
	public class VOVestingPlan
	{
		private imSabayaContext context;
		public VOVestingPlan(imSabayaContext context)
		{
			this.context = context;
		}

		private VestingPlan instance;
		public VOVestingPlan(VestingPlan instance)
		{
			this.instance = instance;
		}

		public int VestingPlanID
		{
			get { return instance.VestingPlanID; }
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

		public string Descrpition
		{
			get
			{
				if (instance.Description == null)
					return "-";
				else
					return instance.Description.ToString();
			}
		}

		public string Reference
		{
			get { return instance.Reference; }
		}

		//public UnvestPolicy Unvestpolixy
		//{
		//    get { return instance.UnvestPolicy; }
		//}

		//public PercentageOfAmount PercentageOf
		//{
		//    get { return instance.PercentageOf; }
		//}

		public string Remark
		{
			get { return instance.Remark; }
		}

		//public String IsLower
		//{
		//    get
		//    {
		//        if (instance.IsLowerBoundInclusive == true)
		//        {
		//            return "ขั้นต่ำ<= อายุ <ขั้นสูง ";
		//        }
		//        else
		//        {
		//            return "ขั้นต่ำ< อายุ <=ขั้นสูง";
		//        }
		//    }
		//}

		//public String BasedOn
		//{
		//    get
		//    {
		//        if (instance.BasedOnMembershipDuration == true)
		//        {
		//            return "อายุงาน";
		//        }
		//        else
		//        {
		//            return "อายุสมาชิก";
		//        }
		//    }
		//}

		public String Termination
		{
			get
			{
				if (instance.TerminationCategory.Title == null)
					return "-";
				else
					return instance.TerminationCategory.Title.ToString();
			}
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
				if (instance.EffectivePeriod.Includes(DateTime.Now))
					return true;
				else
					return false;
			}
		}

		public static IList<VOVestingGroup> findGroup(imSabayaContext context, int planID)
		{
			VestingPlan plan = context.PersistencySession.Get<VestingPlan>(planID);
			IList<VOVestingGroup> vos = new List<VOVestingGroup>();
			foreach (VestingGroup item in plan.Groups)
			{
				vos.Add(new VOVestingGroup(context, item));
			}
			ArrayList.Adapter((IList)vos).Sort();
			return vos;
		}
	}
}