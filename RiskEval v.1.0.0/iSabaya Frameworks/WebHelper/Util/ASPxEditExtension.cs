using System.Drawing;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;

namespace WebHelper
{
    public static class ASPxEditExtension
    {
        private static void SetExpression(this ASPxEdit ctrlEdit, string expression, string expErrorText)
        {
            ctrlEdit.ValidationSettings.RegularExpression.ValidationExpression = expression;
            ctrlEdit.ValidationSettings.RegularExpression.ErrorText = expErrorText;
        }

        private static void SetRequired(this ASPxEdit ctrlEdit, bool isRequired, string reqErrorText)
        {
            ctrlEdit.ValidationSettings.RequiredField.IsRequired = isRequired;
            if (reqErrorText != null)
                ctrlEdit.ValidationSettings.RequiredField.ErrorText = reqErrorText;
            else
            {
                ctrlEdit.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
            }
        }

        public static void SetValidation(this ASPxEdit ctrlEdit, string validationGroup, string expression, string expErrorText)
        {
            ctrlEdit.SetValidationBase(validationGroup);
            ctrlEdit.SetExpression(expression, expErrorText);
        }

        public static void SetValidation(this ASPxEdit ctrlEdit, string validationGroup, bool isRequired, string reqErrorText,
            string expression, string expErrorText)
        {
            ctrlEdit.SetValidation(validationGroup, isRequired, reqErrorText);
            ctrlEdit.SetExpression(expression, expErrorText);
        }

        public static void SetValidation(this ASPxEdit ctrlEdit, string validationGroup, bool isRequired, string expression, string expErrorText)
        {
            ctrlEdit.SetValidation(validationGroup, isRequired);
            ctrlEdit.SetExpression(expression, expErrorText);
        }

        public static void SetValidation(this ASPxEdit ctrlEdit, string validationGroup, bool isRequired, string reqErrorText)
        {
            ctrlEdit.SetValidationBase(validationGroup);
            ctrlEdit.SetRequired(isRequired, reqErrorText);
        }

        public static void SetValidation(this ASPxEdit ctrlEdit, string validationGroup, bool isRequired)
        {
            ctrlEdit.SetValidation(validationGroup, isRequired, "กรุณากรอกข้อมูล");
        }

        public static void SetValidation(this ASPxEdit ctrlEdit, string validationGroup)
        {
            ctrlEdit.SetValidation(validationGroup, true);
        }

        public static void SetValidationNoText(this ASPxEdit ctrlEdit, string validationGroup)
        {
            ctrlEdit.SetValidationBase(validationGroup);
            ctrlEdit.SetRequired(true," "); 
        }

        public static void SetValidation_Border(this ASPxEdit ctrlEdit, string validationGroup, bool isRequired, string reqErrorText)
        {
            ctrlEdit.SetValidationBase_Border(validationGroup);
            ctrlEdit.SetRequired(isRequired, reqErrorText);
        }

        public static void SetValidation_Border(this ASPxEdit ctrlEdit, string validationGroup, bool isRequired)
        {
            ctrlEdit.SetValidation_Border(validationGroup, isRequired, null);
        }

        public static void SetValidation_Border(this ASPxEdit ctrlEdit, string validationGroup)
        {
            ctrlEdit.SetValidation_Border(validationGroup, true);
        }

        private static void SetValidationBase(this ASPxEdit ctrlEdit, string validationGroup)
        {
            ctrlEdit.ValidationSettings.ValidationGroup = validationGroup;
            ctrlEdit.ValidationSettings.SetFocusOnError = true;
            ctrlEdit.ValidationSettings.ErrorText = "ErrorText";
            ctrlEdit.ValidationSettings.ValidateOnLeave = false;
            ctrlEdit.ValidationSettings.ErrorImage.Height = Unit.Pixel(16);
            ctrlEdit.ValidationSettings.ErrorImage.Width = Unit.Pixel(16);
            ctrlEdit.ValidationSettings.ErrorImage.AlternateText = "Error";
            ctrlEdit.ValidationSettings.ErrorImage.Url = "~/Images/iconError.png";
            ctrlEdit.ValidationSettings.ErrorFrameStyle.ForeColor = System.Drawing.Color.Red;
            ctrlEdit.ValidationSettings.ErrorFrameStyle.Paddings.Padding = Unit.Pixel(0);
            ctrlEdit.ValidationSettings.ErrorFrameStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            ctrlEdit.ValidationSettings.ErrorFrameStyle.BackgroundImage.ImageUrl = "~/Images/bgError.png";
            ctrlEdit.ValidationSettings.ErrorFrameStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#FD4D3E");
            ctrlEdit.ValidationSettings.ErrorFrameStyle.Border.BorderStyle = BorderStyle.Solid;
            ctrlEdit.ValidationSettings.ErrorFrameStyle.Border.BorderWidth = Unit.Pixel(1);
            ctrlEdit.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingRight = Unit.Pixel(0);
            ctrlEdit.ValidationSettings.Display = Display.Dynamic;
        }

        private static void SetValidationBase_Border(this ASPxEdit ctrlEdit, string validationGroup)
        {
            ctrlEdit.ValidationSettings.ValidationGroup = validationGroup;
            ctrlEdit.ValidationSettings.SetFocusOnError = true;
            ctrlEdit.ValidationSettings.ErrorText = "ErrorText";
            ctrlEdit.ValidationSettings.ValidateOnLeave = false;
            ctrlEdit.ValidationSettings.Display = Display.Dynamic;
            ctrlEdit.FocusedStyle.Border.BorderColor = Color.Red;
            ctrlEdit.FocusedStyle.Border.BorderStyle = BorderStyle.Solid;
            ctrlEdit.FocusedStyle.Border.BorderWidth = Unit.Pixel(1);
            ctrlEdit.InvalidStyle.BackColor = ColorTranslator.FromHtml("#FFF5F5");
            ctrlEdit.InvalidStyle.Border.BorderColor = Color.Red;
            ctrlEdit.InvalidStyle.Border.BorderStyle = BorderStyle.Solid;
            ctrlEdit.InvalidStyle.Border.BorderWidth = Unit.Pixel(1);
        }
    }
    public static class PropertiesExtension
    {
        public static void SetValidation(this EditProperties properties, string validationGroup)
        {
            properties.ValidationSettings.ValidationGroup = validationGroup;
            properties.SetValidation();
        }
        public static void SetValidation(this EditProperties properties)
        {
            properties.ValidationSettings.SetFocusOnError = true;
            properties.ValidationSettings.ErrorText = "ErrorText";
            properties.ValidationSettings.ValidateOnLeave = false;
            properties.ValidationSettings.Display = Display.Dynamic;
            properties.FocusedStyle.Border.BorderColor = Color.Red;
            properties.FocusedStyle.Border.BorderStyle = BorderStyle.Solid;
            properties.FocusedStyle.Border.BorderWidth = Unit.Pixel(1);
            properties.InvalidStyle.BackColor = ColorTranslator.FromHtml("#FFF5F5");
            properties.InvalidStyle.Border.BorderColor = Color.Red;
            properties.InvalidStyle.Border.BorderStyle = BorderStyle.Solid;
            properties.InvalidStyle.Border.BorderWidth = Unit.Pixel(1);
            properties.SetRequired(true, "Required field");
        }
        private static void SetRequired(this EditProperties properties, bool isRequired, string reqErrorText)
        {
            properties.ValidationSettings.RequiredField.IsRequired = isRequired;
            if (reqErrorText != null)
                properties.ValidationSettings.RequiredField.ErrorText = reqErrorText;
            else
                properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
        }
    }
}