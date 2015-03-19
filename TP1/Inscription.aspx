<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Inscription.aspx.cs" Inherits="TP1.Inscription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_Content" runat="server">
    <div id="Inscription">
        <script type="text/javascript" src="ClientFormUtilities.js"></script>
        <table>
            <tr>
                <!------------------------ FORMULAIRE ------------------------>
                <td>
                    <!------- Informations et Boutons ------->
                    <table>
                        <!-- Fullname -->
                        <tr>
                            <td>
                                <asp:Label ID="LBL_Fullname" runat="server" Text="Nom au complet"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="TB_Fullname" name="TB_Fullname" runat="server"></asp:TextBox></td>
                        </tr>
                        <!-- Username -->
                        <tr>
                            <td>
                                <asp:Label ID="LBL_Username" runat="server" Text="Nom d'usager"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="TB_Username" name="TB_Username" runat="server"></asp:TextBox></td>
                        </tr>
                        <!-- Password -->
                        <tr>
                            <td>
                                <asp:Label ID="LBL_Password" runat="server" Text="Mot de passe"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="TB_Password" name="TB_Password" runat="server"></asp:TextBox></td>
                        </tr>
                        <!-- Password Confirm -->
                        <tr>
                            <td>
                                <asp:Label ID="LBL_PasswordConfirm" runat="server" Text="Confirmation du mot de passe"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="TB_PasswordConfirm" name="TB_PasswordConfirm" runat="server"></asp:TextBox></td>
                        </tr>
                        <!-- Email -->
                        <tr>
                            <td>
                                <asp:Label ID="LBL_Email" runat="server" Text="Adresse de courriel"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="TB_Email" name="TB_Email" runat="server"></asp:TextBox></td>
                        </tr>
                        <!-- Email Confirm -->
                        <tr>
                            <td>
                                <asp:Label ID="LBL_EmailConfirm" runat="server" Text="Confirmation de l'adresse de courriel"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="TB_EmailConfirm" name="TB_EmailConfirm" runat="server"></asp:TextBox></td>
                        </tr>
                        <!-- Bouton S'inscrire -->
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BTN_Inscrire" runat="server" OnClick="BTN_Inscrire_Click" Text="S'inscrire..." ValidationGroup="Inscription" /></td>
                        </tr>
                        <!-- Bouton Annuler -->
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BTN_Annuler" runat="server" Text="Annuler..." OnClick="BTN_Annuler_Click" /></td>
                        </tr>
                    </table>
                </td>
                <td>
                    <!------- Captcha et Avatar ------->
                    <table>
                        <!---- Captcha ---->
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="PN_Captcha" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="RegenarateCaptcha" runat="server"
                                                        ImageUrl="~/Images/RegenerateCaptcha.png"
                                                        CausesValidation="False"
                                                        OnClick="RegenarateCaptcha_Click"
                                                        ValidationGroup="Inscription"
                                                        Width="48"
                                                        ToolTip="Regénérer le captcha..." />
                                                </td>
                                                <td>
                                                    <asp:Image ID="IMGCaptcha" ImageUrl="~/captcha.png" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:TextBox ID="TB_Captcha" runat="server" MaxLength="5"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <!---- Image Avatar ---->
                        <tr>
                            <td>
                                <asp:Image ID="IMG_PreviewAvatar" runat="server" ClientIDMode="Static" /></td>
                        </tr>
                        <!---- Bouton Choisir ---->
                        <tr>
                            <td>
                                <asp:FileUpload ID="FU_Avatar" runat="server" ClientIDMode="Static" onchange="PreLoadImage();" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!------------------------ SOMMAIRE D'ERREURS ------------------------>
                <td colspan="2">
                    <asp:ValidationSummary runat="server"
                        ID="ValidationSummary"
                        HeaderText="Erreur sur les champs suivants :"
                        DisplayMode="BulletList"
                        EnableClientScript="true"
                        ValidationGroup="Inscription" />
                    <asp:CustomValidator ID="CV_Fullname" runat="server"
                        ControlToValidate="TB_Fullname"
                        ErrorMessage="Nom complet"
                        OnServerValidate="CV_Fullname_ServerValidate"
                        ValidationGroup="Inscription"
                        ValidateEmptyText="True"
                        Display="None">
                    </asp:CustomValidator>
                    <asp:CustomValidator runat="server"
                        ID="CV_Username"
                        ControlToValidate="TB_Username"
                        ErrorMessage="Nom d'usager"
                        OnServerValidate="CV_Username_ServerValidate"
                        ValidationGroup="Inscription"
                        ValidateEmptyText="true"
                        Display="None">
                    </asp:CustomValidator>
                    <asp:CustomValidator runat="server"
                        ID="CV_Password"
                        ControlToValidate="TB_Password"
                        ErrorMessage="Mot de passe"
                        OnServerValidate="CV_Password_ServerValidate"
                        ValidationGroup="Inscription"
                        ValidateEmptyText="true"
                        Display="None">
                    </asp:CustomValidator>
                    <asp:CustomValidator runat="server"
                        ID="CV_PasswordConfirm"
                        ControlToValidate="TB_PasswordConfirm"
                        ErrorMessage="Confirmation du mot de passe"
                        OnServerValidate="CV_PasswordConfirm_ServerValidate"
                        ValidationGroup="Inscription"
                        ValidateEmptyText="true"
                        Display="None">
                    </asp:CustomValidator>
                    <asp:CustomValidator runat="server"
                        ID="CV_Email"
                        ControlToValidate="TB_Email"
                        ErrorMessage="Courriel"
                        OnServerValidate="CV_Email_ServerValidate"
                        ValidationGroup="Inscription"
                        ValidateEmptyText="true"
                        Display="None">
                    </asp:CustomValidator>
                    <asp:CustomValidator runat="server"
                        ID="CV_EmailConfirm"
                        ControlToValidate="TB_EmailConfirm"
                        ErrorMessage="La confirmation du courriel"
                        OnServerValidate="CV_EmailConfirm_ServerValidate"
                        ValidationGroup="Inscription"
                        ValidateEmptyText="true"
                        Display="None">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="CV_Captcha" runat="server"
                        ErrorMessage="Code captcha incorrect!"
                        ValidationGroup="Inscription"
                        ControlToValidate="TB_Captcha"
                        OnServerValidate="CV_Captcha_ServerValidate"
                        ValidateEmptyText="True">
                    </asp:CustomValidator>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
