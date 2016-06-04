<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="Announce.aspx.cs" Inherits="Announce" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    ประกาศสำนักประเมินผล
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" Runat="Server">
    ประกาศสำนักประเมินผล
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="detail">
<table id="tblogin">
                <tr>
                    <td class="first"><label> หัวข้อหลัก</label></td>
                    <td class="second"><asp:TextBox ID="txtTitle" runat="server" Width="400px" MaxLength="500"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  
                    ControlToValidate="txtTitle"  ValidationGroup="register">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="first"><label> รายละเอียด</label></td>
                    <td class="second"><asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Rows="10" Width="400px"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"  
                    ControlToValidate="txtDesc"   ValidationGroup="register">*</asp:RequiredFieldValidator>

                    <br /><span class="comment">กรุณากรอกข้อมูลให้ครบก่อนการบันทึกข้อมูล</span>
                    </td>
                </tr> <%--
                <tr>
                    <td class="first"><label> สถานะข้อความ</label></td>
                    <td class="second">
                           <asp:DropDownList ID="ddlStatus" runat="server" Width="200" >
                            <asp:ListItem Value="1" Text="แสดง" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text="ไม่แสดง" ></asp:ListItem>
                           </asp:DropDownList>
                    </td>
                </tr>
               <tr>
                    <td class="first"><label> วันที่ประกาศ</label></td>
                    <td class="second">
                    </td>
                </tr>--%>
                <tr>
                    <td class="first"></td>
                    <td class="second">
                        <asp:Button ID="btnSubmit" runat="server" Text="บันทึกข้อมูล" 
                            CssClass="button" ValidationGroup="register" onclick="btnSubmit_Click" />

                            <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" 
                            CssClass="button" ValidationGroup="register" onclick="btnCancel_Click"  />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdSaveStatus" Value="" runat="server" /> <asp:HiddenField ID="hdAnnounce_id" runat="server" Value=""/>
            <div class="hr"></div>
            <asp:GridView ID="gvAnnounce" runat="server" Width="100%" CssClass="gvStyle" 
                        DataKeyNames="announce_id"
                        AutoGenerateColumns="False" AllowPaging="True" 
                        AllowSorting="True" PageSize="5" 
                        RowStyle-Font-Size="16px" HeaderStyle-Font-Size="10px" 
                        onpageindexchanging="gvAnnounce_PageIndexChanging" 
                        onrowdeleting="gvAnnounce_RowDeleting" onrowediting="gvAnnounce_RowEditing">
                        <RowStyle Font-Size="16px" />
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect" runat="server" CommandName="Edit" Text="เลือก" Visible="true"></asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" Text="ลบ"
                                        Visible="true" OnClientClick="return confirm('กรุณายืนยันการลบข้อมูล')"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100" HorizontalAlign="Center" Font-Size="20px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="หัวข้อหลัก" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblTitle" runat="server" Text='<%#(Eval("announce_title"))%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle ForeColor="White" />
                                <ItemStyle HorizontalAlign="Left" Font-Size="16pt"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รายละเอียด" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesc" runat="server" Text='<%# (Eval("announce_detail"))%>'></asp:Label>
                                    <%--<asp:HiddenField ID="hdStatus" runat="server" Value='<%# (Eval("announce_status"))%>'/>--%>
                                </ItemTemplate>
                                <HeaderStyle ForeColor="White" />
                                <ItemStyle HorizontalAlign="Left" Font-Size="16pt" />
                            </asp:TemplateField>
                            
                        </Columns>
                        <PagerStyle ForeColor="#000000" HorizontalAlign="Right" Font-Size="18pt"/>
                        <HeaderStyle BackColor="#8f8f06" ForeColor="White" Height="30"  Font-Size="16pt" HorizontalAlign="Center"/>
                        <AlternatingRowStyle BackColor="#eeeebf" />
                    </asp:GridView>
</div> 
</asp:Content>

