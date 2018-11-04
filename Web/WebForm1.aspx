<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Form.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="UDIRS.Web.WebForm1" %>
                                                                      
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link href="../Styles/BSite.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="GridviewDiv">
<table style="width: 420px" border="0" cellpadding="0" cellspacing="1" class="GridviewTable">
<tr >
<td style="width: 40px;">
UserId
</td>
<td style="width: 120px;" >
LastName
</td>
<td style="width: 130px;">
UserName
</td>
<td style="width: 130px;">
Location
</td>
</tr>
<tr >
<td style="width: 40px;">
</td>
<td style="width: 120px;">
</td>
<td style="width: 130px;">
</td>
<td style="width: 130px;">
<asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" Width="120px"
 Font-Size="11px" onselectedindexchanged="ddlLocation_SelectedIndexChanged">
</asp:DropDownList>
</td>
</tr>
<tr>
<td colspan="4">
<asp:GridView runat="server" ID="gvDetails" ShowHeader="false" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" Width="420px"  CssClass="Gridview">
<Columns>
<asp:BoundField DataField="UserId" HeaderText="UserId" ItemStyle-Width="40px" />
<asp:BoundField DataField="Education" HeaderText="Education" ItemStyle-Width="120px" />
<asp:BoundField DataField="UserName" HeaderText="UserName" ItemStyle-Width="130px"/>
<asp:BoundField DataField="Location" HeaderText="Location" ItemStyle-Width="130px"/>
</Columns>
</asp:GridView>
</td>
</tr>
</table>
</div>


</asp:Content>
