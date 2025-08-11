using dominio;
using negocio;
using System;

namespace GestionBicicleteria
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Seguridad.sessionActiva(Session["trainee"]))
                {
                    Trainee user = (Trainee)Session["trainee"];

                    txtEmail.Text = user.Email;
                    txtNombre.Text = user.Nombre;
                    txtApellido.Text = user.Apellido;
                    txtUsuario.Text = user.Usuario;
                    txtFechaNac.Text = user.FechaNacimiento.ToString("yyyy-MM-dd");
               
                    if (!string.IsNullOrEmpty(user.ImagenPerfilUrl))
                        imgnuevoPerfil.ImageUrl = "~/Images/" + user.ImagenPerfilUrl;
                    else
                        imgnuevoPerfil.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbh24Xuyh_r2IQZmUNeGgpxiWSMr-jn1xwHQ&s";
                }
            }
        }

        protected void btnGuardarPerfil_Click(object sender, EventArgs e)
        {
            Trainee user = (Trainee)Session["trainee"];
            TraineeNegocio negocio = new TraineeNegocio();

            try
            {
                if (Seguridad.sessionActiva(user))
                {
                    user.Apellido = txtApellido.Text;
                    user.Nombre = txtNombre.Text;
                    user.Usuario = txtUsuario.Text;
                    user.FechaNacimiento = DateTime.Parse(txtFechaNac.Text);

                    // Guardar imagen solo si hay archivo nuevo
                    if (txtImagen.HasFile)
                    {
                        string extension = System.IO.Path.GetExtension(txtImagen.FileName);
                        string nombreArchivo = "perfil-" + user.Id + extension;
                        string rutaDisco = Server.MapPath("~/Images/" + nombreArchivo);
                        txtImagen.SaveAs(rutaDisco);
                        user.ImagenPerfilUrl = nombreArchivo;
                    }
                    negocio.actualizar(user);
                    Response.Redirect("Default.aspx", false);
                }
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("error.aspx", false);
            }
        }

        protected void btnEliminarFoto_Click(object sender, EventArgs e)
        {
            TraineeNegocio negocio = new TraineeNegocio();
            Trainee user = (Trainee)Session["trainee"];

            try
            {
                string rutaImagen = Server.MapPath("~/Images/" + user.ImagenPerfilUrl);

                // Verificar si el archivo existe antes de intentar eliminarlo
                if (System.IO.File.Exists(rutaImagen))
                {
                    System.IO.File.Delete(rutaImagen); // Eliminación física

                    // Lógicamente eliminar la imagen
                    user.ImagenPerfilUrl = null;
                    negocio.actualizar(user);

                    //   Session["trainee"] = user;

                    // Actualizo imagen en esta página
                    imgnuevoPerfil.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbh24Xuyh_r2IQZmUNeGgpxiWSMr-jn1xwHQ&s";

                    // 🔁 Refresca la página para que el MasterPage recargue la sesión actualizada
                    Response.Redirect(Request.RawUrl, false);

                }
            }

            catch (Exception ex)
            {
                Session["error"] = ex.ToString();
                Response.Redirect("error.aspx", false);
            }
        }
    }
}