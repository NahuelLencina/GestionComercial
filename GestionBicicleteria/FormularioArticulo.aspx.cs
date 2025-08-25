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
    public partial class FormularioArticulo : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            TxtId.Visible = false;
            ConfirmaEliminacion = false;

            // Manejo de botones según si hay id en la URL
            if (Request.QueryString["id"] != null)
            {
                btnEliminarArt.Visible = true;
                btnInactivar.Visible = true;
            }
            else
            {
                btnEliminarArt.Visible = false;
                btnInactivar.Visible = false;
            }
            try
            {
               
                    CargaCategorias();
                    CargaProveedores();

                

                string idStr = Request.QueryString["id"] ?? "";

                if (!string.IsNullOrEmpty(idStr) && int.TryParse(idStr, out int id))
                {
                    PrecargaArticulos(id);
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
            }
        }

        private void CargaProveedores()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            ddlProveedores.DataSource = negocio.listarProveedores();
            ddlProveedores.DataValueField = "Id";
            ddlProveedores.DataTextField = "Nombre";
            ddlProveedores.DataBind();
        }

        private void CargaCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            ddlCategoria.DataSource = negocio.listarCategorias();
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataBind();

        }

        private void PrecargaArticulos(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            List<Articulo> lista = negocio.listaArticulosConSP(id);

            if (lista.Count > 0)
            {
                Articulo seleccionado = lista[0];
                Session["artSeleccionado"] = seleccionado;

                if (!IsPostBack)
                {
                    TxtId.Text = id.ToString();
                    txtNombre.Text = seleccionado.Nombre;
                    txtPrecio.Text = seleccionado.Precio.ToString("0.00");
                    txtDescripcion.Text = seleccionado.Descripcion;
                    // Imagen
                    if (!string.IsNullOrEmpty(seleccionado.UrlImagen))
                        imgArticulo.ImageUrl = "~/Images/" + seleccionado.UrlImagen;
                    else
                        imgArticulo.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbh24Xuyh_r2IQZmUNeGgpxiWSMr-jn1xwHQ&s";

                    //  Carmbia texto del boton 
                    if (!seleccionado.Activo)
                        btnInactivar.Text = "Reactivar";

                    //  Categoría
                    if (seleccionado.idCategoria > 0)
                        ddlCategoria.SelectedValue = seleccionado.idCategoria.ToString();

                    //  Proveedor
                    if (seleccionado.idProveedor > 0)
                        ddlProveedores.SelectedValue = seleccionado.idProveedor.ToString();
                }
            }
            else
            {
                Session["Error"] = "No se encontró el artículo solicitado";
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();

                nuevo.Nombre = txtNombre.Text;
                nuevo.Precio = double.Parse(txtPrecio.Text);
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Proveedor = new Proveedor();
                nuevo.idProveedor = int.Parse(ddlProveedores.SelectedValue);
                nuevo.Categoria = new Categoria();
                nuevo.idCategoria = int.Parse(ddlCategoria.SelectedValue);
                nuevo.Activo = true;

                // Guardar imagen solo si hay archivo nuevo
                if (txtImagenArticulo.HasFile)
                {
                    string extension = System.IO.Path.GetExtension(txtImagenArticulo.FileName);
                    string nombreArchivo = "perfil-" + nuevo.Id + extension;
                    string rutaDisco = Server.MapPath("~/Images/" + nombreArchivo);
                    txtImagenArticulo.SaveAs(rutaDisco);
                    nuevo.UrlImagen = nombreArchivo;
                }

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(TxtId.Text);
                    negocio.modificarArticulo(nuevo);
                }
                else
                {
                    nuevo.Activo = true;
                    negocio.agregarArticulo(nuevo);
                }
                Response.Redirect("Default.aspx");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnInactivar_Click1(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo seleccionado = (Articulo)Session["artSeleccionado"];
                negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);
                Response.Redirect("Default.aspx");

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }

        protected void btnEliminarFotoArticulo_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado = (Articulo)Session["artSeleccionado"];
            try
            {
                string rutaImg = Server.MapPath("~/Images/" + seleccionado.UrlImagen);
                if (System.IO.File.Exists(rutaImg))
                {
                    System.IO.File.Delete(rutaImg);
                    seleccionado.UrlImagen = null;
                    negocio.modificarArticulo(seleccionado);

                    imgArticulo.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbh24Xuyh_r2IQZmUNeGgpxiWSMr-jn1xwHQ&s";
                    Response.Redirect(Request.RawUrl, false);
                }

            }
            catch (Exception ex)
            {
                Session["error"] = ex.ToString();
                Response.Redirect("error.aspx", false);
            }

        }

        protected void btnEliminarArt_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnConfirmaEliminacion_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            negocio.eliminar(int.Parse(TxtId.Text));
            Response.Redirect("Default.aspx");
        }
    }
}