<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ListCustomerRiskProfileControl" Codebehind="ListCustomerRiskProfileControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="WebHelper" Namespace="WebHelper.Controls" TagPrefix="ucdt" %>
<%@ Register Src="CustomerRiskProfileViewerControl.ascx" TagName="CustomerRiskProfileViewer"
    TagPrefix="uccrpv" %>
<%@ Register Src="RiskProfileDescriptionControl.ascx" TagName="RiskProfileDescriptionControl"
    TagPrefix="ucrpdc" %>
<asp:HiddenField ID="hddMfAccountID" runat="server" />
<dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="รายละเอียดข้อมูลของลูกค้า"
    View="GroupBox" GroupBoxHeaderStyle-Border-BorderWidth="0" GroupBoxHeaderStyle-BackgroundImage-ImageUrl=" ">
    <PanelCollection>
        <dxp:PanelContent>
            <table id="tbComboCustomerProfile" runat="server" class="tbFormbase" cellpadding="0"
                cellspacing="0">
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lblSelectCustomerProfile" runat="server">
                        </dxe:ASPxLabel>
                    </td>
                    <td style="padding-left: 5">
                        <dxe:ASPxComboBox ID="comboCustomerProfile" runat="server" Width="250" DropDownWidth="550"
                            DropDownStyle="DropDownList" ValueField="QuestionnaireID" ValueType="System.String"
                            TextFormatString="{0} {1}" EnableCallbackMode="true" IncrementalFilteringMode="Contains"
                            CallbackPageSize="30">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" />
                                <dxe:ListBoxColumn FieldName="Title" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </td>
                </tr>
            </table>
            <br />
            <table id="tbCustomerProfile" runat="server" class="tbFormbase" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <dx:ASPxGridView ID="gridCustomerProfile" ClientInstanceName="gridCustomerProfile"
                            KeyFieldName="ResponseID" AutoGenerateColumns="false" EnableRowsCache="false"
                            runat="server" OnCustomCallback="gridCustomerProfile_CustomCallback" OnDataBinding="gridCustomerProfile_DataBinding">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="Questionnaire.Code">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="Respondent">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="RespondedDate">
                                </dx:GridViewDataColumn>
                                <dx:GridViewCommandColumn Caption="Action" ButtonType="Image">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="btnViewDetailCustomerProfile" Text="">
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </td>
                </tr>
            </table>
        </dxp:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>
<br />
<dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" HeaderText="ความสามารถในการรับความเสี่ยง"
    View="GroupBox" GroupBoxHeaderStyle-Border-BorderWidth="0" GroupBoxHeaderStyle-BackgroundImage-ImageUrl=" ">
    <PanelCollection>
        <dxp:PanelContent>
            <dx:ASPxRoundPanel ID="ASPxRoundPanel3" runat="server" HeaderText="รายละเอียด" View="GroupBox"
                GroupBoxHeaderStyle-Border-BorderWidth="0" GroupBoxHeaderStyle-BackgroundImage-ImageUrl=" ">
                <PanelCollection>
                    <dxp:PanelContent>
                        <ucrpdc:RiskProfileDescriptionControl ID="ctrlRiskProfileDescription" runat="server" />
                    </dxp:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
            <br />
            <dx:ASPxRoundPanel ID="ASPxRoundPanel4" runat="server" HeaderText="รายการแบบฟอร์ม"
                View="GroupBox" GroupBoxHeaderStyle-Border-BorderWidth="0" GroupBoxHeaderStyle-BackgroundImage-ImageUrl=" ">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table id="tbRiskProfile" runat="server" class="tbFormbase" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="lblSelectRiskProfile" runat="server">
                                    </dxe:ASPxLabel>
                                </td>
                                <td style="padding-left: 5">
                                    <dxe:ASPxComboBox ID="comboRiskProfile" runat="server" Width="250" DropDownWidth="550"
                                        DropDownStyle="DropDownList" ValueField="QuestionnaireID" ValueType="System.String"
                                        TextFormatString="{0} {1}" EnableCallbackMode="true" IncrementalFilteringMode="Contains"
                                        CallbackPageSize="30">
                                        <Columns>
                                            <dxe:ListBoxColumn FieldName="Code" />
                                            <dxe:ListBoxColumn FieldName="Title" />
                                        </Columns>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table id="tbRiskProfiel" runat="server" class="tbFormbase" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <dx:ASPxGridView ID="gridRiskProfile" ClientInstanceName="gridCustomerProfile" KeyFieldName="ResponseID"
                                        AutoGenerateColumns="false" EnableRowsCache="false" runat="server" OnCustomCallback="gridRiskProfile_CustomCallback"
                                        OnDataBinding="gridRiskProfile_DataBinding">
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="Questionnaire.Code">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Respondent">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Score">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="RespondedDate">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewCommandColumn Caption="Action" ButtonType="Image" CellStyle-Paddings-PaddingRight="5">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="btnViewDetailRiskProfile" Text="">
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <%--<dx:GridViewCommandColumnCustomButton ID="btnHelpRiskProfile" Text="">
                                                    </dx:GridViewCommandColumnCustomButton>--%>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
        </dxp:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>
<dx:ASPxPopupControl ASPxPopupControl ID="popQuestionniare" runat="server" AllowDragging="True"
    CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above"
    HeaderText="แบบฟอร์ม" ShowPageScrollbarWhenModal="true" ShowFooter="false">
    <ContentCollection>
        <dx:PopupControlContentControl ID="popQContent1" runat="server">
            <dxcp:ASPxCallbackPanel ID="cpbViewCustomerRiskProfile" runat="server" OnCallback="cpbViewCustomerRiskProfile_Callback">
                <PanelCollection>
                    <dxp:PanelContent>
                        <uccrpv:CustomerRiskProfileViewer ID="ctrlCustomerRiskProfileviewer" runat="server"
                            FormWidth="800" />
                        <br />
                        <center>
                            <table class="tbFormbase" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btnClosePopQuestion" runat="server" AutoPostBack="false">
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
<dx:ASPxPopupControl ASPxPopupControl ID="popRiskProfileDescription" runat="server"
    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above"
    ShowHeader="false">
    <ContentCollection>
        <dx:PopupControlContentControl>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>