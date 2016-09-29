using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using WebHelper.ValueObject;
using NHibernate;
using imSabaya;

namespace WebHelper
{
	[Serializable]
	public class HelperRecieptItem
	{
		private List<ReceiptItem> receiptItems;

		public List<ReceiptItem> ReceiptItems
		{
			get
			{
				if (receiptItems == null)
				{
					receiptItems = new List<ReceiptItem>();
				}
				return receiptItems;

			}
			set { this.receiptItems = value; }
		}

		private int receiptItemID;
		public virtual int ReceiptItemID
		{
			get { return receiptItemID; }
			set { receiptItemID = value; }
		}
		
		private int seqNo;
		public  int SeqNo
		{
			get { return seqNo; }
			set { seqNo = value; }
		}

		private Receipt receipt;
		public virtual Receipt Receipt
		{
			get { return receipt; }
			set { receipt = value; }
		}

		private TreeListNode category;
		public virtual TreeListNode Category
		{
			get { return category; }
			set { category = value; }
		}

		private String detail;
		public virtual String Detail
		{
			get { return detail; }
			set { detail = value; }
		}

		private double units;
		public virtual double Units
		{
			get { return units; }
			set { units = value; }
		}

		private Money unitPrice;
		public virtual Money UnitPrice
		{
			get { return unitPrice; }
			set { unitPrice = value; }
		}

		private Money amount;
		public virtual Money Amount
		{
			get { return amount; }
			set { amount = value; }
		}

		protected User updatedBy;
		public virtual User UpdatedBy
		{
			get { return updatedBy; }
			set { updatedBy = value; }
		}

		protected DateTime updatedTS;
		public virtual DateTime UpdatedTS
		{
			get { return updatedTS; }
			set { updatedTS = value; }
		}

		public void RemoveReceiptItem(int ReceiptItemID)
		{
			int i = 0;
			int index = 0;
			foreach (ReceiptItem ri in ReceiptItems)
			{
				if (ri.ReceiptItemID == ReceiptItemID)
				{
					index = i;
					break;
				}
				i++;
			}
			ReceiptItems.RemoveAt(index);
		}
	}
}
