using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> listarProveedores()
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, Nombre, cuit From PROVEEDOR");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor proveedor = new Proveedor();
                    proveedor.Id = (int)datos.Lector["Id"];
                    proveedor.Nombre = (string)datos.Lector["Nombre"];
                    
                    lista.Add(proveedor);
                }

                return lista;
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
