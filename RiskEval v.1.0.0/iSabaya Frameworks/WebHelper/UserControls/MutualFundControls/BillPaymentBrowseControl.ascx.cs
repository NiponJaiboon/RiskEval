using System;
using System.Collections;
using System.Collections.Generic;
using imSabaya.MutualFundSystem;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;
using WebHelper;
using WebHelper.ValueObject.master;

public partial class ctrls_BillPaymentBrowseControl : iSabayaControl
{
    //coke 14072009 hh:mm

    #region Validation Section

    private bool isRequiredField = false;

    public bool IsRequiredField
    {
        get { return isRequiredField; }
        set { this.isRequiredField = value; }
    }

    private String validationGroup;

    /// <summary>
    /// Get or sets the group of controls for which the editor forces validation when it posts back to the server.
    /// </summary>
    public String ValidationGroup
    {
        get { return validationGroup; }
        set { this.validationGroup = value; }
    }

    #endregion Validation Section

    protected void Page_Load(object sender, EventArgs e)
    {
        //coke 14072009 hh:mm
        if (IsRequiredField)
        {
            //            MFAccountControl1.ValidationGroup = ValidationGroup;
            //   MFAccountControl1.IsRequiredField = this.IsRequiredField;
            //controlxx.ValidationSettings.ValidationGroup = ValidationGroup;

            //controlxx.ValidationSettings.SetFocusOnError = true;
            //controlxx.ValidationSettings.ErrorText = "ErrorText";
            //controlxx.ValidationSettings.ValidateOnLeave = true;
            //controlxx.ValidationSettings.ErrorImage.Height = Unit.Pixel(16);
            //controlxx.ValidationSettings.ErrorImage.Width = Unit.Pixel(16);
            //controlxx.ValidationSettings.ErrorImage.AlternateText = "Error";
            //controlxx.ValidationSettings.ErrorImage.Url = "~/Images/iconError.png";
            //controlxx.ValidationSettings.RequiredField.IsRequired = true;
            //controlxx.ValidationSettings.RequiredField.ErrorText = "กรุณากรอกข้อมูล";
            //controlxx.ValidationSettings.ErrorFrameStyle.ForeColor = System.Drawing.Color.Red;
            //controlxx.ValidationSettings.ErrorFrameStyle.Paddings.Padding = Unit.Pixel(3);
            //controlxx.ValidationSettings.ErrorFrameStyle.Paddings.PaddingLeft = Unit.Pixel(4);
            //controlxx.ValidationSettings.ErrorFrameStyle.BackgroundImage.ImageUrl = "~/Images/bgError.png";
            //controlxx.ValidationSettings.ErrorFrameStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#FD4D3E");
            //controlxx.ValidationSettings.ErrorFrameStyle.Border.BorderStyle = BorderStyle.Solid;
            //controlxx.ValidationSettings.ErrorFrameStyle.Border.BorderWidth = Unit.Pixel(1);
            //controlxx.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingRight = Unit.Pixel(3);
        }

        if (IsPostBack == false)
        {
            Session["ctrls_PaymentBrowseControl_paymentId"] = null;
            Session["ctrls_PaymentBrowseControl_payments"] = null;
            Session["ctrls_PaymentBrowseControl_MFTransactions"] = null;
        }
        else
        {
            if (Session["ctrls_PaymentBrowseControl_payments"] != null)
            {
                gridMainPayment.DataSource = Session["ctrls_PaymentBrowseControl_payments"];
                gridMainPayment.DataBind();
            }
            if (Session["ctrls_PaymentBrowseControl_MFTransactions"] != null)
            {
                gridTransactions.DataSource = Session["ctrls_PaymentBrowseControl_MFTransactions"];
                gridTransactions.DataBind();
                gridTransactions.Visible = true;
            }
            else
            {
                gridTransactions.Visible = false;
            }
        }
    }

    public IList<VOPayment> PaymentList;
    public DateTime PaymentDate;

    private List<VOMFTransaction> BindGridTransaction(IList<MFTransaction> MFTransactions)
    {
        List<VOMFTransaction> voList = new List<VOMFTransaction>();
        foreach (MFTransaction item in MFTransactions)
        {
            VOMFTransaction vo = new VOMFTransaction(item);
            voList.Add(vo);
        }
        return voList;
    }

    private List<VOPayment> FindPaymentsOfPayer(MFAccount account)
    {
        ISession session = PersistenceLayer.WebSessionManager.PersistenceSession;
        ICriteria crit = session.CreateCriteria<Payment>()
                                .Add(Expression.Eq("Payer", account))
                                .Add(Expression.IsNull("Reference"));
        List<VOPayment> voList = new List<VOPayment>();
        foreach (Payment item in crit.List<Payment>())
        {
            VOPayment vo = new VOPayment(item);
            voList.Add(vo);
        }
        return voList;
    }

    private void FindTransaction()
    {
        //TreeListNode bpCat = Config.CurrentConfig.MFTransactionChannelRootNode.GetChild("BillPayment");
        //IList billPaymentMatchingList = new ArrayList();
        //ISession session = PersistenceLayer.WebSessionManager.PersistenceSession;
        //IList<MFTransaction> bpTransactions = session.CreateCriteria<MFTransaction>()
        //                                            .Add(Expression.Eq("TransactionChannel", bpCat))
        //                                            .List<MFTransaction>();
        //IList<BillPaymentMatching> matches = BillPaymentMatching.TryMatchingBillPayments(session, bpTransactions, null,
    }

    protected void cbSearch_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        if (null != this.PaymentList && this.PaymentList.Count > 0)
            Session["ctrls_PaymentBrowseControl_payments"] = this.PaymentList;
        //else if (null != MFAccountControl1.MFAccount)
        //Session["ctrls_PaymentBrowseControl_payments"] = FindPaymentsOfPayer(MFAccountControl1.MFAccount);
        else
        {
            Session["ctrls_PaymentBrowseControl_payments"] = null;
            Session["ctrls_PaymentBrowseControl_message"] = "No criteria for selecting payments";
        }
    }

    public Payment Payment
    {
        get
        {
            if (Session["ctrls_PaymentBrowseControl_paymentId"] == null) return null;
            //List<Object> selected = gridMainPayment.GetSelectedFieldValues(new String[] { "PaymentID" });
            int paymentId = (int)Session["ctrls_PaymentBrowseControl_paymentId"];
            Payment payment = Payment.Find(iSabayaContext, paymentId);

            return payment;
        }
    }

    public MFTransaction MFTransaction
    {
        get
        {
            IList<MFTransaction> MFTransactions =
                MFTransaction.FindByPayment(iSabayaContext, this.Payment.PaymentID);
            return MFTransactions[0];
        }
    }

    protected void cbgridMainPaymentChangeFocus_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        if (int.Parse(e.Parameter) < 0) return;
        String parameter = e.Parameter;
        VOPayment payment = (VOPayment)gridMainPayment.GetRow(int.Parse(parameter));
        Session["ctrls_PaymentBrowseControl_paymentId"] = payment.PaymentID;
        IList<MFTransaction> MFTransactions = MFTransaction.FindByPayment(iSabayaContext, payment.PaymentID);
        Session["ctrls_PaymentBrowseControl_MFTransactions"] = BindGridTransaction(MFTransactions); ;
    }

    protected void cbgridTransactionChangeFocus_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
    }
}