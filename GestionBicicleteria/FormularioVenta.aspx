<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormularioVenta.aspx.cs" Inherits="GestionBicicleteria.FormularioVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <asp:Button ID="btnVerVista2" runat="server" Text="Ir a Vista 2" OnClick="btnVerVista2_Click" />
    <asp:Button ID="btnVerVista1" runat="server" Text="Volver vista 1" OnClick="btnVerVista1_Click" />

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <h2>Vista 1</h2>

            <!--Columna Izquierda-->
            <div class="row">
                <div class="col-md-6">
                    <!--Filttro y Check-->
                    <div class="mb-3">
                        <asp:Label runat="server" Text="Filtrar"></asp:Label>
                        <asp:TextBox ID="txtFiltroRapido" onfocus="this.select()" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:CheckBox ID="chkAvanzado" Text="Filtro Avanzado 🔍" CssClass="" AutoPostBack="true" runat="server" />
                    </div>


                    <div class="row">
                        <div class="col-3">
                            <div class="mb-3">
                                <asp:Label ID="lblCampo" runat="server" Text="Campo"></asp:Label>
                                <asp:DropDownList ID="ddlCampo" CssClass="form-control" AutoPostBack="true" runat="server">
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
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-primary mb-3" />
                            </div>
                        </div>
                    </div>

                    <div class="col-7 mb-2">
                        <asp:Button ID="btnCrearPresupuesto" CssClass="btn btn-success" runat="server" Text="📝 Crear Presupuesto" />
                        <asp:Button ID="btnCargaCliente" CssClass="btn btn-primary" runat="server" Text="👤 Cargar cliente" />
                    </div>

                    <asp:Panel ID="pnlArticulos" CssClass="border mt-3" runat="server">
                        <asp:GridView ID="dgvArticulos" runat="server"
                            CssClass="table" AutoGenerateColumns="false">

                            <Columns>
                                <asp:BoundField HeaderText="Id" DataField="Id" />
                                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                                <asp:BoundField HeaderText="Precio" DataField="Precio" />
                                <asp:BoundField HeaderText="Categoria" DataField="Categoria" />

                                <asp:TemplateField HeaderText="" Visible="false">
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



                    </asp:Panel>

                </div>


                <!--Columna derecha-->
                <div class="col-md-6">
                    <asp:Panel CssClass="border p-3" runat="server">
                        <div class="row">
                            <div class="col-6">
                                <div>
                                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txtCuit" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-6">
                                <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>

                      <asp:Panel ID="Panel1" CssClass="border mt-3" runat="server">
                        <asp:GridView ID="GridView1" runat="server"
                            CssClass="table" AutoGenerateColumns="false">

                            <Columns>
                                <asp:BoundField HeaderText="Id" DataField="Id" />
                                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                                <asp:BoundField HeaderText="Precio" DataField="Precio" />
                                <asp:BoundField HeaderText="Categoria" DataField="Categoria" />

                                <asp:TemplateField HeaderText="" Visible="false">
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



                    </asp:Panel>

                </div>
            </div>
        </asp:View>

        <asp:View ID="View2" runat="server">
            <h2>Vista 2</h2>
            <p>Contenido de la segunda vista</p>
        </asp:View>
    </asp:MultiView>

</asp:Content>
