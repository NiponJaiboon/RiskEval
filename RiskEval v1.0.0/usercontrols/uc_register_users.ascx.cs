using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ton;
using System.Data;
using ton.config;


public partial class usercontrols_uc_register_users : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_register_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
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
                    ton.JavaScript.MessageBox(" -เลขบัตรประชาชนนี้ได้ลงทะเบียนแล้ว \r\n -ไม่สามารถลงทะเบียนซ้ำได้");
                }
                else
                {
                    sds_register_staff_persons.Insert();
                }
            }
            catch (Exception ex)
            {
                ton.JavaScript.MessageBox(ex.Message.ToString());
            }
        }
    }
    protected void sds_register_user_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {

            string p_id = e.Command.Parameters["@Identity"].Value.ToString();

            // Delete all Detail of Created User by Mark as Is_Delete
            sds_register_staff_persons_detail.DeleteParameters["p_id"].DefaultValue = p_id;
            sds_register_staff_persons_detail.Delete();

            // Start Inserting new Data
            {
                {
                    string d_id = ddl_department.SelectedValue;
                    sds_register_staff_persons_detail.InsertParameters["p_id"].DefaultValue = p_id;
                    sds_register_staff_persons_detail.InsertParameters["d_id"].DefaultValue = d_id;
                    sds_register_staff_persons_detail.Insert();
                }
            }
            // End Registration Process Return to Root
            ton.JavaScript.MessageBox("คุณได้ลงทะเบียนเรียบร้อยแล้ว", Global_config.RootURL);
        }
        else
        {
            ton.JavaScript.MessageBox("การลงทะเบียนล้มเหลว");
        }
    }
    protected void ddl_ministry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_department.Items.Clear();
        ddl_department.Items.Insert(0, new ListItem("กรุณาเลือก", "-1"));
    }

}