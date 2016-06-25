using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace iSabaya.Questionnaire
{
    public enum ControlType
    {
        /// <summary>
        /// Default control for single select choice is a RadioButton,
        /// and for multi-select choice, a CheckBox.
        /// For Date, a DateTimeControl.
        /// For other value types, a TextBox.
        /// </summary>
        Default,
        /// <summary>
        /// Applicable for single select choices only
        /// </summary>
        ComboBox,
    }

    /// <summary>
    /// Specifies whether the text associated with a check box or radio button control
    /// appears to the left or to the right of the control.
    /// </summary>
    public enum TextAlign
    {
        /// <summary>
        /// Text associated with a check box or radio button control appears to the left
        /// of the control.
        /// </summary>
        Left = 1,
        /// <summary>
        /// Text associated with a check box or radio button control appears to the right
        /// of the control.
        /// </summary>
        Right = 2,
    }

    [Serializable]
    public class LayoutStyle
    {
        public LayoutStyle()
        {
            Columns = 1;
            TextAlignment = TextAlign.Right;
            HorizontalAlignment = HorizontalAlign.Left;
            VerticalAlignment = VerticalAlign.Top;
            ColumnRightPadding = 10;
            AlignValues = true;
            AlignSuffixes = true;
        }

        #region persistent

        /// <summary>
        /// True indicates that the suffixes of member questions in the same column will be aligned.
        /// </summary>
        public virtual bool AlignSuffixes { get; set; }
        /// <summary>
        /// True indicates that the values of member questions in the same column will be aligned.
        /// </summary>
        public virtual bool AlignValues { get; set; }
        /// <summary>
        /// Applicable only if RowsPerStyle is more than 0.
        /// </summary>
        public virtual VisualStyle AlternateVisualStyle { get; set; }
        /// <summary>
        /// In pixels.  Applicable when Columns > 1.
        /// </summary>
        public virtual int ColumnRightPadding { get; set; }
        public virtual int Columns { get; set; }
        public virtual ControlType ControlType { get; set; }
        /// <summary>
        /// Applied when Columns > 1.
        /// True implies fill choices or questions vertically then horizontally.
        /// Fill vice versa if false.  Used in conjunction with FillRightToLeft.
        /// </summary>
        public virtual bool FillVerticalThenHorizontal { get; set; }
        /// <summary>
        /// Applied when Columns > 1.
        /// True implies fill choices or questions from right to left.
        /// Fill vice versa if false.  Used in conjunction with FillVerticalThenHorizontal.
        /// </summary>
        public virtual bool FillRightToLeft { get; set; }
        public virtual HorizontalAlign HorizontalAlignment { get; set; }
        /// <summary>
        /// In pixels. 
        /// </summary>
        public virtual int Indentation { get; set; }
        public virtual bool ItemNoIsVisible { get; set; }
        public virtual VisualStyle MainVisualStyle { get; set; }
        /// <summary>
        /// If RowsPerStyle is less than 1 then only MainVisualStyle is applicable.
        /// Otherwise, style of the row will continue for RowsPerStyle rows before the style is alternated.
        /// </summary>
        public virtual int RowsPerStyle { get; set; }
        /// <summary>
        /// True implies the control accepting user's response start on the row after the title row.
        /// Applicable when 
        /// </summary>
        public virtual bool StartOnTheNextRow { get; set; }
        /// <summary>
        /// Specifies whether the text associated with a check box or radio button control 
        /// appears to the left or to the right of the control.
        /// </summary>
        public virtual TextAlign TextAlignment { get; set; }
        /// <summary>
        /// In pixels. 
        /// </summary>
        public virtual int TitleWidth { get; set; }
        public virtual VerticalAlign VerticalAlignment { get; set; }

        #endregion persistent


        internal void Persist(Context context)
        {
            if (null != this.AlternateVisualStyle && this.AlternateVisualStyle.ID == 0)
                this.AlternateVisualStyle.Persist(context);
            if (null != this.MainVisualStyle && this.MainVisualStyle.ID == 0)
                this.MainVisualStyle.Persist(context);
        }
    }
}
