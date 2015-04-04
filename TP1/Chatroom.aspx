﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Chatroom.aspx.cs" Inherits="TP1.Chatroom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_Content" runat="server">
    <asp:Timer ID="TimerChatroom" runat="server" Interval="3000" OnTick="TimerChatroom_Tick"></asp:Timer>

    <asp:UpdatePanel ID="UPN_Chatroom" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="TimerChatroom" EventName="Tick" />
        </Triggers>

        <ContentTemplate>
            <table>
                <tr>
                    <td style="width: 15%">
                        <asp:Label ID="LBL_Threads" runat="server" Text="Vos discussions"></asp:Label></td>
                    <td style="width: 70%">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="LBL_Title" runat="server" Text="Aucune discussion séectionée"></asp:Label></td>
                                <td style="text-align: right">
                                    <asp:Label ID="LBL_Creator" runat="server" Text="..."></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 15%">
                        <asp:Label ID="LBL_Users" runat="server" Text="Invités"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="PN_Threads" runat="server"></asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="PN_Messages" runat="server"></asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="PN_Users" runat="server"></asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UPN_Text" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td id="empty"></td>
                    <td>
                        <asp:TextBox ID="TB_Text" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="BTN_Send" runat="server" Text="Envoyer" />
                        <br />
                        <asp:Button ID="BTN_Back" runat="server" Text="Retour" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
