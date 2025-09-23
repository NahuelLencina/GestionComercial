using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace GestionBicicleteria
{
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        protected Articulo articulo;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"])&&
                    int.TryParse(Request.QueryString["id"], out int id))
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    articulo = negocio.BuscarPorId(id);
                    Session["articulo"] = articulo;
                }
                else
                    Response.Redirect("gestionComercialFrond.aspx");
            }
            else
                articulo = (Articulo)Session["articulo"];
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestionComercialFrond.aspx");
        }
    }
}