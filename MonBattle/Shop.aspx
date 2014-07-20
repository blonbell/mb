<%@ Page Title="" Language="C#" MasterPageFile="~/MonBattle.Master" AutoEventWireup="true" CodeBehind="Shop.aspx.cs" Inherits="Shop" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/shop.css" rel="stylesheet" type="text/css" />

    <% if (finishTime.HasValue) { %>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
        <script src="js/shop.js"></script>
        <script>
            $(document).ready(function () {
                var trainD = new Date("<%= finishTime.Value.Year %>",
                                      "<%= finishTime.Value.Month - 1 %>",
                                      "<%= finishTime.Value.Day %>",
                                      "<%= finishTime.Value.Hour %>",
                                      "<%= finishTime.Value.Minute %>",
                                      "<%= finishTime.Value.Second %>");
                var t = new Date();
                if (t < trainD) {
                    $(".runFinish").hide();
                    startTime(trainD);
                }
            });
        </script>
    <% } %>
    <script>
        $(document).ready(function () {
            $(".desc-image").click(function () {
                console.log("Clicked");
                //$(this).prop("disabled", "true");
            });
        });
    </script>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="clear-float" style="height:200px;">
        <div style="float:left;">
            <h1><asp:Literal ID="litTrainingTitle" runat="server"></asp:Literal></h1> 
        </div>
        
        <div style="float:right;">
            <div class="char-table-panel">
                <asp:Image ID="charImg" CssClass="thumbnail" runat="server" />
            </div>
            <div class="char-table-panel">
                <p class="mid-align">
                    <img class="small-icon" src="images/HP-Icon.png" />
                    <asp:Label ID="charHp" CssClass="pad-left-5" runat="server" />
                </p>
                <p class="mid-align">
                    <img class="small-icon" src="images/Attack-Icon.png" />
                    <asp:Label ID="charAtk" CssClass="pad-left-5" runat="server" />
                </p>
                <p class="mid-align">
                    <img class="small-icon" src="images/Speed-Icon.png" />
                    <asp:Label ID="charSpd" CssClass="pad-left-5" runat="server" />
                </p> 
            </div> 
        </div>
    </div>

    <asp:Panel ID="shopPanel" CssClass="master-content" runat="server">
        <div class="wrapFrames">
            <div class="frame-border inline">
                <p>Train your Attack!</p>
                <asp:ImageButton ID="btnAtk" CssClass="desc-image mid-large-icon" ImageUrl="~/images/Attack-Icon.png" 
                    runat="server" onclick ="btnAtk_Click" />
                <p class="clear-float"><span class="desc-left">Effect</span><span class="desc-right">+<%= effect %>ATK</span></p>
                <p class="clear-float"><span class="desc-left">Cost</span><span class="desc-right"><%= usage %>MP</span></p>
                <p class="clear-float"><span class="desc-left">Time</span><span class="desc-right"><%= trainingHour %>hours</span></p>
            </div>
            <div class="frame-border inline">
                <p>Train your Health!</p>
                <asp:ImageButton ID="btnHp" CssClass="desc-image mid-large-icon" ImageUrl="~/images/HP-Icon.png" 
                    runat="server" OnClick="btnHp_Click" />
                <p class="clear-float"><span class="desc-left">Effect</span><span class="desc-right">+<%= effect %>HP</span></p>
                <p class="clear-float"><span class="desc-left">Cost</span><span class="desc-right"><%= usage %>MP</span></p>
                <p class="clear-float"><span class="desc-left">Time</span><span class="desc-right"><%= trainingHour %>hours</span></p>
            </div>
            <div class="frame-border inline">
                <p>Train your Speed!</p>
                <asp:ImageButton ID="btnSpd" CssClass="desc-image mid-large-icon" ImageUrl="~/images/Speed-Icon.png" 
                    runat="server" OnClick="btnSpd_Click" />
                <p class="clear-float"><span class="desc-left">Effect</span><span class="desc-right">+<%= effect %>SPD</span></p>
                <p class="clear-float"><span class="desc-left">Cost</span><span class="desc-right"><%= usage %>MP</span></p>
                <p class="clear-float"><span class="desc-left">Time</span><span class="desc-right"><%= trainingHour %>hours</span></p>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="trainingPanel" CssClass="master-content" runat="server" Visible="false">
        <div class="wrapFrames">
            <div class="frame-border inline width-90">
                <div id ="trainingPanel-left" class="inline">
                    <asp:Image ID="trainingImgIcon" CssClass="desc-image large-icon" runat="server" />
                    <p class="clear-float">
                        <span class="desc-left">Effect</span><span class="desc-right">
                            +<%= effect %><%= user.character.trainingType.ToString() %></span></p>
                </div>
                <div id = "trainingPanel-right" class="inline">
                    <div>
                        <asp:Label ID="lblTraining" runat="server" Text="" />
                    </div>
                    <div>
                        <asp:Label ID="lblTime" runat="server" Text="Time Left: " />
                        <span id="lblTimer">0:00:00</span>
                    </div>
                    <div>
                        <asp:Button ID="btnFinish" CssClass="runFinish" runat="server" Text="Finish!" OnClick="btnFinish_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Button runat="server" ID="btn_hiddenUnused" style="display:none" />

    <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modal-popup-background" runat="server" ID="popupext_vote" TargetControlID="btn_hiddenUnused" PopupControlID="pnl_popupVote" 
        OkControlID="btn_popupClose"></ajaxToolkit:ModalPopupExtender>
    
    <asp:Panel CssClass="panel-popup popup-vote" runat="server" ID="pnl_popupVote" DefaultButton="btn_popupClose">
        <div id="Div1" runat="server">
            <asp:Label runat="server" ID="lbl_popupMessage" Text="Thank you for voting!"></asp:Label>
            <br />
            <asp:Button runat="server" ID="btn_popupClose" Text="OK" />
        </div>
    </asp:Panel>
</asp:Content>