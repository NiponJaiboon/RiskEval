using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BBAnalysisWeb.Models
{
    public class MinistryViewModel
    {

        public MinistryViewModel()
        {

        }
        public long ID { get; set; }
        public int No { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        #region Event
        public string Edit { get { return "<a href='#' onclick='editMinistry(" + this.ID + ")' class='event link'>แก้ไข</a>"; } }
        #endregion

        public List<DepartmentViewModel> Departments { get; set; }
    }
}