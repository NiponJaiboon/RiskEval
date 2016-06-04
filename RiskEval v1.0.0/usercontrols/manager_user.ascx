<%@ Control Language="C#" AutoEventWireup="true" CodeFile="manager_user.ascx.cs"
    Inherits="usercontrols_manager_user" %>
<style type="text/css">
    .imgbtn
    {
    }
    .chkbox_selectAll
    {
    }
    .chkbox_select
    {
    }
</style>
<script type="text/javascript">
    //    function swapOnOffImg(id) {
    //        $(id).hover(
    //             function () {
    //                 id.src = id.src.replace("_off", "_on");
    //             },
    //             function () {
    //                 id.src = id.src.replace("_on", "_off");
    //             }
    //            );
    //         }

    //    Pure JQuery
    $(document).ready(function () {

        //        // find all img name suffix "_on" 
        //        var imglist = [];
        //        $(".imgbtn").each(function (i,img) {
        //            imglist[i] = img.src.replace("_off", "_on");
        //        });
        //        //alert(imglist);
        //        // preload on mouse over image
        //        $.preLoadImages(imglist);


        $(".imgbtn").hover(
             function () {
                 this.src = this.src.replace("_off", "_on");
             },
             function () {
                 this.src = this.src.replace("_on", "_off");
             }
            );
    });

    (function ($) {
        var cache = [];
        $.preLoadImages = function () {
            var args_len = arguments.length;
            for (var i = args_len; i--; ) {
                var cacheImage = document.createElement('img');
                cacheImage.src = arguments[i];
                cache.push(cacheImage);
            }
        }
    })(jQuery)
</script>
<script type="text/javascript">
    
    function checkbox_seq_click(id) {
        //alert($(id).attr("id").indexOf('selectAll'));
        if ($(id).attr("id").indexOf('selectAll') > -1) {
            //alert($(id).attr("checked"));
            $("#<%= gv_uers.ClientID %>").find("input:checkbox").attr("checked", $(id).attr("checked"));
        }
        else {
            var values = true;
            $('#<%=gv_uers.ClientID%>').find('input:checkbox').each(function (i,chklist) {

                //if ($(this).attr("id").indexOf('selectAll') == -1) {
                if(chklist.id.indexOf('selectAll') == -1) {
                    //alert($(this).attr('id')+chklist.src);
                    var currentValue = $(this).attr('checked');

                    // for filter only checkbox.checked = true,checked
                    //if (currentValue) 
                    {
                        //alert(currentValue + '  |  ' + values);
                        values = values && currentValue;
                    }
                }
            })
//            alert(values);

            $(".chkbox_selectAll input:checkbox").attr('checked', values); 
        }
    }
    function checkbox_select(id) {
    }

    function popup_user_detail(id) {
        var ur = location.protocol + "//"+ location.host + <%=  ton.config.Global_config.RootURL  %> + "manage_user_detail.aspx?id=" + id;
        //alert(ur);
        var child_window = window.open(ur, "myWindow", "status = 0, height = 390, width = 450, resizable = 1, location = 0").focus();
    }

    function toggleUserDetail(id)
    {
        $("#"+id).toggle();
    }
</script>
<div id="filter" runat="server">
    <table>
        <tr>
            <td>
                เลขบัตรประชาชน
            </td>
            <td>
                <asp:TextBox ID="txt_idno" runat="server" MaxLength="13"></asp:TextBox>
            </td>
            <td>
                &nbsp;<asp:Literal ID="lit_is_active" runat="server" Text="เปิดใช้งาน"></asp:Literal>
            </td>
            <td>
                <asp:DropDownList ID="ddl_is_active" runat="server">
                    <asp:ListItem Value="">กรุณาเลือก</asp:ListItem>
                    <asp:ListItem Value="1">เปิดใช้งาน</asp:ListItem>
                    <asp:ListItem Value="0">ปิดใช้งาน</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Panel ID="pnl_is_delete" runat="server">
                    <asp:Literal ID="lit_is_delete" runat="server" Text="ลบแล้ว"></asp:Literal>
                    <br />
                    <asp:DropDownList ID="ddl_is_delete" runat="server">
                        <asp:ListItem Value="">กรุณาเลือก</asp:ListItem>
                        <asp:ListItem Value="0">ยังไม่ลบ</asp:ListItem>
                        <asp:ListItem Value="1">ลบแล้ว</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                ชื่อ
            </td>
            <td>
                <asp:TextBox ID="txt_name" runat="server"></asp:TextBox>
            </td>
            <td>
                นามสกุล
            </td>
            <td>
                <asp:TextBox ID="txt_sname" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td rowspan="2">
                <asp:Button ID="btn_search" runat="server" Text="ค้นหา" Style="height: 100%" OnClick="btn_search_Click" />
                <asp:Button ID="btn_search0" runat="server" Text="รายงานผู้ใช้" Style="height: 100%"
                    OnClick="btn_search0_Click" />
            </td>
        </tr>
        <tr>
            <td>
                กระทรวง
            </td>
            <td>
                <asp:DropDownList ID="ddl_ministry" runat="server" AutoPostBack="True" DataSourceID="sds_ministry"
                    DataTextField="mi_desc" DataValueField="mi_id" OnSelectedIndexChanged="ddl_ministry_SelectedIndexChanged"
                    AppendDataBoundItems="True">
                    <asp:ListItem Value="-1">กรุณาเลือก</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                สถานะ
            </td>
            <td>
                <asp:DropDownList ID="ddl_role" runat="server" AppendDataBoundItems="True" DataSourceID="sds_role"
                    DataTextField="role_name" DataValueField="role_id">
                    <asp:ListItem Value="-1">กรุณาเลือก</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                หน่วยงาน
            </td>
            <td>
                <asp:DropDownList ID="ddl_department" runat="server" DataSourceID="sds_department"
                    DataTextField="d_desc" DataValueField="d_id" AppendDataBoundItems="True">
                    <asp:ListItem Value="-1">กรุณาเลือก</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                วันเดือนปีที่ลงทะเบียน
            </td>
            <td>
                <asp:RadioButtonList ID="rbl_user_create_orderby" runat="server">
                    <asp:ListItem Value="1">มากไปน้อย</asp:ListItem>
                    <asp:ListItem Value="2">น้อยไปมาก</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</div>
<asp:GridView ID="gv_uers" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" DataKeyNames="p_id,p_idno" DataSourceID="sds_users"
    EnableModelValidation="True" OnRowDataBound="gv_uers_RowDataBound" ShowFooter="True"
    CssClass="grid">
    <Columns>
        <asp:TemplateField HeaderText="ลำดับ">
            <FooterTemplate>
                <asp:CheckBox ID="cbk_seq_selectAll" runat="server" CssClass="chkbox_selectAll" OnClick="checkbox_seq_click(this)" />
            </FooterTemplate>
            <HeaderTemplate>
                <asp:CheckBox ID="cbk_seq_selectAll" runat="server" CssClass="chkbox_selectAll" OnClick="checkbox_seq_click(this)" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="cbk_seq" CssClass="chkbox_select" runat="server" OnClick="checkbox_seq_click(this)" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="p_id" HeaderText="p_id" InsertVisible="False" ReadOnly="True"
            SortExpression="p_id" Visible="False" />
        <asp:TemplateField HeaderText="บัตร ปชช." SortExpression="p_idno">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("p_idno") %>'></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <a href="#" onclick="popup_user_detail('<%# Eval("p_id") %>');return false;">
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("p_idno") %>'></asp:Label>
                </a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="p_name_thai" HeaderText="ชื่อ(ไทย)" SortExpression="p_name_thai" />
        <asp:BoundField DataField="p_sname_thai" HeaderText="นามสกุล(ไทย)" SortExpression="p_sname_thai" />
        <asp:BoundField DataField="p_name_eng" HeaderText="ชื่อ(อังกฤษ)" SortExpression="p_name_eng"
            Visible="False" />
        <asp:BoundField DataField="p_sname_eng" HeaderText="นามสกุล(อังกฤษ)" SortExpression="p_sname_eng"
            Visible="False" />
        <asp:BoundField DataField="p_address" HeaderText="ที่อยู่" SortExpression="p_address"
            Visible="False" />
        <asp:BoundField DataField="p_phone" HeaderText="โทรศัทพ์" SortExpression="p_phone"
            Visible="False" />
        <asp:BoundField DataField="p_phone_ext" HeaderText="เบอร์ต่อ" SortExpression="p_phone_ext"
            Visible="False" />
        <asp:BoundField DataField="p_mobile" HeaderText="เบอร์มือถือ" SortExpression="p_mobile"
            Visible="False" />
        <asp:BoundField DataField="m_id" HeaderText="m_id" SortExpression="m_id" Visible="False" />
        <asp:BoundField DataField="mi_code" HeaderText="รหัสกระทรวง" SortExpression="mi_code"
            Visible="False" />
        <asp:BoundField DataField="mi_name" HeaderText="กระทรวง" SortExpression="mi_name"
            Visible="False" />
        <asp:BoundField DataField="o_id" HeaderText="o_id" SortExpression="o_id" Visible="False" />
        <asp:BoundField DataField="o_name" HeaderText="สังกัด" SortExpression="o_name" Visible="False" />
        <asp:BoundField DataField="p_role_id" HeaderText="p_role_id" SortExpression="p_role_id"
            Visible="False" />
        <asp:BoundField DataField="role_name" HeaderText="สถานะ" SortExpression="role_name"
            Visible="False" />
        <asp:BoundField DataField="subrole_name" HeaderText="สถานะ" SortExpression="subrole_name"
            Visible="False" />
        <asp:BoundField DataField="sangad_department" HeaderText="สังกัดหน่วยงาน" SortExpression="sangad_department" />
        <asp:TemplateField HeaderText="เปิดใช้งาน" SortExpression="p_is_active">
            <EditItemTemplate>
                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("p_is_active") %>' />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Image ID="img_is_active" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ลบแล้ว" SortExpression="p_is_delete">
            <EditItemTemplate>
                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("p_is_delete") %>' />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Image ID="img_is_delete" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="อยุ่ในระบบ" SortExpression="p_is_online">
            <ItemTemplate>
                <asp:Image ID="img_is_online" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="หน่วยงานที่ดูแล" Visible="False">
            <ItemTemplate>
                <asp:Image ID="img_exp_users_detail" runat="server" ImageUrl="~/images/icon/icon_13.gif" />
                <asp:GridView ID="grd_users_detail" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                    GridLines="None" ShowHeader="False">
                    <Columns>
                        <asp:BoundField DataField="d_code" HeaderText="รหัสหน่วยงาน" SortExpression="d_code" />
                        <asp:BoundField DataField="d_name" HeaderText="ชื่อหน่วยงาน" SortExpression="d_name" />
                    </Columns>
                </asp:GridView>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle CssClass="gridAltRow" />
    <FooterStyle CssClass="gridFooter" />
    <HeaderStyle CssClass="gridHeader" />
    <PagerStyle CssClass="gridPager" HorizontalAlign="Center" />
    <RowStyle CssClass="gridRow" />
    <EmptyDataTemplate>
        ไม่มีข้อมูล</EmptyDataTemplate>
</asp:GridView>
<asp:SqlDataSource ID="sds_users" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
    SelectCommand="SELECT persons.p_id, persons.p_idno, persons.p_name_thai, persons.p_sname_thai, persons.p_name_eng, persons.p_sname_eng, persons.p_address, persons.p_phone, 
               persons.p_phone_ext, persons.p_mobile, persons.m_id, ministry.mi_code, ministry.mi_name, persons.o_id, office.o_name, persons.p_role_id, roles.role_name, 
               persons.p_is_active, persons.p_is_delete, persons.p_is_online, roles_sub.subrole_name, tbl_sangard.sangad_department
FROM  persons INNER JOIN
               ministry ON persons.m_id = ministry.mi_id INNER JOIN
               roles ON persons.p_role_id = roles.role_id LEFT OUTER JOIN
                   (SELECT DISTINCT 
               TOP (100) PERCENT persons_1.p_id, 
               CASE WHEN sum_detail.member_count &lt;= 1 THEN department.d_name WHEN sum_detail.member_count &gt; 1 THEN roles_sub_1.subrole_name END AS sangad_department, department.d_id
FROM  (SELECT p_id, COUNT(d_id) AS member_count
               FROM   persons_detail
               WHERE (ISNULL(pdt_is_delete, '') &lt;&gt; 1)
               GROUP BY p_id) AS sum_detail INNER JOIN
               persons_detail AS persons_detail_1 ON persons_detail_1.p_id = sum_detail.p_id INNER JOIN
               department ON department.d_id = persons_detail_1.d_id INNER JOIN
               persons AS persons_1 ON persons_detail_1.p_id = persons_1.p_id INNER JOIN
               roles AS roles_1 ON persons_1.p_role_id = roles_1.role_id LEFT OUTER JOIN
               roles_sub AS roles_sub_1 ON persons_1.p_role_sub_id = roles_sub_1.subrole_id
WHERE (ISNULL(persons_detail_1.pdt_is_delete, '') &lt;&gt; 1)
ORDER BY sangad_department) AS tbl_sangard ON persons.p_id = tbl_sangard.p_id LEFT OUTER JOIN
               roles_sub ON persons.p_role_sub_id = roles_sub.subrole_id LEFT OUTER JOIN
               office ON persons.o_id = office.o_id

WHERE ((persons.p_is_delete = @p_is_delete) OR (@p_is_delete IS NULL)) AND (persons.p_idno LIKE '%' + @p_idno + '%') AND (persons.p_name_thai LIKE N'%' + @p_name_thai + N'%') AND (persons.p_sname_thai LIKE N'%' + @p_sname_thai + N'%') AND ((persons.m_id = @m_id) OR (@m_id = -1)) AND ((persons.p_role_id = @role_id) OR (@role_id = -1)) AND ((p_is_active = @p_is_active) OR (@p_is_active IS 
    null)) and ((tbl_sangard.d_id = @d_id) OR (@d_id = -1)) order by CASE @OrderBy WHEN  -1 THEN persons.p_idno end asc, CASE @OrderBy   WHEN 1 THEN persons.created_date  end asc, CASE @OrderBy  WHEN 2 THEN persons.created_date END desc"
    UpdateCommand="UPDATE persons SET p_is_active = @p_is_active, p_is_delete = @p_is_delete, modified_date = GETDATE() WHERE (p_id = @p_id)"
    OnSelecting="sds_users_Selecting" CancelSelectOnNullParameter="False">
    <SelectParameters>
        <asp:ControlParameter ControlID="ddl_is_delete" DefaultValue="" Name="p_is_delete"
            PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="txt_idno" DefaultValue="%%" Name="p_idno" PropertyName="Text" />
        <asp:ControlParameter ControlID="txt_name" DefaultValue="%%" Name="p_name_thai" PropertyName="Text" />
        <asp:ControlParameter ControlID="txt_sname" DefaultValue="%%" Name="p_sname_thai"
            PropertyName="Text" />
        <asp:ControlParameter ControlID="ddl_ministry" DefaultValue="" Name="m_id" PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="ddl_department" DefaultValue="" Name="d_id" PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="ddl_role" DefaultValue="" Name="role_id" PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="rbl_user_create_orderby" DefaultValue="-1" Name="OrderBy" PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="ddl_is_active" DefaultValue="" ConvertEmptyStringToNull="true"
            Name="p_is_active" PropertyName="SelectedValue" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="p_is_active" />
        <asp:Parameter Name="p_is_delete" />
        <asp:Parameter Name="p_id" />
    </UpdateParameters>
</asp:SqlDataSource>
<div id="mangeuser_action">
    <asp:ImageButton ID="imgbtn_save" runat="server" ImageUrl="~/images/icon/icon_save_off.gif"
        OnClick="imgbtn_save_Click" CssClass="imgbtn" Visible="False" />
    <asp:ImageButton ID="imgbtn_del" runat="server" ImageUrl="~/images/icon/icon_del_off.gif"
        OnClick="imgbtn_del_Click" OnClientClick="return confirm('ท่านมั่นใจที่จะ ลบ ผู้ที่ถูกเลือก  ?');"
        AlternateText="ลบ" CssClass="imgbtn" />
    <asp:ImageButton ID="imgbtn_activate" runat="server" ImageUrl="~/images/icon/icon_active_off.gif"
        OnClick="imgbtn_activate_Click" AlternateText="เปิดการใช้งาน" CssClass="imgbtn" />
    <asp:ImageButton ID="imgbtn_deactivate_off" runat="server" ImageUrl="~/images/icon/icon_inactive_off.gif"
        OnClick="imgbtn_deactivate_off_Click" OnClientClick="return  confirm('ท่านมั่นใจที่จะ ปิดการใช้งาน ของผู้ที่ถูกเลือก ?');"
        AlternateText="ปิดการใช้การ" CssClass="imgbtn" />
    <asp:ImageButton ID="imgbtn_unlock_off" runat="server" ImageUrl="~/images/icon/icon_unlock_off.gif"
        OnClick="imgbtn_unlock_off_Click" OnClientClick="return  confirm('ท่านมั่นใจที่จะ ปลดล๊อก ของผู้ที่ถูกเลือก ?');"
        AlternateText="ปลดล๊อก" CssClass="imgbtn" />
    &nbsp;
</div>
<asp:SqlDataSource ID="sds_role" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
    SelectCommand="SELECT role_id, role_name FROM roles ORDER BY role_id" CancelSelectOnNullParameter="False">
    <SelectParameters>
        <asp:Parameter DefaultValue="%%" Name="role_name" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sds_office" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
    SelectCommand="SELECT o_id, o_name FROM office ORDER BY o_id"></asp:SqlDataSource>
<asp:SqlDataSource ID="sds_ministry" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
    SelectCommand="SELECT mi_id, mi_code + ' / ' + mi_name AS mi_desc FROM ministry ORDER BY mi_code">
</asp:SqlDataSource>
<asp:SqlDataSource ID="sds_users_detail" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
    SelectCommand="SELECT department.d_code, department.d_name FROM persons_detail INNER JOIN department ON persons_detail.d_id = department.d_id WHERE (persons_detail.p_id = @p_id)">
    <SelectParameters>
        <asp:Parameter Name="p_id" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sds_department" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
    SelectCommand="SELECT d_id, (d_code + ' / ' + d_name) AS [d_desc], mi_id FROM department WHERE (mi_id = @mi_id)">
    <SelectParameters>
        <asp:ControlParameter ControlID="ddl_ministry" Name="mi_id" PropertyName="SelectedValue"
            Type="Int32" DefaultValue="" />
    </SelectParameters>
</asp:SqlDataSource>
