<%@ Page Title="" Language="C#" MasterPageFile="~/MonBattle.Master" AutoEventWireup="true" CodeBehind="ViewCharacter.aspx.cs" Inherits="ViewCharacter" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/characters.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel CssClass="text-mid view-character-panel" ID="viewPanel" runat="server">
        <asp:Image ID="imgAvatar" CssClass="smallprint" runat="server" />
        <div>
            <asp:Label ID="lblName" runat="server" Text="Name: "/>
            <span><%= character.Name %></span>
        </div>
        <div>
            <p>Statistics</p>
            <div>
                <img class="small-icon" src="images/HP-Icon.png" />
                <span><%= character.Health %>/<%= character.MaxHealth %></span></div>
            <div>
                <img class="small-icon" src="images/Attack-Icon.png" />
            </div>
            <div>
                <img class="small-icon" src="images/Speed-Icon.png" />
            </div>
        </div>
        <div>
            <p>Move List</p>
            <p>Select four skills from the ddl and add to character</p>
            <asp:DropDownList ID="moveList" runat="server"></asp:DropDownList>
            <asp:Button ID="btnAddMove" runat="server" Text="Add Skill" OnClick="btnAddMove_Click" />
        </div>
    </asp:Panel>
    <asp:Panel CssClass="text-mid view-character-panel" ID="noCharFoundPanel" runat="server">
        <p>We did not find any info on the character specified.</p>
    </asp:Panel>
</asp:Content>
