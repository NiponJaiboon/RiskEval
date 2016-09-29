using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using imSabaya;
using imSabaya.ProvidentFundSystem;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOEmployer
    {
        private Employer instance;
        private imSabayaContext context;
        private string langCode;
        public VOEmployer(imSabayaContext context, Employer instance)
        {
            this.instance = instance;
            this.context = context;
            this.langCode = context.CurrentLanguage.Code;//default
        }
        public int EmployerOrgParentID
        {
            get
            {
                if (instance.EmployerOrg == null)
                    return 0;
                else
                    return instance.EmployerOrg.OrganizationID;
            }
        }
        public string Fullname
        {
            get { return instance.FullName; }
        }
        public int EmployerID
        {
            get { return instance.EmployerID; }
        }
        public string EmployerNo
        {
            get { return instance.EmployerNo; }
        }
        public string EmployerOrg
        {
            get
            {
                if (instance.EmployerOrg == null)
                    return "";
                else
                    return instance.EmployerOrg.ToString();
            }
        }
        public string Category
        {
            get
            {
                if (instance.Category == null)
                    return "";
                else
                    return instance.Category.ToString();
            }
        }
        public string EmployeeCategoryRoot
        {
            get
            {
                if (instance.EmployeeCategoryParent == null)
                    return "";
                else
                    return instance.EmployeeCategoryParent.ToString();
            }
        }
        public string EffectivePeriod
        {
            get
            {
                if (instance.EffectivePeriod == null)
                    return "";
                else
                    return instance.EffectivePeriod.ToString();
            }
        }
        public string Name
        {
            get
            {
                if (instance.Name == null)
                    return "";
                else
                    return instance.Name.ToString(langCode);
            }
        }
        public string MultilingualName
        {
            get
            {
                if (instance.EmployerOrg == null || instance.EmployerOrg.MultilingualName == null)
                    return "";
                else
                    return instance.EmployerOrg.MultilingualName.ToString(langCode);
            }
        }
    }
}
