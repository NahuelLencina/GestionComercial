<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="GestionBicicleteria.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="d-flex justify-content-center align-items-center">
        <h2>Crea tu perfil</h2>
    </div>

    <div class="row justify-content-center align-content-center">
        <div class="col-4">

            <div class="mb-3">
                <asp:Label ID="lblUsuario" CssClass=" form-label" runat="server" Text="Usuario"></asp:Label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label ID="lblPass" CssClass=" form-label" runat="server" Text="Contraseña"></asp:Label>
                <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label ID="lblEmail" CssClass=" form-label" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div>
                <asp:Button ID="btnRegistrarse" runat="server" OnClick="btnRegistrarse_Click" CssClass="btn btn-primary" Text="Registrarse" />
                <a href="/Default.aspx">Cancelar</a>
            </div>

        </div>
    </div>

</asp:Content>
