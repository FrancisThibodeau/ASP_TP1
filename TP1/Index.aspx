<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TP1.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_Content" runat="server">
    <div>
        <table>
            <tr>
                <td><asp:Button ID="BTN_Profil" runat="server" Text="Modifier votre profil..." OnClick="BTN_Profil_Click" CssClass="SubmitButton"/></td>
            </tr>
            <tr>
                <td><asp:Button ID="BTN_EnLigne" runat="server" Text="Usagers en ligne..." CssClass="SubmitButton" OnClick="BTN_EnLigne_Click"/></td>
            </tr>
            <tr>
                <td><asp:Button ID="BTN_Journal" runat="server" Text="Journal des visites" CssClass="SubmitButton" OnClick="BTN_Journal_Click"/></td>
            </tr>
            <tr>
                <td><asp:Button ID="BTN_Logout" runat="server" Text="Déconnexion" OnClick="BTN_Logout_Click" CssClass="SubmitButton"/></td>
            </tr>
        </table>
    </div>
</asp:Content>
