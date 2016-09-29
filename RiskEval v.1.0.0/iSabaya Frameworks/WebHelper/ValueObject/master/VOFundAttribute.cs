using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOFundAttribute
    {
        private int fundAttributeID;
        public int FundAttributeID
        {
            get { return fundAttributeID; }
            set { fundAttributeID = value; }
        }

        protected string attributeRootNode;
        public string AttributeRootNode
        {
            get { return attributeRootNode; }
            set { attributeRootNode = value; }
        }

        protected string attributeKey;
        public string AttributeKey
        {
            get { return attributeKey; }
            set { attributeKey = value; }
        }

        protected string attributeDescription;
        public string AttributeDescription
        {
            get { return attributeDescription; }
            set { attributeDescription = value; }
        }

        private string fund;
        public string Fund
        {
            get { return fund; }
            set { fund = value; }
        }
    }
}
