using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxGridView;

using iSabaya;
using imSabaya;

using WebHelper.Controls;

namespace WebHelper.Controls
{
    [ToolboxData("<{0}:QuestionnaireForm runat=server></{0}:QuestionnaireForm>")]
    public class QuestionnaireForm : iSabayaWebControlBase
    {
        private const string NAME_DELIMITER = "$";
        private const string SELECT_CODE = "C";
        private const string PREFIX_PAGEID = "p_";
        private const string POSTFIX_COMBOBOX_VALUE = "$DDD$L";
        protected Table tableMain;
        private delegate ASPxCheckBox CreateChoiceControl(ResponseChoice item, ChoiceQuestion q);
        private delegate int DetermineValueIndex(int mRow, int nCol);
        private delegate void RowOrCellSpan(Table table, TableCell cell, int nRow, int nCol);

        private PagerControl topPager = null;
        private PagerControl bottomPager = null;

        public iSabaya.Response ResponseForm
        {
            get
            {
                if (this.Page.Session[this.ClientID + "response"] == null)
                    return null;
                return (iSabaya.Response)this.Page.Session[this.ClientID + "response"];
            }
            set
            {
                this.Page.Session[this.ClientID + "response"] = value;
            }
        }

        private System.Web.UI.WebControls.HorizontalAlign pagerAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
        public System.Web.UI.WebControls.HorizontalAlign PagerAlign
        {
            get { return this.pagerAlign; }
            set { this.pagerAlign = value; }
        }

        public Unit FormWidth
        {
            get { return tableMain.Width; }
            set { tableMain.Width = value; }
        }

        private Unit editorWidth = Unit.Pixel(170);
        public Unit EditorWidth
        {
            get { return this.editorWidth; }
            set { this.editorWidth = value; }
        }

        public bool ReadOnly { get; set; }

        public string ClientInstanceName
        {
            get { return this.ClientInstanceNameInternal; }
            set { this.ClientInstanceNameInternal = value; }
        }

        //private StringBuilder checkListValidationScript;
        //protected StringBuilder CheckListValidationScript
        //{
        //    get
        //    {
        //        if (checkListValidationScript == null)
        //            checkListValidationScript = new StringBuilder();
        //        return checkListValidationScript;
        //    }
        //    set { checkListValidationScript = value; }
        //}

        //        private string ValidationClientScript
        //        {
        //            get
        //            {
        //                return @"function()
        //                { 
        //                    var layout;
        //                    var isValid = true;
        //                    var groupValid;
        //
        //                    var chkGroup;
        //                    var layoutIDs = new Array();
        //                    var chks = new Array();
        //                    " + this.CheckListValidationScript.ToString() + @"
        //                    var element;
        //                    
        //                    for(var i = 0; i < layoutIDs.length; i++)
        //                    {
        //                        element = document.getElementById(layoutIDs[i]);
        //                        if(element != null && element.style.display != 'none')
        //                        {
        //                            chkGroup = chks[i];
        //                            groupValid = false;
        //                            for(var j = 0; j < chkGroup.length; j++)
        //                            {
        //                                groupValid = groupValid | chkGroup[j].GetChecked();
        //                                
        //                            }
        //                            isValid = isValid & groupValid;
        //                            if(!groupValid)
        //                            {
        //                                element.style.border = 'thin solid #FF0000';
        //                                element.scrollIntoView();
        //                            }
        //                            else
        //                            {
        //                                element.style.border = '';
        //                                element.style.borderBottom = 'thin solid LightGray';
        //                            }
        //                        }
        //                    }
        //                    var edit = ASPxClientEdit.ValidateGroup('" + ValidationGroup + @"');
        //                    alert(isValid + ': ' + edit)
        //                    isValid = isValid & edit;
        //                    return isValid
        //                }";
        //            }
        //        }

        protected new string ValidationGroup { get; set; }

        public QuestionnaireForm()
        {
            if (this.Page == null)
                this.Page = this.Context.Handler as Page;
            tableMain = new Table();
            this.Controls.Add(tableMain);

            topPager = new PagerControl()
            {
                LayoutWidth = Unit.Percentage(100),
            };
            bottomPager = new PagerControl()
            {
                LayoutWidth = Unit.Percentage(100),
            };
        }
        #region Questions

        /// <summary>
        /// Create html span containner for text
        /// </summary>
        /// <param name="mls">mls value</param>
        /// <returns>span continner present text</returns>
        private Control CreateTextLabel(MultilingualString mls)
        {
            return CreateTextLabel(mls, null);
        }
        private Control CreateTextLabel(MultilingualString mls, string prefix)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.Style.Add(HtmlTextWriterStyle.Padding, "2px");
            if (mls != null)
                span.InnerHtml = (prefix + " " + mls.ToString(base.LanguageCode)).Trim();
            span.Attributes.Add("class", "dxeBase");

            return span;
        }

        private DetermineValueIndex GetValueIndexMethod(LayoutStyle memberLayout, int columns, int rows)
        {
            DetermineValueIndex cal = delegate(int mRow, int nCol) { return (columns * mRow) + nCol; };

            if (memberLayout != null)
            {
                if (!memberLayout.FillVerticalThenHorizontal)
                {
                    if (memberLayout.FillRightToLeft)
                        cal = delegate(int mRow, int nCol) { return (columns * mRow) + (columns - nCol - 1); };
                }
                else
                {
                    if (!memberLayout.FillRightToLeft)
                        cal = delegate(int mRow, int nCol) { return (rows * nCol) + mRow; };
                    else
                        cal = delegate(int mRow, int nCol) { return (rows * (columns - nCol - 1)) + mRow; };
                }
            }
            return cal;
        }
        /// <summary>
        /// Determine span method for row or column span , if columns > 1
        /// </summary>
        /// <param name="memberLayout"></param>
        /// <param name="width">width cell</param>
        /// <returns>row or cell span delegate method</returns>
        private RowOrCellSpan GetSpanMethod(LayoutStyle memberLayout)
        {
            RowOrCellSpan method = delegate(Table tb, TableCell cell, int m, int n)
            {
                if (tb.Rows[m] != null && tb.Rows[m].Cells[tb.Rows[m].Cells.Count - 1] != null) // default horizontal, to right
                {
                    TableCell spanCell = tb.Rows[m].Cells[tb.Rows[m].Cells.Count - 1];
                    if (spanCell.ColumnSpan == 0)
                        spanCell.ColumnSpan = 2;
                    else
                        spanCell.ColumnSpan += 1;
                    //spanCell.Width = Unit.Percentage(width + spanCell.Width.Value);
                    cell = null;
                }
            };
            if (memberLayout != null)
            {
                if (!memberLayout.FillVerticalThenHorizontal && memberLayout.FillRightToLeft)
                {
                    method = delegate(Table tb, TableCell cell, int m, int n)
                    {
                        cell.ColumnSpan += 1;
                    };
                }
                else if (memberLayout.FillVerticalThenHorizontal)
                {
                    method = delegate(Table tb, TableCell cell, int m, int n)
                    {
                        if (tb.Rows[m - 1] != null && tb.Rows[m - 1].Cells[n] != null)
                        {
                            TableCell spanCell = tb.Rows[m - 1].Cells[n];
                            if (spanCell.RowSpan == 0)
                                spanCell.RowSpan = 2;
                            else
                                spanCell.RowSpan += 1;
                            cell = null;
                        }
                    };
                }
            }
            return method;
        }
        private LayoutStyle GetMemberStyleFromParent(object parent)
        {
            LayoutStyle layout = null;
            QuestionBase q = parent as QuestionBase;
            if (q != null)
            {
                layout = q.MemberLayout;
                if (layout != null)
                    return layout;
                layout = GetMemberStyleFromParent(q.Parent);
            }
            return layout;
        }
        private TableRow GetLastesRow(Table table, LayoutStyle memberLayout)
        {
            if ((memberLayout != null && memberLayout.StartOnTheNextRow) || table.Rows.Count == 0)
                table.Rows.Add(new TableRow());
            return table.Rows[table.Rows.Count - 1];
        }

        private void SetCellBorderStyle(TableCell cell, LineStyle top, LineStyle right, LineStyle bottom, LineStyle left)
        {
            if (top != null)
                cell.Style.Add("border-top", LineStyleToString(cell, top));
            if (right != null)
                cell.Style.Add("border-right", LineStyleToString(cell, right));
            if (bottom != null)
                cell.Style.Add("border-bottom", LineStyleToString(cell, bottom));
            if (left != null)
                cell.Style.Add("border-left", LineStyleToString(cell, left));
        }
        private static string LineStyleToString(TableCell cell, LineStyle s)
        {
            StringBuilder borderStyle = new StringBuilder();
            if (!s.Width.IsEmpty)
                borderStyle.Append(" " + s.Width.ToString());
            borderStyle.Append(" " + s.WebControlBorderStyle.ToString().ToLower());
            if (s.Color != null && !s.Color.IsEmpty)
                borderStyle.Append(" " + System.Drawing.ColorTranslator.ToHtml(s.Color));
            return borderStyle.ToString();
        }

        private void SetMemberStyle(TableCell cell, LayoutStyle memberStyle)
        {
            if (cell != null && memberStyle != null)
            {
                cell.HorizontalAlign = (System.Web.UI.WebControls.HorizontalAlign)Enum.Parse(typeof(System.Web.UI.WebControls.HorizontalAlign),
                            memberStyle.HorizontalAlignment.ToString());
                cell.VerticalAlign = (System.Web.UI.WebControls.VerticalAlign)Enum.Parse(typeof(System.Web.UI.WebControls.VerticalAlign),
                                            memberStyle.VerticalAlignment.ToString());
                cell.Style.Add(HtmlTextWriterStyle.PaddingRight, memberStyle.ColumnRightPadding.ToString() + "px");
            }
        }
        private void SetVisualStyle(TableCell cell, VisualStyle style)
        {
            if (style != null && cell != null)
            {
                cell.HorizontalAlign = (System.Web.UI.WebControls.HorizontalAlign)Enum.Parse(typeof(System.Web.UI.WebControls.HorizontalAlign),
                                        style.HorizontalAlignment.ToString());
                cell.VerticalAlign = (System.Web.UI.WebControls.VerticalAlign)Enum.Parse(typeof(System.Web.UI.WebControls.VerticalAlign),
                                        style.VerticalAlignment.ToString());
                style.SetStyle(cell.ControlStyle);

                //if (null != style.Font)
                //{
                //    cell.Font.Bold = style.Font.Bold;
                //    cell.Font.Italic = style.Font.Italic;
                //    if (style.Font.Name != null)
                //        cell.Font.Name = style.Font.Name;
                //    cell.Font.Size = style.Font.Size;
                //    cell.Font.Strikeout = style.Font.Strikeout;
                //    cell.Font.Underline = style.Font.Underline;
                //}

                if (null != style.Border)
                {
                    this.SetCellBorderStyle(cell, style.Border.TopLineStyle, style.Border.RightLineStyle,
                        style.Border.BottomLineStyle, style.Border.LeftLineStyle);

                    //if (style.Border.BackColor != null && !style.Border.BackColor.IsEmpty)
                    //    cell.ControlStyle.BackColor = style.Border.BackColor;
                    //if (style.Border.ForeColor != null && !style.Border.ForeColor.IsEmpty)
                    //    cell.ControlStyle.ForeColor = style.Border.ForeColor;
                    //if (!style.Border.Width.IsEmpty)
                    //    cell.Width = style.Border.Width;
                    //if (!style.Border.Height.IsEmpty)
                    //    cell.Height = style.Border.Height;
                }
            }
        }
        private void SetRowStyle(Table table, VisualStyle s, int rows)
        {
            if (s != null)
            {
                for (int i = table.Rows.Count - rows; i < table.Rows.Count; i++)
                    s.SetStyle(table.Rows[i].ControlStyle);
            }
        }
        private void SetRowStyle(Table table, LayoutStyle memberLayout, int rows, int childrenNo)
        {
            //assume table rows > 0
            if (memberLayout == null)
                return;
            if (memberLayout.RowsPerStyle < 1)
            {
                SetRowStyle(table, memberLayout.MainVisualStyle, rows);
            }
            else
            {
                bool isAlternate = ((childrenNo / memberLayout.RowsPerStyle) % 2) == 1;
                if (!isAlternate)
                {
                    if (memberLayout != null)
                        SetRowStyle(table, memberLayout.MainVisualStyle, rows);
                }
                else
                {
                    if (memberLayout != null)
                    {
                        if (memberLayout.AlternateVisualStyle != null)
                            SetRowStyle(table, memberLayout.AlternateVisualStyle, rows);
                        else
                            SetRowStyle(table, memberLayout.MainVisualStyle, rows);
                    }
                }
            }
        }
        private void SetReadOnlyStyle(ASPxCheckBox control)
        {
            control.ReadOnly = this.ReadOnly;
            if (!control.Checked)
                control.ReadOnlyStyle.ForeColor = System.Drawing.Color.Gray;
        }
        private void SetReadOnlyStyle(ASPxComboBox control)
        {
            control.ReadOnly = this.ReadOnly;
            control.DropDownButton.Enabled = !this.ReadOnly;
        }
        private void SetReadOnlyStyle(ASPxDateEdit control)
        {
            control.ReadOnly = this.ReadOnly;
            control.DropDownButton.Enabled = !this.ReadOnly;
        }
        private void SetReadOnlyStyle(ASPxTextBox control)
        {
            control.ReadOnly = this.ReadOnly;
        }

        private void AddValueControl(TableCell cell, Control c, QuestionBase q, MultilingualString suffix)
        {
            LayoutStyle memberLayout = this.GetMemberStyleFromParent(q.Parent);
            Control additionalControl = null;
            if (suffix != null)
            {

                if (memberLayout == null || memberLayout.AlignSuffixes)
                {
                    // add control
                    Table tb = new Table();
                    TableRow row = new TableRow();
                    tb.Rows.Add(row);
                    row.Cells.Add(new TableCell());
                    row.Cells[0].Controls.Add(c);
                    // add suffix
                    TableCell suffixCell = new TableCell();
                    this.AddCellToRow(this.GetLastesRow(tb, memberLayout), suffixCell, memberLayout);
                    suffixCell.Controls.Add(this.CreateTextLabel(suffix));
                    c = tb;
                }
                else
                {
                    // value control
                    HtmlGenericControl spanItem = new HtmlGenericControl("span");
                    spanItem.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                    spanItem.Controls.Add(c);
                    c = spanItem;
                    // suffix control
                    additionalControl = this.CreateTextLabel(suffix);
                }
            }

            if (memberLayout == null || memberLayout.AlignValues)
            {
                this.AddControlToCell(cell, c, memberLayout);
                if (additionalControl != null)
                    this.AddControlToCell(cell, additionalControl, memberLayout);
            }
            else
            {
                if (additionalControl == null)
                {
                    if (c is Table)
                        ((Table)c).Style.Add("float", "left");
                    this.AddControlToCell(cell, c, memberLayout);
                }
                else
                {
                    this.AddControlToCell(cell, c, memberLayout);
                    this.AddControlToCell(cell, additionalControl, memberLayout);
                }
            }
        }
        private void AddCellToRow(TableRow row, TableCell cell, LayoutStyle memberLayout)
        {
            if (memberLayout == null || memberLayout.TextAlignment == iSabaya.TextAlign.Right)
                row.Cells.Add(cell);
            else
                row.Cells.AddAt(0, cell);
        }
        private void AddControlToCell(TableCell cell, Control control, LayoutStyle memberLayout)
        {
            if (memberLayout == null || memberLayout.TextAlignment == iSabaya.TextAlign.Right)
                cell.Controls.Add(control);
            else
                cell.Controls.AddAt(0, control);
        }

        private TableCell PrepareTableCellControl(Table table, QuestionBase q)
        {
            TableRow row = new TableRow();
            table.Rows.Add(row);

            TableCell cell = null;
            ASPxLabel lblTittle = null;
            LayoutStyle memberLayout = this.GetMemberStyleFromParent(q.Parent);

            // Title cell
            if (q.TitleIsVisible && q.Title != null)
            {
                cell = new TableCell();
                row.Cells.Add(cell);

                if (q is QuestionGroup || q is ChoiceQuestion)
                {
                    cell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                    cell.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Top;
                }
                cell.Style.Add(HtmlTextWriterStyle.Padding, "3px");

                lblTittle = new ASPxLabel();
                if (memberLayout != null && memberLayout.ItemNoIsVisible)
                    lblTittle.Text = q.ItemNo + " " + q.Title.ToString(base.LanguageCode);
                else
                    lblTittle.Text = q.Title.ToString(base.LanguageCode);
                cell.Controls.Add((lblTittle));
            }

            // separate title, value, suffix into each table cell
            if (memberLayout == null || memberLayout.AlignValues)
            {
                // Title
                if (cell != null)
                {
                    cell.Style.Add(HtmlTextWriterStyle.WhiteSpace, "pre");
                    this.SetVisualStyle(cell, q.TitleStyle);
                    if (memberLayout != null && memberLayout.TitleWidth > 0)
                        cell.Width = Unit.Pixel(memberLayout.TitleWidth);
                }
                // Rubric cell
                if (q.Rubric != null)
                {
                    if (q.RubricIsVisible)
                    {
                        cell = new TableCell();
                        this.AddCellToRow(this.GetLastesRow(table, memberLayout), cell, memberLayout);
                        this.SetVisualStyle(cell, q.RubricStyle);
                        cell.Controls.Add(this.CreateTextLabel(q.Rubric));
                        cell.Style.Add(HtmlTextWriterStyle.WhiteSpace, "pre");
                        cell.Style.Add(HtmlTextWriterStyle.Padding, "3px");
                    }
                    else
                        lblTittle.ToolTip = q.Rubric.ToString(base.LanguageCode);
                }
                // Control cell
                cell = new TableCell()
                {
                    ID = "td_" + topPager.PageCount + "_" + q.ID.ToString(),
                };
                this.AddCellToRow(this.GetLastesRow(table, memberLayout), cell, memberLayout);
                if (!(q is QuestionGroup))
                    cell.Style.Add(HtmlTextWriterStyle.Padding, "3px");
                if (memberLayout != null)
                    this.SetVisualStyle(cell, memberLayout.MainVisualStyle);
                cell.Width = Unit.Percentage(99);
                // column span lasted cell
                if (memberLayout == null || memberLayout.Columns <= 1)
                {
                    if (memberLayout != null && !memberLayout.StartOnTheNextRow)
                        cell.ColumnSpan = 3 - row.Cells.Count;
                }
            }
            else
            {
                if (cell == null)
                {
                    cell = new TableCell() { Width = Unit.Percentage(100), };
                    row.Cells.Add(cell);
                    cell.Style.Add(HtmlTextWriterStyle.Padding, "3px");
                }
                cell.ID = "td_" + topPager.PageCount + "_" + q.ID.ToString();
                if (memberLayout != null)
                    this.SetVisualStyle(cell, memberLayout.MainVisualStyle);

                if (lblTittle != null)
                {
                    //cell.Controls.Add(lblTittle);
                    if (q.Rubric != null)
                        lblTittle.ToolTip = q.Rubric.ToString(base.LanguageCode);
                }
            }
            return cell;
        }

        private ASPxCheckBox CreateCheckboxChoice(ResponseChoice item, ChoiceQuestion q)
        {
            QuestionChoice qc = item.QuestionChoice;
            ASPxCheckBox chk = new ASPxCheckBox()
            {
                ID = q.ID.ToString() + "_" + qc.ID.ToString(),
                Value = qc.ID,
                Layout = RepeatLayout.OrderedList,
            };
            chk.Checked = item.IsSelected;
            this.SetReadOnlyStyle(chk);
            return chk;
        }
        private ASPxCheckBox CreateRadioChoice(ResponseChoice item, ChoiceQuestion q)
        {
            QuestionChoice qc = item.QuestionChoice;
            ASPxRadioButton rdo = new ASPxRadioButton()
            {
                ID = q.ID.ToString() + "_" + qc.ID.ToString(),
                Value = qc.ID,
                GroupName = q.ID.ToString(),
            };
            rdo.Checked = item.IsSelected;
            rdo.ReadOnly = this.ReadOnly;
            this.SetReadOnlyStyle(rdo);
            return rdo;
        }

        private void CreateCheckListQuestion(Table table, ChoiceResponse r)
        {
            ChoiceQuestion q = r.Question;
            TableCell cell = PrepareTableCellControl(table, q);
            CreateChoiceControl createControlMethod = null;
            if (q.AllowMultipleSelections)
                createControlMethod = CreateCheckboxChoice;
            else
                createControlMethod = CreateRadioChoice;
            // ===================== item table ================================
            Table tb = new Table()
            {
                CellSpacing = 0,
                CellPadding = 0,
                //Width = Unit.Percentage(100)
            };
            //tb.Style.Add(HtmlTextWriterStyle.Padding, "2px");

            cell.Controls.Add(tb);
            TableRow tr = null;
            TableCell td = null;


            ResponseChoice responseChoice = null;
            //int count = q.Choices.Count<QuestionChoice>();
            int count = r.Choices.Count;
            //string[] chkClientNames = new string[count];

            int columns = 1;
            if (q.MemberLayout != null)
                columns = q.MemberLayout.Columns;

            int rows = (int)Math.Ceiling((double)count / (double)columns);
            //double width = 100 / columns;
            int m, n, indexValue;
            DetermineValueIndex cal = GetValueIndexMethod(q.MemberLayout, columns, rows);
            RowOrCellSpan spanMethod = GetSpanMethod(q.MemberLayout);
            int Cells = count;
            if (columns > 0)
                Cells = rows * columns; // full cell
            for (int i = 0; i < Cells; i++)
            {
                if (columns > 0)
                {
                    n = i % columns;
                    m = i / columns;
                }
                else
                {
                    n = i;
                    m = 0;
                }
                indexValue = cal(m, n);
                if (n == 0)
                {
                    tr = new TableRow();
                    tb.Rows.Add(tr);
                }

                if (indexValue < count)
                {
                    if (td == null)
                    {
                        td = new TableCell()
                        {
                            //Width = Unit.Percentage(width),
                            VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Top,
                            HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left,
                        };
                        this.SetMemberStyle(td, q.MemberLayout);
                        tr.Cells.Add(td);
                    }

                    QuestionChoice item = r.Choices[indexValue].QuestionChoice;
                    ASPxCheckBox c = createControlMethod(r.Choices[indexValue], q);
                    c.Text = item.Title.ToString(base.LanguageCode);
                    // item style
                    if (item.TitleStyle != null)
                        item.TitleStyle.SetStyle(c.ControlStyle);

                    if (q.MemberLayout != null)
                        c.TextAlign = (System.Web.UI.WebControls.TextAlign)Enum.Parse(typeof(System.Web.UI.WebControls.TextAlign),
                                        q.MemberLayout.TextAlignment.ToString());
                    // rubric
                    if (item.Rubric != null)
                        c.ToolTip = item.Rubric.ToString(base.LanguageCode);

                    //========================= add controls =================================
                    responseChoice = r.Choices[indexValue];

                    if (item.FurtherQuestion == null)
                        td.Controls.Add(c);
                    else
                    {
                        Table innerQTable = new Table()
                        {
                            CellPadding = 0,
                            CellSpacing = 0,
                        };

                        td.Controls.Add(innerQTable);

                        TableRow innerQRow = new TableRow();
                        innerQTable.Rows.Add(innerQRow);
                        // =============== add item check box =========================
                        TableCell innerQCell = new TableCell()
                        {
                            VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Top,
                            HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left,
                        };
                        innerQRow.Cells.Add(innerQCell);
                        innerQCell.Controls.Add(c);

                        innerQCell.Style.Add(HtmlTextWriterStyle.PaddingTop, "3px");
                        this.SetMemberStyle(innerQCell, q.MemberLayout);

                        // =============== add table futher question =====================
                        innerQCell = new TableCell()
                        {
                            ID = string.Format("td_{0}_{1}", item.ID, item.FurtherQuestion.ID),
                            VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle,
                            HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left,
                        };
                        innerQRow = this.GetLastesRow(innerQTable, q.MemberLayout);
                        innerQRow.Cells.Add(innerQCell);

                        innerQCell.Style.Add(HtmlTextWriterStyle.PaddingLeft, "3px");
                        this.SetMemberStyle(innerQCell, q.MemberLayout);

                        Table fqTable = new Table()
                        {
                            //ID = string.Format("tb_{0}_{1}", item.ID, item.FurtherQuestion.ID),
                            ID = "fq_" + item.ID,
                            CellPadding = 0,
                            CellSpacing = 0,
                            Width = Unit.Percentage(100),
                        };
                        innerQCell.Controls.Add(fqTable);
                        if (!responseChoice.IsSelected)
                            innerQCell.Style.Add(HtmlTextWriterStyle.Display, "none");
                        else
                            innerQCell.Style.Add(HtmlTextWriterStyle.Display, "");
                        fqTable.Style.Add(HtmlTextWriterStyle.PaddingLeft, "4px");
                        CreateQuestionForm(fqTable, responseChoice.FurtherResponse);

                        c.ClientSideEvents.CheckedChanged = @"function(s,e)
                        {
                            var element = document.getElementById('" + innerQCell.ClientID + @"');
                            if(element != null)
                            {
                                if(s.GetChecked())
                                    element.style.display = '';
                                else
                                    element.style.display = 'none';
                            }
                        }";
                    }
                    c.ClientInstanceName = c.ClientID;
                    //chkClientNames[indexValue] = c.ClientInstanceName;
                    td = null;
                }
                else
                    spanMethod(tb, td, m, n);
            }
            //            if (count > 0)
            //            {
            //                string str = @"
            //                    layoutIDs[layoutIDs.length] = '" + cell.ClientID + @"';
            //                    var chkGroup = new Array(" + string.Join(",",chkClientNames) + @");
            //                    chks[chks.length] = chkGroup;
            //                    ";
            //                CheckListValidationScript.Insert(0, str);
            //            }
        }
        private void CreateComboBoxQuestion(Table table, ChoiceResponse r)
        {
            ChoiceQuestion q = r.Question;
            TableCell cell = PrepareTableCellControl(table, q);
            LayoutStyle memberLayout = q.MemberLayout;
            ASPxComboBox cbo = new ASPxComboBox()
            {
                ID = q.ID.ToString(),
                EnableAnimation = false,
            };
            cbo.SetValidation(ValidationGroup);
            cbo.ValidationSettings.ValidateOnLeave = true;
            cbo.ValueType = typeof(int);
            this.SetReadOnlyStyle(cbo);

            Table tb = new Table() { CellSpacing = 0, CellPadding = 0 };
            tb.Rows.Add(new TableRow());
            tb.Rows[0].Cells.Add(new TableCell());
            tb.Rows[0].Cells[0].Controls.Add(cbo);
            cell.Controls.Add(tb);

            List<ResponseChoice> responseFurther = new List<ResponseChoice>();
            QuestionChoice choice = null;
            foreach (ResponseChoice item in r.Choices)
            {
                choice = item.QuestionChoice;
                cbo.Items.Add(choice.Title.ToString(base.LanguageCode), choice.ID);
                if (item.IsSelected)
                    cbo.Items[cbo.Items.Count - 1].Selected = true;
                if (choice.FurtherQuestion != null)
                    responseFurther.Add(item);
            }
            if (responseFurther.Count > 0)
            {
                cbo.ClientSideEvents.SelectedIndexChanged = @"function(s,e){
                    var element;
                    var value = s.GetSelectedItem().value;
                ";
                TableRow row = this.GetLastesRow(tb, q.MemberLayout);
                cell = new TableCell();
                row.Cells.Add(cell);

                Table fTable = new Table() { CellPadding = 0, CellSpacing = 0, };
                cell.Controls.Add(fTable);
                fTable.Style.Add(HtmlTextWriterStyle.BorderCollapse, "collapse");
                fTable.Rows.Add(new TableRow());

                TableCell fCell = null;
                string[] elementIDs = new string[responseFurther.Count];
                string[] itemIDs = new string[responseFurther.Count];
                ResponseChoice item;
                for (int i = 0; i < responseFurther.Count; i++)
                {
                    item = responseFurther[i];
                    choice = item.QuestionChoice;
                    fCell = new TableCell() { ID = q.ID + "_" + choice.ID };
                    fTable.Rows[0].Cells.Add(fCell);
                    tb = new Table();
                    fCell.Controls.Add(tb);

                    elementIDs[i] = fCell.ClientID;
                    itemIDs[i] = choice.ID.ToString();

                    if (!item.IsSelected)
                        fCell.Style.Add(HtmlTextWriterStyle.Display, "none");
                    else
                        fCell.Style.Add(HtmlTextWriterStyle.Display, "");

                    CreateQuestionForm(tb, item.FurtherResponse);
                }
                cbo.ClientSideEvents.SelectedIndexChanged += @"
                var elements = new Array('" + string.Join("','", elementIDs) + @"');
                var itemIDs = new Array('" + string.Join("','", itemIDs) + @"');
                for(var i = 0; i < elements.length; i++)
                    {
                        element = document.getElementById(elements[i]);
                        if(value == itemIDs[i])
                            element.style.display = '';
                        else
                            element.style.display = 'none';
                    }
                }";
            }
        }
        private void CreateQuestionForm(Table table, ChoiceResponse r)
        {
            ChoiceQuestion q = r.Question;
            if (q.AllowMultipleSelections || q.MemberLayout == null || q.MemberLayout.ControlType != iSabaya.ControlType.ComboBox)
                this.CreateCheckListQuestion(table, r);
            else
                this.CreateComboBoxQuestion(table, r);
        }

        private void CreateQuestionForm(Table table, DateValueResponse r)
        {
            DateValueQuestion q = r.Question;
            TableCell cell = PrepareTableCellControl(table, q);
            WebHelper.Controls.DateTimeControl ctrl = new WebHelper.Controls.DateTimeControl();
            ctrl.ID = q.ID.ToString();
            this.SetReadOnlyStyle(ctrl);

            ctrl.SetValidation(ValidationGroup);
            if (r != null && r.ID > 0)
                ctrl.Date = r.ResponseValue;
            if (q.RubricIsVisible && q.Rubric != null)
                ctrl.ToolTip = q.Rubric.ToString(base.LanguageCode);
            this.AddValueControl(cell, ctrl, q, q.Suffix);
        }
        private void CreateQuestionForm(Table table, IntegerValueResponse r)
        {
            IntegerValueQuestion q = r.Question;
            TableCell cell = PrepareTableCellControl(table, q);
            ASPxTextBox txt = new ASPxTextBox()
            {
                ID = q.ID.ToString(),
                Width = this.EditorWidth,
            };
            txt.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
            txt.SetValidation(ValidationGroup);
            txt.ValidationSettings.ValidateOnLeave = true;
            this.SetReadOnlyStyle(txt);
            txt.ClientSideEvents.Validation = @"function(s,e)
            {
                var value = s.GetText()*1;
                var lowerBound = " + q.LowerBound + @";
                var upperBound = " + q.UpperBound + @";
                e.isValid = true;
                if(value < lowerBound)
                {
                    e.isValid = false;
                    e.errorText = '" + q.Title.ToString(base.LanguageCode) + @"' + ' < ' + lowerBound; 
                }
                else if(value > upperBound)
                {
                    e.isValid = false;
                    e.errorText = '" + q.Title.ToString(base.LanguageCode) + @"' + ' > ' + upperBound; 
                }
            }";
            txt.ClientSideEvents.KeyPress = @"function(s, e) 
            {
                if( (e.htmlEvent.keyCode == 189 || e.htmlEvent.keyCode == 109) || 
                    (e.htmlEvent.keyCode >= 0 && e.htmlEvent.keyCode <= 57) ||
                    (e.htmlEvent.keyCode >= 96 && e.htmlEvent.keyCode <= 105) 
                { 
                    return true;
                }
                else 
                { 
                    return _aspxPreventEvent(e.htmlEvent);
                }
            }";
            if (q.MemberLayout != null && q.MemberLayout.TextAlignment == iSabaya.TextAlign.Right)
                txt.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            if (r != null && r.ID > 0)
                txt.Text = r.ResponseValue.ToString();
            else
                txt.Text = q.DefaultValue.ToString();
            this.AddValueControl(cell, txt, q, q.Suffix);
            if (q.RubricIsVisible && q.Rubric != null)
                txt.ToolTip = q.Rubric.ToString(base.LanguageCode);
        }
        private void CreateQuestionForm(Table table, RealValueResponse r)
        {
            RealValueQuestion q = r.Question;
            TableCell cell = PrepareTableCellControl(table, q);

            ASPxTextBox txt = new ASPxTextBox()
            {
                ID = q.ID.ToString(),
                Width = this.EditorWidth,
            };
            txt.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
            txt.SetValidation(ValidationGroup);
            txt.ValidationSettings.ValidateOnLeave = true;
            this.SetReadOnlyStyle(txt);
            txt.ClientSideEvents.Validation = @"function(s,e)
            {
                var value = s.GetText()*1;
                var lowerBound = " + q.LowerBound + @";
                var upperBound = " + q.UpperBound + @";
                e.isValid = true;
                if(value < lowerBound)
                {
                    e.isValid = false;
                    e.errorText = '" + q.Title.ToString(base.LanguageCode) + @"' + ' < ' + lowerBound; 
                }
                else if(value > upperBound)
                {
                    e.isValid = false;
                    e.errorText = '" + q.Title.ToString(base.LanguageCode) + @"' + ' > ' + upperBound; 
                }
            }";
            txt.ClientSideEvents.KeyDown = @"function(s, e) 
            {
                if( (e.htmlEvent.keyCode == 189 || e.htmlEvent.keyCode == 109 || 
                    e.htmlEvent.keyCode == 9 || e.htmlEvent.keyCode == 13 || 
                    e.htmlEvent.keyCode == 45 || e.htmlEvent.keyCode == 46 || 
                    e.htmlEvent.keyCode == 27 || e.htmlEvent.keyCode == 8)||
                    (e.htmlEvent.keyCode >= 16 && e.htmlEvent.keyCode <= 20) ||
                    (e.htmlEvent.keyCode >= 33 && e.htmlEvent.keyCode <= 40) ||
                    (e.htmlEvent.keyCode >= 48 && e.htmlEvent.keyCode <= 57) ||
                    (e.htmlEvent.keyCode >= 96 && e.htmlEvent.keyCode <= 105) ) 
                {
                    return true;
                }
                else if(e.htmlEvent.keyCode == 110 || e.htmlEvent.keyCode == 190)
                {
                    if(s.GetText().indexOf('.') > 0)
                        return _aspxPreventEvent(e.htmlEvent);
                    else
                        return true;
                }
                else
                { 
                    return _aspxPreventEvent(e.htmlEvent);
                }
            }";
            if (q.MemberLayout != null && q.MemberLayout.TextAlignment == iSabaya.TextAlign.Right)
                txt.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            if (r != null && r.ID > 0)
                txt.Text = r.ResponseValue.ToString();
            else
                txt.Text = q.DefaultValue.ToString();

            this.AddValueControl(cell, txt, q, q.Suffix);
            if (q.RubricIsVisible && q.Rubric != null)
                txt.ToolTip = q.Rubric.ToString(base.LanguageCode);
        }
        private void CreateQuestionForm(Table table, TextValueResponse r)
        {
            TextValueQuestion q = r.Question;
            TableCell cell = PrepareTableCellControl(table, q);
            ASPxTextBox txt = new ASPxTextBox()
            {
                ID = q.ID.ToString(),
                Width = this.EditorWidth,
            };
            this.SetReadOnlyStyle(txt);
            txt.Style.Add(HtmlTextWriterStyle.Display, "inline-block");

            if (q.LowerBound > 0)
            {
                txt.SetValidation(ValidationGroup);
                txt.ValidationSettings.ValidateOnLeave = true;
            }
            if (q.MemberLayout != null && q.MemberLayout.TextAlignment == iSabaya.TextAlign.Right)
                txt.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            if (r != null && r.ID > 0)
                txt.Value = r.ResponseValue;
            else
                txt.Value = q.DefaultValue;
            this.AddValueControl(cell, txt, q, q.Suffix);

            if (q.RubricIsVisible && q.Rubric != null)
                txt.ToolTip = q.Rubric.ToString(base.LanguageCode);
        }
        private void CreateQuestionForm(Table table, EmptyResponse r)
        {
            Comment c = r.Question;
            if (c.Title != null && c.TitleIsVisible)
            {
                LayoutStyle s = this.GetMemberStyleFromParent(c.Parent);
                TableRow row = table.Rows[table.Rows.Count - 1];
                if (table.Rows.Count == 0 || (s != null && s.Columns == row.Cells.Count))
                {
                    row = new TableRow();
                    table.Rows.Add(row);
                }
                TableCell cell = new TableCell();
                cell.Style.Add(HtmlTextWriterStyle.Padding, "3px");
                this.SetVisualStyle(cell, c.TitleStyle);
                row.Cells.Add(cell);
                cell.Controls.Add(this.CreateTextLabel(c.Title));
            }
        }
        private void CreateQuestionForm(Table table, MoneyValueResponse r)
        {
            MoneyValueQuestion q = r.Question;
            TableCell cell = this.PrepareTableCellControl(table, q);
            MoneyControl ctrl = new MoneyControl();
            ctrl.ID = q.ID.ToString();
            ctrl.IsRequiredField = true;
            ctrl.ValidationGroup = ValidationGroup;
            ctrl.LowerBound = q.LowerBound;
            ctrl.UpperBound = q.UpperBound;
            ctrl.Value = q.DefaultValue;
            ctrl.ReadOnly = this.ReadOnly;
            ctrl.AmountReadOnlyStyle.BackColor = System.Drawing.Color.LightGray;
            ctrl.CurrencyReadOnlyStyle.BackColor = System.Drawing.Color.LightGray;

            if (q.RubricIsVisible && q.Rubric != null)
                ctrl.ToolTip = q.Rubric.ToString(base.LanguageCode);
            this.AddValueControl(cell, ctrl, q, q.Suffix);
        }

        /// <summary>
        /// Create question form by question type
        /// </summary>
        /// <param name="table">containner table present question</param>
        /// <param name="r">response base from further question</param>
        private void CreateQuestionForm(Table table, ResponseBase r)
        {
            QuestionBase q = r.Question;
            if (q != null)
            {
                if (q is ChoiceQuestion)
                    CreateQuestionForm(table, (ChoiceResponse)r);
                else if (q is DateValueQuestion)
                    CreateQuestionForm(table, (DateValueResponse)r);
                else if (q is IntegerValueQuestion)
                    CreateQuestionForm(table, (IntegerValueResponse)r);
                else if (q is RealValueQuestion)
                    CreateQuestionForm(table, (RealValueResponse)r);
                else if (q is TextValueQuestion)
                    CreateQuestionForm(table, (TextValueResponse)r);
                else if (q is Comment)
                    CreateQuestionForm(table, (EmptyResponse)r);
                else if (q is MoneyValueQuestion)
                    CreateQuestionForm(table, (MoneyValueResponse)r);
                else if (q is QuestionGroup)
                    CreateQuestionForm(table, (ResponseGroup)r);
            }
        }
        private void CreateQuestionForm(Table table, ResponseGroup r, ArrayList indexes = null)
        {
            QuestionBase q = r.Question;
            if (q is ChoiceQuestionGroup)
                CreateChoiceQuestionGroup(table, (ResponseGroup)r, indexes);
            else if (q is QuestionGroup)
                CreateQuestionGroup(table, (ResponseGroup)r, indexes);
        }

        private void BreakQuestionPage(ResponseBase r, ArrayList indexes)
        {
            if (r.Question.Parent != null)
            {
                if (r.Question.Parent is QuestionGroup)
                {
                    QuestionGroup qg = (QuestionGroup)r.Question.Parent;
                    int index = qg.Children.ToList<QuestionBase>().IndexOf(r.Question);
                    indexes.Add(index);
                }
                else
                    indexes.Add(0);
                this.BreakQuestionPage(r.Parent, indexes);
            }
            else
            {
                LayoutStyle layout = new LayoutStyle();
                layout.StartOnTheNextRow = true;

                TableCell cell = new TableCell()
                {
                    ID = PREFIX_PAGEID + topPager.PageCount,
                };
                TableRow contentRow = this.GetLastesRow(tableMain, layout);
                contentRow.CssClass = "trContent";
                contentRow.Cells.Add(cell);
                Table tb = new Table()
                {
                    Width = Unit.Percentage(100),
                };
                cell.Controls.Add(tb);

                if (r is ResponseGroup)
                    this.CreateQuestionForm(tb, (ResponseGroup)r, indexes);
                else
                    this.CreateQuestionForm(tb, (ResponseGroup)r);
            }
        }

        private void CreateQuestionGroup(Table table, ResponseGroup rgroup, ArrayList indexes)
        {
            QuestionGroup q = rgroup.Question;

            if (table != null && q != null)
            {
                TableCell cell = PrepareTableCellControl(table, q);

                Table tb = new Table() { CellPadding = 0, CellSpacing = 0 };

                tb.Style.Add(HtmlTextWriterStyle.BorderCollapse, "collapse");

                cell.Controls.Add(tb);

                // columns == 1
                if (q.MemberLayout == null || q.MemberLayout.Columns == 1)
                {
                    tb.Width = Unit.Percentage(100);
                    int rows = 0;
                    int i = 0;
                    if (indexes != null && indexes.Count > 0)
                    {
                        i = (int)indexes[indexes.Count - 1];
                        indexes.RemoveAt(indexes.Count - 1);
                    }
                    for (; i < rgroup.Children.Count; i++)
                    {
                        if (rgroup.Children[i].Question.PageBreak && indexes == null)
                        {
                            topPager.PageCount++;
                            indexes = new ArrayList();
                            indexes.Add(i);
                            BreakQuestionPage(rgroup.Children[i], indexes);
                            break;
                        }
                        rows = tb.Rows.Count;
                        this.CreateQuestionForm(tb, rgroup.Children[i]);
                        SetRowStyle(tb, q.MemberLayout, tb.Rows.Count - rows, i);
                    }
                }
                //columns > 1
                else
                {
                    TableRow tr = null;
                    TableCell td = null;
                    Table innerTable = null;
                    int count = rgroup.Children.Count;
                    int columns = 1;
                    if (q.MemberLayout != null)
                        columns = q.MemberLayout.Columns;
                    int rows = (int)Math.Ceiling((double)count / (double)columns);
                    //double width = 100 / columns;
                    int m, n, indexValue;
                    DetermineValueIndex cal = GetValueIndexMethod(q.MemberLayout, columns, rows);
                    RowOrCellSpan spanMethod = GetSpanMethod(q.MemberLayout);
                    int Cells = count;
                    if (columns > 0)
                        Cells = rows * columns;
                    int i = 0;
                    if (indexes != null && indexes.Count > 0)
                    {
                        i = (int)indexes[indexes.Count - 1];
                        indexes.RemoveAt(indexes.Count - 1);
                    }
                    for (; i < Cells; i++)
                    {
                        n = i % columns;
                        m = i / columns;
                        indexValue = cal(m, n);
                        if (n == 0)
                        {
                            tr = new TableRow();
                            tb.Rows.Add(tr);
                            SetRowStyle(tb, q.MemberLayout, 1, m);
                        }

                        if (indexValue < count)
                        {
                            if (rgroup.Children[indexValue].Question.PageBreak && indexes == null)
                            {
                                topPager.PageCount++;
                                indexes = new ArrayList();
                                indexes.Add(indexValue);
                                BreakQuestionPage(rgroup.Children[indexValue], indexes);
                                break;
                            }
                            if (td == null)
                            {
                                td = new TableCell()
                                {
                                    //Width = Unit.Percentage(width),
                                    VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Top,
                                    HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left,
                                };
                                //td.Style.Add(HtmlTextWriterStyle.Position, "relative");
                                tr.Cells.Add(td);
                            }
                            innerTable = new Table()
                            {
                                CellPadding = 0,
                                CellSpacing = 0,
                            };
                            td.Controls.Add(innerTable);
                            this.CreateQuestionForm(innerTable, rgroup.Children[indexValue]);
                            td = null;
                        }
                        else
                        {
                            spanMethod(tb, td, m, n);
                        }
                    }
                }
            }
        }
        private void CreateChoiceQuestionGroup(Table table, ResponseGroup r, ArrayList indexes)
        {
            ChoiceQuestionGroup q = (ChoiceQuestionGroup)r.Question;
            // 1. Prepare empty cell
            LayoutStyle memberLayout = this.GetMemberStyleFromParent(q.Parent);
            TableRow row = this.GetLastesRow(table, memberLayout);
            TableCell cell = new TableCell();
            if (memberLayout == null || (memberLayout.AlignValues && !memberLayout.StartOnTheNextRow))
                cell.ColumnSpan = 3;
            row.Cells.Add(cell);
            // 2. create inner question table
            Table qTable = new Table()
            {
                Width = Unit.Percentage(100),
            };
            cell.Controls.Add(qTable);
            // 3. create first row for title and choices label
            row = new TableRow();
            qTable.Rows.Add(row);
            // 4. create each cell and title label
            cell = new TableCell();
            row.Cells.Add(cell);
            this.SetVisualStyle(cell, q.TitleStyle);
            if (q.Title != null && q.TitleIsVisible)
                cell.Controls.Add(this.CreateTextLabel(q.Title));

            if (q.ChoiceList != null)
            {
                foreach (ChoiceItem item in q.ChoiceList.Choices)
                {
                    cell = new TableCell();
                    cell.Controls.Add(this.CreateTextLabel(item.Title));
                    row.Cells.Add(cell);
                }
            }

            CreateChoiceControl createControlMethod = null;
            GroupChoiceQuestion cq = null;
            ChoiceResponse cResponse = null;
            ResponseChoice rchoice = null;
            ASPxCheckBox chk = null;

            int i = 0;
            if (indexes != null && indexes.Count > 0)
            {
                i = (int)indexes[indexes.Count - 1];
                indexes.RemoveAt(indexes.Count - 1);
            }
            for (; i < r.Children.Count; i++)
            {
                if (r.Children[i] is ChoiceResponse)
                {
                    if (r.Children[i].Question.PageBreak && indexes == null)
                    {
                        topPager.PageCount++;
                        indexes = new ArrayList();
                        indexes.Add(i);
                        BreakQuestionPage(r.Children[i], indexes);
                        break;
                    }
                    cResponse = (ChoiceResponse)r.Children[i];
                    cq = (GroupChoiceQuestion)cResponse.Question;
                    row = new TableRow();
                    qTable.Rows.Add(row);
                    cell = new TableCell();
                    row.Cells.Add(cell);
                    cell.Controls.Add(this.CreateTextLabel(cq.Title));

                    if (cq.AllowMultipleSelections)
                        createControlMethod = CreateCheckboxChoice;
                    else
                        createControlMethod = CreateRadioChoice;

                    for (int j = 0; j < cResponse.Choices.Count; j++)
                    {
                        rchoice = cResponse.Choices[j];
                        cell = new TableCell();
                        row.Cells.Add(cell);
                        chk = createControlMethod(rchoice, cq);
                        chk.ID = cq.ID.ToString() + "_" + ((ChoiceItemQuestionChoice)rchoice.QuestionChoice).ChoiceItem.ChoiceItemID.ToString();
                        cell.Controls.Add(chk);
                    }
                }
            }
        }

        public void Initial()
        {
            tableMain.Rows.Clear();
            if (ResponseForm != null && ResponseForm.Questionnaire != null)
            {
                this.topPager.ClientInstanceName = this.ClientID + "_pgTop";
                this.bottomPager.ClientInstanceName = this.ClientID + "_pgBottom";
                // Title
                Questionnaire qn = ResponseForm.Questionnaire;
                LayoutStyle layout = new LayoutStyle();
                layout.StartOnTheNextRow = true;
                TableCell cell = null;
                if (qn.Title != null)
                {
                    cell = new TableCell();
                    this.SetVisualStyle(cell, qn.TitleStyle);
                    this.GetLastesRow(tableMain, layout).Cells.Add(cell);
                    cell.Controls.Add(this.CreateTextLabel(qn.Title));
                }
                //Description
                if (qn.Description != null)
                {
                    cell = new TableCell();
                    this.SetVisualStyle(cell, qn.DescriptionStyle);
                    this.GetLastesRow(tableMain, layout).Cells.Add(cell);
                    cell.Controls.Add(this.CreateTextLabel(qn.Description));
                }
                // add top pager
                AddPagerControl(this.topPager, this.bottomPager.ClientInstanceName);
                // question 
                cell = new TableCell()
                {
                    ID = PREFIX_PAGEID + topPager.PageCount,
                };
                TableRow contentRow = this.GetLastesRow(tableMain, layout);
                contentRow.CssClass = "trContent";
                contentRow.Cells.Add(cell);
                Table tb = new Table()
                {
                    Width = Unit.Percentage(100),
                };
                cell.Controls.Add(tb);
                CreateQuestionForm(tb, ResponseForm.Responses);

                // add bottom pager
                this.bottomPager.PageCount = this.topPager.PageCount;
                this.bottomPager.CurrentPageNumber = this.topPager.CurrentPageNumber;
                AddPagerControl(this.bottomPager, this.topPager.ClientInstanceName);
                SetCurrentPageNumber(this.bottomPager.CurrentPageNumber);
            }
        }
        private void AddPagerControl(PagerControl pager, string synchroClientName)
        {
            TableCell cell = new TableCell() { HorizontalAlign = PagerAlign };
            tableMain.Rows.Add(new TableRow());
            tableMain.Rows[tableMain.Rows.Count - 1].Cells.Add(cell);
            cell.Controls.Add(pager);
            pager.ClientSideEvents.PageNumberChanged = GetPageChangedClientScript(synchroClientName);
        }
        private string GetPageChangedClientScript(string synchroClientName)
        {
            return @"function(s,e)
                {
                    var max = s.GetMaxValue();
                    var current = s.GetNumber();
                    var cell;
                    
                    for(var i = 1; i <= max;i++)
                    {
                        cell = document.getElementById('" + this.ClientID + "_" + PREFIX_PAGEID + @"' + i);
                        if(current == i)
                            cell.style.display = '';
                        else
                            cell.style.display = 'none';
                    }
                    " + synchroClientName + @".SetText(current);
                }";
        }
        private void SetCurrentPageNumber(int pageNumber)
        {
            TableCell cell = null;
            string currentTableID = PREFIX_PAGEID + pageNumber;
            for (int i = 0; i < tableMain.Rows.Count; i++)
            {
                if (tableMain.Rows[i].CssClass == "trContent")
                {
                    for (int j = 0; j < tableMain.Rows[i].Cells.Count; j++)
                    {
                        cell = tableMain.Rows[i].Cells[j];
                        if (cell.ID == currentTableID)
                            cell.Style.Add("display", "");
                        else
                            cell.Style.Add("display", "none");
                    }
                }
            }
        }

        #endregion

        #region Responses

        private object GetCheckBoxResponse(ChoiceResponse r)
        {
            string key = this.UniqueID + NAME_DELIMITER + r.Question.ID.ToString();
            string value = null;
            ItemSelection[] selectedItems = null;
            ItemSelection selectedItem;
            QuestionChoice ch = null;
            if (r.Question.AllowMultipleSelections)
            {
                ArrayList list = new ArrayList();
                for (int i = 0; i < r.Choices.Count; i++)
                {
                    ch = r.Choices[i].QuestionChoice;
                    value = Request.Form[key + "_" + ch.ID];
                    if (value == SELECT_CODE)
                    {
                        selectedItem = new ItemSelection() { ChoiceNo = i, };
                        if (ch.FurtherQuestion != null)
                            selectedItem.Value = GetValueResponse(r.Choices[i].FurtherResponse);
                        list.Add(selectedItem);
                    }
                }
                selectedItems = (ItemSelection[])list.ToArray(typeof(ItemSelection));
            }
            else
            {
                for (int i = 0; i < r.Choices.Count; i++)
                {
                    ch = r.Choices[i].QuestionChoice;
                    if (ch is ChoiceItemQuestionChoice)
                        value = Request.Form[key + "_" + ((ChoiceItemQuestionChoice)ch).ChoiceItem.ChoiceItemID];
                    else
                        value = Request.Form[key + "_" + ch.ID];
                    if (value == SELECT_CODE)
                    {
                        selectedItem = new ItemSelection() { ChoiceNo = i, };
                        if (ch.FurtherQuestion != null)
                            selectedItem.Value = GetValueResponse(r.Choices[i].FurtherResponse);
                        return selectedItem;
                    }
                }
            }
            return selectedItems;
        }
        private object GetComboBoxResponse(ChoiceResponse r)
        {
            string value = Request.Form[this.UniqueID + NAME_DELIMITER + r.Question.ID.ToString() + POSTFIX_COMBOBOX_VALUE];

            ItemSelection selectedItem;
            QuestionChoice ch = null;

            for (int i = 0; i < r.Choices.Count; i++)
            {
                ch = r.Choices[i].QuestionChoice;
                if (value == ch.ID.ToString())
                {
                    selectedItem = new ItemSelection() { ChoiceNo = i };
                    if (ch.FurtherQuestion != null)
                        selectedItem.Value = GetValueResponse(r.Choices[i].FurtherResponse);
                    return selectedItem;
                }
            }
            return null;
        }

        private object GetValueResponse(ChoiceResponse r)
        {
            if (r.Question.MemberLayout == null || r.Question.MemberLayout.ControlType != iSabaya.ControlType.ComboBox)
                return GetCheckBoxResponse(r);
            else
                return GetComboBoxResponse(r);
        }
        private string GetValueResponse(int questionID)
        {
            string key = this.UniqueID + NAME_DELIMITER + questionID.ToString();
            return Request.Form[key];
        }
        private object GetValueResponse(IntegerValueResponse r)
        {
            return GetValueResponse(r.Question.ID);
        }
        private object GetValueResponse(RealValueResponse r)
        {
            return GetValueResponse(r.Question.ID);
        }
        private object GetValueResponse(TextValueResponse r)
        {
            return GetValueResponse(r.Question.ID);
        }
        private object GetValueResponse(DateValueResponse r)
        {
            string value = GetValueResponse(r.Question.ID);
            return DateTime.ParseExact(value, base.DateInputFormat, new CultureInfo(WebConstants.DateTimeCulture));
        }
        private object GetValueResponse(MoneyValueResponse r)
        {

            string key = this.UniqueID + NAME_DELIMITER + r.Question.ID.ToString();
            //Request.Form[key];
            string moneyString = Request.Form[key + NAME_DELIMITER + MoneyControl.POST_CURRENCY_KEY];
            moneyString += Request.Form[key + NAME_DELIMITER + MoneyControl.POST_AMOUNT_KEY];
            try
            {
                return Money.Parse(moneyString);
            }
            catch (Exception)
            {
                return null;
            }
        }
        private object GetValueResponse(ResponseBase r)
        {
            object obj = null;
            if (r != null)
            {
                if (r is ChoiceResponse)
                    obj = GetValueResponse((ChoiceResponse)r);
                else if (r is DateValueResponse)
                    obj = GetValueResponse((DateValueResponse)r);
                else if (r is IntegerValueResponse)
                    obj = GetValueResponse(((IntegerValueResponse)r));
                else if (r is RealValueResponse)
                    obj = GetValueResponse(((RealValueResponse)r));
                else if (r is TextValueResponse)
                    obj = GetValueResponse(((TextValueResponse)r));
                else if (r is MoneyValueResponse)
                    obj = GetValueResponse(((MoneyValueResponse)r));
                else if (r is ResponseGroup)
                    GetValueToResponse((ResponseGroup)r);
            }
            r.SetValue(obj);
            return obj;
        }
        private void GetValueToResponse(ResponseGroup rg)
        {
            if (rg != null)
            {
                for (int i = 0; i < rg.Children.Count; i++)
                    GetValueResponse(rg.Children[i]);
            }
        }
        public void GetValueToResponces()
        {
            if (ResponseForm != null)
                GetValueToResponse(ResponseForm.Responses);
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
            if (!Page.IsPostBack)
                ValidationGroup = this.ClientID;
        }
        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            object[] values = new object[2];

            values[0] = this.pagerAlign;
            values[1] = this.ReadOnly;
            return new Pair(obj, values);
        }

        protected override void LoadControlState(object savedState)
        {
            Pair p = (Pair)savedState;
            base.LoadControlState(p.First);

            object[] values = (object[])p.Second;
            this.pagerAlign = (System.Web.UI.WebControls.HorizontalAlign)values[0];
            this.ReadOnly = (bool)values[1];
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //if (!string.IsNullOrEmpty(ClientInstanceName))
            //{
            //    if (!Page.ClientScript.IsClientScriptBlockRegistered(ClientInstanceName))
            //    {
            //        StringBuilder script = new StringBuilder();
            //        script.AppendLine("<script type='text/javascript'>");
            //        script.AppendLine("var q = new Object();");
            //        script.AppendLine(string.Format("window['{0}'] = q;", ClientInstanceName));
            //        script.AppendLine("q.Validate = " + this.ValidationClientScript);
            //        script.AppendLine("</script>");
            //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), ClientInstanceName, script.ToString());
            //    }
            //}
        }
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
        }
    }
}