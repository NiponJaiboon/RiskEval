using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ton;
using System.Data.SqlClient;
using riskEval;

public partial class question_set_A : System.Web.UI.Page
{

    private List<SqlParameter> insertParameters = new List<SqlParameter>();

    protected void Page_Load(object sender, EventArgs e)
    {

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();
        string max = "";
        if (ck != null)
        {

            string strSQL1 = "select * from question1 q1, question2 q2, question3 q3 where q1.qset_id = 1 and q1.q1_id = q2.q1_id and q2.q2_id = q3.q2_id and q1.q1_id = 1";

            SqlDataSource1.SelectCommand = strSQL1;
            SqlDataSource1.DataBind();

            DataView dv1 = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

            //ประเด็นที่หนึ่ง
            lblQuestion1.Text = "ประเด็นที่ " + dv1.Table.Rows[0]["q1_order"].ToString() + " " + dv1.Table.Rows[0]["q1_text"].ToString();
            lblAssumption.Text = "สมมุติฐาน " + dv1.Table.Rows[0]["q1_order"].ToString() + " " + dv1.Table.Rows[0]["q1_assumption"].ToString();

            lblQuestion2_1.Text = "คำถามที่ " + dv1.Table.Rows[0]["q2_order"].ToString() + " " + dv1.Table.Rows[0]["q2_text"].ToString();

            lblQuestion3_1.Text = dv1.Table.Rows[0]["q3_order"].ToString() + " " + dv1.Table.Rows[0]["q3_text"].ToString();
            lblQuestion3_2.Text = dv1.Table.Rows[1]["q3_order"].ToString() + " " + dv1.Table.Rows[1]["q3_text"].ToString();
            lblQuestion3_3.Text = dv1.Table.Rows[2]["q3_order"].ToString() + " " + dv1.Table.Rows[2]["q3_text"].ToString();
            lblQuestion3_4.Text = dv1.Table.Rows[3]["q3_order"].ToString() + " " + dv1.Table.Rows[3]["q3_text"].ToString();
            lblQuestion3_5.Text = dv1.Table.Rows[4]["q3_order"].ToString() + " " + dv1.Table.Rows[4]["q3_text"].ToString();



            //ประเด็นที่สอง
            string strSQL2 = "select * from question1 q1, question2 q2, question3 q3 where q1.qset_id = 1 and q1.q1_id = q2.q1_id and q2.q2_id = q3.q2_id and q1.q1_id = 2";

            SqlDataSource2.SelectCommand = strSQL2;
            SqlDataSource2.DataBind();

            DataView dv2 = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            lblQuestion2.Text = "ประเด็นที่ " + dv2.Table.Rows[0]["q1_order"].ToString() + " " + dv2.Table.Rows[0]["q1_text"].ToString();
            lblAssumption2.Text = "สมมุติฐาน " + dv2.Table.Rows[0]["q1_order"].ToString() + " " + dv2.Table.Rows[0]["q1_assumption"].ToString();

            lblQuestion2_2.Text = "คำถามที่ " + dv2.Table.Rows[0]["q2_order"].ToString() + " " + dv2.Table.Rows[0]["q2_text"].ToString();

            lblQuestion3_6.Text = dv2.Table.Rows[0]["q3_order"].ToString() + " " + dv2.Table.Rows[0]["q3_text"].ToString();
            lblQuestion3_7.Text = dv2.Table.Rows[1]["q3_order"].ToString() + " " + dv2.Table.Rows[1]["q3_text"].ToString();
            lblQuestion3_8.Text = dv2.Table.Rows[2]["q3_order"].ToString() + " " + dv2.Table.Rows[2]["q3_text"].ToString();

            mgCookie.UpdateCookies("qset_id", "1"); //คำถามชุด ก.
            
              if (!Page.IsPostBack)
            {
                // ต้น comment
                //string strQ2ID = ck.q2_id;

                #region ต้น
                answer ans = new answer();
                max = ans.getLatestAnswerQ2(ck.pj_id, ans.getLatestAnswerQSetID(ck.pj_id));
                string strQ2ID = string.IsNullOrEmpty(max) ? "0" : max;

                #endregion

                if (strQ2ID != null)
                {
                    // ค่า max ที่ได้คือข้อล่าสุดที่ได้ทำไป ดังนั้นข้อต่อไปคือ max + 1
                    if (strQ2ID == "0")
                    {
                        panel1.Visible = true;
                        panel2.Visible = false;
                    }
                    else if (strQ2ID == "1")
                    {
                        panel1.Visible = false;
                        panel2.Visible = true;
                    }
                    else if (strQ2ID == "2")
                    {
                        litfinish.Text = "เสร็จสิ้นการประเมินชุด ก: ขั้นตอนการริเริ่มแผนงาน/โครงการและวิเคราะห์เบื้องต้น โปรดดำเนินการต่อ";
                        litfinish.Visible = true;
                        btnToQB.Visible = true;
                        panel1.Visible = false;
                        panel2.Visible = false;
                    }
                    else
                    {
                        
                        litfinish.Visible = false;
                        btnToQB.Visible = false;
                        panel1.Visible = false;
                        panel2.Visible = false;
                    }

                }

            }

            if (panel1.Visible == true)
            {
                mgCookie.UpdateCookies("q1_id", "1"); //ประเด็นที่หนึ่ง
                //mgCookie.UpdateCookies("q2_id", dv1.Table.Rows[0]["q2_id"].ToString());
                mgCookie.UpdateCookies("q2_id", "1");

            }
            else if (panel2.Visible == true)
            {
                mgCookie.UpdateCookies("q1_id", "2");  //ประเด็นที่สอง
                //mgCookie.UpdateCookies("q2_id", dv2.Table.Rows[0]["q2_id"].ToString());
                mgCookie.UpdateCookies("q2_id", "2");
            }

        }

    }

    protected void btnNextToQ2_2_Click(object sender, EventArgs e)
    {
        //บันทักข้อมูล แล้วแสดงประเด็นที่ 2
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        answer ans = new answer();

        int ret = ans.insertOrUpdateAnswerQ2(radanswer2_1.SelectedValue, ck.pj_id, "1", "1");

        mgCookie.UpdateCookies("answer_q2_id", ret.ToString());

        int ret2 = 0;

        if (radanswer2_1.SelectedIndex == 0) // ตอบว่า ใช่
        {
            // คำถามย่อยข้อ 1.1
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_1.Text, ck.pj_id, "1", ret.ToString());

            // คำถามย่อยข้อ 1.2
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_2.Text, ck.pj_id, "2", ret.ToString());

            // คำถามย่อยข้อ 1.3
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_3.Text, ck.pj_id, "3", ret.ToString());

            // คำถามย่อยข้อ 1.4
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_4.Text, ck.pj_id, "4", ret.ToString());

            // คำถามย่อยข้อ 1.5
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_5.Text, ck.pj_id, "5", ret.ToString());
        }
        else
        {
            // คำถามย่อยข้อ 1.1
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "1", ret.ToString());

            // คำถามย่อยข้อ 1.2
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "2", ret.ToString());

            // คำถามย่อยข้อ 1.3
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "3", ret.ToString());

            // คำถามย่อยข้อ 1.4
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "4", ret.ToString());

            // คำถามย่อยข้อ 1.5
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "5", ret.ToString());

        }



        if (ret2 > 0)
        {
            //บันทึกสำเร็จ

            panel1.Visible = false;
            panel2.Visible = true;

        }
        else
        {
            //บันทึกไม่สำเร็จ

        }

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        //บันทักข้อมูล
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        answer ans = new answer();

        int ret = ans.insertOrUpdateAnswerQ2(radQ2.SelectedValue, ck.pj_id, "2", "1");

        mgCookie.UpdateCookies("answer_q2_id", ret.ToString());

        int ret2 = 0;

        if (radQ2.SelectedIndex == 0) // ตอบว่า ใช่
        {
            // คำถามย่อยข้อ 1.1
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_6.Text, ck.pj_id, "6", ret.ToString());

            // คำถามย่อยข้อ 1.2
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_7.Text, ck.pj_id, "7", ret.ToString());

            // คำถามย่อยข้อ 1.3
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion3_8.Text, ck.pj_id, "8", ret.ToString());
        }
        else
        {
            // คำถามย่อยข้อ 1.1
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "6", ret.ToString());

            // คำถามย่อยข้อ 1.2
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "7", ret.ToString());

            // คำถามย่อยข้อ 1.3
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "8", ret.ToString());

        }

        if (ret2 > 0)
        {
            //บันทึกสำเร็จ

            litfinish.Text = "เสร็จสิ้นการประเมินชุด ก: ขั้นตอนการริเริ่มแผนงาน/โครงการและวิเคราะห์เบื้องต้น โปรดดำเนินการต่อ";
            litfinish.Visible = true;
            btnToQB.Visible = true;
            panel2.Visible = false;

        }
        else
        {
            //บันทึกไม่สำเร็จ

        }
    }

    protected void answer2_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radanswer2_1.SelectedIndex == 0)
        {
            txtAnswerQuestion3_1.Enabled = true;
            txtAnswerQuestion3_2.Enabled = true;
            txtAnswerQuestion3_3.Enabled = true;
            txtAnswerQuestion3_4.Enabled = true;
            txtAnswerQuestion3_5.Enabled = true;

            RequiredFieldValidator1.Enabled = true;
            RequiredFieldValidator2.Enabled = true;
            RequiredFieldValidator3.Enabled = true;
            RequiredFieldValidator4.Enabled = true;
            RequiredFieldValidator5.Enabled = true;

            RegularExpressionValidator1.Enabled = true;
            RegularExpressionValidator2.Enabled = true;
            RegularExpressionValidator3.Enabled = true;
            RegularExpressionValidator4.Enabled = true;
            RegularExpressionValidator5.Enabled = true;
        }
        else
        {
            txtAnswerQuestion3_1.Enabled = false;
            txtAnswerQuestion3_2.Enabled = false;
            txtAnswerQuestion3_3.Enabled = false;
            txtAnswerQuestion3_4.Enabled = false;
            txtAnswerQuestion3_5.Enabled = false;

            RequiredFieldValidator1.Enabled = false;
            RequiredFieldValidator2.Enabled = false;
            RequiredFieldValidator3.Enabled = false;
            RequiredFieldValidator4.Enabled = false;
            RequiredFieldValidator5.Enabled = false;

            RegularExpressionValidator1.Enabled = false;
            RegularExpressionValidator2.Enabled = false;
            RegularExpressionValidator3.Enabled = false;
            RegularExpressionValidator4.Enabled = false;
            RegularExpressionValidator5.Enabled = false;

        }
    }

    protected void rad2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radQ2.SelectedIndex == 0)
        {
            txtAnswerQuestion3_6.Enabled = true;
            txtAnswerQuestion3_7.Enabled = true;
            txtAnswerQuestion3_8.Enabled = true;
        }
        else
        {
            txtAnswerQuestion3_6.Enabled = false;
            txtAnswerQuestion3_7.Enabled = false;
            txtAnswerQuestion3_8.Enabled = false;

            RequiredFieldValidator7.Enabled = false;
            RequiredFieldValidator8.Enabled = false;
            RequiredFieldValidator9.Enabled = false;

            RegularExpressionValidator6.Enabled = false;
            RegularExpressionValidator7.Enabled = false;
            RegularExpressionValidator8.Enabled = false;
        }
    }

    protected void btnToQB_Click(object sender, EventArgs e)
    {
        Response.Redirect("project_pickquestion.aspx");
    }
}