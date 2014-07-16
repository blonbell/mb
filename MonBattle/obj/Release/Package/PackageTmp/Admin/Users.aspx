<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="MonBattle.Admin.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="div-users">
        <asp:GridView CssClass="grid-users" runat="server" ID="grid_users" 
            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Width="100%" AllowSorting="True" DataSourceID="UserSource" DataKeyNames="UserID">
            <Columns>
                <asp:BoundField DataField="UserID" HeaderText="UserID" InsertVisible="False" ReadOnly="True" SortExpression="UserID" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
                <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
                <asp:BoundField DataField="Points" HeaderText="Points" SortExpression="Points" />
                <asp:BoundField DataField="BattleCounter" HeaderText="BattleCounter" SortExpression="BattleCounter" />
            </Columns>

        </asp:GridView>
        <asp:SqlDataSource ID="UserSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MonBattleConnectionString %>" 
            SelectCommand="SELECT [UserID], [Email], [Username], [DateCreated], [Points], [BattleCounter] FROM [monbattle].[Users]"></asp:SqlDataSource>
    </div>

</asp:Content>
