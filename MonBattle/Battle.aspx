<%@ Page Title="" Language="C#" MasterPageFile="~/MonBattle.Master" AutoEventWireup="true" CodeBehind="Battle.aspx.cs" Inherits="MonBattle.Battle" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/battle.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="battlePanel" CssClass="master-content" runat="server">
        <h1>Battle</h1>
        <asp:Label ID="lblBCounter" CssClass="battle-counter" runat="server" Text=""></asp:Label>
        <h3>Select an opponent</h3>
        <asp:Panel ID="oppContainer" runat="server">
            
        </asp:Panel>
    </asp:Panel>
</asp:Content>

<!-- Todo make duel page prototype -->