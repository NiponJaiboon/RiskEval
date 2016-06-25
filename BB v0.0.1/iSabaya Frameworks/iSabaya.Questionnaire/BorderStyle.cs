using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class BorderStyle
    {
        public BorderStyle()
        {
        }

        #region persistent

        protected virtual int BackColorARGB
        {
            get { return this.BackColor.ToArgb(); }
            set { this.BackColor = Color.FromArgb(value); }
        }

        public virtual LineStyle BottomLineStyle { get; set; }
        public virtual LineStyle LeftLineStyle { get; set; }
        public virtual LineStyle RightLineStyle { get; set; }
        public virtual LineStyle TopLineStyle { get; set; }

        protected virtual int ForeColorARGB
        {
            get { return this.ForeColor.ToArgb(); }
            set { this.ForeColor = Color.FromArgb(value); }
        }

        protected virtual String HeightString
        {
            get { return this.Height.ToString(); }
            set { this.Height = Unit.Parse(value); }
        }

        protected virtual String WidthString
        {
            get { return this.Width.ToString(); }
            set { this.Width = Unit.Parse(value); }
        }

        #endregion persistent

        public virtual Color BackColor { get; set; }
        public virtual Color ForeColor { get; set; }
        public virtual Unit Height { get; set; }
        public virtual Unit Width { get; set; }
    }
}