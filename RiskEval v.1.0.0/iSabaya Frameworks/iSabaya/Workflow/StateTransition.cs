using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya
{
	[Serializable]
	public class StateTransition
	{
		//nattapong
		private String titleDisplay;
		public virtual String TitleDisplay
		{
			get { return titleDisplay; }
			set { titleDisplay = value; }
		}

		public StateTransition()
		{

		}

		public StateTransition(int seqNo, MultilingualString title, State fromState, State toState, Rule transitionRule)
		{
			this.seqNo = seqNo;
			this.title = title;
			this.fromState = fromState;
			this.toState = toState;
			this.transitionRule = transitionRule;
		}
		
		#region persistent

		private int stateTransitionID;
		public virtual int StateTransitionID
		{
			get { return stateTransitionID; }
			set { stateTransitionID = value; }
		}

		private int seqNo;
		public virtual int SeqNo
		{
			get { return seqNo; }
			set { seqNo = value; }
		}

		private MultilingualString title;
		public virtual MultilingualString Title
		{
			get { return title; }
			set
			{
				title = value;
                this.TitleDisplay = title.ToString();
			}
		}

		private State fromState;
		public virtual State FromState
		{
			get { return fromState; }
			set { fromState = value; }
		}

		private State toState;
		public virtual State ToState
		{
			get { return toState; }
			set { toState = value; }
		}

		private Rule transitionRule;
		public virtual Rule TransitionRule
		{
			get { return transitionRule; }
			set { transitionRule = value; }
		}

		#endregion persistent

		public virtual RuleResult PreTransit(ParameterList parameters)
		{
			if (fromState != null) 
                return fromState.OnExit(parameters);
            else
                return RuleResult.Success;
		}
		
		public virtual RuleResult Transit(ParameterList parameters)
		{
            if (transitionRule != null)
                return transitionRule.Execute(this, parameters);
            else
                return RuleResult.Success;
		}

        //public virtual RuleResult PostTransit(ParameterList parameters)
        //{
        //    RuleResult result = RuleResult.Success;

        //    if (toState != null) result = toState.OnEnter(parameters);
        //    return result;
        //}

		public virtual void Save(Context context)
		{
			//if (fromState != null) fromState.Save(context);
			//if (toState != null) toState.Save(context);
			if (transitionRule != null) transitionRule.Save(context);
            if (title != null) title.Persist(context);
			context.PersistenceSession.SaveOrUpdate(this);
		}

		public static StateTransition Find(Context context, int id)
		{
			StateTransition st = (StateTransition)context.PersistenceSession.Get(typeof(StateTransition), id);
			return st;
		}

        public virtual string ToLog()
        {
            StringBuilder builder = new StringBuilder();
           
            return builder.ToString();
        }


        public static IList<StateTransition> List(Context context)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(StateTransition));
            return crit.List<StateTransition>();
        }        

    }
}
