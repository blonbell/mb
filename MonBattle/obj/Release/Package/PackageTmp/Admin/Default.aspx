<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MonBattle.Admin.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".date-picker").datepicker();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:Panel ID="Panel2" runat="server">
        <asp:TextBox ID="txtDatePicker" class="date-picker" runat="server"></asp:TextBox>
        <asp:Button ID="btnFindVoters" runat="server" Text="Button" OnClick="btnFindVoters_Click" />
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server">
        <asp:Literal ID="ltlTest" runat="server"></asp:Literal>
    </asp:Panel>
    
</asp:Content>
