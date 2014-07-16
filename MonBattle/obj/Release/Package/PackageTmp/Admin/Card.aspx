<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Card.aspx.cs" Inherits="MonBattle.Admin.Card" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Table CssClass="card-table" runat="server" ID="tbl_card" Width="100%">
        <asp:TableRow>
            <asp:TableCell Width="100px">
                Name:
            </asp:TableCell>

            <asp:TableCell>
                <asp:TextBox runat="server" ID="txt_name" Width="100%"></asp:TextBox>
            </asp:TableCell>

            <asp:TableCell>

            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                Picture:
            </asp:TableCell>

            <asp:TableCell>
                <asp:FileUpload runat="server" ID="fileUpload_picture" Width="100%" />
            </asp:TableCell>

            <asp:TableCell>

            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                <asp:Image runat="server" ID="img_card" Width="200px" Visible="false" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">
                Description:
            </asp:TableCell>

            <asp:TableCell VerticalAlign="Top">
                <asp:TextBox runat="server" ID="txt_description" TextMode="MultiLine" Rows="4" Width="100%" style="font-size: 13px"></asp:TextBox>
            </asp:TableCell>

            <asp:TableCell>

            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell ColumnSpan="3" HorizontalAlign="Center" style="padding-top: 10px">
                <asp:Button CssClass="save-card-button" runat="server" ID="btn_createCard" Text="Save Card" OnClick="btn_createCard_Click" Visible="false" />
                <asp:Button CssClass="save-card-button" runat="server" ID="btn_updateCard" Text="Update Card" OnClick="btn_updateCard_Click" Visible="false" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

</asp:Content>
