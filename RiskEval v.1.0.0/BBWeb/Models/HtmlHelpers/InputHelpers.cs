using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BBWeb.Models.HtmlHelpers
{
    public static class InputHelpers
    {

        public static MvcHtmlString BBButton(this HtmlHelper html, string name, object value)
        {
            StringBuilder result = new StringBuilder();
            result.Append(string.Format("<input id='{0}' value='{1}' class='btn btn-default' type='button'>", name, value));
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString BBButton(this HtmlHelper html, string name, object value, string onclick)
        {
            StringBuilder result = new StringBuilder();
            result.Append(string.Format("<input id='{0}' value='{1}' class='btn btn-default' type='button' onclick='{2}'>", name, value, onclick));
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString BBTextBox(this HtmlHelper html, string name, object value)
        {
            StringBuilder result = new StringBuilder();
            result.Append(string.Format("<input id='{0}' class='form-control'  style='width:400px' value='{1}' type='text'>", name, value));
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString BBTextArea(this HtmlHelper html, string name)
        {
            StringBuilder result = new StringBuilder();
            result.Append(string.Format("<textarea class='form-control' cols='20' rows='10' id='{0}'  style='width:400px'></textarea>", name));
            return MvcHtmlString.Create(result.ToString());
        }



        public static MvcHtmlString TextBoxNumber(this HtmlHelper html, string id)
        {
            StringBuilder result = new StringBuilder();

            result.Append(string.Format("<input id='{0}' class='numericOnly form-control'  style='width:100px' value='' type='text'>", id));


            return MvcHtmlString.Create(result.ToString());
        }




    }
}