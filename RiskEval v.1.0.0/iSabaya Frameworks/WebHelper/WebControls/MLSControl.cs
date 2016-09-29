﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using iSabaya;

namespace WebHelper.Controls
{
    public enum MLSRequiredType
    {
        FullRequired,
        DefaultLanguage,
        NotRequired
    }
    [ToolboxData("<{0}:MLSControl runat=server></{0}:MLSControl>")]
    public class MLSControl : iSabayaWebControlBase
    {
        IList<ASPxButtonEdit> listTextBox = null;
        Table tbControl = null;
        HiddenField hddID = null;


        public string JSSetClientEnabled(bool enabled)
        {
            if (listTextBox == null)
                return string.Empty;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < listTextBox.Count; i++)
            {
                str.AppendLine(string.Format("{0}.SetEnabled({1});", listTextBox[i].ClientInstanceName, enabled.ToString()));
            }
            return str.ToString();
        }
        public string JSSetClientVisible(bool enabled)
        {
            if (listTextBox == null)
                return string.Empty;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < listTextBox.Count; i++)
            {
                str.AppendLine(string.Format("{0}.SetVisible({1});", listTextBox[i].ClientInstanceName, enabled.ToString()));
            }
            return str.ToString();
        }
        private MLSRequiredType requiredField = MLSRequiredType.DefaultLanguage;
        public MLSRequiredType RequiredField
        {
            get { return this.requiredField; }
            set { this.requiredField = value; }
        }
        private new bool IsRequiredField { get; set; }

        private bool clientEnabled = true;
        public bool ClientEnabled
        {
            get { return this.clientEnabled; }
            set { this.clientEnabled = value; }
        }

        private MultilingualString value;
        public MultilingualString Value
        {
            get
            {
                string code = string.Empty;
                if (hddID.Value == "0" || string.IsNullOrEmpty(hddID.Value))
                {
                    value = new MultilingualString();
                    for (int i = 0; i < listTextBox.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(listTextBox[i].Text))
                        {
                            code = listTextBox[i].ID.Replace('_', '-');
                            value.Values.Add(new MLSValue() { Owner = value, LanguageCode = code, Value = listTextBox[i].Text });
                        }
                    }
                }
                else
                {
                    value = iSabayaContext.PersistenceSession.Get<MultilingualString>(long.Parse(hddID.Value));
                    if (value == null)
                        value = new MultilingualString();
                    bool inserted;
                    for (int i = 0; i < listTextBox.Count; i++)
                    {
                        code = listTextBox[i].ID.Replace('_', '-');
                        inserted = false;
                        for (int j = 0; j < value.Values.Count; j++)
                        {
                            if (value.Values[j].LanguageCode == code)
                            {
                                value.Values[j].Value = listTextBox[i].Text;
                                inserted = true;
                                break;
                            }
                        }
                        if (!inserted && !string.IsNullOrEmpty(listTextBox[i].Text))
                            value.Values.Add(new MLSValue() { Owner = value, LanguageCode = code, Value = listTextBox[i].Text });
                    }
                }
                return value;
            }
            set
            {
                this.value = value;
                string code;
                EnsureControlList();
                if (value == null)
                {
                    hddID.Value = "0";
                    if (listTextBox != null)
                    {
                        for (int i = 0; i < listTextBox.Count; i++)
                        {
                            code = listTextBox[i].ID.Replace('_', '-');
                            listTextBox[i].Text = string.Empty;
                        }
                    }
                }
                else
                {
                    hddID.Value = value.MLSID.ToString();
                    if (listTextBox != null)
                    {
                        for (int i = 0; i < listTextBox.Count; i++)
                        {
                            code = listTextBox[i].ID.Replace('_', '-');
                            listTextBox[i].Text = this.value.GetValue(code);
                        }
                    }
                }
            }
        }
        public MLSControl()
        {
            tbControl = new Table() { CellPadding = 0, CellSpacing = 0 };
            hddID = new HiddenField();
            CreateControlList();

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
        }
        private void EnsureControlList()
        {
            if (iSabayaContext.Languages != null)
            {
                if (listTextBox == null || iSabayaContext.Languages.Count != listTextBox.Count)
                {
                    CreateControlList();
                    InitializeControls();
                    AddChildControls();
                }
            }
        }
        private void CreateControlList()
        {
            listTextBox = new List<ASPxButtonEdit>();
            string code = null;
            for (int i = 0; i < iSabayaContext.Languages.Count; i++)
            {
                code = iSabayaContext.Languages[i].Code;
                ASPxButtonEdit txt = new ASPxButtonEdit();
                txt.ID = code.Replace('-', '_');
                txt.ButtonStyle.Font.Bold = true;
                txt.ButtonStyle.Cursor = "pointer";
                EditButton btn = new EditButton();
                btn.Width = Unit.Pixel(15);
                btn.Text = code.Substring(0, code.IndexOf('-')).ToUpper();
                btn.Enabled = true;
                btn.ToolTip = iSabayaContext.Languages[i].Title;
                txt.ButtonStyle.Paddings.Padding = Unit.Pixel(0);
                txt.Buttons.Add(btn);
                listTextBox.Add(txt);
            }
        }
        private void InitializeControls()
        {
            ASPxButtonEdit txt;
            for (int i = 0; i < listTextBox.Count; i++)
            {
                txt = listTextBox[i];
                txt.Width = this.Width;
                txt.ClientEnabled = this.ClientEnabled;
                txt.ClientInstanceName = this.ClientID + txt.ID;
                txt.Border.Assign(this.Border);
                if (i > 0)
                    txt.BorderTop.BorderWidth = Unit.Pixel(1);
            }
        }

        protected override void CreateChildControls()
        {
            InitializeControls();
            AddChildControls();
        }

        private void AddChildControls()
        {
            this.Controls.Add(hddID);
            this.Controls.Add(tbControl);
            ASPxButtonEdit txt;
            for (int i = 0; i < listTextBox.Count; i++)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell()
                {
                    HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left,
                    VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Top
                });
                txt = listTextBox[i];
                row.Cells[0].Controls.Add(txt);
                row.Attributes.Add("style", "padding-bottom:2px");
                tbControl.Rows.Add(row);
            }
            switch (RequiredField)
            {
                case MLSRequiredType.FullRequired:
                    for (int i = 0; i < listTextBox.Count; i++)
                        listTextBox[i].SetValidation(ValidationGroup);
                    break;
                case MLSRequiredType.DefaultLanguage:
                    Language defaultLanguage = iSabayaContext.Configuration.DefaultLanguage;
                    if (defaultLanguage == null)
                        listTextBox[0].SetValidation(ValidationGroup);
                    else
                    {
                        for (int i = 0; i < listTextBox.Count; i++)
                        {
                            if (listTextBox[i].ID.Replace('_', '-') == defaultLanguage.Code)
                            {
                                listTextBox[i].SetValidation(ValidationGroup);
                                break;
                            }
                        }
                    }
                    break;
            }
        }
        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            object[] values = new object[1];
            values[0] = this.clientEnabled;
            return new Pair(obj, values);
        }

        protected override void LoadControlState(object savedState)
        {
            Pair p = (Pair)savedState;
            base.LoadControlState(p.First);
            object[] values = (object[])p.Second;
            this.clientEnabled = (bool)values[0];
        }
        public override void Focus()
        {
            if (listTextBox.Count > 0)
                listTextBox[0].Focus();
        }
    }
    
}