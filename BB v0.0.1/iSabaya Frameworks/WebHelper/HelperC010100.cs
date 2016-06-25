using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper
{
    [Serializable]
    public class HelperC010100
    {
        private String cardCode;

        public String CardCode
        {
            get { return cardCode; }
            set { cardCode = value; }
        }
        private DateTime registerDate;

        public DateTime RegisterDate
        {
            get { return registerDate; }
            set { registerDate = value; }
        }
        private TreeListNode cardType;

        public TreeListNode CardType
        {
            get { return cardType; }
            set { cardType = value; }
        }
        private String issue;

        public String Issue
        {
            get { return issue; }
            set { issue = value; }
        }
        private DateTime issueDate;

        public DateTime IssueDate
        {
            get { return issueDate; }
            set { issueDate = value; }
        }
        private DateTime expireDate;

        public DateTime ExpireDate
        {
            get { return expireDate; }
            set { expireDate = value; }
        }
        private TreeListNode namePrefix;

        public TreeListNode NamePrefix
        {
            get { return namePrefix; }
            set { namePrefix = value; }
        }
        private String firstName;

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        private String lastName;

        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        private object gender;

        public object Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        private object ownerTaxType;

        public object OwnerTaxType
        {
            get { return ownerTaxType; }
            set { ownerTaxType = value; }
        }
        private String phone;

        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private String mobile;

        public String Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        private String email;

        public String Email
        {
            get { return email; }
            set { email = value; }
        }
        private String nationality;

        public String Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }
        private GeographicAddress address1;

        public GeographicAddress Address1
        {
            get { return address1; }
            set { address1 = value; }
        }
        private GeographicAddress address2;

        public GeographicAddress Address2
        {
            get { return address2; }
            set { address2 = value; }
        }
        private GeographicAddress address3;

        public GeographicAddress Address3
        {
            get { return address3; }
            set { address3 = value; }
        }
        private TreeListNode occupation;

        public TreeListNode Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }
        private String texID;

        public String TexID
        {
            get { return texID; }
            set { texID = value; }
        }
        private object taxType;

        public object TaxType
        {
            get { return taxType; }
            set { taxType = value; }
        }
    }

}
