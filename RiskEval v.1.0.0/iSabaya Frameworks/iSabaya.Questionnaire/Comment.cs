using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class Comment : QuestionBase
    {
        public Comment()
            : base()
        {
        }

        public Comment(IQuestionParent parent, Comment original)
            :base(parent, original)
        {
        }

        #region persistent

        #endregion persistent

        public override ResponseBase CreateEmptyResponse(Response response, ResponseGroup parent)
        {
            return new EmptyResponse(response, parent, this);
        }

        public override void SetItemOutlineNo(ref int itemNo, String prefix)
        {
            //intentionally empty
        }

        public override QuestionBase Clone(QuestionChoice parent)
        {
            return new Comment(parent, this);
        }

        public override QuestionBase Clone(QuestionGroup parent)
        {
            return new Comment(parent, this);
        }
    }
}
