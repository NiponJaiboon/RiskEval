using System;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTreeList;
using Resources;
using iSabaya;

namespace WebHelper
{
    public static class HandlerMethod
    {
        public static void gdvEffective_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                ASPxGridView gdv = (ASPxGridView)sender;
                object[] effectivePeriod = (object[])gdv.GetRowValues(e.VisibleIndex, "EffectiveFrom", "EffectiveTo");
                if (!((DateTime)effectivePeriod[0] <= DateTime.Now && (DateTime)effectivePeriod[1] >= DateTime.Now))
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        public static void gdvEffectivePeriod_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                ASPxGridView gdv = (ASPxGridView)sender;
                iSabaya.TimeInterval effectivePeriod = (iSabaya.TimeInterval)gdv.GetRowValues(e.VisibleIndex, "EffectivePeriod");
                // not effective
                if (!(effectivePeriod.IsEffectiveOn(DateTime.Now)))
                {
                    // pass
                    if (effectivePeriod.ExpiryDate < DateTime.Now)
                        e.Row.ForeColor = System.Drawing.Color.Red;
                    // future
                    else
                        e.Row.ForeColor = System.Drawing.Color.Blue;
                }
            }
        }

        public static void gdvEffective_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            ASPxGridView gdv = (ASPxGridView)sender;
            object[] effectivePeriod = (object[])gdv.GetRowValues(e.VisibleIndex, "EffectiveFrom", "EffectiveTo");
            SetGridButton(ref e, (DateTime)effectivePeriod[0], (DateTime)effectivePeriod[1]);
        }

        public static void gdvEffectivePeriod_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            ASPxGridView gdv = (ASPxGridView)sender;
            iSabaya.TimeInterval effectivePeriod = (iSabaya.TimeInterval)gdv.GetRowValues(e.VisibleIndex, "EffectivePeriod");
            SetGridButton(ref e, effectivePeriod.EffectiveDate, effectivePeriod.ExpiryDate);
        }

        public static void gdvGeneral_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            ASPxGridView gdv = (ASPxGridView)sender;
            SetGridButton(ref e);
        }

        private static void SetGridButton(ref ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.ButtonID.Contains("btnViewDetail"))
            {
                e.Image.Url = ResImageURL.Detail;
                if (string.IsNullOrEmpty(e.Text))
                    e.Text = ResGeneral.Description;
            }
            if (e.ButtonID.Contains("btnHelp"))
            {
                e.Image.Url = ResImageURL.Help;
                if (string.IsNullOrEmpty(e.Text))
                    e.Text = ResGeneral.Help;
            }
        }

        private static void SetGridButton(ref ASPxGridViewCustomButtonEventArgs e, DateTime from, DateTime to)
        {
            if (to >= DateTime.Now) // Effective now and future
            {
                if (e.ButtonID.Contains("btnExpire"))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                    e.Image.Url = ResImageURL.Expire;
                    if (string.IsNullOrEmpty(e.Text))
                        e.Text = ResGeneral.Expire;
                }
                else if (e.ButtonID.Contains("btnEdit"))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                    e.Image.Url = ResImageURL.Edit;
                    if (string.IsNullOrEmpty(e.Text))
                        e.Text = ResGeneral.Edit;
                }

                else if (e.ButtonID.Contains("btnPerson"))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                    e.Image.Url = "~/Images/led_icon/employee_account.png";
                    if (string.IsNullOrEmpty(e.Text))
                        e.Text = ResGeneral.Edit;
                }
            }
            else
            {
                if (e.ButtonID.Contains("btnExpire"))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
                else if (e.ButtonID.Contains("btnEdit"))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }

                else if (e.ButtonID.Contains("btnPerson"))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }

        private static void SetGridButton(ref ASPxGridViewCustomButtonEventArgs e, iSabaya.TimeInterval effectivePeriod)
        {
            if ((effectivePeriod.EffectiveDate <= DateTime.Now && effectivePeriod.ExpiryDate >= DateTime.Now))
            {
                if (e.ButtonID.Contains("btnEdit"))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                    e.Image.Url = ResImageURL.Edit;
                    if (string.IsNullOrEmpty(e.Text))
                        e.Text = ResGeneral.Edit;
                }
                else if (e.ButtonID.Contains("btnExpire"))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                    e.Image.Url = ResImageURL.Expire;
                    if (string.IsNullOrEmpty(e.Text))
                        e.Text = ResGeneral.Expire;
                }
                else if (e.ButtonID.Contains("btnPerson"))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                    e.Image.Url = "~/Images/led_icon/employee_account.png";
                    if (string.IsNullOrEmpty(e.Text))
                        e.Text = ResGeneral.Edit;
                }
            }
            else
            {
                if (e.ButtonID.Contains("btnEdit"))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
                else if (e.ButtonID.Contains("btnExpire"))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
                else if (e.ButtonID.Contains("btnPerson"))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }

        public static void gdvDelete_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.Url = ResImageURL.Delete;
                e.Text = ResGeneral.Delete;
            }
        }

        public static void gdvItemListControl_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightgray';this.style.cursor='pointer'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        public static void treeItemListControl_HtmlRowCreated(object sender, TreeListHtmlRowEventArgs e)
        {
            if (e.RowKind == TreeListRowKind.Data)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightgray';this.style.cursor='pointer'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }
    }
}