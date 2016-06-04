<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="question_set_D_edit.aspx.cs" Inherits="question_set_D_edit" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามแผนงาน/โครงการ คำถามชุด ง
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" runat="Server">
    คำถามชุด ง: การเตรียมการเพื่อติดตามความก้าวหน้าของการดำเนินโครงการ
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="panel1" runat="server" Visible="true">

     <div class="box-question">
        <div style="text-align: left; margin-top: 10px;">
            <div style="color: Red; font-size: 20px;" class="QL1">
                <asp:Literal ID="lblQuestion7" runat="server"></asp:Literal></div>
<div style="display:none">
            <div style="color: blue; font-size: 18px;" class="QL2">
                <asp:Literal ID="lblAssumption7" runat="server"></asp:Literal></div></div>

            <asp:Panel ID="pnl7_1" runat="server">
                <div style="margin-left: 15px; margin-top: 15px;">
                    <asp:Literal ID="lblQuestion7_1" runat="server"></asp:Literal>
                    <asp:RequiredFieldValidator ID="reqRad1" runat="server" Text="*" ErrorMessage="กรุณาเลือกใช่ หรือ ไม่ใช่" CssClass="ErrorDokJan" 
                        SetFocusOnError="true" Display="Dynamic" ControlToValidate="radanswer7_1"></asp:RequiredFieldValidator><br />
                    <asp:RadioButtonList ID="radanswer7_1" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="true" OnSelectedIndexChanged="radanswer7_1_SelectedIndexChanged">
                        <asp:ListItem Value="มี">มี</asp:ListItem>
                        <asp:ListItem Value="ไม่มี">ไม่มี</asp:ListItem>
                    </asp:RadioButtonList>
                    <div style="margin-left: 10px">
                        ถ้าตอบว่า "มี" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion7_1_1" runat="server"></asp:Literal></span><br />
                            <asp:TextBox ID="txtAnswerQuestion7_1_1" CssClass="TextArea" runat="server" TextMode="MultiLine"
                                Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAnswerQuestion7_1_1" CssClass="ErrorDokJan" 
                                Text="*" Display="Dynamic" SetFocusOnError="true" ErrorMessage="กรุณากรอกคำตอบข้อ1.1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAnswerQuestion7_1_1"
                                ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" Display="Dynamic" SetFocusOnError="true" CssClass="question-error" 
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
                            </asp:RegularExpressionValidator>
                        </p>
                         <div style="margin: 5px 5px 5px 5px; text-align: center;">
                        <asp:Button ID="btnToQ7_2" runat="server" OnClick="btnToQ7_2_Click" Text="บันทึกและดำเนินการต่อไป"
                        OnClientClick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnl7_2" runat="server">
                <div style="margin-left: 15px; margin-top: 15px;">
                    <asp:Literal ID="lblQuestion7_2" runat="server"></asp:Literal>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="*" CssClass="ErrorDokJan"
                        ErrorMessage="กรุณาเลือกใช่ หรือ ไม่ใช่" SetFocusOnError="true" Display="Dynamic"
                        ControlToValidate="radanswer7_2"></asp:RequiredFieldValidator><br />
                    <asp:RadioButtonList ID="radanswer7_2" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="true" OnSelectedIndexChanged="radanswer7_2_SelectedIndexChanged">
                        <asp:ListItem Value="มี">มี</asp:ListItem>
                        <asp:ListItem Value="ไม่มี">ไม่มี</asp:ListItem>
                    </asp:RadioButtonList>
                    <div style="margin-left: 10px">
                        ถ้าตอบว่า "มี" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion7_2_1" runat="server"></asp:Literal></span><br />
                            <asp:TextBox ID="txtAnswerQuestion7_2_1" CssClass="TextArea" runat="server" TextMode="MultiLine"
                                Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAnswerQuestion7_2_1" CssClass="ErrorDokJan" 
                                Text="*" Display="Dynamic" SetFocusOnError="true" ErrorMessage="กรุณากรอกคำตอบข้อ2.1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAnswerQuestion7_2_1"
                                ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" Display="Dynamic" SetFocusOnError="true" CssClass="question-error" 
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
                            </asp:RegularExpressionValidator>
                        </p>
                        <div style="margin: 5px 5px 5px 5px; text-align: center;">
                        <asp:Button ID="btnToQ7_3" runat="server" OnClick="btnToQ7_3_Click" Text="บันทึกและดำเนินการต่อไป" 
                        OnClientClick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}"/>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnl7_3" runat="server">
                <div style="margin-left: 15px; margin-top: 15px;">
                    <asp:Literal ID="lblQuestion7_3" runat="server"></asp:Literal>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="*"
                        ErrorMessage="กรุณาเลือกใช่ หรือ ไม่ใช่" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorDokJan"
                        ControlToValidate="radanswer7_3"></asp:RequiredFieldValidator><br />
                    <asp:RadioButtonList ID="radanswer7_3" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="true" OnSelectedIndexChanged="radanswer7_3_SelectedIndexChanged">
                        <asp:ListItem Value="มี">มี</asp:ListItem>
                        <asp:ListItem Value="ไม่มี">ไม่มี</asp:ListItem>
                    </asp:RadioButtonList>
                    <div style="margin-left: 10px">
                        ถ้าตอบว่า "มี" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion7_3_1" runat="server"></asp:Literal></span><br />
                            <asp:TextBox ID="txtAnswerQuestion7_3_1" CssClass="TextArea" runat="server" TextMode="MultiLine"
                                Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAnswerQuestion7_3_1" CssClass="ErrorDokJan"
                                Text="*" Display="Dynamic" SetFocusOnError="true" ErrorMessage="กรุณากรอกคำตอบข้อ3.1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtAnswerQuestion7_3_1"
                                ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" Display="Dynamic" SetFocusOnError="true" CssClass="question-error" 
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$"> 
                            </asp:RegularExpressionValidator>
                        </p>
                        <div style="margin: 5px 5px 5px 5px; text-align: center;">
                        <asp:Button ID="btnToQ8_4" runat="server" OnClick="btnToQ8_4_Click" Text="บันทึกและดำเนินการต่อไป" OnClientClick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}"/>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
      </div>
    </asp:Panel>

    <asp:Panel ID="panel2" runat="server" Visible="false">
     <div class="box-question">

        <div style="text-align: left; margin-top: 10px;">
            <div style="color: Red; font-size: 20px;" class="QL1">
                <asp:Literal ID="lblQuestion8" runat="server"></asp:Literal></div>

        <div style="display:none">
            <div style="color: blue; font-size: 18px;" class="QL2">
                <asp:Literal ID="lblAssumption8" runat="server"></asp:Literal></div></div>

            <asp:Panel ID="pnl8_4" runat="server">
                <div style="margin-left: 15px; margin-top: 15px;">
                    <asp:Literal ID="lblQuestion8_4" runat="server"></asp:Literal>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Text="*" CssClass="ErrorDokJan" 
                        ErrorMessage="กรุณาเลือกมี หรือ ไม่มี" SetFocusOnError="true" Display="Dynamic"
                        ControlToValidate="radanswer8_4"></asp:RequiredFieldValidator><br />
                    <asp:RadioButtonList ID="radanswer8_4" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="true" OnSelectedIndexChanged="radanswer8_4_SelectedIndexChanged">
                        <asp:ListItem Value="มี">มี</asp:ListItem>
                        <asp:ListItem Value="ไม่มี">ไม่มี</asp:ListItem>
                    </asp:RadioButtonList>
                    <div style="margin-left: 10px">
                        ถ้าตอบว่า "มี" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion8_4_1" runat="server"></asp:Literal></span><br />
                            <asp:TextBox ID="txtAnswerQuestion8_4_1" CssClass="TextArea" runat="server" TextMode="MultiLine"
                                Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAnswerQuestion8_4_1" CssClass="ErrorDokJan" 
                                Text="*" Display="Dynamic" SetFocusOnError="true" ErrorMessage="กรุณากรอกคำตอบข้อ4.1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtAnswerQuestion8_4_1" CssClass="question-error" 
                                ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" Display="Dynamic" SetFocusOnError="true"
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
                            </asp:RegularExpressionValidator>
                        </p>
                         <div style="margin: 5px 5px 5px 5px; text-align: center;">
                            <asp:Button ID="btnToQ8_5" runat="server"  OnClick="btnToQ8_5_Click" Text="บันทึกและดำเนินการต่อไป" 
                            OnClientClick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}"/>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnl8_5" runat="server">
                <div style="margin-left: 15px; margin-top: 15px;">
                    <asp:Literal ID="lblQuestion8_5" runat="server"></asp:Literal> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Text="*" CssClass="ErrorDokJan"
                        ErrorMessage="กรุณาเลือกใช่ หรือ ไม่ใช่" SetFocusOnError="true" Display="Dynamic"
                        ControlToValidate="radanswer8_5"></asp:RequiredFieldValidator><br />
                    <asp:RadioButtonList ID="radanswer8_5" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="true" OnSelectedIndexChanged="radanswer8_5_SelectedIndexChanged">
                        <asp:ListItem Value="มี">มี</asp:ListItem>
                        <asp:ListItem Value="ไม่มี">ไม่มี</asp:ListItem>
                    </asp:RadioButtonList>
                    <div style="margin-left: 10px">
                        ถ้าตอบว่า "มี" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion8_5_1" runat="server"></asp:Literal></span><br />
                            <asp:TextBox ID="txtAnswerQuestion8_5_1" CssClass="TextArea" runat="server" TextMode="MultiLine"
                                Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtAnswerQuestion8_5_1" CssClass="ErrorDokJan"
                                Text="*" Display="Dynamic" SetFocusOnError="true" ErrorMessage="กรุณากรอกคำตอบข้อ5.1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtAnswerQuestion8_5_1"
                                ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" Display="Dynamic" SetFocusOnError="true" CssClass="question-error" 
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
                            </asp:RegularExpressionValidator>
                        </p>
                    </div>
                </div>
                 <div style="margin: 5px 5px 5px 5px; text-align: center;">
                            <asp:Button ID="btnToQ9_6" runat="server"  OnClick="btnToQ9_6_Click" Text="บันทึกและดำเนินการต่อไป" 
                            OnClientClick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}"/>
                        </div>
            </asp:Panel>

           
        </div>

        </div>
    </asp:Panel>

    <asp:Panel ID="panel3" runat="server" Visible="false">

     <div class="box-question">

        <div style="text-align: left; margin-top: 10px;">
            <div style="color: Red; font-size: 20px;" class="QL1">
                <asp:Literal ID="lblQuestion9" runat="server"></asp:Literal></div>
                <div style="display:none">
            <div style="color: blue; font-size: 18px;" class="QL2">
            <asp:Literal ID="lblAssumption9" runat="server"></asp:Literal></div>
            </div>
            <div style="margin-left: 15px; margin-top: 15px;">
                <asp:Literal ID="lblQuestion9_6" runat="server"></asp:Literal>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Text="*" CssClass="ErrorDokJan" 
                    ErrorMessage="กรุณาเลือกมี หรือ ไม่มี" SetFocusOnError="true" Display="Dynamic"
                    ControlToValidate="radanswer9_6"></asp:RequiredFieldValidator><br />
                <asp:RadioButtonList ID="radanswer9_6" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="true" OnSelectedIndexChanged="radanswer9_6_SelectedIndexChanged">
                    <asp:ListItem Value="มี">มี</asp:ListItem>
                    <asp:ListItem Value="ไม่มี">ไม่มี</asp:ListItem>
                </asp:RadioButtonList>

                <asp:Panel ID="pnl9_6" runat="server">
                    <div style="margin-left: 10px">
                        ถ้าตอบว่า "มี" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion9_6_1" runat="server"></asp:Literal></span><br />
                            <asp:TextBox ID="txtAnswerQuestion9_6_1" CssClass="TextArea" runat="server" TextMode="MultiLine"
                                Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtAnswerQuestion9_6_1" CssClass="ErrorDokJan"
                                Text="*" Display="Dynamic" SetFocusOnError="true" ErrorMessage="กรุณากรอกคำตอบข้อ6.1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtAnswerQuestion9_6_1"
                                ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" Display="Dynamic" SetFocusOnError="true" CssClass="question-error" 
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
                            </asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion9_6_2" runat="server"></asp:Literal></span><br />
                            <asp:TextBox ID="txtAnswerQuestion9_6_2" CssClass="TextArea" runat="server" TextMode="MultiLine"
                                Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtAnswerQuestion9_6_2" CssClass="ErrorDokJan"
                                Text="*" Display="Dynamic" SetFocusOnError="true" ErrorMessage="กรุณากรอกคำตอบข้อ6.1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtAnswerQuestion9_6_2"
                                ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" Display="Dynamic" SetFocusOnError="true" CssClass="question-error" 
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
                            </asp:RegularExpressionValidator>
                        </p>
                    </div>
                    <div style="margin: 5px 5px 5px 5px; text-align: center;">
                        <asp:Button ID="btnToSetE" runat="server" Text="บันทึกข้อมูลและดำเนินงานต่อไป" OnClick="btnToSetE_Click" OnClientClick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}"/>
                    </div>
                </asp:Panel>
            </div>
        </div>

        </div>
    </asp:Panel>

    <asp:Literal ID="litfinish" runat="server" Visible="false"></asp:Literal><br />
    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
    </asp:SqlDataSource>
    <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
    </asp:SqlDataSource>
</asp:Content>
