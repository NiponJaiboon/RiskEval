using BBWeb.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BBWeb.Models
{
    public class Department
    {
        public int Id { get; set; }
        public int No { get; set; }

        [Required(ErrorMessage = "กรุณากรอกรหัสหน่วยงาน")]
        [StringLength(25, ErrorMessage = "ความยาวต้องไม่เกิน 25 ตัวอักษร")]
        public string Code { get; set; }

        [Required(ErrorMessage = "กรุณากรอกชื่อหน่วยงาน")]
        [StringLength(450, ErrorMessage = "ความยาวต้องไม่เกิน 450 ตัวอักษร")]
        public string Name { get; set; }
        public MinistryViewModel Ministry { get; set; }

        public string MinistryCode { get { if (this.Ministry == null) return string.Empty; return this.Ministry.Code; } }
        public string MinistryName { get { if (this.Ministry == null) return string.Empty; return this.Ministry.Name; } }

        #region Event
        public string Edit { get { return "<a href='#' onclick='edit(" + this.Id + ")' class='event link'>แก้ไข</a>"; } }
        #endregion

        public override string ToString()
        {
            return this.Name;
        }
    }
}