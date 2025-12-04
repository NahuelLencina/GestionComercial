<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="GaleriaArticulos.aspx.cs" Inherits="GestionBicicleteria.gestionComercialFrond" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class=" d-flex justify-content-center align-items-center">
        <h1>Catalogo...</h1>
    </div>

    <div class="row row-cols-md-3 g-4">
        <%
            foreach (dominio.Articulo articulo in ListaArticulo)
            {
        %>
        <div class="col">
            <div class="card">
                <h5 class="card-title d-flex justify-content-center"><%: articulo.Nombre%></h5>
                <img src="<%:ResolveUrl("~/Images/" + articulo.UrlImagen) %>" class="card-img-top" alt="" />
                <div class="card-body">
                    <p class="card-text"><%: articulo.Descripcion%></p>

                    <a href="DetalleArticulo.aspx?Id=<%: articulo.Id %>">Ver Detalle</a>
                </div>
            </div>
        </div>
        <% }%>
    </div>
</asp:Content>
