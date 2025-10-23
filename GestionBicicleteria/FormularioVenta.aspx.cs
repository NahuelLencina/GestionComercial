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
    public partial class FormularioVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

                Session.Add("listaArticulos", negocio.listaArticulosConSP());

            
            }
        }

        protected void btnVerVista2_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnVerVista1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected bool cargaCliente
        {
            get { return ViewState["FiltroAvanzado"] != null && (bool)ViewState["FiltroAvanzado"]; }
            set { ViewState["FiltroAvanzado"] = value; }
        }
    }
}