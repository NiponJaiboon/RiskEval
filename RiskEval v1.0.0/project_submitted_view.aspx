<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="project_submitted_view.aspx.cs" Inherits="project_submitted_view" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามแผนงาน/โครงการ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" Runat="Server">

    <asp:Literal ID="litHeader" runat="server"></asp:Literal>

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


    <div class="submit-box">            
    
  
        <table id="tb-sumbit" cellpadding="2" cellspacing="0">
            <tr class="tr-submit">
                <td class="td-text">สรุปผลคะแนนการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล อยู่ในเกณฑ์ที่มีระดับความเสี่ยง</td>
                <td style="border-style:solid; width:100px; text-align:center; border-color:#666600;"><asp:Literal ID="litRisk1" runat="server"></asp:Literal></td>
           </tr>

            <tr class="tr-submit">
                <td class="td-text">ผลคะแนนการวิเคราะห์ความเสี่ยงด้านสภาพแวดล้อมภายในและภายนอก อยู่ในระดับที่มีความเสี่ยง</td>
                <td style="border-style:solid; width:100px; text-align:center; border-color:#666600;"><asp:Literal ID="litRisk2" runat="server"></asp:Literal></td>
            </tr>

           <!--
           <tr>     
                 <td class="td-text">สรุปผลคะแนนการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามชุดคำถามหลัก</td>
                 <td style="border-style:solid; width:100px; text-align:center; border-color:#666600;"><asp:Literal ID="litRisk12" runat="server"></asp:Literal></td>
            </tr>
               <tr>     
                 <td class="td-text">สรุปผลคะแนนการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามชุดคำถามย่อย</td>
                 <td style="border-style:solid; width:100px; text-align:center; border-color:#666600;"><asp:Literal ID="litRisk13" runat="server"></asp:Literal></td>
            </tr>
            -->
        </table>
 
       
    
    </div>
    <div class="submit-box">     
        <p><asp:HyperLink ID="linkreport1" runat="server" Text="รายงานที่ 1 รายงานการกลั่นกรองโครงการ" NavigateUrl="project_report.aspx?reportid=3"></asp:HyperLink></p>
        <p><asp:HyperLink ID="linkreport2" runat="server" Text="รายงานที่ 2 รายงานการวิเคราะห์ความเสี่ยงตามหลักธรรมาธิบาล" NavigateUrl="project_report.aspx?reportid=6"></asp:HyperLink></p>
        <p><asp:HyperLink ID="linkreport3" runat="server" Text="รายงานที่่ 3 รายงานการวิเคราะห์ความเสี่ยงด้านสภาพแวดล้อมภายในและภายนอก" NavigateUrl="project_report.aspx?reportid=2"></asp:HyperLink></p>
    </div>


</asp:Content>




