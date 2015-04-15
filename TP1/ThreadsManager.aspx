<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ThreadsManager.aspx.cs" Inherits="TP1.ThreadsManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_Content" runat="server">

    <div>
        <table>
            <tbody>
                <tr>
                    <td>
                        <h3>Liste de mes discussions</h3>
                        <asp:ListBox ID="LBL_ListDiscussions" runat="server" Width="200px" Height="800px"></asp:ListBox>
                        <br />
                        <asp:Button ID="BTN_New" runat="server" Text="Nouveau" OnClick="BTN_New_Click" />
                        <br />
                        <asp:Button ID="BTN_Modify" runat="server" Text="Modifier" OnClick="BTN_Modify_Click" />
                        <br />
                        <asp:Button ID="BTN_Delete" runat="server" Text="Supprimer" OnClick="BTN_Delete_Click" />
                        <br />
                        <asp:Button ID="BTN_Retour" runat="server" Text="Retour" OnClick="BTN_Retour_Click" />
                    </td>
                    <td>
                        <h3>Titre de la discussion</h3>
                        <asp:TextBox ID="TBX_NewThread" runat="server" Width="200px"></asp:TextBox>
                        <br />
                        <h4>Sélection des invités</h4>
                        <asp:CheckBox ID="CBX_All" runat="server"/>
                        Tous les usagers
                        <asp:Panel ID="PN_User_Content" runat="server">
                            <asp:CheckBoxList ID="CBX_Users" runat="server"></asp:CheckBoxList>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>

                    <!-- Seems like a blanck space filled with <td> and a <div> -->

                </tr>
            </tbody>
        </table>
        <!-- <div> seems to be the submit button -->

        <!-- Two spans containing master content -->
    </div>











    <%-- <table>
        <tr>
            <td>
                <label>Liste de mes discussions</label>
            </td>
            <td>
                <label>Titre de la discussion</label>
            </td>
        </tr>
        <tr>
            <td rowspan="2">
                <asp:ListView ID="LV_Discussions" runat="server">
                </asp:ListView>
                <asp:DataGrid ID="DGV_Discussions" runat="server">
                    <Columns>
                        <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                    </Columns>

                </asp:DataGrid>
            </td>
            <td>
                <asp:TextBox ID="TBX_TitreDiscussion" runat="server"></asp:TextBox>
                <asp:CustomValidator
                    ID="CVal_TitreDiscussion"
                    runat="server"
                    ErrorMessage="Le titre ne peut pas être vide!"
                    Text="!"
                    ControlToValidate="TBX_TitreDiscussion"
                    OnServerValidate="CVal_TitreDiscussion_ServerValidate"
                    ValidateEmptyText="True" />
                <asp:CustomValidator
                    ID="CVal_DiscussionExiste"
                    runat="server"
                    ErrorMessage="Le titre existe déjà!"
                    Text="!"
                    ControlToValidate="TBX_TitreDiscussion"
                    OnServerValidate="CVal_DiscussionExiste_Exists"
                    ValidateEmptyText="True" />

            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="CBOX_AllUsers" runat="server" Text="Tous les usagers" OnCheckedChanged="CBOX_AllUsers_CheckedChanged" />
                <asp:Table ID="TB_AllExistingUsers" runat="server"></asp:Table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_New" Text="Nouvelle Discussion" OnClick="BTN_New_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_Modify" Text="Modifier" OnClick="BTN_Modify_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="BTN_Delete" Text="Delete" OnClick="BTN_Delete_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="BTN_Retour" runat="server" Text="Retour" OnClick="BTN_Retour_Click" />
            </td>
        </tr>
    </table>--%>
</asp:Content>
