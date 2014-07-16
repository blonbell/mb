<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" 
    CodeBehind="ResetBattleCounter.aspx.cs" Inherits="MonBattle.Admin.ResetBattleCounter" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/listing.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>
        Refresh all battle counters
    </h2>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="UserCharacterSource">
            <Columns>
                <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Points" HeaderText="Points" SortExpression="Points" />
                <asp:BoundField DataField="Attack" HeaderText="Attack" SortExpression="Attack" />
                <asp:BoundField DataField="Health" HeaderText="Health" SortExpression="Health" />
                <asp:BoundField DataField="MaxHealth" HeaderText="MaxHealth" SortExpression="MaxHealth" />
                <asp:BoundField DataField="Speed" HeaderText="Speed" SortExpression="Speed" />
                <asp:ImageField DataImageUrlField="Username" HeaderText="Images">
                </asp:ImageField>
            </Columns>
        </asp:GridView>
    <asp:SqlDataSource ID="UserCharacterSource" runat="server" ConnectionString="<%$ ConnectionStrings:MonBattleConnectionString %>" SelectCommand="SELECT monbattle.Users.Username, monbattle.CardCharacters.Name, monbattle.Users.Points, monbattle.CardCharacters.Attack, monbattle.CardCharacters.Health, monbattle.CardCharacters.MaxHealth, monbattle.CardCharacters.Speed, monbattle.CardCharacters.ImageUrl FROM monbattle.CardCharacters INNER JOIN monbattle.Users ON monbattle.CardCharacters.OwnerID = monbattle.Users.UserID"></asp:SqlDataSource>

    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
    <asp:Label ID="lblComp" runat="server" Text=""></asp:Label>
</asp:Content>



