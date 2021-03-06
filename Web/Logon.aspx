﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true"
    CodeBehind="Logon.aspx.cs" Inherits="UDIRS.Web.Logon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div title="Login" class="page-home-in">
        <div title="overlap" class="home-Log-in">
            <fieldset title="Login" class="fieldset-login" style="width: 320px; height: 300px">
                <legend>Login</legend>
                <div>
                    <br />
                    <asp:Label ID="Name" runat="server" Text="UserName:" />
                    <asp:TextBox ID="txtUserName" runat="server" Height="22px" />
                    <asp:RequiredFieldValidator ID="RV1" runat="server" ControlToValidate="txtUserName"
                        ErrorMessage="Please enter your User Name" SetFocusOnError="True">*
                    </asp:RequiredFieldValidator><br />
                </div>
                <br />
                <div>
                    <asp:Label ID="lblPwd" runat="server" Text="Password:" />
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Height="22px" />
                    <asp:RequiredFieldValidator ID="RV2" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="Please enter your Password" SetFocusOnError="True">*
                    </asp:RequiredFieldValidator><br />
                </div>
                <div id="AdI-R3" style="margin-top:20px;">
                    <asp:Button ID="btnLogIn" class="control-Btncontainer" Text="Sign In" runat="server" />
                </div>
                <div>
                    <asp:HyperLink ID="hplForgot_PWD" runat="server" Style="color: #E9FFB1;" NavigateUrl="https://eservices.uod.edu.sa/reset-password/resetpassword.aspx">Forgot Password ?</asp:HyperLink>
                    <br />
                    <a>If you do not have a University ID, Please</a>
                    <asp:HyperLink ID="New_Registration" runat="server" Style="color: #E9FFB1;" NavigateUrl="~/Web/Registration.aspx">Register</asp:HyperLink>
                </div>
                <br />
                <br />
                <asp:Label ID="lblMsg" runat="server" Text="" />
            </fieldset>
        </div>
    </div>
</asp:Content>
<%--


--%>