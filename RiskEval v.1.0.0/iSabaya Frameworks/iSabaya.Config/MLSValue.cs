using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace iSabaya
{
    [Serializable]
    public class MLSValue
    {
        public MLSValue()
        {
        }

        public MLSValue(String languageCode, String stringValue)
        {
            this.LanguageCode = languageCode;
            this.Value = stringValue;
        }

        public MLSValue(MultilingualString owner, MLSValue value)
        {
            this.Owner = owner;
            this.LanguageCode = value.LanguageCode;
            this.Value = value.Value;
            this.UpdatedTS = DateTime.Now;
        }

        public MLSValue(MultilingualString owner, string languageCode, string value)
        {
            this.LanguageCode = languageCode;
            this.Owner = owner;
            this.Value = value;
            this.UpdatedTS = DateTime.Now;
        }

        #region persistent

        [XmlIgnore]
        public virtual long ID { get; set; }

        [XmlIgnore]
        public virtual MultilingualString Owner { get; set; }

        public virtual String LanguageCode { get; set; }

        public virtual String Value { get; set; }

        protected DateTime updatedTS = DateTime.Now;
        [XmlIgnore]
        public virtual DateTime UpdatedTS
        {
            get { return updatedTS; }
            set { updatedTS = value; }
        }

        #endregion persistent

        //Used by operator of MultilingualString
        [XmlIgnore]
        private bool matched;
        [XmlIgnore]
        public virtual bool Matched
        {
            get { return matched; }
            set { matched = value; }
        }
    }
}
