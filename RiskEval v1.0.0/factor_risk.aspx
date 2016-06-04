<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage/Main.master" AutoEventWireup="true" CodeFile="factor_risk.aspx.cs" Inherits="factor_risk" Culture="th-TH" UICulture="th-TH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="linkheader" Runat="Server">
    <style type="text/css">
        .tbl_inside_panel
        {
            width:100%;
        }
        .tbl_inside_panel .fm 
        {
            color:Green;
        }
        .tbl_inside_panel .fm .td_text
        {
            font-size:25px;
        }        
        .tbl_inside_panel .fm .td_oppo
        {
            font-size:25px;
        }
        .tbl_inside_panel .fm .td_effect
        {
            font-size:25px;
        }
        .tbl_inside_panel .fm .td_impact
        {
            font-size:25px;
        }
        .tbl_inside_panel .fs
        {
            color:Olive;
        }
        .tbl_inside_panel .fs .td_text
        {
            padding-left: 20px;
            font-size:20px;
        }        
        .tbl_inside_panel .fs .td_oppo
        {            margin-left: 200px;
        }
        .tbl_inside_panel .fs .td_effect
        {
        }
        .tbl_inside_panel .fs .td_impact
        {
        }
        .desc_head
        {
            color:green;
            font-size:25px;
        }
        .desc_text
        {
        }
        .desc_text .desc_more_text
        {
            width:98%;
            height:50px;
            margin-top: 0px;
        }
        .save
        {
            text-align:center;
            padding:5px;
        }
        .save .confirm_save_button
        {
            width:250px;
            padding:3px;
           margin:3px;
        }
        .next
        {
            text-align:center; 
        }
        .next .btn_next
        {
           width:250px;  
           padding:3px;
           margin:3px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
                        
        })
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentTitle" Runat="Server">
    
    คำถามปัจจัยภายในภายนอก
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:Panel ID="pnl_fm_1" runat="server">
        <table class="tbl_inside_panel">
            <tr class="fm">
                <td class="td_text">
                    <asp:Label ID="lbl_fm_order_1" runat="server"></asp:Label> 
                    . &nbsp;<asp:Label ID="lbl_fm_id_1" runat="server"></asp:Label>
&nbsp;<asp:Label ID="fm_factors_text_1" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:Label ID="lbl_oppo" runat="server" CssClass="oppo_text" Text="โอกาส"></asp:Label>
                </td>
                <td class="td_effect">
                    <asp:Label ID="lbl_effect" runat="server" CssClass="effect_text" Text="ผลกระทบ"></asp:Label>
                </td>
                <td class="td_impact">
                    <asp:Label ID="lbl_impact" runat="server" CssClass="impact_text" 
                        Text="ความรุนแรง"></asp:Label>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_1_1" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_1_1" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_1_1" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_1_1" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_1_1" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_1_1" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_1_1" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_1_1" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_1_1" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    
                    <asp:Label ID="lbl_fs_order_1_2" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_1_2" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_1_2" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_1_2" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_1_2" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_1_2" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_1_2" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_1_2" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_1_2" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    
                    <asp:Label ID="lbl_fs_order_1_3" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_1_3" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_1_3" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_1_3" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_1_3" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_1_3" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_1_3" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_1_3" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_1_3" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    
                    <asp:Label ID="lbl_fs_order_1_4" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_1_4" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_1_4" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_1_4" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_1_4" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_1_4" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_1_4" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_1_4" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_1_4" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    
                    <asp:Label ID="lbl_fs_order_1_5" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_1_5" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_1_5" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_1_5" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_1_5" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_1_5" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_1_5" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_1_5" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_1_5" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    
                    <asp:Label ID="lbl_fs_order_1_6" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_1_6" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_1_6" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_1_6" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_1_6" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_1_6" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_1_6" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_1_6" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_1_6" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    
                    <asp:Label ID="lbl_fs_order_1_7" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_1_7" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_1_7" runat="server"></asp:Label>
                    <asp:TextBox ID="txt_fs_factors_etc_1" runat="server" CssClass="fs_factors_etc"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="reg_etc_1" runat="server" 
                        ControlToValidate="txt_fs_factors_etc_1" 
                        ErrorMessage="ตัวหนังสือ และ ตัวเลข ปรกติเท่านั้น" SetFocusOnError="true" 
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
                        ValidationGroup="error_panel_1"></asp:RegularExpressionValidator>
                    </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_1_7" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_1_7" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_1_7" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_1_7" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_1_7" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_1_7" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="desc_head">
                <td colspan="4">
                    คำอธิบายเพิ่มเติม
                        <asp:RegularExpressionValidator ID="reg_desc_1" 
	                    runat="server" 
                        ControlToValidate="txt_desc_more_text_1" 
	                    ErrorMessage="ห้ามเกิน 1000 ตัวอักษร"
                         Display="Dynamic"
                         SetFocusOnError="true"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1000}$">
                        ValidationGroup="error_panel_1"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="desc_text">
                <td colspan="4">
                    <asp:TextBox ID="txt_desc_more_text_1" runat="server" CssClass="desc_more_text" 
                        TextMode="MultiLine" ValidationGroup="error_panel_1"></asp:TextBox>
                </td>
            </tr>
            <tr class="save">
                <td colspan="4">
                    <asp:Button ID="btn_save_fm_1" runat="server" OnClientClick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}"
                        Text="บันทึก" CssClass="confirm_save_button" onclick="btn_save_fm_Click" 
                        CommandArgument="1" ValidationGroup="error_panel_1" CommandName="save" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnl_fm_2" runat="server">
        <table class="tbl_inside_panel">
            <tr class="fm">
                <td class="td_text">
                    <asp:Label ID="lbl_fm_order_2" runat="server"></asp:Label>.&nbsp;
                    <asp:Label ID="lbl_fm_id_2" runat="server"></asp:Label>
                    <asp:Label ID="fm_factors_text_2" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:Label ID="lbl_oppo0" runat="server" CssClass="oppo_text" Text="โอกาส"></asp:Label>
                </td>
                <td class="td_effect">
                    <asp:Label ID="lbl_effect0" runat="server" CssClass="effect_text" 
                        Text="ผลกระทบ"></asp:Label>
                </td>
                <td class="td_impact">
                    <asp:Label ID="lbl_impact0" runat="server" CssClass="impact_text" 
                        Text="ความรุนแรง"></asp:Label>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">

                    <asp:Label ID="lbl_fs_order_2_1" runat="server"></asp:Label>
                    
                    <asp:Label ID="lbl_fs_id_2_1" runat="server"></asp:Label>
                    
                    <asp:Label ID="lbl_fs_factors_text_2_1" runat="server"></asp:Label>

                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_2_1" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_2_1" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_2_1" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_2_1" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_2_1" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_2_1" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_2_2" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_2_2" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_2_2" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_2_2" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_2_2" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_2_2" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_2_2" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_2_2" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_2_2" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_2_3" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_2_3" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_2_3" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_2_3" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_2_3" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_2_3" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_2_3" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_2_3" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_2_3" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_2_4" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_2_4" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_2_4" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_2_4" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_2_4" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_2_4" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_2_4" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_2_4" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_2_4" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_2_5" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_2_5" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_2_5" runat="server"></asp:Label>
                    <asp:TextBox ID="txt_fs_factors_etc_2" runat="server" CssClass="fs_factors_etc"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="reg_etc_2" runat="server" 
                        ControlToValidate="txt_fs_factors_etc_2" 
                        ErrorMessage="ตัวหนังสือ และ ตัวเลข ปรกติเท่านั้น" SetFocusOnError="true" 
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
                        ValidationGroup="error_panel_2"></asp:RegularExpressionValidator>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_2_5" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_2_5" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_2_5" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_2_5" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_2_5" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_2_5" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="desc_head">
                <td colspan="4">
                    คำอธิบายเพิ่มเติม
                        <asp:RegularExpressionValidator ID="reg_desc_2" 
	                    runat="server" 
                        ControlToValidate="txt_desc_more_text_2" 
	                    ErrorMessage="ห้ามเกิน 1000 ตัวอักษร"
                         Display="Dynamic"
                         SetFocusOnError="true"
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1000}$">
                        ValidationGroup="error_panel_2"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="desc_text">
                <td colspan="4">
                    <asp:TextBox ID="txt_desc_more_text_2" runat="server" CssClass="desc_more_text" 
                        TextMode="MultiLine" ValidationGroup="error_panel_2"></asp:TextBox>
                </td>
            </tr>
            <tr class="save">
                <td colspan="4">
                
                    <asp:Button ID="btn_save_fm_2" runat="server" CommandArgument="2" 
                        CssClass="confirm_save_button" onclick="btn_save_fm_Click" 
                        onclientclick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}" 
                        Text="บันทึก" ValidationGroup="error_panel_2" CommandName="save" />
                
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnl_fm_3" runat="server">
        <table class="tbl_inside_panel">
            <tr class="fm">
                <td class="td_text">
                    <asp:Label ID="lbl_fm_order_3" runat="server"></asp:Label>
                    .&nbsp;
                    <asp:Label ID="lbl_fm_id_3" runat="server"></asp:Label>
                    <asp:Label ID="fm_factors_text_3" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:Label ID="lbl_oppo1" runat="server" CssClass="oppo_text" Text="โอกาส"></asp:Label>
                </td>
                <td class="td_effect">
                    <asp:Label ID="lbl_effect1" runat="server" CssClass="effect_text" 
                        Text="ผลกระทบ"></asp:Label>
                </td>
                <td class="td_impact">
                    <asp:Label ID="lbl_impact1" runat="server" CssClass="impact_text" 
                        Text="ความรุนแรง"></asp:Label>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_3_1" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_3_1" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_3_1" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_3_1" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_3_1" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_3_1" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_3_1" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_3_1" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_3_1" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_3_2" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_3_2" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_3_2" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_3_2" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_3_2" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_3_2" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_3_2" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_3_2" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_3_2" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_3_3" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_3_3" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_3_3" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_3_3" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_3_3" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_3_3" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_3_3" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_3_3" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_3_3" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_3_4" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_3_4" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_3_4" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_3_4" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_3_4" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_3_4" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_3_4" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_3_4" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_3_4" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_3_5" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_3_5" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_3_5" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_3_5" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_3_5" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_3_5" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_3_5" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_3_5" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_3_5" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_3_6" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_3_6" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_3_6" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_3_6" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_3_6" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_3_6" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_3_6" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_3_6" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_3_6" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_3_7" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_3_7" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_3_7" runat="server"></asp:Label>
                    <asp:TextBox ID="txt_fs_factors_etc_3" runat="server" CssClass="fs_factors_etc"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="reg_etc_3" runat="server" 
                        ControlToValidate="txt_fs_factors_etc_3" 
                        ErrorMessage="ตัวหนังสือ และ ตัวเลข ปรกติเท่านั้น" SetFocusOnError="true" 
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
                        ValidationGroup="error_panel_3"></asp:RegularExpressionValidator>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_3_7" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_3_7" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_3_7" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_3_7" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_3_7" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_3_7" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="desc_head">
                <td colspan="4">
                    คำอธิบายเพิ่มเติม
                    <asp:RegularExpressionValidator ID="reg_desc_3" runat="server" 
                        ControlToValidate="txt_desc_more_text_3" Display="Dynamic" 
                        ErrorMessage="ห้ามเกิน 1000 ตัวอักษร" SetFocusOnError="true" 
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1000}$">
                        ValidationGroup="error_panel_3"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="desc_text">
                <td colspan="4">
                    <asp:TextBox ID="txt_desc_more_text_3" runat="server" CssClass="desc_more_text" 
                        TextMode="MultiLine" ValidationGroup="error_panel_3"></asp:TextBox>
                </td>
            </tr>
            <tr class="save">
                <td colspan="4">
                    <asp:Button ID="btn_save_fm_3" runat="server" CommandArgument="3" 
                        CssClass="confirm_save_button" onclick="btn_save_fm_Click" 
                        onclientclick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}" 
                        Text="บันทึก" ValidationGroup="error_panel_3" CommandName="save" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnl_fm_4" runat="server">
        <table class="tbl_inside_panel">
            <tr class="fm">
                <td class="td_text">
                    <asp:Label ID="lbl_fm_order_4" runat="server"></asp:Label>
                    .&nbsp;
                    <asp:Label ID="lbl_fm_id_4" runat="server"></asp:Label>
                    <asp:Label ID="fm_factors_text_4" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:Label ID="lbl_oppo2" runat="server" CssClass="oppo_text" Text="โอกาส"></asp:Label>
                </td>
                <td class="td_effect">
                    <asp:Label ID="lbl_effect2" runat="server" CssClass="effect_text" 
                        Text="ผลกระทบ"></asp:Label>
                </td>
                <td class="td_impact">
                    <asp:Label ID="lbl_impact2" runat="server" CssClass="impact_text" 
                        Text="ความรุนแรง"></asp:Label>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_4_1" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_4_1" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_4_1" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_4_1" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_4_1" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_4_1" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_4_1" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_4_1" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_4_1" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_4_2" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_4_2" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_4_2" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_4_2" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_4_2" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_4_2" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_4_2" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_4_2" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_4_2" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_4_3" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_4_3" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_4_3" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_4_3" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_4_3" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_4_3" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_4_3" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_4_3" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_4_3" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_4_4" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_4_4" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_4_4" runat="server"></asp:Label>
                    <asp:TextBox ID="txt_fs_factors_etc_4" runat="server" CssClass="fs_factors_etc"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="reg_etc_4" runat="server" 
                        ControlToValidate="txt_fs_factors_etc_4" 
                        ErrorMessage="ตัวหนังสือ และ ตัวเลข ปรกติเท่านั้น" SetFocusOnError="true" 
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
                        ValidationGroup="error_panel_4"></asp:RegularExpressionValidator>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_4_4" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_4_4" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_4_4" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_4_4" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_4_4" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_4_4" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="desc_head">
                <td colspan="4">
                    คำอธิบายเพิ่มเติม
                    <asp:RegularExpressionValidator ID="reg_desc_4" runat="server" 
                        ControlToValidate="txt_desc_more_text_4" Display="Dynamic" 
                        ErrorMessage="ห้ามเกิน 1000 ตัวอักษร" SetFocusOnError="true" 
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1000}$">
                        ValidationGroup="error_panel_4"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="desc_text">
                <td colspan="4">
                    <asp:TextBox ID="txt_desc_more_text_4" runat="server" CssClass="desc_more_text" 
                        TextMode="MultiLine" ValidationGroup="error_panel_4"></asp:TextBox>
                </td>
            </tr>
            <tr class="save">
                <td colspan="4">
                    <asp:Button ID="btn_save_fm_4" runat="server" CommandArgument="4" 
                        CssClass="confirm_save_button" onclick="btn_save_fm_Click" 
                        onclientclick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}" 
                        Text="บันทึก" ValidationGroup="error_panel_4" CommandName="save" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnl_fm_5" runat="server">
        <table class="tbl_inside_panel">
            <tr class="fm">
                <td class="td_text">
                    <asp:Label ID="lbl_fm_order_5" runat="server"></asp:Label>
                    .&nbsp;
                    <asp:Label ID="lbl_fm_id_5" runat="server"></asp:Label>
                    <asp:Label ID="fm_factors_text_5" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:Label ID="lbl_oppo3" runat="server" CssClass="oppo_text" Text="โอกาส"></asp:Label>
                </td>
                <td class="td_effect">
                    <asp:Label ID="lbl_effect3" runat="server" CssClass="effect_text" 
                        Text="ผลกระทบ"></asp:Label>
                </td>
                <td class="td_impact">
                    <asp:Label ID="lbl_impact3" runat="server" CssClass="impact_text" 
                        Text="ความรุนแรง"></asp:Label>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_5_1" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_5_1" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_5_1" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_5_1" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_5_1" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_5_1" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_5_1" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_5_1" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_5_1" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_5_2" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_5_2" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_5_2" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_5_2" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_5_2" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_5_2" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_5_2" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_5_2" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_5_2" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_5_3" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_5_3" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_5_3" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_5_3" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_5_3" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_5_3" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_5_3" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_5_3" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_5_3" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_5_4" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_5_4" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_5_4" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_5_4" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_5_4" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_5_4" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_5_4" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_5_4" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_5_4" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_5_5" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_5_5" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_5_5" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_5_5" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_5_5" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_5_5" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_5_5" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_5_5" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_5_5" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_5_6" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_5_6" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_5_6" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_5_6" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_5_6" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_5_6" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_5_6" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_5_6" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_5_6" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_5_7" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_5_7" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_5_7" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_5_7" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_5_7" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_5_7" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_5_7" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_5_7" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_5_7" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_5_8" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_5_8" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_5_8" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_5_8" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_5_8" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_5_8" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_5_8" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_5_8" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_5_8" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_5_9" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_5_9" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_5_9" runat="server"></asp:Label>
                    <asp:TextBox ID="txt_fs_factors_etc_5" runat="server" CssClass="fs_factors_etc"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="reg_etc_5" runat="server" 
                        ControlToValidate="txt_fs_factors_etc_5" 
                        ErrorMessage="ตัวหนังสือ และ ตัวเลข ปรกติเท่านั้น" SetFocusOnError="true" 
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
                        ValidationGroup="error_panel_5"></asp:RegularExpressionValidator>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_5_9" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_5_9" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_5_9" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_5_9" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_5_9" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_5_9" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="desc_head">
                <td colspan="4">
                    คำอธิบายเพิ่มเติม
                    <asp:RegularExpressionValidator ID="reg_desc_5" runat="server" 
                        ControlToValidate="txt_desc_more_text_5" Display="Dynamic" 
                        ErrorMessage="ห้ามเกิน 1000 ตัวอักษร" SetFocusOnError="true" 
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1000}$">
                        ValidationGroup="error_panel_5"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="desc_text">
                <td colspan="4">
                    <asp:TextBox ID="txt_desc_more_text_5" runat="server" CssClass="desc_more_text" 
                        TextMode="MultiLine" ValidationGroup="error_panel_5"></asp:TextBox>
                </td>
            </tr>
            <tr class="save">
                <td colspan="4">
                    <asp:Button ID="btn_save_fm_5" runat="server" CommandArgument="5" 
                        CssClass="confirm_save_button" onclick="btn_save_fm_Click" 
                        onclientclick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}" 
                        Text="บันทึก" ValidationGroup="error_panel_5" CommandName="save" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnl_fm_6" runat="server">
        <table class="tbl_inside_panel">
            <tr class="fm">
                <td class="td_text">
                    <asp:Label ID="lbl_fm_order_6" runat="server"></asp:Label>
                    .&nbsp;
                    <asp:Label ID="lbl_fm_id_6" runat="server"></asp:Label>
                    <asp:Label ID="fm_factors_text_6" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:Label ID="lbl_oppo4" runat="server" CssClass="oppo_text" Text="โอกาส"></asp:Label>
                </td>
                <td class="td_effect">
                    <asp:Label ID="lbl_effect4" runat="server" CssClass="effect_text" 
                        Text="ผลกระทบ"></asp:Label>
                </td>
                <td class="td_impact">
                    <asp:Label ID="lbl_impact4" runat="server" CssClass="impact_text" 
                        Text="ความรุนแรง"></asp:Label>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_6_1" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_6_1" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_6_1" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_6_1" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_6_1" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_6_1" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_6_1" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_6_1" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_6_1" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_6_2" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_6_2" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_6_2" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_6_2" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_6_2" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_6_2" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_6_2" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_6_2" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_6_2" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_6_3" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_6_3" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_6_3" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_6_3" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_6_3" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_6_3" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_6_3" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_6_3" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_6_3" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_6_4" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_6_4" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_6_4" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_6_4" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_6_4" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_6_4" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_6_4" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_6_4" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_6_4" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_6_5" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_6_5" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_6_5" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_6_5" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_6_5" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_6_5" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_6_5" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_6_5" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_6_5" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_6_6" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_6_6" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_6_6" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_6_6" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_6_6" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_6_6" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_6_6" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_6_6" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_6_6" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_6_7" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_6_7" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_6_7" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_6_7" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_6_7" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_6_7" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_6_7" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_6_7" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_6_7" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_6_8" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_6_8" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_6_8" runat="server"></asp:Label>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_6_8" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_6_8" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_6_8" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_6_8" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_6_8" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_6_8" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="fs">
                <td class="td_text">
                    <asp:Label ID="lbl_fs_order_6_9" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_id_6_9" runat="server"></asp:Label>
                    <asp:Label ID="lbl_fs_factors_text_6_9" runat="server"></asp:Label>
                    <asp:TextBox ID="txt_fs_factors_etc_6" runat="server" CssClass="fs_factors_etc"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="reg_etc_6" runat="server" 
                        ControlToValidate="txt_fs_factors_etc_6" 
                        ErrorMessage="ตัวหนังสือ และ ตัวเลข ปรกติเท่านั้น" SetFocusOnError="true" 
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1500}$">
                        ValidationGroup="error_panel_6"></asp:RegularExpressionValidator>
                </td>
                <td class="td_oppo">
                    <asp:DropDownList ID="ddl_oppo_6_9" runat="server" CssClass="oppo">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_oppo_6_9" runat="server" 
                        ControlToValidate="ddl_oppo_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
                <td class="td_effect">
                    <asp:DropDownList ID="ddl_effect_6_9" runat="server" CssClass="effect">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_effect_6_9" runat="server" 
                        ControlToValidate="ddl_effect_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
                <td class="td_impact">
                    <asp:DropDownList ID="ddl_impact_6_9" runat="server" CssClass="impact">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvld_impact_6_9" runat="server" 
                        ControlToValidate="ddl_impact_1_1" ErrorMessage="*" InitialValue="กรุณาเลือก" Enabled="false"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="desc_head">
                <td colspan="4">
                    คำอธิบายเพิ่มเติม
                    <asp:RegularExpressionValidator ID="reg_desc_6" runat="server" 
                        ControlToValidate="txt_desc_more_text_6" Display="Dynamic" 
                        ErrorMessage="ห้ามเกิน 1000 ตัวอักษร" SetFocusOnError="true" 
                        ValidationExpression="^[a-zA-Z0-9ก-๙\x22\s\.\+\(\)\-\:,\=\%\w\d/\\/]{0,1000}$">
                        ValidationGroup="error_panel_6"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="desc_text">
                <td colspan="4">
                    <asp:TextBox ID="txt_desc_more_text_6" runat="server" CssClass="desc_more_text" 
                        TextMode="MultiLine" ValidationGroup="error_panel_6"></asp:TextBox>
                </td>
            </tr>
            <tr class="save">
                <td colspan="4">
                    <asp:Button ID="btn_save_fm_6" runat="server" CommandArgument="6" 
                        CssClass="confirm_save_button" onclick="btn_save_fm_Click" 
                        onclientclick="if (Page_ClientValidate()){ return confirm('ต้องการบันทึกและดำเนินการต่อไป')}" 
                        Text="บันทึก" ValidationGroup="error_panel_6" CommandName="save" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div class="next">
        <asp:Button ID="btn_next" runat="server" Text="ทำข้อถัดไป" CssClass="btn_next" 
            onclick="btn_next_Click" Visible="False" />
    </div>
</asp:Content>

