using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WebHelper
{
    public class ValueTextTemplate : ITemplate
    {
        protected string value;
        protected Label label;
        public ValueTextTemplate(string value)
        {
            this.value = value;
        }
        public void InstantiateIn(Control container)
        {
            label = new Label();
            label.Text = value;
            container.Controls.Add(label);
        }

    }
}
