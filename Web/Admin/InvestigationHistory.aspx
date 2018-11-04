<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Form.Master" AutoEventWireup="true"
    CodeBehind="InvestigationHistory.aspx.cs" Inherits="UDIRS.Web.Admin.InvestigationHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <style type="text/css">
        .hiddencol
        {
            display: none;
        }
    </style>
       <script>
           $( function() {
               $( ".dateTxt" ).datepicker();
           } );
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMsg" runat="server" />
    <div class="search-container">
            <div class="search-item-label">
                <asp:Label ID="lblSearch" runat="server" Text="Search By : "></asp:Label>
                <asp:TextBox ID="txtSearch" placeholder="Reference Number" runat="server"></asp:TextBox>
            </div>
            <div class="search-item-label">
                Status :
                <asp:DropDownList ID="ddlStatus" runat="server">
                </asp:DropDownList>
            </div>
            <div class="search-item-label">
                Person Injured :
                <asp:DropDownList ID="ddlPersonInjured" runat="server">
                    <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="search-item-label">
                Treatment Provided :
                <asp:DropDownList ID="ddlTreatmentProvided" runat="server">
                    <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="search-item-label">
                <asp:Label ID="lblFromDate" runat="server" Text="From Date : "></asp:Label>
                <asp:TextBox ID="txtFromDate" placeholder="Date" CssClass="dateTxt" runat="server"></asp:TextBox>
            </div>
            <div class="search-item-label">
                <asp:Label ID="lblToDate" runat="server" Text="To Date : "></asp:Label>
                <asp:TextBox ID="txtToDate" placeholder="Date" CssClass="dateTxt" runat="server"></asp:TextBox>
            </div>
            <div class="search-item-label">
                <asp:Button ID="btnSearch" runat="server" Text="Search" />
            </div>
            <div class="search-item-label">
                <asp:Button ID="btnReset" runat="server" Text="Reset" />
            </div>
    </div>
    <%-- <div>
        <fieldset>
            <legend><strong>Search By</strong></legend>
        </fieldset>
    </div>--%>
    <div id="dvGrid" class="gv-standard" runat="server">
        <asp:GridView ID="gvAdminHistory" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
            runat="server">
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
                <asp:TemplateField HeaderText="IncidentLocation">
                    <ItemTemplate>
                        <asp:Label ID="lblIncidentLocation" Text='<%# Eval("IncidentLocation")%>' runat="server"></asp:Label>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OtherLocation" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblOtherLocation" Text='<%# Eval("OtherLocation")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IsAnyPersonInjured" HeaderText="PersonInjured" ReadOnly="True" />
                <asp:BoundField DataField="IsTreatmentProvided" HeaderText="TreatmentProvided" ReadOnly="True" />
                <asp:BoundField DataField="IncidentDate" HeaderText="Date/Time" ReadOnly="True" />
                <asp:BoundField DataField="IncidentStatus" HeaderText="Status" ReadOnly="True" />
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
