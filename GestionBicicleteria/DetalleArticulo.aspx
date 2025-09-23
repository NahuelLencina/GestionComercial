<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="GestionBicicleteria.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row row-cols-md-3 g-4 d-flex justify-content-center">
        <div class="col">
            <div class="card">
                <h5 class="card-title d-flex justify-content-center"><%: articulo.Nombre %></h5>
                <img src="<%:ResolveUrl("~/Images/" + articulo.UrlImagen) %>" class="card-img-top" alt="" />
                <div class="card-body">
                    <p class="card-text"><%: articulo.Nombre%></p>
                    <p class="card-text"><%: articulo.Descripcion %></p>
                </div>
                <asp:Button ID="btnVolver" CssClass="btn btn-outline-primary mt-3" OnClick="btnVolver_Click" runat="server" Text="Volver" />
            </div>
        </div>
    </div>

</asp:Content>
