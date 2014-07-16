<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Battles.aspx.cs" Inherits="MonBattle.Admin.Battles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="div-battles">

        <asp:GridView CssClass="grid-battles" runat="server" ID="grid_battles" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" Width="100%" OnRowDataBound="grid_battles_RowDataBound">

            <Columns>
                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                    <ItemTemplate>
                        <asp:HyperLink CssClass="grid-battles-link" runat="server" ID="hyplink_cardBattle" Text='<%# Eval("CardBattleID") %>' NavigateUrl='<%# Eval("CardBattleID", "Battle.aspx?ID={0}") %>' Font-Bold="true"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Card One">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lbl_cardOne" Text='<%# Eval("CardOneID") %>' Visible="false"></asp:Label>
                        <asp:HyperLink CssClass="grid-battles-link" runat="server" ID="hyplink_cardOne" Text='<%# Eval("CardOneName") %>' NavigateUrl='<%# Eval("CardOneID", "Card.aspx?ID={0}") %>' Font-Bold="true"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Card Two">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lbl_cardTwo" Text='<%# Eval("CardTwoID") %>' Visible="false"></asp:Label>
                        <asp:HyperLink CssClass="grid-battles-link" runat="server" ID="hyplink_cardTwo" Text='<%# Eval("CardTwoName") %>' NavigateUrl='<%# Eval("CardTwoID", "Card.aspx?ID={0}") %>' Font-Bold="true"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Battle Date" DataField="BattleDate" DataFormatString="{0:MMM d, yyyy}" HtmlEncode="false" SortExpression="BattleDate" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="100px" />

                <asp:TemplateField HeaderText="Winner" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lbl_winner" Text='<%# Eval("WinnerCardID") %>' Visible="false"></asp:Label>
                        <asp:Button runat="server" ID="btn_calculateWinner" Text="Announce" Visible="false" OnCommand="btn_calculateWinner_Command" CommandArgument='<%# Eval("CardBattleID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

        </asp:GridView>

    </div>

    <asp:Button runat="server" ID="btn_hiddenUnused" style="display:none" />

    <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modal-popup-background" runat="server" ID="popupext_calculateWinner" TargetControlID="btn_hiddenUnused" PopupControlID="pnl_popupCalculateWinner" 
        OkControlID="btn_popupClose"></ajaxToolkit:ModalPopupExtender>
    
    <asp:Panel CssClass="panel-popup popup-battle-winner" runat="server" ID="pnl_popupCalculateWinner" DefaultButton="btn_popupClose">
        <div id="Div1" runat="server">
            <asp:Label runat="server" ID="lbl_successfulCalculate" Text="Battle results announced!"></asp:Label>
            <br />
            <asp:Button runat="server" ID="btn_popupClose" Text="OK" />
        </div>
    </asp:Panel>

</asp:Content>
