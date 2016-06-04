<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true"
    CodeFile="project_approval_3.aspx.cs" Inherits="project_approval_3" Culture="th-TH" UICulture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="linkheader" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTitle" runat="Server">
    รายงานสรุปการวิเคราะห์ความเสี่ยงตามหลักธรรมภิบาล  
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                    <td colspan="2" class="bold1">
                        ยุทธศาสตร์การจัดสรรงบประมาณ :
                    </td>
                    <td colspan="2">
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
    <br />
    <div class="appr_part2_blue_text">
        ส่วนที่ 2 รายงานสรุปการวิเคราะห์ความเสี่ยงตามหลักธรรมภิบาล
    </div>
    <div class="submit-box" style="display:none">
        <center>
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
                <%--Hide by Tai Request--%>
                <tr style="display: none">
                    <td class="td-text">
                        สรุปผลคะแนนการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล ตามชุดคำถามหลัก
                    </td>
                    <td style="border-style: solid; width: 100px; text-align: center; border-color: #666600;">
                        <asp:Literal ID="litRisk12" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr style="display: none">
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
        </center>
    </div>
    <asp:Panel ID="pnl_main" runat="server" CssClass="approval_main" 
        GroupingText="ส่วนความคิดเห็นเพิ่มเติม">
        <!--
        <div class="appr_part2_black_text">
            แสดงความคิดเห็นคำของบประมาณ
        </div>
        -->
        <asp:Panel ID="pnl_L0" runat="server">
            <span class="appr_L0_text">
                <asp:Literal ID="Literal1" runat="server">แสดงความคิดเห็นคำของบประมาณ</asp:Literal>
            </span>
            <asp:RequiredFieldValidator ID="req_L0" runat="server" ErrorMessage="*" CssClass="ErrorDokJan"
                ControlToValidate="radlst_L0" ValidationGroup="comment"></asp:RequiredFieldValidator>
            <asp:RadioButtonList ID="radlst_L0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="radlst_L0_SelectedIndexChanged">
                <asp:ListItem>มีความคิดเห็นเพิ่มเติม</asp:ListItem>
                <asp:ListItem>ไม่มีความคิดเห็นเพิ่มเติม</asp:ListItem>
            </asp:RadioButtonList>
                                <asp:RegularExpressionValidator ID="reg_desc_L0" 
                runat="server" ControlToValidate="txt_L0"
                        Display="Dynamic" ErrorMessage="ห้ามเกิน 300 ตัวอักษร" SetFocusOnError="true"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\w\d/\\/]{0,300}$" 
                ValidationGroup="comment"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="req_desc_L0" runat="server" ControlToValidate="txt_L0"
                        CssClass="ErrorDokJan" ErrorMessage="*" Display="Dynamic" 
                ValidationGroup="comment"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txt_L0" runat="server" CssClass="textArea" TextMode="MultiLine"></asp:TextBox>



            <%--Hide by Tai's Request-------------------- --%>
            <asp:Panel ID="pnl_L1" runat="server" Visible="false">
                <span class="appr_L1_text">1.กรณีที่มีความคิดเห็นเพิ่มเติมในข้อคำถาม </span>
                <asp:Panel ID="pnl_L1_1" runat="server">
                    <span class="appr_L2_text">1.1 ความเห็นเพิ่มเติมในข้อคำถามหลัก </span>
                    <asp:RequiredFieldValidator ID="req_L1_1" runat="server" ControlToValidate="radlst_L1_1"
                        CssClass="ErrorDokJan" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RadioButtonList ID="radlst_L1_1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="radlst_L2_SelectedIndexChanged">
                        <asp:ListItem>มีความคิดเห็นเพิ่มเติม</asp:ListItem>
                        <asp:ListItem>ไม่มีความคิดเห็นเพิ่มเติม</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RegularExpressionValidator ID="reg_desc_L1_1" runat="server" ControlToValidate="txt_L1_1"
                        Display="Dynamic" ErrorMessage="ห้ามเกิน 300 ตัวอักษร" SetFocusOnError="true"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\w\d/\\/]{0,300}$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="req_desc_L1_1" runat="server" ControlToValidate="txt_L1_1"
                        CssClass="ErrorDokJan" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txt_L1_1" runat="server" CssClass="textArea" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
                <asp:Panel ID="pnl_L1_2" runat="server">
                    <span class="appr_L2_text">1.2 ความเห็นเพิ่มเติมในข้อคำถามย่อย </span>
                    <asp:RequiredFieldValidator ID="req_L1_2" runat="server" ControlToValidate="radlst_L1_2"
                        CssClass="ErrorDokJan" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RadioButtonList ID="radlst_L1_2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="radlst_L2_SelectedIndexChanged">
                        <asp:ListItem>มีความคิดเห็นเพิ่มเติม</asp:ListItem>
                        <asp:ListItem>ไม่มีความคิดเห็นเพิ่มเติม</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RegularExpressionValidator ID="reg_desc_L1_2" runat="server" ControlToValidate="txt_L1_2"
                        Display="Dynamic" ErrorMessage="ห้ามเกิน 300 ตัวอักษร" SetFocusOnError="true"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\w\d/\\/]{0,300}$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="req_desc_L1_2" runat="server" ControlToValidate="txt_L1_2"
                        CssClass="ErrorDokJan" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txt_L1_2" runat="server" CssClass="textArea" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="pnl_L2" runat="server" Visible="false">
                <span class="appr_L1_text">2. ความเห็นเพิ่มเติมในการวิเคราะห์ความเสี่ยงด้านสภาพแวดล้อมภายในและภายนอก
                </span>
                <asp:RequiredFieldValidator ID="req_L2" runat="server" ControlToValidate="radlst_L2"
                    CssClass="ErrorDokJan" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br />
                <asp:RadioButtonList ID="radlst_L2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="radlst_L2_SelectedIndexChanged">
                    <asp:ListItem>มีความคิดเห็นเพิ่มเติม</asp:ListItem>
                    <asp:ListItem>ไม่มีความคิดเห็นเพิ่มเติม</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RegularExpressionValidator ID="reg_desc_L2" runat="server" ControlToValidate="txt_L2"
                    Display="Dynamic" ErrorMessage="ห้ามเกิน 300 ตัวอักษร" SetFocusOnError="true"
                    ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\w\d/\\/]{0,300}$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="req_desc_L2" runat="server" ControlToValidate="txt_L2"
                    CssClass="ErrorDokJan" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txt_L2" runat="server" CssClass="textArea" TextMode="MultiLine"></asp:TextBox>
            </asp:Panel>
            <asp:Panel ID="pnl_L3" runat="server" Visible="false">
                <span class="appr_L1_text">3. ผลการพิจารณา </span>
                <asp:RequiredFieldValidator ID="req_L3" runat="server" ControlToValidate="radlst_L3"
                    CssClass="ErrorDokJan" ErrorMessage="*"></asp:RequiredFieldValidator>
                &nbsp;<asp:RadioButtonList ID="radlst_L3" runat="server">
                    <asp:ListItem>ผ่านการพิจารณาประกอบการจัดทำงบประมาณ</asp:ListItem>
                    <asp:ListItem>ไม่ผ่านการพิจารณาประกอบการจัดทำงบประมาณ</asp:ListItem>
                </asp:RadioButtonList>
            </asp:Panel>
        </asp:Panel>
        <asp:Button ID="btn_save" runat="server" Text="ยืนยันการบันทึกความคิดเห็น"
            OnClick="btn_save_Click" 
            onclientclick="if (Page_ClientValidate('comment')){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}" 
            ValidationGroup="comment" />
        <asp:SqlDataSource ID="sds_project_approve_comment" runat="server" ConnectionString="<%$ ConnectionStrings:GovBudgetingConnectionString %>"
            UpdateCommand="UPDATE projects SET pj_approval_status = @pj_approval_status, pj_approval_comment = @pj_L0, pj_approval_comment_1_1 = @pj_L1_1, pj_approval_comment_1_2 = @pj_L1_2, pj_approval_comment_2 = @pj_L2, pj_approval_budget = @pj_approval_budget, pj_lastupdate = GETDATE() WHERE (pj_id = @pj_id)"
            
            
            SelectCommand="SELECT pj_id, pj_approval_status, pj_approval_comment, pj_approval_comment_1_1, pj_approval_budget FROM projects WHERE (pj_id = @pj_id)">
            <SelectParameters>
                <asp:Parameter Name="pj_id" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="pj_approval_status" />
                <asp:Parameter Name="pj_L0" />
                <asp:Parameter Name="pj_L1_1" />
                <asp:Parameter Name="pj_L1_2" />
                <asp:Parameter Name="pj_L2" />
                <asp:Parameter Name="pj_id" />
                <asp:Parameter Name="pj_approval_budget" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </asp:Panel>
    
    <!-- Parliament Region -->
    
</asp:Content>
