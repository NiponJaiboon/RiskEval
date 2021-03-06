using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;

namespace iSabaya
{
    public delegate bool BusinessRuleProcedure(params Object[] parameters);

    [Serializable]
    public class TreeListNode : IRelatable /*: IValueGroup, IEnumerator<IValueNode>*/
    {
        public const String NodePathDelimiter = "/";

        public TreeListNode()
        {
        }

        public TreeListNode(String code)
        {
            this.code = code;
            this.remark = code;
        }

        public TreeListNode(TreeListNode parent, int seqNo, String code, MultilingualString title)
        {
            this.seqNo = seqNo;
            this.code = code;
            this.title = title;
            if (null != parent)
                parent.AddChild(this);
        }

        public TreeListNode(TreeListNode parent, int seqNo, String code, MultilingualString title, MultilingualString shortTitle)
        {
            this.seqNo = seqNo;
            this.code = code;
            this.title = title;
            this.shortTitle = shortTitle;
            if (null != parent)
                parent.AddChild(this);
        }

        public TreeListNode(TreeListNode parent, int seqNo, String code, MultilingualString title,
                            MultilingualString shortTitle, bool isActive, bool isDefault, bool isImmutable,
                            String valueString, double valueNumber, DateTime valueDate)
        {
            this.seqNo = seqNo;
            this.code = code;
            this.title = title;
            this.shortTitle = shortTitle;
            this.valueNumber = valueNumber;
            this.isActive = isActive;
            this.isDefault = isDefault;
            this.isImmutable = isImmutable;
            if (null != parent)
                parent.AddChild(this);
        }

        #region persistent

        protected int nodeID;
        public virtual int NodeID
        {
            get { return nodeID; }
            set { nodeID = value; }
        }

        protected IList<TreeListNode> children;
        public virtual IList<TreeListNode> Children
        {
            get
            {
                if (children == null) children = new List<TreeListNode>();
                return children;
            }
            set { children = value; }
        }

        protected String code;
        public virtual String Code
        {
            get { return this.code; }
            set
            {
                if (this.code != value)
                {
                    this.code = value;
                    return;
                }
                this.ResetPath();
            }
        }

        protected Country country;
        public virtual Country Country
        {
            get { return country; }
            set { country = value; }
        }

        protected MultilingualString description;
        public virtual MultilingualString Description
        {
            get { return description; }
            set { description = value; }
        }

        protected int level;
        public virtual int Level
        {
            get { return level; }
            set { level = value; }
        }

        protected bool isActive = true;
        public virtual bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        protected bool isBuiltin = false;
        public virtual bool IsBuiltin
        {
            get { return isBuiltin; }
            set { isBuiltin = value; }
        }

        protected bool isCredit = false;
        public virtual bool IsCredit
        {
            get { return isCredit; }
            set { isCredit = value; }
        }

        protected bool isDebit = false;
        public virtual bool IsDebit
        {
            get { return isDebit; }
            set { isDebit = value; }
        }

        protected bool isDefault = false;
        public virtual bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }

        protected bool isImmutable = false;
        public virtual bool IsImmutable
        {
            get { return isImmutable; }
            set { isImmutable = value; }
        }

        protected bool isMandatory = false;
        public virtual bool IsMandatory
        {
            get { return isMandatory; }
            set { isMandatory = value; }
        }

        protected bool isParent = false;
        public virtual bool IsParent
        {
            get { return isParent; }
            set { isParent = value; }
        }

        protected String remark = "";
        public virtual String Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        protected TimeInterval effectivePeriod;
        public virtual TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        protected TreeListNode parent;
        public virtual TreeListNode Parent
        {
            get { return parent; }
            set
            {
                //if (null != parent) parent.Children.Remove(this);
                this.parent = value;
                this.SetRoot(null == this.parent ? this : this.parent.root);
                //if (null != parent) parent.Children.Add(this);
            }
        }

        protected String path;
        public virtual String Path
        {
            get { return path; }
            set { path = value; }
        }

        protected TreeListNode relatedNode;
        public virtual TreeListNode RelatedNode
        {
            get { return relatedNode; }
            set { relatedNode = value; }
        }

        protected MultilingualString relatedNodeTitle;
        public virtual MultilingualString RelatedNodeTitle
        {
            get { return relatedNodeTitle; }
            set { relatedNodeTitle = value; }
        }

        private TreeListNode root;
        public virtual TreeListNode Root
        {
            get
            {
                if (null == this.root && null != this.parent)
                    root = this.parent.Root;
                return this.root;

            }
            set { root = value; }
        }

        protected IDotNetRule rule;
        public virtual IDotNetRule Rule
        {
            get { return rule; }
            set { rule = value; }
        }

        protected int seqNo = 0;
        public virtual int SeqNo
        {
            get { return seqNo; }
            set { seqNo = value; }
        }

        protected MultilingualString shortTitle;
        public virtual MultilingualString ShortTitle
        {
            get { return shortTitle; }
            set { shortTitle = value; }
        }

        protected MultilingualString title;
        public virtual MultilingualString Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// ValueTypes is an or-ed bits of enum AttributeValueType.
        /// </summary>
        public virtual AttributeValueTypes ValueTypes { get; set; }

        protected DateTime valueDate = TimeInterval.MinDate;
        public virtual DateTime ValueDate
        {
            get { return valueDate; }
            set { this.valueDate = value; }
        }

        protected MultilingualString valueMLS = null;
        public virtual MultilingualString ValueMLS
        {
            get { return valueMLS; }
            set { valueMLS = value; }
        }

        protected double valueNumber = 0;
        public virtual double ValueNumber
        {
            get { return valueNumber; }
            set { this.valueNumber = value; }
        }

        protected String valueString;
        public virtual String ValueString
        {
            get { return valueString; }
            set { this.valueString = value; }
        }

        protected double weight = 0;
        public virtual double Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        #endregion persistent

        public virtual Object Tag { get; set; }
        public virtual BusinessRuleProcedure BusinessRuleProcedure { get; set; }

        private void SetRoot(TreeListNode root)
        {
            this.Root = root;
            foreach (TreeListNode child in this.Children)
                child.SetRoot(root);
        }

        private void ResetPath()
        {
            if (null != this.Parent)
                this.Path = this.Parent.Path + NodePathDelimiter + this.Code;
            else
                this.Path = this.Code;

            foreach (TreeListNode n in this.Children)
            {
                n.ResetPath();
            }
        }

        public virtual String ToString(String languageCode)
        {
            if (null == this.Title)
                return this.Code;
            else
                return this.Title.ToString(languageCode);
            //if (parent == null)
            //    if (null == this.Title)
            //        return this.Code;
            //    else
            //        return this.Title.ToString(languageCode);
            //else
            //    if (null == this.Title)
            //        return parent.ToString(languageCode) + "/" + this.Code;
            //    else
            //        return parent.ToString(languageCode) + "/" + this.Title.ToString(languageCode);
        }

        public override String ToString()
        {
            if (null == this.Title)
                return this.Code;
            else
                return this.Title.ToString();
            //if (parent == null)
            //    if (null == this.Title)
            //        return this.Code;
            //    else
            //        return this.Title.ToString();
            //else
            //    if (null == this.Title)
            //        return parent.ToString() + "/" + this.Code;
            //    else
            //        return parent.ToString() + "/" + this.Title.ToString();
        }

        public virtual bool IsMeOrMyAncestor(TreeListNode node)
        {
            TreeListNode n = this;

            do
            {
                if (node == n) return true;
                n = n.Parent;
            } while (n != null);

            return false;
        }

        public virtual TreeListNode GetChild(String code)
        {
            foreach (TreeListNode c in this.Children)
            {
                if (c.Code == code) return c;
            }
            return null;
        }

        public virtual TreeListNode GetDescendant(String code)
        {
            TreeListNode d;
            foreach (TreeListNode c in this.Children)
            {
                if (c.Code == code) return c;
                d = c.GetDescendant(code);
                if (null != d) return d;
            }
            return null;
        }

        public virtual bool AddChild(TreeListNode child)
        {
            //prevent null
            if (child == null) return false;

            //prevents cycle
            if (IsMeOrMyAncestor(child)) return false;

            //add new child
            child.Parent = this;
            this.Children.Add(child);
            child.ResetPath();
            this.isParent = true;
            return true;
        }

        public virtual bool RemoveChild(TreeListNode child)
        {
            bool success = this.Children.Remove(child);
            if (success)
            {
                this.IsParent = this.Children.Count > 0;
                child.Root = child;
                child.Parent = null;
                child.ResetPath();
            }
            return success;
        }

        public static bool operator ==(TreeListNode lhs, TreeListNode rhs)
        {
            if (Object.ReferenceEquals(lhs, rhs)) return true;
            if (Object.ReferenceEquals(lhs, null) || Object.ReferenceEquals(rhs, null)) return false;
            if (lhs.NodeID == 0 || rhs.NodeID == 0)
                return lhs.Code == rhs.Code && lhs.SeqNo == rhs.SeqNo && lhs.parent == rhs.parent;
            else
                return lhs.NodeID == rhs.NodeID;
        }

        public static bool operator !=(TreeListNode lhs, TreeListNode rhs)
        {
            return !(lhs == rhs);
        }

        /*nattapong 21-3-51*/
        public virtual void Save(Context context)
        {
            if (Title != null) title.Persist(context);
            if (ShortTitle != null) shortTitle.Persist(context);
            context.PersistenceSession.SaveOrUpdate(this);
            foreach (TreeListNode n in children)
            {
                n.Save(context);
            }
        }

        public static IList<TreeListNode> GetTreeFromCode(Context context, String rootCode)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(TreeListNode));
            crit.CreateAlias("Parent", "p");
            crit.Add(Expression.Eq("p.Code", rootCode));
            IList<TreeListNode> listNodes = crit.List<TreeListNode>();
            return listNodes;
        }

        public static TreeListNode FindByCode(Context context, String rootCode, String parentCode, String code)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(TreeListNode));

            crit.Add(Expression.Eq("Code", code));
            if (!String.IsNullOrEmpty(rootCode))
            {
                crit.CreateAlias("Root", "r")
                    .Add(Expression.Eq("r.Code", rootCode));
            }
            if (!String.IsNullOrEmpty(parentCode))
            {
                crit.CreateAlias("Parent", "p")
                    .Add(Expression.Eq("p.Code", parentCode));
            }
            return crit.UniqueResult<TreeListNode>();
        }

        public static TreeListNode FindByCode(Context context, TreeListNode parent, String code)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(TreeListNode));

            return crit.Add(Expression.Eq("Code", code))
                        .Add(Expression.Eq("Parent", parent))
                        .UniqueResult<TreeListNode>();
        }

        public static TreeListNode FindByCode(Context context, String code)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(TreeListNode));
            crit.Add(Expression.IsNotNull("Parent"));// is not root nat
            crit.Add(Expression.Eq("Code", code));
            TreeListNode node = crit.UniqueResult<TreeListNode>();
            return node;
        }

        public static TreeListNode FindRootByCode(Context context, String code)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(TreeListNode));
            crit.Add(Expression.IsNull("Parent"));// is root
            crit.Add(Expression.Eq("Code", code));
            TreeListNode node = crit.UniqueResult<TreeListNode>();
            return node;
        }

        public static TreeListNode Find(Context context, int nodeId)
        {
            return (TreeListNode)context.PersistenceSession.Get<TreeListNode>(nodeId);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is TreeListNode)) return false;
            TreeListNode node = obj as TreeListNode;
            if (node.NodeID == this.NodeID)
                return true;
            else
                return object.ReferenceEquals(node, this);
        }

        public override int GetHashCode()
        {
            if (this.NodeID > 0)
                return this.NodeID;
            else
                return base.GetHashCode();
        }

        public virtual void Delete(Context context)
        {
            context.PersistenceSession.Delete(this);
        }

        public virtual void Update(Context context)
        {
            context.PersistenceSession.Update(this);
        }

        //public virtual String Desc
        //{
        //    get
        //    {
        //        if (null == this.Title)
        //            return "";
        //        else
        //            return this.Title.GetValue();
        //    }
        //}

        #region IRelatable Members

        public virtual int GetID()
        {
            return nodeID;
        }

        #endregion
    }

    public class TreeListNodeComparer : IComparer
    {
        public int Compare(Object xx, Object yy)
        {
            TreeListNode x = (TreeListNode)xx;
            TreeListNode y = (TreeListNode)yy;
            if (x.NodeID > y.NodeID)
            {
                return -1;
            }
            else if (x.NodeID < y.NodeID)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

} // iSabaya
