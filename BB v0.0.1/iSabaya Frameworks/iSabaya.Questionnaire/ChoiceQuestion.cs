using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public abstract class ChoiceQuestion : QuestionItem
    {
        public ChoiceQuestion()
        {
        }

        public ChoiceQuestion(IQuestionParent parent, ChoiceQuestion original)
            : base(parent, original)
        {
            this.AllowMultipleSelections = original.AllowMultipleSelections;
            IList<QuestionChoice> choices = new List<QuestionChoice>();
            foreach (QuestionChoice c in original.choices)
            {
                choices.Add(c.Clone(this));
            }
            this.choices = choices;
        }

        #region persistent

        public virtual bool AllowMultipleSelections { get; set; }
        protected IEnumerable<QuestionChoice> choices;
        public virtual IEnumerable<QuestionChoice> Choices
        {
            get { return this.choices; }
            set { this.choices = value; }
        }

        #endregion persistent

        public override ResponseBase CreateEmptyResponse(Response response, ResponseGroup parent)
        {
            ChoiceResponse r = new ChoiceResponse(response, parent, this);
            return r;
        }

        public override void Persist(Context context)
        {
            base.Persist(context);
            foreach (QuestionChoice qc in this.Choices)
            {
                qc.ParentQuestion = this;
                qc.Save(context);
            }
        }
    }
}
