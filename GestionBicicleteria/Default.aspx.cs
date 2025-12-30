using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using negocio;
using dominio;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.Services;
using System.Web.Script.Serialization;
using Microsoft.Win32;


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
                ActualizarVistaPresupuesto();
                ActualizarVistaPanelCliente();
            }
        }

        private void ActualizarVistaPanelCliente()
        {
            var cliente = Session["clienteSeleccionado"] as Cliente;


            if (cliente != null)
            {
                // Asignamos los datos
                txtNombreCliente.Text = cliente.Nombre;
                txtCuit.Text = cliente.Cuit;
                txtDireccion.Text = cliente.Direccion;
                txtMail.Text = cliente.Email;

                //cargaCliente = !cargaCliente;
                // Mostrar u ocultar el panel según el estado actual
                pnlCargaCliente.Visible = cargaCliente;

                // Cambiar el texto del botón
                btnCargaCliente.Text = cargaCliente ? "Ocultar Datos" : "Datos Cliente";
            }

        }

        // Revisa el grid presupuesto, si no tiene un item cargado lo borra
        private void ActualizarVistaPresupuesto()
        {
            var presupuesto = Session["presupuesto"] as List<Articulo>;

            if (presupuesto != null && presupuesto.Any())
            {
                // Cambia el tamaño del gridView
                pnlArticulos.CssClass = "col-md-7";
                dgvArticulos.Columns[4].Visible = true; // Muestra los botones +/-
                btnCargaCliente.Visible = true;
                btnLimpiarPresupuesto.Visible = true;
                // Muestro el GripView presupuesto
                pnlPresupuesto.Visible = true;
                titlePresupuesto.Visible = true;


                if (presupuesto.Count > 0)
                {
                    btnConfirmarPresupuesto.Visible = true;
                    lblTotalApagar.Visible = true;
                    actualizarTotalApagar();
                }

                dgvPresupuesto.DataSource = (List<Articulo>)Session["presupuesto"];
                dgvPresupuesto.DataBind();
            }

            else
            {
                if (Session["clienteSeleccionado"] != null)
                {
                    //Borramos El cliente seleccionado
                    Session.Remove("clienteSeleccionado");
                    Session.Remove("cargaCliente");

                    cargaCliente = cargaCliente;
                    // Mostrar u ocultar el panel según el estado actual
                    pnlCargaCliente.Visible = cargaCliente;
                    btnCargaCliente.Text = !cargaCliente ? "👤 Cargar cliente" : "Ocultar Datos";
                    limpiarPanelCliente();
                }

                pnlPresupuesto.Visible = false;
                titlePresupuesto.Visible = false;
                pnlArticulos.CssClass = "col-12";
                dgvArticulos.Columns[4].Visible = false; // oculta los botones +/-
            }

        }

        public void actualizarTotalApagar()
        {
            var presupuesto = Session["presupuesto"] as List<Articulo>;

            if (presupuesto!=null && presupuesto.Count>0) 
            {
                double totalPagar = presupuesto.Sum(a => a.Cantidad * a.Precio);
                lblTotalApagar.Text = $"Total a pagar: ${totalPagar}";
                Session["presupuesto"] = presupuesto;
            }
            else
            {   
                lblTotalApagar.Text = string.Empty;
                lblTotalApagar.Visible = false;
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
            ClienteNegocio negocio = new ClienteNegocio();
            Session.Add("listaClientes", negocio.storeListarClientes());


            // Cambia el tamaño del gridView
            dgvArticulos.Columns[4].Visible = true;
            pnlArticulos.CssClass = "col-md-7";
            btnCargaCliente.Visible = true;
            btnLimpiarPresupuesto.Visible = true;
            // Muestro el GripView presupuesto
            pnlPresupuesto.Visible = true;
            titlePresupuesto.Visible = true;

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
                    btnConfirmarPresupuesto.Visible = true;
                    lblTotalApagar.Visible = true;
                    

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
                    if (presupuesto.Count <= 0)
                    {
                        lblTotalApagar.Visible = false;
                        btnConfirmarPresupuesto.Visible = false;
                    }
                }
            }
            // Si no hay articulo seleccionado salgo
            else
                return;

            // Actualizamos la Sessión
            Session["presupuesto"] = presupuesto;
            dgvPresupuesto.DataSource = presupuesto;
            dgvPresupuesto.DataBind();
            actualizarTotalApagar();
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
                ;
            }

            // Actualizamos la Session presupuesto

            Session["presupuesto"] = presupuesto;
            dgvPresupuesto.DataSource = presupuesto;
            dgvPresupuesto.DataBind();

        }

        protected bool cargaCliente
        {
            get { return Session["cargaCliente"] != null && (bool)Session["cargaCliente"]; }
            set { Session["cargaCliente"] = value; }
        }

        protected void btnCargaCliente_Click(object sender, EventArgs e)
        {
            ClienteNegocio negocio = new ClienteNegocio();
            try
            {
                cargaCliente = !cargaCliente;
                // Mostrar u ocultar el panel según el estado actual
                pnlCargaCliente.Visible = cargaCliente;
                Session["listaClientes"] = negocio.storeListarClientes();

                btnCargaCliente.CssClass = "btn btn-primary";
                // Cambiar el texto del botón


                if (Session["clienteSeleccionado"] != null)
                    btnCargaCliente.Text = cargaCliente ? "Ocultar datos" : "Datos Cliente";
                else
                    btnCargaCliente.Text = cargaCliente ? "Ocultar datos" : "👤 Cargar cliente";

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // Metodo para Obtener clientes con el mismo nombre ingresado
        public List<Cliente> obtenerListaClientes(string nombre)
        {
            ClienteNegocio negocio = new ClienteNegocio();
            return negocio.storeListarClientes(nombre);
        }

        // Metodo para Obtener lista de clientes
        public List<Cliente> obtenerListaClientes()
        {
            ClienteNegocio negocio = new ClienteNegocio();
            return negocio.storeListarClientes();
        }
        protected void btnLimpiarPresupuesto_Click(object sender, EventArgs e)
        {
            var enPresupuesto = Session["presupuesto"] as List<Articulo>;

            if (enPresupuesto != null && enPresupuesto.Count > 0)
            {
                titleModal.InnerText = "Confirmar Acción";
                lblMensajeModal.Text = "Desea eliminar el presupuesto?";
                btnAceptar.Visible = true;
                btnCancelar.Visible = true;
                // Ejecuta JavaScript para abrir el modal
                ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModal", "abrirModal();", true);

            }

        }

        // Metodo para limpiar los Campos del panel de datos del cliente
        private void limpiarPanelCliente()
        {
            if (!string.IsNullOrWhiteSpace(txtNombreCliente.Text))
                txtNombreCliente.Text = null;
            if (!string.IsNullOrWhiteSpace(txtCuit.Text))
                txtCuit.Text = null;
            if (!string.IsNullOrWhiteSpace(txtDireccion.Text))
                txtDireccion.Text = null;
            if (!string.IsNullOrWhiteSpace(txtMail.Text))
                txtMail.Text = null;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            var enPresupuesto = Session["presupuesto"] as List<Articulo>;

            if (enPresupuesto != null)
            {
                //Borramos el presupuesto
                enPresupuesto.Clear();
                Session.Remove("presupuesto");

                if (Session["clienteSeleccionado"] != null)
                {
                    //Borramos El cliente seleccionado
                    Session.Remove("clienteSeleccionado");
                    Session.Remove("cargaCliente");

                    cargaCliente = cargaCliente;
                    // Mostrar u ocultar el panel según el estado actual
                    pnlCargaCliente.Visible = cargaCliente;
                    btnCargaCliente.Text = !cargaCliente ? "👤 Cargar cliente" : "Ocultar Datos";
                    limpiarPanelCliente();
                }

                btnConfirmarPresupuesto.Visible = false;
                actualizarTotalApagar();
                //Refrescamos el GridView
                dgvPresupuesto.DataSource = null;
                dgvPresupuesto.DataBind();
            }
        }

        protected void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {

            if (txtNombreCliente.Text.Length >= 3)
            {
                var lista = obtenerListaClientes(txtNombreCliente.Text);
                gvClientes.DataSource = lista;
                gvClientes.DataBind();

                // Abrir el offcanvas usando JS desde el servidor
                ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirOffcanvas",
                    "var offcanvas = new bootstrap.Offcanvas(document.getElementById('offcanvasRight')); offcanvas.show();", true);

            }
        }

        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvClientes.SelectedDataKey == null)
                return;

            int idCliente = Convert.ToInt32(gvClientes.SelectedDataKey.Value);
            var listaClientes = Session["listaClientes"] as List<Cliente>;

            if (listaClientes == null)
                return;

            var cliente = listaClientes.FirstOrDefault(c => c.Id == idCliente);
            if (cliente == null)
                return;

            // Asignamos los datos
            txtNombreCliente.Text = cliente.Nombre;
            txtCuit.Text = cliente.Cuit;
            txtDireccion.Text = cliente.Direccion;
            txtMail.Text = cliente.Email;

            Session["clienteSeleccionado"] = cliente;

            // Cerramos el offcanvas
            ScriptManager.RegisterStartupScript(this, this.GetType(), "cerrarOffcanvas",
                "var offcanvas = bootstrap.Offcanvas.getInstance(document.getElementById('offcanvasRight')); if(offcanvas) offcanvas.hide();", true);
        }

        protected void btnTodosClientes_Click(object sender, EventArgs e)
        {

            var lista = obtenerListaClientes();
            gvClientes.DataSource = lista;
            gvClientes.DataBind();
            // Abrir el offcanvas usando JS desde el servidor
            ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirOffcanvas",
                "var offcanvas = new bootstrap.Offcanvas(document.getElementById('offcanvasRight')); offcanvas.show();", true);
        }

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioAltaCliente.aspx");
        }

        protected void btnConfirmarPresupuesto_Click(object sender, EventArgs e)
        {

            if (Session["clienteSeleccionado"] == null || string.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                titleModal.InnerText = "Atención";
                lblMensajeModal.Text = "Falta seleccionar un cliente";
                ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "abrirModal();", true);
                btnAceptar.Visible = false;
                btnCancelar.Visible = false;
                return;
            }

            Cliente cliente = (Cliente)Session["clienteSeleccionado"];
            Trainee usuarioLog = (Trainee)Session["trainee"];

            var enPresupuesto = Session["presupuesto"] as List<Articulo>;

            if (enPresupuesto == null || enPresupuesto.Count == 0)
                return;

            VentaNegocio negocio = new VentaNegocio();
            Venta nueva = new Venta();

            nueva.IdCliente = cliente.Id;
            nueva.IdUsuario = usuarioLog.Id;
            nueva.Total = enPresupuesto.Sum(a => a.Cantidad * a.Precio);


            VentaItemNegocio itemNegocio = new VentaItemNegocio();
            //  Guardar la venta en BD
            int idVenta = negocio.agregarVenta(nueva);

            foreach (Articulo art in enPresupuesto)
            {
                VentaItem item = new VentaItem();
                item.IdVenta = idVenta;
                item.IdProducto = art.Id;
                item.Cantidad = art.Cantidad;   
                item.PrecioUnitario = art.Precio;
                item.PrecioTotal = art.Precio * art.Cantidad;

                itemNegocio.agregarVentaItem(item); 
            }

            // Borramos los paneles Cliente y presupuesto.
            limpiarPanelCliente();
            enPresupuesto.Clear();

            btnConfirmarPresupuesto.Visible = false;
            btnCargaCliente.Visible = false;
            btnLimpiarPresupuesto.Visible = false;
            
            ActualizarVistaPresupuesto();
            ActualizarVistaPanelCliente();
            actualizarTotalApagar();


            titleModal.InnerText = "Confirmación";
            lblMensajeModal.Text = "La venta se guardo correctamente...";
            btnAceptar.Visible = false;
            btnCancelar.Visible = false;


            // Ejecuta JavaScript para abrir el modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModal", "abrirModal();", true);


         //  Response.Redirect("Default.aspx"); 

        }

        protected void btnLimpiarCampor_Click(object sender, EventArgs e)
        {
            if (Session["clienteSeleccionado"] != null)
            {
                //Borramos El cliente seleccionado
                Session.Remove("clienteSeleccionado");
                limpiarPanelCliente();
            }
        }
    }
}