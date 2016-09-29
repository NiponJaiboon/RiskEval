using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class LineStyle
    {
        #region persistent

        protected virtual int ColorARGB
        {
            get { return this.Color.ToArgb(); }
            set { this.Color = Color.FromArgb(value); }
        }

        public virtual BorderLineStyle Style { get; set; }

        protected virtual String WidthString
        {
            get { return this.Width.ToString(); }
            set { this.Width = Unit.Parse(value); }
        }

        #endregion persistent

        public virtual System.Web.UI.WebControls.BorderStyle WebControlBorderStyle
        {
            get { return (System.Web.UI.WebControls.BorderStyle)(int)this.Style; }
            set { this.Style = (BorderLineStyle)(int)value; }
        }
        public virtual Color Color { get; set; }
        public virtual Unit Width { get; set; }
    }
}