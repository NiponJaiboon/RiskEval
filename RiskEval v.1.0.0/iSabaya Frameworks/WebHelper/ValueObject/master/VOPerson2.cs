using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

using imSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOPerson2
    {
        private int fundManagerID;

        public int FundManagerID
        {
            get { return fundManagerID; }
            set { fundManagerID = value; }
        }
        private string livePeriod;

        public string LivePeriod
        {
            get { return livePeriod; }
            set { livePeriod = value; }
        }
        private string category;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        private string properties;

        public string Properties
        {
            get { return properties; }
            set { properties = value; }
        }
        private string personalURL;

        public string PersonalURL
        {
            get { return personalURL; }
            set { personalURL = value; }
        }
        private string currentName;

        public string CurrentName
        {
            get { return currentName; }
            set { currentName = value; }
        }
        private string gender;

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        private string bloodGroup;

        public string BloodGroup
        {
            get { return bloodGroup; }
            set { bloodGroup = value; }
        }
        private string religion;

        public string Religion
        {
            get { return religion; }
            set { religion = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private string mobilePhone;

        public string MobilePhone
        {
            get { return mobilePhone; }
            set { mobilePhone = value; }
        }
        private string occupation;

        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }
        private string nationality;

        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }
        private string effectivePeriodManager;

        public string EffectivePeriodManager
        {
            get { return effectivePeriodManager; }
            set { effectivePeriodManager = value; }
        }
        private string managerCategory;

        public string ManagerCategory
        {
            get { return managerCategory; }
            set { managerCategory = value; }
        }
        private float accountablityRatio;

        public float AccountablityRatio
        {
            get { return accountablityRatio; }
            set { accountablityRatio = value; }
        }
    }
}
