<%@ Page Title="" Language="C#" MasterPageFile="~/MonBattle.Master" AutoEventWireup="true" 
    CodeBehind="Default.aspx.cs" Inherits="MonBattle.Vote" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-mid">
        <a href="http://www.getmonstercards.com">Site Updates</a>
    </div>

    <asp:UpdatePanel runat="server" ID="updpnl_battle">
        <ContentTemplate>

            <div class="div-battle">

                <table style="width: 100%">
                    <tr>
                        <td colspan="3" style="text-align: center" runat="server" id="tblcell_login" visible="false">
                            <a class="hyperlink" href="Login.aspx">Login</a> or <a class="hyperlink" href="Register.aspx">register</a> to vote!

                            <p style="text-align:center"><a class="hyperlink" href="About.aspx">What is Monbattle? Click to learn.</a></p>
                        </td>

                        <td colspan="3" style="text-align:center">
                            <asp:Label runat="server" ID="lbl_voteMessage" Text="Click on the card to vote!" Font-Size="14px" style="display:block; margin-bottom: 5px"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="3" style="text-align: center">
                            <iframe runat="server" id="countDownClock" visible="false" frameborder="0" width="191" height="52"></iframe>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align:center">
                            <asp:Label runat="server" ID="lbl_cardOne" style="display:block; margin-bottom: 5px"></asp:Label>
                        </td>

                        <td>

                        </td>

                        <td style="text-align:center">
                            <asp:Label runat="server" ID="lbl_cardTwo" style="display:block; margin-bottom: 5px"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 200px">
                            <asp:ImageButton runat="server" ID="imgbtn_cardOne" Visible="false" Width="200px" OnClick="imgbtn_cardOne_Click" />
                            <ajaxToolkit:ConfirmButtonExtender ID="conbtnext_cardOne" runat="server" ConfirmText="Are you sure you want to pick this card?" TargetControlID="imgbtn_cardOne"></ajaxToolkit:ConfirmButtonExtender>
                        </td>

                        <td style="text-align: center; vertical-align: middle; font-size: 16px; color: #4e6565">
                            <asp:Image runat="server" ID="img_vs" ImageUrl="~/images/vs.png" Visible="false" />
                        </td>

                        <td style="width: 200px; text-align:right">
                            <asp:ImageButton runat="server" ID="imgbtn_cardTwo" Visible="false" Width="200px" OnClick="imgbtn_cardTwo_Click" />
                            <ajaxToolkit:ConfirmButtonExtender ID="conbtnext_cardTwo" runat="server" ConfirmText="Are you sure you want to pick this card?" TargetControlID="imgbtn_cardTwo"></ajaxToolkit:ConfirmButtonExtender>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="3" style="text-align:center">
                            <asp:Label runat="server" ID="lbl_message" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>

                <table runat="server" id="tbl_tomorrowBattle" style="width: 100%; font-size: 13px; color: #4e6565" visible="false">
                    <tr>
                        <td colspan="3" style="text-align:center">
                            <br />
                            <br />
                            Tomorrow's battle...
                            <br />
                            <br />
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 250px; text-align:right; padding-right: 10px">
                            <asp:Image runat="server" ID="img_cardOneTomorrow" Visible="false" Width="100px" />
                        </td>

                        <td style="text-align: center; vertical-align: middle">
                            <asp:Image runat="server" ID="img_vsTomorrow" ImageUrl="~/images/vs.png" Width="30px" />
                        </td>

                        <td style="width: 250px; text-align:left; padding-left: 10px">
                            <asp:Image runat="server" ID="img_cardTwoTomorrow" Visible="false" Width="100px" />
                        </td>
                    </tr>
                </table>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Button runat="server" ID="btn_hiddenUnused" style="display:none" />

    <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modal-popup-background" runat="server" ID="popupext_vote" TargetControlID="btn_hiddenUnused" PopupControlID="pnl_popupVote" 
        OkControlID="btn_popupClose"></ajaxToolkit:ModalPopupExtender>
    
    <asp:Panel CssClass="panel-popup popup-vote" runat="server" ID="pnl_popupVote" DefaultButton="btn_popupClose">
        <div id="Div1" runat="server">
            <asp:Label runat="server" ID="lbl_popupMessage" Text="Thank you for voting!"></asp:Label>
            <br />
            <asp:Button runat="server" ID="btn_popupClose" Text="OK" OnClientClick="window.location='Default.aspx';" />
        </div>
    </asp:Panel>

</asp:Content>
