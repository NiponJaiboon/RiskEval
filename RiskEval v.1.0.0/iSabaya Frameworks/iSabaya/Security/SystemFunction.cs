using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public class SystemFunction
    {
        #region persistent

        public virtual int ID { get; set; }
        public virtual string Code { get; set; }
        public virtual SystemEnum SystemID { get; set; }
        public virtual MultilingualString ShortTitle { get; set; }
        public virtual MultilingualString Title { get; set; }
        public virtual MultilingualString Description { get; set; }
        public virtual TimeInterval EffectivePeriod { get; set; }
        public virtual string URL { get; set; }
        public virtual string Action { get; set; }
        private IList<FunctionMenu> functionMenus;
        public virtual IList<FunctionMenu> FunctionMenus
        {
            get
            {
                if (null == this.functionMenus)
                    this.functionMenus = new List<FunctionMenu>();
                return this.functionMenus;
            }

            set
            {
                this.functionMenus = value;
            }
        }

        #endregion persistent

        public virtual void Save(Context context)
        {
            if (null != this.ShortTitle) this.ShortTitle.Persist(context);
            if (null != this.Title) this.Title.Persist(context);
            if (null != this.Description) this.Description.Persist(context);
            context.Persist(this);

            //foreach (FunctionMenu m in this.FunctionMenus)
            //{
            //    m.Function = this;
            //    m.Save(context);
            //}
        }
    }
}