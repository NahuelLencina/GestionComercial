using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listarCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select Id, Nombre From CATEGORIA");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria= new Categoria();
                    categoria.Id = (int)datos.Lector["Id"];
                    categoria.Nombre = (string)datos.Lector["Nombre"];
                    lista.Add(categoria);
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
