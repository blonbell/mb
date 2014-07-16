<%@ Page Title="" Language="C#" MasterPageFile="~/MonBattle.Master" AutoEventWireup="true" CodeBehind="CreateAChar.aspx.cs" Inherits="MonBattle.CreateAChar" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/characters.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="formPanel" CssClass="master-content" runat="server">
        <h1>Create a Character</h1>
        <div class="form-group text-info">
            <asp:Label ID="lblName" runat="server" Text="Name"/>
            <asp:TextBox ID="txtName" CssClass="form-control" placeholder="Enter the name" runat="server" />
        </div>
        <div class="form-group text-info">
            <asp:Label ID="lblImage"  runat="server" Text="Image"/>
            <asp:FileUpload ID="imgUpload" runat="server" ViewStateMode="Enabled" />
            <p class="help-block">The image will be resized to 445px by 575px and can not be bigger than 750kb</p> 
            <asp:Label ID="lblHelp" CssClass="help-block" runat="server" Text=""></asp:Label>
        </div>
        <div class="form-group text-info">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button ID="btnPreview" CssClass="btn btn-default" runat="server" Text="Preview" OnClick="btnPreview_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="previewPanel" runat="server" Visible="false">
        <h1>Preview Character</h1>
        
        <div class="text-center" style="text-align: center;">
            <asp:Image ID="imgCard" runat="server" Width="300px" Height="450px" />
            <p><asp:Label ID="lblPrevName" runat="server" Text="Label"></asp:Label></p>
            <p class="help-block">Please review the contents. Once saved, the Character profile cannot be changed.</p> 
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </div>
    </asp:Panel>
</asp:Content>


