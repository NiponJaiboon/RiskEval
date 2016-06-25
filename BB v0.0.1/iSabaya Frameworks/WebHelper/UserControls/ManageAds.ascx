<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageAds.ascx.cs" Inherits="BizPortalAdminWeb.UserControls.Ads" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<div class="travelWidget animatedTabWidget">
    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="AdsDataSource">
        <ItemTemplate>
            <div id='<%# GetAdsPageID(Eval("ID")) %>' class="page visible">
                <%--<dx:ASPxImage ID="mediumImage" runat="server" ImageUrl='<%# Eval("SmallImage") %>'
                    CssClass="medium" />--%>
                <a href='<%# Eval("Link") %>' style="background-color:transparent" target="_blank" title='<%# Eval("Detail")%>'><dx:ASPxImage  ID="largeImage" runat="server" ImageUrl='<%# Eval("LargeImage") %>'
                    Width="722px" Height="91.5%" BackColor="Transparent" /></a>
                <div class="item">
                    <%--<div class="location">
                        <%--<%# Eval("Detail")%>
                    </div>--%>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div style="position: absolute; bottom: 0;">
        <dx:ASPxTabControl ID="AdsTabControl" runat="server" DataSourceID="AdsDataSource"
            RenderMode="Lightweight" TabPosition="Bottom" TabAlign="Justify" OnTabDataBound="AdsTabControl_TabDataBound"
            OnCustomJSProperties="AdsTabControl_CustomJSProperties" Width="722px">
            <TabStyle ForeColor="White" HorizontalAlign="Right">
            </TabStyle>
            <ClientSideEvents Init="Widgets.AnimatedTabWidget.Init" ActiveTabChanging="Widgets.AnimatedTabWidget.ActiveTabChanging"
                TabClick="Widgets.AnimatedTabWidget.TabClick" />
        </dx:ASPxTabControl>
    </div>
</div>
<asp:XmlDataSource ID="AdsDataSource" runat="server" DataFile="E:\BizPortal\Ads.xml"
    XPath="/Ads/*" />