<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Cards.aspx.cs" Inherits="MonBattle.Admin.Cards" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="div-cards">

        <asp:GridView CssClass="grid-cards" runat="server" ID="grid_cards" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" Width="100%">

            <Columns>
                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" ID="hyplink_card" Text='<%# Eval("CardID") %>' NavigateUrl='<%# Eval("CardID", "Card.aspx?ID={0}") %>' Font-Bold="true"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" ID="hyplink_card" Text='<%# Eval("Name") %>' NavigateUrl='<%# Eval("CardID", "Card.aspx?ID={0}") %>' Font-Bold="true"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Date Added" DataField="DateCreated" DataFormatString="{0:MMM d, yyyy}" HtmlEncode="false" SortExpression="DateCreated" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="100px" />

            </Columns>

        </asp:GridView>

    </div>

</asp:Content>
