<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" 
    CodeBehind="NeutralCharacterListing.aspx.cs" Inherits="MonBattle.Admin.NeutralCharacterListing" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/listing.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="CharListings" runat="server" 
        AutoGenerateColumns="False" DataSourceID="NeutralCharSource" AllowSorting="True" DataKeyNames="CharID">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="ImageUrl" HeaderText="ImageUrl" SortExpression="ImageUrl" />
            <asp:BoundField DataField="Attack" HeaderText="Attack" SortExpression="Attack" />
            <asp:BoundField DataField="Health" HeaderText="Health" SortExpression="Health" />
            <asp:BoundField DataField="Speed" HeaderText="Speed" SortExpression="Speed" />
            <asp:BoundField DataField="rewards" HeaderText="rewards" SortExpression="rewards" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="NeutralCharSource" runat="server" ConnectionString="<%$ ConnectionStrings:MonBattleConnectionString %>" 
        SelectCommand="SELECT * FROM [monbattle].[NeutralCharacters]" DeleteCommand="DELETE FROM monbattle.NeutralCharacters WHERE (CharID = @CharID)">
        <DeleteParameters>
            <asp:Parameter Name="CharID" />
        </DeleteParameters>
    </asp:SqlDataSource>
</asp:Content>

