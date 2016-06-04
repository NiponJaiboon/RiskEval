using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using riskEval;
using System.Data.SqlClient;

public partial class question_set_B : System.Web.UI.Page
{
    public const string Q2Seq = "คำถามที่ ";
    public question_set qset = new question_set();
    protected void Page_Load(object sender, EventArgs e)
    {

        
        
        #region loadData
            
        qset.qset_id = "2";
        qset.loadQuestionSet();
        qset.loadQuestionL1();
        lbl_qset_text.Text = qset.qset_text;

        for (int q1_i = 0; q1_i < qset.qL1_list.Count; q1_i++)
        {
            question q1 = qset.qL1_list[q1_i];
            q1.loadQuestionL2();
            for (int q2_i = 0; q2_i < q1.qL2_list.Count; q2_i++)
            {
                questionL2 q2 = q1.qL2_list[q2_i];
                q2.loadQuestionL3();
            }
        }
        #endregion
        #region MapQuestion to Interface
        //--------------ประเด็น3---------------------------
        lblQuestion_1.Text = qset.qL1_list[0].q1_text;

        lblAssumption_1.Text = qset.qL1_list[0].q1_assumption;

        lblQuestion_id_1.Text = qset.qL1_list[0].q1_id;
        lblQuestion_id_praden_1.Text = lblQuestion_id_1.Text;

        //---------------คำถาม----------------------------
        lblQuestion2_1.Text = Q2Seq + qset.qL1_list[0].qL2_list[0].q2_order + " " + qset.qL1_list[0].qL2_list[0].q2_text;

        //--------------คำถามย่อย--------------------------------
        lblQuestion3_1_1.Text = qset.qL1_list[0].qL2_list[0].qL3_list[0].q3_order + " " + qset.qL1_list[0].qL2_list[0].qL3_list[0].q3_text;
        lblQuestion3_1_2.Text = qset.qL1_list[0].qL2_list[0].qL3_list[1].q3_order + " " + qset.qL1_list[0].qL2_list[0].qL3_list[1].q3_text;
        lblQuestion3_1_3.Text = qset.qL1_list[0].qL2_list[0].qL3_list[2].q3_order + " " + qset.qL1_list[0].qL2_list[0].qL3_list[2].q3_text;

        //---------------คำถาม----------------------------
        lblQuestion2_2.Text = Q2Seq + qset.qL1_list[0].qL2_list[1].q2_order + " " + qset.qL1_list[0].qL2_list[1].q2_text;
        //--------------คำถามย่อย--------------------------------
        lblQuestion3_2_1.Text = qset.qL1_list[0].qL2_list[1].qL3_list[0].q3_order + " " + qset.qL1_list[0].qL2_list[1].qL3_list[0].q3_text;

        //---------------คำถาม----------------------------
        lblQuestion2_3.Text = Q2Seq + qset.qL1_list[0].qL2_list[2].q2_order + " " + qset.qL1_list[0].qL2_list[2].q2_text;
        //--------------คำถามย่อย--------------------------------
        lblQuestion3_3_1.Text = qset.qL1_list[0].qL2_list[2].qL3_list[0].q3_order + " " + qset.qL1_list[0].qL2_list[2].qL3_list[0].q3_text;

        //---------------คำถาม----------------------------
        lblQuestion2_4.Text = Q2Seq + qset.qL1_list[0].qL2_list[3].q2_order + " " + qset.qL1_list[0].qL2_list[3].q2_text;
        //--------------คำถามย่อย--------------------------------
        lblQuestion3_4_1.Text = qset.qL1_list[0].qL2_list[3].qL3_list[0].q3_order + " " + qset.qL1_list[0].qL2_list[3].qL3_list[0].q3_text;
        lblQuestion3_4_2.Text = qset.qL1_list[0].qL2_list[3].qL3_list[1].q3_order + " " + qset.qL1_list[0].qL2_list[3].qL3_list[1].q3_text;
        lblQuestion3_4_3.Text = qset.qL1_list[0].qL2_list[3].qL3_list[2].q3_order + " " + qset.qL1_list[0].qL2_list[3].qL3_list[2].q3_text;
        lblQuestion3_4_4.Text = qset.qL1_list[0].qL2_list[3].qL3_list[3].q3_order + " " + qset.qL1_list[0].qL2_list[3].qL3_list[3].q3_text;

        //-----------จบ ประเด็น3--------------------------------

        //--------------ประเด็น4---------------------------
        lblQuestion_2.Text = qset.qL1_list[1].q1_text;

        lblAssumption_2.Text = qset.qL1_list[1].q1_assumption;

        lblQuestion_id_2.Text = qset.qL1_list[1].q1_id;
        lblQuestion_id_praden_2.Text = lblQuestion_id_2.Text;

        lblQuestion2_5_1.Text = Q2Seq + qset.qL1_list[1].qL2_list[0].q2_order + " " + qset.qL1_list[1].qL2_list[0].q2_text;

        lblQuestion3_4_1_1.Text = qset.qL1_list[1].qL2_list[0].qL3_list[0].q3_order + " " + qset.qL1_list[1].qL2_list[0].qL3_list[0].q3_text;
        //-----------จบ ประเด็น4--------------------------------

        //--------------ประเด็น5---------------------------
        lblQuestion_3.Text = qset.qL1_list[2].q1_text;

        lblAssumption_3.Text = qset.qL1_list[2].q1_assumption;

        lblQuestion_id_3.Text = qset.qL1_list[2].q1_id;
        lblQuestion_id_praden_3.Text = lblQuestion_id_3.Text;

        lblQuestion2_6_1.Text = Q2Seq + qset.qL1_list[2].qL2_list[0].q2_order + " " + qset.qL1_list[2].qL2_list[0].q2_text;

        lblQuestion3_5_1_1.Text = qset.qL1_list[2].qL2_list[0].qL3_list[0].q3_order + " " + qset.qL1_list[2].qL2_list[0].qL3_list[0].q3_text;
        lblQuestion3_5_1_2.Text = qset.qL1_list[2].qL2_list[0].qL3_list[1].q3_order + " " + qset.qL1_list[2].qL2_list[0].qL3_list[1].q3_text;
        //-----------จบ ประเด็น5--------------------------------

        #endregion
        
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();
        string max = "";
        if (ck != null)
        {

            mgCookie.UpdateCookies("qset_id", qset.qset_id); //คำถามชุด ข.
            answer ans = new answer();
            max = ans.getLatestAnswerQ2(ck.pj_id, ans.getLatestAnswerQSetID(ck.pj_id));
            
            //if (pnlQL1_3.Visible == true)
            //{
            //    //ประเด็น 3
            //    mgCookie.UpdateCookies("q1_id", qset.qL1_list[0].q1_id);
            //    mgCookie.UpdateCookies("q2_id", qset.qL1_list[0].qL2_list[0].q2_id);
            //}
            //else if (pnlQL1_4.Visible == true)
            //{
            //    //ประเด็นที่ 4
            //    mgCookie.UpdateCookies("q1_id", qset.qL1_list[1].q1_id);
            //    mgCookie.UpdateCookies("q2_id", qset.qL1_list[1].qL2_list[0].q2_id);
            //}
            //else if (pnlQL1_5.Visible == true)
            //{
            //    //ประเด็นที่ 5
            //    mgCookie.UpdateCookies("q1_id", qset.qL1_list[2].q1_id);
            //    mgCookie.UpdateCookies("q2_id", qset.qL1_list[2].qL2_list[0].q2_id);
            //}
        }
        else
        {
            Response.Redirect(ton.config.Global_config.RootURL);
        }

        //ไม่ใช่
        if ((!Page.IsPostBack) && (ck != null) )
        {
            //-------get latest------
            string strQ2ID = max;
            //ton.JavaScript.MessageBox(strQ2ID);
            if (strQ2ID != null)
            {
                if (strQ2ID == "2")
                {
                    // Origin Q2_1
                    pnlQL1_3.Visible = true;
                    // Nested Panel inside pnlQL1_3
                    //--Only 1st Question visible
                    pnlQL2_1.Visible = true;
                    pnlQL2_2.Visible = false;
                    pnlQL2_3.Visible = false;
                    pnlQL2_4.Visible = false;
                    //-----End Nested  Panel -----
                    pnlQL1_4.Visible = false;
                    pnlQL1_5.Visible = false;
                }
                else if (strQ2ID == "3")
                {
                    // Next is Q2_2
                    pnlQL1_3.Visible = true;
                    // Nested Panel inside pnlQL1_3
                    //--Only 1st Question visible
                    pnlQL2_1.Visible = false;
                    pnlQL2_2.Visible = true;
                    pnlQL2_3.Visible = false;
                    pnlQL2_4.Visible = false;
                    //-----End Nested  Panel -----
                    pnlQL1_4.Visible = false;
                    pnlQL1_5.Visible = false;
                }
                else if (strQ2ID == "4")
                {
                    // Next is Q2_3
                    pnlQL1_3.Visible = true;
                    // Nested Panel inside pnlQL1_3
                    //--Only 1st Question visible
                    pnlQL2_1.Visible = false;
                    pnlQL2_2.Visible = false;
                    pnlQL2_3.Visible = true;
                    pnlQL2_4.Visible = false;
                    //-----End Nested  Panel -----
                    pnlQL1_4.Visible = false;
                    pnlQL1_5.Visible = false;
                }
                else if (strQ2ID == "5")
                {
                    // Next is Q2_4
                    pnlQL1_3.Visible = true;
                    // Nested Panel inside pnlQL1_3
                    //--Only 1st Question visible
                    pnlQL2_1.Visible = false;
                    pnlQL2_2.Visible = false;
                    pnlQL2_3.Visible = false;
                    pnlQL2_4.Visible = true;
                    //-----End Nested  Panel -----
                    pnlQL1_4.Visible = false;
                    pnlQL1_5.Visible = false;
                }
                else if (strQ2ID == "6")
                {
                    // Next is Q2_5
                    pnlQL1_3.Visible = false;
                    // Nested Panel inside pnlQL1_3
                    //--Only 1st Question visible
                    pnlQL2_1.Visible = false;
                    pnlQL2_2.Visible = false;
                    pnlQL2_3.Visible = false;
                    pnlQL2_4.Visible = false;
                    //-----End Nested  Panel -----
                    pnlQL1_4.Visible = true;
                    pnlQL1_5.Visible = false;
                }
                else if (strQ2ID == "7")
                {
                    // Next is Q2_6
                    pnlQL1_3.Visible = false;
                    // Nested Panel inside pnlQL1_3
                    //--Only 1st Question visible
                    pnlQL2_1.Visible = false;
                    pnlQL2_2.Visible = false;
                    pnlQL2_3.Visible = false;
                    pnlQL2_4.Visible = false;
                    //-----End Nested  Panel -----
                    pnlQL1_4.Visible = false;
                    pnlQL1_5.Visible = true;
                }
                else if (strQ2ID == "8")
                {
                    // Next is QSet C ค.
                    litfinish.Text = "เสร็จสิ้นการแก้ไขประเมิน" + qset.qset_text;
                    litfinish.Visible = true;
                    btnToQB.Visible = true;

                    pnlQL1_3.Visible = false;
                    pnlQL1_4.Visible = false;
                    pnlQL1_5.Visible = false;
                }
                else
                {
                    // Origin Q2_1
                    pnlQL1_3.Visible = false;
                    // Nested Panel inside pnlQL1_3
                    //--Only 1st Question visible
                    pnlQL2_1.Visible = false;
                    pnlQL2_2.Visible = false;
                    pnlQL2_3.Visible = false;
                    pnlQL2_4.Visible = false;
                    //-----End Nested  Panel -----
                    pnlQL1_4.Visible = false;
                    pnlQL1_5.Visible = false;
                }
            }
            //------------------


        /*
        #region LoadData_from_DB
            // Load and Map DB Data For Edit Mode only
            string strSQL1 = "";
            SqlCommand cmd = new SqlCommand();
            DataView dv1 = new DataView();
            // Load and Map Data from DB to interface
            //คำตอบ
            // ประเด็นที่ 3 
            #region คำตอบ คำถามที่ 1
            strSQL1 = "select * from dbo.answer_q2 q2, answer_q3 q3 "
        + " where q2.qset_id = @qset_id and "
        + "q2.pj_id = @pj_id and "
        + "q2.q2_id = @q2_id and "
        + "q2.answer_q2_id = q3.answer_q2_id";

            cmd = new SqlCommand(strSQL1);
            cmd.Parameters.AddWithValue("@qset_id", qset.qset_id);
            cmd.Parameters.AddWithValue("@pj_id", ck.pj_id);
            cmd.Parameters.AddWithValue("@q2_id", qset.qL1_list[0].qL2_list[0].q2_id);


            dv1 = ton.Data.DBHelper.getDataSet(cmd).Tables[0].DefaultView;

            
            radanswer2_1.SelectedValue = dv1.Table.Rows[0]["answer_q2_text"].ToString();
            txtAnswerQuestion3_1_1.Text = dv1.Table.Rows[0]["answer_q3_text"].ToString();
            txtAnswerQuestion3_1_2.Text = dv1.Table.Rows[1]["answer_q3_text"].ToString();
            txtAnswerQuestion3_1_3.Text = dv1.Table.Rows[2]["answer_q3_text"].ToString(); 
            #endregion

            #region คำตอบ คำถามที่ 2
            strSQL1 = "select * from dbo.answer_q2 q2, answer_q3 q3 "
        + " where q2.qset_id = @qset_id and "
        + "q2.pj_id = @pj_id and "
        + "q2.q2_id = @q2_id and "
        + "q2.answer_q2_id = q3.answer_q2_id";

            cmd = new SqlCommand(strSQL1);
            cmd.Parameters.AddWithValue("@qset_id", qset.qset_id);
            cmd.Parameters.AddWithValue("@pj_id", ck.pj_id);
            cmd.Parameters.AddWithValue("@q2_id", qset.qL1_list[0].qL2_list[1].q2_id);


            dv1 = ton.Data.DBHelper.getDataSet(cmd).Tables[0].DefaultView;

            radanswer2_2.SelectedValue = dv1.Table.Rows[0]["answer_q2_text"].ToString();
            txtAnswerQuestion3_2_1.Text = dv1.Table.Rows[0]["answer_q3_text"].ToString();
            #endregion

            #region คำตอบ คำถามที่ 3
            strSQL1 = "select * from dbo.answer_q2 q2, answer_q3 q3 "
        + " where q2.qset_id = @qset_id and "
        + "q2.pj_id = @pj_id and "
        + "q2.q2_id = @q2_id and "
        + "q2.answer_q2_id = q3.answer_q2_id";

            cmd = new SqlCommand(strSQL1);
            cmd.Parameters.AddWithValue("@qset_id", qset.qset_id);
            cmd.Parameters.AddWithValue("@pj_id", ck.pj_id);
            cmd.Parameters.AddWithValue("@q2_id", qset.qL1_list[0].qL2_list[2].q2_id);


            dv1 = ton.Data.DBHelper.getDataSet(cmd).Tables[0].DefaultView;

            radanswer2_3.SelectedValue = dv1.Table.Rows[0]["answer_q2_text"].ToString();
            txtAnswerQuestion3_3_1.Text = dv1.Table.Rows[0]["answer_q3_text"].ToString();
            #endregion

            #region คำตอบ คำถามที่ 4
            strSQL1 = "select * from dbo.answer_q2 q2, answer_q3 q3 "
        + " where q2.qset_id = @qset_id and "
        + "q2.pj_id = @pj_id and "
        + "q2.q2_id = @q2_id and "
        + "q2.answer_q2_id = q3.answer_q2_id";

            cmd = new SqlCommand(strSQL1);
            cmd.Parameters.AddWithValue("@qset_id", qset.qset_id);
            cmd.Parameters.AddWithValue("@pj_id", ck.pj_id);
            cmd.Parameters.AddWithValue("@q2_id", qset.qL1_list[0].qL2_list[3].q2_id);


            dv1 = ton.Data.DBHelper.getDataSet(cmd).Tables[0].DefaultView;

            radanswer2_4.SelectedValue = dv1.Table.Rows[0]["answer_q2_text"].ToString();
            txtAnswerQuestion3_4_1.Text = dv1.Table.Rows[0]["answer_q3_text"].ToString();
            txtAnswerQuestion3_4_2.Text = dv1.Table.Rows[1]["answer_q3_text"].ToString();
            txtAnswerQuestion3_4_3.Text = dv1.Table.Rows[2]["answer_q3_text"].ToString();
            txtAnswerQuestion3_4_4.Text = dv1.Table.Rows[3]["answer_q3_text"].ToString();
            #endregion

            //ประเด็นที่ 4
            #region คำตอบ คำถามที่ 5
            strSQL1 = "select * from dbo.answer_q2 q2, answer_q3 q3 "
        + " where q2.qset_id = @qset_id and "
        + "q2.pj_id = @pj_id and "
        + "q2.q2_id = @q2_id and "
        + "q2.answer_q2_id = q3.answer_q2_id";

            cmd = new SqlCommand(strSQL1);
            cmd.Parameters.AddWithValue("@qset_id", qset.qset_id);
            cmd.Parameters.AddWithValue("@pj_id", ck.pj_id);
            cmd.Parameters.AddWithValue("@q2_id", qset.qL1_list[1].qL2_list[0].q2_id);


            dv1 = ton.Data.DBHelper.getDataSet(cmd).Tables[0].DefaultView;

            radanswer2_5_1.SelectedValue = dv1.Table.Rows[0]["answer_q2_text"].ToString();
            txtAnswerQuestion3_4_1_1.Text = dv1.Table.Rows[0]["answer_q3_text"].ToString();
            #endregion 

            //ประเด็นที่ 5
            #region คำตอบ คำถามที่ 6
            strSQL1 = "select * from dbo.answer_q2 q2, answer_q3 q3 "
        + " where q2.qset_id = @qset_id and "
        + "q2.pj_id = @pj_id and "
        + "q2.q2_id = @q2_id and "
        + "q2.answer_q2_id = q3.answer_q2_id";

            cmd = new SqlCommand(strSQL1);
            cmd.Parameters.AddWithValue("@qset_id", qset.qset_id);
            cmd.Parameters.AddWithValue("@pj_id", ck.pj_id);
            cmd.Parameters.AddWithValue("@q2_id", qset.qL1_list[2].qL2_list[0].q2_id);


            dv1 = ton.Data.DBHelper.getDataSet(cmd).Tables[0].DefaultView;

            radanswer2_6_1.SelectedValue = dv1.Table.Rows[0]["answer_q2_text"].ToString();
            txtAnswerQuestion3_5_1_1.Text = dv1.Table.Rows[0]["answer_q3_text"].ToString();
            txtAnswerQuestion3_5_1_2.Text = dv1.Table.Rows[1]["answer_q3_text"].ToString();

            #endregion
        #endregion
        */
        }

        // set cookies to keep the lastest question Set , Q2
        #region set cookies follow to visible panel

        #region ไม่ใช้
        //if (pnlQL1_3.Visible == true)
        //{
        //    mgCookie.UpdateCookies("q1_id", "3"); //ประเด็นที่สาม

        //    if (pnlQL2_1.Visible == true)
        //    {
        //        mgCookie.UpdateCookies("q2_id", "3"); //คำถามที่1 , IDคำถามเริ่มที่3
        //    }
        //    else if (pnlQL2_2.Visible == true)
        //    {
        //        mgCookie.UpdateCookies("q2_id", "4"); //คำถามที่2
        //    }
        //    else if (pnlQL2_3.Visible == true)
        //    {
        //        mgCookie.UpdateCookies("q2_id", "5"); //คำถามที่3
        //    }
        //    else if (pnlQL2_4.Visible == true)
        //    {
        //        mgCookie.UpdateCookies("q2_id", "6"); //คำถามที่3
        //    }
        //}
        //else if (pnlQL1_4.Visible == true)
        //{
        //    mgCookie.UpdateCookies("q1_id", "4");  //ประเด็นที่สี่
        //    mgCookie.UpdateCookies("q2_id", "7");  //คำถามที่ห้า
        //}
        //else if (pnlQL1_5.Visible == true)
        //{
        //    mgCookie.UpdateCookies("q1_id", "5");  //ประเด็นที่ห้า
        //    mgCookie.UpdateCookies("q2_id", "8");  //คำถามที่หก
        //} 
        #endregion

        #endregion
        //----------------------------------------


        //คำถามที่ 1
        if (radanswer2_1.SelectedValue == "มี") // ตอบว่า มี
        {
            txtAnswerQuestion3_1_1.Enabled = txtAnswerQuestion3_1_2.Enabled = txtAnswerQuestion3_1_3.Enabled = true;
            reqvld_3_1_1.Enabled = reqvld_3_1_2.Enabled = reqvld_3_1_3.Enabled = true;
        }
        else
        {
            txtAnswerQuestion3_1_1.Enabled = txtAnswerQuestion3_1_2.Enabled = txtAnswerQuestion3_1_3.Enabled = false;
            reqvld_3_1_1.Enabled = reqvld_3_1_2.Enabled = reqvld_3_1_3.Enabled = false;
        }

        //คำถามที่ 2
        if (radanswer2_2.SelectedValue == "มี") // ตอบว่า มี
        {
            txtAnswerQuestion3_2_1.Enabled = true;
            reqvld_3_2_1.Enabled = true;
        }
        else
        {
            txtAnswerQuestion3_2_1.Enabled = false;
            reqvld_3_2_1.Enabled = false;
        }

        //คำถามที่ 3
        if (radanswer2_3.SelectedValue == "มี") // ตอบว่า มี
        {
            txtAnswerQuestion3_3_1.Enabled = true;
            reqvld_3_3_1.Enabled = true;
        }
        else
        {
            txtAnswerQuestion3_3_1.Enabled = false;
            reqvld_3_3_1.Enabled = false;
        }
        
        //คำถามที่ 4
        if (radanswer2_4.SelectedValue == "มี") // ตอบว่า มี
        {
            txtAnswerQuestion3_4_1.Enabled = txtAnswerQuestion3_4_2.Enabled = txtAnswerQuestion3_4_3.Enabled = txtAnswerQuestion3_4_4.Enabled = true;
            reqvld_3_4_1.Enabled = reqvld_3_4_2.Enabled = reqvld_3_4_3.Enabled = reqvld_3_4_4.Enabled = true;
        }
        else
        {
            txtAnswerQuestion3_4_1.Enabled = txtAnswerQuestion3_4_2.Enabled = txtAnswerQuestion3_4_3.Enabled = txtAnswerQuestion3_4_4.Enabled = false;
            reqvld_3_4_1.Enabled = reqvld_3_4_2.Enabled = reqvld_3_4_3.Enabled = reqvld_3_4_4.Enabled = false;
        }

        //คำถามที่ 5
        if ((radanswer2_5_1.SelectedValue == "มี") || (radanswer2_5_1.SelectedValue == "มีบางส่วน"))  // ตอบว่า มี หรือ มีบางส่วน
        {
            txtAnswerQuestion3_4_1_1.Enabled = true;
            reqvld3_4_1_1.Enabled = true;
        }
        else
        {
            txtAnswerQuestion3_4_1_1.Enabled = false;
            reqvld3_4_1_1.Enabled = false;
        }

        //คำถามที่ 6
        if (radanswer2_6_1.SelectedValue == "มี")   // ตอบว่า มี หรือ มีบางส่วน
        {
            txtAnswerQuestion3_5_1_1.Enabled = txtAnswerQuestion3_5_1_2.Enabled = true;
            reqvld_3_5_1_1.Enabled = reqvld_3_5_1_2.Enabled = true;
        }
        else
        {
            txtAnswerQuestion3_5_1_1.Enabled = txtAnswerQuestion3_5_1_2.Enabled = false;
            reqvld_3_5_1_1.Enabled = reqvld_3_5_1_2.Enabled = false;
        }
        
    }

    protected void btnSaveQL1(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        Button btn_temp = sender as Button;
        string arg = btn_temp.CommandArgument;

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        answer ans = new answer();

        int ret = -1;
        int ret2 = 0;

        if (ck == null)
        {
            ton.JavaScript.MessageBox(" ข้อมูลโปรเจค ว่างเปล่า \r\n กรุณาเข้าสู่ระบบ ");
            return;
        }
        
        if (arg == "3.2.1")
        {
            //ประเด็น3

            #region 3.2.1
            ret = ans.insertOrUpdateAnswerQ2(radanswer2_1.SelectedValue, ck.pj_id, qset.qL1_list[0].qL2_list[0].q2_id, ck.qset_id);
            mgCookie.UpdateCookies("answer_q2_id", ret.ToString());
            // มี หรือ ไม่มี
            if (radanswer2_1.SelectedValue == "มี") // ตอบว่า มี
            {
                // คำถามย่อยข้อ 1.1
                ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_1_1.Text, ck.pj_id, qset.qL1_list[0].qL2_list[0].qL3_list[0].q3_id, ret.ToString());

                // คำถามย่อยข้อ 1.2
                ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_1_2.Text, ck.pj_id, qset.qL1_list[0].qL2_list[0].qL3_list[1].q3_id, ret.ToString());

                // คำถามย่อยข้อ 1.3
                ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_1_3.Text, ck.pj_id, qset.qL1_list[0].qL2_list[0].qL3_list[2].q3_id, ret.ToString());

            }
            else
            {
                // คำถามย่อยข้อ 1.1
                ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, qset.qL1_list[0].qL2_list[0].qL3_list[0].q3_id, ret.ToString());

                // คำถามย่อยข้อ 1.2
                ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, qset.qL1_list[0].qL2_list[0].qL3_list[1].q3_id, ret.ToString());

                // คำถามย่อยข้อ 1.3
                ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, qset.qL1_list[0].qL2_list[0].qL3_list[2].q3_id, ret.ToString());
            } 
            #endregion

            if (ret2 > 0)
            {
                //บันทึกสำเร็จ
                pnlQL1_3.Visible = true;
                // Nested Panel inside pnlQL1_3
                //--Only 1st Question visible
                pnlQL2_1.Visible = false;
                pnlQL2_2.Visible = true;
                pnlQL2_3.Visible = false;
                pnlQL2_4.Visible = false;
                //-----End Nested  Panel -----
                pnlQL1_4.Visible = false;
                pnlQL1_5.Visible = false;
            }
            else
            {
                //บันทึกไม่สำเร็จ
                ton.JavaScript.MessageBox("บันทึก ประเด็น3 ไม่สำเร็จ");
            }
        }
        else if (arg == "3.2.2")
        {
            //ประเด็น3

            #region 3.2.2
            ret = ans.insertOrUpdateAnswerQ2(radanswer2_2.SelectedValue, ck.pj_id, qset.qL1_list[0].qL2_list[1].q2_id, ck.qset_id);
            mgCookie.UpdateCookies("answer_q2_id", ret.ToString());
            if (radanswer2_2.SelectedValue == "มี") // ตอบว่า มี
            {
                // คำถามย่อยข้อ 2.1
                ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_2_1.Text, ck.pj_id, qset.qL1_list[0].qL2_list[1].qL3_list[0].q3_id, ret.ToString());
            }
            else
            {
                // คำถามย่อยข้อ 2.1
                ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, qset.qL1_list[0].qL2_list[1].qL3_list[0].q3_id, ret.ToString());
            }
            #endregion

            if (ret2 > 0)
            {
                //บันทึกสำเร็จ
                pnlQL1_3.Visible = true;
                // Nested Panel inside pnlQL1_3
                //--Only 1st Question visible
                pnlQL2_1.Visible = false;
                pnlQL2_2.Visible = false;
                pnlQL2_3.Visible = true;
                pnlQL2_4.Visible = false;
                //-----End Nested  Panel -----
                pnlQL1_4.Visible = false;
                pnlQL1_5.Visible = false;
            }
            else
            {
                //บันทึกไม่สำเร็จ
                ton.JavaScript.MessageBox("บันทึก ประเด็น3 ไม่สำเร็จ");
            }
        }
        else if (arg == "3.2.3")
        {
            //ประเด็น3

            #region 3.2.3
            ret = ans.insertOrUpdateAnswerQ2(radanswer2_3.SelectedValue, ck.pj_id, qset.qL1_list[0].qL2_list[2].q2_id, ck.qset_id);
            mgCookie.UpdateCookies("answer_q2_id", ret.ToString());
            if (radanswer2_3.SelectedValue == "มี") // ตอบว่า มี
            {
                // คำถามย่อยข้อ 3.1
                ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_3_1.Text, ck.pj_id, qset.qL1_list[0].qL2_list[2].qL3_list[0].q3_id, ret.ToString());
            }
            else
            {
                ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, qset.qL1_list[0].qL2_list[2].qL3_list[0].q3_id, ret.ToString());
            }
            #endregion

            if (ret2 > 0)
            {
                //บันทึกสำเร็จ
                pnlQL1_3.Visible = true;
                // nested panel inside pnlql1_3
                //--only 1st question visible
                pnlQL2_1.Visible = false;
                pnlQL2_2.Visible = false;
                pnlQL2_3.Visible = false;
                pnlQL2_4.Visible = true;
                //-----end nested  panel -----
                pnlQL1_4.Visible = false;
                pnlQL1_5.Visible = false;
            }
            else
            {
                //บันทึกไม่สำเร็จ
                ton.JavaScript.MessageBox("บันทึก ประเด็น3 ไม่สำเร็จ");
            }
        }
        else if (arg == "3.2.4")
        {
            //ประเด็น3

            #region 3.2.4
            ret = ans.insertOrUpdateAnswerQ2(radanswer2_4.SelectedValue, ck.pj_id, qset.qL1_list[0].qL2_list[3].q2_id, ck.qset_id);
            mgCookie.UpdateCookies("answer_q2_id", ret.ToString());
            if (radanswer2_4.SelectedValue == "มี")
            {
                // คำถามย่อยข้อ 4.1
                ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_4_1.Text, ck.pj_id, qset.qL1_list[0].qL2_list[3].qL3_list[0].q3_id, ret.ToString());
                // คำถามย่อยข้อ 4.2
                ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_4_2.Text, ck.pj_id, qset.qL1_list[0].qL2_list[3].qL3_list[1].q3_id, ret.ToString());
                // คำถามย่อยข้อ 4.3
                ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_4_3.Text, ck.pj_id, qset.qL1_list[0].qL2_list[3].qL3_list[2].q3_id, ret.ToString());
                // คำถามย่อยข้อ 4.4
                ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_4_4.Text, ck.pj_id, qset.qL1_list[0].qL2_list[3].qL3_list[3].q3_id, ret.ToString());
            }
            else
            {
                ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, qset.qL1_list[0].qL2_list[3].qL3_list[0].q3_id, ret.ToString());
                ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, qset.qL1_list[0].qL2_list[3].qL3_list[1].q3_id, ret.ToString());
                ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, qset.qL1_list[0].qL2_list[3].qL3_list[2].q3_id, ret.ToString());
                ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, qset.qL1_list[0].qL2_list[3].qL3_list[3].q3_id, ret.ToString());

            }
            #endregion

            if (ret2 > 0)
            {
                //บันทึกสำเร็จ
                pnlQL1_3.Visible = false;
                // Nested Panel inside pnlQL1_3
                //--Only 1st Question visible
                pnlQL2_1.Visible = false;
                pnlQL2_2.Visible = false;
                pnlQL2_3.Visible = false;
                pnlQL2_4.Visible = false;
                //-----End Nested  Panel -----
                pnlQL1_4.Visible = true;
                pnlQL1_5.Visible = false;
            }
            else
            {
                //บันทึกไม่สำเร็จ
                ton.JavaScript.MessageBox("บันทึก ประเด็น3 ไม่สำเร็จ");
            }
        }
        else if (arg == "4")
        {
            //ประเด็น4
            ret = ans.insertOrUpdateAnswerQ2(radanswer2_5_1.SelectedValue, ck.pj_id, qset.qL1_list[1].qL2_list[0].q2_id, ck.qset_id);
            mgCookie.UpdateCookies("answer_q2_id", ret.ToString());
            // มี หรือ ไม่มี
            if ((radanswer2_5_1.SelectedValue == "มี") || (radanswer2_5_1.SelectedValue == "มีบางส่วน"))  // ตอบว่า มี
            {
                // คำถามย่อยข้อ 5.1
                ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_4_1_1.Text, ck.pj_id, qset.qL1_list[1].qL2_list[0].qL3_list[0].q3_id, ret.ToString());
            }
            else
            {
                // คำถามย่อยข้อ 5.1
                ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, qset.qL1_list[1].qL2_list[0].qL3_list[0].q3_id, ret.ToString());
            }
            if (ret2 > 0)
            {
                //บันทึกสำเร็จ
                pnlQL1_3.Visible = false;
                pnlQL1_4.Visible = false;
                pnlQL1_5.Visible = true;

            }
            else
            {
                //บันทึกไม่สำเร็จ
                ton.JavaScript.MessageBox("บันทึก ประเด็น4 ไม่สำเร็จ");
            }
            
        }
        else if (arg == "5")
        {
            //ประเด็น5
            ret = ans.insertOrUpdateAnswerQ2(radanswer2_6_1.SelectedValue, ck.pj_id, qset.qL1_list[2].qL2_list[0].q2_id, ck.qset_id);
            mgCookie.UpdateCookies("answer_q2_id", ret.ToString());
            // มี หรือ ไม่มี
            if (radanswer2_6_1.SelectedValue == "มี") // ตอบว่า มี
            {
                // คำถามย่อยข้อ 6.1
                ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_5_1_1.Text, ck.pj_id, qset.qL1_list[2].qL2_list[0].qL3_list[0].q3_id, ret.ToString());
                // คำถามย่อยข้อ 6.2
                ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_5_1_2.Text, ck.pj_id, qset.qL1_list[2].qL2_list[0].qL3_list[1].q3_id, ret.ToString());
            }
            else
            {
                // คำถามย่อยข้อ 6.1
                ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, qset.qL1_list[2].qL2_list[0].qL3_list[0].q3_id, ret.ToString());
                // คำถามย่อยข้อ 6.2
                ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, qset.qL1_list[2].qL2_list[0].qL3_list[1].q3_id, ret.ToString());
            }
            if (ret2 > 0)
            {
                //บันทึกสำเร็จ
                litfinish.Text = "เสร็จสิ้นการแก้ไขประเมิน" + qset.qset_text;
                litfinish.Visible = true;
                btnToQB.Visible = true;

                pnlQL1_3.Visible = false;
                pnlQL1_4.Visible = false;
                pnlQL1_5.Visible = false;

            }
            else
            {
                //บันทึกไม่สำเร็จ
                ton.JavaScript.MessageBox("บันทึก ประเด็น5 ไม่สำเร็จ");
            }
        }
    }
    protected void radanswer2_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rad_temp = sender as RadioButtonList;
        //คำถามที่ 1
        if (radanswer2_1.SelectedValue == "มี") // ตอบว่า มี
        {
            txtAnswerQuestion3_1_1.Enabled = txtAnswerQuestion3_1_2.Enabled = txtAnswerQuestion3_1_3.Enabled = true;
            reqvld_3_1_1.Enabled = reqvld_3_1_2.Enabled = reqvld_3_1_3.Enabled = true;
        }
        else
        {
            txtAnswerQuestion3_1_1.Enabled = txtAnswerQuestion3_1_2.Enabled = txtAnswerQuestion3_1_3.Enabled = false;
            reqvld_3_1_1.Enabled = reqvld_3_1_2.Enabled = reqvld_3_1_3.Enabled = false;
        }
    }
    protected void radanswer2_2_SelectedIndexChanged(object sender, EventArgs e)
    { 
        RadioButtonList rad_temp = sender as RadioButtonList;
        //คำถามที่ 2
        if (radanswer2_2.SelectedValue == "มี") // ตอบว่า มี
        {
            txtAnswerQuestion3_2_1.Enabled = true;
            reqvld_3_2_1.Enabled =  true;
        }
        else
        {
            txtAnswerQuestion3_2_1.Enabled = false;
            reqvld_3_2_1.Enabled = false;
        }
    }
    protected void radanswer2_3_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rad_temp = sender as RadioButtonList;
        //คำถามที่ 3
        if (radanswer2_3.SelectedValue == "มี") // ตอบว่า มี
        {
            txtAnswerQuestion3_3_1.Enabled = true;
            reqvld_3_3_1.Enabled = true;
        }
        else
        {
            txtAnswerQuestion3_3_1.Enabled = false;
            reqvld_3_3_1.Enabled = false;
        }
    }
    protected void radanswer2_4_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rad_temp = sender as RadioButtonList;
        //คำถามที่ 4
        if (radanswer2_4.SelectedValue == "มี") // ตอบว่า มี
        {
            txtAnswerQuestion3_4_1.Enabled = txtAnswerQuestion3_4_2.Enabled = txtAnswerQuestion3_4_3.Enabled = txtAnswerQuestion3_4_4.Enabled = true;
            reqvld_3_4_1.Enabled = reqvld_3_4_2.Enabled = reqvld_3_4_3.Enabled = reqvld_3_4_4.Enabled = true;
        }
        else
        {
            txtAnswerQuestion3_4_1.Enabled = txtAnswerQuestion3_4_2.Enabled = txtAnswerQuestion3_4_3.Enabled = txtAnswerQuestion3_4_4.Enabled = false;
            reqvld_3_4_1.Enabled = reqvld_3_4_2.Enabled = reqvld_3_4_3.Enabled = reqvld_3_4_4.Enabled = false;
        }
    }
    protected void radanswer2_5_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rad_temp = sender as RadioButtonList;
        //คำถามที่ 5
        if ((radanswer2_5_1.SelectedValue == "มี") || (radanswer2_5_1.SelectedValue == "มีบางส่วน"))  // ตอบว่า มี หรือ มีบางส่วน
        {
            txtAnswerQuestion3_4_1_1.Enabled = true;
            reqvld3_4_1_1.Enabled = true;
        }
        else
        {
            txtAnswerQuestion3_4_1_1.Enabled = false;
            reqvld3_4_1_1.Enabled = false;
        }
    }
    protected void radanswer2_6_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rad_temp = sender as RadioButtonList;
        //คำถามที่ 6
        if (radanswer2_6_1.SelectedValue == "มี")   // ตอบว่า มี หรือ มีบางส่วน
        {
            txtAnswerQuestion3_5_1_1.Enabled = txtAnswerQuestion3_5_1_2.Enabled = true;
            reqvld_3_5_1_1.Enabled = reqvld_3_5_1_2.Enabled = true;
        }
        else
        {
            txtAnswerQuestion3_5_1_1.Enabled = txtAnswerQuestion3_5_1_2.Enabled = false;
            reqvld_3_5_1_1.Enabled = reqvld_3_5_1_2.Enabled = false;
        }
    }
    protected void btnToQC_Click(object sender, EventArgs e)
    {
        //Response.Redirect("question_set_C.aspx");
        //Edit Mode
        Response.Redirect("project_pickquestion.aspx");
    }
}