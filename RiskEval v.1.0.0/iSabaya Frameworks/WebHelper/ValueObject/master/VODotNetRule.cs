using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject.master
{
    public class VODotNetRule
    {
        private int dotNetRuleID;
        private string description;

        public int DotNetRuleID
        {
            get { return dotNetRuleID; }
            set { dotNetRuleID = value; }
        }
        public string Description
        {
            get
            {
                if (description == null)
                {
                    return "";
                }
                return description;
            }
            set { description = value; }
        }
    }
}
