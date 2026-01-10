using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace GestionBicicleteria
{
    public partial class ListadoVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.esAdmin(Session["trainee"]))
            {
                Session.Add("Error.aspx", "Se requiere permisos de administrador");
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                VentaNegocio negocio = new VentaNegocio();
                dgvVentas.DataSource = negocio.listaVentas();
                dgvVentas.DataBind();
            }
        }

        protected void dgvVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void txtFlitroRapVentas_TextChanged(object sender, EventArgs e)
        {

        }
    }
}