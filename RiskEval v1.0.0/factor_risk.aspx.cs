using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ton.Data;
using riskEval;

public partial class factor_risk : System.Web.UI.Page
{
    int[] fm = {7,5,7,4,9,9};


    /// <summary>
    /// This Function will override the initial setting value in each control (in .aspx design form)
    /// Initial Component by assign the basic value for Dropdownlist , RequiredValidator, TextBox and Label
    /// Assign Validation Group value "error_panel_[panel index]" to each controls inside that panel.
    /// Re-Assign ControlToValidate of each RequiredValidator to map each control like. ddl_oppo_6_1 , reqvld_oppo_6_1
    /// Load Existing Data from DB and Map into each control in sequence.
    /// </summary>
    public void initializeComponent()
    {
        ListItem[] liA_oppo = new ListItem[3];
        ListItem[] liA_effect = new ListItem[3];
        ListItem[] liA_impact = new ListItem[3];
        liA_effect[0] = new ListItem("กรุณาเลือก", "");
        liA_effect[1] = new ListItem("ยอมรับได้", "ยอมรับได้");
        liA_effect[2] = new ListItem("ยอมรับไม่ได้", "ยอมรับไม่ได้");

        liA_oppo[0] = new ListItem("กรุณาเลือก", "");
        liA_oppo[1] = new ListItem("ต่ำ", "ต่ำ");
        liA_oppo[2] = new ListItem("สูง", "สูง");

        liA_impact[0] = new ListItem("กรุณาเลือก", "");
        liA_impact[1] = new ListItem("จัดการได้", "จัดการได้");
        liA_impact[2] = new ListItem("จัดการไม่ได้", "จัดการไม่ได้");

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if ((ck==null) || string.IsNullOrEmpty(ck.pj_id) )
        {
            ton.JavaScript.MessageBox(" โปรเจค ว่างเปล่า กรุณาเข้าสู่ระบบ ");
            //Response.Redirect(ton.config.Global_config.RootURL);
            Response.Redirect("project_home.aspx");
        }

        factors f = new factors();

        #region loadData
        f.loadFactorMain();
        for (int i = 0; i < f.factor_main_list.Count; i++)
        {
            f.factor_main_list[i].loadFactorSub();
        }
        #endregion

        int panel_index = 1;
        Control parent = pnl_fm_1.Parent;
        foreach (Control pnl in parent.Controls)
        {
            if (pnl is Panel)
            {
                
                Panel pnl_tmp = pnl as Panel;
                answer_factors_sub ans_fact_sub = new answer_factors_sub();
                int lbl_fs_index = 1;
                foreach (Control c in pnl.Controls)
                {
                               
                    if (c is DropDownList)
                    {
                        #region initialDropdownlist
                        
                        DropDownList ddl_temp =  c as DropDownList;
                        ddl_temp.Items.Clear();
                        if (ddl_temp.CssClass == "effect")
                        {
                            ddl_temp.Items.Add(new ListItem(liA_effect[0].Text,liA_effect[0].Value));
                            ddl_temp.Items.Add(new ListItem(liA_effect[1].Text, liA_effect[1].Value));
                            ddl_temp.Items.Add(new ListItem(liA_effect[2].Text, liA_effect[2].Value));
                            ddl_temp.ValidationGroup = "error_panel_" + panel_index;
                            //ddl_temp.Items.AddRange(liA_effect);
                            if (string.IsNullOrEmpty(ans_fact_sub.af_effect))
                            {
                                ddl_temp.SelectedIndex = 0;
                            }
                            else
                            {
                                ddl_temp.SelectedValue = ans_fact_sub.af_effect;
                            }

                        }
                        else if (ddl_temp.CssClass == "oppo")
                        {
                            //ddl_temp.Items.AddRange(liA_oppo);
                            ddl_temp.Items.Add(new ListItem(liA_oppo[0].Text, liA_oppo[0].Value));
                            ddl_temp.Items.Add(new ListItem(liA_oppo[1].Text, liA_oppo[1].Value));
                            ddl_temp.Items.Add(new ListItem(liA_oppo[2].Text, liA_oppo[2].Value));
                            ddl_temp.ValidationGroup = "error_panel_" + panel_index;
                            if (string.IsNullOrEmpty(ans_fact_sub.af_opportunity))
                            {
                                ddl_temp.SelectedIndex = 0;
                            }
                            else
                            {
                                ddl_temp.SelectedValue = ans_fact_sub.af_opportunity;
                            }
                        }
                        else if (ddl_temp.CssClass == "impact")
                        {
                            //ddl_temp.Items.AddRange(liA_impact);
                            ddl_temp.Items.Add(new ListItem(liA_impact[0].Text, liA_impact[0].Value));
                            ddl_temp.Items.Add(new ListItem(liA_impact[1].Text, liA_impact[1].Value));
                            ddl_temp.Items.Add(new ListItem(liA_impact[2].Text, liA_impact[2].Value));
                            ddl_temp.ValidationGroup = "error_panel_" + panel_index;
                            if (string.IsNullOrEmpty(ans_fact_sub.af_impact))
                            {
                                ddl_temp.SelectedIndex = 0;
                            }
                            else
                            {
                                ddl_temp.SelectedValue = ans_fact_sub.af_impact;
                            }
                        }
                        else
                        {
                        }
                        //ddl_temp.Items[0].Attributes.Add("style", "background-color:lime");
                        //ddl_temp.Items[1].Attributes.Add("style", "background-color:red");
                        //ddl_temp.Attributes.Add("style",ddl_temp.SelectedItem.Attributes["style"]);
                        
                        #endregion
                    }
                    else if (c is RequiredFieldValidator)
                    {
                        #region RequiredFieldValidator
                        RequiredFieldValidator reqvld_temp = c as RequiredFieldValidator;
                        if (c.ID.Contains("reqvld_oppo_"))
                        {
                            reqvld_temp.InitialValue = liA_oppo[0].Value;
                            reqvld_temp.ErrorMessage = "*";
                            reqvld_temp.ControlToValidate = "ddl_oppo_" + c.ID.Replace("reqvld_oppo_", "");
                        }
                        else if (c.ID.Contains("reqvld_effect_"))
                        {
                            reqvld_temp.InitialValue = liA_effect[0].Value;
                            reqvld_temp.ErrorMessage = "*";
                            reqvld_temp.ControlToValidate = "ddl_effect_" + c.ID.Replace("reqvld_effect_", "");
                        }
                        else if (c.ID.Contains("reqvld_impact_"))
                        {
                            reqvld_temp.InitialValue = liA_impact[0].Value;
                            reqvld_temp.ErrorMessage = "*";
                            reqvld_temp.ControlToValidate = "ddl_impact_" + c.ID.Replace("reqvld_impact_", "");
                        }
                        reqvld_temp.ValidationGroup = "error_panel_" + panel_index;
                        reqvld_temp.CssClass = "ErrorDokJan";
                        #endregion
                    }
                    else if (c is Label)
                    {
                        #region Label
                        Label lbl_temp = c as Label;
                        if (c.ID.Contains("lbl_fm_order_"))
                        {
                            lbl_temp.Text = f.factor_main_list[panel_index - 1].fm_order;
                        }
                        else if (c.ID.Contains("lbl_fm_id_"))
                        {
                            lbl_temp.Text = f.factor_main_list[panel_index - 1].fm_id;
                            lbl_temp.Visible = false;
                        }
                        else if (c.ID.Contains("fm_factors_text_"))
                        {
                            lbl_temp.Text = f.factor_main_list[panel_index - 1].fm_factors_text;
                        }
                        else if (c.ID.Contains("lbl_fs_order_"))
                        {
                            string suffix = (panel_index - 1).ToString() + "_" + (lbl_fs_index - 1).ToString();
                            lbl_temp.Text = f.factor_main_list[panel_index - 1].fs_list[lbl_fs_index - 1].fs_order;
                            // Comment because Seq of Label is Order , id ,  Text
                            //lbl_fs_index++;
                        }
                        else if (c.ID.Contains("lbl_fs_id_"))
                        {
                            string suffix = (panel_index - 1).ToString() + "_" + (lbl_fs_index - 1).ToString();
                            lbl_temp.Text = f.factor_main_list[panel_index - 1].fs_list[lbl_fs_index - 1].fs_id;
                            lbl_temp.Visible = false;
                            ans_fact_sub.load_answer_factors_sub_id(ck.pj_id, lbl_temp.Text);
                            // Comment because Seq of Label is Order , id ,  Text
                        }
                        else if (c.ID.Contains("lbl_fs_factors_text_"))
                        {
                            string suffix = (panel_index - 1).ToString() + "_" + (lbl_fs_index - 1).ToString();
                            lbl_temp.Text = f.factor_main_list[panel_index - 1].fs_list[lbl_fs_index - 1].fs_factors_text;
                            lbl_fs_index++;
                        } 
                        #endregion
                    }
                    else if (c is TextBox)
                    {
                        #region TextBox
                        TextBox txt_temp = c as TextBox;
                        if (c.ID.Contains("txt_fs_factors_etc_"))
                        {
                            if (string.IsNullOrEmpty(ans_fact_sub.factors_sub_etc))
                                txt_temp.Text = "";
                            else
                                txt_temp.Text = ans_fact_sub.factors_sub_etc;
                        }
                        else if (c.ID.Contains("txt_desc_more_text_"))
                        {
                            if (string.IsNullOrEmpty(ans_fact_sub.description_more))
                                txt_temp.Text = "";
                            else
                                txt_temp.Text = ans_fact_sub.description_more;
                        } 
                        #endregion
                    } 

                }
                panel_index++;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string mode = "";
        if(!String.IsNullOrEmpty(Request.QueryString["mode"]))
         mode = Server.UrlEncode(Request.QueryString["mode"].Replace("'",""));

        string p = "";
        if (!String.IsNullOrEmpty(Request.QueryString["p"]))
            p = Server.UrlEncode(Request.QueryString["p"].Replace("'", ""));
        
        // Protect variable P which must be in the specific rage.
        int p_index;
        if ((int.TryParse(p, out p_index)) && (p_index > 0) && (p_index <= fm.Count()))
        {
            // -1 for adjusting to 0 started base.
            p_index = p_index - 1;
        }
        else
        {
            
            p_index = 0;
        }

        if (!Page. IsPostBack)
        {
            initializeComponent();
        }

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();
        answer_factors_sub ans_fact_sub = new answer_factors_sub();

        Control parent = pnl_fm_1.Parent;

        int max = ans_fact_sub.getMax_fm_id(ck.pj_id);
        if (max < 0) { max = 0; }
        
        if (max >= fm.Count())
        {
            //----In Edit Mode--
            // Edit Mode Enable when All questions must be completed. Protect User try to hack system.
            if (mode == "edit")
            {
                // load panel which specified by request.querstring[p]
                max = p_index;
            }
            else
            {

                projects pj = new projects();
                pj.updateProjectCompleteStatus(ck.pj_id, "กรอกสมบูรณ์");

                Response.Redirect("project_summary.aspx");
            }
        }
        else
        {
            if (mode == "edit")
            {
                ton.JavaScript.MessageBox("การแก้ไข ทำได้ต่อเมื่อทำชุดคำถามนี้ครบทุกข้อแล้ว เท่านั้น!", "project_edit.aspx");
            }
        }
        //---------------------------------------------------

        for (int i = 0; i < fm.Count(); i++)
        {
            // +1 for adjust from 0 based array to 1 based array
            int suffix = i + 1;

            Panel pnl_temp = parent.FindControl("pnl_fm_" + suffix.ToString()) as Panel;

            if (suffix == (max + 1))
            {
                pnl_temp.Visible = true;
                pnl_temp.Enabled = true;
            }
            else
            {
                pnl_temp.Visible = false;
                pnl_temp.Enabled = false;
            }
        }
        //------------------------------------------
    }

    protected void btn_save_fm_Click(object sender, EventArgs e)
    {

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();
        if (String.IsNullOrEmpty(ck.pj_id))
        {
            ton.JavaScript.MessageBox(" โปรเจค ID ว่างเปล่า กรุณาเข้าสู่ระบบอีกครั้ง ");
            return;
        }
        Button btn_tmep = sender as Button;
        Control c = btn_tmep.Parent;
        
        Page.Validate("error_panel_" + btn_tmep.CommandArgument);
        if (!Page.IsValid)
        {
            return;
        }

        if (btn_tmep.CommandName == "save")
        {
            int panel_index = Convert.ToInt32(btn_tmep.CommandArgument);
            string suffix = panel_index.ToString() + "_";
            int sum = 0;
            int total = fm[panel_index - 1];
            for (int i = 1; i <= total; i++)
            {
                string final_suffix = suffix + i.ToString();
                answer_factors_sub ans_fact_sub = new answer_factors_sub();

                ans_fact_sub.af_opportunity = (c.FindControl("ddl_oppo_" + final_suffix) as DropDownList).SelectedValue;
                ans_fact_sub.af_effect = (c.FindControl("ddl_effect_" + final_suffix) as DropDownList).SelectedValue;
                ans_fact_sub.af_impact = (c.FindControl("ddl_impact_" + final_suffix) as DropDownList).SelectedValue;
                ans_fact_sub.factors_sub_etc = (c.FindControl("txt_fs_factors_etc_" + panel_index) as TextBox).Text;
                ans_fact_sub.description_more = (c.FindControl("txt_desc_more_text_" + panel_index) as TextBox).Text;
                ans_fact_sub.fs_id = (c.FindControl("lbl_fs_id_" + final_suffix) as Label).Text;
                ans_fact_sub.pj_id = ck.pj_id;

                int result = ans_fact_sub.saveToTable();
                if (result > 0)
                {
                    sum++;
                }
            }
            if (c is Panel)
            {
                Panel pnl_temp = c as Panel;
                
                pnl_temp.Enabled = false;
                btn_tmep.Visible = false;
                btn_next.Visible = true;
            }
            ton.JavaScript.MessageBox("บันทึก สำเร็จ " + sum + " จาก " + total + " ข้อ ");
        }
    }
    protected void btn_next_Click(object sender, EventArgs e)
    {
        Button btn_temp = sender as Button;
        string mode = "";
        if (!String.IsNullOrEmpty(Request.QueryString["mode"]))
            mode = Server.UrlEncode(Request.QueryString["mode"].Replace("'", ""));

        if (mode == "edit")
        {
            Response.Redirect("project_edit.aspx");
        }
        else
        {
            btn_temp.Visible = false;
        }
    }
}