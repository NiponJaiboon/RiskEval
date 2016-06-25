using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class RoleFunction
    {
        public virtual int ID {get;set;}

        public virtual SystemFunction Function { get; set; }

        public virtual Role Role {get;set;}

        //private bool canDisplay;
        //public virtual bool CanDisplay
        //{
        //    get { return canDisplay; }
        //    set { canDisplay = value; }
        //}

        //private bool canAddData;
        //public virtual bool CanAddData
        //{
        //    get { return canAddData; }
        //    set { canAddData = value; }
        //}

        //private bool canChangeData;
        //public virtual bool CanChangeData
        //{
        //    get { return canChangeData; }
        //    set { canChangeData = value; }
        //}

        //private bool canDeleteData;
        //public virtual bool CanDeleteData
        //{
        //    get { return canDeleteData; }
        //    set { canDeleteData = value; }
        //}

        //private bool canPrintData;
        //public virtual bool CanPrintData
        //{
        //    get { return canPrintData; }
        //    set { canPrintData = value; }
        //}

        private int seqNo;
        public virtual int SeqNo
        {
            get { return seqNo; }
            set { seqNo = value; }
        }

        public static RoleFunction Find(Context context, Role role, SystemFunction menu)
        {
            return context.PersistenceSession
                        .CreateCriteria<RoleFunction>()
                        .Add(Expression.Eq("Role", role))
                        .Add(Expression.Eq("Function", menu))
                        .UniqueResult<RoleFunction>();
        }

        public static IList<RoleFunction> List(Context context)
        {
            return context.PersistenceSession
                        .CreateCriteria<RoleFunction>()
                        .List<RoleFunction>();
        }

        public static IList<RoleFunction> List(Context context, int functionID)
        {
            return context.PersistenceSession
                        .CreateCriteria<RoleFunction>()
                        .Add(Expression.Eq("Function", new SystemFunction { ID = functionID }))
                        .List<RoleFunction>();
        }
    }
}
