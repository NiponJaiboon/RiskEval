using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class RunningNumber
    {
        private int id;
        private String code;
        private int next;
        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }
        public virtual String Code
        {
            get { return code; }
            set { code = value; }
        }
        public virtual int Next
        {
            get { return next; }
            set { next = value; }
        }

        public static int NextMLSID(Context context)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(RunningNumber));
            crit.Add(Expression.Eq("Code", "MultilingualString"));
            RunningNumber runNumber = crit.UniqueResult<RunningNumber>();
            int next = runNumber.Next;
            runNumber.Next++;
            context.PersistenceSession.Update(runNumber);
            return next;
        }

        public static int CurrentAccountNo(Context context)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(RunningNumber));
            crit.Add(Expression.Eq("Code", "Account.AccountNo"));
            RunningNumber runNumber = crit.UniqueResult<RunningNumber>();
            int next = runNumber.Next;
            return next;
        }

        public static int UpdateAccountNo(Context context, int number)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(RunningNumber));
            crit.Add(Expression.Eq("Code", "Account.AccountNo"));
            RunningNumber runNumber = crit.UniqueResult<RunningNumber>();
            int next = runNumber.Next;

            if (next == number)
            {
                runNumber.Next++;
                context.PersistenceSession.Update(runNumber);
                //session.Flush();
                return number;
            }
            else
            {
                return next;
            }

        }

        public static int CurrentTransactionNo(Context context)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(RunningNumber));
            crit.Add(Expression.Eq("Code", "TransactionNo"));
            RunningNumber runNumber = crit.UniqueResult<RunningNumber>();
            int next = runNumber.Next;
            return next;
        }

        public static int UpdateTransactionNo(Context context, int number)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(RunningNumber));
            crit.Add(Expression.Eq("Code", "TransactionNo"));
            RunningNumber runNumber = crit.UniqueResult<RunningNumber>();
            int next = runNumber.Next;
            if (next == number)
            {
                runNumber.Next++;
                context.PersistenceSession.Update(runNumber);
                return number;
            }
            else
            {
                return next;
            }
        }

        public virtual string ToLog()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            builder.Append("Id:");
            builder.Append(Id);
            builder.Append(", ");

            builder.Append("Code:");
            builder.Append(Code);
            builder.Append(", ");

            builder.Append("Next:");
            builder.Append(Next);
            builder.Append("]");

            return builder.ToString();
        }

    }
}
