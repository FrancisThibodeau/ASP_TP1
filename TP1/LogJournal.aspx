<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LogJournal.aspx.cs" Inherits="TP1.LogJournal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_Content" runat="server">

    <asp:Button ID="BTN_Retour" runat="server" Text="Retour" CssClass="SubmitButton" OnClick="BTN_Retour_Click"/>
    <asp:GridView ID="PN_GridView" CssClass="gridview" runat="server"></asp:GridView>

</asp:Content>
