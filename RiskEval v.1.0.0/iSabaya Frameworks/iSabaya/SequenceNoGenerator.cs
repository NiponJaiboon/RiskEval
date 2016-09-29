using System;
using System.Data;
using System.Collections.Generic;
using NHibernate;

namespace iSabaya
{
    public delegate string DelegateStringContext(Context context);
    public delegate string DelegateStringContextTimestamp(Context context, DateTime timestamp);

    [Serializable]
    public class SequenceNoGenerator
    {
        private static List<SequenceNoGenerator> instances;
        private static List<SequenceNoGenerator> Instances
        {
            get
            {
                if (instances == null) instances = new List<SequenceNoGenerator>();
                return instances;
            }
        }

        public static SequenceNoGenerator GetInstance(int systemID, int type, long subtype, long seed = 1, long increment = 1)
        {
            SequenceNoGenerator newGenerator = null;
            foreach (SequenceNoGenerator g in Instances)
                if (g.SystemID == systemID && g.SequenceType == type && g.SubsequenceType == subtype)
                {
                    newGenerator = g;
                    break;
                }

            if (null == newGenerator)
            {
                newGenerator = new SequenceNoGenerator(systemID, type, subtype, seed, increment);
                Instances.Add(newGenerator);
            }
            
            return newGenerator;
        }

        public int SystemID { get; protected set; }

        public int SequenceType { get; protected set; }

        public long SubsequenceType { get; protected set; }

        private long seed;
        public long Seed
        {
            get { return seed; }
        }

        private long increment;
        public long Increment
        {
            get { return increment; }
            //set { increment = value; }
        }

        protected SequenceNoGenerator(int systemID, int sequenceType, long subsequenceType, long seed, long increment)
        {
            this.SystemID = systemID;
            this.SequenceType = sequenceType;
            this.SubsequenceType = subsequenceType;
            this.seed = seed;
            this.increment = increment;
        }

        private System.Data.IDbCommand resetCommand;
        public virtual void ResetSequenceNumber(Context context)
        {

            if (resetCommand == null)
            {
                //String connectionString = ConfigurationSettings.AppSettings["strConnectionString"].ToString();
                //SqlConnection adoCon = new SqlConnection(connectionString);
                System.Data.IDbConnection adoCon = context.PersistenceSession.Connection;
                resetCommand = adoCon.CreateCommand();
                resetCommand.CommandText = "exec dbo.usp_ResetSequenceNo "
                                                + SystemID.ToString() + ","
                                                + SequenceType.ToString() + ","
                                                + SubsequenceType.ToString() + ";";
            }
            resetCommand.ExecuteNonQuery();
        }

        public virtual long GenSequenceNumber(Context context)
        {
            if (context.PersistenceSession.Connection.State != ConnectionState.Open)
                context.PersistenceSession.Connection.Open();

            if (context.PersistenceSession.Connection.State != ConnectionState.Open)
                throw new iSabayaException("Can't open connection using the given session.");

            System.Data.IDbConnection adoCon = context.PersistenceSession.Connection;

            System.Data.IDbCommand genSeqNoCommand = adoCon.CreateCommand();
            genSeqNoCommand.CommandText = "declare @seqNo bigint; exec dbo.usp_GenSequenceNo "
                                                + ((int)context.MySystem.SystemID).ToString() + ","
                                                + SequenceType.ToString() + ","
                                                + SubsequenceType.ToString() + ", "
                                                + seed.ToString() + ", "
                                                + increment.ToString()
                                                + ",@seqNo output; select @seqNo";

            if (context.PersistenceSession.Transaction.IsActive)
                context.PersistenceSession.Transaction.Enlist(genSeqNoCommand);

            if (genSeqNoCommand.Connection.State != ConnectionState.Open)
                genSeqNoCommand.Connection.Open();
            return (long)genSeqNoCommand.ExecuteScalar();
        }
    }
}
