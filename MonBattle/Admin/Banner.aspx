<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Banner.aspx.cs" Inherits="MonBattle.Admin.Banner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Table CssClass="banner-table" runat="server" ID="tbl_banner" Width="100%">

        <asp:TableRow>
            <asp:TableCell>
                Picture:
            </asp:TableCell>

            <asp:TableCell>
                <asp:FileUpload runat="server" ID="fileUpload_picture" Width="100%" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                Link:
            </asp:TableCell>

            <asp:TableCell>
                <asp:TextBox runat="server" ID="txt_link" Width="100%"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="100%" ColumnSpan="2" style="text-align:center">
                <asp:Button runat="server" ID="btn_uploadBanner" Text="Upload Banner" OnClick="btn_uploadBanner_Click" />
                <br />
                <br />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                Current banner:
            </asp:TableCell>

            <asp:TableCell>

            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:ImageButton runat="server" ID="img_banner" Visible="false" Width="540px" Height="90px" />
            </asp:TableCell>
        </asp:TableRow>

    </asp:Table>


</asp:Content>
