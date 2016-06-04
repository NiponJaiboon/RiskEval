<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_register_users.ascx.cs" Inherits="usercontrols_uc_register_users" %>

<asp:Panel ID="pnl_register" runat="server">
    <table>
        <tr>
            <td>เลขที่บัตรประชาชน</td>
            <td>
                <asp:TextBox ID="txt_idno" runat="server" MaxLength="13"></asp:TextBox>
                <asp:RegularExpressionValidator ID="vld_idno" runat="server" 
                    ControlToValidate="txt_idno" ErrorMessage="เลขบัตรประชาชนไม่ถูกต้อง" 
                    ValidationExpression="\d{13}" ValidationGroup="register">*</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txt_idno" ErrorMessage="กรุณาใส่เลขบัตรประชาน13หลัก" 
                    ValidationGroup="register">*</asp:RequiredFieldValidator>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>ชื่อ(ภาษาไทย)(ไม่ต้องระบุคำนำหน้า)</td>
            <td>
                <asp:TextBox ID="txt_firstname_thai" runat="server" ></asp:TextBox>
                <asp:RegularExpressionValidator ID="vld_idno0" runat="server" 
                    ControlToValidate="txt_firstname_thai" ErrorMessage="ชื่อภาษาไทยไม่ถูกต้อง" 
                    ValidationExpression="[ก-์]+" ValidationGroup="register">*</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txt_firstname_thai" ErrorMessage="กรุณาใส่ชื่อภาษาไทย" 
                    ValidationGroup="register">*</asp:RequiredFieldValidator>
            </td>
            <td>นามสกุล(ภาษาไทย)</td>
            <td>
                <asp:TextBox ID="txt_lastname_thai" runat="server" ></asp:TextBox>

                <asp:RegularExpressionValidator ID="vld_idno1" runat="server" 
                    ControlToValidate="txt_lastname_thai" ErrorMessage="นามสกุลภาษาไทยไม่ถูกต้อง" 
                    ValidationExpression="[ก-์]+" ValidationGroup="register">*</asp:RegularExpressionValidator>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="txt_lastname_thai" ErrorMessage="กรุณาใส่นามสกุลภาษาไทย" 
                    ValidationGroup="register">*</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td>ชื่อ(ภาษาอังกฤษ)</td>
            <td>
                <asp:TextBox ID="txt_firstname_eng" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="vld_idno3" runat="server" 
                    ControlToValidate="txt_firstname_eng" ErrorMessage="ชื่อภาษาอังกฤษไม่ถูกต้อง" 
                    ValidationExpression="[a-zA-Z]+" ValidationGroup="register">*</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="txt_firstname_eng" ErrorMessage="กรุณาใส่ชื่อภาษาอังกฤษ" 
                    ValidationGroup="register">*</asp:RequiredFieldValidator>
            </td>
            <td>นามสกุล(ภาษาอังกฤษ)</td>
            <td>
                <asp:TextBox ID="txt_lastname_eng" runat="server" ></asp:TextBox>
                <asp:RegularExpressionValidator ID="vld_idno2" runat="server" 
                    ControlToValidate="txt_lastname_eng" ErrorMessage="นามสกุลภาษาอังกฤษไม่ถูกต้อง" 
                    ValidationExpression="[a-zA-Z]+" ValidationGroup="register">*</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                    ControlToValidate="txt_lastname_eng" ErrorMessage="กรุณาใส่นามสกุลภาษาอังกฤษ" 
                    ValidationGroup="register">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>ที่อยู่หน่วยงาน (ส่งเอกสาร)</td>
            <td colspan="3">
                <asp:TextBox ID="txt_address" runat="server" TextMode="MultiLine" 
                    CssClass="textArea" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txt_address" ErrorMessage="กรุณาใส่ที่อยู่หน่วยงาน" 
                    ValidationGroup="register">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>หมายเลขโทรศัพท์กลาง</td>
            <td>
                <asp:TextBox ID="txt_telephoneno" runat="server" MaxLength="10" ></asp:TextBox>
                <asp:RegularExpressionValidator ID="vld_idno5" runat="server" 
                    ControlToValidate="txt_telephoneno" ErrorMessage="เบอร์โทรศัพท์ไม่ถูกต้อง" 
                    ValidationExpression="[\d]+" ValidationGroup="register">*</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                    ControlToValidate="txt_telephoneno" ErrorMessage="กรุณาใส่เบอร์โทรศัพท์" 
                    ValidationGroup="register" Enabled="False">*</asp:RequiredFieldValidator>
            </td>
            <td>เบอร์ต่อ</td>
            <td>
                <asp:TextBox ID="txt_telephoneno_ext" runat="server" ></asp:TextBox>
                <asp:RegularExpressionValidator ID="vld_idno9" runat="server" 
                    ControlToValidate="txt_telephoneno_ext" ErrorMessage="เบอร์ต่อไม่ถูกต้อง" 
                    ValidationExpression="[\d]*" ValidationGroup="register">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>หมายเลขโทรศัพท์ตรง</td>
            <td>
                <asp:TextBox ID="txt_telephoneno2" runat="server" MaxLength="10" ></asp:TextBox>
                <asp:RegularExpressionValidator ID="vld_idno20" runat="server" 
                    ControlToValidate="txt_telephoneno2" ErrorMessage="เบอร์โทรศัพท์ตรงไม่ถูกต้อง" 
                    ValidationExpression="[\d]+" ValidationGroup="register">*</asp:RegularExpressionValidator>
            </td>
            <td>หมายเลขโทรศัพท์มือถือ</td>
            <td>
                <asp:TextBox ID="txt_mobileno" runat="server" MaxLength="10" ></asp:TextBox>
                <asp:RegularExpressionValidator ID="vld_idno6" runat="server" 
                    ControlToValidate="txt_mobileno" ErrorMessage="เบอร์โทรศัพท์มือถือไม่ถูกต้อง" 
                    ValidationExpression="[\d]+" ValidationGroup="register">*</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                    ControlToValidate="txt_mobileno" ErrorMessage="กรุณาใส่เบอร์มือถือ" 
                    ValidationGroup="register" Enabled="False">*</asp:RequiredFieldValidator>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>E-Mail</td>
            <td colspan="3">
                <asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="req_email" runat="server" 
                    ErrorMessage="อีเมล์ไม่ถูกต้อง" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ControlToValidate="txt_email" ValidationGroup="register"
                    >*</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                    ControlToValidate="txt_email" ErrorMessage="กรุณากรอก E-mail" 
                    ValidationGroup="register">*</asp:RequiredFieldValidator>
                    
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="lit_office" runat="server" Text="สังกัด" Visible="False"></asp:Literal>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddl_office" runat="server" DataSourceID="sds_office" 
                    DataTextField="o_name" DataValueField="o_id" AppendDataBoundItems="True" 
                    Visible="False">
                    <asp:ListItem Selected="True" Value="-1">กรุณาเลือก</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                    ControlToValidate="ddl_office" ErrorMessage="กรุณาเลือกสังกัด" 
                    ValidationGroup="register" InitialValue="-1" Enabled="False" 
                    Visible="False">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                สถานะผู้ใช้งาน</td>
            <td colspan="3">
                <asp:DropDownList ID="ddl_role" runat="server" DataSourceID="sds_role" 
                    DataTextField="role_name" DataValueField="role_id" 
                    AppendDataBoundItems="True">
                    <asp:ListItem Value="-1">กรุณาเลือก</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                    ControlToValidate="ddl_role" ErrorMessage="กรุณาเลือกสถานะผู้ใช้งาน" 
                    ValidationGroup="register" InitialValue="-1">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                สังกัดกระทรวง/กลุ่มจังหวัด</td>
            <td colspan="3">
                <asp:DropDownList ID="ddl_ministry" runat="server" AutoPostBack="True" 
                    DataSourceID="sds_ministry" DataTextField="mi_desc" DataValueField="mi_id" 
                    onselectedindexchanged="ddl_ministry_SelectedIndexChanged" 
                    AppendDataBoundItems="True">
                    <asp:ListItem Value="-1">กรุณาเลือก</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="ddl_ministry" ErrorMessage="กรุณาเลือกกระทรวงที่ดูแล" 
                    ValidationGroup="register" InitialValue="-1">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>สังกัดหน่วยงาน/กลุ่มจังหวัด</td>
            <td colspan="3">
                <asp:DropDownList ID="ddl_department" runat="server" 
                    DataSourceID="sds_department" DataTextField="d_desc" DataValueField="d_id" 
                    AppendDataBoundItems="True" >
                    <asp:ListItem Value="-1">กรุณาเลือก</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="ddl_department" ErrorMessage="กรุณาเลือกหน่วยงานที่ดูแล" 
                    ValidationGroup="register" InitialValue="-1">*</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="pnl_department_list" runat="server" style="width:100%">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <center>
                    <asp:Button ID="btn_register" runat="server" Text="ลงทะเบียน" 
                        ValidationGroup="register" onclick="btn_register_Click" 
                        onclientclick="return confirm('คุณกรอกข้อมูลลงทะเบียนถูกหรือไม่ ?');" />
                </center>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ValidationGroup="register" ShowMessageBox="True" />
    </asp:Panel>
<asp:SqlDataSource ID="sds_ministry" runat="server" 
    ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>" 
    
    SelectCommand="SELECT mi_id, mi_code + ' / ' + mi_name AS mi_desc FROM ministry">
</asp:SqlDataSource>
<asp:SqlDataSource ID="sds_department" runat="server" 
    ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>" 
    
    
    SelectCommand="SELECT d_id, (d_code + ' / ' + d_name) AS [d_desc], mi_id FROM department WHERE (mi_id = @mi_id)">
    <SelectParameters>
        <asp:ControlParameter ControlID="ddl_ministry" Name="mi_id" 
            PropertyName="SelectedValue" Type="Int32" DefaultValue="" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sds_register_staff_persons" runat="server" 
    ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>" 
    InsertCommand="INSERT INTO persons(p_idno, p_name_thai, p_sname_thai, p_name_eng, p_sname_eng, p_address, p_phone, p_phone_ext, p_phone_direct,p_mobile, p_email, m_id, o_id, p_is_active, p_is_delete, p_role_id, p_role_sub_id) VALUES (@p_idno, @p_name_thai, @p_sname_thai, @p_name_eng, @p_sname_eng, @p_address, @p_phone, @p_phone_ext, @p_phone_direct,@p_mobile, @p_email, @m_id, @o_id, @p_is_active, @p_is_delete, @p_role_id, @p_role_sub_id)
SET @Identity = @@Identity" 
    
    SelectCommand="SELECT p_id, p_idno, p_name_thai, p_sname_thai, p_name_eng, p_sname_eng, p_address, p_phone, p_phone_ext, p_mobile, p_email ,m_id, o_id, p_is_active, p_is_delete, p_role_id, p_role_sub_id FROM persons WHERE (p_idno = @p_idno) AND (p_is_delete &lt;&gt; 1)" 
    oninserted="sds_register_user_Inserted">
    <InsertParameters>
        <asp:ControlParameter ControlID="txt_idno" Name="p_idno" PropertyName="Text" />
        <asp:ControlParameter ControlID="txt_firstname_thai" Name="p_name_thai" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="txt_lastname_thai" Name="p_sname_thai" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="txt_firstname_eng" Name="p_name_eng" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="txt_lastname_eng" Name="p_sname_eng" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="txt_address" Name="p_address" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="txt_telephoneno" Name="p_phone" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="txt_telephoneno_ext" Name="p_phone_ext" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="txt_telephoneno2" Name="p_phone_direct" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="txt_mobileno" Name="p_mobile" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="txt_email" Name="p_email" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="ddl_ministry" Name="m_id" 
            PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="ddl_office" Name="o_id" 
            PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="ddl_role" Name="p_role_sub_id" 
            PropertyName="SelectedValue" />
        <asp:Parameter Name="p_is_active" DefaultValue="0" />
        <asp:Parameter Name="p_is_delete" DefaultValue="0" />
        <asp:Parameter Name="p_role_id" DefaultValue="1" />
        
        <asp:Parameter Name="Identity" Direction="Output" DefaultValue="" Type="Int32" />
    </InsertParameters>
    <SelectParameters>
        <asp:ControlParameter ControlID="txt_idno" Name="p_idno" PropertyName="Text" />
    </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sds_register_staff_persons_detail" runat="server" 
    ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>" 
    InsertCommand="INSERT INTO persons_detail(p_id, d_id) VALUES (@p_id, @d_id)" 
    
    SelectCommand="SELECT p_id, d_id FROM persons_detail WHERE (pdt_is_delete &lt;&gt; 1) AND (p_id = @p_id) AND (d_id = @d_id)" 
    DeleteCommand="UPDATE persons_detail SET pdt_is_delete = 1, modified_date = GETDATE() WHERE (p_id = @p_id)">
    <DeleteParameters>
        <asp:Parameter Name="p_id" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="p_id" DbType="Int32" DefaultValue="-1"  />
        <asp:Parameter Name="d_id" DbType="Int32" DefaultValue="-1" />
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter Name="p_id" />
        <asp:Parameter Name="d_id" />
    </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sds_office" runat="server" 
    ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>" 
    SelectCommand="SELECT [o_id], [o_name] FROM [office]"></asp:SqlDataSource>
<asp:SqlDataSource ID="sds_role" runat="server" 
    ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>" 
    
    
    SelectCommand="SELECT roles_sub.subrole_id as [role_id], roles_sub.subrole_name as [role_name]
FROM  roles INNER JOIN
               roles_sub ON roles.role_id = roles_sub.role_id
WHERE (roles_sub.role_id = 1)
Order by roles_sub.subrole_id">
</asp:SqlDataSource>


