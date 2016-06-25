using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOTreeListNode2
    {
        private TreeListNode instance;
        public VOTreeListNode2(TreeListNode instance)
        {
            this.instance = instance;
        }

        public int NodeID
        {
            get { return instance.NodeID; }
        }

        public string Code
        {
            get { return instance.Code; }
        }

        public int SeqNo
        {
            get { return instance.SeqNo; }
        }

        public int Level
        {
            get { return instance.Level; }
        }

        public double Weight
        {
            get { return instance.Weight; }
        }

        public double ValueNumber
        {
            get { return instance.ValueNumber; }
        }

        public string Remark
        {
            get { return instance.Remark; }
        }

        public bool IsBuiltin
        {
            get { return instance.IsBuiltin; }
        }

        public bool IsParent
        {
            get { return instance.IsParent; }
        }

        public bool IsActive
        {
            get { return instance.IsActive; }
        }

        public string Title
        {
            get
            {
                if (instance.Title == null)
                    return "-";
                else
                    return instance.Title.ToString();
            }
        }

        public string ShortTitle
        {
            get
            {
                if (instance.ShortTitle == null)
                    return "-";
                else
                    return instance.ShortTitle.ToString();
            }
        }

        public string Description
        {
            get
            {
                if (instance.Description == null)
                    return "-";
                else
                    return instance.Description.ToString();
            }
        }

        public string RelatedTreeListRoot
        {
            get
            {
                if (instance.RelatedNode == null)
                    return "-";
                else
                    return instance.RelatedNode.ToString();
            }
        }

        public string Root
        {
            get
            {
                if (instance.Root == null)
                    return "-";
                else
                    return instance.Root.ToString();
            }
        }

        public string Parent
        {
            get
            {
                if (instance.Parent == null)
                    return "-";
                else
                    return instance.Parent.ToString();
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
    }
}
