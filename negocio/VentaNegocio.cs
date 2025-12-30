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


        public List<Venta> listaVentas()
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select v.IdCLiente, c.Cliente, v.Fecha , v.Total from ventas v inner join clientes as c ON C.ID = V.IDCliente");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta venta = new Venta();

                    venta.IdCliente = datos.Lector["IDCliente"] != DBNull.Value
                        ? Convert.ToInt32(datos.Lector["IDCliente"])
                        : 0;

                    venta.Fecha = datos.Lector["Fecha"] != DBNull.Value
                        ? Convert.ToDateTime(datos.Lector["Fecha"])
                        : DateTime.MinValue;

                    venta.Total = datos.Lector["Total"] != DBNull.Value
                        ? Convert.ToDouble(datos.Lector["Total"])
                        : 0;

                    lista.Add(venta);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return lista;
        }
    }

}
