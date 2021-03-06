using System;
using System.Collections;
using System.Collections.Generic;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;
using WebHelper;
namespace WebHelper.pvdWeb
{
    public partial class ChequeBatchItemControl : iSabayaControl
    {
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
            if (!IsPostBack)
            {
                Session[this.ClientID + "ctrls_ChequeBatchItemControl_ChequeBatchItems"] = null;
            }

            //coke 13072009 hh:mm
            if (IsRequiredField)
            {
                textChequeBatchItemEdit.SetValidation(validationGroup);
            }
        }

        public void BindBatchItem()
        {
            if (Session["SessionBankAccount"] != null)
            {
                BankAccount ba = (BankAccount)Session["SessionBankAccount"];
                List<long> cheque = new List<long>();
                IList<Cheque> ch = new List<Cheque>();
                ICriteria critCheque = iSabayaContext.PersistencySession.CreateCriteria(typeof(Cheque));
                critCheque.Add(Expression.Eq("BankAccount", ba));
                ch = critCheque.List<Cheque>();
                for (int i = 0; i < ch.Count; i++)
                {
                    if (ch[i].BatchItem != null)
                    {
                        cheque.Add(ch[i].BatchItem.ChequeBatchItemID);
                    }
                }
                Session["ctrls_ChequeBatchItemControl_ChequeBatchItems"] = null;
                IList<ChequeBatch> chequeB = ChequeBatch.FindAvailable(iSabayaContext, ba);
                IList<ChequeBatchItem> chequeList = new List<ChequeBatchItem>();
                for (int i = 0; i < chequeB.Count; i++)
                {
                    ICriteria crit = iSabayaContext.PersistencySession.CreateCriteria(typeof(ChequeBatchItem));
                    crit.Add(Expression.Eq("ChequeBatch", chequeB[i]));
                    crit.Add(Expression.IsNull("Cheque"));//เช็คว่าเช็คใช้หรือยัง ถ้าใช้แล้วเช็ค ChequeBatchItem จะมีChequeID
                    if (cheque != null)
                    {
                        crit.Add(Expression.Not(Expression.In("ChequeBatchItemID", cheque)));
                    }
                    foreach (ChequeBatchItem item in crit.List<ChequeBatchItem>())
                    {
                        chequeList.Add(item);
                    }
                }
                Session["ctrls_ChequeBatchItemControl_ChequeBatchItems"] = chequeList;
            }
        }

        protected void gridChequeBatchItems_OnCustomCallback(object source, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            onload();
        }

        protected void gridChequeBatchItems_OnDataBinding(object source, EventArgs e)
        {
            if (Session["ctrls_ChequeBatchItemControl_ChequeBatchItems"] != null)
                gridChequeBatchItems.DataSource = (List<ChequeBatchItem>)Session["ctrls_ChequeBatchItemControl_ChequeBatchItems"];
        }

        private void onload()
        {
            BindBatchItem();
            IList<ChequeBatchItem> chequeList = (List<ChequeBatchItem>)Session["ctrls_ChequeBatchItemControl_ChequeBatchItems"];
            if (chequeList.Count != 0)
            {
                textKeyWord.Text = chequeList[0].ChequeNo;
                textChequeBatchItemEdit.Text = chequeList[0].ChequeNo;
                lblDetail.Text = "คงเหลือจำนวน: " + chequeList.Count.ToString() + " ใบ";
                Session["ctrls_ChequeBatchItemControl_ChequeBatchItemsID"] = chequeList[0];
                gridChequeBatchItems.DataBind();
            }
        }

        protected void cbSearch_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            BindBatchItem();
            Session["ctrls_ChequeBatchItemControl_ChequeBatchItems"] = Session["ctrls_ChequeBatchItemControl_ChequeBatchItems"];
            gridChequeBatchItems.DataBind();
        }

        protected void cbSelectedChequeBatchItem_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            int index = int.Parse(e.Parameter);
            ChequeBatchItem vo = (ChequeBatchItem)gridChequeBatchItems.GetRow(index);
            Session["ctrls_ChequeBatchItemControl_SelectedChequeBatchItem"] = vo;
        }

        protected void cbpTextChequeBatchItemEdit_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            ChequeBatchItem vo = (ChequeBatchItem)Session["ctrls_ChequeBatchItemControl_SelectedChequeBatchItem"];
            textChequeBatchItemEdit.Text = vo.ChequeNo.ToString();
            textChequeBatchItemEdit.Value = vo.ChequeBatchItemID;
            labelChequeBatchItem.Text = vo.ChequeNo.ToString();
        }

        public IList<ChequeBatchItem> ChequeBatchItems
        {
            get
            {
                IList<ChequeBatchItem> cbis = (IList<ChequeBatchItem>)Session["ctrls_ChequeBatchItemControl_ChequeBatchItems"];
                return cbis;
            }
        }

        public ChequeBatchItem ChequeBatchItem
        {
            get
            {
                ChequeBatchItem vo = (ChequeBatchItem)Session["ctrls_ChequeBatchItemControl_SelectedChequeBatchItem"];
                return vo;
            }
            set
            {
                textChequeBatchItemEdit.Text = value.ChequeNo.ToString();
            }
        }
    }
}