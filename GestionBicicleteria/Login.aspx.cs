using dominio;
using negocio;
using System;

namespace GestionBicicleteria
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Trainee trainee = new Trainee();
            TraineeNegocio negocio = new TraineeNegocio();
            try
            {
                trainee.Email = txtUsuario.Text;
                trainee.Pass = txtPass.Text;
                if (negocio.Login(trainee))
                {
                    Session.Add("trainee", trainee);
                    Response.Redirect("~/MiPerfil.aspx", false);
                }
                else
                {
                    lblMensageError.Text = "Usuario o Contraseña incorrecto!";
                    //Session.Add("error", "Debes Loguearte para ingresar");
                    //Response.Redirect("Error.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}