<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListadoVentas.aspx.cs" Inherits="GestionBicicleteria.ListadoVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server" />


    <div class="row ">
        <div class="col-md-5">
            <div class="mb-3">
                <asp:Label Text="Filtro" runat="server"> </asp:Label>
                <asp:TextBox ID="txtFlitroRapVentas" onfocus="this.select()" AutoPostBack="true" OnTextChanged="txtFlitroRapVentas_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>


    <div class="row">
      
        <asp:Panel ID="pnlVentas" CssClass="col-5" runat="server">
            <asp:GridView ID="dgvVentas" OnRowCommand="dgvVentas_RowCommand" runat="server" CssClass="table" DataKeyNames="Id" AutoGenerateColumns="false">
                <Columns>

                    <%-- <asp:BoundField HeaderText="Cliente" DataField="IdCliente" />--%>
                    <asp:BoundField HeaderText="Cuit" DataField="Cuit" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                    <asp:BoundField HeaderText="Fecha" DataField="Fecha" />
                    <asp:BoundField HeaderText="Total" DataField="Total" />

                </Columns>
            </asp:GridView>
        </asp:Panel>
      
    </div>

</asp:Content>
