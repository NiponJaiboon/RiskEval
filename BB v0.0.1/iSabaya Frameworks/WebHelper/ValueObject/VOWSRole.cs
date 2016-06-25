using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using imSabaya;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOWSRole
    {
        private Role instance;
        public VOWSRole(Role role)
        {
            this.instance = role;
        }

        #region property

        public int RoleId
        {
            get { return this.instance.Id; }
            set { this.instance.Id = value; }
        }

        public string Code
        {
            get { return this.instance.Code; }
            set { this.instance.Code = value; }
        }

        public string Description
        {
            get { return this.instance.Description; }
            set { this.instance.Description = value; }
        }

        public int System
        {
            get
            {
                return this.instance.ApplicationID;
            }
        }

        public int SystemID
        {
            get { return this.instance.ApplicationID; }
            set { this.instance.ApplicationID = value; }
        }

        public bool IsAdministrator
        {
            get { return this.instance.IsAdministrator; }
            set { this.instance.IsAdministrator = value; }
        }

        public bool IsBuiltin
        {
            get { return this.instance.IsBuiltin; }
            set { this.instance.IsBuiltin = value; }
        }

        #endregion

        #region static method

        public static IList<VOWSRole> List(imSabayaContext context)
        {
            IList<VOWSRole> RoleList = new List<VOWSRole>();
            foreach (Role r in Role.List(context))
            {
                VOWSRole objRole = new VOWSRole(r);
                RoleList.Add(objRole);
            }
            return RoleList;
        }

        /// <summary>
        /// Get List VOWSRole By RoleId
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="xRoleId"></param>
        /// <returns></returns>
        public static VOWSRole Get(imSabayaContext context, int roleId)
        {
            Role role = Role.Find(context, roleId);
            if (role == null) return null;
            return new VOWSRole(role);
        }

        #endregion

        public DataTable GetByRoleId(string xConnectionString, int xRoleId)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection objConn = new SqlConnection(xConnectionString))
                {
                    if (objConn.State == ConnectionState.Open)
                    {
                        objConn.Close();
                    }
                    objConn.Open();

                    using (SqlCommand objComm = new SqlCommand())
                    {
                        objComm.Connection = objConn;
                        objComm.CommandType = CommandType.Text;
                        objComm.CommandText = @"    SELECT  RoleId, Code, Description,IsAdministrator, IsBuiltin, SeqNo, SystemID
                                                FROM    Role
                                                WHERE   RoleId = @RoleId";
                        objComm.Parameters.Add("@RoleId", SqlDbType.Int).Value = xRoleId;

                        using (SqlDataAdapter da = new SqlDataAdapter(objComm))
                        {
                            da.Fill(ds, "0");
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return ds.Tables["0"];
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
                    sqlCommand.CommandText = @"INSERT INTO
													[imSabaya].[dbo].[Role]
                                                    (
														[Code],
														[Description],
														[IsAdministrator],
														[IsBuiltin],
														[SystemID]
													)
                                                     VALUES
													(
														@Code,
														@Description,
														@IsAdministrator,
														@IsBuiltin,
														@SystemID
													)";
                    sqlCommand.Parameters.Add("@Code", SqlDbType.VarChar).Value = this.Code;
                    sqlCommand.Parameters.Add("@Description", SqlDbType.VarChar).Value = this.Description;
                    sqlCommand.Parameters.Add("@IsAdministrator", SqlDbType.Bit).Value = this.IsAdministrator;
                    sqlCommand.Parameters.Add("@IsBuiltin", SqlDbType.Bit).Value = this.IsBuiltin;
                    sqlCommand.Parameters.Add("@SystemID", SqlDbType.Int).Value = this.SystemID;
                    rowAffect = sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                rowAffect = 0;
            }
            finally
            {
                message = rowAffect == 1 ? "Inserted" : "Operation Insert Not Success";
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
                    sqlCommand.CommandText = @" UPDATE
													[imSabaya].[dbo].[Role]
                                                SET
													[Code] = @Code,
													[Description] = @Description,
													[IsAdministrator] = @IsAdministrator,
													[IsBuiltin] = @IsBuiltin,
													[SystemID] = @SystemID
                                                WHERE RoleId = @RoleId";
                    sqlCommand.Parameters.Add("@RoleId", SqlDbType.Int).Value = this.RoleId;
                    sqlCommand.Parameters.Add("@Code", SqlDbType.VarChar).Value = this.Code;
                    sqlCommand.Parameters.Add("@Description", SqlDbType.VarChar).Value = this.Description;
                    sqlCommand.Parameters.Add("@IsAdministrator", SqlDbType.Bit).Value = this.IsAdministrator;
                    sqlCommand.Parameters.Add("@IsBuiltin", SqlDbType.Bit).Value = this.IsBuiltin;
                    sqlCommand.Parameters.Add("@SystemID", SqlDbType.Int).Value = this.SystemID;
                    rowAffect = sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                rowAffect = 0;
            }
            finally
            {
                message = rowAffect == 1 ? "Updated" : "Operation Update Not Success";
            }
        }

        public void Delete(string xConnectionString)
        {
            int rowAffect = 0;

            string message = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(xConnectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = connection.CreateCommand();
                    sqlCommand.CommandText = @" DELETE FROM [imSabaya].[dbo].[Role]
                                                WHERE RoleId = @RoleId";
                    sqlCommand.Parameters.Add("@RoleId", SqlDbType.Int).Value = this.RoleId;
                    rowAffect = sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                rowAffect = 0;
            }
            finally
            {
                message = rowAffect == 1 ? "Deleted" : "Operation Delete Not Success";
            }
        }
    }
}