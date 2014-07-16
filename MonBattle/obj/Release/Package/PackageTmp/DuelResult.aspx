<%@ Page Title="" Language="C#" MasterPageFile="~/MonBattle.Master" AutoEventWireup="true" CodeBehind="DuelResult.aspx.cs" Inherits="DuelResult" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/duel.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.greyout.js"></script>
    <script>
        $(document).ready(function () {
            $(".loser").duplicate();
            $(".greyClone").greyout();

            setTimeout(grayOutEffect, 1500);
        });

        function grayOutEffect() {
            $(".colorOriginal").stop().animate({ opacity: 0 }, 1500);
        }
    </script>
    
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="battlePanel" CssClass="master-content" runat="server">
        <div id = "versusPanel" class="row">
            <div id="you" class="opp-item">
                <p class="big"><%= self.Name %></p>
                <asp:Image ID="imgSelf" CssClass="thumbnail" runat="server" />
                <div>
                    <asp:Label ID="lblDamage" runat="server" Text="" />
                </div>
            </div>
            <div id="versusLogoDiv">
                <img id="versusLogo" class="duelResult" src="images/vs.png" alt="Versus" />
            </div>
            <div id="opponent" class="opp-item">
                <p class="big"><%= opponent.Name %></p>
                <asp:Image ID="imgOther" CssClass="thumbnail" runat="server" />
                <div style="height:22px;">
                </div>
            </div>
            <div>
                <asp:Literal ID="lblMessage" runat="server"></asp:Literal>
            </div>
        </div>
        <div>
            <asp:Label ID="lblBackMessage" runat="server" Text="Back to Battle Menu"></asp:Label>
            <asp:Button ID="btnBack" runat="server" Text="Back" PostBackUrl="~/Battle.aspx" />
        </div>
    </asp:Panel>
</asp:Content>


