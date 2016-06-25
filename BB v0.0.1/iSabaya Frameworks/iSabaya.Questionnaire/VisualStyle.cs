using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public enum BorderLineStyle
    {
        /// <summary>
        /// No set border style.
        /// </summary>
        NotSet = 0,
        /// <summary>
        //     No border.
        /// </summary>
        None = 1,
        /// <summary>
        /// A dotted line border.
        /// </summary>
        Dotted = 2,
        /// <summary>
        /// A dashed line border.
        /// </summary>
        Dashed = 3,
        /// <summary>
        /// A solid line border.
        /// </summary>
        Solid = 4,
        /// <summary>
        /// A double solid line border.
        /// </summary>
        Double = 5,
        /// <summary>
        /// A grooved border for a sunken border appearance.
        /// </summary>
        Groove = 6,
        /// <summary>
        /// A ridged border for a raised border appearance.
        /// </summary>
        Ridge = 7,
        /// <summary>
        /// An inset border for a sunken control appearance.
        /// </summary>
        Inset = 8,
        /// <summary>
        /// An outset border for a raised control appearance.
        /// </summary>
        Outset = 9,
    }

    [Serializable]
    public enum HorizontalAlign
    {
        /// <summary>
        /// The horizontal alignment is not set.
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// The contents of a container are left justified.
        /// </summary>
        Left = 1,
        /// <summary>
        /// The contents of a container are centered.
        /// </summary>
        Center = 2,
        /// <summary>
        /// The contents of a container are right justified.
        /// </summary>
        Right = 3,
        /// <summary>
        /// The contents of a container are uniformly spread out and aligned with both
        /// the left and right margins.
        /// </summary>
        Justify = 4,
    }

    /// <summary>
    /// Specifies the vertical alignment of an object or text in a control.
    /// </summary>
    [Serializable]
    public enum VerticalAlign
    {
        /// <summary>
        /// Vertical alignment is not set.
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Text or object is aligned with the top of the enclosing control.
        /// </summary>
        Top = 1,
        /// <summary>
        /// Text or object is aligned with the center of the enclosing control.
        /// </summary>
        Middle = 2,
        /// <summary>
        /// Text or object is aligned with the bottom of the enclosing control.
        /// </summary>
        Bottom = 3,
    }

    [Serializable]
    public class VisualStyle
    {
        private VisualStyle visualStyle;

        public VisualStyle()
        {
            HorizontalAlignment = HorizontalAlign.Left;
            VerticalAlignment = VerticalAlign.Top;
        }

        public VisualStyle(VisualStyle visualStyle)
        {
            // TODO: Complete member initialization
            this.visualStyle = visualStyle;
        }

        #region persistent

        public virtual int ID { get; set; }
        public virtual BorderStyle Border { get; set; }
        public virtual String Code { get; set; }
        public virtual String Description { get; set; }
        public virtual TimeInterval EffectivePeriod { get; set; }
        public virtual FontStyle Font { get; set; }
        public virtual HorizontalAlign HorizontalAlignment { get; set; }
        public virtual VerticalAlign VerticalAlignment { get; set; }

        #endregion persistent

        public virtual void SetStyle(Style s)
        {
            if (null != this.Font && null != s.Font)
            {
                s.Font.Bold = this.Font.Bold;
                s.Font.Italic = this.Font.Italic;
                if (this.Font.Name != null)
                    s.Font.Name = this.Font.Name;
                s.Font.Size = this.Font.Size;
                s.Font.Strikeout = this.Font.Strikeout;
                s.Font.Underline = this.Font.Underline;
            }

            if (null != this.Border)
            {
                s.BackColor = this.Border.BackColor;
                s.ForeColor = this.Border.ForeColor;
                //s.BorderColor = this.Border.BorderColor;
                //s.BorderStyle = (System.Web.UI.WebControls.BorderStyle)(int)this.Border.Style;
                //s.BorderWidth = this.Border.BorderWidth;
                s.Height = this.Border.Height;
                s.Width = this.Border.Width;
            }
        }

        public virtual void Persist(Context context)
        {
            context.PersistenceSession.SaveOrUpdate(this);
        }

        public static VisualStyle Find(Context context, string code)
        {
            return context.PersistenceSession.CreateCriteria<VisualStyle>()
                            .Add(Expression.Eq("Code", code))
                            .UniqueResult<VisualStyle>();
        }
    }
}
