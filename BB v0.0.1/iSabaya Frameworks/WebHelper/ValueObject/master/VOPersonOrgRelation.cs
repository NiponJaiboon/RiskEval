using System;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOPersonOrgRelation
    {
        private PersonOrgRelation instance;

        public VOPersonOrgRelation(PersonOrgRelation instance)
        {
            this.instance = instance;
        }

        private bool isInvestmentPlanner = false;
        public bool IsInvestmentPlanner
        {
            get { return this.isInvestmentPlanner; }
            set { this.isInvestmentPlanner = value; }
        }

        public int PersonOrgRelationID
        {
            get { return instance.PersonOrgRelationID; }
        }

        public string RelationshipCategory
        {
            get
            {
                if (instance.RelationshipCategory == null)
                    return "-";
                else
                    return instance.RelationshipCategory.ToString();
            }
        }

        public string Person
        {
            get
            {
                if (instance.Person == null)
                    return "-";
                else
                    return instance.Person.ToString();
            }
        }

        public int PersonID
        {
            get
            {
                if (instance.Person == null)
                    return 0;
                else
                    return instance.Person.PersonID;
            }
        }

        public string Organization
        {
            get
            {
                if (instance.Organization == null)
                    return "-";
                else
                    return instance.Organization.FullName;
            }
        }

        public string OrgUnit
        {
            get
            {
                if (instance.OrgUnit == null)
                    return "-";
                else
                    return instance.OrgUnit.FullName;
            }
        }

        public TimeInterval EffectivePeriod
        {
            get
            {
                return instance.EffectivePeriod;
            }
        }

        public string Code
        {
            get { return instance.Code; }
        }

        public string RelationshipNo
        {
            get { return instance.RelationshipNo; }
        }

        public string Reference
        {
            get { return instance.Reference; }
        }

        public string Remark
        {
            get { return instance.Remark; }
        }

        public int SeqNo
        {
            get { return instance.SeqNo; }
        }

        public int Level
        {
            get { return instance.Level; }
        }
    }
}