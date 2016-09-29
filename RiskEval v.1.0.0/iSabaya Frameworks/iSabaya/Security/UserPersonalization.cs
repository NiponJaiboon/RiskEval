using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public class UserPersonalization
    {
        public virtual int ID { get; set; }
        public virtual User User { get; set; }
        public virtual int PageID { get; set; }
        public virtual string Personalization { get; set; }

        public virtual void Persist(Context context)
        {
            context.Persist(this);
        }
    }
}
