<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="project_basicinfo.aspx.cs" Inherits="project_basicinfo" MaintainScrollPositionOnPostback="true"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามแผนงาน/โครงการ กรอกข้อมูลพื้นฐานโครงการ
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" Runat="Server">
    กรอกข้อมูลพื้นฐานโครงการ
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
                   <!--
                           <tr>
                             <td colspan="2" class="bold1">โครงการบูรณาการที่สำนักงบประมาณกำหนด:</td>
                             <td colspan="2"><asp:Label ID="lblIntegrateProject" runat="server"></asp:Label></td>
                        </tr>
                          <tr>
                             <td colspan="2" class="bold1">หน่วยงานที่เกี่ยงข้องกับการบูรณาการ:</td>
                             <td colspan="2"><asp:Label ID="lblRelateDept" runat="server"></asp:Label></td>
                        </tr>
                        -->
                    </table>     
                </fieldset>
            </div>

             <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
               </asp:SqlDataSource>


            
        <div class="title">กรอกข้อมูลพื้นฐานโครงการ</div>    

       <div style="margin: 10px 10px 10px 10px">

        <table style="border:1px solid white;">
          <tr>
            <td class="bold1">1. ความเป็นมาของโครงการ</td>
          </tr>
           <tr>
            <td>
                <asp:TextBox ID="txtBackground" runat="server"  TextMode="MultiLine" Rows="7" Columns="70" MaxLength="2000"></asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="reqBG" 
                    runat="server" 
                    ControlToValidate="txtBackground" 
                    Text="*" 
                     Display="Dynamic"
                      SetFocusOnError="true"
                      CssClass="ErrorDokJan"
                    ErrorMessage="กรุณากรอกความเป็นมาของโครงการ"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
	                runat="server" 
                    ControlToValidate="txtBackground" 
	                ErrorMessage="ห้ามเกิน 2000 ตัวอักษร"
                     CssClass="question-error"
                     Display="Dynamic"
                     SetFocusOnError="true"
                    ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,2000}$">
	            </asp:RegularExpressionValidator>

            </td>
          </tr>
           <tr>
            <td class="bold1">2. ความสำคัญ / เร่งด่วนของโครงการ</td>
          </tr>
           <tr>
            <td>
                <asp:TextBox ID="txtUrgency" runat="server"  TextMode="MultiLine" Rows="7" Columns="70" MaxLength="2000"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="reqUG" 
                    runat="server" 
                    ControlToValidate="txtUrgency" 
                     Display="Dynamic"
                    Text="*" 
                    SetFocusOnError="true"
                    CssClass="ErrorDokJan"
                    ErrorMessage="กรุณากรอกความสำคัญ/เร่งด่วนของโครงการ"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
	                runat="server" 
                    ControlToValidate="txtUrgency" 
	                ErrorMessage="ห้ามเกิน 2000 ตัวอักษร"
                    SetFocusOnError="true" 
                     Display="Dynamic"
                     CssClass="question-error"
                     ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,2000}$">
	            </asp:RegularExpressionValidator>
              
            </td>
          </tr>

        
        </table>
      </div>

      <div>
        <asp:ValidationSummary ID="sum1" runat="server"/>
      </div>

      <div>

        <asp:Button 
            ID="btnNext" 
            runat="server" 
            Text="บันทึกข้อมูลและดำเนินการต่อไป" 
            onclientclick="return confirm('ต้องการบันทึกและดำเนินการต่อไป');"
            onclick="btnNext_Click" />
      </div>


      <asp:SqlDataSource 
        ID="SqlDataSource1" 
        runat="server" 
        OnUpdating="SqlDataSource1_Updating"
        ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
        UpdateCommand="update [projects] set pj_background = @pj_background, pj_urgency = @pj_urgency where [pj_id] = @pj_id"></asp:SqlDataSource>

</asp:Content>



