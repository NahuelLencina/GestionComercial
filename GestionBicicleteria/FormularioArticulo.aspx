<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormularioArticulo.aspx.cs" Inherits="GestionBicicleteria.FormularioArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="d-flex justify-content-center align-items-center">
        <h1>Nuevo Articulo</h1>
    </div>

    <div class="card p-4 shadow">
        <div class="row">

            <!-- Columna izquierda: Datos -->
            <div class="col-md-6">
                <div class="mb-3">
                    
                    <asp:TextBox ID="TxtId" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre Art: </label>
                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
                </div>

                <div class="mb-3">
                    <label for="txtPrecio" class="form-label">Precio: </label>
                    <asp:TextBox ID="txtPrecio" CssClass="form-control" runat="server" />
                </div>

                <div class="mb-3">
                    <label for="ddlCategoria" class="form-label">Categoria: </label>
                    <asp:DropDownList ID="ddlCategoria" CssClass="form-select" runat="server">
                    </asp:DropDownList>
                </div>

                <div class="mb-3">
                    <label for="ddlProveedores" class="form-label">Proveedor: </label>
                    <asp:DropDownList ID="ddlProveedores" CssClass="form-select" runat="server">
                    </asp:DropDownList>
                </div>

                <div class="mb-3 d-flex gap-2">
                    <asp:Button ID="btnAceptar" CssClass="btn btn-outline-primary mt-3" OnClick="btnAceptar_Click" runat="server" Text="Aceptar" />
                    <asp:Button ID="btnEliminarArt" OnClick="btnEliminarArt_Click" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger mt-3"/>
                    <a href="Default.aspx" class="btn btn-outline-primary mt-3">Cancelar</a>
                    <asp:Button ID="btnInactivar" OnClick="btnInactivar_Click1" runat="server" CssClass="btn btn-warning mt-3" Text="Inactivar" />
                </div>
            </div>


            <!-- Columna derecha: Imagen -->
            <div class="col-md-6">
                <div class="mb-3">
                     <label for="txtDescripcion" class="form-label">Descripción: </label>
                    <asp:TextBox ID="txtDescripcion" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblUrlImagenArticulo" runat="server" Text="Foto del articulo"></asp:Label>
                    <asp:FileUpload ID="txtImagenArticulo" runat="server" CssClass="form-control mt-2" onchange="mostrarVistaPreviaArticulo(this)"/>
                </div>

                <asp:Image ID="imgArticulo"
                    ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbh24Xuyh_r2IQZmUNeGgpxiWSMr-jn1xwHQ&s"
                    runat="server"
                    CssClass="img-fluid rounded shadow mt-3"
                    Style="max-height: 250px;" />

                <br />
                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="mb-3">
                                    <asp:LinkButton ID="btnEliminarFotoArticulo" runat="server" OnClick="btnEliminarFotoArticulo_Click"> Eliminar </asp:LinkButton>
                                </div>

                                <%  if (ConfirmaEliminacion)
                                    { %>
                                <div class="mb-3">
                                    <asp:CheckBox ID="ChxConfirmaEliminacion" runat="server" Text="Confirmar Eliminación" />
                                    <asp:Button ID="btnConfirmaEliminacion" runat="server" OnClick="btnConfirmaEliminacion_Click" Text="Eliminar" CssClass="btn btn-outline-danger" />
                                </div>
                                <% } %>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </div>
    </div>


    <script type="text/javascript">
        function mostrarVistaPreviaArticulo(input) {
            if (input.files && input.files[0]) {
                var lector = new FileReader();
                lector.onload = function (e) {
                    var img = document.getElementById('<%= imgArticulo.ClientID %>');
                    img.src = e.target.result;
                };
                lector.readAsDataURL(input.files[0]);
            }
        }
    </script>


</asp:Content>
