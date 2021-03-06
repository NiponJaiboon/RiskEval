using System;
using System.Web.UI.WebControls;
using WebHelper;

public partial class CustomerRiskProfileViewerControl : iSabayaControl
{
    private iSabaya.Response responseForm;

    public iSabaya.Response ResponseForm
    {
        get { return this.responseForm; }
        set { this.responseForm = value; }
    }

    private int formWidth = 800;

    public int FormWidth
    {
        get { return formWidth; }
        set { formWidth = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            InitializeControls();
        if (Page.IsCallback)
        {
            if (null != this.responseForm)
                ShowCustomerRiskProfile();
        }
    }

    private void InitializeControls()
    {
        string CssPostFix = ASPxLabel1.CssPostfix;
        //tbViewQuestionniare.SetCssHtmlTable_H(CssPostFix);
        ASPxLabel1.Text = "วันที่ทำการประเมิณ";
    }

    public void ShowCustomerRiskProfile()
    {
        lblQuestionniareDate.Text = this.responseForm.RespondedDate.ToString(base.DateOutputFormat);
        ctrlQuestionniare.ResponseForm = this.responseForm;
        ctrlQuestionniare.FormWidth = new Unit(this.formWidth);
        ctrlQuestionniare.Initial();
    }

    //public void ShowCustomerRiskProfile()
    //{
    //    lblQuestionniareDate.Text = this.responseForm.RespondedDate.ToString(base.DateOutputFormat);
    //    string CssPostFix = ASPxLabel1.CssPostfix;
    //    ColumnHeaderInfo[] headerColumns =
    //    new ColumnHeaderInfo[]
    //        {
    //            new ColumnHeaderInfo(CssPostFix, "คำถาม"),
    //            new ColumnHeaderInfo(CssPostFix, "คำตอบ"),
    //        };

    //    HtmlTableRow tr = new HtmlTableRow();
    //    tbViewQuestionniare.Rows.Add(tr);

    //    DisplayResponseUtil.DisplayResponseGroup
    //    (
    //        tbViewQuestionniare,
    //        this.responseForm.Responses,
    //        headerColumns,
    //        base.LanguageCode
    //    );
    //}
}