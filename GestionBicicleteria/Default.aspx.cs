using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using negocio;
using dominio;
using System.Web.UI.WebControls;

namespace GestionBicicleteria
{
    public partial class Default : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.esAdmin(Session["trainee"]))
            {
                Session.Add("Error.aspx", "Se requiere permisos de administrador");
                Response.Redirect("Login.aspx");
            }

            FiltroAvanzado = chkAvanzado.Checked;
            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Session.Add("listaArticulos", negocio.listaArticulosConSP());

                if (Session["PageSize"] != null)
                {
                    dgvArticulos.PageSize = (int)Session["PageSize"];
                    ddlCambiarFilas.SelectedValue = dgvArticulos.PageSize.ToString();
                }
                CargarGridview();
                pnlPresupuesto.Visible = false;
            }


        }
        private void CargarGridview(List<Articulo> lista = null)
        {
            if (lista == null)
                lista = (List<Articulo>)Session["listaArticulos"];

            dgvArticulos.DataSource = lista;
            dgvArticulos.DataBind();
        }

        protected void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada = ((List<Articulo>)Session["listaArticulos"])
                .FindAll(x => x.Nombre.ToUpper().Contains(txtFiltroRapido.Text.ToUpper()));
            CargarGridview(listaFiltrada);

        }

        protected void dgvArticulos_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            dgvArticulos.PageIndex = e.NewPageIndex;
            CargarGridview();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioArticulo.aspx");
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }


        protected void ddlCambiarFilas_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Leo el valor elegido por el usuario
            int pageSizeDdl = int.Parse(ddlCambiarFilas.SelectedValue);

            // Guardo en Sessión
            Session["PageSize"] = pageSizeDdl;

            // Aplico al gridview
            dgvArticulos.PageSize = pageSizeDdl;
            dgvArticulos.PageIndex = 0;

            // Refrescamos los datos
            CargarGridview();
        }

        protected void dgvArticulos_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string id = dgvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioArticulo.aspx?id=" + id);
        }

        protected void btnCrearPresupuesto_Click(object sender, EventArgs e)
        {
            // Cambia el tamaño del gridView
            pnlArticulos.CssClass = "col-7";
            dgvArticulos.Columns[4].Visible = true;

            // Muestro el GripView presupuesto
            pnlPresupuesto.Visible = true;

            if (Session["presupuesto"] == null)
                Session["presupuesto"] = new List<Articulo>();

            dgvPresupuesto.DataSource = (List<Articulo>)Session["presupuesto"];
            dgvPresupuesto.DataBind();

        }

        protected void dgvArticulos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            // Obtengo el Id del Item seleccionado
            int idArticulo = Convert.ToInt32(e.CommandArgument);
            if (!int.TryParse(e.CommandArgument?.ToString(), out idArticulo))
                return;


            // Busco el articulo seleccionado en la lista Articulos
            var listaArticulos = (List<Articulo>)Session["listaArticulos"];
            var articuloSeleccionado = listaArticulos.FirstOrDefault(a => a.Id == idArticulo);
           
            // Obtengo la lista de presupuesto o la creo 
            var presupuesto = (List<Articulo>)Session["presupuesto"] ?? new List<Articulo>();

            

            // si hay un articulo seleccionado sumo o agrego
            if (articuloSeleccionado != null)
            {
                // Verifico si ya fue cargado el articulo en el presupuesto
                var enPresupuesto = presupuesto.FirstOrDefault(a => a.Id == idArticulo);

                // Logica de botones
                if (e.CommandName == "sumar")
                {
                    if (enPresupuesto == null)
                    {
                        var nuevoArticulo = new Articulo
                        {
                            Id = articuloSeleccionado.Id,
                            Nombre = articuloSeleccionado.Nombre,
                            Precio = articuloSeleccionado.Precio,
                            Categoria = articuloSeleccionado.Categoria,
                            Cantidad = 1
                        };
                        presupuesto.Add(nuevoArticulo);
                    }
                    else
                        enPresupuesto.Cantidad++;

                }
                else if (e.CommandName == "restar" && enPresupuesto != null)
                {
                    enPresupuesto.Cantidad--;
                    if (enPresupuesto.Cantidad <= 0)
                        presupuesto.Remove(enPresupuesto);
                }
            }
            // Si no hay articulo seleccionado salgo
            else
                return;

            // Actualizamos la Sessión
            Session["presupuesto"] = presupuesto;
            dgvPresupuesto.DataSource = presupuesto;
            dgvPresupuesto.DataBind();
        }

        protected void txtcantidad_TextChanged(object sender, EventArgs e)
        {
            // El sender es el textbox que dispara el evento
            TextBox txt = (TextBox)sender;

            GridViewRow row = (GridViewRow)txt.NamingContainer;
            int idArticulo = Convert.ToInt32(dgvPresupuesto.DataKeys[row.RowIndex].Value);

            // Busco el id en la lista presupuesto
            var presupuesto = (List<Articulo>)Session["presupuesto"];
            
            // convierto el valor ingresado en el textBox
            int nuevaCantidad;

            if (int.TryParse(txt.Text, out nuevaCantidad) && nuevaCantidad > 0)
            {
                var articulo = presupuesto.FirstOrDefault(a => a.Id == idArticulo);
                if (articulo != null)
                    articulo.Cantidad = nuevaCantidad;
;            }

            // Actualizamos la Session presupuesto

            Session["presupuesto"] = presupuesto;
            dgvPresupuesto.DataSource = presupuesto;
            dgvPresupuesto.DataBind();

        }
    }
}