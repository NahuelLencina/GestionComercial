<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GestionBicicleteria.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="~/Estilos/StyleSheet.css" rel="stylesheet" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <h1>Lista de Articulos</h1>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label runat="server" Text="Filtrar"></asp:Label>
                        <asp:TextBox ID="txtFiltroRapido" onfocus="this.select()" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtFiltroRapido_TextChanged" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
                    <div class="mb-3">
                        <asp:CheckBox ID="chkAvanzado" Text="Filtro Avanzado 🔍" CssClass="" AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" runat="server" />
                    </div>
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

                <% } %>

                <div>
                    <asp:Button ID="btnCrearPresupuesto" CssClass="btn btn-success mb-3" OnClick="btnCrearPresupuesto_Click" runat="server" Text="📝 Crear Presupuesto" />
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


                        <div class="d-flex justify-content-end mt-3">
                            <asp:Label runat="server" Text="Filas "></asp:Label>
                            <asp:DropDownList ID="ddlCambiarFilas" AutoPostBack="true" OnSelectedIndexChanged="ddlCambiarFilas_SelectedIndexChanged" runat="server">
                                <asp:ListItem Text="5" Value="5" />
                                <asp:ListItem Text="10" Value="10" />
                                <asp:ListItem Text="15" Value="15" />
                            </asp:DropDownList>
                        </div>
                    </asp:Panel>


                    <!--Columna derecha presupuesto-->
                    <asp:Panel ID="pnlPresupuesto" CssClass="col-5" Visible="false" runat="server">
                        <asp:GridView ID="dgvPresupuesto" CssClass="table" EmptyDataText="No hay articulos en el presupuesto" AutoGenerateColumns="false" runat="server" DataKeyNames="Id">
                            <Columns>
                                <%--<asp:BoundField HeaderText="Id" DataField="Id" />--%>
                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtcantidad" runat="server" 
                                            AutoPostBack="true" OnTextChanged="txtcantidad_TextChanged" 
                                            Text='<%# Eval("Cantidad")%>' Width="50px"></asp:TextBox>
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
            <div>
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar ➕" OnClick="btnAgregar_Click" CssClass="btn btn-outline-primary mt-3" />
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
