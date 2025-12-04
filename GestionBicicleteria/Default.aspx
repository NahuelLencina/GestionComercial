<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GestionBicicleteria.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="~/Estilos/StyleSheet.css" rel="stylesheet" />
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server" />



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-7">
                    <h1>Lista de Articulos</h1>
                </div>
                <div class="col-5">
                    <h1 id="titlePresupuesto" runat="server">Presupuesto</h1>
                </div>
            </div>

            <div class="row">
                <div class="col-md-7">
                    <div class="col-7">
                        <div class="mb-3">
                            <asp:Label runat="server" Text="Filtrar"></asp:Label>
                            <asp:TextBox ID="txtFiltroRapido" onfocus="this.select()" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtFiltroRapido_TextChanged" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="mb-3">
                        <asp:CheckBox ID="chkAvanzado" Text="Filtro Avanzado 🔍" CssClass="" AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" runat="server" />
                    </div>


                    <% if (FiltroAvanzado)
                        {
                    %>
                    <div class="row">
                        <div class="col-3">
                            <div class="mb-3">
                                <asp:Label ID="lblCampo" runat="server" Text="Campo"></asp:Label>
                                <asp:DropDownList ID="ddlCampo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged" runat="server">
                                    <asp:ListItem Text="Nombre" />
                                    <asp:ListItem Text="Proveedor" />
                                    <asp:ListItem Text="Categoria" />

                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-3">
                            <div class="mb-3">
                                <asp:Label runat="server" Text="Criterio"></asp:Label>
                                <asp:DropDownList ID="ddlCriterio" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-3">
                            <div class="mb-3">
                                <asp:Label Text="Filtro" runat="server" />
                                <asp:TextBox ID="txtFiltroAvanzado" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-3">
                            <div class="mb-3">
                                <asp:Label Text="Estado" runat="server" />
                                <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control">
                                    <asp:ListItem Text="Todos" />
                                    <asp:ListItem Text="Activo" />
                                    <asp:ListItem Text="Inactivo" />
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-3">
                            <div class="mb-3">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-primary mb-3" OnClick="btnBuscar_Click" />
                            </div>
                        </div>
                    </div>

                    <% }
                    %>
                </div>
                <div class="col-5 align-content-end">
                    <asp:Panel ID="pnlCargaCliente" Visible="false" CssClass="border p-3 mt-3" runat="server">
                        <div class="row">
                            <h4>Formulario de venta</h4>


                            <%if (cargaCliente)
                                {
                            %>

                            <div class="col-6">
                                <div>
                                    <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                                    <asp:TextBox ID="txtNombreCliente" AutoPostBack="true" OnTextChanged="txtNombreCliente_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>

                                    <asp:Label ID="lblCuit" runat="server" Text="Cuit"></asp:Label>
                                    <asp:TextBox ID="txtCuit" CssClass="form-control" runat="server"></asp:TextBox>

                                    <asp:Button ID="btnTodosClientes" OnClick="btnTodosClientes_Click" runat="server" Text="👤" CssClass="btn btn-outline-primary mt-2" ToolTip="Lista clientes" />
                                    <asp:Button ID="btnAgregarCliente" OnClick="btnAgregarCliente_Click" runat="server" Text="➕" CssClass="btn btn-outline-primary mt-2" ToolTip="Agregar cliente" />

                                </div>
                            </div>
                            <div class="col-6">
                                <asp:Label ID="lblDireccion" runat="server" Text="Dirección"></asp:Label>
                                <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="lblMail" runat="server" Text="E-mail"></asp:Label>
                                <asp:TextBox ID="txtMail" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>



                            <%
                                }%>
                        </div>
                    </asp:Panel>
                </div>

            </div>

            <!-- Botón Crear Presupuesto carga datos Cliente -->
            <div class="row">
                <div class="col-7 mb-2">
                    <asp:Button ID="btnCrearPresupuesto" CssClass="btn btn-success" OnClick="btnCrearPresupuesto_Click" runat="server" Text="📝 Crear Presupuesto" />
                    <asp:Button ID="btnCargaCliente" CssClass="btn btn-primary" Visible="false" runat="server" Text="👤 Cargar cliente" OnClick="btnCargaCliente_Click" />
                    <asp:Button ID="btnLimpiarPresupuesto" OnClick="btnLimpiarPresupuesto_Click" Visible="false" CssClass="btn btn-primary" runat="server" Text="Limpiar formulario" />
                    <asp:Button ID="btnConfirmarPresupuesto" runat="server" Visible="false" CssClass="btn btn-success" Text="Confirmar presupuesto" OnClick="btnConfirmarPresupuesto_Click"/>
                </div>
            </div>


            <div class="modal" tabindex="-1" id="modalConfirmarElim">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" runat="server" id="titleModal" title=""></h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblMensajeModal" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button id="btnCancelar" runat="server" type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnAceptar" CssClass="btn btn-primary" data-bs-dismiss="modal" OnClick="btnAceptar_Click" runat="server" Text="Aceptar" />                            
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">

                <asp:Panel ID="pnlArticulos" CssClass="col-12" runat="server">

                    <asp:GridView ID="dgvArticulos" DataKeyNames="Id" runat="server" OnRowCommand="dgvArticulos_RowCommand"
                        CssClass="table" AutoGenerateColumns="false"
                        OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged1"
                        OnPageIndexChanging="dgvArticulos_PageIndexChanging" AllowPaging="true" PageSize="5">
                        <Columns>
                            <asp:BoundField HeaderText="Id" DataField="Id" />
                            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                            <asp:BoundField HeaderText="Precio" DataField="Precio" />
                            <asp:BoundField HeaderText="Categoria" DataField="Categoria" />

                            <asp:TemplateField HeaderText="Agregar/Quitar" Visible="false">
                                <ItemTemplate>

                                    <div class="d-flex gap-2 align-items-center">
                                        <!-- Botón para decrementar -->
                                        <asp:Button ID="btnDecrementar" runat="server"
                                            CssClass="btn btn-light btn-sm rounded-circle d-flex align-items-center justify-content-center border"
                                            Style="width: 32px; height: 32px;" Text="-" CommandName="restar" CommandArgument='<%# Eval("Id") %>' />


                                        <!-- Botón para incrementar -->
                                        <asp:Button ID="btnIncrementar" runat="server" Text="+" CssClass="btn btn-light btn-sm rounded-circle d-flex align-items-center justify-content-center border "
                                            Style="width: 32px; height: 32px;" CommandName="sumar" CommandArgument='<%# Eval("Id") %>' />
                                    </div>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
                            <asp:CommandField ShowSelectButton="true" SelectText="Modificar" HeaderText="Acción" />
                        </Columns>
                    </asp:GridView>


                    <div class="d-flex justify-content-end mt-3">
                        <asp:Label runat="server" Text="Filas "></asp:Label>
                        <asp:DropDownList ID="ddlCambiarFilas" AutoPostBack="true" OnSelectedIndexChanged="ddlCambiarFilas_SelectedIndexChanged" runat="server">
                            <asp:ListItem Text="5" Value="5" />
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="15" Value="15" />
                        </asp:DropDownList>
                    </div>

                    <div>
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar ➕" OnClick="btnAgregar_Click" CssClass="btn btn-outline-primary mt-3" />
                    </div>
                </asp:Panel>



                <!--Columna derecha presupuesto-->
                <div class="col-md-5">
                    <asp:Panel ID="pnlPresupuesto" CssClass="border p-3 w-100" Visible="false" runat="server">
                        <asp:GridView ID="dgvPresupuesto" CssClass="table" EmptyDataText="No hay articulos en el presupuesto" AutoGenerateColumns="false" runat="server" DataKeyNames="Id">
                            <Columns>
                                <%-- <asp:BoundField HeaderText="Id" DataField="Id" />--%>
                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtcantidad" runat="server"
                                            AutoPostBack="true" OnTextChanged="txtcantidad_TextChanged"
                                            Text='<%# Eval("Cantidad")%>' Width="50px">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                                <asp:BoundField HeaderText="Precio" DataField="Precio" />
                                <asp:BoundField HeaderText="Total" DataField="Total" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="offcanvas offcanvas-end bg-dark text-white" id="offcanvasRight" tabindex="-1" aria-labelledby="offcanvasRightLabel">
        <div class="offcanvas-header">
            <h5 class="offcanvas-title">Lista de clientes</h5>
            <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
        </div>
        <div class="offcanvas-body">
            <asp:UpdatePanel ID="updClientes" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-hover" OnSelectedIndexChanged="gvClientes_SelectedIndexChanged"
                        OnRowCommand="gvClientes_RowCommand" DataKeyNames="Id">
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:TemplateField HeaderText="Elegir">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSeleccionar" runat="server"
                                        CommandName="Select"
                                        CssClass="btn btn-outline-success btn-sm rounded-circle"
                                        ToolTip="Seleccionar cliente">
                                        <i class="bi bi-person-check"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <%-- funcion en JS que abre un modal de confirmacion --%>
    <script>
        function abrirModal() {
            var myModal = new bootstrap.Modal(document.getElementById('modalConfirmarElim'));
            myModal.show();
        }
    </script>


    <script>
        function seleccionar(valor) {
            document.getElementById('<%= txtFiltroRapido.ClientID %>').value = valor;

            // Cierra el offcanvas
            var offcanvasEl = document.getElementById('offcanvasRight');
            var offcanvas = bootstrap.Offcanvas.getInstance(offcanvasEl);
            if (offcanvas) {
                offcanvas.hide();
            }
        }
    </script>
 


</asp:Content>
