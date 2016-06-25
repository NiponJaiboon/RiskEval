using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
	[Serializable]
	public class State
	{

		public State()
		{
		}

		public State(StatefulEntityType owner, int category, int seqNo, string code, MultilingualString title,
					bool isInitial, bool isFinal, Rule onEnterRule, Rule onExitRule)
		{
			this.owner = owner;
			this.category = category;
			this.seqNo = seqNo;
			this.code = code;
			this.title = title;
			this.isInitialState = isInitial;
			this.isFinalState = isFinal;
			this.onEnterRule = onEnterRule;
            this.onExitRule = onExitRule;
			this.updatedTS = DateTime.Now;
		}

        public State(StatefulEntityType owner, TreeListNode category, int seqNo, string code, MultilingualString title,
                    bool isInitial, bool isFinal, Rule onEnterRule, Rule onExitRule, User user)
        {
            this.owner = owner;
            this.CategoryNode = category;
            this.seqNo = seqNo;
            this.code = code;
            this.title = title;
            this.isInitialState = isInitial;
            this.isFinalState = isFinal;
            this.onEnterRule = onEnterRule;
            this.onExitRule = onExitRule;
            this.updatedTS = DateTime.Now;
            this.updatedBy = user;
        }

		#region persistent

		private int stateID;
		public virtual int StateID
		{
			get { return stateID; }
			set { stateID = value; }
		}

        public virtual TreeListNode CategoryNode { get; set; }

        private int category;
        public virtual int Category
		{
			get { return category; }
			set { category = value; }
		}

		private int seqNo;
		public virtual int SeqNo
		{
			get { return seqNo; }
			set { seqNo = value; }
		}

		private string code;
		public virtual string Code
		{
			get { return code; }
			set { code = value; }
		}

		private MultilingualString title;
		public virtual MultilingualString Title
		{
			get { return title; }
			set { title = value; }
		}

		private MultilingualString description;
		public virtual MultilingualString Description
		{
			get { return description; }
			set { description = value; }
		}

		private bool isFinalState;
		public virtual bool IsFinalState
		{
			get { return isFinalState; }
			set { isFinalState = value; }
		}

        private bool isInitialState;
        public virtual bool IsInitialState
        {
            get { return isInitialState; }
            set { isInitialState = value; }
        }

        private Rule onEnterRule;
        public virtual Rule OnEnterRule
        {
            get { return onEnterRule; }
            set { onEnterRule = value; }
        }

        private Rule onExitRule;
        public virtual Rule OnExitRule
        {
            get { return onExitRule; }
            set { onExitRule = value; }
        }

        protected Rule onTimeoutRule;
        public virtual Rule OnTimeoutRule
        {
            get { return onTimeoutRule; }
            set { onTimeoutRule = value; }
        }

        private StatefulEntityType owner;
        public virtual StatefulEntityType Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        private Rule rollbackRule;
        public virtual Rule RollbackRule
        {
            get { return rollbackRule; }
            set { rollbackRule = value; }
        }

		private bool requireRemark;
		public virtual bool RequireRemark
		{
			get { return requireRemark; }
			set { requireRemark = value; }
		}

        private TimeDuration timeOutDuration;
        public virtual TimeDuration TimeOutDuration
        {
            get { return timeOutDuration; }
            set { timeOutDuration = value; }
        }

		private IList<StateTransition> transitions;
		public virtual IList<StateTransition> Transitions
		{
			get
			{
				if (transitions == null) transitions = new List<StateTransition>();
				return transitions;
			}
			set { transitions = value; }
		}

		private DateTime updatedTS = DateTime.Now;
		public virtual DateTime UpdatedTS
		{
			get { return updatedTS; }
			set { updatedTS = value; }
		}

		private User updatedBy;
		public virtual User UpdatedBy
		{
			get { return updatedBy; }
			set { updatedBy = value; }
		}

		#endregion persistent

		public virtual RuleResult OnEnter(ParameterList parameters)
		{
			if (onEnterRule != null) return onEnterRule.Execute(this, parameters);
			return RuleResult.Success;
		}

		public virtual RuleResult OnExit(ParameterList parameters)
		{
			if (onExitRule != null) return onExitRule.Execute(this, parameters);
			return RuleResult.Success;
		}

		public virtual RuleResult Rollback(ParameterList parameters)
		{
			if (rollbackRule != null)
				return RuleResult.Error;
			else
				return rollbackRule.Execute(this, parameters);
		}

		public virtual void Save(Context context)
		{
            if (title != null) title.Persist(context);
            if (description != null) description.Persist(context);
			if (onEnterRule != null) onEnterRule.Save(context);
			if (onExitRule != null) onExitRule.Save(context);
			if (rollbackRule != null) rollbackRule.Save(context);
			context.PersistenceSession.SaveOrUpdate(this);
			//foreach (StateTransition t in transitions)
			//    t.Save(context);
		}
		/*
		public static State FindByCode(Context context, string code)
		{
			ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(State));
			crit.Add(Expression.Eq("Code", code));
			return crit.UniqueResult<State>();
		}

		public static State FindByCode(Context context, string stateCode, bool isFinalState)
		{
			ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(State));

			if (stateCode != null)
			{
				crit.Add(Expression.Eq("Code", stateCode));
			}

			crit.Add(Expression.Eq("IsFinalState", isFinalState));
			return crit.UniqueResult<State>();
		}
		*/
		public virtual StateTransition AddTransition(int seqNo, State toState, MultilingualString title, Rule transitionRule)
		{
			StateTransition transition = new StateTransition(seqNo, title, this, toState, transitionRule);
			this.Transitions.Add(transition);
			return transition;
		}

        public virtual string SetTimeOutSchedule()
        {
            StringBuilder builder = new StringBuilder();

            return builder.ToString();
        }

        public virtual string ToLog()
        {
            StringBuilder builder = new StringBuilder();
         
            return builder.ToString();
        }


        public static IList<State> List(Context context)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(State));
            return crit.List<State>();
        }

        public static State Find(Context context, int p)
        {
            return (State)context.PersistenceSession.Get(typeof(State), p);
        }

        public override string ToString()
        {
            return this.Title.ToString();
        }
    }
}
