<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามแผนงาน/โครงการ
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="linkheader" runat="Server">
    <style type="text/css">
        .leftnav
        {
            list-style: none outside none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" runat="Server">
    <asp:Literal ID="ltTitle" runat="server">เข้าสู่ระบบ</asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-left">
        <asp:Panel ID="pnl_register" runat="server">
            <div id="loginPanel" runat="server" visible="true">
                <div class="title">
                    ระบุชื่อผู้ใช้ และรหัสผ่าน</div>
                <div class="separate">
                </div>
                <table id="tblogin">
                    <tr>
                        <td class="first" valign="top">
                            <label>
                                เลขบัตรประชาชน</label>
                        </td>
                        <td class="second">
                            <asp:TextBox ID="txtIdCard" runat="server" Width="200px" MaxLength="13"></asp:TextBox><br />
                            <asp:RegularExpressionValidator ID="vld_idno" runat="server" ControlToValidate="txtIdCard"
                                ErrorMessage="* เลขบัตรประชาชนไม่ถูกต้อง" ValidationExpression="^\d{13}" ValidationGroup="register"
                                SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rvld_idno" runat="server" ControlToValidate="txtIdCard"
                                ErrorMessage="* กรุณาใส่เลขบัตรประชาชน 13 หลัก" ValidationGroup="register" SetFocusOnError="True"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="first" valign="top">
                            <label>
                                ชื่อ (ภาษาอังกฤษ)</label>
                        </td>
                        <td class="second">
                            <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                            <br />
                            <span class="comment">กรอกเฉพาะชื่อ(ภาษาอังกฤษ)เท่านั้น</span>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtName"
                                ErrorMessage="* กรุณาใส่ชื่อภาษาอังกฤษ" ValidationGroup="register" SetFocusOnError="True"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="vld_idno3" runat="server" ControlToValidate="txtName"
                                ErrorMessage="* ชื่อภาษาอังกฤษไม่ถูกต้อง" ValidationExpression="[a-zA-Z\s]+"
                                ValidationGroup="register" SetFocusOnError="True" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="first" valign="top">
                            <label>
                                สถานะผู้ใช้</label>
                        </td>
                        <td class="second">
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="200">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlstatus"
                                ErrorMessage="* กรุณาเลือกสถานะผูัใช้" InitialValue="ระบุสถานะผู้ใช้" ValidationGroup="register"
                                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="first">
                        </td>
                        <td class="second">
                            <asp:Button ID="btnLogin" runat="server" Text="เข้าสู่ระบบ" CssClass="button" ValidationGroup="register"
                                OnClick="btnLogin_Click" />
                        </td>
                    </tr>
                </table>
                <div class="separate">
                </div>
                <ul>
                    <li><a href="register.aspx">ลงทะเบียน (ส่วนราชการ รัฐวิสาหกิจ หน่วยงานอื่นของรัฐ จังหวัด
                        และกลุ่มจังหวัด)</a></li>
                    <li><a href="register_staff.aspx">ลงทะเบียน (สำนักงบประมาณ)</a></li>
                </ul>
                <div class="help-manual">
                    <p class="title">
                        คู่มือการใช้งานระบบ</p>
                    <ul>
                        <li><a href="document/1คู่มือการใช้โปรแกรมของส่วนราชการ.pdf">คู่มือการใช้งานการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</a></li>
                        <li><a href="document/2คู่มือส่วนราชการ.pdf">คู่มือการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาลสำหรับคำของบประมาณ</a></li>
                        <li><a href="document/3แบบฟอร์มประกอบแนวทางการตอบแบบสอบถาม.docx">แบบฟอร์มประกอบแนวทางการตอบแบบสอบถาม</a></li>
                    </ul>
                </div>
            </div>
        </asp:Panel>
        <div id="MenuPanel" runat="server" visible="false">
            <asp:Literal ID="ltLeftmenu" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="content-right">
        <div class="box">
            <div class="box-header">
            </div>
            <div class="box-body">
                <div>
                    <p class="title">
                        ประกาศสำนักประเมินผล</p>
                    <asp:Literal ID="ltannounce" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="box-footer">
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //        $(document).ready(function () {
        //            var result = '<%=msg%>';
        //            if (result != "") { alert(result); }
        //        });

    </script>
</asp:Content>
