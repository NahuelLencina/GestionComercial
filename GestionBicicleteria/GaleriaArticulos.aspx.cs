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
    public partial class gestionComercialFrond : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ListaArticulo = negocio.listaArticulosConSP();

            ListaArticulo = ListaArticulo.Where(a => a.Activo && !string.IsNullOrEmpty(a.UrlImagen)).ToList();

        }
    }
}