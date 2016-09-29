using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class DynamicMenu
    {
        public DynamicMenu()
        {
        }
        public DynamicMenu(int id)
        {
            this.id = id;
        }

        public DynamicMenu(String code, String linkURL, bool isTop)
        {
            this.code = code;
            this.linkURL = linkURL;
            this.isTop = isTop;
        }
        public DynamicMenu(int id, String code, String linkURL, bool isTop)
        {
            this.id = id;
            this.code = code;
            this.linkURL = linkURL;
            this.isTop = isTop;
        }

        #region persistent

        private int id;
        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        private String code;
        public virtual String Code
        {
            get { return code; }
            set { code = value; }
        }

        //private bool haveChild;
        //public virtual bool HaveChild
        //{
        //    get { return haveChild; }
        //    set { haveChild = value; }
        //}

        private bool isObsolete;
        public virtual bool IsObsolete
        {
            get { return isObsolete; }
            set { isObsolete = value; }
        }

        private bool isTop;
        public virtual bool IsTop
        {
            get { return isTop; }
            set { isTop = value; }
        }

        private String linkURL;
        public virtual String LinkURL
        {
            get { return linkURL; }
            set { linkURL = value; }
        }

        private String pagecode;
        public virtual String PageCode
        {
            get { return pagecode; }
            set { pagecode = value; }
        }

        private DynamicMenu parent;
        public virtual DynamicMenu Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        private SystemEnum systemID;
        public virtual SystemEnum SystemID
        {
            get { return systemID; }
            set { systemID = value; }
        }

        private MultilingualString title;
        public virtual MultilingualString Title
        {
            get { return title; }
            set { title = value; }
        }

        public virtual int UsecaseNumber { get; set; }

        private IList<DynamicMenu> children;
        public virtual IList<DynamicMenu> Children
        {
            get
            {
                return children;
            }
            set { children = value; }
        }

        private String comment;
        public virtual String Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private int seqNo;
        public virtual int SeqNo
        {
            get { return seqNo; }
            set { seqNo = value; }
        }

        #endregion persistent

        public override bool Equals(object obj)
        {
            DynamicMenu menu = obj as DynamicMenu;
            if (null == menu) return false;
            return this.Id == menu.Id;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }

        public virtual bool Show { get; set; }

        public override string ToString()
        {
            if (this.Children.Count == 0)
                return seqNo + ":" + id + ":" + this.PageCode;
            else
                return seqNo + ":" + id + ":" + this.linkURL;
        }

        public static IList<DynamicMenu> GetHaveChild(Context context)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria<DynamicMenu>();

            crit.Add(Expression.Eq("HaveChild", true));

            return crit.List<DynamicMenu>();
        }

        public static IList<DynamicMenu> GetTopMenus(Context context, SystemEnum system)
        {
            return context.PersistenceSession
                        .QueryOver<DynamicMenu>()
                        .Where(m => m.SystemID == system && m.IsObsolete == false && m.IsTop == true)
                        .OrderBy(m => m.SeqNo)
                        .Asc()
                        .List();
            //return context.PersistenceSession.CreateCriteria<DynamicMenu>()
            //            .Add(Expression.Eq("SystemID", (int)system))
            //            .Add(Expression.Eq("IsObsolete", false))
            //            .Add(Expression.Eq("IsTop", true))
            //            .AddOrder(Order.Asc("SeqNo"))
            //            .List<DynamicMenu>();
        }

        public static DynamicMenu Find(Context context, String url, SystemEnum system)
        {
            return context.PersistenceSession
                            .QueryOver<DynamicMenu>()
                            .Where(m => m.SystemID == system)
                            .WhereRestrictionOn(m => m.LinkURL)
                            .IsInsensitiveLike(url)
                            .SingleOrDefault();
            //return context.PersistenceSession.CreateCriteria<DynamicMenu>()
            //                .Add(Expression.InsensitiveLike("LinkURL", url))
            //                .Add(Expression.Eq("SystemID", SystemID))
            //                .UniqueResult<DynamicMenu>();
        }

        public static DynamicMenu Find(Context context, int id)
        {
            DynamicMenu dynamicMenu = (DynamicMenu)context.PersistenceSession.Get(typeof(DynamicMenu), id);
            return dynamicMenu;
        }

        public virtual void Delete(Context context)
        {
            context.PersistenceSession.Delete(this);
        }

        public virtual void Update(Context context)
        {
            context.PersistenceSession.Update(this);
        }

        public virtual void save(Context context)
        {
            context.Persist(this);
        }

        public static IList<DynamicMenu> ListParent(Context context)
        {
            return context.PersistenceSession.CreateCriteria<DynamicMenu>()
                            .Add(Expression.Eq("HaveChild", true))
                            .Add(Expression.Eq("IsTop", true))
                            .List<DynamicMenu>();
        }
    }
}