using iSabaya;
using iSabaya.Questionnaire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget
{
    public class ProjectQuestionnaireResponse : PersistentTemporalEntity
    {
        public ProjectQuestionnaireResponse()
        {
        }

        public ProjectQuestionnaireResponse(Project project, QuestionnaireResponse questionnaireResponse)
        {
            this.project = project;
            this.QuestionnaireResponse = questionnaireResponse;
        }

        #region persistent

        protected Project project;
        public virtual Project Project
        {
            get { return project; }
            set { project = value; }
        }

        protected QuestionnaireResponse questionnaireResponse;
        public virtual QuestionnaireResponse QuestionnaireResponse
        {
            get { return questionnaireResponse; }
            set { questionnaireResponse = value; }
        }

        #endregion persistent
    }
}
