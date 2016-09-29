using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBWeb.Models
{
    public class Contact
    {
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public IList<ContactList> ContactPersons { get; set; }
    }

    public class ContactList
    {
        public string No { get; set; }
        public string DepartmentCode { get; set; }
        public string Department { get; set; }
        public string Person { get; set; }
        public string Phone { get; set; }
    }
}