using System;
using System.Net;
using System.Net.Mail;

namespace negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            server = new SmtpClient();
            server.Credentials = new NetworkCredential("ac192f4956ec27", "a8eead52986a49");
            server.EnableSsl = true;
            server.Port = 2525;
            server.Host = "sandbox.smtp.mailtrap.io";
        }

        public void armarCorreo(string emailDestino, string cuerpo, string asunto)
        {
            email = new MailMessage();
            email.From = new MailAddress("noresponder@pokedexWeb.com");
            email.To.Add(emailDestino);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            //email.Body = "<h1> Reporte de materias a las que se haya inscripto </h1> <br>Hola, te inscribiste... bla bla "
            email.Body = cuerpo;
            ;
        }

        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
