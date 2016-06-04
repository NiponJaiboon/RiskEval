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

public partial class question_set_D_edit : System.Web.UI.Page
{
    private List<SqlParameter> insertParameters = new List<SqlParameter>();

    protected void Page_Load(object sender, EventArgs e)
    {
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if (ck != null)
        {

            enableValidation();

            RequiredFieldValidator1.Enabled = false;
            RegularExpressionValidator1.Enabled = false;

            string strSQL1 = "select * from question1 q1, question2 q2, question3 q3 where q1.qset_id = 4 and q1.q1_id = q2.q1_id and q2.q2_id = q3.q2_id and q1.q1_id = 7";

            SqlDataSource1.SelectCommand = strSQL1;
            SqlDataSource1.DataBind();

            DataView dv1 = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

            //ประเด็นที่ 7
            lblQuestion7.Text = "ประเด็นที่ " + dv1.Table.Rows[0]["q1_order"].ToString() + " " + dv1.Table.Rows[0]["q1_text"].ToString();
            lblAssumption7.Text = "สมมุติฐาน " + dv1.Table.Rows[0]["q1_order"].ToString() + " " + dv1.Table.Rows[0]["q1_assumption"].ToString();

            lblQuestion7_1.Text = "คำถามที่ " + dv1.Table.Rows[0]["q2_order"].ToString() + " " + dv1.Table.Rows[0]["q2_text"].ToString();
            lblQuestion7_2.Text = "คำถามที่ " + dv1.Table.Rows[1]["q2_order"].ToString() + " " + dv1.Table.Rows[1]["q2_text"].ToString();
            lblQuestion7_3.Text = "คำถามที่ " + dv1.Table.Rows[2]["q2_order"].ToString() + " " + dv1.Table.Rows[2]["q2_text"].ToString();

            lblQuestion7_1_1.Text = dv1.Table.Rows[0]["q3_order"].ToString() + " " + dv1.Table.Rows[0]["q3_text"].ToString();
            lblQuestion7_2_1.Text = dv1.Table.Rows[1]["q3_order"].ToString() + " " + dv1.Table.Rows[1]["q3_text"].ToString();
            lblQuestion7_3_1.Text = dv1.Table.Rows[2]["q3_order"].ToString() + " " + dv1.Table.Rows[2]["q3_text"].ToString();

            //ประเด็นที่ 8
            string strSQL2 = "select * from question1 q1, question2 q2, question3 q3 where q1.qset_id = 4 and q1.q1_id = q2.q1_id and q2.q2_id = q3.q2_id and q1.q1_id = 8";
            SqlDataSource2.SelectCommand = strSQL2;
            SqlDataSource2.DataBind();
            dv1 = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);

            lblQuestion8.Text = "ประเด็นที่ " + dv1.Table.Rows[0]["q1_order"].ToString() + " " + dv1.Table.Rows[0]["q1_text"].ToString();
            lblAssumption8.Text = "สมมุติฐาน " + dv1.Table.Rows[0]["q1_order"].ToString() + " " + dv1.Table.Rows[0]["q1_assumption"].ToString();

            lblQuestion8_4.Text = "คำถามที่ " + dv1.Table.Rows[0]["q2_order"].ToString() + " " + dv1.Table.Rows[0]["q2_text"].ToString();
            lblQuestion8_5.Text = "คำถามที่ " + dv1.Table.Rows[1]["q2_order"].ToString() + " " + dv1.Table.Rows[1]["q2_text"].ToString();

            lblQuestion8_4_1.Text = dv1.Table.Rows[0]["q3_order"].ToString() + " " + dv1.Table.Rows[0]["q3_text"].ToString();
            lblQuestion8_5_1.Text = dv1.Table.Rows[1]["q3_order"].ToString() + " " + dv1.Table.Rows[1]["q3_text"].ToString();

            //ประเด็นที่ 9
            string strSQL3 = "select * from question1 q1, question2 q2, question3 q3 where q1.qset_id = 4 and q1.q1_id = q2.q1_id and q2.q2_id = q3.q2_id and q1.q1_id = 9";
            SqlDataSource2.SelectCommand = strSQL3;
            SqlDataSource2.DataBind();
            dv1 = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);

            lblQuestion9.Text = "ประเด็นที่ " + dv1.Table.Rows[0]["q1_order"].ToString() + " " + dv1.Table.Rows[0]["q1_text"].ToString();
            lblAssumption9.Text = "สมมุติฐาน " + dv1.Table.Rows[0]["q1_order"].ToString() + " " + dv1.Table.Rows[0]["q1_assumption"].ToString();

            lblQuestion9_6.Text = "คำถามที่ " + dv1.Table.Rows[0]["q2_order"].ToString() + " " + dv1.Table.Rows[0]["q2_text"].ToString();
            //lblQuestion9_7.Text = "คำถามที่ " + dv1.Table.Rows[1]["q2_order"].ToString() + " " + dv1.Table.Rows[1]["q2_text"].ToString();

            lblQuestion9_6_1.Text = dv1.Table.Rows[0]["q3_order"].ToString() + " " + dv1.Table.Rows[0]["q3_text"].ToString();
            lblQuestion9_6_2.Text = dv1.Table.Rows[1]["q3_order"].ToString() + " " + dv1.Table.Rows[1]["q3_text"].ToString();


            if (!Page.IsPostBack)
            {

                //populate current values to controls
                strSQL1 = "select answer_q2_text from dbo.answer_q2 where qset_id = 4 and pj_id = " + ck.pj_id + " order by q2_id asc";

                SqlDataSource1.SelectCommand = strSQL1;
                SqlDataSource1.DataBind();
                dv1 = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

                radanswer7_1.SelectedValue = dv1.Table.Rows[0]["answer_q2_text"].ToString();
                radanswer7_2.SelectedValue = dv1.Table.Rows[1]["answer_q2_text"].ToString();
                radanswer7_3.SelectedValue = dv1.Table.Rows[2]["answer_q2_text"].ToString();
                radanswer8_4.SelectedValue = dv1.Table.Rows[3]["answer_q2_text"].ToString();
                radanswer8_5.SelectedValue = dv1.Table.Rows[4]["answer_q2_text"].ToString();
                radanswer9_6.SelectedValue = dv1.Table.Rows[5]["answer_q2_text"].ToString();

                strSQL1 = "select answer_q2_text from dbo.answer_q2 where qset_id = 4 and pj_id = " + ck.pj_id + " order by q2_id asc";

                strSQL1 = "select answer_q3_text " +
                    "from dbo.answer_q3 q3 " +
                    "left join dbo.answer_q2 q2 on q3.answer_q2_id = q2.answer_q2_id " +
                    "where q2.qset_id = 4 and q2.pj_id = " + ck.pj_id + " order by q3.q3_id asc";

                SqlDataSource1.SelectCommand = strSQL1;
                SqlDataSource1.DataBind();

                dv1 = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                txtAnswerQuestion7_1_1.Text = dv1.Table.Rows[0]["answer_q3_text"].ToString();
                txtAnswerQuestion7_2_1.Text = dv1.Table.Rows[1]["answer_q3_text"].ToString();
                txtAnswerQuestion7_3_1.Text = dv1.Table.Rows[2]["answer_q3_text"].ToString();
                txtAnswerQuestion8_4_1.Text = dv1.Table.Rows[3]["answer_q3_text"].ToString();
                txtAnswerQuestion8_5_1.Text = dv1.Table.Rows[4]["answer_q3_text"].ToString();
                txtAnswerQuestion9_6_1.Text = dv1.Table.Rows[5]["answer_q3_text"].ToString();
                txtAnswerQuestion9_6_2.Text = dv1.Table.Rows[6]["answer_q3_text"].ToString();

                string strPendingQ1 = string.Empty;
                string strPendingQ2 = string.Empty;

                if (Request["q1"] != null && Request["q2"] != null)
                {
                    strPendingQ1 = Request["q1"].ToString();
                    strPendingQ2 = Request["q2"].ToString();

                    switch (strPendingQ1)
                    {
                        case "1":
                            panel1.Visible = true;
                            panel2.Visible = false;
                            panel3.Visible = false;
                            break;
                        case "2":
                            panel1.Visible = false;
                            panel2.Visible = true;
                            panel3.Visible = false;
                            break;
                        case "3":
                            panel1.Visible = false;
                            panel2.Visible = false;
                            panel3.Visible = true;
                            break;
                        default:
                            panel1.Visible = true;
                            panel2.Visible = false;
                            panel3.Visible = false;
                            break;
                    }

                    switch (strPendingQ2)
                    {
                        case "1":
                            pnl7_1.Visible = true;
                            pnl7_2.Visible = false;
                            pnl7_3.Visible = false;
                            pnl8_4.Visible = false;
                            pnl8_5.Visible = false;
                            pnl9_6.Visible = false;
                            break;
                        case "2":
                            pnl7_1.Visible = false;
                            pnl7_2.Visible = true;
                            pnl7_3.Visible = false;
                            pnl8_4.Visible = false;
                            pnl8_5.Visible = false;
                            pnl9_6.Visible = false;
                            break;
                        case "3":
                            pnl7_1.Visible = false;
                            pnl7_2.Visible = false;
                            pnl7_3.Visible = true;
                            pnl8_4.Visible = false;
                            pnl8_5.Visible = false;
                            pnl9_6.Visible = false;
                            break;
                        case "4":
                            pnl7_1.Visible = false;
                            pnl7_2.Visible = false;
                            pnl7_3.Visible = false;
                            pnl8_4.Visible = true;
                            pnl8_5.Visible = false;
                            pnl9_6.Visible = false;
                            break;
                        case "5":
                            pnl7_1.Visible = false;
                            pnl7_2.Visible = false;
                            pnl7_3.Visible = false;
                            pnl8_4.Visible = false;
                            pnl8_5.Visible = true;
                            pnl9_6.Visible = false;
                            break;
                        case "6":
                            pnl7_1.Visible = false;
                            pnl7_2.Visible = false;
                            pnl7_3.Visible = false;
                            pnl8_4.Visible = false;
                            pnl8_5.Visible = false;
                            pnl9_6.Visible = true;
                            break;
                        default:
                            pnl7_1.Visible = true;
                            pnl7_2.Visible = false;
                            pnl7_3.Visible = false;
                            pnl8_4.Visible = false;
                            pnl8_5.Visible = false;
                            pnl9_6.Visible = false;
                            break;
                    }

                }
                else
                {
                    //answer ans = new answer();
                    //string strQ2ID = ans.getLatestAnswerQ2(ck.pj_id, "4");

                    string strQ2ID = ck.q2_id;

                    if (strQ2ID != "0")
                    {
                        switch (strQ2ID)
                        {
                            case "10":
                                panel1.Visible = true;
                                panel2.Visible = false;
                                panel3.Visible = false;

                                pnl7_1.Visible = true;
                                pnl7_2.Visible = false;
                                pnl7_3.Visible = false;
                                pnl8_4.Visible = false;
                                pnl8_5.Visible = false;
                                pnl9_6.Visible = false;
                                break;

                            case "11":
                                panel1.Visible = true;
                                panel2.Visible = false;
                                panel3.Visible = false;

                                pnl7_1.Visible = false;
                                pnl7_2.Visible = true;
                                pnl7_3.Visible = false;
                                pnl8_4.Visible = false;
                                pnl8_5.Visible = false;
                                pnl9_6.Visible = false;
                                break;

                            case "12":

                                panel1.Visible = true;
                                panel2.Visible = false;
                                panel3.Visible = false;

                                pnl7_1.Visible = false;
                                pnl7_2.Visible = false;
                                pnl7_3.Visible = true;
                                pnl8_4.Visible = false;
                                pnl8_5.Visible = false;
                                pnl9_6.Visible = false;
                                break;

                            case "13":
                                panel1.Visible = false;
                                panel2.Visible = true;
                                panel3.Visible = false;

                                pnl7_1.Visible = false;
                                pnl7_2.Visible = false;
                                pnl7_3.Visible = false;
                                pnl8_4.Visible = true;
                                pnl8_5.Visible = false;
                                pnl9_6.Visible = false;
                                break;

                            case "14":
                                panel1.Visible = false;
                                panel2.Visible = true;
                                panel3.Visible = false;

                                pnl7_1.Visible = false;
                                pnl7_2.Visible = false;
                                pnl7_3.Visible = false;
                                pnl8_4.Visible = false;
                                pnl8_5.Visible = true;
                                pnl9_6.Visible = false;
                                break;

                            case "15":
                                panel1.Visible = false;
                                panel2.Visible = false;
                                panel3.Visible = true;

                                pnl7_1.Visible = false;
                                pnl7_2.Visible = false;
                                pnl7_3.Visible = false;
                                pnl8_4.Visible = false;
                                pnl8_5.Visible = false;
                                pnl9_6.Visible = true;
                                break;
                            default:
                                panel1.Visible = true;
                                panel2.Visible = false;
                                panel3.Visible = false;

                                pnl7_1.Visible = true;
                                pnl7_2.Visible = false;
                                pnl7_3.Visible = false;
                                pnl8_4.Visible = false;
                                pnl8_5.Visible = false;
                                pnl9_6.Visible = false;
                                break;
                        }
                    }


                }

            }

        }

    }

    protected void btnToQ7_2_Click(object sender, EventArgs e)
    {
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        answer ans = new answer();

        int ret = ans.insertOrUpdateAnswerQ2(radanswer7_1.SelectedValue, ck.pj_id, "10", "4");

        mgCookie.UpdateCookies("answer_q2_id", ret.ToString());

        int ret2 = 0;

        if (radanswer7_1.SelectedIndex == 0) // ตอบว่า ใช่
        {

            // คำถามย่อยข้อ 7.1
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion7_1_1.Text, ck.pj_id, "24", ret.ToString());
        }
        else if (radanswer7_1.SelectedIndex == 1)
        {

            // คำถามย่อยข้อ 7.1
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "24", ret.ToString());
        }


        if (ret2 > 0)
        {
            //บันทึกสำเร็จ
            pnl7_1.Visible = false;
            pnl7_2.Visible = true;
            pnl7_3.Visible = false;
        }
        else
        {
            //บันทึกไม่สำเร็จ

        }

    }

    protected void btnToQ7_3_Click(object sender, EventArgs e)
    {
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        answer ans = new answer();

        int ret = ans.insertOrUpdateAnswerQ2(radanswer7_1.SelectedValue, ck.pj_id, "11", "4");

        mgCookie.UpdateCookies("answer_q2_id", ret.ToString());

        int ret2 = 0;

        if (radanswer7_2.SelectedIndex == 0) // ตอบว่า ใช่
        {
            // คำถามย่อยข้อ 7.2
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion7_2_1.Text, ck.pj_id, "25", ret.ToString());
        }
        else if (radanswer7_2.SelectedIndex == 1)
        {
            // คำถามย่อยข้อ 7.2
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "25", ret.ToString());
        }


        if (ret2 > 0)
        {
            //บันทึกสำเร็จ
            pnl7_1.Visible = false;
            pnl7_2.Visible = false;
            pnl7_3.Visible = true;

        }
        else
        {
            //บันทึกไม่สำเร็จ

        }

    }

    protected void btnToQ8_4_Click(object sender, EventArgs e)
    {
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        answer ans = new answer();

        int ret = ans.insertOrUpdateAnswerQ2(radanswer7_1.SelectedValue, ck.pj_id, "12", "4");

        mgCookie.UpdateCookies("answer_q2_id", ret.ToString());

        int ret2 = 0;

        if (radanswer7_3.SelectedIndex == 0) // ตอบว่า ใช่
        {
            // คำถามย่อยข้อ 7.3
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion7_3_1.Text, ck.pj_id, "26", ret.ToString());
        }
        else if (radanswer7_3.SelectedIndex == 1)
        {
            // คำถามย่อยข้อ 7.3
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "26", ret.ToString());
        }


        if (ret2 > 0)
        {
            //บันทึกสำเร็จ
            pnl7_1.Visible = false;
            pnl7_2.Visible = false;
            pnl7_3.Visible = false;

            Response.Redirect("question_set_D_edit.aspx?q1=2&q2=4");

        }
        else
        {
            //บันทึกไม่สำเร็จ

        }
    }

    protected void btnToQ8_5_Click(object sender, EventArgs e)
    {

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        answer ans = new answer();

        int ret = ans.insertOrUpdateAnswerQ2(radanswer8_4.SelectedValue, ck.pj_id, "13", "4");

        mgCookie.UpdateCookies("answer_q2_id", ret.ToString());

        int ret2 = 0;

        if (radanswer8_4.SelectedIndex == 0) // ตอบว่า ใช่
        {
            // คำถามย่อยข้อ 8.4
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion8_4_1.Text, ck.pj_id, "27", ret.ToString());
        }
        else if (radanswer8_4.SelectedIndex == 1)
        {
            // คำถามย่อยข้อ 8.4
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "27", ret.ToString());
        }

        if (ret2 > 0)
        {
            //บันทึกสำเร็จ

            Response.Redirect("question_set_D_edit.aspx?q1=2&q2=5");

        }
        else
        {
            //บันทึกไม่สำเร็จ


        }
    }

    protected void btnToQ9_6_Click(object sender, EventArgs e)
    {
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        answer ans = new answer();

        int ret = ans.insertOrUpdateAnswerQ2(radanswer8_5.SelectedValue, ck.pj_id, "14", "4");

        mgCookie.UpdateCookies("answer_q2_id", ret.ToString());

        int ret2 = 0;

        if (radanswer8_5.SelectedIndex == 0) // ตอบว่า ใช่
        {
            // คำถามย่อยข้อ 8.5
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion8_5_1.Text, ck.pj_id, "28", ret.ToString());
        }
        else if (radanswer8_5.SelectedIndex == 1)
        {
            // คำถามย่อยข้อ 8.5
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "28", ret.ToString());
        }

        if (ret2 > 0)
        {
            //บันทึกสำเร็จ
            Response.Redirect("question_set_D_edit.aspx?q1=3&q2=6");
        }
        else
        {
            //บันทึกไม่สำเร็จ
        }
    }

    protected void btnToSetE_Click(object sender, EventArgs e)
    {
        //บันทักข้อมูล แล้วแสดงข้อคำถามชุดต่อไปกรณีโครงการต่อเนื่อง หรือ เข้าประเมินปัจจัยถายใน ภายนอก กรณีโครงการใหม่
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        answer ans = new answer();

        int ret = ans.insertOrUpdateAnswerQ2(radanswer9_6.SelectedValue, ck.pj_id, "15", "4");

        mgCookie.UpdateCookies("answer_q2_id", ret.ToString());

        int ret2 = 0;

        if (radanswer9_6.SelectedIndex == 0) // ตอบว่า ใช่
        {
            // คำถามย่อยข้อ 9.6
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion9_6_1.Text, ck.pj_id, "29", ret.ToString());
        }
        else if (radanswer9_6.SelectedIndex == 1)
        {
            // คำถามย่อยข้อ 9.6
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "29", ret.ToString());
        }

        ret = ans.insertOrUpdateAnswerQ2(radanswer9_6.SelectedValue, ck.pj_id, "15", "4");

        mgCookie.UpdateCookies("answer_q2_id", ret.ToString());

        if (radanswer9_6.SelectedIndex == 0) // ตอบว่า ใช่
        {
            // คำถามย่อยข้อ 6.2
            ret2 = ans.insertOrUpdateAnswerQ3(txtAnswerQuestion9_6_2.Text, ck.pj_id, "30", ret.ToString());
        }
        else if (radanswer9_6.SelectedIndex == 1)
        {
            // คำถามย่อยข้อ 6.2
            ret2 = ans.insertOrUpdateAnswerQ3(string.Empty, ck.pj_id, "30", ret.ToString());
        }

        if (ret2 > 0)
        {
            //บันทึกสำเร็จ
            //บันทักข้อมูล แล้วแสดงข้อคำถามชุดต่อไปกรณีโครงการต่อเนื่อง หรือ เข้าประเมินปัจจัยถายใน ภายนอก กรณีโครงการใหม่
            projects pj = new projects();
            string pjType = pj.getProjectInfo(ck.pj_id, "pj_type");

            if (pjType == "โครงการใหม่")
            {
                //Response.Redirect("factor_risk.aspx");
                Response.Redirect("project_edit.aspx");
            }
            else
            {
                //Response.Redirect("question_set_E.aspx");
                Response.Redirect("project_edit.aspx");
            }
        }
        else
        {
            //บันทึกไม่สำเร็จ

        }
    }

    protected void radanswer7_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radanswer7_1.SelectedIndex == 0)
        {
            txtAnswerQuestion7_1_1.Enabled = true;

            RequiredFieldValidator1.Enabled = true;
            RegularExpressionValidator1.Enabled = true;

        }
        else
        {
            txtAnswerQuestion7_1_1.Enabled = false;

            RequiredFieldValidator1.Enabled = false;
            RegularExpressionValidator1.Enabled = false;
        }
    }

    protected void radanswer7_2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radanswer7_2.SelectedIndex == 0)
        {
            txtAnswerQuestion7_2_1.Enabled = true;

            RequiredFieldValidator3.Enabled = true;
            RegularExpressionValidator2.Enabled = true;
        }
        else
        {
            txtAnswerQuestion7_2_1.Enabled = false;

            RequiredFieldValidator3.Enabled = false;
            RegularExpressionValidator2.Enabled = false;
        }
    }

    protected void radanswer7_3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radanswer7_3.SelectedIndex == 0)
        {
            txtAnswerQuestion7_3_1.Enabled = true;

            RequiredFieldValidator5.Enabled = true;
            RegularExpressionValidator3.Enabled = true;
        }
        else
        {
            txtAnswerQuestion7_3_1.Enabled = false;

            RequiredFieldValidator5.Enabled = false;
            RegularExpressionValidator3.Enabled = false;
        }
    }

    protected void radanswer8_4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radanswer8_4.SelectedIndex == 0)
        {
            txtAnswerQuestion8_4_1.Enabled = true;

            RequiredFieldValidator7.Enabled = true;
            RegularExpressionValidator4.Enabled = true;
        }
        else
        {
            txtAnswerQuestion8_4_1.Enabled = false;

            RequiredFieldValidator7.Enabled = false;
            RegularExpressionValidator4.Enabled = false;
        }
    }

    protected void radanswer8_5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radanswer8_5.SelectedIndex == 0)
        {
            txtAnswerQuestion8_5_1.Enabled = true;

            RequiredFieldValidator9.Enabled = true;
            RegularExpressionValidator5.Enabled = true;
        }
        else
        {
            txtAnswerQuestion8_5_1.Enabled = false;

            RequiredFieldValidator9.Enabled = false;
            RegularExpressionValidator5.Enabled = false;
        }
    }

    protected void radanswer9_6_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radanswer9_6.SelectedIndex == 0)
        {
            txtAnswerQuestion9_6_1.Enabled = true;

            RequiredFieldValidator11.Enabled = true;
            RegularExpressionValidator6.Enabled = true;
            RequiredFieldValidator14.Enabled = true;
            RegularExpressionValidator8.Enabled = true;
        }
        else
        {
            txtAnswerQuestion9_6_1.Enabled = false;
            RequiredFieldValidator11.Enabled = false;
            RegularExpressionValidator6.Enabled = false;
            RequiredFieldValidator14.Enabled = false;
            RegularExpressionValidator8.Enabled = false;
        }
    }

    protected void enableValidation()
    {
        if (radanswer7_1.SelectedIndex == 0)
        {

            RequiredFieldValidator1.Enabled = true;
            RegularExpressionValidator1.Enabled = true;
        }
        else
        {

            RequiredFieldValidator1.Enabled = false;
            RegularExpressionValidator1.Enabled = false;
        }

        if (radanswer7_2.SelectedIndex == 0)
        {

            RequiredFieldValidator3.Enabled = true;
            RegularExpressionValidator2.Enabled = true;
        }
        else
        {


            RequiredFieldValidator3.Enabled = false;
            RegularExpressionValidator2.Enabled = false;
        }

        if (radanswer7_3.SelectedIndex == 0)
        {


            RequiredFieldValidator5.Enabled = true;
            RegularExpressionValidator3.Enabled = true;
        }
        else
        {

            RequiredFieldValidator5.Enabled = false;
            RegularExpressionValidator3.Enabled = false;
        }

        if (radanswer8_4.SelectedIndex == 0)
        {

            RequiredFieldValidator7.Enabled = true;
            RegularExpressionValidator4.Enabled = true;
        }
        else
        {

            RequiredFieldValidator7.Enabled = false;
            RegularExpressionValidator4.Enabled = false;
        }

        if (radanswer8_5.SelectedIndex == 0)
        {

            RequiredFieldValidator9.Enabled = true;
            RegularExpressionValidator5.Enabled = true;
        }
        else
        {

            RequiredFieldValidator9.Enabled = false;
            RegularExpressionValidator5.Enabled = false;
        }

        if (radanswer9_6.SelectedIndex == 0)
        {

            RequiredFieldValidator11.Enabled = true;
            RegularExpressionValidator6.Enabled = true;
            RequiredFieldValidator14.Enabled = true;
            RegularExpressionValidator8.Enabled = true;
        }
        else
        {
            RequiredFieldValidator11.Enabled = false;
            RegularExpressionValidator6.Enabled = false;
            RequiredFieldValidator14.Enabled = false;
            RegularExpressionValidator8.Enabled = false;
        }

    }

}