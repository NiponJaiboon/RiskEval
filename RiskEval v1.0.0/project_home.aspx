<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="project_home.aspx.cs" Inherits="project_home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ของแผนงาน/โครงการ หน้าหลัก
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" runat="Server">
    หน้าหลัก
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-left">
        <div class="title">
            หน้าหลัก</div>
        <ul>
            <li class="style1"><a href="project_intro.aspx?status=real"><strong>บันทึกการประเมินโครงการเพื่อประกอบคำของบประมาณ</strong></a></li>
            <li class="style1"><a href="project_intro.aspx?status=sim"><strong>ระบบจำลองการกรอกข้อมูลเพื่อเรียนรู้ก่อนการบันทึกจริง</strong></a></li>
        </ul>
    </div>
    <div class="content-right">
        <div class="box">
            <div class="box-header">
            </div>
            <div class="box-body">
                <div>
                    <p class="title">คู่มือการใช้งานระบบ</p>
                    <ul>
                        <li><a href="document/1คู่มือการใช้โปรแกรมของส่วนราชการ.pdf">คู่มือการใช้งานการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</a></li>
                        <li><a href="document/2คู่มือส่วนราชการ.pdf">คู่มือการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล สำหรับคำของบประมาณ</a></li>
                        <li><a href="document/3แบบฟอร์มประกอบแนวทางการตอบแบบสอบถาม.docx">แบบฟอร์มประกอบแนวทางการตอบแบบสอบถาม</li>
                    </ul>
                </div>
            </div>
            <div class="box-footer">
            </div>
        </div>
    </div>
</asp:Content>