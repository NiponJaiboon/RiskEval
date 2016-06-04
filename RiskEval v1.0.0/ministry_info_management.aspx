<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="ministry_info_management.aspx.cs" Inherits="ministry_info_management" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล จัดการข้อมูลกระทรวง
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" Runat="Server">
    จัดการข้อมูลกระทรวง
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


 <div>
         <div class="accountInfo">
                <fieldset class="login">
                    <legend class="bold1">จัดการข้อมูลกระทรวง</legend>
                 
                    <table style="margin:0px 0px 0px 0px;" >
                         <tr>
                            <td>รหัสกระทรวง</td>
                            <td><asp:TextBox ID="txtMCode" runat="server" MaxLength="25"></asp:TextBox>  
                            </td>
                         </tr>
                         <tr>
                            <td>ชื่อกระทรวง</td>
                             <td><asp:TextBox ID="txtMName" runat="server" MaxLength="450" Width="450"></asp:TextBox>
                             </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td ><asp:Button ID="btnSave" runat="server"  Text="บันทึกการแก้ไข" onclick="btnSave_Click"/>
                            <asp:Button ID="btnAdd" runat="server" Text="เพิ่มเป็นกระทรวงใหม่" 
                                    onclick="btnAdd_Click" />

                            <asp:Button ID="btnCancel" runat="server" Text="ล้างข้อมูล" 
                                    onclick="btnCancel_Click" />

                                <asp:Label id="lblResult" runat="server" CssClass="question-error"></asp:Label>
                                    </td>
                        </tr>
                    </table>     
                </fieldset>

               

                <asp:HiddenField ID="hddmiid" runat="server" />
            </div>

             <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                SelectCommand="select mi_id, mi_code, mi_name from ministry order by mi_code"
                  ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
               </asp:SqlDataSource>

        <div style="margin-top:10px">
        <asp:GridView 
            DataKeyNames="mi_id" 
            ID="GridView1" 
            runat="server" 
            AutoGenerateColumns="False" 
            OnRowCommand="GridView1_RowCommand" 
            OnRowDataBound="GridView1_RowDataBound" 
            OnRowDeleting="GridView1_RowDeleting"
            OnRowUpdating="GridView1_RowUpdating"
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
                     CommandArgument='<%# Eval("mi_id") %>' 
                     CommandName="Update" runat="server">
                     แก้ไข</asp:LinkButton>
                 </ItemTemplate>
               </asp:TemplateField>

               <asp:BoundField DataField="mi_code" HeaderText="รหัสกระทรวง"/>
               <asp:BoundField DataField="mi_name" HeaderText="ชื่อกระทรวง" />
    
              </Columns>

        <FooterStyle Cssclass="gridFooter" />
        <HeaderStyle CssClass="gridHeader" />
        <PagerStyle CssClass="gridPager" HorizontalAlign="Center" />
        <RowStyle  CssClass="gridRow" />
      
            </asp:GridView>
        
        </div>
        
 



    </div>
</asp:Content>
