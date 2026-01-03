using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
               // datos.setearConsulta("select IdCliente ,Cliente, Fecha ,Total from ventas");

                datos.setearConsulta("Select v.IdCliente, c.Cliente, c.Cuit, v.Fecha, v.Total From Ventas v Inner Join Clientes c on c.Id = v.Idcliente");


                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta venta = new Venta();
                   

                    venta.IdCliente = (int)datos.Lector["IdCliente"];
                    venta.Nombre = datos.Lector["Cliente"].ToString();
                    venta.Fecha = (DateTime)datos.Lector["Fecha"];
                    venta.Total = (double)datos.Lector["Total"];
                    venta.Cuit = datos.Lector["Cuit"].ToString();

                      

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
