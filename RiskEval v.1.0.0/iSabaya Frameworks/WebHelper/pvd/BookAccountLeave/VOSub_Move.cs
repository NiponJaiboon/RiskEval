using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using iSabaya;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using imSabaya.ProvidentFundSystem;

namespace WebHelper.pvd.BookAccountLeave
{
    [Serializable]
    public class VOSub_Move : VOItem
    {
        String department;
        String toEmployee;
        bool haveVest;
        private DataTable dtTransferInvestment;
        public DataTable DTTransferInvestment
        {
            get { return dtTransferInvestment; }
            set { dtTransferInvestment = value; }
        }
        public static String CATEGORY_KEY = "NodeID";
        public static String CATEGORY_TRANSFER_OUT = "CategoryOut";
        public static String CATEGORY_TRANSFER_IN = "CategoryIn";
        public static String IS_MERGE = "IsMerge";

        public VOSub_Move(Member employee)
            : base(employee)
        {
            this.InitDTTransferInvestment();
        }
        private void InitDTTransferInvestment()
        {
            dtTransferInvestment = new DataTable();
            DataColumn dc = new DataColumn(CATEGORY_KEY, typeof(String));
            dtTransferInvestment.Columns.Add(dc);
            dtTransferInvestment.Columns.Add(CATEGORY_TRANSFER_OUT, typeof(iSabaya.TreeListNode));
            dtTransferInvestment.Columns.Add(CATEGORY_TRANSFER_IN, typeof(iSabaya.TreeListNode));
            dtTransferInvestment.Columns.Add(IS_MERGE, typeof(bool));
            dtTransferInvestment.PrimaryKey = new DataColumn[] { dc };
        }
        public void SetDTTransferInvestment(TreeListNode nodeIDCategoryOut, TreeListNode nodeIDCategoryIn, bool isMerge)
        {
            DataRow dr =  this.dtTransferInvestment.Rows.Find(nodeIDCategoryOut.Code);
            if (dr == null)
            {
                dr = this.dtTransferInvestment.NewRow();
                dr[CATEGORY_KEY] = nodeIDCategoryOut.Code;
                dr[CATEGORY_TRANSFER_OUT] = nodeIDCategoryOut;
                dr[CATEGORY_TRANSFER_IN] = nodeIDCategoryIn;
                dr[IS_MERGE] = isMerge;
                this.dtTransferInvestment.Rows.Add(dr);
            }
            else
            {
                dr[CATEGORY_KEY] = nodeIDCategoryOut.Code;
                dr[CATEGORY_TRANSFER_OUT] = nodeIDCategoryOut;
                dr[CATEGORY_TRANSFER_IN] = nodeIDCategoryIn;
                dr[IS_MERGE] = isMerge;
            }
        }

        private String getTransferTo(String investmentCategoryCode)
        {
            return dtTransferInvestment.Rows.Find(investmentCategoryCode)[CATEGORY_TRANSFER_IN].ToString();
        }
        public TreeListNode GetTransferTo(String investmentCategoryCode)
        {
            return (TreeListNode)dtTransferInvestment.Rows.Find(investmentCategoryCode)[CATEGORY_TRANSFER_IN];
        }
        public bool GetTransferMerge(String investmentCategoryCode)
        {
            return (bool)dtTransferInvestment.Rows.Find(investmentCategoryCode)[IS_MERGE];
        }
        private bool getTransferMerge(String investmentCategoryCode)
        {
            return (bool)dtTransferInvestment.Rows.Find(investmentCategoryCode)[IS_MERGE];
        }
        public String InvestmentOut
        {
            get
            {
                StringBuilder str = new StringBuilder();
                foreach (DataRow dr in dtTransferInvestment.Rows)
                {
                    str.AppendLine(dr[CATEGORY_TRANSFER_OUT].ToString());
                }
                return str.ToString();
            }
        }
        public String InvestmentIn
        {
            get
            {
                StringBuilder str = new StringBuilder();
                foreach (DataRow dr in dtTransferInvestment.Rows)
                {
                    str.AppendLine(dr[CATEGORY_TRANSFER_IN].ToString());
                }
                return str.ToString();
            }
        }
        public String InvestmentMerge
        {
            get
            {
                StringBuilder str = new StringBuilder();
                foreach (DataRow dr in dtTransferInvestment.Rows)
                {
                    str.AppendLine(dr[IS_MERGE].ToString());
                }
                return str.ToString();
            }
        }
        public String PresentTo
        {
            get { return this.getTransferTo(PFConstants.InvestmentCategoryCodePresent); }
        }
        public bool PresentMerge
        {
            get { return this.getTransferMerge(PFConstants.InvestmentCategoryCodePresent); }
        }
        public String DonateTo
        {
            get { return this.getTransferTo(PFConstants.InvestmentCategoryCodeDonation); }
        }
        public bool DonateMerge
        {
            get { return this.getTransferMerge(PFConstants.InvestmentCategoryCodeDonation); }
        }
        public String FineTo
        {
            get { return this.getTransferTo(PFConstants.InvestmentCategoryCodeFine); }
        }
        public bool FineMerge
        {
            get { return this.getTransferMerge(PFConstants.InvestmentCategoryCodeFine); }
        }
        public String InitialTo
        {
            get { return this.getTransferTo(PFConstants.InvestmentCategoryCodeInitial); }
        }
        public bool InitialMerge
        {
            get { return this.getTransferMerge(PFConstants.InvestmentCategoryCodeInitial); }
        }
        public String PreviousTo
        {
            get { return this.getTransferTo(PFConstants.InvestmentCategoryCodePrevious); }
        }
        public bool PreviousMerge
        {
            get { return this.getTransferMerge(PFConstants.InvestmentCategoryCodePrevious); }
        }
        public String TransferredTo
        {
            get { return this.getTransferTo(PFConstants.InvestmentCategoryCodeTransferred); }
        }
        public bool TransferredMerge
        {
            get { return this.getTransferMerge(PFConstants.InvestmentCategoryCodeTransferred); }
        }
        public String UnvestedTo
        {
            get { return this.getTransferTo(PFConstants.InvestmentCategoryCodeUnvested); }
        }
        public bool UnvestedMerge
        {
            get { return this.getTransferMerge(PFConstants.InvestmentCategoryCodeUnvested); }
        }

        public Organization ToOrganization { get; set; }

        //public String ToExtEmployer { get; set; }

        public String Department
        {
            get { return this.department; }
            set { this.department = value; }
        }

        public String ToEmployee
        {
            get { return this.toEmployee; }
            set { this.toEmployee = value; }
        }

        public bool HaveVest
        {
            get { return this.haveVest; }
            set { this.haveVest = value; }
        }

        TreeListNode currentFund;

        public TreeListNode CurrentFund
        {
            get { return currentFund; }
            set { currentFund = value; }
        }
        TreeListNode oldFund;

        public TreeListNode OldFund
        {
            get { return oldFund; }
            set { oldFund = value; }
        }
        TreeListNode transferFund;

        public TreeListNode TransferFund
        {
            get { return transferFund; }
            set { transferFund = value; }
        }
        TreeListNode initialFund;

        public TreeListNode InitialFund
        {
            get { return initialFund; }
            set { initialFund = value; }
        }
        bool isMerge1;

        public bool IsMerge1
        {
            get { return isMerge1; }
            set { isMerge1 = value; }
        }
        bool isMerge2;

        public bool IsMerge2
        {
            get { return isMerge2; }
            set { isMerge2 = value; }
        }
        bool isMerge3;

        public bool IsMerge3
        {
            get { return isMerge3; }
            set { isMerge3 = value; }
        }
        bool isMerge4;

        public bool IsMerge4
        {
            get { return isMerge4; }
            set { isMerge4 = value; }
        }
        TreeListNode currentFundToType;

        public TreeListNode CurrentFundToType
        {
            get { return currentFundToType; }
            set { currentFundToType = value; }
        }
        TreeListNode oldFundToType;

        public TreeListNode OldFundToType
        {
            get { return oldFundToType; }
            set { oldFundToType = value; }
        }
        TreeListNode transferFundToType;

        public TreeListNode TransferFundToType
        {
            get { return transferFundToType; }
            set { transferFundToType = value; }
        }
        TreeListNode initialFundToType;

        public TreeListNode InitialFundToType
        {
            get { return initialFundToType; }
            set { initialFundToType = value; }
        }
        int totalYearStore;
        public int TotalYearStore
        {
            get { return totalYearStore; }
            set { totalYearStore = value; }
        }
        //int fundType2=-1;
        //int fundType3=-1;
        //int fundType4=-1;
        /*
        กองทุนปัจจุบัน 0
        กองทุนเก่า 1
        กองทุนโอนมา 2
        กองทุนประเดิม 3
         */
        //public int FundType1
        //{
        //    get { return this.fundType1; }
        //    set { this.fundType1 = value; }
        //}
        //public int FundType2
        //{
        //    get { return this.fundType2; }
        //    set { this.fundType2 = value; }
        //}
        //public int FundType3
        //{
        //    get { return this.fundType3; }
        //    set { this.fundType3 = value; }
        //}

        //public int FundType4
        //{
        //    get { return this.fundType4; }
        //    set { this.fundType4= value; }
        //}
        //

    }
}
