using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class VentaItemNegocio
    {
        public void agregarVentaItem(VentaItem nuevoItem)
        { 
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT into ventasItems(IDVenta, IDProducto, PrecioUnitario, Cantidad)" + "values (@IdVenta, @IdProducto,@PrecioUnitario,@Cantidad)");
                datos.setearParametro("@IdVenta", nuevoItem.IdVenta);
                datos.setearParametro("@IdProducto", nuevoItem.IdProducto);
                datos.setearParametro("@PrecioUnitario", nuevoItem.PrecioUnitario);
                datos.setearParametro("@Cantidad", nuevoItem.Cantidad);
     

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
