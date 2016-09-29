using BBWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace BBWeb.Controllers
{
    public class ContactsController : BaseController
    {
        public IList<User> UserSession
        {
            get { return (IList<User>)Session["UserSession"]; }
            set { Session["UserSession"] = value; }
        }

        //public IList<Models.Menu> MenuSession
        //{
        //    get { return (IList<Models.Menu>)Session["MenuSession"]; }
        //    set { Session["MenuSession"] = value; }
        //}

        // GET: Contacts
        public ActionResult Index()
        {
            //ViewBag.Menus = MenuSession;

            List<Contact> contacts = new List<Contact>();
            XmlDocument xmldoc = new XmlDocument();
            XmlNodeList xmlnode;
            FileStream fs = new FileStream(Server.MapPath("~/XML/Contacts.xml"), FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName("Contacts");
            if (xmlnode.Count > 0)
            {
                for (int i = 0; i < xmlnode[0].ChildNodes.Count; i++)
                {
                    XmlNode contactPoint = xmlnode[0].ChildNodes[i];
                    Contact newContact = new Contact()
                    {
                        Title1 = contactPoint["Title1"].InnerText,
                        Title2 = contactPoint["Title2"].InnerText,
                        ContactPersons = new List<ContactList>(),
                    };

                    for (int j = 0; j < contactPoint["ContactPersons"].ChildNodes.Count; j++)
                    {
                        XmlNode contactPerson = contactPoint["ContactPersons"].ChildNodes[j];
                        newContact.ContactPersons.Add(new ContactList() { 
                            No = contactPerson["No"].InnerText, 
                            DepartmentCode = contactPerson["DepartmentCode"].InnerText, 
                            Department = contactPerson["Department"].InnerText, 
                            Person = contactPerson["Person"].InnerText, 
                            Phone =  contactPerson["Phone"].InnerText});
                    }
                    contacts.Add(newContact);
                }
            }
            ViewBag.aaa = contacts;

            return View();
        }

        public override string TabIndex
        {
            get { return "10"; }
        }

        public override int pageID
        {
            get { throw new NotImplementedException(); }
        }
    }
}