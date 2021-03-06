<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="ctrls_EmployeeControl" Codebehind="MemberControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<table cellspacing="0px" cellpadding="0px">
	<tr>
		<td valign="top">
			<dxcp:ASPxCallbackPanel ID="cbpTxtEmployerNo" runat="server" OnCallback="cbpTxtEmployerNo_Callback">
				<PanelCollection>
					<dxp:PanelContent ID="PanelContent1" runat="server">
						<dxe:ASPxButtonEdit ID="btnEmployeeBrowse" runat="server">
							<Buttons>
								<dxe:EditButton>
								</dxe:EditButton>
							</Buttons>
						</dxe:ASPxButtonEdit>
					</dxp:PanelContent>
				</PanelCollection>
			</dxcp:ASPxCallbackPanel>
		</td>
	</tr>
	<tr>
		<td>
			<dxe:ASPxLabel ID="labelTitle" runat="server" />
			<%--<dx:ASPxHiddenField ID="lblEmployerID" runat="server" >
			</dx:ASPxHiddenField>--%>
			<dxe:ASPxLabel ID="lblEmployerID" runat="server" BackColor="Transparent" ForeColor="Transparent" />
			<dxcb:ASPxCallback ID="cbChangeEmployer" runat="server" OnCallback="cbChangeEmployer_Callback">
			</dxcb:ASPxCallback>
		</td>
	</tr>
</table>
<dxcb:ASPxCallback ID="cbGetAccountNo" runat="server" OnCallback="cbGetAccountNo_Callback">
</dxcb:ASPxCallback>
<dxpc:ASPxPopupControl ID="popupAccount" runat="server" AllowDragging="True" CloseAction="CloseButton"
	HeaderText="พนักงาน" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
	<ContentCollection>
		<dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
			<dxcb:ASPxCallback ID="CallbacklikeCustomerName" runat="server" ClientInstanceName="likeCustomerNameCallback"
				OnCallback="likeCustomerNameCallback_Callback">
			</dxcb:ASPxCallback>
			<dxcb:ASPxCallback ID="cbFindEmployer" runat="server" OnCallback="cbFindEmployer_Callback">
			</dxcb:ASPxCallback>
			<dxtc:ASPxPageControl Width="400px" ID="ASPxPageControl1" runat="server" ActiveTabIndex="1"
				EnableHierarchyRecreation="True">
				<TabPages>
					<dxtc:TabPage Name="Search by name" Text="หาด้วยชื่อ">
						<ContentCollection>
							<dxw:ContentControl ID="Contentcontrol2" runat="server">
								<table>
									<tr>
										<td>
											<dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="แผนก">
											</dxe:ASPxLabel>
										</td>
										<td>
											<dxe:ASPxComboBox ID="ddDepartment" runat="server" IncrementalFilteringMode="StartsWith"
												EnableClientSideAPI="True" OnCallback="ddDepartment_Callback">
											</dxe:ASPxComboBox>
										</td>
									</tr>
									<tr>
										<td>
											<dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="หาจาก">
											</dxe:ASPxLabel>
										</td>
										<tr>
											<td colspan="2">
												<dxe:ASPxRadioButtonList ID="ASPxRadioButtonList1" runat="server" RepeatDirection="Horizontal"
													SelectedIndex="0">
													<Items>
														<dxe:ListEditItem Text="รหัสพนักงาน" Value="0" />
														<dxe:ListEditItem Text="ชื่อ" Value="1" />
														<dxe:ListEditItem Text="สกุล" Value="2" />
													</Items>
												</dxe:ASPxRadioButtonList>
											</td>
										</tr>
										<tr>
											<td>
												<dxe:ASPxLabel ID="lblKeyword" runat="server" CssClass="defaultFont" Text="Keyword">
												</dxe:ASPxLabel>
											</td>
											<td>
												<dxe:ASPxTextBox ID="txtFirstName" runat="server" ClientInstanceName="likeCustomerName"
													Width="300px">
												</dxe:ASPxTextBox>
											</td>
										</tr>
									</tr>
								</table>
								<table>
									<tr>
										<td>
											<dxe:ASPxButton ID="btnFindName" Text="ค้นหา" runat="server" AutoPostBack="False"
												EnableClientSideAPI="True">
											</dxe:ASPxButton>
										</td>
										<td>
											<dxe:ASPxButton ID="btnAddAll" Text="เลือกทั้งหมด" runat="server" AutoPostBack="False"
												EnableClientSideAPI="True">
											</dxe:ASPxButton>
										</td>
									</tr>
								</table>
							</dxw:ContentControl>
						</ContentCollection>
					</dxtc:TabPage>
				</TabPages>
			</dxtc:ASPxPageControl>
			<dxwgv:ASPxGridView runat="server" ClientInstanceName="gridCustomer" KeyFieldName="AccountID"
				AutoGenerateColumns="False" ID="GridCustomer" Width="100%">
				<Columns>
					<dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" />
					<dxwgv:GridViewDataColumn FieldName="DivisionCode" ReadOnly="True" Caption="รหัสแผนก"
						ShowInCustomizationForm="False" VisibleIndex="1">
					</dxwgv:GridViewDataColumn>
					<dxwgv:GridViewDataColumn FieldName="AccountNo" ReadOnly="True" Caption="เลขที่"
						ShowInCustomizationForm="False" VisibleIndex="2" Visible="false">
						<EditFormSettings Visible="False"></EditFormSettings>
					</dxwgv:GridViewDataColumn>
					<dxwgv:GridViewDataColumn FieldName="FullName" ReadOnly="True" Caption="ชื่อ" ShowInCustomizationForm="False"
						VisibleIndex="3">
						<EditFormSettings Visible="False"></EditFormSettings>
					</dxwgv:GridViewDataColumn>
					<dxwgv:GridViewCommandColumn Caption="Action" VisibleIndex="4">
						<CustomButtons>
							<dxwgv:GridViewCommandColumnCustomButton ID="buttonSelect" Text="เลือก">
							</dxwgv:GridViewCommandColumnCustomButton>
						</CustomButtons>
					</dxwgv:GridViewCommandColumn>
				</Columns>
				<SettingsPager Mode="ShowAllRecords">
				</SettingsPager>
				<Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="300" />
			</dxwgv:ASPxGridView>
		</dxpc:PopupControlContentControl>
	</ContentCollection>
</dxpc:ASPxPopupControl>
