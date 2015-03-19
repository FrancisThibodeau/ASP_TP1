<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TP1.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_Content" runat="server">
    <div>
        <table>
            <tr>
                <td><asp:Button ID="BTN_Profil" runat="server" Text="Modifier votre profil..." /></td>
            </tr>
            <tr>
                <td><asp:Button ID="BTN_EnLigne" runat="server" Text="Usagers en ligne..." /></td>
            </tr>
            <tr>
                <td><asp:Button ID="BTN_Journal" runat="server" Text="Journal des visites" /></td>
            </tr>
            <tr>
                <td><asp:Button ID="BTN_Logout" runat="server" Text="Déconnexion" OnClick="BTN_Logout_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
