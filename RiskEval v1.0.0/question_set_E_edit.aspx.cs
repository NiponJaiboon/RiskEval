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


public partial class question_set_E_edit : System.Web.UI.Page
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
                string strSQL1 = "select * from question1 q1, question2 q2, question3 q3 where q1.qset_id = 5 and q1.q1_id = q2.q1_id and q2.q2_id = q3.q2_id";
                SqlDataSource1.SelectCommand = strSQL1;
                SqlDataSource1.DataBind();
                DataView dv1 = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                lblQuestion1.Text = "ประเด็นที่ " + dv1.Table.Rows[0]["q1_order"].ToString() + " " + dv1.Table.Rows[0]["q1_text"].ToString();
                lblAssumption.Text = "สมมุติฐาน " + dv1.Table.Rows[0]["q1_order"].ToString() + " " + dv1.Table.Rows[0]["q1_assumption"].ToString();
                lblQuestion2_1.Text = "คำถามที่ " + dv1.Table.Rows[0]["q2_order"].ToString() + " " + dv1.Table.Rows[0]["q2_text"].ToString();
                lblQuestion3_1.Text = dv1.Table.Rows[0]["q3_order"].ToString() + " " + dv1.Table.Rows[0]["q3_text"].ToString();
                lblQuestion3_2.Text = dv1.Table.Rows[1]["q3_order"].ToString() + " " + dv1.Table.Rows[1]["q3_text"].ToString();

                //ประเด็นที่หนึ่ง คำตอบ
                strSQL1 = "select * from dbo.answer_q2 q2, answer_q3 q3 "
                    + " where q2.qset_id = 5 and "
                    + "q2.pj_id = " + ck.pj_id + " and "
                    + "q2.q2_id = 16 and "
                    + "q2.answer_q2_id = q3.answer_q2_id";
                SqlDataSource1.SelectCommand = strSQL1;
                SqlDataSource1.DataBind();
                dv1 = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

                //radanswer2_1.SelectedValue = dv1.Table.Rows[0]["answer_q2_text"].ToString();
                if (dv1.Table.Rows[0]["answer_q2_text"].ToString() == "มี" ||
                dv1.Table.Rows[0]["answer_q2_text"].ToString() == "ใช่")
                {
                    radanswer2_1.SelectedIndex = 0;
                    txtAnswerQuestion3_1.Enabled = true;
                    txtAnswerQuestion3_2.Enabled = true;

                    RequiredFieldValidator1.Enabled = true;
                    RequiredFieldValidator2.Enabled = true;
                }
                else
                {
                    radanswer2_1.SelectedIndex = 1;
                    txtAnswerQuestion3_1.Enabled = false;
                    txtAnswerQuestion3_2.Enabled = false;

                    RequiredFieldValidator1.Enabled = false;
                    RequiredFieldValidator2.Enabled = false;
                }


                txtAnswerQuestion3_1.Text = dv1.Table.Rows[0]["answer_q3_text"].ToString();
                txtAnswerQuestion3_2.Text = dv1.Table.Rows[1]["answer_q3_text"].ToString();


                mgCookie.UpdateCookies("qset_id", "5"); //คำถามชุด ง.

                if (panel1.Visible == true)
                {
                    mgCookie.UpdateCookies("q1_id", "5"); //ประเด็นที่หนึ่ง
                    mgCookie.UpdateCookies("q2_id", dv1.Table.Rows[0]["q2_id"].ToString());
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


        }
        else
        {
            // คำถามย่อยข้อ 1.1
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "1", ret.ToString());

            // คำถามย่อยข้อ 1.2
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "2", ret.ToString());


        }

        mgCookie.UpdateCookies("q1_id", "2");  //ประเด็นที่สอง
        mgCookie.UpdateCookies("q2_id", "2");

        if (ret2 > 0)
        {
            //บันทึกสำเร็จ
            litfinish.Text = "เสร็จสิ้นการแก้ไขประเมินชุด จ: ขั้นตอนการริเริ่มแผนงาน/โครงการและวิเคราะห์เบื้องต้น โปรดดำเนินการต่อ";
            litfinish.Visible = true;
            btnToQB.Visible = true;
            panel1.Visible = false;

        }
        else
        {
            //บันทึกไม่สำเร็จ

        }

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {

    }

    protected void answer2_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radanswer2_1.SelectedIndex == 0)
        {
            txtAnswerQuestion3_1.Enabled = true;
            txtAnswerQuestion3_2.Enabled = true;

            RequiredFieldValidator1.Enabled = true;
            RequiredFieldValidator2.Enabled = true;
        }
        else
        {

            txtAnswerQuestion3_1.Enabled = false;
            txtAnswerQuestion3_2.Enabled = false;

            RequiredFieldValidator1.Enabled = false;
            RequiredFieldValidator2.Enabled = false;

        }
    }
     

    protected void btnToQB_Click(object sender, EventArgs e)
    {
        Response.Redirect("project_edit.aspx");
    }
}