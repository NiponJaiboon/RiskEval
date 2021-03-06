using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace iSabaya
{
    //public enum MLSCategory
    //{
    //    Undefined,
    //    Model,
    //    Controller,
    //    View,
    //    Report,
    //    Config,
    //    Master,
    //    Transaction,
    //}

    [Serializable]
    public class MultilingualString : IComparable, IComparable<MultilingualString>
    {
        #region constructors

        public MultilingualString()
        {
            this.code = "";
        }

        public MultilingualString(MultilingualString original)
        {
            this.code = original.code;
            this.description = original.description;
            foreach (MLSValue v in original.Values)
            {
                this.Values.Add(new MLSValue(this, v));
            }
        }

        public MultilingualString(string language, String value)
        {
            this.code = "";
            this.Values.Add(new MLSValue(this, language, value));
        }

        /// <summary>
        /// </summary>
        /// <param name="languageCodeValuePairs">array of pairs of LanguageCode and StringValue </param>
        public MultilingualString(params String[] languageCodeValuePairs)
        {
            if (null == languageCodeValuePairs)
                return;

            if (1 == languageCodeValuePairs.Length % 2)
                throw new iSabayaException("The number elememnts in Language code-value array is not an even number.");

            this.code = "";
            for (int i = 0; i < languageCodeValuePairs.Length; i += 2)
            {
                this.Values.Add(new MLSValue(languageCodeValuePairs[i], languageCodeValuePairs[i + 1]));
            }
        }

        public MultilingualString(MLSValue mlsValue)
        {
            this.code = "";
            if (values == null)
            {
                values = new List<MLSValue>();
            }
            values.Add(mlsValue);
        }

        public MultilingualString(Dictionary<String, String> dic)
        {
            this.code = "";
            this.Values = new List<MLSValue>();

            DateTime updatedTS = DateTime.Now;
            foreach (string lang in dic.Keys)
            {
                MLSValue valueTH = new MLSValue(this, lang, dic[lang]);
                this.Values.Add(valueTH);
            }
        }

        public MultilingualString(String code, string language, String value)
        {
            this.code = code;
            this.Values.Add(new MLSValue(this, language, value));
        }

        public MultilingualString(String code, String[] languageCodeValuePairs)
        {
            this.code = code;
            for (int i = 0; i < languageCodeValuePairs.Length; i += 2)
            {
                this.Values.Add(new MLSValue(languageCodeValuePairs[i], languageCodeValuePairs[i + 1]));
            }
        }

        public MultilingualString(String code, MLSValue mlsValue)
        {
            this.code = code;
            if (values == null)
            {
                values = new List<MLSValue>();
            }
            values.Add(mlsValue);
        }

        public MultilingualString(String code, Dictionary<string, String> dic)
        {
            this.Code = code;
            this.Values = new List<MLSValue>();

            DateTime updatedTS = DateTime.Now;
            foreach (string lang in dic.Keys)
            {
                MLSValue valueTH = new MLSValue(this, lang, dic[lang]);
                this.Values.Add(valueTH);
            }
        }

        #endregion

        #region persistent

        protected long mlsID;

        public virtual long MLSID
        {
            get { return mlsID; }
            set { mlsID = value; }
        }

        [XmlIgnore]
        protected string code;

        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        [XmlIgnore]
        protected string description;

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        [XmlIgnore]
        protected IList<MLSValue> values;
        [XmlIgnore]
        public virtual IList<MLSValue> Values
        {
            get
            {
                if (values == null) values = new List<MLSValue>();
                return values;
            }
            set
            {
                values = value;
                if (null == this.values) return;
                foreach (MLSValue v in this.values)
                {
                    v.Owner = this;
                }
            }
        }

        #endregion persistent

        public virtual MLSValue[] SerializableValues
        {
            get
            {
                return this.Values.ToArray();
            }
            set
            {
                this.Values = new List<MLSValue>(value);
            }
        }

        #region operations

        [XmlIgnore]
        public virtual string this[string languageCode]
        {
            get
            {
                foreach (MLSValue v in this.Values)
                {
                    if (v.LanguageCode == languageCode)
                        return v.Value;
                }
                return null;
            }
            set
            {
                foreach (MLSValue v in this.Values)
                {
                    if (v.LanguageCode == languageCode)
                    {
                        v.Value = value;
                        return;
                    }
                }
                this.Values.Add(new MLSValue(this, languageCode, value));
            }
        }

        //[XmlIgnore]
        //public virtual Context Context { get; set; }

        public virtual void AddOrReplace(MLSValue mlsValue)
        {
            foreach (MLSValue v in values)
            {
                if (v.LanguageCode == mlsValue.LanguageCode)
                {
                    v.Value = mlsValue.Value;
                    return;
                }
            }
            Values.Add(mlsValue);
        }

        public virtual void AddOrReplace(string languageCode, String value)
        {
            foreach (MLSValue v in values)
            {
                if (v.LanguageCode == languageCode)
                {
                    v.Value = value;
                    return;
                }
            }
            Values.Add(new MLSValue(this, languageCode, value));
        }

        public override String ToString()
        {
            //StringBuilder sb = new StringBuilder();
            //if (!String.IsNullOrEmpty(this.Code))
            //    sb.Append(this.Code);
            //return sb.ToString();

            //0 = en-US, 1 = th-TH
            if (!String.IsNullOrEmpty(this.GetValue("th-TH")))
            {
                return this.GetValue("th-TH");
            }
            else
            {
                return this.GetValue("en-US");
            }
            //return this.Values.Count == 2 ? this.Values[1].Value : this.Values.Count == 0 ? null : this.Values[0].Value;

            //foreach (MLSValue v in this.Values)
            //{
            //    if (null == v.Language || v.Language.SeqNo == 0)
            //        sb.Append(" " + v.Value);
            //}
            //if (null == this.Context)
            //{
            //    if (null != Configuration.CurrentConfiguration && null != Configuration.CurrentConfiguration.DefaultLanguage)
            //    {
            //        string defaultLanguageCode = Configuration.CurrentConfiguration.DefaultLanguage.Code;
            //        foreach (MLSValue v in Values)
            //        {
            //            if (null == v.Language || v.Language.Code == defaultLanguageCode)
            //                return v.Value;
            //        }
            //    }
            //    else
            //    {
            //        foreach (MLSValue v in Values)
            //        {
            //            if (null == v.Language || v.Language.SeqNo == 0)
            //                return v.Value;
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (MLSValue v in Values)
            //    {
            //        if (null == v.Language || v.Language == this.Context.CurrentLanguage)
            //            return v.Value;
            //    }
            //}
        }

        public virtual String ToString(String langCode)
        {
            foreach (MLSValue v in Values)
            {
                if (v.LanguageCode == langCode)
                    return v.Value;
            }
            return "";
        }

        public virtual String GetValue()
        {
            return this.ToString();
        }

        public virtual String GetValue(String langCode)
        {
            return this.ToString(langCode);
        }

        public virtual void UpdateData(MultilingualString newValue)
        {
            DateTime now = DateTime.Now;
            foreach (MLSValue mlsValue in Values)
            {
                String code = mlsValue.LanguageCode;
                mlsValue.Value = newValue.GetValue(code);
                mlsValue.UpdatedTS = now;
            }
        }
        public virtual void ChangeValueTo(MultilingualString newValue)
        {
            foreach (MLSValue val in Values)
            {
                foreach (MLSValue nval in newValue.Values)
                {
                    if (val.LanguageCode == nval.LanguageCode)
                    {
                        val.Value = nval.Value;
                    }
                }
            }
        }

        public virtual void Append(MultilingualString mls)
        {
            if (mls == null) return;

            foreach (MLSValue lv in this.Values)
            {
                foreach (MLSValue rv in mls.Values)
                {
                    if (lv.LanguageCode == rv.LanguageCode)
                    {
                        lv.Value += rv.Value;
                        rv.Matched = true;
                        break;
                    }
                }
            }

            //check for values of the languages not found in this
            //xxx
            foreach (MLSValue rv in mls.Values)
            {
                if (rv.Matched)
                    rv.Matched = false;
                else
                    this.Values.Add(new MLSValue(this, rv));
            }
        }

        public virtual void Append(String s)
        {
            foreach (MLSValue lv in this.Values)
            {
                lv.Value += s;
            }
        }

        public virtual String ToLog()
        {
            throw new iSabayaException("The method or operation is not implemented.");
        }

        public virtual bool IsMatched(String p)
        {
            foreach (MLSValue v in this.Values)
            {
                if (p == v.Value) return true;
            }
            return false;
        }

        #endregion operations

        #region static

        public static MultilingualString CreateMLS(String[] languageCodes, String[] texts)
        {
            MultilingualString mls = new MultilingualString();
            for (int i = 0; i < languageCodes.Length; i++)
            {
                MLSValue mlsValue = new MLSValue(mls, languageCodes[i], texts[i]);
                mls.Values.Add(mlsValue);
            }
            return mls;
        }

        public static MultilingualString operator +(MultilingualString left, MultilingualString right)
        {
            if (left == null)
                if (right == null)
                    return null;
                else
                    return right.Clone();
            else
                if (right == null)
                    return left.Clone();

            MultilingualString result = new MultilingualString();
            bool noMatch = true;
            foreach (MLSValue lv in left.Values)
            {
                foreach (MLSValue rv in right.Values)
                {
                    if (lv.LanguageCode == rv.LanguageCode)
                    {
                        result.Values.Add(new MLSValue(result, lv.LanguageCode, lv.Value + rv.Value));
                        rv.Matched = true;
                        noMatch = false;
                        break;
                    }
                }
                if (noMatch) //language in left that is not found in right
                {
                    result.Values.Add(new MLSValue(result, lv));
                    noMatch = true;
                }
            }

            //check for values in right but not in left
            foreach (MLSValue rv in right.Values)
            {
                if (rv.Matched)
                    rv.Matched = false;
                else
                    result.Values.Add(new MLSValue(result, rv));
            }
            return result;
        }

        public static MultilingualString operator +(MultilingualString left, string right)
        {
            if (left == null) return null;

            MultilingualString result = new MultilingualString();
            foreach (MLSValue v in left.Values)
            {
                result.Values.Add(new MLSValue(result, v.LanguageCode, v.Value + right));
            }
            return result;
        }

        public static MultilingualString operator +(string left, MultilingualString right)
        {
            if (right == null) return null;

            MultilingualString result = new MultilingualString();
            foreach (MLSValue v in right.Values)
            {
                result.Values.Add(new MLSValue(result, v.LanguageCode, left + v.Value));
            }
            return result;
        }

        public static MultilingualString Clone(MultilingualString original)
        {
            if (null == original)
                return null;
            else
                return new MultilingualString(original);
        }

        #endregion static

        #region IComparable<TimeInterval> Members

        public virtual int CompareTo(MultilingualString other)
        {
            if (Object.ReferenceEquals(null, other))
                return 1;
            if (Object.ReferenceEquals(this, other))
                return 0;
            return String.Compare(this.ToString(), other.ToString());
        }

        #endregion IComparable<TimeInterval> Members

        #region IComparable Members

        public virtual int CompareTo(object obj)
        {
            return String.Compare(this.ToString(), ((MultilingualString)obj).ToString());
        }

        #endregion IComparable Members

        public static bool IsNullOrEmpty(MultilingualString mls)
        {
            if (null == mls || 0 == mls.Values.Count)
                return true;

            bool isEmpty = true;
            foreach (MLSValue v in mls.Values)
            {
                isEmpty &= (null == v || String.IsNullOrEmpty(v.Value));
            }
            return isEmpty;
        }
    }
}