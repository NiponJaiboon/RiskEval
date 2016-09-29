using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya.Questionnaire
{
    public struct ItemSelection
    {
        /// <summary>
        /// Zero-based index no.
        /// </summary>
        public int ChoiceNo;
        //public bool IsSelected;
        /// <summary>
        /// Null if no value is defined.
        /// </summary>
        public object Value;
    }

    [Serializable]
    public class ChoiceResponse : ResponseBase
    {
        public ChoiceResponse()
        {
        }

        public ChoiceResponse(Response response, ResponseGroup parent, ChoiceQuestion question)
        {
            this.Response = response;
            this.Parent = parent;
            this.Question = question;
            this.choices = CreateEmptyChoices(response);
        }

        private List<ResponseChoice> CreateEmptyChoices(Response response)
        {
            List<ResponseChoice> rchoices = new List<ResponseChoice>();
            foreach (QuestionChoice i in this.Question.Choices)
            {
                rchoices.Add(new ResponseChoice(response, this, i));
            }
            return rchoices;
        }

        #region persistent

        public new virtual ChoiceQuestion Question
        {
            get { return (ChoiceQuestion)base.Question; }
            set { base.Question = value; }
        }

        private IList<ResponseChoice> choices;
        public virtual IList<ResponseChoice> Choices
        {
            get
            {
                if (null == this.choices)
                    this.choices = new List<ResponseChoice>();
                return this.choices;
            }
            set { this.choices = value; }
        }

        #endregion persistent

        public override double ComputeScore()
        {
            double score = 0d;
            foreach (ResponseChoice i in this.Choices)
            {
                if (i.IsSelected)
                    score += i.QuestionChoice.Score;
            }
            return score * this.Question.Weight;
        }

        public override void Reset()
        {
            foreach (ResponseChoice i in this.Choices)
            {
                i.Reset();
            }
        }

        public override void SetValue(Object value)
        {
            if (null == value) return;


            if (this.Question.AllowMultipleSelections)
            {
                //int i = 0;
                //foreach (ResponseChoice rc in this.Choices)
                //{
                //    ItemSelection responseItem = responses[i++];
                //    rc.IsSelected = responseItem.IsSelected;
                //    if (rc.IsSelected && null != rc.FurtherResponse)
                //        rc.FurtherResponse.SetValue(responseItem.Value);
                //}
                ItemSelection[] multiresponses = (ItemSelection[])value;
                foreach (ItemSelection response in multiresponses)
                {
                    if (response.ChoiceNo >= this.Choices.Count)
                        throw new iSabayaException(String.Format("Choice no. of question '{0}' is out of bound.", this.Question.ToString()));
                    ResponseChoice rc = this.Choices[response.ChoiceNo];
                    rc.IsSelected = true;
                    if (null != response.Value && null != rc.FurtherResponse)
                        rc.FurtherResponse.SetValue(response.Value);
                }
            }
            else //single selection
            {
                ItemSelection singleResponse = (ItemSelection)value;
                if (singleResponse.ChoiceNo >= this.Choices.Count)
                        throw new iSabayaException(String.Format("Choice no. of question '{0}' is out of bound.", this.Question.ToString()));
                ResponseChoice rc = this.Choices[singleResponse.ChoiceNo];
                rc.IsSelected = true;
                if (null != singleResponse.Value && null != rc.FurtherResponse)
                    rc.FurtherResponse.SetValue(singleResponse.Value);
            }
        }

        public override void Save(Context context)
        {
            context.PersistenceSession.SaveOrUpdate(this);
            foreach (ResponseChoice i in this.Choices)
            {
                i.Parent = this;
                i.Save(context);
            }
        }
    }
}
