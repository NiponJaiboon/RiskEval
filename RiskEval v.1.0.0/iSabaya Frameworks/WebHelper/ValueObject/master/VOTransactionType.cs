using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOTransactionType
    {
        private int transactionTypeID;
        private string code;
        private string title;
        private string description;
        private DateTime effectiveFrom;
        private DateTime effectiveTo;
        private DateTime updatedTS;
        private string updatedBy;
        private string creationRule;
        private string validationRule = null;
        private string rollbackRule;

        public int TransactionTypeID
        {
            get { return transactionTypeID; }
            set { transactionTypeID = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public DateTime EffectiveFrom
        {
            get { return effectiveFrom; }
            set { effectiveFrom = value; }
        }

        public DateTime EffectiveTo
        {
            get { return effectiveTo; }
            set { effectiveTo = value; }
        }

        public DateTime UpdatedTS
        {
            get { return updatedTS; }
            set { updatedTS = value; }
        }

        public string UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        public string CreationRule
        {
            get { return creationRule; }
            set { creationRule = value; }
        }

        public string ValidationRule
        {
            get { return validationRule; }
            set { ValidationRule = value; }
        }

        public string RollbackRule
        {
            get { return rollbackRule; }
            set { rollbackRule = value; }
        }
    }
}
