using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using imSabaya;

namespace WebHelper.ValueObject.master
{
	//  SELECT [UserID]
	//    ,[RoleId]
	//    ,[EffectiveFrom]
	//    ,[EffectiveTo]
	//FROM [imSabaya].[dbo].[RolePeriod]

	[Serializable]
	public class VOUserRole
	{
		private int userID;
		private int roleId;
		private DateTime effectiveFrom;
		private DateTime effectiveTo;
		private string code;
		private string description;

		#region Property
		public int UserID
		{
			get { return userID; }
			set { userID = value; }
		}

		public int RoleId
		{
			get { return roleId; }
			set { roleId = value; }
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

		public string Code
		{
			get { return code; }
			set { code = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		public String PrimaryKeyField
		{
			get { return RoleId + "_" + userID; }
		}

		#endregion

		#region Static Method
		public static List<VOUserRole> List(imSabayaContext context, int userId)
		{
			List<VOUserRole> list = new List<VOUserRole>();
			try
			{
				IDbConnection objConn = context.PersistencySession.Connection;
				if (objConn.State == ConnectionState.Closed)
				{
					objConn.Open();
				}
				IDbCommand command = objConn.CreateCommand();
				command.CommandText = @"select * 
										from UserRole
										inner join role on UserRole.roleid = role.roleid
										where userId = " + userId.ToString();

				IDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					VOUserRole objVORolePeriod = new VOUserRole();

					objVORolePeriod.UserID = (int)reader["UserID"];
					objVORolePeriod.RoleId = (int)reader["RoleId"];
					objVORolePeriod.EffectiveFrom = (DateTime)reader["EffectiveFrom"];
					objVORolePeriod.EffectiveTo = (DateTime)reader["EffectiveTo"];
					objVORolePeriod.Code = (string)reader["Code"];
					objVORolePeriod.Description = (string)reader["Description"];
					list.Add(objVORolePeriod);
				}
				reader.Close();
			}
			catch (Exception)
			{

			}
			return list;
		}
		

		public void ExpirePassword(string xConnectionString)
		{
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(xConnectionString))
				{
					sqlConnection.Open();
					SqlCommand sqlCommand = sqlConnection.CreateCommand();
					sqlCommand.CommandText = @"     UPDATE    UserRole
                                                    SET              EffectiveTo = @EffectiveTo
                                                    WHERE     (UserID = @UserID) AND (RoleId = @RoleId)";
					sqlCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = this.UserID;
					sqlCommand.Parameters.Add("@RoleId", SqlDbType.Int).Value = this.RoleId;
					sqlCommand.Parameters.Add("@EffectiveTo", SqlDbType.DateTime).Value = DateTime.Today;

					int rowAffect = sqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void Insert(string xConnectionString)
		{
			int rowAffect = 0;

			string message = "";
			try
			{
				using (SqlConnection connection = new SqlConnection(xConnectionString))
				{
					connection.Open();
					SqlCommand sqlCommand = connection.CreateCommand();
					sqlCommand.CommandText = @"     INSERT INTO UserRole
                                                    (UserID, RoleId, EffectiveFrom, EffectiveTo)
                                                    VALUES     (@UserID,@RoleId,@EffectiveFrom,@EffectiveTo)";
					sqlCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = this.UserID;
					sqlCommand.Parameters.Add("@RoleId", SqlDbType.Int).Value = this.RoleId;
					sqlCommand.Parameters.Add("@EffectiveFrom", SqlDbType.DateTime).Value = this.EffectiveFrom;
					sqlCommand.Parameters.Add("@EffectiveTo", SqlDbType.DateTime).Value = this.EffectiveTo;
					rowAffect = sqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception)
			{
				rowAffect = 0;
			}
			finally
			{
				message = rowAffect == 1 ? "Inserted Success" : "Inserted Not Success";
			}
		}

		public void Update(string xConnectionString)
		{
			int rowAffect = 0;

			string message = "";
			try
			{
				using (SqlConnection connection = new SqlConnection(xConnectionString))
				{
					connection.Open();
					SqlCommand sqlCommand = connection.CreateCommand();
					sqlCommand.CommandText = @"     UPDATE      UserRole
                                                    SET         EffectiveFrom = @EffectiveFrom, EffectiveTo = @EffectiveTo
                                                    WHERE       UserID = @UserID AND RoleId = @RoleId ";
					sqlCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = this.UserID;
					sqlCommand.Parameters.Add("@RoleId", SqlDbType.Int).Value = this.RoleId;
					sqlCommand.Parameters.Add("@EffectiveFrom", SqlDbType.DateTime).Value = this.EffectiveFrom;
					sqlCommand.Parameters.Add("@EffectiveTo", SqlDbType.DateTime).Value = this.EffectiveTo;

					rowAffect = sqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception)
			{
				rowAffect = 0;
			}
			finally
			{
				message = rowAffect == 1 ? "Updated Success" : "Updated Not Success";
			}
		}

		#endregion
	}
}
