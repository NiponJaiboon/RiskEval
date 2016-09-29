using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class CommonChoice : QuestionChoice
    {
        public CommonChoice()
            : base()
        {
        }

        public CommonChoice(MultilingualString title, double score)
            : base(score)
        {
            this.Title = title;
        }

        #region persistent

        /// <summary>
        /// Parent is an instance of either ChoiceList or CustomListQuestion
        /// </summary>
        public virtual CommonChoiceList ChoiceList { get; set; }

        #endregion persistent

        public virtual bool IsSelected { get; set; }

        public virtual bool ToggleSelection()
        {
            this.IsSelected = !this.IsSelected;
            return this.IsSelected;
        }

        public override QuestionChoice Clone(ChoiceQuestion parent)
        {
            throw new NotImplementedException();
        }

        private MultilingualString title;
        public override MultilingualString Title
        {
            get { return this.title; }
            set { this.title = value; }
        }
    }
}
