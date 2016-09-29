using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class FontStyle
    {
        public FontStyle()
        {
        }

        #region persistent

        public virtual bool Bold { get; set; }
        public virtual bool Italic { get; set; }
        public virtual bool Underline { get; set; }
        public virtual bool Strikeout { get; set; }
        public virtual String SizeString
        {
            get { return this.Size.ToString(); }
            set { this.Size = FontUnit.Parse(value); }
        }
        public virtual String Name { get; set; }

        #endregion persistent

        public virtual FontUnit Size { get; set; }

        public virtual void SetStyle(Style s)
        {
            s.Font.Bold = this.Bold;
            s.Font.Italic = this.Italic;
            s.Font.Strikeout = this.Strikeout;
            s.Font.Underline = this.Underline;
            if (this.Name != null)
                s.Font.Name = this.Name;
        }
    }
}
