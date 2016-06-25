using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOCountry
    {
        private Country instance;
        public VOCountry(Country instance)
        {
            this.instance = instance;
        }

        //CountryID 
        public int CountryID
        {
            get { return instance.CountryID; }
        }
        //LevelCount 
        public int LevelCount
        {
            get { return instance.LevelCount; }
        }
        //Code2 
        public string Code2
        {
            get { return instance.Code2; }
        }
        //Code3 
        public string Code3
        {
            get { return instance.Code3; }
        }
        //Remark 
        public string Remark
        {
            get { return instance.Remark; }
        }
        //IsActive 
        public bool IsActive
        {
            get { return instance.IsActive; }
        }
        //OfficialLanguage 
        public string OfficialLanguage
        {
            get
            {
                if (instance.OfficialLanguage == null)
                    return "-";
                else
                    return instance.OfficialLanguage.ToString();
            }
        }
        //AltOfficialLanguage 
        public string AltOfficialLanguage
        {
            get
            {
                if (instance.AltOfficialLanguage == null)
                    return "-";
                else
                    return instance.AltOfficialLanguage.ToString();
            }
        }
        //Name 
        public string Name
        {
            get
            {
                if (instance.Name == null)
                    return "-";
                else
                    return instance.Name.ToString();
            }
        }
        //AbbreviatedName 
        public string AbbreviatedName
        {
            get
            {
                if (instance.AbbreviatedName == null)
                    return "-";
                else
                    return instance.AbbreviatedName.ToString();
            }
        }
        //NationalityName 
        public string NationalityName
        {
            get
            {
                if (instance.NationalityName == null)
                    return "-";
                else
                    return instance.NationalityName.ToString();
            }
        }
        //RegionLevel1Title 
        public string RegionLevel1Title
        {
            get
            {
                if (instance.RegionLevel1Title == null)
                    return "-";
                else
                    return instance.RegionLevel1Title.ToString();
            }
        }
        //RegionLevel2Title 
        public string RegionLevel2Title
        {
            get
            {
                if (instance.RegionLevel2Title == null)
                    return "-";
                else
                    return instance.RegionLevel2Title.ToString();
            }
        }
        //RegionLevel3Title 
        public string RegionLevel3Title
        {
            get
            {
                if (instance.RegionLevel3Title == null)
                    return "-";
                else
                    return instance.RegionLevel3Title.ToString();
            }
        }






    }
}
