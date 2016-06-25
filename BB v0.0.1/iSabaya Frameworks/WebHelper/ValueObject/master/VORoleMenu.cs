using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using iSabaya;
using NHibernate;
using imSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VORoleMenu
    {
        private RoleMenu instance;

        public VORoleMenu()
        {
        }

        public VORoleMenu(RoleMenu instance)
        {
            this.instance = instance;
        }

        public bool CanDisplay
        {
            get { return this.instance.CanDisplay; }
            set { this.instance.CanDisplay = value; }
        }

        public bool CanAddData
        {
            get { return this.instance.CanAddData; }
            set { this.instance.CanAddData = value; }
        }

        public bool CanChangeData
        {
            get { return this.instance.CanChangeData; }
            set { this.instance.CanChangeData = value; }
        }

        public bool CanDeleteData
        {
            get { return this.instance.CanDeleteData; }
            set { this.instance.CanDeleteData = value; }
        }

        public bool CanPrintData
        {
            get { return this.instance.CanPrintData; }
            set { this.instance.CanPrintData = value; }
        }

        public int RoleId
        {
            get { return this.instance.Role.Id; }
            set { this.instance.Role.Id = value; }
        }

        public string Description
        {
            get { return this.instance.Role.Description; }
            set { this.instance.Role.Description = value; }
        }

        public int MenuId
        {
            get { return this.instance.Menu.Id; }
            set { this.instance.Menu.Id = value; }
        }

        public string Code
        {
            get { return this.instance.Menu.Code; }
            set { this.instance.Menu.Code = value; }
        }
        public int ParentId
        {
            get { return this.instance.Menu.Parent.Id; }
            set { this.instance.Menu.Parent.Id = value; }
        }

        public string LinkURL
        {
            get { return this.instance.Menu.LinkURL; }
            set { this.instance.Menu.LinkURL = value; }
        }

        public string PageCode
        {
            get { return this.instance.Menu.PageCode; }
            set { this.instance.Menu.PageCode = value; }
        }

        private bool isDeleted = false;

        #region Static Method
        public static IList<VORoleMenu> List(imSabayaContext context, int roleId)
        {
            //return value
            List<VORoleMenu> list = new List<VORoleMenu>();
            foreach (RoleMenu rm in RoleMenu.List(context, roleId))
            {
                VORoleMenu objRoleMenu = new VORoleMenu(rm);
                list.Add(objRoleMenu);
            }
            return list;
        }

        public static List<VORoleMenu> List(imSabayaContext context)
        {
            List<VORoleMenu> list = new List<VORoleMenu>();
            foreach (RoleMenu rm in RoleMenu.List(context))
            {
                VORoleMenu objRoleMenu = new VORoleMenu(rm);
                list.Add(objRoleMenu);
            }
            return list;
        }

        public static List<VORoleMenu> ListByParentId(imSabayaContext context, int menuParentId)
        {
            List<VORoleMenu> list = new List<VORoleMenu>();
            foreach (RoleMenu rm in RoleMenu.List(context))
            {
                if (rm.Menu.Parent.Id != menuParentId) continue;
                VORoleMenu objRoleMenu = new VORoleMenu(rm);
                list.Add(objRoleMenu);
            }
            return list;

            //            try
            //            {
            //                using (SqlConnection objConn = new SqlConnection(xConnectionString))
            //                {
            //                    //Open DataBase
            //                    objConn.Open();

            //                    //ถ่ายทอด Command Class มาจาก Connection ที่เปิดไว้
            //                    SqlCommand objComm = objConn.CreateCommand();
            //                    objComm.CommandText = @"    select MenuId, Code, ParentId, LinkURL, PageCode
            //                                                from menu
            //                                                where ParentId = @ParentId
            //                                                and MenuId not in (select menuid from view_rolemenu where parentid = @ParentId)";
            //                    objComm.Parameters.Add("@ParentId", SqlDbType.Int).Value = xParentId;
            //                    SqlDataReader objReader = objComm.ExecuteReader();

            //                    while (objReader.Read())
            //                    {
            //            VORoleMenu objRoleMenu = new VORoleMenu();

            //            objRoleMenu.MenuId = (int)objReader["MenuId"];
            //            objRoleMenu.Code = (string)objReader["Code"];
            //            objRoleMenu.ParentId = objReader["ParentId"] == DBNull.Value ? 0 : (int)objReader["ParentId"];
            //            objRoleMenu.LinkURL = (string)objReader["LinkURL"];
            //            objRoleMenu.PageCode = objReader["PageCode"] == DBNull.Value ? "" : (string)objReader["PageCode"];
            //            list.Add(objRoleMenu);
            //        }
            //    }
            //}
            //catch (Exception)
            //{

            //}
            //return list;
        }
        #endregion

        public int Remove(imSabayaContext context)
        {
            if (this.isDeleted) return 0;
            int affected = 0;
            try
            {
                IDbConnection objConn = context.PersistencySession.Connection;
                objConn.Open();
                IDbCommand objComm = objConn.CreateCommand();
                objComm.CommandText = String.Format(@"DELETE FROM RoleMenu WHERE RoleId = {0} AND MenuId = {1}",
                                                    this.RoleId, this.MenuId);
                affected = objComm.ExecuteNonQuery();
                this.isDeleted = true;
            }
            catch (Exception)
            {
                affected = 0;   //not remove
            }
            return affected;       //success remove
        }

        public int Insert(imSabayaContext context)
        {
            int affected = 0;
            try
            {
                IDbConnection objConn = context.PersistencySession.Connection;
                objConn.Open();
                IDbCommand objComm = objConn.CreateCommand();
                objComm.CommandText = String.Format(@"INSERT INTO [imSabaya].[dbo].[RoleMenu]([RoleId],[MenuId])VALUES({0},{1})",
                                                    this.RoleId, this.MenuId);
                affected = objComm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                affected = 0;   //not remove
            }
            return affected;       //success remove
        }
    }
}
