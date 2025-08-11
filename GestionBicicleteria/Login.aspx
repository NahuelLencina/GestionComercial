<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GestionBicicleteria.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="d-flex justify-content-center align-items-center">
        <h1>Bienvenido</h1>
    </div>

    <div class="row justify-content-center align-content-center">
        <div class="col-4">
            <asp:Panel ID="pnlLogin" runat="server" DefaultButton="btnIngresar">
                <div class="mb-3">
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblPass" runat="server" Text="Contraseña"></asp:Label>
                    <asp:TextBox ID="txtPass" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="lblMensageError" CssClass="text-danger" runat="server" Text=""></asp:Label>
                </div>

                <div>
                    <asp:Button ID="btnIngresar" CssClass="btn btn-primary" runat="server" OnClick="btnIngresar_Click" Text="Ingresar" />
                    <a href="/Registro.aspx">Registrarme</a>           
                </div>
            </asp:Panel>
        </div>
    </div>

</asp:Content>
