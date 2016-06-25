<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="PVDFundUserControl" Codebehind="PVDFundUserControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<table cellpadding="0px" cellspacing="0px" border="0px">
	<tr>
		<td>
			<dxe:ASPxDropDownEdit ID="DropDownEdit" runat="server" ClientInstanceName="DropDownEdit"
				Width="170px" AllowUserInput="False" EnableAnimation="False">
				<DropDownWindowStyle>
					<Border BorderWidth="0px" />
				</DropDownWindowStyle>
				<DropDownWindowTemplate>
					<dxwgv:ASPxGridView ID="GridView" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridView"
						Width="500px" OnCustomJSProperties="GridView_CustomJSProperties" DataSourceID="SqlDataSource1"
						KeyFieldName="EmployeeIDFundID">
						<SettingsBehavior ConfirmDelete="True" EnableRowHotTrack="True" AllowFocusedRow="True" />
						<Columns>
							<dxwgv:GridViewDataTextColumn FieldName="EmployerNo" VisibleIndex="0" Caption="บริษัท">
							</dxwgv:GridViewDataTextColumn>
							<dxwgv:GridViewDataTextColumn FieldName="FundCode" VisibleIndex="1" Caption="รหัสกองทุน">
							</dxwgv:GridViewDataTextColumn>
							<dxwgv:GridViewDataTextColumn FieldName="FundName" VisibleIndex="2" Caption="กองทุน">
							</dxwgv:GridViewDataTextColumn>
						</Columns>
						<ClientSideEvents Init="function GridViewInitHandler(s, e) {
                                                    var keyValue = DropDownEdit.GetKeyValue();
                                                    var index = -1;
                                                    if(keyValue != null)
                                                        index = ASPxClientUtils.ArrayIndexOf(GridView.cpKeyValues, keyValue);
                                                        //alert(index);
                                                    GridView.SetFocusedRowIndex(index);
                                                    GridView.MakeRowVisible(index);
                                                }
                                                " RowClick="function RowClickHandler(s, e) {
                                                    DropDownEdit.SetKeyValue(GridView.cpKeyValues[e.visibleIndex]);
                                                    DropDownEdit.SetText(GridView.cpEmployeeNames[e.visibleIndex]);
                                                    DropDownEdit.HideDropDown();
                                                }" />
						<SettingsPager Mode="ShowAllRecords">
						</SettingsPager>
						<Settings ShowVerticalScrollBar="True" />
						<Settings ShowFilterRow="True" ShowFilterRowMenu="true" />
					</dxwgv:ASPxGridView>
				</DropDownWindowTemplate>
				<%--<ClientSideEvents DropDown="DropDownHandler" />--%>
			</dxe:ASPxDropDownEdit>
		</td>
	</tr>
</table>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:imSabayaConnectionString %>"
	SelectCommand="
 SELECT  
 CONVERT(nvarchar(20), dbo.Employer.EmployerID) +'-'+ CONVERT(nvarchar(20), dbo.Fund.FundID  )EmployeeIDFundID,
 dbo.Fund.FundID, dbo.Fund.Code AS FundCode, dbo.Employer.EmployerID, dbo.Employer.EmployerNo, dbo.f_mls(dbo.Fund.TitleMLSID, N'th-TH') AS FundName
 FROM         dbo.Employer INNER JOIN
 dbo.EmployerFund ON dbo.Employer.EmployerID = dbo.EmployerFund.EmployerID INNER JOIN
 dbo.Fund ON dbo.EmployerFund.FundID = dbo.Fund.FundID
 where EmployerFund.EffectiveFrom &lt;= GETDATE() AND EmployerFund.EffectiveTo &gt;= GETDATE()                     
    "></asp:SqlDataSource>
