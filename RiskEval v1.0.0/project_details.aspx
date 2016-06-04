<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="project_details.aspx.cs" Inherits="project_details" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามแผนงาน/โครงการ ระบุข้อมูลรายละเอียดโครงการ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" Runat="Server">
    ระบุข้อมูลรายละเอียดโครงการ
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

        function numberWithCommas(x) {
            return x.toString().replace(/\B(?=(?:\d{3})+(?!\d))/g, ",");
        }

        $(document).ready(function () {
            var textbox = $('#<%=txtBudget.ClientID %>');
            textbox.blur(function () {

                var v_pound = textbox.val().replace(',', '');
                v_pound = numberWithCommas(v_pound);
                $(this).val(v_pound)

            });
        });

    </script>

    <div>
         <div class="accountInfo">
                <fieldset class="login">
                    <legend class="bold1">ข้อมูลโครงการ</legend>
                    <table style="margin:0px 0px 0px 0px;" >
                         <tr>
                            <td class="bold1">รหัสกระทรวง:</td>
                            <td><asp:Label ID="lblDeptCode" runat="server"></asp:Label></td>  
                            <td class="bold1">กระทรวง:</td>
                            <td><asp:Label ID="lblDeptName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                             <td class="bold1">รหัสหน่วยงาน:</td>
                            <td><asp:Label ID="lblDivisionCode" runat="server"></asp:Label></td>
                            <td class="bold1">หน่วยงาน:</td>
                            <td><asp:Label ID="lblDivisionName" runat="server"></asp:Label></td>
                        </tr>
                    </table>     
                       
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
                    </asp:SqlDataSource>

                </fieldset>
            </div>

        <div class="title">ระบุข้อมูลรายละเอียดโครงการ</div>    
       
        <div>
                  <table style="margin:0px 0px 0px 0px">
                        <tr>
                            <td class="bold1">รหัสโครงการ:</td>
                            <td><asp:Label ID="lblProjectCode" runat="server"></asp:Label></td>  
                        </tr>
                        <tr>
                            <td class="bold1">ชื่อโครงการ:</td>
                            <td><asp:TextBox ID="txtProjectName" runat="server" Width="500px" MaxLength="850" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqProjectName" runat="server" ControlToValidate="txtProjectName" Text="*" ErrorMessage="กรุณากรอกชื่อโครงกการ" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
             
                            </td>
                        </tr>
                        <tr>
                            <td class="bold1">ยุทธศาสตร์การจัดสรรงบประมาณ:</td>
                            <td><asp:DropDownList ID="ddlYudtasad" runat="server" DataSourceID="SqlDataSource1" DataTextField="yut_name" DataValueField="yut_id" >
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="bold1">ปีงบประมาณ:</td>
                            <td><asp:DropDownList ID="ddlYear" runat="server">
                                <asp:ListItem>2556</asp:ListItem>
                                <asp:ListItem>2557</asp:ListItem>
                                <asp:ListItem>2558</asp:ListItem>
                                <asp:ListItem>2559</asp:ListItem>
                                <asp:ListItem>2560</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                              <td class="bold1">วงเงินงบประมาณทั้งสิ้น (บาท):</td>
                            <td><asp:TextBox ID="txtBudget" runat="server" Width="200px" MaxLength="200"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="reqBudget" runat="server" ControlToValidate="txtBudget" Text="*" ErrorMessage="กรุณากรอกงบประมาณ" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                              <asp:RegularExpressionValidator runat="server" ControlToValidate="txtBudget" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$" Text="*" ErrorMessage="กรุณากรอกงบประมาณเป็นจำนวนเงิน ใส่ทศนิยมได้สองตำแน่งเท่านั้น"  SetFocusOnError="true" Display="Dynamic"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                         <tr>
                            <td class="bold1">อยู่ในงบรายจ่ายประเภท:</td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server">
                                    <asp:ListItem Text="กรุณาเลือก" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="งบดำเนินงาน" Value="งบดำเนินงาน"></asp:ListItem>
                                    <asp:ListItem Text="งบลงทุน" Value="งบลงทุน"></asp:ListItem>
                                    <asp:ListItem Text="งบเงินอุดหนุน" Value="งบเงินอุดหนุน"></asp:ListItem>
                                    <asp:ListItem Text="งบรายจ่ายอื่น" Value="งบรายจ่ายอื่น"></asp:ListItem>   
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddl" runat="server"
                                 ControlToValidate="DropDownList2" 
                                  Text="*"
                                  SetFocusOnError="true" 
                                  Display="Dynamic" 
                                  CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <!-- 
                         <tr>
                            <td class="bold1">โครงการบูรณาการที่สำนักงบประมาณกำหนด:</td>
                            <td><asp:TextBox ID="txtIntegrate" runat="server"  Width="500px" MaxLength="850"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="bold1">หน่วยงานที่เกี่ยวข้องกับบูรณาการ:</td>
                            <td><asp:TextBox ID="txtRelate" runat="server"  Width="500px" MaxLength="850"></asp:TextBox></td>
                        </tr>
                        -->
                    </table>
        </div>
        <div>
            
            <asp:Button ID="btnNext" runat="server"  Text="บันทึกข้อมูลและดำเนินการต่อไป" 
                onclick="btnNext_Click" 
                onclientclick="return confirm('ต้องการบันทึกและดำเนินการต่อไป');" />

                <asp:ValidationSummary ID="sumErr" runat="server" CssClass="question-error"/>

        </div>

    </div> 

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>" 
        SelectCommand="SELECT * FROM [yutasad] where isActive = 1"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server"
    ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"></asp:SqlDataSource>

   <asp:SqlDataSource ID="SqlDataSource3" runat="server"
    ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
    InsertCommand="insert into projects (pj_code, p_id, pj_lastupdate, pj_isinuse, pj_status, d_id) values (@pj_code, @p_id, getdate(), 1, @pj_status, @d_id)" OnInserting="SqlDataSource3_Inserting">
    </asp:SqlDataSource>

    <asp:SqlDataSource 
        ID="SqlDataSource44" 
        runat="server" 
        OnUpdating="SqlDataSource44_Updating"
        ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
         UpdateCommand="update projects set p_id = @p_id, d_id = @d_id, mi_id = @mi_id,pj_name  = @pj_name, pj_yut_id  = @pj_yut_id, pj_year = @pj_year,pj_budget  = @pj_budget, pj_budget_money = @pj_budget_money, pj_lastupdate = getdate(), pj_budget_type = @pj_budget_category where pj_code = @pj_code and pj_status = @pj_status"></asp:SqlDataSource>
 
  <asp:SqlDataSource ID="SqlDataSource5" runat="server"
    ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"></asp:SqlDataSource>

  <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
    ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"></asp:SqlDataSource>

    
  <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
    ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"></asp:SqlDataSource>

</asp:Content>

