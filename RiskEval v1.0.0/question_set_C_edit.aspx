<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="question_set_C_edit.aspx.cs" Inherits="question_set_C_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามแผนงาน/โครงการ คำถามชุด ค
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" Runat="Server">
คำถามชุด ค: การจัดลำดับและจัดสรรงบประมาณโครงการ
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:Panel ID="panel1" runat="server" Visible="true"  CssClass="QL1">
    <div class="box-question">
        <div class="text"><asp:Literal ID="lblQuestion1" runat="server"></asp:Literal></div>
        <div  class="assumption"><asp:Literal id="lblAssumption" runat="server"></asp:Literal></div>
         <div class="QL2"  >

                <asp:Literal ID="lblQuestion2_1" runat="server"></asp:Literal>  

                <asp:RequiredFieldValidator 
                ID="reqRad1" 
                runat="server" 
                 Text="*"
                  ErrorMessage="กรุณาเลือกใช่ หรือ ไม่ใช่"
                   SetFocusOnError="true"
                    Display="Dynamic" CssClass="ErrorDokJan" 
                    ControlToValidate="radanswer2_1"></asp:RequiredFieldValidator><br />

                <asp:RadioButtonList ID="radanswer2_1" runat="server"  
                    RepeatDirection="Horizontal" 
                     AutoPostBack="true"
                    onselectedindexchanged="answer2_1_SelectedIndexChanged">
                    <asp:ListItem Value="ใช่">ใช่</asp:ListItem>
                    <asp:ListItem Value="ไม่ใช่">ไม่ใช่</asp:ListItem>
                </asp:RadioButtonList>


            <div class="question-option">
        
                ถ้าตอบว่า "ใช่" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้

            <p>
                <span class="QL3"><asp:Literal ID="lblQuestion3_1" runat="server"></asp:Literal></span><br />
                <asp:TextBox ID="txtAnswerQuestion3_1" CssClass="TextArea" runat="server"  TextMode="MultiLine" Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                     <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator1" 
                        runat="server" 
                        ControlToValidate="txtAnswerQuestion3_1" 
                        Text="*" 
                         Display="Dynamic"
                          SetFocusOnError="true" CssClass="ErrorDokJan" 
                        ErrorMessage="กรุณากรอกคำตอบข้อ1.1"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
	                    runat="server" 
                        ControlToValidate="txtAnswerQuestion3_1" 
	                    ErrorMessage="ห้ามเกิน 1500 ตัวอักษร"
                         Display="Dynamic" CssClass="ErrorDokJan" 
                         SetFocusOnError="true"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
	                </asp:RegularExpressionValidator>
                    </p>

                <p>
               <span class="QL3"><asp:Literal ID="lblQuestion3_2" runat="server"></asp:Literal></span><br />
                <asp:TextBox ID="txtAnswerQuestion3_2" CssClass="TextArea" runat="server"  TextMode="MultiLine" Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                     <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator2" 
                        runat="server" 
                        ControlToValidate="txtAnswerQuestion3_2" 
                        Text="*" 
                         Display="Dynamic" CssClass="ErrorDokJan" 
                          SetFocusOnError="true"
                        ErrorMessage="กรุณากรอกคำตอบข้อ1.2"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
	                    runat="server" 
                        ControlToValidate="txtAnswerQuestion3_2" 
	                    ErrorMessage="ห้ามเกิน 1500 ตัวอักษร"
                         Display="Dynamic" CssClass="ErrorDokJan" 
                         SetFocusOnError="true"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
	                </asp:RegularExpressionValidator>
                </p>

            <%--<p>
              <span class="QL3"><asp:Literal ID="lblQuestion3_3" runat="server"></asp:Literal></span><br />
                <asp:TextBox ID="txtAnswerQuestion3_3" runat="server" CssClass="TextArea"  TextMode="MultiLine" Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                     <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator3" 
                        runat="server" 
                        ControlToValidate="txtAnswerQuestion3_3" 
                        Text="*" 
                         Display="Dynamic"
                          SetFocusOnError="true"
                        ErrorMessage="กรุณากรอกคำตอบข้อ1.3"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
	                    runat="server" 
                        ControlToValidate="txtAnswerQuestion3_3" 
	                    ErrorMessage="ห้ามเกิน 1500 ตัวอักษร"
                         Display="Dynamic"
                         SetFocusOnError="true"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
	                </asp:RegularExpressionValidator>
                    </p>--%>

            </div>
        </div>
    </div>
    <div class="question-button">
    <asp:Button ID="btnNextToQ2_2" runat="server" Text="บันทึกข้อมูลและดำเนินงานต่อไป" 
    OnClientClick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}" 
    onclick="btnNextToQ2_2_Click" />
    </div>

  </asp:Panel>

<asp:Literal ID="litfinish" runat="server" Visible="false"></asp:Literal><br />
<asp:Button ID="btnToQB" runat="server" onclick="btnToQB_Click" Visible="false"  Text="ดำเนินการต่อไป"/>

<asp:SqlDataSource runat="server" ID="SqlDataSource1" 
ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"></asp:SqlDataSource>
</asp:Content>
