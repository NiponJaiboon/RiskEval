using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizPortal;
using iSabaya;

namespace WebHelper.Util
{
    public class RemarkTransaction
    {
        public static string AddMemberUser(User user)
        {
            string remark = "";
            if (user.UserGroupUsers.Count == 0)
            {
                remark = "เพิ่มผู้ใช้งาน : " + user.LoginName;
            }
            else
            {
                remark = "เพิ่มผู้ใช้งาน : " + user.LoginName + " และนำเข้ากลุ่ม : ";
                Boolean first = true;

                foreach (UserGroupUser item in user.UserGroupUsers)
                {
                    if (first)
                    {
                        remark += item.Group.Title;
                        first = false;
                    }
                    else
                        remark += ", ";
                    remark += item.Group.Title;
                }
            }
            return remark;
        }
        public static string EditMemberUser(User user)
        {
            string remark = "";
            if (user.UserGroupUsers.Count == 0)
            {
                remark = "แก้ไขข้อมูลผู้ใช้งาน : " + user.LoginName;
            }
            else
            {
                remark = "แก้ไขข้อมูลผู้ใช้งาน : " + user.LoginName + " และนำเข้ากลุ่ม : ";
                Boolean first = true;
                foreach (UserGroupUser item in user.UserGroupUsers)
                {
                    if (first && !item.ToBeDeleted)
                    {
                        remark += item.Group.Title;
                        first = false;
                    }
                    else if (!first && !item.ToBeDeleted)
                    {
                        remark += ", ";
                        remark += item.Group.Title;
                    }
                }
            }
            return remark;
        }
    }
}