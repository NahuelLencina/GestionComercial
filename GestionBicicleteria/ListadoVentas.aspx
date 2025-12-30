<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListadoVentas.aspx.cs" Inherits="GestionBicicleteria.ListadoVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server" />

    <div class="row">

        <asp:Panel ID="pnlVentas" CssClass="col-5" runat="server">
            <asp:GridView ID="dgvVentas" OnRowCommand="dgvVentas_RowCommand" runat="server" CssClass="table" DataKeyNames="Id" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="Id" DataField="Id" />
                    <asp:BoundField HeaderText="IdCliente" DataField="Cliente" />
                </Columns>
            </asp:GridView>
        </asp:Panel>

    </div>

</asp:Content>
