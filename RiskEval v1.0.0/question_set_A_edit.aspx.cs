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

public partial class question_set_A_edit : System.Web.UI.Page
{

    private List<SqlParameter> insertParameters = new List<SqlParameter>();



    protected void Page_Load(object sender, EventArgs e)
    {
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if (!Page.IsPostBack)
        {

            if (ck != null)
            {

                //ประเด็นที่หนึ่ง คำถาม
                string strSQL1 = "select * from question1 q1, question2 q2, question3 q3 where q1.qset_id = 1 and q1.q1_id = q2.q1_id and q2.q2_id = q3.q2_id and q1.q1_id = 1";
                SqlDataSource1.SelectCommand = strSQL1;
                SqlDataSource1.DataBind();
                DataView dv1 = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                lblQuestion1.Text = "ประเด็นที่ " + dv1.Table.Rows[0]["q1_order"].ToString() + " " + dv1.Table.Rows[0]["q1_text"].ToString();
                lblAssumption.Text = "สมมุติฐาน " + dv1.Table.Rows[0]["q1_order"].ToString() + " " + dv1.Table.Rows[0]["q1_assumption"].ToString();
                lblQuestion2_1.Text = "คำถามที่ " + dv1.Table.Rows[0]["q2_order"].ToString() + " " + dv1.Table.Rows[0]["q2_text"].ToString();
                lblQuestion3_1.Text = dv1.Table.Rows[0]["q3_order"].ToString() + " " + dv1.Table.Rows[0]["q3_text"].ToString();
                lblQuestion3_2.Text = dv1.Table.Rows[1]["q3_order"].ToString() + " " + dv1.Table.Rows[1]["q3_text"].ToString();
                lblQuestion3_3.Text = dv1.Table.Rows[2]["q3_order"].ToString() + " " + dv1.Table.Rows[2]["q3_text"].ToString();
                lblQuestion3_4.Text = dv1.Table.Rows[3]["q3_order"].ToString() + " " + dv1.Table.Rows[3]["q3_text"].ToString();
                lblQuestion3_5.Text = dv1.Table.Rows[4]["q3_order"].ToString() + " " + dv1.Table.Rows[4]["q3_text"].ToString();

                //ประเด็นที่หนึ่ง คำตอบ
                strSQL1 = "select * from dbo.answer_q2 q2, answer_q3 q3 "
                    + " where q2.qset_id = 1 and "
                    + "q2.pj_id = " + ck.pj_id + " and "
                    + "q2.q2_id = 1 and "
                    + "q2.answer_q2_id = q3.answer_q2_id";
                SqlDataSource1.SelectCommand = strSQL1;
                SqlDataSource1.DataBind();
                dv1 = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

                if (dv1.Table.Rows[0]["answer_q2_text"].ToString() == "มี" ||
                    dv1.Table.Rows[0]["answer_q2_text"].ToString() == "ใช่")
                {
                    radanswer2_1.SelectedIndex = 0;
                }
                else
                {
                    radanswer2_1.SelectedIndex = 1;
                }

                txtAnswerQuestion3_1.Text = dv1.Table.Rows[0]["answer_q3_text"].ToString();
                txtAnswerQuestion3_2.Text = dv1.Table.Rows[1]["answer_q3_text"].ToString();
                txtAnswerQuestion3_3.Text = dv1.Table.Rows[2]["answer_q3_text"].ToString();
                txtAnswerQuestion3_4.Text = dv1.Table.Rows[3]["answer_q3_text"].ToString();
                txtAnswerQuestion3_5.Text = dv1.Table.Rows[4]["answer_q3_text"].ToString();

                //ประเด็นที่สอง คำถาม
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


                //ประเด็นที่สอง คำตอบ
                strSQL2 = "select * from dbo.answer_q2 q2, answer_q3 q3 "
                    + " where q2.qset_id = 1 and "
                    + "q2.pj_id = " + ck.pj_id + " and "
                    + "q2.q2_id = 2 and "
                    + "q2.answer_q2_id = q3.answer_q2_id";

                SqlDataSource2.SelectCommand = strSQL2;
                SqlDataSource2.DataBind();
                dv2 = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
                radQ2.SelectedValue = dv2.Table.Rows[0]["answer_q2_text"].ToString();
                txtAnswerQuestion3_6.Text = dv2.Table.Rows[0]["answer_q3_text"].ToString();
                txtAnswerQuestion3_7.Text = dv2.Table.Rows[1]["answer_q3_text"].ToString();
                txtAnswerQuestion3_8.Text = dv2.Table.Rows[2]["answer_q3_text"].ToString();

                mgCookie.UpdateCookies("qset_id", "1"); //คำถามชุด ก.

                if (panel1.Visible == true)
                {
                    mgCookie.UpdateCookies("q1_id", "1"); //ประเด็นที่หนึ่ง
                    mgCookie.UpdateCookies("q2_id", dv1.Table.Rows[0]["q2_id"].ToString());
                }
                else if (panel2.Visible == true)
                {
                    mgCookie.UpdateCookies("q1_id", "2");  //ประเด็นที่สอง
                    mgCookie.UpdateCookies("q2_id", dv2.Table.Rows[0]["q2_id"].ToString());
                }

            }

        }
       
       
    }


    protected void btnNextToQ2_2_Click(object sender, EventArgs e)
    {
        //บันทักข้อมูล แล้วแสดงประเด็นที่ 2
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        answer ans = new answer();

        int ret = ans.insertOrUpdateAnswerQ2(radanswer2_1.SelectedValue, ck.pj_id, ck.q2_id, ck.qset_id);
        
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
 
        mgCookie.UpdateCookies("q1_id", "2");  //ประเด็นที่สอง
        mgCookie.UpdateCookies("q2_id", "2");
         
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

        int ret = ans.insertOrUpdateAnswerQ2(radQ2.SelectedValue, ck.pj_id, ck.q2_id, ck.qset_id);

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

            //Response.Redirect("project_edit.aspx");

            litfinish.Text = "เสร็จสิ้นการแก้ไขประเมินชุด ก: ขั้นตอนการริเริ่มแผนงาน/โครงการและวิเคราะห์เบื้องต้น โปรดดำเนินการต่อ";
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

        }
        else
        {
    
            txtAnswerQuestion3_1.Enabled = false;
            txtAnswerQuestion3_2.Enabled = false;
            txtAnswerQuestion3_3.Enabled = false;
            txtAnswerQuestion3_4.Enabled = false;
            txtAnswerQuestion3_5.Enabled = false;

            RequiredFieldValidator1.Enabled = true;
            RequiredFieldValidator2.Enabled = true;
            RequiredFieldValidator3.Enabled = true;
            RequiredFieldValidator4.Enabled = true;
            RequiredFieldValidator5.Enabled = true;

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
    
        }
    }

    protected void btnToQB_Click(object sender, EventArgs e)
    {
        Response.Redirect("project_edit.aspx");
    }
}