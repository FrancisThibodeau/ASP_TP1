<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Room.aspx.cs" Inherits="TP1.ListerUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_Content" runat="server">
    <asp:Timer ID="TimerRoom" runat="server" Interval="3000" OnTick="TimerRoom_Tick"></asp:Timer>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="TimerRoom" EventName="tick" />
        </Triggers>
        <ContentTemplate>
            <asp:Button ID="BTN_Retour" runat="server" Text="Retour" CssClass="SubmitButton" OnClick="BTN_Retour_Click" />
            <asp:Panel ID="PN_GridView" CssClass="gridview" runat="server"></asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
