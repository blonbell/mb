<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" 
    AutoEventWireup="true" CodeBehind="AddNeutralMonster.aspx.cs" Inherits="MonBattle.Admin.AddNeutralMonster" %>
<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="formPanel" CssClass="master-content" runat="server">
        <h1>Add Neutral Card Characters</h1>
        <asp:Label ID="lblHelp" runat="server" Text=""></asp:Label>
        <div>
            <asp:Label ID="lblName" runat="server" Text="Name"/>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lblAttack" runat="server" Text="Attack"/>
            <asp:TextBox ID="txtAttack" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lblHealth" runat="server" Text="Health"/>
            <asp:TextBox ID="txtHealth" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lblSpeed" runat="server" Text="Speed"/>
            <asp:TextBox ID="txtSpeed" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="lblReward" runat="server" Text="Reward"/>
            <asp:TextBox ID="txtReward" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:FileUpload ID="imgUpload" runat="server" />
            <asp:Label ID="lblCaution" runat="server" Text="Same filenames upload will overwrite the previous image"/>
        </div>
        <div>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </div>
    </asp:Panel>
</asp:Content>
