using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class Job
    {

        protected int id;
        public virtual int ID
        {
            get { return id; }
            set { id = value; }
        }

        protected string code;
        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        protected MultilingualString title;
        public virtual MultilingualString Title
        {
            get { return title; }
            set { title = value; }
        }

        protected MultilingualString shortTitle;
        public virtual MultilingualString ShortTitle
        {
            get { return shortTitle; }
            set { shortTitle = value; }
        }

        protected TimeInterval effectivePeriod;
        public virtual TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        protected MultilingualString description;
        public virtual MultilingualString Description
        {
            get { return description; }
            set { description = value; }
        }

        protected string remark;
        public virtual string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        protected bool isActive;
        public virtual bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        protected int maxPersonnelLevel;
        public virtual int MaxPersonnelLevel
        {
            get { return maxPersonnelLevel; }
            set { maxPersonnelLevel = value; }
        }

        protected int minPersonnelLevel;
        public virtual int MinPersonnelLevel
        {
            get { return minPersonnelLevel; }
            set { minPersonnelLevel = value; }
        }

        protected Money maxSalary;
        public virtual Money MaxSalary
        {
            get { return maxSalary; }
            set { maxSalary = value; }
        }

        protected Money minSalary;
        public virtual Money MinSalary
        {
            get { return minSalary; }
            set { minSalary = value; }
        }

        private Organization organization;
        public virtual Organization Organization
        {
            get { return organization; }
            set { organization = value; }
        }

        public virtual string ToLog()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            builder.Append("ID:");
            builder.Append(ID);
            builder.Append(", ");

            builder.Append("Code:");
            builder.Append(Code);
            builder.Append(", ");

            builder.Append("Title:");
            builder.Append(Title.ToLog());
            builder.Append(", ");

            builder.Append("ShortTitle:");
            builder.Append(ShortTitle.ToLog());
            builder.Append(", ");

            //builder.Append("EffectivePeriod:");
            //builder.Append(EffectivePeriod.ToLog());
            //builder.Append(", ");

            builder.Append("Description:");
            builder.Append(Description.ToLog());
            builder.Append(", ");

            builder.Append("Remark:");
            builder.Append(Remark);
            builder.Append(", ");

            builder.Append("IsActive:");
            builder.Append(IsActive);
            builder.Append(", ");

            builder.Append("MaxPersonnelLevel:");
            builder.Append(MaxPersonnelLevel);
            builder.Append(", ");

            builder.Append("MinPersonnelLevel:");
            builder.Append(MinPersonnelLevel);
            builder.Append(", ");

            builder.Append("MaxSalary:");
            builder.Append(MaxSalary);
            builder.Append(", ");

            builder.Append("MinSalary:");
            builder.Append(MinSalary);
            builder.Append(", ");

            //builder.Append("Organization:");
            //builder.Append(Organization.ToLog());
            //builder.Append("]");

            return builder.ToString();
        }

    }
} // iSabaya.Organization
