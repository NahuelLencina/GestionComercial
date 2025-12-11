using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using negocio;
using dominio;

namespace negocio
{
    public class VentaNegocio
    {
        public void agregarVenta(Venta nueva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("AgregarVenta_SP");
                datos.setearParametro("@IDCliente", nueva.IdCliente);
                datos.setearParametro("@Fecha", nueva.Fecha);
                datos.setearParametro("@Total",nueva.Total);
                datos.setearParametro("@IdUsuario", nueva.IdUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                datos.cerrarConexion();
            }
        }

    }
}
