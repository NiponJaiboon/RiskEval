using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.Questionnaire
{
    public static class QuestioniareExtensions
    {
        public static Questionnaire Clone(this Questionnaire original, User user)
        {
            if (original == null)
                return null;

            Questionnaire clone = new Questionnaire
            {
                Code = original.Code,
                Description = original.Description.Clone(),
                DescriptionStyle = original.DescriptionStyle.Clone(),
                EffectivePeriod = TimeInterval.EmptyInterval,
                Reference = original.Reference,
                Remark = original.Remark,
                Title = original.Title.Clone(),
                TitleStyle = original.TitleStyle.Clone(),
                CreateAction = new UserAction(user),
            };
            clone.Questions = (QuestionGroup)original.Questions.Clone(clone);
            return clone;
        }

        public static BorderStyle Clone(this BorderStyle original)
        {
            if (original == null)
                return null;

            BorderStyle clone = new BorderStyle
            {
                BackColor = original.BackColor,
                BottomLineStyle = original.BottomLineStyle,
                ForeColor = original.ForeColor,
                Height = original.Height,
                LeftLineStyle = original.LeftLineStyle,
                RightLineStyle = original.RightLineStyle,
                TopLineStyle = original.TopLineStyle,
                Width = original.Width,
            };
            return clone;

        }

        public static LayoutStyle Clone(this LayoutStyle original)
        {
            if (original == null)
                return null;

            LayoutStyle clone = new LayoutStyle
            {
                AlignSuffixes = original.AlignSuffixes,
                AlignValues = original.AlignValues,
                AlternateVisualStyle = original.AlternateVisualStyle.Clone(),
                ColumnRightPadding = original.ColumnRightPadding,
                Columns = original.Columns,
                ControlType = original.ControlType,
                FillRightToLeft = original.FillRightToLeft,
                FillVerticalThenHorizontal = original.FillVerticalThenHorizontal,
                HorizontalAlignment = original.HorizontalAlignment,
                Indentation = original.Indentation,
                ItemNoIsVisible = original.ItemNoIsVisible,
                MainVisualStyle = original.MainVisualStyle.Clone(),
                RowsPerStyle = original.RowsPerStyle,
                StartOnTheNextRow = original.StartOnTheNextRow,
                TextAlignment = original.TextAlignment,
                TitleWidth = original.TitleWidth,
                VerticalAlignment = original.VerticalAlignment,
            };
            return clone;

        }

        public static FontStyle Clone(this FontStyle original)
        {
            if (original == null)
                return null;

            FontStyle clone = new FontStyle
            {
                Bold = original.Bold,
                Italic = original.Italic,
                Name = original.Name,
                Size = original.Size,
                SizeString = original.SizeString,
                Strikeout = original.Strikeout,
                Underline = original.Underline,
            };
            return clone;

        }

        public static LineStyle Clone(this LineStyle original)
        {
            if (original == null)
                return null;

            LineStyle clone = new LineStyle
            {
                Color = original.Color,
                Style = original.Style,
                WebControlBorderStyle = original.WebControlBorderStyle,
                Width = original.Width,
            };
            return clone;

        }

        public static VisualStyle Clone(this VisualStyle original)
        {
            if (original == null)
                return null;

            VisualStyle clone = new VisualStyle
            {
                Border = original.Border.Clone(),
                Code = original.Code,
                Description = original.Description,
                EffectivePeriod = original.EffectivePeriod.Clone(),
                Font = original.Font.Clone(),
                HorizontalAlignment = original.HorizontalAlignment,
                VerticalAlignment = original.VerticalAlignment,
            };
            return clone;

        }
    }
}
