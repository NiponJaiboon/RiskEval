using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BBWeb.Models
{
    public class GoodGovernance
    {
        public int Id { get; set; }
        public int No { get; set; }

        [Required(ErrorMessage = "กรุณากรอกชื่อยุทธศาสตร์")]
        [StringLength(800, ErrorMessage = "ความยาวต้องไม่เกิน 800 ตัวอักษร")]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        #region Event
        public string Event { 
            get 
            {
                return string.Format("{0} | {1}",
                    "<a href='#' onclick='editGoodGovernance(" + this.Id + ")' class='event link'>แก้ไข</a>",
                    "<a href='#' onclick='deleteGoodGovernance(" + this.Id + ")' class='event link'>ลบ</a>");
            } }
        #endregion
    }
}