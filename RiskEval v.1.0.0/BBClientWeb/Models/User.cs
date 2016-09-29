using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBClientWeb.Models
{

    public enum Role
    {
        None,
        Admin, //เจ้าหน้าที่ดูแลระบบ
        User, //ส่วนราชการ
        Budgetor, //เจ้าหน้าที่จัดทำงบประมาณ
        Evaluator //เจ้าหน้าที่ประเมิณงบประมาณ
    }


    public class User
    {
        public int Id { get; set; }
        public int No { get; set; }
        public string IdCard { get; set; }
        public string FirstNameTh { get; set; }
        public string LastNameTh { get; set; }

        public string FirstNameEn { get; set; }
        public string LastNameEn { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public string PhoneCenter { get; set; }
        public string Mobile { get; set; }
        public string PhoneDirect { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public bool IsDelete { get; set; }
        public bool IsOnline { get; set; }
        public bool IsSelect { get; set; }


        public string MinistryCode { get; set; }
        public string MinistryName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }


        public Role Role { get; set; }
        public bool IsActive { get { return Status == "เปิดใช้งาน"; } }
        public string EventTitle { get { return IsActive ? "ปิดการใช้งาน" : "เปิดการใช้งาน"; } }

        #region Event
        public string Checkbox { get { return "<input type='checkbox' name='user' id='" + this.Id + "'/>"; } }
        public string Activate
        {
            get
            {
                if (Status == "เปิดใช้งาน")
                {
                    return "<img src='" + this.ImageStatus + "' class='icon-event' onclick='event.preventDefault();enableOrdisableUser(" + this.Id + "," + "true" + ")' title='" + this.EventTitle + " : " + this.FirstNameTh + "' />";
                }
                else
                {
                    return "<img src='" + this.ImageStatus + "' class='icon-event' onclick='event.preventDefault();enableOrdisableUser(" + this.Id + "," + "false" + ")' title='" + this.EventTitle + " : " + this.FirstNameTh + "' />";
                }
            }
        }
        public string Log
        {
            get
            {
                if (IsOnline)
                {
                    return "<img src='" + this.ImageOnline + "'class='icon-event' onclick='unLogUser(" + this.Id + ")' title='ปลดล็อคผู้ใช้งาน : " + this.FirstNameTh + "'/>";
                }
                else
                {
                    return "<img src='" + this.ImageOnline + "'/>";
                }
            }
        }

        public string Delete
        {
            get
            {
                if (IsDelete)
                {
                    return "<img src='" + this.ImageDelete + "'/>";
                }
                else
                {
                    return "<img src='" + this.ImageDelete + "' class='icon-event' onclick='deleteUser(" + this.Id + ")' title='ลบผู้ใช้งาน : " + this.FirstNameTh + "'/>";
                }
            }
        }
        public string Display
        {
            get
            {
                return "<a href='#' id='department' class='link' onclick='display(" + this.Id + ")'>" + this.IdCard + "</a>";
            }
        }

        #endregion

        #region Icon
        public string ImageStatus
        {
            get
            {
                return Status == "เปิดใช้งาน" ? "Images/icon/icon_active_on.gif" : "Images/icon/icon_active_off.gif";
            }
        }

        public string ImageDelete
        {
            get
            {
                return IsDelete == false ? "Images/icon/icon_del_off.gif" : "Images/icon/icon_del_on.gif";
            }
        }

        public string ImageOnline
        {
            get
            {
                return IsOnline == false ? "Images/icon/icon_is_online_off.gif" : "Images/icon/icon_is_online_on.gif";
            }
        }
        #endregion

        public string Ministry { get; set; }

        public string ToNumber { get; set; }
    }
}