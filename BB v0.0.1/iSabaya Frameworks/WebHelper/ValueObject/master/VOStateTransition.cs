using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject.master
{
    public class VOStateTransition
    {
        #region member
        private int stateTransitionID;
        private int fromState;
        private int toState;
        private string titleMLSID;
        private string transitionRule;
        #endregion

        #region member
        public int StateTransitionID
        {
            get { return stateTransitionID; }
            set { stateTransitionID = value; }
        }
        public int FromState
        {
            get { return fromState; }
            set { fromState = value; }
        }
        public int ToState
        {
            get { return toState; }
            set { toState = value; }
        }
        public string TitleMLSID
        {
            get { return titleMLSID; }
            set { titleMLSID = value; }
        }
        public string TransitionRule
        {
            get { return transitionRule; }
            set { transitionRule = value; }
        }
        #endregion
    }
}
