using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using imSabaya;

namespace WebHelper.ValueObject.master
{
	/*
		select passwordId, password, userId, effectiveFrom, effectiveTo from Password     
	 */
	[Serializable]
	public class VOPassword
	{
		private int passwordId;
		private String password;
		private int userId;
		private DateTime effectiveFrom;
		private DateTime effectiveTo;

		#region Property
		public int PasswordId
		{
			get { return passwordId; }
			set { passwordId = value; }
		}
		public String Password
		{
			get { return password; }
			set { password = value; }
		}

		public int UserId
		{
			get { return userId; }
			set { userId = value; }
		}

		public DateTime EffectiveFrom
		{
			get { return effectiveFrom; }
			set { effectiveFrom = value; }
		}

		public DateTime EffectiveTo
		{
			get { return effectiveTo; }
			set { effectiveTo = value; }
		}

		public String PrimaryKeyField
		{
			get { return PasswordId + "_" + UserId; }
		}
		#endregion

		#region static method
		public static List<VOPassword> List(imSabayaContext context, int userId)
		{
			List<VOPassword> list = new List<VOPassword>();

			try
			{
				IDbConnection objConn = context.PersistencySession.Connection;
				if (objConn.State == ConnectionState.Closed)
				{
					objConn.Open();
				}
				IDbCommand objComm = objConn.CreateCommand();
				objComm.CommandText = @"select passwordId, EncryptedPassword, userId, effectiveFrom, effectiveTo from Password where userId = " + userId;
				IDataReader reader = objComm.ExecuteReader();

				while (reader.Read())//all row
				{
					VOPassword vo = new VOPassword();
					vo.PasswordId = (int)reader["passwordId"];
					vo.Password = (String)reader["EncryptedPassword"];
					vo.UserId = (int)reader["userId"];
					vo.EffectiveFrom = (DateTime)reader["effectiveFrom"];
					vo.EffectiveTo = (DateTime)reader["effectiveTo"];
					list.Add(vo);
				}
				reader.Close();

			}
			catch (Exception ex)
			{
				throw ex;
			}
			return list;
		}
		#endregion

		public void Save(imSabayaContext context)
		{
			try
			{
				IDbConnection objConn = context.PersistencySession.Connection;
				if (objConn.State == ConnectionState.Closed)
				{
					objConn.Open();
				}
				IDbCommand objComm = objConn.CreateCommand();
				objComm.CommandText = @"INSERT INTO [imSabaya].[dbo].[Password]
                                           ([password]
                                           ,[UserID]
                                           ,[EffectiveFrom]
                                           ,[EffectiveTo])
                                     VALUES(";

				objComm.CommandText += "'" + this.Password + "',";
				objComm.CommandText += this.UserId + ",";
				objComm.CommandText += "'" + this.EffectiveFrom.ToString("yyyy-MM-dd HH:mm:ss") + "',";
				objComm.CommandText += "'" + this.EffectiveTo.ToString("yyyy-MM-dd HH:mm:ss") + "')";

				int rowUpdated = objComm.ExecuteNonQuery();

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void ExpirePassword(imSabayaContext context)
		{
			try
			{
				IDbConnection objConn = context.PersistencySession.Connection;
				if (objConn.State == ConnectionState.Closed)
				{
					objConn.Open();
				}
				IDbCommand objComm = objConn.CreateCommand();
				objComm.CommandText = @"     UPDATE    Password
                                                    SET       EffectiveTo = @EffectiveTo
                                                    WHERE     passwordId = @passwordId";

				SqlParameterCollection Parms = (SqlParameterCollection)objComm.Parameters;
				Parms.AddWithValue("@passwordId", this.PasswordId);
                Parms.AddWithValue("@EffectiveTo", DateTime.Today);

				int rowUpdated = objComm.ExecuteNonQuery();

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void Update(imSabayaContext context)
		{
			try
			{
				IDbConnection objConn = context.PersistencySession.Connection;
				if (objConn.State == ConnectionState.Closed)
				{
					objConn.Open();
				}
				IDbCommand objComm = objConn.CreateCommand();
				objComm.CommandText = @"     UPDATE    Password
                                                    SET              password = @password, EffectiveFrom = @EffectiveFrom, EffectiveTo = @EffectiveTo
                                                    WHERE     (passwordId = @passwordId)";

				SqlParameterCollection Parms = (SqlParameterCollection)objComm.Parameters;
                Parms.AddWithValue("@passwordId", this.PasswordId);
                Parms.AddWithValue("@password", this.Password);
                Parms.AddWithValue("@EffectiveFrom", this.EffectiveFrom);
                Parms.AddWithValue("@EffectiveTo", this.EffectiveTo);
				int rowUpdated = objComm.ExecuteNonQuery();

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
