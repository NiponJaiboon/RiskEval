using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using WebHelper.Controls;

namespace WebHelper
{
    public struct ColumnHeaderInfo
    {
        public ColumnHeaderInfo(String style, String label)
        {
            Literal literal = null;
            HorizontalAlignment = System.Web.UI.WebControls.HorizontalAlign.Left;
            VerticalAlignment = System.Web.UI.WebControls.VerticalAlign.Top;
            TextWrapping = true;
            literal = new Literal();
            literal.Text = label;
            Label = label;
            ControlLiteral = literal;
            Style = style;
        }

        public System.Web.UI.WebControls.HorizontalAlign HorizontalAlignment;
        public System.Web.UI.WebControls.VerticalAlign VerticalAlignment;
        public Control ControlLiteral;
        public String Label;
        public String Style;
        public bool TextWrapping;
    }

    public struct ColumnDataCellInfo
    {
        public ColumnDataCellInfo(String style, String label, String cellID = null)
        {
            Literal literal = null;
            HorizontalAlignment = System.Web.UI.WebControls.HorizontalAlign.Left;
            VerticalAlignment = System.Web.UI.WebControls.VerticalAlign.Top;
            TextWrapping = true;
            literal = new Literal();
            literal.Text = label;
            Label = label;
            ControlLiteral = literal;
            Style = style;
            CellID = cellID;
        }

        public System.Web.UI.WebControls.HorizontalAlign HorizontalAlignment;
        public System.Web.UI.WebControls.VerticalAlign VerticalAlignment;
        public Control ControlLiteral;
        public String Label;
        public String Style;
        public bool TextWrapping;
        public String CellID;
    }

    public static class TableBuilder
    {
        public static void AddHeaderRow(this Table table, String headerStyle, ColumnHeaderInfo[] columns)
        {
            TableHeaderRow hRow = new TableHeaderRow();
            hRow.SetRowStyle(headerStyle);

            foreach (ColumnHeaderInfo ci in columns)
            {
                TableHeaderCell col = new TableHeaderCell();
                if (!String.IsNullOrEmpty(ci.Style))
                    col.SetHeaderCellStyle(ci.Style);
                if (!String.IsNullOrEmpty(ci.Label))
                    col.Text = ci.Label;
                col.HorizontalAlign = ci.HorizontalAlignment;
                col.VerticalAlign = ci.VerticalAlignment;
                col.Wrap = ci.TextWrapping;
                hRow.Cells.Add(col);
            }
            table.Rows.Add(hRow);
        }

        public static TableCell AddDataCell(this TableRow row, Control control, String cellID = null, bool wrap = true,
                                        System.Web.UI.WebControls.HorizontalAlign horAlign = System.Web.UI.WebControls.HorizontalAlign.Left,
                                        System.Web.UI.WebControls.VerticalAlign verAlign = System.Web.UI.WebControls.VerticalAlign.Top
                                        )
        {
            TableCell cell = new TableCell();
            cell.SetDataCellStyle();
            cell.ID = cellID;
            cell.HorizontalAlign = horAlign;
            cell.VerticalAlign = verAlign;
            cell.Wrap = wrap;
            cell.Controls.Add(control);

            row.Cells.Add(cell);
            return cell;
        }

        //Html Table Control
        public static string tdTag = "td";
        public static string thTag = "th";

        public static void AddHeaderRow(this HtmlTable table, String headerStyle, ColumnHeaderInfo[] columns)
        {
            HtmlTableRow hRow = new HtmlTableRow();
            if (!String.IsNullOrEmpty(headerStyle))
                hRow.SetRowStyle(headerStyle);

            foreach (ColumnHeaderInfo ci in columns)
            {
                HtmlTableCell col = new HtmlTableCell();
                if (!String.IsNullOrEmpty(ci.Style))
                    col.SetHeaderCellStyle(ci.Style);
                if (!String.IsNullOrEmpty(ci.Label))
                    col.Controls.Add(ci.ControlLiteral);
                hRow.Cells.Add(col);
            }

            table.Rows.Add(hRow);
        }

        public static void AddDataCell(this HtmlTable table, String cellStyle, ColumnDataCellInfo[] columns)
        {
            HtmlTableRow row = new HtmlTableRow();
            if (!String.IsNullOrEmpty(cellStyle))
                row.SetRowStyle(cellStyle);
            foreach (ColumnDataCellInfo dci in columns)
            {
                HtmlTableCell cell = new HtmlTableCell();
                if (!String.IsNullOrEmpty(dci.CellID))
                    cell.ID = dci.CellID;
                if (!String.IsNullOrEmpty(dci.Style))
                    cell.SetDataCellStyle();
                if (!String.IsNullOrEmpty(dci.Label))
                    cell.Controls.Add(dci.ControlLiteral);
                row.Cells.Add(cell);
            }

            table.Rows.Add(row);
        }

        public static HtmlTableCell AddDataCell(this HtmlTableRow row, Control control,
                                            String cellID = null, String colSpan = null, String headerStyle = null)
        {
            HtmlTableCell cell = new HtmlTableCell();
            if (!String.IsNullOrEmpty(headerStyle))
                cell.SetHeaderCellStyle(headerStyle);
            else
                cell.SetDataCellStyle();
            if (!String.IsNullOrEmpty(colSpan))
                cell.ColSpan = Convert.ToInt32(colSpan);
            cell.ID = cellID;
            cell.Controls.Add(control);

            row.Cells.Add(cell);
            return cell;
        }

        public static void SetButtonsTable(this HtmlTable table, String unit = null)
        {
            table.SetTableStyleBasic();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 1; j < table.Rows[i].Cells.Count; j++)
                {
                    table.Rows[i].Cells[j].Style.Add(HtmlTextWriterStyle.PaddingLeft, (unit == null ? "5" : unit));
                }
            }
        }

        public static void SetTableStyleBasic(this HtmlTable table, String display = null)
        {
            table.Attributes.Add("cellpadding", "0");
            table.Attributes.Add("cellspacing", "0");
            table.Attributes.Add("style", "border-collapse: collapse; empty-cells: show;");
            table.Attributes.Add("border", "0");

            if (!String.IsNullOrEmpty(display))
                table.Style.Add(HtmlTextWriterStyle.Display, display);
        }

        public static void SetTableControlStyle(this HtmlTable table, string postfix)
        {
            table.SetTableStyleBasic();
            table.Attributes.Add("class", string.Format("dxgvControl_{0}", postfix));
        }

        public static void SetTableStyle(this HtmlTable table, string postfix)
        {
            table.SetTableStyleBasic();
            table.Attributes.Add("class", string.Format("dxgvTable_{0}", postfix));
        }

        public static void SetPageContentStyle(this HtmlTable table, string postfix)
        {
            table.Attributes.Add("class", string.Format("dxtcPageContent_{0}", postfix));
        }

        public static void SetRowStyle(this HtmlTableRow row, string postfix)
        {
            row.Attributes.Add("class", string.Format("dxgvDataRow_{0}", postfix));
        }

        public static void SetHeaderCellStyle(this HtmlTableCell cell, string postfix)
        {
            cell.Attributes.Add("class", string.Format("dxgvHeader_{0}", postfix));
            cell.Style[System.Web.UI.HtmlTextWriterStyle.Cursor] = "default";
        }

        public static void SetDataCellStyle(this HtmlTableCell cell)
        {
            cell.Attributes.Add("class", "dxgv");
            cell.Style["empty-cells"] = "show";
        }

        public static void SetGdvTitlePanelCellStyle(this HtmlTableCell cell, string postfix)
        {
            cell.Attributes.Add("class", string.Format("dxgvTitlePanel_{0}", postfix));
            cell.Style["empty-cells"] = "show";
        }

        public static void SetTableTitlePanelStyle(this HtmlTable table, string postfix)
        {
            // table must have a row and a cell.
            table.SetTableStyleBasic();
            table.Rows[0].Cells[0].SetGdvTitlePanelCellStyle(postfix);
        }

        public static void SetCssHtmlTable_H(this HtmlTable tableContent, string postfix)
        {
            tableContent.SetTableStyle(postfix);
            for (int i = 0; i < tableContent.Rows.Count; i++)
            {
                tableContent.Rows[i].SetRowStyle(postfix);
                for (int j = 0; j < tableContent.Rows[i].Cells.Count; j++)
                {
                    if (i == 0)
                        tableContent.Rows[i].Cells[j].SetHeaderCellStyle(postfix);
                    else
                        tableContent.Rows[i].Cells[j].SetDataCellStyle();
                }
            }
        }

        public static void SetCssHtmlTable_H(this HtmlTable tableContent, string postfix, bool datacellOnly)
        {
            tableContent.SetTableStyle(postfix);
            for (int i = 0; i < tableContent.Rows.Count; i++)
            {
                tableContent.Rows[i].SetRowStyle(postfix);
                for (int j = 0; j < tableContent.Rows[i].Cells.Count; j++)
                {
                    if (i == 0 && datacellOnly == false)
                        tableContent.Rows[i].Cells[j].SetHeaderCellStyle(postfix);
                    else
                        tableContent.Rows[i].Cells[j].SetDataCellStyle();
                }
            }
        }

        public static void SetCssHtmlTable_V(this HtmlTable tableContent, string postfix)
        {
            tableContent.SetTableStyle(postfix);
            for (int i = 0; i < tableContent.Rows.Count; i++)
            {
                tableContent.Rows[i].SetRowStyle(postfix);
                for (int j = 0; j < tableContent.Rows[i].Cells.Count; j++)
                {
                    if (j == 0)
                        tableContent.Rows[i].Cells[j].SetHeaderCellStyle(postfix);
                    else
                        tableContent.Rows[i].Cells[j].SetDataCellStyle();
                }
            }
        }

        public static void SetCssHtmlTable_V(this Table tableContent, string postfix)
        {
            tableContent.SetTableStyle(postfix);
            for (int i = 0; i < tableContent.Rows.Count; i++)
            {
                tableContent.Rows[i].SetRowStyle(postfix);
                for (int j = 0; j < tableContent.Rows[i].Cells.Count; j++)
                {
                    if (j == 0)
                        tableContent.Rows[i].Cells[j].SetHeaderCellStyle(postfix);
                    else
                        tableContent.Rows[i].Cells[j].SetDataCellStyle();
                }
            }
        }
        /// <summary>
        /// Assign data cell style to all table cell (use Devexpress css theme)
        /// </summary>
        /// <param name="table">table</param>
        /// <param name="postfix">postfix get from current used theme</param>
        /// <remarks></remarks>
        public static void SetAllDataCell(this HtmlTable table, string postfix)
        {
            table.SetTableStyle(postfix);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i].SetRowStyle(postfix);
                for (int j = 0; j < table.Rows[i].Cells.Count; j++)
                {
                    table.Rows[i].Cells[j].SetDataCellStyle();
                }
            }
        }

        //Table Control
        public static void SetTableStyleBasic(this Table table)
        {
            table.Attributes.Add("cellpadding", "0");
            table.Attributes.Add("cellspacing", "0");
            table.Attributes.Add("style", "border-collapse: collapse; empty-cells: show;");
            table.Attributes.Add("border", "0");
        }

        public static void SetTableControlStyle(this Table table, string postfix)
        {
            table.SetTableStyleBasic();
            table.Attributes.Add("class", "dxgvControl_" + postfix);
        }

        public static void SetTableStyle(this Table table, string postfix)
        {
            table.SetTableStyleBasic();
            table.Attributes.Add("class", "dxgvTable_" + postfix);
        }

        public static void SetPageContentStyle(this Table table, string postfix)
        {
            table.Attributes.Add("class", "dxtcPageContent_" + postfix);
        }

        public static void SetRowStyle(this TableRow row, string postfix)
        {
            row.Attributes.Add("class", "dxgvDataRow_" + postfix);
        }

        public static void SetHeaderCellStyle(this TableCell cell, string postfix)
        {
            cell.Attributes.Add("class", "dxgvHeader_" + postfix);
            cell.Style[System.Web.UI.HtmlTextWriterStyle.Cursor] = "default";
        }

        public static void SetDataCellStyle(this TableCell cell)
        {
            cell.Attributes.Add("class", "dxgv");
            cell.Style["empty-cells"] = "show";
        }

        public static void setTableColumnVisible(this HtmlTable table, bool visible, int indexColumn)
        {
            string editDisplay = visible == true ? "" : "none";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i].Cells[indexColumn].Style.Add(HtmlTextWriterStyle.Display, editDisplay);
            }
        }

        public static void setTableColumnVisible(this Table table, bool visible, int indexColumn)
        {
            string editDisplay = visible == true ? "" : "none";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i].Cells[indexColumn].Style.Add(HtmlTextWriterStyle.Display, editDisplay);
            }
        }

        public static void setTableRowVisible(this Table table, bool visible, int indexRow)
        {
            string editDisplay = visible == true ? "" : "none";
            table.Rows[indexRow].Style.Add(HtmlTextWriterStyle.Display, editDisplay);
        }

        public static void setTableRowVisible(this HtmlTable table, bool visible, int indexRow)
        {
            string editDisplay = visible == true ? "" : "none";
            table.Rows[indexRow].Style.Add(HtmlTextWriterStyle.Display, editDisplay);
        }

        public static void setTableColumnToConfirm(this HtmlTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i].Cells[1].Controls[0] is ASPxTextBox)
                {
                    ((ASPxLabel)table.Rows[i].Cells[2].Controls[0]).Text = ((ASPxTextBox)table.Rows[i].Cells[1].Controls[0]).Text;
                }
                else if (table.Rows[i].Cells[1].Controls[0] is iSabayaControl)
                {
                    ((ASPxLabel)table.Rows[i].Cells[2].Controls[0]).Text = ((iSabayaControl)table.Rows[i].Cells[1].Controls[0]).Text;
                }
                else if (table.Rows[i].Cells[1].Controls[0] is CategoryControl)
                {
                    ((ASPxLabel)table.Rows[i].Cells[2].Controls[0]).Text = ((CategoryControl)table.Rows[i].Cells[1].Controls[0]).SelectedNode != null ?
                        ((CategoryControl)table.Rows[i].Cells[1].Controls[0]).SelectedNode.ToString() : "";
                }
            }
        }

        public static void setTableColumnToConfirm(this Table table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                // check if 2 column gen new column
                if (table.Rows[i].Cells.Count == 2)
                {
                    TableCell tc = new TableCell();
                    tc.Wrap = false;
                    tc.SetDataCellStyle();
                    tc.Width = 170;
                    tc.Controls.Add(new ASPxLabel { Width = 150 });
                    table.Rows[i].Cells.Add(tc);
                }
                if (table.Rows[i].Cells[1].Controls[0] is ASPxSpinEdit)
                {
                    ((ASPxLabel)table.Rows[i].Cells[2].Controls[0]).Text = ((ASPxSpinEdit)table.Rows[i].Cells[1].Controls[0]).Value.ToString();
                }

                else if (table.Rows[i].Cells[1].Controls[0] is ASPxMemo)
                {
                    ((ASPxLabel)table.Rows[i].Cells[2].Controls[0]).Text = ((ASPxMemo)table.Rows[i].Cells[1].Controls[0]).Text;
                }

                else if (table.Rows[i].Cells[1].Controls[0] is ASPxComboBox)
                {
                    ((ASPxLabel)table.Rows[i].Cells[2].Controls[0]).Text = ((ASPxComboBox)table.Rows[i].Cells[1].Controls[0]).Text;
                }

                else if (table.Rows[i].Cells[1].Controls[0] is ASPxTextBox)
                {
                    ((ASPxLabel)table.Rows[i].Cells[2].Controls[0]).Text = ((ASPxTextBox)table.Rows[i].Cells[1].Controls[0]).Text;
                }
                else if (table.Rows[i].Cells[1].Controls[0] is ASPxCheckBox)
                {
                    table.Rows[i].Cells[2].Controls.AddAt(0, new ASPxCheckBox { ReadOnly = true });
                    ((ASPxCheckBox)table.Rows[i].Cells[2].Controls[0]).Checked = ((ASPxCheckBox)table.Rows[i].Cells[1].Controls[0]).Checked;
                }
                else if (table.Rows[i].Cells[1].Controls[0] is iSabayaControl)
                {
                    ((ASPxLabel)table.Rows[i].Cells[2].Controls[0]).Text = ((iSabayaControl)table.Rows[i].Cells[1].Controls[0]).Text;
                }
                else if (table.Rows[i].Cells[1].Controls[0] is CategoryControl)
                {
                    ((ASPxLabel)table.Rows[i].Cells[2].Controls[0]).Text = ((CategoryControl)table.Rows[i].Cells[1].Controls[0]).SelectedNode != null ?
                        ((CategoryControl)table.Rows[i].Cells[1].Controls[0]).SelectedNode.ToString() : "";
                }
                else if (table.Rows[i].Cells[1].Controls[0] is MLSControl)
                {
                    ((ASPxLabel)table.Rows[i].Cells[2].Controls[0]).Text =
                        ((MLSControl)table.Rows[i].Cells[1].Controls[0]).Value != null ? ((MLSControl)table.Rows[i].Cells[1].Controls[0]).Value.ToString() : "";
                }
                else if (table.Rows[i].Cells[1].Controls[0] is DateTimeControl)
                {
                    ((ASPxLabel)table.Rows[i].Cells[2].Controls[0]).Text =
                        ((DateTimeControl)table.Rows[i].Cells[1].Controls[0]).Value != null ? ((DateTimeControl)table.Rows[i].Cells[1].Controls[0]).Value.ToString() : "";
                }
                else if (table.Rows[i].Cells[1].Controls[0] is Table)
                {
                    Table t = table.Rows[i].Cells[1].Controls[0] as Table;
                    if (t.Rows[0].Cells[0].Controls[0] is ASPxTextBox)
                    {
                        ((ASPxLabel)table.Rows[i].Cells[2].Controls[0]).Text = ((ASPxTextBox)t.Rows[0].Cells[0].Controls[0]).Text;
                    }
                }
            }
            table.setTableColumnVisible(false, 1);
        }

        public static void setLabelTitle(this Table table, String[] listTitle)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Rows[i].Cells.Count; j++)
                {
                    table.Rows[i].Cells[j].Wrap = false;
                    if (j == 0) // set title
                    {
                        (table.Rows[i].Cells[j].Controls[0] as ASPxLabel).Text = listTitle[i];
                    }
                }
            }
        }

        public static void ClearTable(Table table)
        {
            ASPxEdit.ClearEditorsInContainer(table);
        }

        public static void ClearTable(this HtmlTable table)
        {
            ASPxEdit.ClearEditorsInContainer(table);
        }

        public static TableCell AddEmptyCellAtLatestRow(this Table table)
        {
            TableCell cell = new TableCell();
            table.Rows[table.Rows.Count - 1].Cells.Add(cell);
            return cell;
        }
        public static TableRow AddEmptyRow(this Table table)
        {
            TableRow row = new TableRow();
            table.Rows.Add(row);
            return row;
        }
        public static TableCell AddEmptyCell(this TableRow row)
        {
            TableCell cell = new TableCell();
            row.Cells.Add(cell);
            return cell;
        }
        public static TableCell AddEmptyRowAndCell(this Table table)
        {
            return table.AddEmptyRow().AddEmptyCell();
        }
    }
}