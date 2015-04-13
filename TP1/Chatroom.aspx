<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Chatroom.aspx.cs" Inherits="TP1.Chatroom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 547px;
        }

        .auto-style2 {
            width: 98px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_Content" runat="server">
    <asp:Timer ID="TimerChatroom" runat="server" Interval="3000" OnTick="TimerChatroom_Tick"></asp:Timer>

    <asp:UpdatePanel ID="UPN_Chatroom" runat="server" UpdateMode="Conditional">

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
    <asp:UpdatePanel ID="PN_Message" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td id="empty" class="auto-style2"></td>
                    <td class="auto-style1">
                        <asp:TextBox ID="TB_Message" runat="server" TextMode="MultiLine" Width="536px" ClientIDMode="Static"
                            onkeydown="char = (event.which || event.keyCode); if (char == 13) document.getElementById(&quot;BTN_Send&quot;).click();"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="BTN_Send" runat="server" Text="Envoyer" CssClass="SubmitButton" OnClick="BTN_Send_Click" ClientIDMode="Static" />
                        <br />
                        <asp:Button ID="BTN_Back" runat="server" Text="Retour" CssClass="SubmitButton" OnClick="BTN_Back_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
