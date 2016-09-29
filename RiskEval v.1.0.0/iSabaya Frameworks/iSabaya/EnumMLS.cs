using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    public enum EnumMLS
    {
        #region A010100
        Save = 500000,
        DateOpenAccount,
        Fund,
        FundName,
        MutualFundCustomer,
        CardType,
        CardCode,
        CustomerName,
        Scriptless,
        yes,
        no,
        PayMethod,
        Connective,
        And,
        Or,
        For,
        By,
        BankAccount,
        Bank,
        Branch,
        AccountNumber,
        InvestmentPlanner,
        InvestmentPlannerName,
        RelateAccount,
        CustomerNo,
        AdvisedBy,
        AdvisedType,
        Add,
        A010000,//500030
        A010100,
        A000000,
        #endregion

        #region F010000
        F010000,//เปิดกองทุน       
        FundCode,
        FundType, //เก็บใน FundAttribute
        FundRelateOrg,
        OpenTime,
        CloseTime,
        OpenTimeMFC,
        CloseTimeMFC,
        Par,
        Currency,
        RegisterDate,
        SizeOfFund1,
        SizeOfFund2,
        F020000,//เลิกกองทุน
        F030000,//บันทึกบัญชีธนาคารกองทุน
        F040000,//ปรับปรุงมูลค่าหน่วยลงทุน
        F010100,//คำนวนขนาดกองทุนเฉพาะกอง
        F010200,//คำนวนขนาดกองทุนทุกกอง
        F020100,//บันทึกรายการตอบรับจากผู้ถือหน่วย
        F040100,//สอบถามมูลค่าทรัพย์สินสุทธิต่อหน่วย
        F040200,//สรุปรายการที่ยังไม่สมบูรณ์



        #endregion

        FromDate,
        ToDate,
        Organization,
        Show,
        Date,
        OrganizationControlProfitFund,
        OrganizationCode,
        Check,
        C010000,
        C010100,
        C010200,
        C010300,
        C010400,
        C010500,
        Select,
        UseCriteriaFund,

        Amount,
        All,
        MutualFundAccount,

        AddressNo,
        Street1,
        Street2,
        RegionLevel1,
        RegionLevel2,
        RegionLevel3,
        postalCode,
        Country,
        Phone,
        Foreign,
        //C010100
        DateRegister,
        Issue,
        DateIssue,
        DateExpire,
        Title,
        Name,
        Lastname,
        Gender,
        TypeOwnerUnit,
        Telephone,
        Mobile,
        Nationality,
        Occupation,
        TaxID,
        TaxPaymentMethod,
        Email,
        ContactCode,

       
        A020000,
        A030000,
        A040000,
        A050000,
        
        A010200,
        A020100,
        A020200,
        A020300,
        A020400,
        A030100,
        A040100,
        A040200,
        A050100,
        A050200,
        T010000,
        T010100,
        T010200,
        T000000,

        T020100,
        T020200,
        T020000
    }
}
