using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOState
    {
        #region member
        private int stateID;
        private int category;
        private int seqNo;
        private string code;
        private string language;
        private string stateTitle;
        private string onEnterRule;
        private string onExitRule;
        #endregion

        #region Property
        public int StateID
        {
            get { return stateID; }
            set { stateID = value; }
        }
        public int Category
        {
            get { return category; }
            set { category = value; }
        }
        public int SeqNo
        {
            get { return seqNo; }
            set { seqNo = value; }
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public string Language
        {
            get { return language; }
            set { language = value; }
        }
        public string StateTitle
        {
            get { return stateTitle; }
            set { stateTitle = value; }
        }
        public string OnEnterRule
        {
            get { return onEnterRule; }
            set { onEnterRule = value; }
        }
        public string OnExitRule
        {
            get { return onExitRule; }
            set { onExitRule = value; }
        }
        #endregion
    }
}
