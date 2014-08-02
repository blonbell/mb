<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" 
    AutoEventWireup="true" CodeBehind="AddMoveCatalog.aspx.cs" Inherits="MonBattle.Admin.AddMoveCatalog" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel2" runat="server">
        <asp:GridView ID="GridView1" runat="server" DataSourceID="MoveSetSource" AutoGenerateColumns="False" DataKeyNames="MoveID">
            <Columns>
                <asp:BoundField DataField="MoveID" HeaderText="MoveID" InsertVisible="False" ReadOnly="True" SortExpression="MoveID" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="Turns" HeaderText="Turns" SortExpression="Turns" />
                <asp:CheckBoxField DataField="Linger" HeaderText="Linger" SortExpression="Linger" />
                <asp:BoundField DataField="MeterCost" HeaderText="MeterCost" SortExpression="MeterCost" />
                <asp:BoundField DataField="CommandStr" HeaderText="CommandStr" SortExpression="CommandStr" />
            </Columns>
        </asp:GridView>
        <%-- You can change the data connection if you prefer not to use SqlDataSource. --%>
        <asp:SqlDataSource ID="MoveSetSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MonBattleConnectionString %>" 
            SelectCommand="SELECT [MoveID], [Name], [Description], [Turns], [Linger], [MeterCost], [CommandStr] FROM [monbattle].[Moves]">
        </asp:SqlDataSource>
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server">
        <div>
            <asp:Label ID="Label3" runat="server" Text="Name"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="Label4" runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
        </div>
    
        <div>
            <asp:Label ID="Label2" runat="server" Text="Turns"></asp:Label>
            <asp:TextBox ID="txtTurn" runat="server"></asp:TextBox>
            <asp:RadioButtonList ID="lingerList" runat="server" 
                RepeatDirection="Horizontal" RepeatLayout="Table">
                <asp:ListItem Text="Linger" Value="0"></asp:ListItem>
                <asp:ListItem Text="Charge" Value="1"></asp:ListItem>
            </asp:RadioButtonList>
        </div>

        <div>
            <asp:Label ID="Label1" runat="server" Text="Effect String"></asp:Label>
            <asp:TextBox ID="txtCommandStr" runat="server"></asp:TextBox>
        </div>

        <div>
            <asp:Label ID="Label5" runat="server" Text="Meter Cost"></asp:Label>
            <asp:TextBox ID="txtMeterCost" runat="server"></asp:TextBox>
        </div>

        <div>
            <asp:Label ID="Label6" runat="server" Text="Redeem Cost"></asp:Label>
            <asp:TextBox ID="txtRedeemCost" runat="server"></asp:TextBox>
        </div>

        <div>
            <asp:Label ID="Label7" runat="server" Text="ImageUrl (make this a file upload)"></asp:Label>
            <asp:TextBox ID="txtImageUrl" runat="server"></asp:TextBox>
        </div>

        <asp:Button ID="btnAddMove" runat="server" Text="Add Move" OnClick="btnAddMove_Click" />
    </asp:Panel>
</asp:Content>