<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormularioAltaCliente.aspx.cs" Inherits="GestionBicicleteria.FormularioAltaCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-center align-content-center">
        <h1>Formulario de alta de nuevos clientes</h1>
    </div>

    <div class=" card p-4 shadow">
        <div class=" row ">

            <!-- Columna izquierda: Datos -->
            <div class="col-md-4">
                <div class="mb-3">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre Apellido:"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblTelefono" runat="server" Text="Tel:"></asp:Label>
                    <asp:TextBox ID="txtNumTel" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblCuit" runat="server" Text="Cuit:"></asp:Label>
                    <asp:TextBox ID="txtCuit" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblDireccion" runat="server" Text="Dirección:"></asp:Label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <!-- Columna derecha: Imagen -->
            <div class="col-md-6">
                <div class="mb-3">
                </div>

            </div>
        </div>
    </div>

    <div class="d-flex gap-2 mt-3">
        <asp:Button ID="btnGuardarPerfil" OnClick="btnGuardarPerfil_Click" CssClass="btn btn-primary" runat="server" Text="Guardar" />
        <a href="/Default.aspx" class="btn btn-secondary">Regresar</a>
    </div>


</asp:Content>
