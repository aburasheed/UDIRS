﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Form.Master" AutoEventWireup="true" CodeBehind="Userhistory.aspx.cs" Inherits="UDIRS.Web.Userhistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style type="text/css">
        .hiddencol
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMsg" runat="server" />
<div id="dvGrid" style="margin-left:270px; margin-top: 40px; margin-bottom: 520px;" runat="server">
    <asp:GridView ID="gvHistory" AutoGenerateColumns="False" CellPadding="4" 
        runat="server" OnRowDataBound="gvHistory_RowDataBound" ForeColor="#333333">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>

         <asp:BoundField DataField="Id" HeaderText="IncidentID" ItemStyle-CssClass="hiddencol"
                    HeaderStyle-CssClass="hiddencol" ReadOnly="True" />
                <asp:TemplateField HeaderText="ReferenceID">
                    <ItemTemplate>
                        <a runat="server" id="ahReference">
                            <%#Eval("ReferenceID")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="ReferenceID">
                <ItemTemplate>
                    <a style="text-decoration: none;" href='<%#"IncidentReported.aspx?RID="+DataBinder.Eval(Container.DataItem,"ReferenceID") %>'>
                        <%#Eval("ReferenceID")%>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="IncidentLocation">
                <ItemTemplate>
                    <asp:Label ID="lblIncidentLocation"  Text='<%# Eval("IncidentLocation")%>' runat="server"></asp:Label>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="OtherLocation" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblOtherLocation" Text='<%# Eval("OtherLocation")%>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="IncidentLocation" HeaderText="Incident Location" ReadOnly="True" />--%>
            <asp:BoundField DataField="IncidentDate" HeaderText="Date/Time" ReadOnly="True" />
            <%--<asp:BoundField DataField="IncidentStatus" HeaderText="Status" ReadOnly="True" />--%>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#173a63" ForeColor="White" Font-Bold="True" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#173a63" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
</div>
</asp:Content>
