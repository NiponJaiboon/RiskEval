using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BBClientWeb.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public long ID { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }
        public MinistryViewModel Ministry { get; set; }

        public string MinistryCode { get { if (this.Ministry == null) return string.Empty; return this.Ministry.Code; } }
        public string MinistryName { get { if (this.Ministry == null) return string.Empty; return this.Ministry.Name; } }

        #region Event
        public string Edit { get { return "<a href='#' onclick='edit(" + this.ID + ")' class='event link'>แก้ไข</a>"; } }
        #endregion

        public override string ToString()
        {
            return this.Name;
        }
    }
}