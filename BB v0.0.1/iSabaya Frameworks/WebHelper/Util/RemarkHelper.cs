using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxEditors;

namespace WebHelper.Util
{
    public class RemarkHelper
    {
        public static string setRemark(string eventHead, string body)
        {
            return string.Format("<b>ส่งรายการ{0}</b> {1}", eventHead, body);
        }

        public static string setRemark(string eventHead, string body, string eventHead1, string body1)
        {
            return string.Format("<b>ส่งรายการ{0}</b> {1} <b>{2}</b> {3}", eventHead, body, eventHead1, body1);
        }

        public static string setMessageSucess(string mes)
        {
            return String.Format("{0} เรียบร้อยแล้ว <br/>รอการอนุมัติ", mes);
        }

        public static string setMessageFail()
        {
            return "ส่งรายการไม่สำเร็จ";
        }

        public static string setMessageFail(string mesFail)
        {
            return mesFail != "" ? mesFail : "ส่งรายการไม่สำเร็จ";
        }

        public static void hiddleFiled(ASPxTextBox txt)
        {
            txt.Enabled = false;
        }

        public static void hiddleFiled(ASPxButton bt)
        {
            bt.Enabled = false;
        }
    }
}