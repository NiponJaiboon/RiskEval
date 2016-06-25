using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BBAdminWeb.Models
{
    public class GoodGovernanceViewModel
    {
        public long ID { get; set; }
        public int No { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        #region Event
        public string Event { 
            get 
            {
                return string.Format("{0} | {1}",
                    "<a href='#' onclick='editGoodGovernance(" + this.ID + ")' class='event link'>แก้ไข</a>",
                    "<a href='#' onclick='deleteGoodGovernance(" + this.ID + ")' class='event link'>ลบ</a>");
            } }
        #endregion
    }
}