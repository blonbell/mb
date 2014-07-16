<%@ Page Title="" Language="C#" MasterPageFile="~/MonBattle.Master" AutoEventWireup="true" CodeBehind="Duel.aspx.cs" Inherits="Duel" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/duel.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />
    <script>
        $(document).ready(function () {
            $("#atkBoostSlider").slider({
                range: "min",
                max: 200,
                min: 101,
                value: 180,
                slide: function (event, ui) {
                    $("#atkBoost").text($(this).slider("value") + "%");
                    $("#succRate").text(calcSuccessRate($(this).slider("value")) + "%");
                    $("input[id*='abs']").val($(this).slider("value"));

                    var r = ($(this).slider("value") - 101) * (255 / 100);
                    var b = (200 - $(this).slider("value")) * (255 / 100);
                    var rgb = "rgb(" + parseInt(r) + ",5," + parseInt(b) + ") 0 0 8px";

                    $(this).css("border-color", rgb);
                    $(this).css("box-shadow", rgb);
                },
                change: function (event, ui) {
                    $("#atkBoost").text($(this).slider("value") + "%");
                    $("#succRate").text(calcSuccessRate($(this).slider("value")) + "%");
                    $("input[id*='abs']").val($(this).slider("value"));

                    var r = ($(this).slider("value") - 101) * (255 / 100);
                    var b = (200 - $(this).slider("value")) * (255 / 100);
                    var rgb = "rgb(" + parseInt(r) + ",5," + parseInt(b) + ") 0 0 8px";

                    $(this).css("border-color", rgb);
                    $(this).css("box-shadow", rgb);
                }
            });

            $("#atkBoost").text($("#atkBoostSlider").slider("value") + "%");
            $("#succRate").text(calcSuccessRate($("#atkBoostSlider").slider("value")) + "%");
        });

        function calcSuccessRate(value) {
            var num = (1 - Math.sqrt(value - 100) / 10) * 100;
            return parseFloat(num).toFixed(2);
        }
    </script>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="battlePanel" CssClass="master-content" runat="server">
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
        <div id = "versusPanel" class="row">
            <div id="you" class="opp-item">
                <p class="big"><%= self.Name %></p>
                <asp:Image ID="imgSelf" CssClass="thumbnail" runat="server" />
                <div class="stat-bar">
                    <asp:Image ID="atkIcon" ImageUrl="~/images/Attack-Icon.png" CssClass="medium-icon" runat="server" />
                    <asp:Literal ID="litAtk" runat="server"></asp:Literal>
                </div>
            </div>
            <div id="versusLogoDiv">
                <img id="versusLogo" src="images/vs.png" alt="Versus" />
            </div>
            <div id="opponent" class="opp-item">
                <p class="big"><%= opponent.Name %></p>
                <asp:Image ID="imgOther" CssClass="thumbnail" runat="server" />
                <div class="stat-bar">
                    <asp:Image ID="hpIcon" ImageUrl="~/images/Hp-Icon.png" CssClass="medium-icon" runat="server" />
                    <asp:Literal ID="litHp" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
        <div id="controlPanel" class="row">
            <div id="atkBoostSlider"></div>
            <asp:HiddenField ID="abs" runat="server" value="180"/>
            <p>Attack Boost = <span id="atkBoost" class="atkBoost">150%</span> x ATK </p>
            <p>Chance of Success = <span id="succRate"></span></p>
            <asp:Button ID="btnDuel" runat="server" Text="Duel" OnClick="btnDuel_Click" />
        </div>
    </asp:Panel>
</asp:Content>
