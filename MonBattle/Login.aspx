<%@ Page Title="" Language="C#" MasterPageFile="~/MonBattle.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MonBattle.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="div-login">

        <asp:Panel runat="server" ID="pnl_login" DefaultButton="btn_login">

            <h2 style="text-align:center">
                <a class="hyperlink" href="About.aspx">
                    How Monbattle Works. Click to learn.
                </a>
            </h2><br>

            <div class="div-login-title">
                Login to MonBattle!
            </div>

            <table>
                <tr>
                    <td>
                        Email:
                    </td>

                    <td>
                        <asp:TextBox CssClass="login-textbox" runat="server" ID="txt_email" Width="190px"></asp:TextBox>

                        <asp:RequiredFieldValidator CssClass="required-field" runat="server" ID="reqfldval_email" ControlToValidate="txt_email" ValidationGroup="Login" Text="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        Password:
                    </td>

                    <td>
                        <asp:TextBox CssClass="login-textbox" runat="server" ID="txt_password" Width="190px" TextMode="Password"></asp:TextBox>

                        <asp:RequiredFieldValidator CssClass="required-field" runat="server" ID="reqfldval_password" ControlToValidate="txt_password" ValidationGroup="Login" Text="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>

<%--                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:CheckBox runat="server" ID="chkbox_rememberMe" Text="Remember me" TextAlign="Right" />
                    </td>
                </tr>--%>

                <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:Label CssClass="password-confirm-validator" runat="server" ID="lbl_incorrectLogin" Text="Email or password incorrect." Visible="false"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:Button CssClass="login-button" runat="server" ID="btn_login" Text="Login" ValidationGroup="Login" OnClick="btn_login_Click" />
                    </td>
                </tr>
            </table>

        </asp:Panel>

        <asp:Panel runat="server" ID="pnl_username" Visible="false" DefaultButton="btn_createUsername">

            <div class="div-login-title">
                Please create a username
            </div>

            <table>
                <tr>
                    <td>
                        Username
                    </td>

                    <td>
                        <asp:TextBox CssClass="login-textbox" runat="server" ID="txt_username" Width="190px"></asp:TextBox>

                        <asp:RequiredFieldValidator CssClass="required-field" runat="server" ID="reqfldval_username" ControlToValidate="txt_username" ValidationGroup="CreateUsername" Text="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="text-align: center; padding: 0; margin: 0">
                        <asp:RegularExpressionValidator CssClass="password-confirm-validator" ID="regexpval_usernameLength" runat="server" ControlToValidate="txt_username" 
                            ErrorMessage="Username must be 3-24 characters." ValidationExpression=".{3}.*" Display="Dynamic" ValidationGroup="CreateUsername" />
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="text-align: center; padding: 0; margin: 0">
                        <asp:RegularExpressionValidator CssClass="password-confirm-validator" ID="regexpval_username" runat="server" ValidationExpression="^[a-zA-Z0-9_]*$" 
                            ControlToValidate="txt_username" ErrorMessage="Username must be alphanumeric." Display="Dynamic" ValidationGroup="CreateUsername"></asp:RegularExpressionValidator>
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:Button CssClass="login-button" runat="server" ID="btn_createUsername" Text="Create" ValidationGroup="CreateUsername" OnClick="btn_createUsername_Click" />
                    </td>
                </tr>
            </table>

        </asp:Panel>

        <asp:Button runat="server" ID="btn_hiddenUnused3" style="display:none" />

        <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modal-popup-background" runat="server" ID="popupext_username" TargetControlID="btn_hiddenUnused3" PopupControlID="pnl_popupUsername" 
            OkControlID="btn_popupUsernameClose"></ajaxToolkit:ModalPopupExtender>
    
        <asp:Panel CssClass="panel-popup popup-registration" runat="server" ID="pnl_popupUsername" DefaultButton="btn_popupUsernameClose">
            <div id="Div1" runat="server">
                <asp:Label runat="server" ID="lbl_username" Text="Username successfully created!"></asp:Label>
                <br />
                <asp:Button runat="server" ID="btn_popupUsernameClose" Text="OK" />
            </div>
        </asp:Panel>

        <asp:Button runat="server" ID="btn_hiddenUnused2" style="display:none" />

        <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modal-popup-background" runat="server" ID="popupext_usernameUsed" TargetControlID="btn_hiddenUnused2" PopupControlID="pnl_popupUsernameUsed" 
            OkControlID="btn_popupUsernameUsedClose"></ajaxToolkit:ModalPopupExtender>
    
        <asp:Panel CssClass="panel-popup popup-registration" runat="server" ID="pnl_popupUsernameUsed" DefaultButton="btn_popupUsernameUsedClose">
            <div id="Div2" runat="server">
                <asp:Label runat="server" ID="lbl_usernameUsed" Text="Username already registered."></asp:Label>
                <br />
                <asp:Button runat="server" ID="btn_popupUsernameUsedClose" Text="OK" />
            </div>
        </asp:Panel>

    </div>

</asp:Content>
