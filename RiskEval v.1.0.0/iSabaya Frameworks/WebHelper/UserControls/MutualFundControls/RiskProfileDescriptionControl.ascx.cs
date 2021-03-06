using System;
using System.Web.UI.HtmlControls;
using WebHelper;

public partial class RiskProfileDescriptionControl : iSabayaControl
{
    private double customerRiskScore;

    public double CustomerRiskScore
    {
        get { return this.customerRiskScore; }
        set { this.customerRiskScore = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            InitializeControls();
        if (Page.IsCallback)
            ShowCustomerRiskDescription();
    }

    private void InitializeControls()
    {
        String CssPostfix = ASPxLabel1.CssPostfix;
        tbShowRiskLevel.SetCssHtmlTable_H(CssPostfix, true);
        tdRiskDetail1.SetHeaderCellStyle(CssPostfix);
        tdRiskDetail2.SetHeaderCellStyle(CssPostfix);
        tbDescription.SetCssHtmlTable_H(CssPostfix);
        popViewDescription.HeaderText = "รายละเอียด";
        imgViewDescription.ToolTip = ResGeneral.Help;
        imgViewDescription.ImageUrl = ResImageURL.Help;

        #region ClientInstanceName

        imgViewDescription.ClientInstanceName = this.ClientID + imgViewDescription.ClientID;
        popViewDescription.ClientInstanceName = this.ClientID + popViewDescription.ClientID;

        #endregion ClientInstanceName

        #region Script

        imgViewDescription.ClientSideEvents.Click = @"function(s, e){
            " + popViewDescription.ClientInstanceName + @".Show();
        }";

        #endregion Script

        #region Label

        //Computed Risk Profile
        ASPxLabel43.Text = "ผลประเมิณ";
        ASPxLabel44.Text = "ระดับความเสี่ยง";

        //Header
        ASPxLabel1.Text = "ประเภทนักลงทุน";
        ASPxLabel2.Text = "ระดับความเสี่ยงที่สามารถลงทุนได้";
        ASPxLabel3.Text = "ระดับความเสี่ยง";
        ASPxLabel4.Text = "ประเภทกองทุน";
        ASPxLabel5.Text = "ประเภทหลักทรัพย์ที่ลงทุนเป็นหลัก";

        //trLevel1
        ASPxLabel7.Text = "นักลงทุนประเภท เสี่ยงต่ำ หมายความว่า ท่านต้องการผลตอบแทนมากกว่าการฝากเงินธนาคารเล็กน้อย ไม่ต้องการความเสี่ยง และมีวัตถุประสงค์การลงทุนในระยะสั้นๆ";
        ASPxLabel8.Text = "สามารถลงทุนในความเสี่ยงระดับ 2-9 ได้ไม่ควรเกินร้อยละ 20 ของวงเงินลงทุนทั้งหมด";
        ASPxLabel10.Text = "กองทุนรวมตลาดเงินที่ลงทุนเฉพาะในประเทศ";
        ASPxLabel11.Text = "มีนโยบายลงทุนเฉพาะในประเทศไทย โดยลงทุนในหรือมีไว้ซึ่งเงินฝาก หรือตราสารหนี้ หรือหลักทรัพย์หรือทรัพย์สินอื่น หรือการหาดอกผลอื่นตามที่สำนักงานกำหนด ซึ่งมีกำหนดชำระคืนเมื่อทวงถามหรือจะครบกำหนดชำระคืน หรือมีอายุสัญญาไม่เกิน 1 ปีนับแต่วันที่ลงทุนในทรัพย์สินหรือเข้าทำสัญญานั้น และมี portfolio duration ในขณะใดขณะหนึ่ง ไม่เกิน 3 เดือน";

        //trLevel2
        ASPxLabel13.Text = "นักลงทุนประเภท เสี่ยงปานกลาง หมายความว่า ท่านเป็นผู้ลงทุนที่รับความเสี่ยงได้น้อยเน้นปกป้องเงินลงทุนโดยมุ่งหวังรายได้สม่ำเสมอ จากกการลงทุน";
        ASPxLabel14.Text = "สามารถลงทุนในความเสี่ยงระดับ 5-9 ได้ไม่ควรเกินร้อยละ 20 ของเงินลงทุนทั้งหมด";
        ASPxLabel16.Text = "กองทุนรวมตลาดเงิน";
        ASPxLabel17.Text = "มีนโยบายลงทุนในต่างประเทศบางส่วน แต่ไม่เกินร้อยละ 50 ของ NAV โดยลงทุนในหรือมีไว้ซึ่งเงินฝาก หรือตราสารหนี้ หรือหลักทรัพย์หรือทรัพย์สินอื่น หรือการหาดอกผลอื่นตามที่สำนักงานกำหนด ซึ่งมีกำหนดชำระคืนเมื่อทวงถามหรือจะครบกำหนดชำระคืน หรือมีอายุสัญญาไม่เกิน 1 ปีนับแต่วันที่ลงทุนในทรัพย์สินหรือเข้าทำสัญญานั้น และมี portfolio duration ในขณะใดขณะหนึ่ง ไม่เกิน 3 เดือน";
        ASPxLabel19.Text = "กองทุนรวมพันธบัตรรัฐบาล";
        ASPxLabel20.Text = "มีนโยบายลงทุนในพันธบัตรรัฐบาลเฉลี่ยรอบปีบัญชีไม่น้อยกว่าร้อยละ 80 ของ NAV";
        ASPxLabel22.Text = "กองทุนรวมตราสารหนี้";
        ASPxLabel23.Text = "มีนโยบายลงทุนในตราสารหนี้ทั่วไป รวมถึงตราสารที่มีลักษณะของสัญญาซื้อขายล่วงหน้าแฝง (structured note) ที่คุ้มครองเงิน";

        //trLevel3
        ASPxLabel24.Text = "นักลงทุนประเภท เสี่ยงปานกลางค่อนข้างสูง หมายความว่าท่านสามารถยอมรับมูลค่าการลงุทนที่ลดลงได้เป็นครั้งคราว";
        ASPxLabel25.Text = "สามารถลงทุนในความเสี่ยงระดับ 7-9 ได้ไม่ควรเกินร้อยละ 20 ของเงินลงทุนทั้งหมด";
        ASPxLabel27.Text = "กองทุนรวมตราสารหนี้ที่มีการลงทุนใน structured note";
        ASPxLabel28.Text = "มีนโยบายลงทุนในตราสารหนี้ทั่วไป ที่มีลักษณะของสัญญาซื้อขายล่วงหน้าแฝง (structured note) ที่ไม่คุ้มครองเงิน";
        ASPxLabel30.Text = "กองทุนรวมผสม";
        ASPxLabel31.Text = "มีนโยบายลงทุนได้ทั้งในตราสารทุนและตราสารหนี้";

        //trLevel4
        ASPxLabel6.Text = "นักลงทุนประเภท เสี่ยงสูง หมายความว่าท่านสามารถยอมรับความเสี่ยงได้สูง รับความผันผวนของตลาดได้ และสามารถยอมรับการขาดทุนได้ โดยมึ่งหวังการเติบโตของเงินทุนและผลตอบแทนในระยะยาว";
        ASPxLabel12.Text = "สามารถลงทุนในความเสี่ยงระดับ 9 ได้ไม่ควรเกินร้อยละ 20 ของเงินลงทุนทั้งหมด";
        ASPxLabel33.Text = "กองทุนรวมตราสารทุน";
        ASPxLabel34.Text = "มีนโยบายลงทุนในตราสารทุนเป็นหลัก โดยเฉี่ลยรอบปีบัญชีไม่น้อยกว่าร้อยละ 65 ของ NAV";
        ASPxLabel36.Text = "กองทุนรวมหมวดอุสาหกรรม";
        ASPxLabel37.Text = "มีนโยบายมุ่งลงทุนโดยเฉพาะเจาะจงในตราสารทุนเพียงบางหมวดอุสาหกรรมโดยเฉี่ลยรอบระยะเวลาบัญชีไม่น้อยกว่าร้อยละ 80 ของ NAV";

        //trLeve5
        ASPxLabel38.Text = "นักลงทุนประเภท เสี่ยงสูงมาก หมายความว่าท่านต้องการได้รับโอกาสที่จะได้รับผลตอบแทนสูง ความเสี่ยงสูงและยอมรับการขาดทุนได้ใน sinificant portion";
        ASPxLabel39.Text = "";
        ASPxLabel41.Text = "กองทุนที่มีการลงทุนทรัพย์สินทางเลือก";
        ASPxLabel42.Text = "มีนโยบายลงทุนในทรัพย์สินที่เป็นทางเลือกใหม่ในการลงทุนหรือมีโครงสร้างซับซ้อนเข้าใจยากเช่น commodity/gold fund/oil fund/derivatives ที่ไม่ใช่เพื่อ hedging รวมถึงตราสารที่มีลักษณะของสัญญาซื้อขายล่วงหน้าแฝงที่ไม่คุ้มครองเงินต้น";

        #endregion Label
    }

    public void ShowCustomerRiskDescription()
    {
        lblRiskScore.Text = this.customerRiskScore.ToString();
        lblRiskLevel.Text = GetRiskDescription(this.customerRiskScore);

        switch (GetRiskLevel(this.customerRiskScore))
        {
            case 0:
                RenderTable(trLevel1, "none");
                RenderTable(trLevel2, "none");
                RenderTable(trLevel2_1, "none");
                RenderTable(trLevel2_2, "none");
                RenderTable(trLevel3, "none");
                RenderTable(trLevel3_1, "none");
                RenderTable(trLevel4, "none");
                RenderTable(trLevel4_1, "none");
                RenderTable(trLevel5, "none");
                break;
            case 1:
                RenderTable(trLevel1, "");
                break;
            case 2:
                RenderTable(trLevel2, "");
                RenderTable(trLevel2_1, "");
                RenderTable(trLevel2_2, "");
                break;
            case 3:
                RenderTable(trLevel3, "");
                RenderTable(trLevel3_1, "");
                break;
            case 4:
                RenderTable(trLevel4, "");
                RenderTable(trLevel4_1, "");
                break;
            case 5:
                RenderTable(trLevel5, "");
                break;
            default:

                break;
        }
    }

    private int GetRiskLevel(double RiskScore)
    {
        if (RiskScore > 0 && RiskScore < 16)
            return 1;
        else if (RiskScore >= 15 && RiskScore < 22)
            return 2;
        else if (RiskScore >= 22 && RiskScore < 30)
            return 3;
        else if (RiskScore >= 30 && RiskScore < 37)
            return 4;
        else if (RiskScore >= 37)
            return 5;
        else
            return 0;
    }

    private String GetRiskDescription(double RiskScore)
    {
        if (RiskScore > 0 && RiskScore < 16)
            return "นักลงทุนประเภท เสี่ยงต่ำ";
        else if (RiskScore >= 15 && RiskScore < 22)
            return "นักลงทุนประเภท เสี่ยงปานกลาง";
        else if (RiskScore >= 22 && RiskScore < 30)
            return "นักลงทุนประเภท เสี่ยงปานกลางค่อนข้างสูง";
        else if (RiskScore >= 30 && RiskScore < 37)
            return "นักลงทุนประเภท เสี่ยงสูง";
        else if (RiskScore >= 37)
            return "นักลงทุนประเภท เสี่ยงสูงมาก";
        else
            return "นักลงทุนประเภท Undefine";
    }

    private void RenderTable(HtmlTableRow TableRow, String Display)
    {
        TableRow.Style.Add(System.Web.UI.HtmlTextWriterStyle.Display, Display);
    }
}