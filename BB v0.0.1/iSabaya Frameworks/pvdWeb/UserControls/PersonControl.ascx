<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="WebHelper.UserControls.PersonControl" Codebehind="PersonControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<dx:ASPxComboBox ID="cbxPerson" runat="server" Width="170px" DropDownWidth="550"
    DropDownStyle="DropDownList" DataSourceID="sdcPerson" ValueField="PersonID" ValueType="System.String"
    TextFormatString="{0} {1} {2}" EnableCallbackMode="true" IncrementalFilteringMode="Contains"
    CallbackPageSize="30">
    <Columns>
        <dx:ListBoxColumn FieldName="AffixName" Name="AffixName" Caption="คำนำหน้า" />
        <dx:ListBoxColumn FieldName="FirstName" Name="FirstName" Caption="ชื่อ" />
        <dx:ListBoxColumn FieldName="LastName" Name="LastName" Caption="สกุล" />
    </Columns>
</dx:ASPxComboBox>
<asp:SqlDataSource ID="sdcPerson" runat="server" DataSourceMode="DataReader" ConnectionString="<%$ ConnectionStrings:imSabayaConnectionString%>"
    SelectCommand="
                   SELECT p.PersonID
                   ,p.OfficialIDNo
                   ,dbo.f_mls(naff.AffixMLSID, 'th-TH') as AffixName
                   ,dbo.f_mls(pn.FirstNameID, 'th-TH') as FirstName
                   ,dbo.f_mls(pn.LastNameID, 'th-TH') as LastName
                   FROM Person p
                   INNER JOIN PersonName pn ON p.CurrentNameID = pn.PersonNameID
                   LEFT JOIN NameAffix naff on pn.NamePrefixID = naff.AffixID
                   ">
    <SelectParameters>
        <asp:SessionParameter Name="languageCode" SessionField="LanguageCode" />
    </SelectParameters>
</asp:SqlDataSource>