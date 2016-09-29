using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BBWeb.Models
{
    public class AnnounceViewModel
    {
        public long Id { get; set; }
        public int No { get; set; }

        public string HeadLine { get; set; }

        public string Content { get; set; }

        #region Event
        public virtual string Edit
        {
            get { return "<a href='#' onclick='editAnnounce(" + this.Id + ")' class='event link'>แก้ไข</a>"; }
        }

        public virtual string Delete
        {
            get { return "<a href='#' onclick='deleteAnnounce(" + this.Id + ")' class='event link'>ลบ</a>"; }
        }

        public virtual string Event { get { return string.Format("{0} | {1}", this.Edit, this.Delete); } }
        #endregion
    }
}