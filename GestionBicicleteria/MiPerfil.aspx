<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="GestionBicicleteria.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center align-content-center">
        <h1>Mi Perfil</h1>
    </div>

    <div class=" card p-4 shadow">
        <div class=" row">

            <!-- Columna izquierda: Datos -->
            <div class="col-md-6">
                <div class="mb-3">
                    <asp:Label ID="lblEmail" runat="server" Text="E-mail"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblApellido" runat="server" Text="Apellido"></asp:Label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblFechaNac" runat="server" Text="Fecha Nacimiento"></asp:Label>
                    <asp:TextBox ID="txtFechaNac" runat="server" TextMode="date" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <!-- Columna derecha: Imagen -->
            <div class="col-md-6">
                <div class="mb-3">
                    <asp:Label ID="lblUrlImagen" runat="server" Text="Imagen de perfil"></asp:Label>
                    <%--<input id="txtImagen" type="file" runat="server" class="form-control mt-2" />--%>
                    <asp:FileUpload ID="txtImagen" runat="server" CssClass="form-control mt-2" onchange="mostrarVistaPrevia(this)" />

                </div>

                <asp:Image ID="imgnuevoPerfil"
                    ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbh24Xuyh_r2IQZmUNeGgpxiWSMr-jn1xwHQ&s"
                    runat="server"
                    CssClass="img-fluid rounded shadow mt-3"
                    Style="max-height: 250px;" />

                <br />
                <br />
                
                <asp:LinkButton ID="btnEliminarFoto" runat="server" OnClick="btnEliminarFoto_Click">Eliminar</asp:LinkButton>
            </div>
        </div>
    </div>

    <div class="d-flex gap-2 mt-3">
        <asp:Button ID="btnGuardarPerfil" OnClick="btnGuardarPerfil_Click" CssClass="btn btn-primary" runat="server" Text="Guardar" />
        <a href="/Default.aspx" class="btn btn-secondary">Regresar</a>
    </div>

    <script type="text/javascript">
        function mostrarVistaPrevia(input) {
            if (input.files && input.files[0]) {
                var lector = new FileReader();
                lector.onload = function (e) {
                    var img = document.getElementById('<%= imgnuevoPerfil.ClientID %>');
                    img.src = e.target.result;
                };
                lector.readAsDataURL(input.files[0]);
            }
        }
    </script>

</asp:Content>
