<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_NewPersonControl" Codebehind="NewPersonControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
   
<dx:ASPxComboBox ID="cbxPerson" runat="server"   ></dx:ASPxComboBox>

<asp:SqlDataSource ID="sdcPerson" runat="server" DataSourceMode="DataReader"
    ConnectionString="<%$ ConnectionStrings:imSabayaConnectionString%>" 
    SelectCommand="
                   SELECT p.PersonID
                   ,p.OfficialIDNo
                   ,dbo.f_mls(pn.FirstNameID,@languageCode) as FirstName
                   ,dbo.f_mls(pn.LastNameID,@languageCode) as LastName
                   FROM Person p
                   INNER JOIN PersonName pn ON p.CurrentNameID = pn.PersonNameID
                   ">
                <SelectParameters>
                <asp:SessionParameter  Name="languageCode" SessionField="LanguageCode"/>
                </SelectParameters>
                </asp:SqlDataSource>
                <%-- SELECT PersonID,dbo.f_GetPersonName(PersonID,@languageCode) as FullName,OfficialIDNo
                   FROM Person--%>
