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
    public partial class FormularioAltaCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGuardarPerfil_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            ClienteNegocio negocio = new ClienteNegocio();
            try
            {
                cliente.Nombre = txtNombre.Text;
                cliente.Telefono = txtNumTel.Text;
                cliente.Cuit = txtCuit.Text;
                cliente.Email = txtEmail.Text;
                cliente.Direccion = txtDireccion.Text;

                negocio.agregarCliente(cliente);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}