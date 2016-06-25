using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOTreeListNode
    {
        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        private int seqNo;

        public int SeqNo
        {
            get { return seqNo; }
            set { seqNo = value; }
        }

        private int level;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        private double weight;
        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        private double value;
        public double ValueNumber
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private bool isBuiltin;
        public bool IsBuiltin
        {
            get { return isBuiltin; }
            set { isBuiltin = value; }
        }

        private bool isParent;
        public bool IsParent
        {
            get { return isParent; }
            set { isParent = value; }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        private string titleCode;
        public string TitleCode
        {
            get { return titleCode; }
            set { titleCode = value; }
        }

        private string shortTitleCode;
        public string ShortTitleCode
        {
            get { return shortTitleCode; }
            set { shortTitleCode = value; }
        }

        private string descriptionCode;
        public string DescriptionCode
        {
            get { return descriptionCode; }
            set { descriptionCode = value; }
        }

        private string relatedTreeListRootCode;
        public string RelatedTreeListRootCode
        {
            get { return relatedTreeListRootCode; }
            set { relatedTreeListRootCode = value; }
        }

        private string countryCode2;
        public string CountryCode2
        {
            get { return countryCode2; }
            set { countryCode2 = value; }
        }

        private string ruleDescription;
        public string RuleDescription
        {
            get { return ruleDescription; }
            set { ruleDescription = value; }
        }
        private int nodeID;
        public virtual int NodeID
        {
            get
            {

                return nodeID;
            }
            set { nodeID = value; }
        }
        private int parentNodeID;
        public virtual int ParentNodeID
        {
            get
            {

                return parentNodeID;
            }
            set { parentNodeID = value; }
        }
    }
}
