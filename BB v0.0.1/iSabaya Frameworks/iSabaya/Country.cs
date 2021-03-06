using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class Country : IEnumerable<GeographicRegion>
    {
        public Country()
        {
        }

        public virtual int ID { get; set; }
        public virtual MultilingualString Name { get; set; }
        public virtual MultilingualString AbbreviatedName { get; set; }
        public virtual MultilingualString NationalityName { get; set; }
        public virtual string Code { get; set; }
        public virtual string ISOCode2 { get; set; }
        public virtual string ISOCode3 { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string Remark { get; set; }
        public virtual MultilingualString RegionLevel1Title { get; set; }
        public virtual MultilingualString RegionLevel2Title { get; set; }
        public virtual MultilingualString RegionLevel3Title { get; set; }
        public virtual MultilingualString RegionLevel4Title { get; set; }
        /// <summary>
        /// between 0 and 4 (inclusive)
        /// </summary>
        public virtual int LevelCount { get; set; }

        public virtual Language OfficialLanguage { get; set; }

        public virtual Language AltOfficialLanguage { get; set; }

        private IList<GeographicRegion> level1Regions;
        public virtual IList<GeographicRegion> Level1Regions
        {
            get
            {
                if (null == level1Regions)
                    level1Regions = new List<GeographicRegion>();
                return level1Regions;
            }
            set
            {
                level1Regions = value;
            }
        }

        private TreeListNode level1RegionRootNode;

        public virtual TreeListNode Level1RegionRootNode
        {
            get { return level1RegionRootNode; }
            set { level1RegionRootNode = value; }
        }

        #region IEnumerable<GeographicRegion> Members

        public virtual IEnumerator<GeographicRegion> GetEnumerator()
        {
            return this.Level1Regions.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Level1Regions.GetEnumerator();
        }

        #endregion

        public static Country FindByCode(Context context, String code)
        {
            return context.PersistenceSession
                        .QueryOver<Country>()
                        .Where(c => c.ISOCode2 == code || c.ISOCode3 == code)
                        .SingleOrDefault();
        }

        public virtual void Save(Context context)
        {
            if (this.Name != null) this.Name.Persist(context);
            if (this.AbbreviatedName != null) this.AbbreviatedName.Persist(context);
            if (this.NationalityName != null) this.NationalityName.Persist(context);
            if (this.RegionLevel1Title != null) this.RegionLevel1Title.Persist(context);
            if (this.RegionLevel2Title != null) this.RegionLevel2Title.Persist(context);
            if (this.RegionLevel3Title != null) this.RegionLevel3Title.Persist(context);
            if (this.RegionLevel4Title != null) this.RegionLevel4Title.Persist(context);
            if (this.level1RegionRootNode != null) this.level1RegionRootNode.Save(context);
            foreach (GeographicRegion r in Level1Regions)
            {
                r.Country = this;
                r.Save(context);
            }
            context.Persist(this);
        }

        public static Country Find(Context context, int id)
        {
            return context.PersistenceSession.Get<Country>(id);
        }

        public static IList<Country> List(Context context)
        {
            return context.PersistenceSession.QueryOver<Country>().List();
        }

        public virtual String ToLog()
        {

            return "";
        }

        public virtual String ToString(String languageCode)
        {
            return this.Name.ToString(languageCode);
        }

        public override String ToString()
        {
            return this.Name.ToString();
        }

    }

} // iSabaya
