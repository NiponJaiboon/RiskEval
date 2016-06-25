using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOOrgTree
    {
        #region member
        private Organization organization;
        private OrgUnit orgUnit;
        private bool isOrganization;
        #endregion
        public VOOrgTree(Organization organization)
        {
            this.organization = organization;
            this.isOrganization = true;
        }
        public VOOrgTree(OrgUnit orgUnit)
        {
            this.orgUnit = orgUnit;
            this.isOrganization = false;
        }
        #region Property
        public int ID
        {
            get
            {
                if (isOrganization) { return organization.OrganizationID; }
                else { return orgUnit.OrgUnitID; }
            }

        }
        
        public int OrgParentID
        {
            get
            {
                if (isOrganization) { return 0; }
                else { return orgUnit.OrganizationParent.OrganizationID; }
            }

        }
        public string ParentCode
        {
            get
            {
                if (isOrganization) { return ""; }
                else { return orgUnit.OrganizationParent.Code; }
            }
        }
        public String Code
        {
            get
            {
                if (isOrganization) { return organization.Code; }
                else { return orgUnit.Code; }
            }

        }
        public String FullName
        {
            get
            {
                if (isOrganization)
                {
                    return organization.FullName;
                }
                else
                {
                    return orgUnit.OrganizationParent.FullName + " " + orgUnit.FullName;
                }
            }
        }

        public Type Type
        {
            get
            {
                if (isOrganization) { return organization.GetType(); }
                else { return orgUnit.GetType(); }
            }
        }

        /*grid เลือกสาขาธนาคารต้องการแบ่งแยกว่าติ๊กเลือกอะไร ไม่สามารถ return Type ได้*/
        public virtual String TypeInString
        {
            get { return Type.ToString(); }
        }

        #endregion
    }
}
