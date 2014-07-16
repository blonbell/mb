<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Battle.aspx.cs" Inherits="MonBattle.Admin.Battle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

        function keyPressForCalendarTextbox(textbox, e) {

            var keynum;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }

            if (keynum != null) {
                // Remove date when backspace pressed
                if (keynum = "8") {
                    textbox.value = "";
                    return false;
                }
                // Let tab and enter go through
                else if (keynum == "9" || keynum == "13") {
                    return true;
                }
                // Otherwise prevent key press
                else {
                    return false;
                }
            }
        }

    </script>

    <asp:UpdatePanel runat="server" ID="updpnl_battle">
        <ContentTemplate>

            <div class="div-battle">

                <table style="width: 100%">

                    <tr>
                        <td colspan="3" style="text-align: center; font-size: 14px; padding-bottom: 10px">
                            Battle Date:&nbsp;
                            <asp:TextBox runat="server" ID="txt_battleDate" Width="100px" OnKeyDown="keyPressForCalendarTextbox(this, event);"></asp:TextBox>
                            
                            <ajaxToolkit:CalendarExtender runat="server" ID="calext_battleDate" TargetControlID="txt_battleDate" Format="MMM dd, yyyy"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator runat="server" ID="reqfldval_battleDate" ControlToValidate="txt_battleDate" CssClass="required-field" Text="*" ValidationGroup="SaveBattle"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 48%; text-align: left">
                            <asp:DropDownList runat="server" ID="drpdwn_cardOne" AppendDataBoundItems="true" OnSelectedIndexChanged="drpdwn_cardOne_SelectedIndexChanged" AutoPostBack="true" Width="200px">
                                <asp:ListItem Text="-- Select --" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td rowspan="2" style="width: 4%; text-align: center; vertical-align: middle; font-size: 16px; color: #4e6565">
                            <asp:Image runat="server" ID="img_vs" ImageUrl="~/images/vs.png" />
                        </td>

                        <td style="width: 48%; text-align:right">
                            <asp:DropDownList runat="server" ID="drpdwn_cardTwo" AppendDataBoundItems="true" OnSelectedIndexChanged="drpdwn_cardTwo_SelectedIndexChanged" AutoPostBack="true" Width="200px">
                                <asp:ListItem Text="-- Select --" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Image runat="server" ID="img_cardOne" Visible="false" Width="200px" />
                        </td>



                        <td style="text-align:right">
                            <asp:Image runat="server" ID="img_cardTwo" Visible="false" Width="200px" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="3" style="text-align:center">
                            <asp:Button CssClass="save-battle-button" runat="server" ID="btn_addBattle" Text="Add Battle" Visible="false" ValidationGroup="SaveBattle" OnClick="btn_addBattle_Click" />
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align:left">
                            <asp:Label runat="server" ID="lbl_cardOneVotes" Visible="false" Width="200px" style="text-align:center"></asp:Label>
                        </td>

                        <td>

                        </td>

                        <td style="text-align:right">
                            <asp:Label runat="server" ID="lbl_cardTwoVotes" Visible="false" Width="200px" style="text-align:center"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
