using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using negocio;
using dominio;

namespace GestionBicicleteria
{
    public partial class Default : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.esAdmin(Session["trainee"]))
            {
                Session.Add("Error.aspx","Se requiere permisos de administrador");
                Response.Redirect("Login.aspx");
            }

            FiltroAvanzado = chkAvanzado.Checked;
            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Session.Add("listaArticulos", negocio.listaArticulosConSP());

                if (Session["PageSize"]!=null)
                {
                    dgvArticulos.PageSize = (int)Session["PageSize"];
                    ddlCambiarFilas.SelectedValue = dgvArticulos.PageSize.ToString();
                }
                CargarGridview();
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
            int pageSizeDdl = int.Parse(ddlCambiarFilas.SelectedValue) ;

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
            Response.Redirect("FormularioArticulo.aspx?id="+id);
        }
    }

}