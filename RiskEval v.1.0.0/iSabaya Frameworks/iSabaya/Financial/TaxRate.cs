using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
	[Serializable]
	public class TaxRate
	{
        #region persistent
		
        private int taxRateID;
		public virtual int TaxRateID
		{
			get { return this.taxRateID; }
            set { this.taxRateID = value; }
		}

        private String code;
        public virtual String Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        private TimeInterval effectivePeriod;
        public virtual TimeInterval EffectivePeriod
		{
			get{ return this.effectivePeriod;}
            set { this.effectivePeriod = value; }
        }

        private float rate;
        public virtual float Rate
		{
			get{return this.rate;}
            set { this.rate = value; }
        }

        private String reference;
        public virtual String Reference
        {
            get { return this.reference; }
            set { this.reference = value; }
        }

        private String remark;
        public virtual String Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        private DateTime updatedTS;
        public virtual DateTime UpdatedTS
		{
			get { return this.updatedTS; }
            set { this.updatedTS = value; }
        }

        private User updatedBy;
        public virtual User UpdatedBy
		{
			get{return this.updatedBy;}
            set { this.updatedBy = value; }
        }

        #endregion persistent

        public virtual void Save(Context context)
        {
            context.PersistenceSession.SaveOrUpdate(this);
        }

        public static IList<TaxRate> List(Context context)
        {
            return context.PersistenceSession.CreateCriteria<TaxRate>().List<TaxRate>();
        }
	}
}
