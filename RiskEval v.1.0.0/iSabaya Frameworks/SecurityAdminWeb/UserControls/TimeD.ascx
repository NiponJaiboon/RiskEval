<%@ Control Language="C#" AutoEventWireup="true" Inherits="ctrls_TimeD" Codebehind="TimeD.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<script type="text/javascript">
<!-- Begin
var strText;
function hr<%=strIdent%>(id, arg) {
	if (arg == "plus") {
		if (document.getElementById(id).value<23) 
			{
			document.getElementById(id).value++;
			strText=document.getElementById(id).value;
				if (strText.length<2) 
				{
				document.getElementById(id).value="0"+strText;
				}
			document.getElementById(id).focus();
			}
	}
	else if (arg == "minus") {
       		if (document.getElementById(id).value>1) 
			{
			document.getElementById(id).value--;
			strText=document.getElementById(id).value;
				if (strText.length<2) 
				{
				document.getElementById(id).value="0"+strText;
				}
			document.getElementById(id).focus();
			}
 	}
}

function mn<%=strIdent%>(id, arg) {
	if (arg == "plus") {
		if (document.getElementById(id).value<60) 
			{
			document.getElementById(id).value++;
			strText=document.getElementById(id).value;
				if (strText.length<2) 
				{
				document.getElementById(id).value="0"+strText;
				}
			document.getElementById(id).focus();
			}
	}
	else if (arg == "minus") {
       		if (document.getElementById(id).value>1) 
			{
			document.getElementById(id).value--;
			strText=document.getElementById(id).value;
				if (strText.length<2) 
				{
				document.getElementById(id).value="0"+strText;
				}
			document.getElementById(id).focus();
			}
 	}
}

function dy<%=strIdent%>(id, arg) {
	if (arg == "plus") {
		if (document.getElementById(id).value<60) 
			{
			document.getElementById(id).value++;
			strText=document.getElementById(id).value;
				if (strText.length<2) 
				{
				document.getElementById(id).value="0"+strText;
				}
			document.getElementById(id).focus();
			}
	}
	else if (arg == "minus") {
       		if (document.getElementById(id).value>1) 
			{
			document.getElementById(id).value--;
			strText=document.getElementById(id).value;
				if (strText.length<2) 
				{
				document.getElementById(id).value="0"+strText;
				}
			document.getElementById(id).focus();
			}
 	}
}

function checkvalue<%=strIdent%>(id) {
if (document.getElementById(id).value<12) 
	{
	strText=document.getElementById(id).value;
	if (strText.length<2) 
		{
		document.getElementById(id).value="0"+strText;
		document.getElementById(id).focus();
		}
	}
}


// End -->
</script>

<%
	String baseUrl = "http://" + Request.ServerVariables["SERVER_NAME"] + ":" +
			  Request.ServerVariables["SERVER_PORT"] + Request.ApplicationPath;
	//Console.WriteLine(baseUrl);
%>
<table>
	<tr>
		<td>
			<table cellspacing="0" cellpadding="0" border="0">
				<tr>
					<td>
						<dx:ASPxLabel ID="lblYear" runat="server" Text="ปี">
						</dx:ASPxLabel>
					</td>
					<td>
						<asp:TextBox runat="server" ID="year" name="year" Style="width: 22px;" Text="00" />
					</td>
					<td>
						<table cellspacing="0" cellpadding="0" border="0">
							<tr>
								<td>
									<asp:Image runat="server" ImageUrl="plus.gif" ID="ObsYearPlus" Style="cursor: hand" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:Image runat="server" ImageUrl="minus.gif" ID="ObsYearMinus" Style="cursor: hand" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
		<td>
			<table cellspacing="0" cellpadding="0" border="0">
				<tr>
					<td>
						<dx:ASPxLabel ID="lblMonth" runat="server" Text="เดือน">
						</dx:ASPxLabel>
					</td>
					<td>
						<asp:TextBox runat="server" ID="month" name="month" Style="width: 22px;" Text="00" />
					</td>
					<td>
						<table cellspacing="0" cellpadding="0" border="0">
							<tr>
								<td>
									<asp:Image runat="server" ImageUrl="plus.gif" ID="ObsMonthPlus" Style="cursor: hand" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:Image runat="server" ImageUrl="minus.gif" ID="ObsMonthMinus" Style="cursor: hand" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
		<td>
			<table cellspacing="0" cellpadding="0" border="0">
				<tr>
					<td>
						<dx:ASPxLabel ID="lblDay" runat="server" Text="วัน">
						</dx:ASPxLabel>
					</td>
					<td>
						<asp:TextBox runat="server" ID="day" name="day" Style="width: 44px;" Text="00.00" />
					</td>
					<td>
						<table cellspacing="0" cellpadding="0" border="0">
							<tr>
								<td>
									<asp:Image runat="server" ImageUrl="plus.gif" ID="ObsDayPlus" Style="cursor: hand" />
								</td>
							</tr>
							<tr>
								<td>
									<asp:Image runat="server" ImageUrl="minus.gif" ID="ObsDayMinus" Style="cursor: hand" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
