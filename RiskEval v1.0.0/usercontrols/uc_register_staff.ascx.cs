using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ton;
using System.Data;
using ton.config;
using riskEval;


public partial class usercontrols_uc_register : System.Web.UI.UserControl
{
    protected bool allowEditbyRole()
    {
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();
        if (ck != null)
        {
            if (!string.IsNullOrEmpty(ck.p_role_id))
            {
                if (ck.p_role_id != "1")
                {
                    return true;
                }
                else
                {
                    Response.Redirect(Global_config.RootURL);
                }
            }
        }
        return false;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region inital dropdownlist data
            DataView dv_office = (DataView)sds_office.Select(DataSourceSelectArguments.Empty);
            DataView dv_role = (DataView)sds_role.Select(DataSourceSelectArguments.Empty);
            DataView dv_ministry = (DataView)sds_ministry.Select(DataSourceSelectArguments.Empty);

            foreach (DataRow dr_office in dv_office.Table.Rows)
            {
                ddl_office.Items.Add(new ListItem(dr_office[1].ToString(), dr_office[0].ToString()));
            }
            foreach (DataRow dr_role in dv_role.Table.Rows)
            {
                ddl_role.Items.Add(new ListItem(dr_role[1].ToString(), dr_role[0].ToString()));
            }
            foreach (DataRow dr_ministry in dv_ministry.Table.Rows)
            {
                ddl_ministry.Items.Add(new ListItem(dr_ministry[1].ToString(), dr_ministry[0].ToString()));
            } 
            #endregion


            ManageCookie mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();
            // เข้าสู่ mode แก้ไข
            if ((ck != null) && allowEditbyRole())
            {
                if (!string.IsNullOrEmpty(ck.p_id))
                {
                    //load Existing Data from Database and Map with related GUI
                    //load ID_No. from Cookies
                    txt_idno.Text = ck.p_idno;

                    //sds_register_staff_persons.SelectParameters["p_idno"].DefaultValue = txt_idno.Text;
                    DataView dv1 = (DataView)sds_register_staff_persons.Select(DataSourceSelectArguments.Empty);

                    if ((dv1 != null) && (dv1.Table != null) && (dv1.Table.Rows.Count == 1))
                    {
                        // load persons
                        txt_firstname_thai.Text = dv1.Table.Rows[0]["p_name_thai"].ToString();
                        txt_lastname_thai.Text = dv1.Table.Rows[0]["p_sname_thai"].ToString();
                        txt_firstname_eng.Text = dv1.Table.Rows[0]["p_name_eng"].ToString();
                        txt_lastname_eng.Text = dv1.Table.Rows[0]["p_sname_eng"].ToString();
                        txt_address.Text = dv1.Table.Rows[0]["p_address"].ToString();
                        txt_telephoneno.Text = dv1.Table.Rows[0]["p_phone"].ToString().Trim();
                        txt_telephoneno_ext.Text = dv1.Table.Rows[0]["p_phone_ext"].ToString().Trim();
                        txt_mobileno.Text = dv1.Table.Rows[0]["p_mobile"].ToString().Trim();
                        txt_telephoneno2.Text = dv1.Table.Rows[0]["p_phone_direct"].ToString().Trim();
                        txt_email.Text = dv1.Table.Rows[0]["p_email"].ToString().Trim();

                        ddl_role.Items.FindByValue(dv1.Table.Rows[0]["p_role_id"].ToString()).Selected = true;
                        ddl_office.Items.FindByValue(dv1.Table.Rows[0]["o_id"].ToString()).Selected = true;
                        ddl_ministry.Items.FindByValue(dv1.Table.Rows[0]["m_id"].ToString()).Selected = true;

                        //Disable Edit Persons
                        txt_idno.Enabled = txt_firstname_eng.Enabled = txt_firstname_thai.Enabled = false;
                        txt_address.Enabled = txt_lastname_eng.Enabled = txt_lastname_thai.Enabled = txt_idno.Enabled;
                        txt_mobileno.Enabled = txt_telephoneno.Enabled = txt_telephoneno_ext.Enabled = txt_idno.Enabled;
                        ddl_ministry.Enabled = ddl_office.Enabled = ddl_role.Enabled = txt_idno.Enabled;
                        txt_email.Enabled = txt_telephoneno2.Enabled = txt_idno.Enabled;

                        initial_ddl_department();

                        #region persons_detail
                        //load persons_detail by calling "sds_register_staff_persons_detail" with parameter
                        sds_register_staff_persons_detail.SelectParameters["p_id"].DefaultValue = ck.p_id;
                        DataView dv2 = (DataView)sds_register_staff_persons_detail.Select(DataSourceSelectArguments.Empty);

                        foreach (DataRow dr in dv2.Table.Rows)
                        {
                            //foreach (ListItem li in cbklist_department.Items)
                            //{
                            //    if (li.Value == dr[1].ToString())
                            //    {
                            //        li.Selected = true;
                            //    }
                            //}

                            try
                            {
                                cbklist_department.Items.FindByValue(dr[1].ToString()).Selected = true;
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        #endregion
                        
                    }
                }
            }
        }
    }
    protected void initial_ddl_department()
    {
        cbklist_department.DataSourceID = "sds_department";
        cbklist_department.DataTextField = "d_desc";
        cbklist_department.DataValueField = "d_id";
        cbklist_department.DataBind();
    }
    protected void btn_register_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ManageCookie mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();
            
            //force role to get rolesub
            ddl_rolesub.DataBind();
            string amomo = ddl_rolesub.SelectedValue;
            //try
            {
                int exist = 0;
                DataView dv = (DataView)sds_register_staff_persons.Select(DataSourceSelectArguments.Empty);

                if (dv.Table != null)
                {
                    exist = dv.Table.Rows.Count;
                }

                // Check for Existing User ( ID Card No. )
                if (exist > 0)
                {
                    // mode แก้ไข
                    if((ck != null) && (!string.IsNullOrEmpty(ck.p_id)) && allowEditbyRole() )
                    {
                        if (save_persons_detail(ck.p_id))
                        {
                            ton.JavaScript.MessageBox(" -แก้ไขข้อมูลบุคคลสำเร็จ ");
                        }
                        else
                        {
                            ton.JavaScript.MessageBox(" -แก้ไขข้อมูลบุคคลล้มเหลว ");
                        }
                    }
                    else
                    {
                        ton.JavaScript.MessageBox(" -เลขบัตรประชาชนนี้ได้ลงทะเบียนแล้ว \r\n -ไม่สามารถลงทะเบียนซ้ำได้");
                    }
                }
                else
                {
                    sds_register_staff_persons.Insert();
                }
            }
            //catch (Exception ex)
            {
                //ton.JavaScript.MessageBox(ex.Message.ToString());
            }
        }
    }
    protected bool save_persons_detail(string p_id)
    {
        // Delete all Detail of Created User by Mark as Is_Delete
        sds_register_staff_persons_detail.DeleteParameters["p_id"].DefaultValue = p_id;
        sds_register_staff_persons_detail.Delete();

        // Start Inserting new Data
        int result = 0;
        int count_select = 0;
        for (int i = 0; i < cbklist_department.Items.Count; i++)
        {
            if (cbklist_department.Items[i].Selected)
            {
                count_select++;
                string d_id = cbklist_department.Items[i].Value.ToString();
                sds_register_staff_persons_detail.InsertParameters["p_id"].DefaultValue = p_id;
                sds_register_staff_persons_detail.InsertParameters["d_id"].DefaultValue = d_id;
                result += sds_register_staff_persons_detail.Insert();
            }
        }
        
        if (result == count_select)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    protected void sds_register_user_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
           
            string p_id = e.Command.Parameters["@Identity"].Value.ToString();

            save_persons_detail(p_id);
            // End Registration Process Return to Root
            ton.JavaScript.MessageBox("คุณได้ลงทะเบียนเรียบร้อยแล้ว",Global_config.RootURL);
        }
        else
        {
            ton.JavaScript.MessageBox("การลงทะเบียนล้มเหลว");
        }
    }
    protected void btn_department_selectAll_Click(object sender, EventArgs e)
    {
        for(int i=0;i<cbklist_department.Items.Count;i++)
        {
            cbklist_department.Items[i].Selected = true;
        }
    }
    protected void CustomValidator_department_ServerValidate(object source, ServerValidateEventArgs args)
    {
        bool values = false;
        for (int i = 0; i < cbklist_department.Items.Count; i++)
        {
            bool currentValue = cbklist_department.Items[i].Selected;
            values = values || currentValue;
        }
        args.IsValid = values;
    }
    protected void ddl_ministry_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbk_department_selectAll.Checked = false;
        initial_ddl_department();
    }
}
