<%@ Page Title="" Language="C#" MasterPageFile="~/MonBattle.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MonBattle.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="div-register">

        <asp:Panel runat="server" ID="pnl_register" DefaultButton="btn_register">

            <p style="text-align:center"><a class="hyperlink" href="About.aspx">What is Monbattle? Click to learn.</a></p>

            <div class="div-register-title">
                Register for MonBattle!
            </div>

            <table>
                <tr>
                    <td>
                        Email:
                    </td>

                    <td>
                        <asp:TextBox CssClass="register-textbox" runat="server" ID="txt_email" Width="190px" AutoCompleteType="None"></asp:TextBox>

                        <asp:RequiredFieldValidator CssClass="required-field" runat="server" ID="reqfldval_email" ControlToValidate="txt_email" ValidationGroup="Register" Text="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        Username:
                    </td>

                    <td>
                        <asp:TextBox CssClass="register-textbox" runat="server" ID="txt_username" Width="190px" AutoCompleteType="None" MaxLength="24"></asp:TextBox>

                        <asp:RequiredFieldValidator CssClass="required-field" runat="server" ID="reqfldval_username" ControlToValidate="txt_username" ValidationGroup="Register" Text="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        Password:
                    </td>

                    <td>
                        <asp:TextBox CssClass="register-textbox" runat="server" ID="txt_password" Width="190px" TextMode="Password" MaxLength="16" AutoCompleteType="None"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator CssClass="required-field" runat="server" ID="reqfldval_password" ControlToValidate="txt_password" ValidationGroup="Register" Text="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>
                        Confirm Password:
                    </td>

                    <td>
                        <asp:TextBox CssClass="register-textbox" runat="server" ID="txt_confirmPassword" Width="190px" TextMode="Password" MaxLength="16" AutoCompleteType="None"></asp:TextBox>

                        <asp:RequiredFieldValidator CssClass="required-field" runat="server" ID="reqfldval_confirmPassword" ControlToValidate="txt_confirmPassword" ValidationGroup="Register" Text="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="text-align: center; padding:0; margin: 0">
                        <asp:CompareValidator CssClass="password-confirm-validator" runat="server" ID="cmpval_confirmPassword" ControlToValidate="txt_confirmPassword" ControlToCompare="txt_password" 
                            ValidationGroup="Register" Text="Passwords do not match." Display="Dynamic"></asp:CompareValidator>
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="text-align: center; padding: 0; margin: 0">
                        <asp:RegularExpressionValidator CssClass="password-confirm-validator" ID="regexpval_password" runat="server" ControlToValidate="txt_password" 
                            ErrorMessage="Password must be 8-16 characters." ValidationExpression=".{8}.*" Display="Dynamic" ValidationGroup="Register" />
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="text-align: center; padding: 0; margin: 0">
                        <asp:RegularExpressionValidator CssClass="password-confirm-validator" ID="regexpval_email" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                            ControlToValidate="txt_email" ErrorMessage="Invalid email format." Display="Dynamic" ValidationGroup="Register"></asp:RegularExpressionValidator>
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="text-align: center; padding: 0; margin: 0">
                        <asp:RegularExpressionValidator CssClass="password-confirm-validator" ID="regexpval_usernameLength" runat="server" ControlToValidate="txt_username" 
                            ErrorMessage="Username must be 3-24 characters." ValidationExpression=".{3}.*" Display="Dynamic" ValidationGroup="Register" />
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="text-align: center; padding: 0; margin: 0">
                        <asp:RegularExpressionValidator CssClass="password-confirm-validator" ID="regexpval_username" runat="server" ValidationExpression="^[a-zA-Z0-9_]*$" 
                            ControlToValidate="txt_username" ErrorMessage="Username must be alphanumeric." Display="Dynamic" ValidationGroup="Register"></asp:RegularExpressionValidator>
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:Button CssClass="register-button" runat="server" ID="btn_register" Text="Register" ValidationGroup="Register" OnClick="btn_register_Click" />
                    </td>
                </tr>
            </table>

        </asp:Panel>

    </div>

    <asp:Button runat="server" ID="btn_hiddenUnused" style="display:none" />

    <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modal-popup-background" runat="server" ID="popupext_register" TargetControlID="btn_hiddenUnused" PopupControlID="pnl_popupRegister" 
        OkControlID="btn_popupClose"></ajaxToolkit:ModalPopupExtender>
    
    <asp:Panel CssClass="panel-popup popup-registration" runat="server" ID="pnl_popupRegister" DefaultButton="btn_popupClose">
        <div runat="server">
            <asp:Label runat="server" ID="lbl_successfulRegister" Text="Welcome to Monbattle! Please log in again. You have received 15MP (monbattle points)!" style="text-wrap:normal" Width="350px"></asp:Label>
            <br />
            <asp:Button runat="server" ID="btn_popupClose" Text="OK" OnClientClick="window.location='Login.aspx';" />
        </div>
    </asp:Panel>


    <asp:Button runat="server" ID="btn_hiddenUnused2" style="display:none" />

    <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modal-popup-background" runat="server" ID="popupext_emailUsed" TargetControlID="btn_hiddenUnused2" PopupControlID="pnl_popupEmailUsed" 
        OkControlID="btn_popupEmailUsedClose"></ajaxToolkit:ModalPopupExtender>
    
    <asp:Panel CssClass="panel-popup popup-registration" runat="server" ID="pnl_popupEmailUsed" DefaultButton="btn_popupEmailUsedClose">
        <div runat="server">
            <asp:Label runat="server" ID="lbl_emailUsed" Text="Email already registered."></asp:Label>
            <br />
            <asp:Button runat="server" ID="btn_popupEmailUsedClose" Text="OK" />
        </div>
    </asp:Panel>

    <asp:Button runat="server" ID="btn_hiddenUnused3" style="display:none" />

    <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modal-popup-background" runat="server" ID="popupext_usernameUsed" TargetControlID="btn_hiddenUnused3" PopupControlID="pnl_popupUsernameUsed" 
        OkControlID="btn_popupUsernameUsedClose"></ajaxToolkit:ModalPopupExtender>
    
    <asp:Panel CssClass="panel-popup popup-registration" runat="server" ID="pnl_popupUsernameUsed" DefaultButton="btn_popupUsernameUsedClose">
        <div runat="server">
            <asp:Label runat="server" ID="lbl_usernameUsed" Text="Username already registered."></asp:Label>
            <br />
            <asp:Button runat="server" ID="btn_popupUsernameUsedClose" Text="OK" />
        </div>
    </asp:Panel>

</asp:Content>
