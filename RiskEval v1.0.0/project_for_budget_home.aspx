<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="project_for_budget_home.aspx.cs" Inherits="project_for_budget_home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
สำนักจัดทำงบประมาณ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="linkheader" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTitle" Runat="Server">
หน้าหลัก
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <div class="content-left">
       <ul class='leftnav'>
        <li><a href="project_for_budgetor.aspx" target="_self">แสดงความคิดเห็นเพิ่มเติมจากเจ้าหน้าที่จัดทำงบประมาณ</a></li>
        <li><a href="project_for_budgetor.aspx?status=1" target="_self">บันทึกผลการพิจารณาจากรัฐสภา</a></li>
        <li><a href="project_for_budgetor.aspx?status=2" target="_self">โครงการที่ผ่านการพิจารณาจากรัฐสภา</a></li>
       </ul>
    </div>
    <div class="content-right">                 
        <div class="box">
            <div class="box-header"></div>
            <div class="box-body">
                <div>
                <p class="title">คู่มือการใช้งานระบบ</p>                 
                <ul>
                    <li><a href="#" target="_blank">คู่มืการลงทะเบียน</a></li>
                    <li><a href="คู่มือเจ้าหน้าที่สำนักงบประมาณ.pdf" target="_blank">คู่มือการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล สำหรับผู้พิจารณาคำของบประมาณ</a></li>
                    <li><a href="#" target="_blank">คู่มือการใช้งานระบบ</a></li>
                </ul>               
                </div>               
            </div>
            <div class="box-footer"></div>
        </div>
    </div>


</asp:Content>

