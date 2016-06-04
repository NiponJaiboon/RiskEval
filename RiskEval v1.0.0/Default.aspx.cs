using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using riskEval;
using ton.config;

public partial class _Default : System.Web.UI.Page
{
    public string msg = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = new DataSet();
            string strsql = string.Format(@"
            select rs.role_id, rs.subrole_name as role_name from roles r 
            inner join roles_sub rs on r.role_id = rs.role_id order by subrole_id 
            select * from announce where announce_status = 1 order by created_date;
            ");
            ds = gUtilities.GetData(strsql, "UserStatus");
            if (ds != null)
            {
                this.ddlStatus.DataSource = ds.Tables[0];
                this.ddlStatus.DataValueField = "role_id";
                this.ddlStatus.DataTextField = "role_name";
                this.ddlStatus.DataBind();
                this.ddlStatus.Items.Insert(0, "ระบุสถานะผู้ใช้");

                if (ds.Tables[1].Rows.Count > 0)
                {

                    ManageCookie mgCookie = new ManageCookie();
                    users ck = mgCookie.ReadCookies();

                    if (ck != null)
                    {

                        if (ck.p_role_id == "3")
                        {

                            string strText = string.Format(@"<ul><li><a href='register.aspx'>ลงทะเบียน (ส่วนราชการ รัฐวิสาหกิจ หน่วยงานอื่นของรัฐ 
                            จังหวัด และกลุ่มจังหวัด)</a></li>
                            <li><a href='register_staff.aspx'>ลงทะเบียน (สำนักงบประมาณ)</a></li>
                            </ul>
                                <div class='help-manual'>
                                    <p class='title'>คู่มือการใช้งานระบบ</p>
                
                                    <ul>
                			<li><a href='document/1คู่มือการใช้โปรแกรมของส่วนราชการ.pdf'>คู่มือการใช้งานการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</a></li>
                			<li><a href='document/2คู่มือส่วนราชการ.pdf'>คู่มือการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล สำหรับคำของบประมาณ</a></li>
                			<li><a href='document/3แบบฟอร์มประกอบแนวทางการตอบแบบสอบถาม.docx'>แบบฟอร์มประกอบแนวทางการตอบแบบสอบถาม</li>
            			    </ul>
                                </div>");

                            ltannounce.Text += strText;

                        }

                    }
                    else
                    {

                        int iCount = 1;
                        foreach (DataRow r in ds.Tables[1].Rows)
                        {
                            ltannounce.Text += string.Format(@"<p>{0}. {1}</p>", iCount.ToString(), r["announce_title"].ToString());
                            ltannounce.Text += string.Format(@"<ul>{0}</ul>", r["announce_detail"].ToString().Replace("\\n", "</li>").Replace("-", "<li>"));
                            iCount++;
                        }

                    }
                }
            }

            if (HttpContext.Current.Request.Cookies["Cookie_Key"] ==null)
            {
                loginPanel.Visible = true;
                MenuPanel.Visible = false;
            }
            else
            {
                ltTitle.Text = "หน้าหลัก";
                loginPanel.Visible = false;
                MenuPanel.Visible = true;
               // ltLeftmenu.Text = "<ul class='leftnav'>" + gUtilities.retMenu(false) +"</ul>";
                ManageCookie mgCookie = new ManageCookie();
                users ck = mgCookie.ReadCookies();
                string role_id = ck.p_role_id;
                if (role_id == "2")
                {
                    ltLeftmenu.Text = string.Format(@"<ul class='leftnav'>
                            <li>
                                <ul class='sub-leftmenu'>
                                    <li><a href='announce.aspx'>โครงการที่ยังไม่</a></li>
                                    <li><a href='announce.aspx'>ข่าวประกาศ</a></li>
                                    <li><a href='announce.aspx'>ข่าวประกาศ</a></li>
                                </ul>
                            </li>                        
                        </ul>");
                }
                else if (role_id == "3")
                {
                    ltLeftmenu.Text = string.Format(@"
                    <ul class='leftnav'>                            
                            <li><b>ผู้ดูและระบบ</b>
                                <ul class='sub-leftmenu'>
                                    <li><a href='manage_user.aspx'>ดูแลและกำหนดสิทธิการเข้าใช้งานในระบบ</a></li>
                                </ul>
                            </li>
                    </ul>
                    <br />
                    <ul class='leftnav'>
                            <li><b>รายงาน</b>
                                <ul class='sub-leftmenu'>
                                    <li><a href='project_submitted_list.aspx'>โครงการที่ผ่านการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</a></li>
                                    <li><a href='project_not_submitted_list.aspx'>โครงการที่ไม่อยู่ในข่ายการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</a></li>
                                    <li><a href='project_for_budgetor.aspx?status=1'>โครงการที่ผ่านการแสดงความคิดเห็นจากเจ้าหน้าที่จัดทำงบประมาณ</a></li>
                                    <li><a href='project_for_budgetor.aspx?status=2'>โครงการที่ผ่านการพิจารณาจากรัฐสภา</a></li>
                                    <li><a href='Project_report_evaluator.aspx?reportid=1'>รายงานภาพรวมทั่วประเทศ แยกตามโครงการที่อยู่ในข่าย/ไม่อยู่ในข่ายการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</a></li>
                                    <li><a href='Project_report_evaluator.aspx?reportid=2'>รายงานภาพรวมทั่วประเทศ แยกตามประเภทโครงการ</a></li>
                                    <li><a href='Project_report_evaluator.aspx?reportid=3'>รายงานภาพรวมทั่วประเทศ แยกตามผลการพิจารณาจากรัฐสภา</a></li>
                                    <li><a href='Project_report_evaluator.aspx?reportid=5'>รายงานภาพรวมทั่วประเทศ แบ่งตามลักษณะของโครงการ</a></li>
                                    <li><a href='Project_report_evaluator.aspx?reportid=4'>รายงานภาพรวมทั่วประเทศ แบ่งตามยุทธศาสตร์จัดสรรโครงการ</a></li>
                                    <li><a href='Project_report_evaluator.aspx?reportid=6'>รายงานภาพรวมทั่วประเทศ แบ่งตามสถานะการวิเคราะห์โครงการ</a></li>
                                    <li><a href='Project_report_evaluator.aspx?reportid=7'>รายงานภาพรวมทั่วประเทศ แยกตามผลการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</a></li>
                                    
                                    <li><a href='check_project_report.aspx'>รายงานการตรวจสอบโครงการของส่วนราชการทั่วประเทศ จำแนกตามรายกระทรวง</a></li>
                                    <li><a href='projects_report.aspx'>รายงานภาพรวมผลการวิเคราะห์ความเสี่ยงตามหลักธรรมมาภิบาลของส่วนราชการทั่วประเทศ จำแนกตามรายกระทรวง</a></li>
                                </ul>
                            </li>                        
                        </ul>");

                    //<li><a href='projects_report.aspx'>รายงานภาพรวมผลการวิเคราะห์ความเสี่ยงตามหลักธรรมมาภิบาลของส่วนราชการทั่วประเทศ จำแนกตามรายกระทรวง</a></li>
                }
                else if (role_id == "4")
                {
                    ltLeftmenu.Text = string.Format(@"
                        <ul class='leftnav'>                            
                                <li><b>รายงาน (Super Admin)</b>
                                    <ul class='sub-leftmenu'>
                                        <li><a href='project_submitted_list.aspx'>โครงการที่ผ่านการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</a></li>
                                        <li><a href='project_not_submitted_list.aspx'>โครงการที่ไม่อยู่ในข่ายการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</a></li>
                                    </ul>
                                </li>
                        </ul>
                        <br />
                        <ul class='leftnav'>                            
                                <li><b>ผู้ดูแลระบบ (Super Admin)</b>
                                    <ul class='sub-leftmenu'>
                                        <li><a href='announce.aspx'>ข่าวประกาศ</a></li>
                                        <li><a href='manage_user.aspx'>ดูแลและกำหนดสิทธิการเข้าใช้งานในระบบ</a></li>
                                        <li><a href='yutasad.aspx'>จัดการเพิ่มเติมและแก้ไขข้อมูลเกี่ยวกับยุทธศาสตร์</a></li>
                                        <li><a href='tammapiban.aspx'>จัดการเพิ่มเติมและแก้ไขข้อมูลธรรมาภิบาล</a></li>
                                        <li><a href='ministry_dept_info_management.aspx'>จัดการข้อมูลกระทรวงและหน่วยงาน</a></li>
                                    </ul>
                                </li>
                        </ul>");
                }
            }
        }
    }

   
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string retVal ="";
        retVal = gUtilities.Login(txtIdCard.Text.Trim(), txtName.Text.Trim(), ddlStatus.SelectedValue);

        if (retVal == "")
        {
            //the login user doesn't exist in the system
            if (Convert.ToInt32(ddlStatus.SelectedValue) == 1) { Response.Redirect("project_home.aspx"); }
            else if (Convert.ToInt32(ddlStatus.SelectedValue) == 2) { Response.Redirect("project_for_budget_home.aspx"); }
            else if (Convert.ToInt32(ddlStatus.SelectedValue) == 3) { Response.Redirect("default.aspx"); }
            else if (Convert.ToInt32(ddlStatus.SelectedValue) == 4) { Response.Redirect("default.aspx"); }
            else { Response.Redirect("contacts.aspx"); }
        }
        else  
        {
            //msg = retVal;
            ton.JavaScript.MessageBox(retVal);
        }


    }
}