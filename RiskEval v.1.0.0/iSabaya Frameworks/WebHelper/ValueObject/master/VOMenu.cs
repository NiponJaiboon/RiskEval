using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOMenu
    {
        #region member
        private int menuId;
        private string code;
        private string linkURL;
        private int parentId;
        private bool isTop;
        private string pageCode;
		private int systemID;
        #endregion

        #region Property
		public int SystemID
		{
			get { return systemID; }
			set { systemID = value; }
		}

        public int MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string LinkURL
        {
            get { return linkURL; }
            set { linkURL = value; }
        }

        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        public bool IsTop
        {
            get { return isTop; }
            set { isTop = value; }
        }

        public string PageCode
        {
            get { return pageCode; }
            set { pageCode = value; }
        }

        public String PrimaryKeyField
        {
            get { return MenuId.ToString(); }
        }
        #endregion

        #region Static Method
        public static List<VOMenu> ListByParentId(string xConnectionString, int xParentId)
        {
            //return value
            List<VOMenu> list = new List<VOMenu>();

            try
            {
                using (SqlConnection objConn = new SqlConnection(xConnectionString))
                {
                    //Open DataBase
                    objConn.Open();

                    //ถ่ายทอด Command Class มาจาก Connection ที่เปิดไว้
                    SqlCommand objComm = objConn.CreateCommand();
                    if (xParentId == 0)
                    {
                        objComm.CommandText = @"    select MenuId, Code, ParentId, LinkURL, PageCode
                                                from menu
                                                where ParentId is null";
                    }
                    else
                    {
                        objComm.CommandText = @"    select MenuId, Code, ParentId, LinkURL, PageCode
                                                from menu
                                                where ParentId = @ParentId
                                                and MenuId not in (select menuid from view_rolemenu where parentid = @ParentId)";
                        objComm.Parameters.Add("@ParentId", SqlDbType.Int).Value = xParentId;
                    }
                    SqlDataReader objReader = objComm.ExecuteReader();

                    while (objReader.Read())
                    {
                        VOMenu objRoleMenu = new VOMenu();

                        objRoleMenu.MenuId = (int)objReader["MenuId"];
                        objRoleMenu.Code = (string)objReader["Code"];
                        objRoleMenu.ParentId = objReader["ParentId"] == DBNull.Value ? 0 : (int)objReader["ParentId"];
                        objRoleMenu.LinkURL = (string)objReader["LinkURL"];
                        objRoleMenu.PageCode = objReader["PageCode"] == DBNull.Value ? "" : (string)objReader["PageCode"];
                        list.Add(objRoleMenu);
                    }
                }
            }
            catch (Exception)
            {

            }
            return list;
        }

        public static List<VOMenu> ListByParentId(string xConnectionString, int xParentId, int xRoleId)
        {
            //return value
            List<VOMenu> list = new List<VOMenu>();

            try
            {
                using (SqlConnection objConn = new SqlConnection(xConnectionString))
                {
                    //Open DataBase
                    objConn.Open();

                    //ถ่ายทอด Command Class มาจาก Connection ที่เปิดไว้
                    SqlCommand objComm = objConn.CreateCommand();
                    if (xParentId == 0)
                    {
                        objComm.CommandText = @"    select MenuId, Code, ParentId, LinkURL, PageCode
                                                from menu
                                                where ParentId is null";
                    }
                    else
                    {
                        objComm.CommandText = @"    select MenuId, Code, ParentId, LinkURL, PageCode
                                                from menu
                                                where ParentId = @ParentId
                                                and MenuId not in (select menuid from view_rolemenu where roleId = @roleId and parentid = @ParentId)";
                        objComm.Parameters.Add("@roleId", SqlDbType.Int).Value = xRoleId;
                        objComm.Parameters.Add("@ParentId", SqlDbType.Int).Value = xParentId;
                    }
                    SqlDataReader objReader = objComm.ExecuteReader();

                    while (objReader.Read())
                    {
                        VOMenu objRoleMenu = new VOMenu();

                        objRoleMenu.MenuId = (int)objReader["MenuId"];
                        objRoleMenu.Code = (string)objReader["Code"];
                        objRoleMenu.ParentId = objReader["ParentId"] == DBNull.Value ? 0 : (int)objReader["ParentId"];
                        objRoleMenu.LinkURL = (string)objReader["LinkURL"];
                        objRoleMenu.PageCode = objReader["PageCode"] == DBNull.Value ? "" : (string)objReader["PageCode"];
                        list.Add(objRoleMenu);
                    }
                }
            }
            catch (Exception)
            {

            }
            return list;
        }
        #endregion

        public int Remove(string xConnectionString, int xMenuId)
        {
            try
            {
                using (SqlConnection objConn = new SqlConnection(xConnectionString))
                {
                    objConn.Open();

                    SqlCommand objComm = objConn.CreateCommand();
                    objComm.CommandText = @"    DELETE FROM Menu 
                                                WHERE   haveChild = 0
                                                AND  (MenuId = @MenuId)";
                    objComm.Parameters.Add("@MenuId", SqlDbType.Int).Value = xMenuId;
                    objComm.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return 0;   //not remove
            }
            return 1;       //success remove
        }

        public bool IsHaveChildNode(string xConnectionString)
        {
            bool returnValue = false;
            try
            {
                using (SqlConnection objConn = new SqlConnection(xConnectionString))
                {
                    DataSet ds = new DataSet();



                    objConn.Open();

                    SqlCommand objComm = objConn.CreateCommand();
                    objComm.CommandText = @"    SELECT  *
                                                FROM    Menu
                                                WHERE   haveChild = 0
                                                AND     (MenuId = @MenuId)";
                    objComm.Parameters.Add("@MenuId", SqlDbType.Int).Value = this.MenuId;
                    objComm.ExecuteNonQuery();

                    SqlDataAdapter da = new SqlDataAdapter(objComm);
                    da.Fill(ds, "0");
                    if (ds.Tables["0"].Rows.Count > 0)
                    {
                        returnValue = true;
                    }
                }
            }
            catch (Exception)
            {
                returnValue = false;
            }

            return returnValue;
        }

        public int GetParentId(string xConnectionString)
        {
            int valueReturn = 0;
            try
            {
                using (SqlConnection objConn = new SqlConnection(xConnectionString))
                {
                    //open DataBase
                    objConn.Open();

                    //ถ่ายทอด Command Class มาจาก Connection ที่เปิดไว้
                    SqlCommand objComm = objConn.CreateCommand();


                    objComm.CommandText = @"    select *
                                                from menu
                                                where	menuId = @menuId";
                    objComm.Parameters.Add("@menuId", SqlDbType.Int).Value = this.MenuId;

                    SqlDataReader objReader = objComm.ExecuteReader();

                    VOMenu objMenu = new VOMenu();

                    while (objReader.Read())
                    {
                        objMenu.MenuId = (int)objReader["MenuId"];
                        objMenu.Code = (string)objReader["Code"];
                        objMenu.LinkURL = (string)objReader["LinkURL"];
                        objMenu.ParentId = objReader["ParentId"] == DBNull.Value ? 0 : (int)objReader["ParentId"];
                        objMenu.IsTop = (bool)objReader["IsTop"];
                        objMenu.PageCode = objReader["PageCode"] == DBNull.Value ? "" : (string)objReader["PageCode"];
                    }

                    valueReturn = objMenu.ParentId == 0 ? objMenu.MenuId : objMenu.ParentId;
                }
            }
            catch (Exception)
            {
                valueReturn = 0;
            }
            return valueReturn;
        }
    }
}
