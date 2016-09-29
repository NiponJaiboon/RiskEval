using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;
namespace WebHelper.pvd.EmployeeModule
{
    public class VOPairDuplicateReg
    {
        private int lineNo;
        private Member mainEmployee;
        private Member duplicateEmployee;

        public VOPairDuplicateReg()
        {

        }

        public VOPairDuplicateReg(Member mainEmployee, Member duplicateEmployee)
        {
            this.mainEmployee = mainEmployee;
            this.duplicateEmployee = duplicateEmployee;
        }

        public int LineNo
        {
            get { return lineNo; }
            set { this.lineNo=value; }
        }

        public Member MainEmployee
        {
            get { return mainEmployee; }           
        }

        public Member DuplicateEmployee
        {
            get { return duplicateEmployee; }            
        }

        public String MainEmployeeName
        {
            get { return mainEmployee.FullName; }
        }

        public String DuplicateEmployeeName
        {
            get { return duplicateEmployee.FullName; }
        }

    }
}
