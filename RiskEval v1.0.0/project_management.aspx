<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="project_management.aspx.cs" Inherits="project_management" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล จัดการโครงการ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" Runat="Server">
    จัดการโครงการในระบบวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


 <div>
         <div class="accountInfo">
                <fieldset class="login">
                    <legend class="bold1">ค้นหาโครงการ</legend>
                    <table style="margin:0px 0px 0px 0px;" >
                         <tr>
                            <td>เลือกประเภทโครงการ
                                <asp:RadioButtonList ID="radProjectType" runat="server">
                                    <asp:ListItem Text="โครงการบันทึกเพื่อส่งผลวิเคราะห์" Value="real" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="โครงการจำลองการกรอกข้อมูล" Value="sim"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                         </tr>
                         <tr>
                            <td>เลือกหน่วยงาน
                                <asp:DropDownList ID="ddlDept"
                                  runat="server"
                                    DataSourceID="SqlDataSource2"
                                     DataTextField="d_name"
                                      DataValueField="d_id" 
                                       AutoPostBack="true"
                                      onselectedindexchanged="ddlDept_SelectedIndexChanged">
                                  </asp:DropDownList>
                            </td>
                        </tr>
                    </table>     
                </fieldset>
            </div>

             <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                SelectCommand="select id, d_id, d_name, d_code, mi_id, (d_id + ' - ' + d_name) as depttext from department order by d_name"
                  ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
               </asp:SqlDataSource>

        <div style="margin-top:10px">
        <asp:GridView 
            DataKeyNames="pj_id" 
            ID="GridView1" 
            runat="server" 
            AutoGenerateColumns="False" 
            OnRowCommand="GridView1_RowCommand" 
            OnRowDataBound="GridView1_RowDataBound" 
            OnRowDeleting="GridView1_RowDeleting"
            CssClass="grid"
            EmptyDataText="ไม่มีข้อมูล" 
            EmptyDataRowStyle-CssClass="warning"
            ShowFooter="True"
            AllowPaging="true"
            AllowSorting="true"
            PageSize="10"
            OnPageIndexChanging="GridView1_PageIndexChanging" 
            OnSorting="GridView1_Sorting">
              <Columns>
            
               <asp:TemplateField HeaderText="ลบ">
                 <ItemTemplate>
                   <asp:LinkButton ID="LinkButton1" 
                     CommandArgument='<%# Eval("pj_id") %>' 
                     CommandName="Delete" runat="server">
                     ลบโครงการ</asp:LinkButton>
                 </ItemTemplate>
               </asp:TemplateField>

               <asp:BoundField DataField="pj_code" HeaderText="รหัสโครงการ"/>
               <asp:BoundField DataField="pj_name" HeaderText="ชื่อโครงการ" />
               <asp:BoundField DataField="pj_category" HeaderText="ลักษณะโครงการ" />
               <asp:BoundField DataField="d_id" HeaderText="รหัสหน่วยงาน" />
               <asp:BoundField DataField="pj_complete_status" HeaderText="สถานะการส่งผล" NullDisplayText="-"/>
               <asp:BoundField DataField="pj_approval_status" HeaderText="สถานะการอนุมัติ" NullDisplayText="-"/>
               <asp:BoundField DataField="pj_lastupdate" HeaderText="วันที่แก้ไขครั้งสุดท้าย" />

              </Columns>

               <FooterStyle Cssclass="gridFooter" />
        <HeaderStyle CssClass="gridHeader" />
        <PagerStyle CssClass="gridPager" HorizontalAlign="Center" />
        <RowStyle  CssClass="gridRow" />
      
            </asp:GridView>
        
        </div>
        
      
     <asp:SqlDataSource ID="SqlDataSource4" runat="server"  ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"></asp:SqlDataSource>



    </div>
</asp:Content>

