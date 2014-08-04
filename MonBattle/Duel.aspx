<%@ Page Title="" Language="C#" MasterPageFile="~/MonBattle.Master" AutoEventWireup="true" CodeBehind="Duel.aspx.cs" Inherits="Duel" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/duel.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="battlePanel" CssClass="master-content" runat="server">
        <asp:Literal ID="charName" runat="server"></asp:Literal><br/>
        <asp:Literal ID="charHp" runat="server"></asp:Literal>
        <asp:Literal ID="charMeter" runat="server"></asp:Literal>

        <asp:Panel ID="MoveSetPanel" runat="server">

        </asp:Panel>

        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="btnAtk" runat="server" Text="Atk" OnClick="btnAtk_Click" />
        <asp:Button ID="btnDuel" runat="server" Text="Skill" OnClick="btnDuel_Click" />
        <asp:Button ID="btnRematch" runat="server" Text="Duel Again?" OnClick="btnRematch_Click" Visible="false" />
        <asp:Literal ID="opName" runat="server"></asp:Literal><br/>
        <asp:Literal ID="opHp" runat="server"></asp:Literal>
        <asp:Literal ID="opMeter" runat="server"></asp:Literal>

        <div style="width:70%; margin:auto; border: solid 1px red;">
            <asp:Literal ID="battleLog" runat="server"></asp:Literal>
        </div>
    </asp:Panel>
</asp:Content>
