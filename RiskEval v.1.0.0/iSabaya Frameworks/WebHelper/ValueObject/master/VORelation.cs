using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VORelation
    {
        private Relation instance;
        public VORelation(Relation instance)
        {
            this.instance = instance;
        }

        public int RelationID
        {
            get { return instance.RelationID; }
        }

        public string Relationship
        {
            get
            {
                if (instance.Relationship == null)
                    return "-";
                else
                    return instance.Relationship.ToString();
            }
        }

        public string Category
        {
            get
            {
                if (instance.Category == null)
                    return "-";
                else
                    return instance.Category.ToString();
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

        public DateTime RecordedDate
        {
            get { return instance.RecordedDate; }
        }

        public string Remark
        {
            get { return instance.Remark; }
        }

        public string Reference
        {
            get { return instance.Reference; }
        }

        public string FirstEntity
        {
            get
            {
                if (instance.FirstEntity == null)
                    return "-";
                else
                    return instance.FirstEntity.ToString();
            }
        }

        public string SecondEntity
        {
            get
            {
                if (instance.SecondEntity == null)
                    return "-";
                else
                    return instance.SecondEntity.ToString();
            }
        }
    }
}
