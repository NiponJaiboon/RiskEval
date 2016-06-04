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

public partial class question_set_E : System.Web.UI.Page
{
    private List<SqlParameter> insertParameters = new List<SqlParameter>();



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ManageCookie mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();
            string max = "";
            if (ck != null)
            {
                answer ans = new answer();
                max = ans.getLatestAnswerQ2(ck.pj_id, ans.getLatestAnswerQSetID(ck.pj_id));

                string strSQL1 = string.Format(@"select * from question1 q1, question2 q2, question3 q3 
                                where q1.qset_id = 5 and q1.q1_id = q2.q1_id and q2.q2_id = q3.q2_id;");

                SqlDataSource1.SelectCommand = strSQL1;
                SqlDataSource1.DataBind();

                DataView dv1 = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

                //ประเด็นที่หนึ่ง
                lblQuestion1.Text = "ประเด็นที่ " + dv1.Table.Rows[0]["q1_order"].ToString() + " " + dv1.Table.Rows[0]["q1_text"].ToString();
                lblAssumption.Text = "สมมุติฐาน " + dv1.Table.Rows[0]["q1_order"].ToString() + " " + dv1.Table.Rows[0]["q1_assumption"].ToString();

                lblQuestion2_1.Text = "คำถามที่ " + dv1.Table.Rows[0]["q2_order"].ToString() + " " + dv1.Table.Rows[0]["q2_text"].ToString();

                lblQuestion3_1.Text = dv1.Table.Rows[0]["q3_order"].ToString() + " " + dv1.Table.Rows[0]["q3_text"].ToString();
                lblQuestion3_2.Text = dv1.Table.Rows[1]["q3_order"].ToString() + " " + dv1.Table.Rows[1]["q3_text"].ToString();


                mgCookie.UpdateCookies("qset_id", "5"); //คำถามชุด ค.

                #region ต้น
                string strQ2ID = max;
                //ton.JavaScript.MessageBox(strQ2ID);
                if (strQ2ID != null)
                {
                    if (strQ2ID == "15")
                    {
                        panel1.Visible = true;
                        litfinish.Visible = false;
                        btnToQB.Visible = false;
                    }
                    else if (strQ2ID == "16")
                    {
                        panel1.Visible = false;
                        litfinish.Text = "เสร็จสิ้นการประเมินชุด จ: ขั้นตอนการริเริ่มแผนงาน/โครงการและวิเคราะห์เบื้องต้น โปรดดำเนินการต่อ";
                        litfinish.Visible = true;
                        btnToQB.Visible = true;
                    }
                    else
                    {
                        panel1.Visible = false;
                        litfinish.Visible = false;
                        btnToQB.Visible = false;
                    }
                }
                #endregion

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
        //บันทักข้อมูล ประเด็นที่ 6
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

        }
        else
        {
            // คำถามย่อยข้อ 1.1
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "1", ret.ToString());

            // คำถามย่อยข้อ 1.2
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "2", ret.ToString());

        }

        if (ret2 > 0)
        {
            //บันทึกสำเร็จ
            litfinish.Text = "เสร็จสิ้นการประเมินชุด จ: ขั้นตอนการริเริ่มแผนงาน/โครงการและวิเคราะห์เบื้องต้น โปรดดำเนินการต่อ";
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
        //บันทักข้อมูล
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        answer ans = new answer();

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
        Response.Redirect("project_pickquestion.aspx");
    }
}