<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="question_set_A_edit.aspx.cs" Inherits="question_set_A_edit"  MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามแผนงาน/โครงการ คำถามชุด ก
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" Runat="Server">
คำถามชุด ก: ขั้นตอนการริเริ่มแผนงาน/โครงการและวิเคราะห์เบื้องต้น (แก้ไข)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:Panel ID="panel1" runat="server" Visible="true">
  <div class="box-question">
    <div style="text-align:left; margin-top:10px;">
        <div style="color:Red; font-size:20px;" class="QL1"><asp:Literal ID="lblQuestion1" runat="server"></asp:Literal></div>

        <div style="display:none"><div style="color:blue; font-size:18px;" class="QL2"><asp:Literal id="lblAssumption" runat="server"></asp:Literal></div></div>

        <div style="margin-left:15px; margin-top:15px;">
                <asp:Literal ID="lblQuestion2_1" runat="server"></asp:Literal>  
                <asp:RequiredFieldValidator 
                ID="reqRad1" 
                runat="server" 
                 Text="*"
                  ErrorMessage="กรุณาเลือกใช่ หรือ ไม่ใช่"
                   SetFocusOnError="true"
                    Display="Dynamic"
                    CssClass="ErrorDokJan"
                    ControlToValidate="radanswer2_1"></asp:RequiredFieldValidator><br />

                <asp:RadioButtonList ID="radanswer2_1" runat="server"  
                    RepeatDirection="Horizontal" 
                     AutoPostBack="true"
                    onselectedindexchanged="answer2_1_SelectedIndexChanged">
                    <asp:ListItem Value="ใช่">ใช่</asp:ListItem>
                    <asp:ListItem Value="ไม่ใช่">ไม่ใช่</asp:ListItem>
                </asp:RadioButtonList>


            <div style="margin-left:10px">
        
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
                          SetFocusOnError="true"
                          CssClass="ErrorDokJan"
                        ErrorMessage="กรุณากรอกคำตอบข้อ1.1"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
	                    runat="server" 
                        ControlToValidate="txtAnswerQuestion3_1" 
	                    ErrorMessage="ห้ามเกิน 1500 ตัวอักษร"
                         Display="Dynamic"
                         SetFocusOnError="true"
                         CssClass="question-error"
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
                         Display="Dynamic"
                          SetFocusOnError="true"
                          CssClass="ErrorDokJan"
                        ErrorMessage="กรุณากรอกคำตอบข้อ1.2"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
	                    runat="server" 
                        ControlToValidate="txtAnswerQuestion3_2" 
	                    ErrorMessage="ห้ามเกิน 1500 ตัวอักษร"
                         Display="Dynamic"
                         SetFocusOnError="true"
                         CssClass="question-error"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
	                </asp:RegularExpressionValidator>
                </p>

            <p>
              <span class="QL3"><asp:Literal ID="lblQuestion3_3" runat="server"></asp:Literal></span><br />
                <asp:TextBox ID="txtAnswerQuestion3_3" runat="server" CssClass="TextArea"  TextMode="MultiLine" Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                     <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator3" 
                        runat="server" 
                        ControlToValidate="txtAnswerQuestion3_3" 
                        Text="*" 
                         Display="Dynamic"
                          SetFocusOnError="true"
                          CssClass="ErrorDokJan"
                        ErrorMessage="กรุณากรอกคำตอบข้อ1.3"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
	                    runat="server" 
                        ControlToValidate="txtAnswerQuestion3_3" 
	                    ErrorMessage="ห้ามเกิน 1500 ตัวอักษร"
                         Display="Dynamic"
                         SetFocusOnError="true"
                         CssClass="question-error"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
	                </asp:RegularExpressionValidator>
                    </p>

                    <p>
                      <span class="QL3"><asp:Literal ID="lblQuestion3_4" runat="server"></asp:Literal></span><br />
                <asp:TextBox ID="txtAnswerQuestion3_4" runat="server" CssClass="TextArea"  TextMode="MultiLine" Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                     <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator4" 
                        runat="server" 
                        ControlToValidate="txtAnswerQuestion3_4" 
                        Text="*" 
                         Display="Dynamic"
                          SetFocusOnError="true"
                         CssClass="ErrorDokJan"
                        ErrorMessage="กรุณากรอกคำตอบข้อ1.4"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
	                    runat="server" 
                        ControlToValidate="txtAnswerQuestion3_4" 
	                    ErrorMessage="ห้ามเกิน 1500 ตัวอักษร"
                         Display="Dynamic"
                         SetFocusOnError="true"
                         CssClass="question-error"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
	                </asp:RegularExpressionValidator>
                    </p>

                    <p>
              <span class="QL3"><asp:Literal ID="lblQuestion3_5" runat="server"></asp:Literal></span><br />
                <asp:TextBox ID="txtAnswerQuestion3_5" CssClass="TextArea" runat="server"  TextMode="MultiLine" Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                     <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator5" 
                        runat="server" 
                        ControlToValidate="txtAnswerQuestion3_5" 
                        Text="*" 
                         Display="Dynamic"
                        CssClass="ErrorDokJan"
                          SetFocusOnError="true"
                        ErrorMessage="กรุณากรอกคำตอบข้อ1.5"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" 
	                    runat="server" 
                        ControlToValidate="txtAnswerQuestion3_5" 
	                    ErrorMessage="ห้ามเกิน 1500 ตัวอักษร"
                         Display="Dynamic"
                         SetFocusOnError="true"
                         CssClass="question-error"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
	                </asp:RegularExpressionValidator>
                    </p>

            </div>
        </div>
    </div>
    <div style="margin:5px 5px 5px 5px; text-align:center;">
    <asp:Button ID="btnNextToQ2_2" runat="server" Text="บันทึกข้อมูลและดำเนินงานต่อไป" onclick="btnNextToQ2_2_Click" />
    </div>
    </div>
  </asp:Panel>

   <asp:Panel ID="panel2" runat="server" Visible="false">
    <div class="box-question">

   <div style="text-align:left; margin-top:15px;">
        <div style="color:Red; font-size:20px;" class="QL1"><asp:Literal ID="lblQuestion2" runat="server"></asp:Literal></div>
        <div style="color:blue; font-size:18px;" class="QL2"><asp:Literal id="lblAssumption2" runat="server"></asp:Literal></div>
        <div style="margin-left:15px; margin-top:15px;">
                <asp:Literal ID="lblQuestion2_2" runat="server"></asp:Literal>  
                <asp:RequiredFieldValidator 
                ID="RequiredFieldValidator6" 
                runat="server" 
                 Text="*"
                  ErrorMessage="กรุณาเลือกมี หรือ ไม่มี"
                   SetFocusOnError="true"
                    Display="Dynamic"
                    CssClass="ErrorDokJan"
                    ControlToValidate="radQ2"></asp:RequiredFieldValidator><br />

                <asp:RadioButtonList ID="radQ2" runat="server"  
                    RepeatDirection="Horizontal" 
                     AutoPostBack="true"
                    onselectedindexchanged="rad2_SelectedIndexChanged">
                    <asp:ListItem Value="มี">มี</asp:ListItem>
                    <asp:ListItem Value="ไม่มี">ไม่มี</asp:ListItem>
                </asp:RadioButtonList>


              

            <div style="margin-left:10px">
        
               ถ้าตอบว่า "มี" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้

            <p>
                <span class="QL3"><asp:Literal ID="lblQuestion3_6" runat="server"></asp:Literal></span><br />
                <asp:TextBox ID="txtAnswerQuestion3_6" CssClass="TextArea" runat="server"  TextMode="MultiLine" Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                     <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator7" 
                        runat="server" 
                        ControlToValidate="txtAnswerQuestion3_6" 
                        Text="*" 
                         Display="Dynamic"
                          SetFocusOnError="true"
                          CssClass="ErrorDokJan"
                        ErrorMessage="กรุณากรอกคำตอบข้อ2.1"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" 
	                    runat="server" 
                        ControlToValidate="txtAnswerQuestion3_6" 
	                    ErrorMessage="ห้ามเกิน 1500 ตัวอักษร"
                         Display="Dynamic"
                         SetFocusOnError="true"
                         CssClass="question-error"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
	                </asp:RegularExpressionValidator>
                    </p>

                <p>
               <span class="QL3"><asp:Literal ID="lblQuestion3_7" runat="server"></asp:Literal></span><br />
                <asp:TextBox ID="txtAnswerQuestion3_7" CssClass="TextArea" runat="server"  TextMode="MultiLine" Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                     <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator8" 
                        runat="server" 
                        ControlToValidate="txtAnswerQuestion3_7" 
                        Text="*" 
                         Display="Dynamic"
                          SetFocusOnError="true"
                          CssClass="ErrorDokJan"
                        ErrorMessage="กรุณากรอกคำตอบข้อ2.2"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" 
	                    runat="server" 
                        ControlToValidate="txtAnswerQuestion3_7" 
	                    ErrorMessage="ห้ามเกิน 1500 ตัวอักษร"
                         Display="Dynamic"
                         SetFocusOnError="true"
                         CssClass="question-error"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
	                </asp:RegularExpressionValidator>
                </p>

            <p>
              <span class="QL3"><asp:Literal ID="lblQuestion3_8" runat="server"></asp:Literal></span><br />
                <asp:TextBox ID="txtAnswerQuestion3_8" runat="server" CssClass="TextArea"  TextMode="MultiLine" Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                     <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator9" 
                        runat="server" 
                        ControlToValidate="txtAnswerQuestion3_8" 
                        Text="*" 
                         Display="Dynamic"
                          SetFocusOnError="true"
                         CssClass="ErrorDokJan"
                        ErrorMessage="กรุณากรอกคำตอบข้อ2.3"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" 
	                    runat="server" 
                        ControlToValidate="txtAnswerQuestion3_8" 
	                    ErrorMessage="ห้ามเกิน 1500 ตัวอักษร"
                         Display="Dynamic"
                         SetFocusOnError="true"
                         CssClass="question-error"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
	                </asp:RegularExpressionValidator>
                    </p>

             <div style="margin:5px 5px 5px 5px; text-align:center;">
                <asp:Button ID="btnNext" runat="server" Text="บันทึกข้อมูลและดำเนินงานต่อไป" onclick="btnNext_Click" />
            </div>


            </div>
        </div>
    </div>

    </div>
   </asp:Panel>

<asp:Literal ID="litfinish" runat="server" Visible="false"></asp:Literal><br />
<asp:Button ID="btnToQB" runat="server" onclick="btnToQB_Click" Visible="false"  Text="ดำเนินการต่อไป"/>


<asp:SqlDataSource runat="server" ID="SqlDataSource1" 
ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"></asp:SqlDataSource>

<asp:SqlDataSource runat="server" ID="SqlDataSource2" 
ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"></asp:SqlDataSource>




</asp:Content>


