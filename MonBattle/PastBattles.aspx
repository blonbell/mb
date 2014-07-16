<%@ Page Title="" Language="C#" MasterPageFile="~/MonBattle.Master" AutoEventWireup="true" CodeBehind="PastBattles.aspx.cs" Inherits="MonBattle.PastBattles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="div-battles">

        <asp:GridView CssClass="grid-battles past-battles" runat="server" ID="grid_battles" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" Width="100%" OnRowDataBound="grid_battles_RowDataBound">

            <Columns>
                <asp:BoundField HeaderText="Won" DataField="VoteResult" ItemStyle-HorizontalAlign="Center" />


                <asp:TemplateField HeaderText="Battle Result" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# string.Format("({1}%) {0} VS {3} ({2}%)", Eval("CardOneName").ToString(), Eval("CardOneVotePercentage").ToString(), Eval("CardTwoVotePercentage").ToString(), Eval("CardTwoName").ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Battle Date" DataField="BattleDate" DataFormatString="{0:MMM d, yyyy}" HtmlEncode="false" SortExpression="BattleDate" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="100px" />


            </Columns>

            <AlternatingRowStyle BackColor="#5c9b9d" />

        </asp:GridView>

    </div>

</asp:Content>
