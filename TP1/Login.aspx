<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TP1.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 225px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_Content" runat="server">
    <div class="LoginForm">
        <table style="width: 100%;">
            <tr>
                <td class="auto-style1">Username : </td>
                <td>
                    <asp:TextBox ID="TB_Username" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">Password : </td>
                <td>
                    <asp:TextBox ID="TB_Password" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Button ID="BTN_Login" runat="server" OnClick="BTN_Login_Click" ValidationGroup="Login" Text="Login" CssClass="SubmitButton"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BTN_Inscription" runat="server" Text="Inscription" OnClick="BTN_Inscription_Click" CssClass="SubmitButton" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BTN_Password" runat="server" Text="Mot de passe oublié" OnClick="BTN_Password_Click" CssClass="SubmitButton"/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary runat="server"
                        ID="ValidationSummary"
                        HeaderText="Erreur sur les champs suivants :"
                        DisplayMode="BulletList"
                        EnableClientScript="true"
                        ValidationGroup="Login" />
                    <asp:CustomValidator runat="server"
                        ID="CV_Username"
                        ControlToValidate="TB_Username"
                        ErrorMessage="Nom d'usager"
                        OnServerValidate="CV_Username_ServerValidate"
                        ValidationGroup="Login"
                        ValidateEmptyText="true"
                        Display="None">
                    </asp:CustomValidator>
                    <asp:CustomValidator runat="server"
                        ID="CV_Password"
                        ControlToValidate="TB_Password"
                        ErrorMessage="Mot de passe"
                        OnServerValidate="CV_Password_ServerValidate"
                        ValidationGroup="Login"
                        ValidateEmptyText="true"
                        Display="None">
                    </asp:CustomValidator>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
