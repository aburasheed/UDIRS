<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Form.Master" AutoEventWireup="true"
    CodeBehind="IMO_History.aspx.cs" Inherits="UDIRS.Web.Admin.IMO_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">


        $( function() {
            $( ".dateTxt" ).datepicker();
        }
       );
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMsg" runat="server" />
    <div class="search-container" style="height: 220px;">
        <div class="search-item-label">
            <div class="lbl-search-ctrl">
                Search By:</div>
            <asp:TextBox ID="txtSearch" placeholder="Reference Number" runat="server"></asp:TextBox>
        </div>
        <div class="search-item-label">
            <div class="lbl-search-ctrl">
                Status:</div>
            <asp:DropDownList ID="ddlStatus" runat="server">
            </asp:DropDownList>
        </div>
        <div class="search-item-label">
            <div class="lbl-search-ctrl">
                Treatment Provided:</div>
            <asp:DropDownList ID="ddlTreatmentProvided" runat="server">
                <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                <asp:ListItem Text="No" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="search-item-label">
            <div class="lbl-search-ctrl">
                Person Injured:</div>
            <asp:DropDownList ID="ddlPersonInjured" runat="server">
                <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                <asp:ListItem Text="No" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="search-item-label">
            <div class="lbl-search-ctrl">
                Incident Location :</div>
            <asp:DropDownList ID="ddlLocation" runat="server" />
        </div>
        <div class="search-item-label">
        </div>
        <div class="search-item-label">
            <div class="lbl-search-ctrl">
                From Date:</div>
            <asp:TextBox ID="txtFromDate" placeholder="Date" CssClass="dateTxt" runat="server"></asp:TextBox>
        </div>
        <div class="search-item-label">
            <div class="lbl-search-ctrl">
                To Date:</div>
            <asp:TextBox ID="txtToDate" placeholder="Date" CssClass="dateTxt" runat="server"></asp:TextBox>
        </div>
        <div class="search-item-label">
            <asp:Button ID="btnSearch" class="btn-search" runat="server" Text="Search" />
        </div>
        <div class="search-item-label">
            <asp:Button ID="btnReset" class="btn-search" runat="server" Text="Reset" />
        </div>
    </div>
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
