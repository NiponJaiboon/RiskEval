using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BBWeb.Models.ViewModels
{
    public class StrategicViewModel
    {
        public long ID { get; set; }
        public int No { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        #region Event
        public virtual string Edit
        {
            get
            {
                return "<a href='#' onclick='editStrategic(" + this.ID + ")' class='event link'>แก้ไข</a>";
            }
        }
        public virtual string Delete
        {
            get
            {
                return "<a href='#' onclick='deleteStrategic(" + this.ID + ")' class='event link'>ลบ</a>";
            }
        }

        public virtual string Event
        {
            get
            {
                return string.Format("{0} | {1}", this.Edit, this.Delete);
            }
        }
        #endregion
    }
}