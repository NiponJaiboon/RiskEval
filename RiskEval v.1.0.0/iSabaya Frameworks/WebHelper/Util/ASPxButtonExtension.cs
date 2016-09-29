using System;
using DevExpress.Utils;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using Resources;

namespace WebHelper
{
    public static class ASPxButtonExtension
    {
        public static void SetAddButton(this ASPxButton btn)
        {
            btn.Text = ResGeneral.Add;
            btn.ImageUrl = ResImageURL.Add;
        }

        public static void SetCancelButton(this ASPxButton btn, String text = null)
        {
            if (String.IsNullOrEmpty(text))
                btn.Text = ResGeneral.Cancel;
            else
                btn.Text = text;
            btn.ImageUrl = ResImageURL.Cancel;
        }

        public static void SetCloseButton(this ASPxButton btn, String text = null)
        {
            if (String.IsNullOrEmpty(text))
                btn.Text = ResGeneral.Close;
            else
                btn.Text = text;
            btn.ImageUrl = ResImageURL.Cancel;
        }

        public static void SetClearButton(this ASPxButton btn)
        {
            btn.Text = ResGeneral.Clear;
            btn.ImageUrl = ResImageURL.Clear;
        }

        public static void SetClearButton(this ASPxButton btn, String text)
        {
            btn.Text = text;
            btn.ImageUrl = ResImageURL.Clear;
        }

        public static void SetConfirmSaveButton(this ASPxButton btn)
        {
            btn.Text = ResGeneral.ConfirmSave;
            btn.ImageUrl = ResImageURL.Accept;
        }

        public static void SetEditButton(this ASPxButton btn)
        {
            btn.Text = ResGeneral.Edit;
            btn.ImageUrl = ResImageURL.Edit;
        }

        public static void SetExpireButton(this ASPxButton btn)
        {
            btn.Text = ResGeneral.Expire;
            btn.ImageUrl = ResImageURL.Expire;
        }

        public static void SetNextButton(this ASPxButton btn)
        {
            btn.Text = ResGeneral.Next;
            btn.ImageUrl = ResImageURL.Next;
            btn.ImagePosition = DevExpress.Web.ASPxClasses.ImagePosition.Right;
        }

        public static void SetPreviousButton(this ASPxButton btn)
        {
            btn.Text = ResGeneral.Previous;
            btn.ImageUrl = ResImageURL.Previous;
        }

        public static void SetSaveButton(this ASPxButton btn)
        {
            btn.Text = ResGeneral.Save;
            btn.ImageUrl = ResImageURL.Save;
        }

        public static void SetShowButton(this ASPxButton btn)
        {
            btn.Text = ResGeneral.Show;
            btn.ImageUrl = ResImageURL.Show;
        }
    }

    public static class AspxGridButtonExtension
    {
        public static void SetEnableButton(this ASPxGridViewCustomButtonEventArgs btn, DefaultBoolean visible = DefaultBoolean.True)
        {
            btn.Visible = visible;
            btn.Image.Url = ResImageURL.Accept;
        }

        public static void SetDisableButton(this ASPxGridViewCustomButtonEventArgs btn, DefaultBoolean visible = DefaultBoolean.True)
        {
            btn.Visible = visible;
            btn.Image.Url = ResImageURL.Cross;
        }
    }
}