<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="project_pickquestion.aspx.cs" Inherits="project_pickquestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
การวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามแผนงาน/โครงการ เลือกชุดคำถาม
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" Runat="Server">
    เลือกชุดคำถาม
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

             <div class="box">
                        <div class="box-header"></div>
                        <div class="box-body">
                            <div>
                            <p class="title">เลือกชุดคำถาม</p> 
                
                                <div>
                                   
                                      <p><asp:HyperLink ID="H1" runat="server" Text="คำถาม ชุด ก: ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น" NavigateUrl="~/question_set_A.aspx" CssClass="bold1"></asp:HyperLink><br />
                                       <asp:Literal ID="Lit1" runat="server"></asp:Literal></p>
          
                                      <p><asp:HyperLink ID="H2" runat="server" Text="คำถาม ชุด ข: การวิเคราะห์และวางแผนรายละเอียดโครงการ" NavigateUrl="~/question_set_B.aspx" CssClass="bold1"></asp:HyperLink><br />
                                      <asp:Literal ID="Lit2" runat="server"></asp:Literal></p>

                                      <p><asp:HyperLink ID="H3" runat="server" Text="คำถาม ชุด ค: การจัดลำดับและจัดสรรงบประมาณโครงการ  " NavigateUrl="~/question_set_C.aspx" CssClass="bold1"></asp:HyperLink><br />
                                        <asp:Literal ID="Lit3" runat="server"></asp:Literal></p>

                                      <p><asp:HyperLink ID="H4" runat="server" Text="คำถาม ชุด ง: การเตรียมการเพื่อติดตามความก้าวหน้าของการดำเนินโครงการ" NavigateUrl="~/question_set_D.aspx" CssClass="bold1"></asp:HyperLink><br />
                                       <asp:Literal ID="Lit4" runat="server"></asp:Literal></p>

                                      <p><asp:HyperLink ID="H5" runat="server" Text="คำถาม ชุด จ: การประเมินผลการดำเนินงานของโครงการต่อเนื่องและโครงการที่ทำเสร็จแล้วและต้องการขยายผลโครงการ" NavigateUrl="~/question_set_E.aspx" CssClass="bold1"></asp:HyperLink><br />
                                       <asp:Literal ID="Lit5" runat="server"></asp:Literal></p>

                                       <p><asp:HyperLink ID="HFactor" 
                                       runat="server" 
                                       Text="คำถาม วิเคราะห์ความเสี่ยงสภาพแวดล้อมภายในภายนอก" 
                                       NavigateUrl="~/factor_risk.aspx" 
                                       Enabled="false"
                                       CssClass="bold1"></asp:HyperLink></p>
                                     
                                </div>  
                            </div>               
                        </div>
                        <div class="box-footer"></div>
                    </div>


                         <asp:SqlDataSource 
                            ID="SqlDataSource4" 
                            runat="server" 
                              ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>">
                           </asp:SqlDataSource>
   

</asp:Content>

