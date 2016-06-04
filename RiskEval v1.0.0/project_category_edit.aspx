<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="project_category_edit.aspx.cs" Inherits="project_category_edit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามแผนงาน/โครงการ ระบุลักษณะโครงการ
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" Runat="Server">
   ระบุลักษณะโครงการ  (แก้ไข)
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
                         <tr>
                            <td class="bold1">รหัสโครงการ:</td>
                            <td><asp:Label ID="lblProjectCode" runat="server"></asp:Label></td>
                            <td class="bold1">ชื่อโครงการ:</td>
                            <td><asp:Label ID="lblProjectName" runat="server"></asp:Label></td>
                        </tr>
                           <tr>
                             <td colspan="2" class="bold1">ยุทธศาสตร์การจัดสรรงบประมาณ:</td>
                             <td colspan="2"><asp:Label ID="lblYutasard" runat="server"></asp:Label></td>
                        </tr>
                         <tr>  
                             <td class="bold1">ปีงบประมาณ:</td>
                             <td><asp:Label ID="lblYear" runat="server"></asp:Label></td>
                             <td class="bold1">วงเงินงบประมาณทั้งสิ้น (บาท):</td>
                             <td><asp:Label ID="lblBudget" runat="server"></asp:Label></td>
                        </tr>
                   
                           <tr>
                             <td colspan="2" class="bold1">โครงการบูรณาการที่สำนักงบประมาณกำหนด:</td>
                             <td colspan="2"><asp:Label ID="lblIntegrateProject" runat="server"></asp:Label></td>
                        </tr>
                          <tr>
                             <td colspan="2" class="bold1">หน่วยงานที่เกี่ยงข้องกับการบูรณาการ:</td>
                             <td colspan="2"><asp:Label ID="lblRelateDept" runat="server"></asp:Label></td>
                        </tr>
                    </table>     
                </fieldset>
            </div>

             <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
               </asp:SqlDataSource>

    <div>                 
        <div class="box">
            <div class="box-header"></div>
            <div class="box-body">
                <div>
                <p class="title">ลักษณะโครงการ</p> 
                
                    <div>
                           <asp:RadioButtonList ID="radProjectCate" runat="server">
                               <asp:ListItem Text="บริหารทั่วไป" Value="บริหารทั่วไป"></asp:ListItem>
                               <asp:ListItem Text="บริหารสังคม" Value="บริหารสังคม"></asp:ListItem>
                               <asp:ListItem Text="เศรษฐกิจ" Value="เศรษฐกิจ"></asp:ListItem>
                                <asp:ListItem Text="อื่นๆ" Value="อื่นๆ"></asp:ListItem>
                           </asp:RadioButtonList>
      
                    </div>  
                </div>               
            </div>
            <div class="box-footer"></div>
        </div>
    </div>

    <div>     
                    <asp:RequiredFieldValidator
                           id="reqPJ"
                            runat="server"
                             ControlToValidate="radProjectCate"
                              Text="กรุณาเลือกลักษณะโครงการ"
                               Display="Dynamic"
                                SetFocusOnError="true"
                               CssClass="ErrorDokJan"
                               ErrorMessage="กรุณาเลือกลักษณะโครงการ"></asp:RequiredFieldValidator>
    </div>
    <div>
    
          <asp:Button 
            ID="btnNext" 
            runat="server" 
            Text="บันทึกข้อมูลและดำเนินการต่อไป" 
            onclientclick="return confirm('ต้องการบันทึกและดำเนินการต่อไป');"
            onclick="btnNext_Click" />
    
    </div>


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnUpdating="SqlDataSource1_Updating"
        ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>" 
         UpdateCommand="update projects set [pj_category] = @pj_category where pj_id = @pj_id"
        
        ></asp:SqlDataSource>

</asp:Content>

