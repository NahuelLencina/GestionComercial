using dominio;
using negocio;
using System;

namespace GestionBicicleteria
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            EmailService emailService = new EmailService();
            try
            {
                Trainee user = new Trainee();
                TraineeNegocio negocio = new TraineeNegocio();
                user.Usuario = txtUsuario.Text;
                user.Pass = txtPass.Text;
                user.Email = txtEmail.Text;
                user.Id = negocio.insertarNuevo(user);
                Session.Add("trainee", user);

                emailService.armarCorreo(user.Email, "Bienvenida ", "Hola! te damos la bienvenida");
                emailService.enviarEmail();
                Response.Redirect("~/", false);
                Context.ApplicationInstance.CompleteRequest();

            }
            catch (Exception ex)
            {
                Session["error"] = ex.ToString();
                Response.Redirect("Error.aspx");
            }
        }
    }
}