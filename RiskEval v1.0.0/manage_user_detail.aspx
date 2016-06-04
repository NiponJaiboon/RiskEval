<%@ Page Language="C#" AutoEventWireup="true" CodeFile="manage_user_detail.aspx.cs" Inherits="manage_user_detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ข้อมูลผู้ใช้</title>
    <link href="./css/layout.css" rel="stylesheet" type="text/css" media="screen" />
    <!-- Overwrite Styel in layout.css -->
    <style type="text/css">
    html,body
    {
        background:none;
    }
    </style>
</head>
<body style="text-align:left">
    <form id="form1" runat="server">
    <div class="content-box">
    
        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
            DataKeyNames="p_id" DataSourceID="sds_user_detail" 
            EnableModelValidation="True" CssClass="grid" 
            >
            
            <Fields>
                <asp:BoundField DataField="p_id" HeaderText="p_id" InsertVisible="False" 
                    ReadOnly="True" SortExpression="p_id" Visible="False" />
                <asp:BoundField DataField="p_name_thai" HeaderText="ชื่อ(ภาษาไทย)" 
                    SortExpression="p_name_thai" />
                <asp:BoundField DataField="p_idno" HeaderText="p_idno" SortExpression="p_idno" 
                    Visible="False" />
                <asp:BoundField DataField="p_sname_thai" HeaderText="นามสกุล(ภาษาไทย)" 
                    SortExpression="p_sname_thai" />
                <asp:BoundField DataField="p_name_eng" HeaderText="ชื่อ(ภาษาอังกฤษ)" 
                    SortExpression="p_name_eng" />
                <asp:BoundField DataField="p_sname_eng" HeaderText="นามสกุล(ภาษาอังกฤษ)" 
                    SortExpression="p_sname_eng" />
                <asp:BoundField DataField="p_address" HeaderText="ที่อยุ่" 
                    SortExpression="p_address" />
                <asp:BoundField DataField="subrole_name" HeaderText="สถานะ" 
                    SortExpression="subrole_name" />
                <asp:TemplateField HeaderText="เบอร์โทรศัพท์กลาง" SortExpression="p_phone">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("p_phone") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("p_phone") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# (Eval("p_phone")).ToString() + " ต่อ " + (Eval("p_phone_ext")).ToString()  %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="p_phone_ext" HeaderText="p_phone_ext" 
                    SortExpression="p_phone_ext" Visible="False" />
                <asp:BoundField DataField="p_mobile" HeaderText="เบอร์โทรศัพท์มือถือ" 
                    SortExpression="p_mobile" />
                <asp:BoundField DataField="p_phone_direct" HeaderText="เบอร์โทรศัพท์ตรง" 
                    SortExpression="p_phone_direct" />
                <asp:TemplateField HeaderText="E-mail" SortExpression="p_email">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("p_email") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("p_email") %>'></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <a href='<%# "mailto:" + Eval("p_email") %>'>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("p_email") %>'></asp:Label>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Fields>
            <EmptyDataTemplate>ไม่มีข้อมูล</EmptyDataTemplate>
            <FieldHeaderStyle CssClass="gridHeader" />
            <AlternatingRowStyle CssClass="gridAltRow" />
            <FooterStyle Cssclass="gridFooter" />
            <HeaderStyle CssClass="gridHeader" />
            <PagerStyle CssClass="gridPager" HorizontalAlign="Center" />
            <RowStyle  CssClass="gridRow" />
        </asp:DetailsView>
        <asp:SqlDataSource ID="sds_user_detail" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>" 
            
            
            SelectCommand="SELECT persons.p_id, persons.p_name_thai, persons.p_idno, persons.p_sname_thai, persons.p_name_eng, persons.p_sname_eng, persons.p_address, persons.p_phone, persons.p_phone_ext, persons.p_mobile, persons.p_phone_direct, persons.p_email, persons.p_role_id, persons.p_role_sub_id, roles.role_name, roles_sub.subrole_name FROM persons INNER JOIN roles ON persons.p_role_id = roles.role_id LEFT OUTER JOIN roles_sub ON persons.p_role_sub_id = roles_sub.subrole_id WHERE (persons.p_id = @p_id)">
            <SelectParameters>
                <asp:QueryStringParameter Name="p_id" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
