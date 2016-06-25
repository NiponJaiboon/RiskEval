using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

using imSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOPerson
    {
        private TimeInterval livePeriod;
        private TreeListNode category;
        private iSabaya.PropertyValueContainerBase properties;
        private PersonName currentName;
        private TreeListNode gender;
        private TreeListNode bloodGroup;
        private TreeListNode religion;
        private string email;
        private string phone;
        private string mobilePhone;
        private IList<PersonName> names;
        private TreeListNode occupation;
        private TreeListNode nationality;
        private User updatedBy;
        private DateTime updatedTS = DateTime.Now;

        public TimeInterval LivePeriod
        {
            get { return livePeriod; }
            set { livePeriod = value; }
        }

        public TreeListNode Category
        {
            get { return category; }
            set { category = value; }
        }

        public iSabaya.PropertyValueContainerBase Properties
        {
            get { return properties; }
            set { properties = value; }
        }

        public User UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        public DateTime UpdatedTS
        {
            get { return updatedTS; }
            set { updatedTS = value; }
        }

        public TreeListNode Gender
        {
            get { return gender; }
            set { gender = value; }
        }


        public TreeListNode BloodGroup
        {
            get { return bloodGroup; }
            set { bloodGroup = value; }
        }

        public TreeListNode Religion
        {
            get { return religion; }
            set { religion = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string MobilePhone
        {
            get { return mobilePhone; }
            set { mobilePhone = value; }
        }

        public PersonName CurrentName
        {
            get { return currentName; }
            set
            {
                if (value == null) return;
                if (currentName != null)
                {
                    currentName.EffectivePeriod.To = value.EffectivePeriod.From.AddDays(-1d);
                }
                currentName = value;
                Names.Add(currentName);
            }
        }

        private Country citizenOf;
        public Country CitizenOf
        {
            get { return citizenOf; }
            set { citizenOf = value; }
        }
        private String url;
        public String URL
        {
            get { return url; }
            set { url = value; }
        }
        public IList<PersonName> Names
        {
            get
            {
                if (names == null) names = new List<PersonName>();
                return names;
            }
            set { names = value; }
        }

        public TreeListNode Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }

        public TreeListNode Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }

        private int fundManagerID;
        public int FundManagerID
        {
            get { return fundManagerID; }
            set { fundManagerID = value; }
        }

        private string fund;
        public string Fund
        {
            get { return fund; }
            set { fund = value; }
        }

        private TimeInterval effectivePeriod;
        public TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        private float accountablityRatio;
        public float AccountablityRatio
        {
            get { return accountablityRatio; }
            set { accountablityRatio = value; }
        }

        private string managerCategory;
        public string ManagerCategory
        {
            get { return managerCategory; }
            set { managerCategory = value; }
        }

        private string manager;
        public string Manager
        {
            get { return manager; }
            set { manager = value; }
        }
    }
}
