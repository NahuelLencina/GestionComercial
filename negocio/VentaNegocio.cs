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
        public int agregarVenta(Venta nueva)
        {

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("AgregarVenta_SP");

                datos.setearParametro("@IDCliente", nueva.IdCliente);
                datos.setearParametro("@Total", nueva.Total);
                datos.setearParametro("@IdUsuario", nueva.IdUsuario);

                int idVenta = datos.ejecutarAccionScalar();

                return idVenta;
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
