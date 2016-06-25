using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOOrganizationTree
    {
        public VOOrganizationTree(Party organization, int parentId)
        {
            this.organization = organization;
            this.ParentId = parentId;
        }
        
        public int ID
        {
            get { return organization.PartyID; }          
        }
        private int parentId;
        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }
        private Party organization;
        public Party Organization
        {
            get { return organization; }
            set { organization = value; }
        }
        private String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }


    }
}
