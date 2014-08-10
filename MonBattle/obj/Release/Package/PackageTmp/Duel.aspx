<%@ Page Title="" Language="C#" MasterPageFile="~/MonBattle.Master" AutoEventWireup="true" CodeBehind="Duel.aspx.cs" Inherits="Duel" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <link href="css/duel.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />


    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css">    
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>

    <script type="text/javascript">

        function abilityAttack() {

            $('#GBAlexImg').animate({ right: '300', bottom: '80' }, 100, function () {
                $('#GBAlexImg').animate({ right: '600', bottom: '40' }, 150, function () {
                    $('#GBAlexImg').animate({ right: '40', bottom: '20' }, 300, 'swing', function () {

                        return true;
                    });
                });
            });
        }

        $(document).ready(function () {

            $('#GBAlexAbil1').click(function () {
                $('#GBAlexImg').animate({ left: '120', top: '120' }, 500, function () {
                    $('#GBAlexImg').animate({ left: '700', top: '120' }, 500, function () {
                        // $('#SquirtImg').animate({left:'100', top'50'}, 500);
                    });
                });
            });

            $('#GBAlexAbil2').click(function () {
                $('#GBAlexImg').animate({ left: '120', top: '120' }, 500, function () {
                    $('#GBAlexImg').animate({ left: '700', top: '120' }, 500, function () {
                        // $('#SquirtImg').animate({left:'100', top'50'}, 500);
                    });
                });
            });

            $('#GBAlexAbil3').click(function () {
                $('#GBAlexImg').animate({ left: '120', top: '120' }, 500, function () {
                    $('#GBAlexImg').animate({ left: '700', top: '120' }, 500, function () {
                        // $('#SquirtImg').animate({left:'100', top'50'}, 500);
                    });
                });
            });

            $('#GBAlexAbil4').click(function () {
                $('#GBAlexImg').animate({ left: '120', top: '120' }, 500, function () {
                    $('#GBAlexImg').animate({ left: '700', top: '120' }, 500, function () {
                        // $('#SquirtImg').animate({left:'100', top'50'}, 500);
                    });
                });
            });
        });

    </script>

    <script language="javascript" type="text/javascript">

         var amountToMoveTotal = 900;
         var amountToMovePerStep = 30;
         var timeToWaitBeforeNextIncrement = 10;
    </script>

    <div class="battle-page">

        <asp:UpdatePanel runat="server" ID="updpnl_duel">
            <ContentTemplate>

                <div style="width: 100%; height: 400px; position: relative; border: 2px solid #4e6565; border-radius: 4px">

                    <!-- Player Character Pic -->
                    <div runat="server" id="div_characterPic" class="img-circular" style="background-image: url('/images/GBAlexSm.png'); right:150px; top: 10px; position: absolute">
                           
                    </div>

                    <!-- Player character stats -->
                    <div style="position: absolute; top: 10px; right: 10px; text-align: right; width: 150px; text-align: center; padding: 5px 10px; background-color: #444444; opacity: 0.8; 
                            box-shadow: rgba(180,180,180, 0.7) 4px 4px; color: #000000; border-radius: 4px; border: 2px solid #222222">
                        
                        <%--<asp:Label runat="server" ID="lbl_characterName" style="color: #FFFFFF"></asp:Label>--%>

                        <!-- Status Bar --- Implemented in the future -->
                        <div>

                        </div>

                        <!-- HP Bar -->
                        <div>
                            <div style="width: 100%; border: 2px solid #000000; border-radius: 4px; height: 20px; padding: 0; position: relative; background-color: #FFFFFF">
                                <asp:Image runat="server" ID="img_playerHP" ImageUrl="~/images/hp_green.png" Height="16px" Width="100%" style="padding: 0; margin: 0; top: 0; left: 0; position:absolute" />

                                <div style="position: absolute; margin-left: auto; margin-right: auto; text-align: center; padding: 0 5px 2px 5px; font-size: 12px; width: 100%">
                                    <b>HP&nbsp;
                                    <asp:Label runat="server" ID="lbl_playerHP"></asp:Label>
                                    </b>
                                </div>
                            </div>
                        </div>

                        <div style="width: 100%; border: 2px solid #000000; border-radius: 4px; height: 20px; padding: 0; margin: 5px 0; position: relative; background-color: #FFFFFF">
                            <asp:Image runat="server" ID="img_playerAP" ImageUrl="~/images/ap_blue.png" Height="16px" style="padding: 0; margin: 0; top: 0; left: 0; position: absolute" />

                            <div style="position: absolute; margin-left: auto; margin-right: auto; text-align: center; padding: 0 10px 2px 10px; font-size: 12px; width: 100%">
                                <b>AP&nbsp;
                                <asp:Label runat="server" ID="lbl_playerAP"></asp:Label>
                                </b>
                            </div>
                        </div>
                    </div>

                    <!-- Enemy Character Pic -->
                    <div runat="server" id="div1" class="img-circular" style="background-image: url('/images/squirtle.png'); left:150px; top: 10px; position: absolute">
                           
                    </div>

                    <!-- Enemy stats -->
                    <div style="position: absolute; top: 10px; left: 10px; text-align: left; width: 150px; text-align: center; padding: 5px 10px; background-color: #444444; opacity: 0.8; 
                            box-shadow: rgba(180,180,180, 0.7) -4px 4px; color: #000000; border-radius: 4px; border: 2px solid #222222">
                        
                        <!-- Status Bar --- Implemented in the future -->
                        <div>

                        </div>

                        <!-- HP Bar -->
                        <div>
                            <div style="width: 100%; border: 2px solid #000000; border-radius: 4px; height: 20px; padding: 0; position: relative; background-color: #FFFFFF">
                                <asp:Image runat="server" ID="img_enemyHP" ImageUrl="~/images/hp_green.png" Height="16px" Width="100%" style="padding: 0; margin: 0; top: 0; left: 0; position:absolute" />

                                <div style="position: absolute; margin-left: auto; margin-right: auto; text-align: center; padding: 0 5px 2px 5px; font-size: 12px; width: 100%">
                                    <b>HP&nbsp;
                                    <asp:Label runat="server" ID="lbl_enemyHP"></asp:Label>
                                    </b>
                                </div>
                            </div>
                        </div>

                        <%--<div style="width: 100%; border: 2px solid #000000; border-radius: 4px; height: 20px; padding: 0; margin: 5px 0; position: relative; background-color: #FFFFFF">
                            <asp:Image runat="server" ID="img_enemyAP" ImageUrl="~/images/ap_blue.png" Height="16px" style="padding: 0; margin: 0; top: 0; left: 0; position: absolute" />

                            <div style="position: absolute; margin-left: auto; margin-right: auto; text-align: center; padding: 0 10px 2px 10px; font-size: 12px; width: 100%">
                                <b>AP&nbsp;
                                <asp:Label runat="server" ID="lbl_enemyAP"></asp:Label>
                                </b>
                            </div>
                        </div>--%>
                    </div>
                    
            
                    <img id="GBAlexImg" src="/images/GBAlexSm.png" style="right:40px; bottom:20px; position:absolute; opacity:1.0" />
                    <img id="SquirtImg" src="/images/squirtle.png" style="left:40px; bottom:20px; position:absolute; opacity:1.0" />


                    <div style="position: absolute; width:50%; top: 10%; left: 25%; right: 25%; font-size: 12px; text-align: center">
                        <asp:Literal ID="battleLog" runat="server"></asp:Literal>
                    </div>

                </div>

                <div class="moninfo-area">
     
                    <div runat="server" id="monster1" class="monster-controls" style="margin: 5px 0; text-align: right; font-size: 13px">
                        <asp:Button CssClass="button-ability-enabled" ID="btnAtk" runat="server" Text="Attack" OnClientClick="return abilityAttack();" OnClick="btnAtk_Click" />
                        <asp:Button CssClass="button-ability-disabled" ID="btn_ability1" runat="server" ind="0" OnClientClick="return abilityAttack();" OnClick="btnDuel_Click" />
                        <asp:Button CssClass="button-ability-disabled" ID="btn_ability2" runat="server" ind="1" OnClientClick="return abilityAttack();" OnClick="btnDuel_Click" />
                        <asp:Button CssClass="button-ability-disabled" ID="btn_ability3" runat="server" ind="2" OnClientClick="return abilityAttack();" OnClick="btnDuel_Click" />
                        <asp:Button CssClass="button-ability-disabled" ID="btn_ability4" runat="server" ind="3" OnClientClick="return abilityAttack();" OnClick="btnDuel_Click" />       
                    </div>
                </div>

                <asp:Panel ID="battlePanel" CssClass="master-content" runat="server">
     
                    <asp:Button ID="btnRematch" runat="server" Text="Duel Again?" OnClick="btnRematch_Click" Visible="false" />

                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>

        
    </div>


    
</asp:Content>
