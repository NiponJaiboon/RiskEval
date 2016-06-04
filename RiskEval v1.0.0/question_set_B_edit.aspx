<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="question_set_B_edit.aspx.cs" Inherits="question_set_B" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="linkheader" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTitle" runat="Server">
    <asp:Label ID="lbl_qset_text" runat="server" Text="ชุดคำถาม"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lbl_qSet_id" runat="server" Text="2" Visible="False"></asp:Label>
    <asp:Panel ID="pnlQL1_3" runat="server" CssClass="QL1">

      <div class="box-question">

        <div style="text-align: left; margin-top: 15px;">
            <div class="text">
                ประเด็นที่&nbsp;<asp:Literal ID="lblQuestion_id_1" runat="server"></asp:Literal>&nbsp;<asp:Literal
                    ID="lblQuestion_1" runat="server"></asp:Literal></div>
            <div class="assumption">
                สมมุติฐาน&nbsp;<asp:Literal ID="lblQuestion_id_praden_1" runat="server"></asp:Literal>&nbsp;<asp:Literal
                    ID="lblAssumption_1" runat="server"></asp:Literal></div>
            <asp:Panel ID="pnlQL2_1" runat="server" CssClass="QL2">
                <div style="margin-left: 15px; margin-top: 15px;">
                    <asp:Literal ID="lblQuestion2_1" runat="server"></asp:Literal>
                    <asp:RequiredFieldValidator ID="reqRad2_1" runat="server" Text="*" ErrorMessage="กรุณาเลือกมี หรือ ไม่มี"
                        SetFocusOnError="true" Display="Dynamic" ControlToValidate="radanswer2_1" CssClass="ErrorDokJan"></asp:RequiredFieldValidator><br />
                    <asp:RadioButtonList ID="radanswer2_1" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="radanswer2_1_SelectedIndexChanged">
                        <asp:ListItem Value="มี">มี</asp:ListItem>
                        <asp:ListItem Value="ไม่มี">ไม่มี</asp:ListItem>
                    </asp:RadioButtonList>
                    <div style="margin-left: 10px">
                        ถ้าตอบว่า "มี" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion3_1_1" runat="server"></asp:Literal></span><br />
                            <asp:TextBox ID="txtAnswerQuestion3_1_1" CssClass="TextArea" runat="server" TextMode="MultiLine"
                                Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqvld_3_1_1" runat="server" ControlToValidate="txtAnswerQuestion3_1_1"
                                Text="*" Display="Dynamic" SetFocusOnError="true" ErrorMessage="กรุณากรอกคำตอบข้อ1.1"
                                CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_3_1_1" runat="server" ControlToValidate="txtAnswerQuestion3_1_1"
                                ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" Display="Dynamic" SetFocusOnError="true"
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$"></asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion3_1_2" runat="server"></asp:Literal></span><br />
                            <asp:TextBox ID="txtAnswerQuestion3_1_2" CssClass="TextArea" runat="server" TextMode="MultiLine"
                                Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqvld_3_1_2" runat="server" ControlToValidate="txtAnswerQuestion3_1_2"
                                Text="*" Display="Dynamic" SetFocusOnError="true" ErrorMessage="กรุณากรอกคำตอบข้อ1.2"
                                CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_3_1_2" runat="server" ControlToValidate="txtAnswerQuestion3_1_2"
                                ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" Display="Dynamic" SetFocusOnError="true"
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
                            </asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion3_1_3" runat="server"></asp:Literal></span><br />
                            <asp:TextBox ID="txtAnswerQuestion3_1_3" runat="server" CssClass="TextArea" TextMode="MultiLine"
                                Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqvld_3_1_3" runat="server" ControlToValidate="txtAnswerQuestion3_1_3"
                                Text="*" Display="Dynamic" SetFocusOnError="true" ErrorMessage="กรุณากรอกคำตอบข้อ1.3"
                                CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_3_1_3" runat="server" ControlToValidate="txtAnswerQuestion3_1_3"
                                ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" Display="Dynamic" SetFocusOnError="true"
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
                            </asp:RegularExpressionValidator>
                        </p>
                    </div>
                </div>
                <div style="margin: 5px 5px 5px 5px; text-align: center;">
                    <asp:Button ID="btnSaveQL2_1" runat="server" Text="บันทึกข้อมูลและดำเนินงานต่อไป"
                        OnClientClick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}"
                        CommandArgument="3.2.1" OnClick="btnSaveQL1" />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlQL2_2" runat="server" CssClass="QL2">
                <div style="margin-left: 15px; margin-top: 15px;">
                    <asp:Literal ID="lblQuestion2_2" runat="server"></asp:Literal>
                    <asp:RequiredFieldValidator ID="reqRad2_2" runat="server" ControlToValidate="radanswer2_2"
                        Display="Dynamic" ErrorMessage="กรุณาเลือกมี หรือ ไม่มี" SetFocusOnError="true"
                        Text="*" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RadioButtonList ID="radanswer2_2" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="radanswer2_2_SelectedIndexChanged">
                        <asp:ListItem Value="มี">มี</asp:ListItem>
                        <asp:ListItem Value="ไม่มี">ไม่มี</asp:ListItem>
                    </asp:RadioButtonList>
                    <div style="margin-left: 10px">
                        ถ้าตอบว่า &quot;มี&quot; โปรดให้รายละเอียดกิจกรรมที่ดำเนินการต่อไปนี้
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion3_2_1" runat="server"></asp:Literal>
                            </span>
                            <br />
                            <asp:TextBox ID="txtAnswerQuestion3_2_1" runat="server" Columns="70" CssClass="TextArea"
                                MaxLength="1500" Rows="7" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqvld_3_2_1" runat="server" ControlToValidate="txtAnswerQuestion3_2_1"
                                Display="Dynamic" ErrorMessage="กรุณากรอกคำตอบข้อ2.1" SetFocusOnError="true"
                                Text="*" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_3_2_1" runat="server" ControlToValidate="txtAnswerQuestion3_2_1"
                                Display="Dynamic" ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" SetFocusOnError="true"
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$"></asp:RegularExpressionValidator>
                        </p>
                    </div>
                </div>
                <div style="margin: 5px 5px 5px 5px; text-align: center;">
                    <asp:Button ID="Button1" runat="server" Text="บันทึกข้อมูลและดำเนินงานต่อไป" OnClientClick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}"
                        CommandArgument="3.2.2" OnClick="btnSaveQL1" />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlQL2_3" runat="server" CssClass="QL2">
                <div style="margin-left: 15px; margin-top: 15px;">
                    <asp:Literal ID="lblQuestion2_3" runat="server"></asp:Literal>
                    <asp:RequiredFieldValidator ID="reqRad2_3" runat="server" ControlToValidate="radanswer2_3"
                        Display="Dynamic" ErrorMessage="กรุณาเลือกมี หรือ ไม่มี" SetFocusOnError="true"
                        Text="*" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RadioButtonList ID="radanswer2_3" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="radanswer2_3_SelectedIndexChanged">
                        <asp:ListItem Value="มี">มี</asp:ListItem>
                        <asp:ListItem Value="ไม่มี">ไม่มี</asp:ListItem>
                    </asp:RadioButtonList>
                    <div style="margin-left: 10px">
                        ถ้าตอบว่า &quot;มี&quot; โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion3_3_1" runat="server"></asp:Literal>
                            </span>
                            <br />
                            <asp:TextBox ID="txtAnswerQuestion3_3_1" runat="server" Columns="70" CssClass="TextArea"
                                MaxLength="1500" Rows="7" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqvld_3_3_1" runat="server" ControlToValidate="txtAnswerQuestion3_3_1"
                                Display="Dynamic" ErrorMessage="กรุณากรอกคำตอบข้อ3.1" SetFocusOnError="true"
                                Text="*" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_3_3_1" runat="server" ControlToValidate="txtAnswerQuestion3_3_1"
                                Display="Dynamic" ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" SetFocusOnError="true"
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$"></asp:RegularExpressionValidator>
                        </p>
                    </div>
                </div>
                <div style="margin: 5px 5px 5px 5px; text-align: center;">
                    <asp:Button ID="Button2" runat="server" Text="บันทึกข้อมูลและดำเนินงานต่อไป" OnClientClick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}"
                        CommandArgument="3.2.3" OnClick="btnSaveQL1" />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlQL2_4" runat="server" CssClass="QL2">
                <div style="margin-left: 15px; margin-top: 15px;">
                    <asp:Literal ID="lblQuestion2_4" runat="server"></asp:Literal>
                    <asp:RequiredFieldValidator ID="reqRad2_4" runat="server" ControlToValidate="radanswer2_4"
                        Display="Dynamic" ErrorMessage="กรุณาเลือกมี หรือ ไม่มี" SetFocusOnError="true"
                        Text="*" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RadioButtonList ID="radanswer2_4" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="radanswer2_4_SelectedIndexChanged">
                        <asp:ListItem Value="มี">มี</asp:ListItem>
                        <asp:ListItem Value="ไม่มี">ไม่มี</asp:ListItem>
                    </asp:RadioButtonList>
                    <div style="margin-left: 10px">
                        ถ้าตอบว่า &quot;มี&quot; โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion3_4_1" runat="server"></asp:Literal>
                            </span>
                            <br />
                            <asp:TextBox ID="txtAnswerQuestion3_4_1" runat="server" Columns="70" CssClass="TextArea"
                                MaxLength="1500" Rows="7" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqvld_3_4_1" runat="server" ControlToValidate="txtAnswerQuestion3_4_1"
                                Display="Dynamic" ErrorMessage="กรุณากรอกคำตอบข้อ4.1" SetFocusOnError="true"
                                Text="*" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_3_4_1" runat="server" ControlToValidate="txtAnswerQuestion3_4_1"
                                Display="Dynamic" ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" SetFocusOnError="true"
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$"></asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion3_4_2" runat="server"></asp:Literal>
                            </span>
                            <br />
                            <asp:TextBox ID="txtAnswerQuestion3_4_2" runat="server" Columns="70" CssClass="TextArea"
                                MaxLength="1500" Rows="7" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqvld_3_4_2" runat="server" ControlToValidate="txtAnswerQuestion3_4_2"
                                Display="Dynamic" ErrorMessage="กรุณากรอกคำตอบข้อ4.2" SetFocusOnError="true"
                                Text="*" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_3_4_2" runat="server" ControlToValidate="txtAnswerQuestion3_4_2"
                                Display="Dynamic" ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" SetFocusOnError="true"
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$"></asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion3_4_3" runat="server"></asp:Literal>
                            </span>
                            <br />
                            <asp:TextBox ID="txtAnswerQuestion3_4_3" runat="server" Columns="70" CssClass="TextArea"
                                MaxLength="1500" Rows="7" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqvld_3_4_3" runat="server" ControlToValidate="txtAnswerQuestion3_4_3"
                                Display="Dynamic" ErrorMessage="กรุณากรอกคำตอบข้อ4.3" SetFocusOnError="true"
                                Text="*" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_3_4_3" runat="server" ControlToValidate="txtAnswerQuestion3_4_3"
                                Display="Dynamic" ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" SetFocusOnError="true"
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$"></asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion3_4_4" runat="server"></asp:Literal>
                            </span>
                            <br />
                            <asp:TextBox ID="txtAnswerQuestion3_4_4" runat="server" Columns="70" CssClass="TextArea"
                                MaxLength="1500" Rows="7" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqvld_3_4_4" runat="server" ControlToValidate="txtAnswerQuestion3_4_4"
                                Display="Dynamic" ErrorMessage="กรุณากรอกคำตอบข้อ4.4" SetFocusOnError="true"
                                Text="*" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_3_4_4" runat="server" ControlToValidate="txtAnswerQuestion3_4_4"
                                Display="Dynamic" ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" SetFocusOnError="true"
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$"></asp:RegularExpressionValidator>
                        </p>
                    </div>
                </div>
                <div style="margin: 5px 5px 5px 5px; text-align: center;">
                    <asp:Button ID="btnSaveQL1_3" runat="server" Text="บันทึกข้อมูลและดำเนินงานต่อไป"
                        OnClientClick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}"
                        CommandArgument="3.2.4" OnClick="btnSaveQL1" />
                </div>
            </asp:Panel>
        </div>

        </div>
    </asp:Panel>
    <!--ประเด็น 4-->
    <asp:Panel ID="pnlQL1_4" runat="server" CssClass="QL1">

      <div class="box-question">

        <div style="text-align: left; margin-top: 15px;">
            <div class="text">
                ประเด็นที่&nbsp;<asp:Literal ID="lblQuestion_id_2" runat="server"></asp:Literal>&nbsp;<asp:Literal
                    ID="lblQuestion_2" runat="server"></asp:Literal></div>
            <div class="assumption">
                สมมุติฐาน&nbsp;<asp:Literal ID="lblQuestion_id_praden_2" runat="server"></asp:Literal>&nbsp;<asp:Literal
                    ID="lblAssumption_2" runat="server"></asp:Literal></div>
            <asp:Panel ID="pnlQL2_5_1" runat="server" CssClass="QL2">
                <div style="margin-left: 15px; margin-top: 15px;">
                    <asp:Literal ID="lblQuestion2_5_1" runat="server"></asp:Literal>
                    <asp:RequiredFieldValidator ID="reqRad2_5_1" runat="server" Text="*" ErrorMessage="กรุณาเลือกมี หรือ ไม่มี"
                        SetFocusOnError="true" Display="Dynamic" ControlToValidate="radanswer2_5_1" CssClass="ErrorDokJan"></asp:RequiredFieldValidator><br />
                    <asp:RadioButtonList ID="radanswer2_5_1" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="radanswer2_5_1_SelectedIndexChanged">
                        <asp:ListItem Value="มี">มี หรือ มีบางส่วน</asp:ListItem>
                        <asp:ListItem Value="ไม่มี">ไม่มี</asp:ListItem>
                    </asp:RadioButtonList>
                    <div style="margin-left: 10px">
                        ถ้าตอบว่า &quot;มี&quot; หรือ &quot;มีบางส่วน&quot; โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion3_4_1_1" runat="server"></asp:Literal></span><br />
                            <asp:TextBox ID="txtAnswerQuestion3_4_1_1" CssClass="TextArea" runat="server" TextMode="MultiLine"
                                Rows="7" Columns="70" MaxLength="1500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqvld3_4_1_1" runat="server" ControlToValidate="txtAnswerQuestion3_4_1_1"
                                Text="*" Display="Dynamic" SetFocusOnError="true" ErrorMessage="กรุณากรอกคำตอบข้อ5.1"
                                CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg3_4_1_1" runat="server" ControlToValidate="txtAnswerQuestion3_4_1_1"
                                ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" Display="Dynamic" SetFocusOnError="true"
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$"></asp:RegularExpressionValidator>
                        </p>
                    </div>
                </div>
            </asp:Panel>
            <!-- ประเด็นที่ 5 -->
        </div>
        <div style="margin: 5px 5px 5px 5px; text-align: center;">
            <asp:Button ID="btnSaveQL1_4" runat="server" Text="บันทึกข้อมูลและดำเนินงานต่อไป"
                OnClientClick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}"
                CommandArgument="4" OnClick="btnSaveQL1" />
        </div>

        </div>
    </asp:Panel>
    <asp:Panel ID="pnlQL1_5" runat="server" CssClass="QL1">

      <div class="box-question">

        <div style="text-align: left; margin-top: 15px;">
            <div class="text">
                ประเด็นที่&nbsp;<asp:Literal ID="lblQuestion_id_3" runat="server"></asp:Literal>&nbsp;<asp:Literal
                    ID="lblQuestion_3" runat="server"></asp:Literal></div>
            <div class="assumption">
                สมมุติฐาน&nbsp;<asp:Literal ID="lblQuestion_id_praden_3" runat="server"></asp:Literal>&nbsp;<asp:Literal
                    ID="lblAssumption_3" runat="server"></asp:Literal></div>
            <asp:Panel ID="pnlQL2_6_1" runat="server" CssClass="QL2">
                <div style="margin-left: 15px; margin-top: 15px;">
                    <asp:Literal ID="lblQuestion2_6_1" runat="server"></asp:Literal>
                    <asp:RequiredFieldValidator ID="reqRad2_6_1" runat="server" ControlToValidate="radanswer2_6_1"
                        Display="Dynamic" ErrorMessage="กรุณาเลือกมี หรือ ไม่มี" SetFocusOnError="true"
                        Text="*" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RadioButtonList ID="radanswer2_6_1" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="True" OnSelectedIndexChanged="radanswer2_6_1_SelectedIndexChanged">
                        <asp:ListItem Value="มี">มี</asp:ListItem>
                        <asp:ListItem Value="ไม่มี">ไม่มี</asp:ListItem>
                    </asp:RadioButtonList>
                    <div style="margin-left: 10px">
                        ถ้าตอบว่า &quot;มี&quot; โปรดให้รายละเอียดกิจกรรมที่ดำเนินการต่อไปนี้
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion3_5_1_1" runat="server"></asp:Literal>
                            </span>
                            <br />
                            <asp:TextBox ID="txtAnswerQuestion3_5_1_1" runat="server" Columns="70" CssClass="TextArea"
                                MaxLength="1500" Rows="7" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqvld_3_5_1_1" runat="server" ControlToValidate="txtAnswerQuestion3_5_1_1"
                                Display="Dynamic" ErrorMessage="กรุณากรอกคำตอบข้อ6.1" SetFocusOnError="true"
                                Text="*" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_3_5_1_1" runat="server" ControlToValidate="txtAnswerQuestion3_5_1_1"
                                Display="Dynamic" ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" SetFocusOnError="true"
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$"></asp:RegularExpressionValidator>
                        </p>
                        <p>
                            <span class="QL3">
                                <asp:Literal ID="lblQuestion3_5_1_2" runat="server"></asp:Literal>
                            </span>
                            <br />
                            <asp:TextBox ID="txtAnswerQuestion3_5_1_2" runat="server" Columns="70" CssClass="TextArea"
                                MaxLength="1500" Rows="7" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqvld_3_5_1_2" runat="server" ControlToValidate="txtAnswerQuestion3_5_1_2"
                                Display="Dynamic" ErrorMessage="กรุณากรอกคำตอบข้อ6.2" SetFocusOnError="true"
                                Text="*" CssClass="ErrorDokJan"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reg_3_5_1_2" runat="server" ControlToValidate="txtAnswerQuestion3_5_1_2"
                                Display="Dynamic" ErrorMessage="ห้ามเกิน 1500 ตัวอักษร" SetFocusOnError="true"
                                ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$"></asp:RegularExpressionValidator>
                        </p>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div style="margin: 5px 5px 5px 5px; text-align: center;">
            <asp:Button ID="btnSaveQL1_5" runat="server" Text="บันทึกข้อมูลและดำเนินงานต่อไป"
                OnClientClick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}"
                CommandArgument="5" OnClick="btnSaveQL1" />
        </div>

        </div>
    </asp:Panel>
    <asp:Literal ID="litfinish" runat="server" Visible="false"></asp:Literal>
    <br />
    <asp:Button ID="btnToQB" runat="server" OnClick="btnToQC_Click" Visible="false" Text="ดำเนินการต่อไป" />
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
    </asp:SqlDataSource>
</asp:Content>
