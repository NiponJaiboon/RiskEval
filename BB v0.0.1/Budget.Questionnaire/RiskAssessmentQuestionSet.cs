using System;
using System.Collections.Generic;
using iSabaya.Questionnaire;
using iSabaya;

namespace Budget
{
    public static class RiskAssessmentQuestionSet
    {
        public static void FillResponse(QuestionnaireItem i)
        {

            if (i is ChoiceQuestion)
            {
                var cq = (ChoiceQuestion)i;
                cq.Choices[0].IsSelected = true;
                if (cq.Choices[0].ContingencyQuestion != null)
                    FillResponse(cq.Choices[0].ContingencyQuestion);
            }
            else if (i is DateQuestion)
                ((DateQuestion)i).ResponseValue = DateTime.Now;
            else if (i is IntegerQuestion)
                ((IntegerQuestion)i).ResponseValue = 100;
            else if (i is MatrixQuestion)
            {
                var q = i as MatrixQuestion;
                var columns = q.ColumnSection;
                //Create response of each column
                foreach (var j in columns.Children)
                {
                    FillResponse(j);
                }
                //Create response of each row question
                foreach (var rq in q.Children)
                {
                    columns.CreateOrUpdateResponseValue();
                    rq.ResponseValue = columns.ResponseValue;
                }
                //q.CreateResponseValue();
            }
            else if (i is MoneyQuestion)
                ((MoneyQuestion)i).ResponseValue = 1234.56m;
            else if (i is RealNumberQuestion)
                ((RealNumberQuestion)i).ResponseValue = 11111.1234d;
            else if (i is TextQuestion)
                ((TextQuestion)i).ResponseValue = "Text response " + DateTime.Now.ToUniversalTime();
            else if (i is BasicQuestionSection)
            {
                var q = i as BasicQuestionSection;
                foreach (var rq in q.Children)
                {
                    FillResponse(rq);
                }
            }
            else if (i is GeneralItemSection)
            {
                var q = i as GeneralItemSection;
                foreach (var rq in q.Children)
                {
                    FillResponse(rq);
                }
            }
        }
    }
}
