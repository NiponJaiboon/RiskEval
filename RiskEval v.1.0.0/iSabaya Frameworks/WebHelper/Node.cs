using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper
{
    [Serializable]
    public class Node
    {
        private int _value;
        private int parentNodeID;
        private String title;
        private String code;

        public Node(String title, int _value, String code, int parentNodeID)
        {
            this.title = title;
            this._value = _value;
            this.code = code;
            this.parentNodeID = parentNodeID;
        }
        public int NodeID
        {
            get { return _value; }
            set { this._value = value; }
        }
        public int ParentNodeID
        {
            get { return parentNodeID; }
            set { this.parentNodeID = value; }
        }
        public String Desc
        {
            get { return title; }
            set { this.title = value; }
        }
        private String parentDesc;
        public String ParentDesc
        {
            get { return parentDesc; }
            set { this.parentDesc = value; }
        }

        public String Code
        {
            get { return code; }
            set { this.code = value; }
        }
    }
}
