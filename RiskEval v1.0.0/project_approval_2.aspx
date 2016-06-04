<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="project_approval_2.aspx.cs" Inherits="project_approval_2" Culture="th-TH" UICulture="th-TH"  %>

<asp:Content ID="Content2" ContentPlaceHolderID="linkheader" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTitle" runat="Server">
    <asp:Literal ID="ltr_contentTitle" runat="server">รายงานการวิเคราะห์ความเสี่ยงตามหลักธรรมภิบาล</asp:Literal>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="accountInfo">
        <fieldset class="login">
            <legend class="bold1">ข้อมูลโครงการ</legend>
            <table style="margin: 0px 0px 0px 0px; width:100%">
                <tr>
                    <td class="bold1">
                        รหัสกระทรวง:
                    </td>
                    <td>
                        <asp:Label ID="lblDeptCode" runat="server"></asp:Label>
                    </td>
                    <td class="bold1">
                        ชื่อกระทรวง:
                    </td>
                    <td>
                        <asp:Label ID="lblDeptName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bold1">
                        รหัสหน่วยงาน:
                    </td>
                    <td>
                        <asp:Label ID="lblDivisionCode" runat="server"></asp:Label>
                    </td>
                    <td class="bold1">
                        ชื่อหน่วยงาน:
                    </td>
                    <td>
                        <asp:Label ID="lblDivisionName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bold1">
                        รหัสโครงการ:
                    </td>
                    <td>
                        <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
                    </td>
                    <td class="bold1">
                        ชื่อโครงการ:
                    </td>
                    <td>
                        <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bold1">
                        ยุทธศาสตร์การจัดสรรงบประมาณ :
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblYutasard" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bold1">
                        ปีงบประมาณ:
                    </td>
                    <td>
                        <asp:Label ID="lblYear" runat="server"></asp:Label>
                    </td>
                    <td class="bold1">
                        งบประมาณ(บาท):
                    </td>
                    <td>
                        <asp:Label ID="lblBudget" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="display:none">
                    <td colspan="2" class="bold1">
                        โครงการบูรณาการที่สำนักงบประมาณกำหนด:
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblIntegrateProject" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="display:none">
                    <td colspan="2" class="bold1">
                        หน่วยงานที่เกี่ยงข้องกับการบูรณาการ:
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblRelateDept" runat="server"></asp:Label>
                        <asp:SqlDataSource ID="sds_project_summary" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
                            SelectCommand="SELECT d.d_code, d.d_name, m.mi_code, m.mi_name, p.pj_code, p.d_id, p.pj_name, y.yut_name, p.pj_year, p.pj_budget, p.pj_integrateProject, p.pj_relateDept, p.pj_background, p.pj_urgency, p.pj_type, p.pj_approval_status,pj_doc_number,pj_date_doc_submitted  FROM projects AS p INNER JOIN department AS d ON p.d_id = d.d_id INNER JOIN ministry AS m ON p.mi_id = m.mi_id INNER JOIN yutasad AS y ON p.pj_yut_id = y.yut_id WHERE (p.pj_id = @pj_id)">
                            <SelectParameters>
                                <asp:Parameter Name="pj_id" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="bold1">
                        เลขหนังสือนำส่ง</td>
                    <td>
                        <asp:Label ID="lbl_pj_doc_no" runat="server"></asp:Label>
                    </td>
                    <td class="bold1">
                        วันที่นำส่ง</td>
                    <td>
                        <asp:Label ID="lbl_pj_date_doc_submitted" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div class="Result_Summary_Area">
        <center>
            <div class="submit-box">
                <table id="tbl_appr" runat="server">
                    <tr>
                        <td class="td_appr_text">
                            ผลการพิจารณา
                        </td>
                        <td class="td_appr_result">
                            <asp:Literal ID="lit_approval" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
                <table id="tb-sumbit" cellpadding="2" cellspacing="0">
                    <tr class="tr-submit">
                        <td class="td-text">
                            สรุปผลคะแนนการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล อยู่ในเกณฑ์ที่มีระดับความเสี่ยง
                        </td>
                        <td style="border-style: solid; width: 100px; text-align: center; border-color: #666600;">
                            <asp:Literal ID="litRisk1" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <%--<br />--%>
                    <tr style="display:none">
                        <td class="td-text">
                            สรุปผลคะแนนการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามชุดคำถามหลัก
                        </td>
                        <td style="border-style: solid; width: 100px; text-align: center; border-color: #666600;">
                            <asp:Literal ID="litRisk12" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td class="td-text">
                            สรุปผลคะแนนการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามชุดคำถามย่อย
                        </td>
                        <td style="border-style: solid; width: 100px; text-align: center; border-color: #666600;">
                            <asp:Literal ID="litRisk13" runat="server"></asp:Literal>
                        </td>
                    </tr>

                </table>
                <br />
                <table id="Table1" cellpadding="2" cellspacing="0">
                    <tr class="tr-submit">
                        <td class="td-text">
                            ผลคะแนนการวิเคราะห์ความเสี่ยงด้านสภาพแวดล้อมภายในและภายนอก อยู่ในระดับที่มีความเสี่ยง
                        </td>
                        <td style="border-style: solid; width: 100px; text-align: center; border-color: #666600;">
                            <asp:Literal ID="litRisk2" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="submit-box" style="display:none">
                <p>
                    <asp:HyperLink ID="linkreport1" runat="server" Text="รายงานที่ 1 รายงานการกลั่นกรองโครงการ"
                        NavigateUrl="project_report.aspx?reportid=3"></asp:HyperLink></p>
                <p>
                    <asp:HyperLink ID="linkreport2" runat="server" Text="รายงานที่ 2 รายงานการวิเคราะห์ความเสี่ยงตามหลักธรรมาธิบาล"
                        NavigateUrl="project_report.aspx?reportid=6"></asp:HyperLink></p>
                <p>
                    <asp:HyperLink ID="linkreport3" runat="server" Text="รายงานที่่ 3 รายงานการวิเคราะห์ความเสี่ยงด้านสภาพแวดล้อมภายในและภายนอก"
                        NavigateUrl="project_report.aspx?reportid=2"></asp:HyperLink></p>
            </div>
        </center>
        <%--<center>
            <table>
                <tr>
                    <td>
                        <asp:LinkButton ID="lbt_goto_step2" runat="server" OnClick="lbt_goto_step2_Click">พิจารณาผลการ ประเมิณความเสี่ยงตามหลักธรรมภิบาล</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </center>--%>
        <center>
            <div>
                <p class="report_gridview_head">
                    &nbsp;</p>
                <p class="report_gridview_head">
                    สรุปการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามชุดคำถามโดยรวม</p>
                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="pj_id,tm_id" DataSourceID="SqlDataSource4" EnableModelValidation="True"
                    ShowFooter="True" OnRowDataBound="GridView4_RowDataBound" CssClass="grid"
                     Width="80%" >
                    
                    <Columns>
                        <asp:BoundField DataField="pj_id" HeaderText="pj_id" ReadOnly="True" SortExpression="pj_id"
                            Visible="false" />
                        <asp:BoundField DataField="tm_id" HeaderText="tm_id" ReadOnly="True" SortExpression="tm_id"
                            Visible="false" />
                        <asp:BoundField DataField="tm_name" HeaderText="สรุปผลคะแนนแยกตามหลักธรรมาภิบาล"
                            SortExpression="tm_name" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="yes_percent" HeaderText="% ดำเนินการ" SortExpression="yes_percent" />
                        <asp:BoundField DataField="answerY" HeaderText="ดำเนินการ" SortExpression="answerY" />
                        <asp:BoundField DataField="answerN" HeaderText="ไม่ดำเนินการ" SortExpression="answerN" />
                    </Columns>
                    <EmptyDataTemplate>ไม่มีข้อมูล</EmptyDataTemplate>
                    <AlternatingRowStyle CssClass="gridAltRow" />
                    <FooterStyle CssClass="gridFooter" />
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle HorizontalAlign="Center" CssClass="gridPager" />
                    <RowStyle CssClass="gridRow" />
                </asp:GridView>
            </div>
            <%--<br />--%>

            <br />
            <div>
                <p class="report_gridview_head">
                    สรุปการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามชุดคำถามหลัก</p>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="pj_id,tm_id"
                    DataSourceID="SqlDataSource1" EnableModelValidation="True" CssClass="grid" 
                     ShowFooter="True" OnRowDataBound="GridView1_RowDataBound" Width="80%"
                    >
                    
                    <Columns>
                        <asp:BoundField DataField="pj_id" HeaderText="pj_id" ReadOnly="True" SortExpression="pj_id"
                            Visible="false" />
                        <asp:BoundField DataField="tm_id" HeaderText="tm_id" ReadOnly="True" SortExpression="tm_id"
                            Visible="false" />
                        <asp:BoundField DataField="tm_name" HeaderText="สรุปผลคะแนนแยกตามหลักธรรมาภิบาล"
                            SortExpression="tm_name" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="yes_percent" HeaderText="% ดำเนินการ" SortExpression="yes_percent" />
                        <asp:BoundField DataField="answerY" HeaderText="ดำเนินการ" SortExpression="answerY" />
                        <asp:BoundField DataField="answerN" HeaderText="ไม่ดำเนินการ" SortExpression="answerN" />
                    </Columns>
                    <AlternatingRowStyle CssClass="gridAltRow" />
                    <FooterStyle CssClass="gridFooter" />
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle HorizontalAlign="Center" CssClass="gridPager" />
                    <RowStyle CssClass="gridRow" />
                    <EmptyDataTemplate>ไม่มีข้อมูล</EmptyDataTemplate>
                </asp:GridView>
            </div>
            <br />
            <div>
                <p class="report_gridview_head">
                    ตารางสรุปผลมิติความเสี่ยงเชิงยุธศาสตร์ 3 ด้าน</p>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="pj_id,sr_id" DataSourceID="SqlDataSource2" EnableModelValidation="True"
                     ShowFooter="True" OnRowDataBound="GridView2_RowDataBound" CssClass="grid" 
                    Width="80%" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="pj_id" HeaderText="pj_id" ReadOnly="True" SortExpression="pj_id"
                            Visible="false" />
                        <asp:BoundField DataField="sr_id" HeaderText="sr_id" ReadOnly="True" SortExpression="sr_id"
                            Visible="false" />
                        <asp:BoundField DataField="sr_name" HeaderText="สรุปผลคะแนนแยกตามมิติความเสี่ยงเชิงยุทธศาสตร์ 3 ด้าน"
                            SortExpression="sr_name" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="yes_percent" HeaderText="% ดำเนินการ" SortExpression="yes_percent" />
                        <asp:BoundField DataField="answerY" HeaderText="ดำเนินการ" SortExpression="answerY" />
                        <asp:BoundField DataField="answerN" HeaderText="ไม่ดำเนินการ" SortExpression="answerN" />
                    </Columns>
                    <AlternatingRowStyle CssClass="gridAltRow" />
                    <FooterStyle CssClass="gridFooter" />
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle HorizontalAlign="Center" CssClass="gridPager" />
                    <RowStyle CssClass="gridRow" />
                    <EmptyDataTemplate>ไม่มีข้อมูล</EmptyDataTemplate>
                </asp:GridView>
            </div>
            <div>
                <p class="report_gridview_head">
                    &nbsp;</p>
                <p class="report_gridview_head">
                    สรุปการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามชุดคำถามย่อย</p>
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="pj_id,tm_id" DataSourceID="SqlDataSource3" EnableModelValidation="True"
                     ShowFooter="True" OnRowDataBound="GridView3_RowDataBound" CssClass="grid" 
                    Width="80%" >
                    
                    <Columns>
                        <asp:BoundField DataField="pj_id" HeaderText="pj_id" ReadOnly="True" SortExpression="pj_id"
                            Visible="false" />
                        <asp:BoundField DataField="tm_id" HeaderText="tm_id" ReadOnly="True" SortExpression="tm_id"
                            Visible="false" />
                        <asp:BoundField DataField="tm_name" HeaderText="สรุปผลคะแนนแยกตามหลักธรรมาภิบาล"
                            SortExpression="tm_name" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="yes_percent" HeaderText="% ดำเนินการ" SortExpression="yes_percent" />
                        <asp:BoundField DataField="answerY" HeaderText="ดำเนินการ" SortExpression="answerY" />
                        <asp:BoundField DataField="answerN" HeaderText="ไม่ดำเนินการ" SortExpression="answerN" />
                    </Columns>
                    <AlternatingRowStyle CssClass="gridAltRow" />
                    <FooterStyle CssClass="gridFooter" />
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle HorizontalAlign="Center" CssClass="gridPager" />
                    <RowStyle CssClass="gridRow" />
                    <EmptyDataTemplate>ไม่มีข้อมูล</EmptyDataTemplate>
                </asp:GridView>
            </div>
            <!-- รายชุดคำถาม -->
            <br />
            <div>
                <p class="report_gridview_head">
                    สรุปการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล รายชุดคำถาม
                </p>
                <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource5" EnableModelValidation="True" CssClass="grid" 
                     ShowFooter="True" OnRowDataBound="GridView5_RowDataBound" Width="80%"
                    >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="qset_id" HeaderText="ชุดคำถาม" SortExpression="qset_id"
                            Visible="false" />
                        <asp:BoundField DataField="qset_text" HeaderText="ขั้นตอนการริเริ่มโครงการใหม่" 
                            SortExpression="qset_text" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="y_percent_total" HeaderText="% ดำเนินการ" SortExpression="y_percent_total" />
                        <asp:BoundField DataField="y_total" HeaderText="จำนวนข้อรวม" SortExpression="y_total" />
                        <asp:BoundField DataField="y_main" HeaderText="จำนวนข้อหลัก" SortExpression="y_main" />
                        <asp:BoundField DataField="y_sub" HeaderText="จำนวนข้อย่อย" SortExpression="y_sub" />
                    </Columns>
                    <AlternatingRowStyle CssClass="gridAltRow" />
                    <FooterStyle CssClass="gridFooter" />
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle HorizontalAlign="Center" CssClass="gridPager" />
                    <RowStyle CssClass="gridRow" />
                    <EmptyDataTemplate>ไม่มีข้อมูล</EmptyDataTemplate>
                </asp:GridView>
            </div>

            <!-- -->
            <div id="factor_tamma">
                <p class="report_gridview_head">
                    <br />
                    สรุปการวิเคราะห์ความเสี่ยงสภาพแวดล้อมภายในภายนอก</p>
                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="sds_factor_tamma" EnableModelValidation="True" Width="80%"
                    showFooter="True" onrowdatabound="GridView6_RowDataBound" CssClass="grid">
                    <Columns>
                        <asp:BoundField DataField="fm_factors_text" HeaderText="สรุปผลคะแนนแยกตามหลักธรรมภิบาล" 
                            SortExpression="fm_factors_text" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="rf_percent" HeaderText="% ดำเนินการ" 
                            SortExpression="rf_percent" />
                        <asp:BoundField DataField="rf_proceed" HeaderText="ดำเนินการ" 
                            SortExpression="rf_proceed" />
                        <asp:BoundField DataField="rf_not_proceed" HeaderText="ไม่ดำเนินการ" 
                            SortExpression="rf_not_proceed" />
                    </Columns>
                    <EmptyDataTemplate>ไม่มีข้อมูล</EmptyDataTemplate>
                    <AlternatingRowStyle CssClass="gridAltRow" />
                    <FooterStyle CssClass="gridFooter" />
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle HorizontalAlign="Center" CssClass="gridPager" />
                    <RowStyle CssClass="gridRow" />
                </asp:GridView>
            </div>
            <div id="tamma_no_proceed">
                <p class="report_gridview_head">
                    <br />
                    สรุปการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล กรณีข้อที่ไม่ได้ดำเนินการ</p>
                <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="sds_tamma_no_proceed" EnableModelValidation="True" Width="80%"
                    ShowFooter="True" CssClass="grid">
                    <Columns>
                        <asp:BoundField DataField="qset_desc" HeaderText="ลำดับข้อ" ReadOnly="True" 
                            SortExpression="qset_desc" />
                        <asp:BoundField DataField="q3_text" HeaderText="รายละเอียด" 
                            SortExpression="q3_text" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>							
                        <asp:BoundField DataField="tm_name" HeaderText="ธรรมภิบาล" 
                            SortExpression="tm_name" />
                    </Columns>
                    <EmptyDataTemplate>ไม่มีข้อมูล</EmptyDataTemplate>
                    <AlternatingRowStyle CssClass="gridAltRow" />
                    <FooterStyle CssClass="gridFooter" />
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle HorizontalAlign="Center" CssClass="gridPager" />
                    <RowStyle CssClass="gridRow" />
                </asp:GridView>
            </div>
            <br />
            <div>
                <asp:Button ID="btn_goto_step3" runat="server" Text="แสดงความเห็นเพิ่มเติมเพื่อประกอบคำของบประมาณ"
                    OnClick="btn_goto_step3_Click" />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
                    
                    SelectCommand="SELECT [pj_id], [tm_id], [tm_name], [yes_percent], [answerY], [answerN] FROM [report_tamma1] WHERE ([pj_id] = @pj_id)" >
                    <SelectParameters>
                        <asp:Parameter Name="pj_id" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
                    SelectCommand="SELECT [pj_id], [sr_id], [sr_name], [yes_percent], [answerY], [answerN] FROM [report_stratrisk] WHERE ([pj_id] = @pj_id)">
                    <SelectParameters>
                        <asp:Parameter Name="pj_id" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
                    SelectCommand="SELECT [pj_id], [tm_id], [tm_name], [yes_percent], [answerY], [answerN] FROM [report_tamma2] WHERE ([pj_id] = @pj_id)">
                    <SelectParameters>
                        <asp:Parameter Name="pj_id" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
                    SelectCommand="SELECT [pj_id], [tm_id], [tm_name], [yes_percent], [answerY], [answerN] FROM [report_tamma_total] WHERE ([pj_id] = @pj_id)">
                    <SelectParameters>
                        <asp:Parameter Name="pj_id" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
                    SelectCommand="SELECT [qset_id], [qset_text], [y_percent_total], [y_total], [y_main], [y_sub] FROM [report_qset_total] WHERE ([pj_id] = @pj_id)">
                    <SelectParameters>
                        <asp:Parameter Name="pj_id" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="sds_factor_tamma" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>" 
                    SelectCommand="SELECT fm_factors_text, rf_percent, rf_proceed, rf_not_proceed FROM report_factor_tammapiban WHERE (pj_id = @pj_id)">
                    <SelectParameters>
                        <asp:Parameter Name="pj_id" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="sds_tamma_no_proceed" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>" 
                    
                    
                    SelectCommand="SELECT qset_name + '-' + q3_order AS qset_desc, q3_text, tm_name FROM report_tamma_noproceed WHERE (pj_id = @pj_id) ORDER BY tm_name, qset_desc">
                    <SelectParameters>
                        <asp:Parameter Name="pj_id" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </center>
    </div>
</asp:Content>
