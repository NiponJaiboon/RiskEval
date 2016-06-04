<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="dept_info_management.aspx.cs" Inherits="dept_info_management" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล จัดการข้อมูลหน่วยงาน
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" Runat="Server">
    จัดการข้อมูลหน่วยงาน
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


 <div>
         <div class="accountInfo">
                <fieldset class="login">
                    <legend class="bold1">จัดการข้อมูลหน่วยงาน</legend>
                 
                    <table style="margin:0px 0px 0px 0px;" >
                        <tr>
                            <td>เลือกกระทรวง</td>
                            <td><asp:DropDownList ID="ddlMin" runat="server" 
                             AutoPostBack="true"
                                    onselectedindexchanged="ddlMin_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                         <tr>
                            <td>รหัสหน่วยงาน</td>
                            <td><asp:TextBox ID="txtDCode" runat="server" MaxLength="25"></asp:TextBox>  
                            </td>
                         </tr>
                         <tr>
                            <td>ชื่อหน่วยงาน</td>
                             <td><asp:TextBox ID="txtDName" runat="server" MaxLength="450" Width="450"></asp:TextBox>
                             </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td ><asp:Button ID="btnSave" runat="server"  Text="บันทึกการแก้ไข" onclick="btnSave_Click"/>
                            <asp:Button ID="btnAdd" runat="server" Text="เพิ่มเป็นหน่วยงานใหม่" 
                                    onclick="btnAdd_Click" />

                            <asp:Button ID="btnCancel" runat="server" Text="ล้างข้อมูล" 
                                    onclick="btnCancel_Click" />

                                <asp:Label id="lblResult" runat="server" CssClass="question-error"></asp:Label>
                                    </td>
                        </tr>
                    </table>     
                </fieldset>
                <asp:HiddenField ID="hdddeptid" runat="server" />
            </div>

             <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                SelectCommand="select mi_id, mi_code, mi_name from ministry order by mi_code"
                  ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
               </asp:SqlDataSource>

                    <asp:SqlDataSource ID="SqlDataSource4" runat="server"
                  ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
               </asp:SqlDataSource>

        <div style="margin-top:10px">
        <asp:GridView 
            DataKeyNames="id" 
            ID="GridView1" 
            runat="server" 
            AutoGenerateColumns="False" 
            OnRowCommand="GridView1_RowCommand" 
            OnRowDataBound="GridView1_RowDataBound" 
            OnRowDeleting="GridView1_RowDeleting"
            OnRowUpdating="GridView1_RowUpdating"
            OnPageIndexChanging="GridView1_PageIndexChanging" 
            CssClass="grid"
            EmptyDataText="ไม่มีข้อมูล" 
            EmptyDataRowStyle-CssClass="warning"
            ShowFooter="True"
            AllowPaging="true"
            AllowSorting="true"
            PageSize="30"
            OnSorting="GridView1_Sorting">
              <Columns>
            
               <asp:TemplateField HeaderText="แก้ไข">
                 <ItemTemplate>
                   <asp:LinkButton ID="LinkButton1" 
                     CommandArgument='<%# Eval("id") %>' 
                     CommandName="Update" runat="server">
                     แก้ไข</asp:LinkButton>
                 </ItemTemplate>
               </asp:TemplateField>

               <asp:BoundField DataField="mi_code" HeaderText="รหัสกระทรวง"/>
               <asp:BoundField DataField="mi_name" HeaderText="ชื่อกระทรวง" />
               <asp:BoundField DataField="d_code" HeaderText="รหัสหน่วยงาน" />
               <asp:BoundField DataField="d_name" HeaderText="ชื่อหน่วยงาน" />
    
              </Columns>

        <FooterStyle Cssclass="gridFooter" />
        <HeaderStyle CssClass="gridHeader" />
        <PagerStyle CssClass="gridPager" HorizontalAlign="Center" />
        <RowStyle  CssClass="gridRow" />
      
            </asp:GridView>
        
        </div>
        
 
 


    </div>
</asp:Content>


