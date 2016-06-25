using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class EmptyResponse : ResponseBase
    {
        public EmptyResponse()
        {
        }

        public EmptyResponse(Response response, ResponseGroup parent, Comment question)
        {
            // TODO: Complete member initialization
            this.Response = response;
            this.Parent = parent;
            this.Question = question;
        }

        #region persistent

        public new virtual Comment Question
        {
            get { return (Comment)base.Question; }
            set { base.Question = value; }
        }

        #endregion persistent

        public override double Score
        {
            get { return 0d; }
            set { }
        }

        public override double ComputeScore()
        {
            return 0d;
        }

        public override void Reset()
        {
        }

        public override void SetValue(object value)
        {
        }
    }
}
