using dominio;
using negocio;
using System;

namespace GestionBicicleteria
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Trainee usuario = Session["trainee"] as Trainee;

                if (!Seguridad.sessionActiva(Session["trainee"]))
                {

                    navfavoritos.Visible = false;
                    navPokList.Visible = false;


                    if (!(Page is Login || Page is Registro || Page is Default || Page is Error))
                    {
                        Response.Redirect("~/Login.aspx", false);
                    }
                }
                else
                {
                    lblUserName.Text = usuario.Usuario;

                    if (string.IsNullOrEmpty(usuario.ImagenPerfilUrl))
                    {
                        // Imagen por defecto si no tiene foto
                        imgAvatar.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbh24Xuyh_r2IQZmUNeGgpxiWSMr-jn1xwHQ&s";
                    }
                    else
                    {
                        imgAvatar.ImageUrl = "~/Images/" + usuario.ImagenPerfilUrl;
                    }

                    if (Seguridad.esAdmin(usuario))
                    {
                        lblUserName.Text += " Admin";
                        navPokList.Visible = true;
                    }
                    else
                        navPokList.Visible = false;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void btnRegistrarme_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Registro.aspx");
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }


    }
}