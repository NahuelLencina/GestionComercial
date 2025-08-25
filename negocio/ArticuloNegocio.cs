using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using negocio;
using System.Data.SqlClient;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listaArticulosConSP()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("storedListar");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Precio = (double)datos.Lector["Precio"];
                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    if (!(datos.Lector["idCategoria"] is DBNull))
                        aux.idCategoria = (int)datos.Lector["idCategoria"];


                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    if (!(datos.Lector["idProveedor"] is DBNull))
                    {
                        aux.idProveedor = (int)datos.Lector["idProveedor"];
                        aux.Proveedor = new Proveedor
                        {
                            Id = aux.idProveedor,
                            Nombre = (string)datos.Lector["ProveedorNombre"],
                            Cuit = (string)datos.Lector["ProveedorCuit"]
                            
                        };
                    }
                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];
                    else
                        aux.Descripcion = "";

                    lista.Add(aux);
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


        public List<Articulo> listaArticulosConSP(int id)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("storedListar");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Precio = (double)datos.Lector["Precio"];
                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());
                
                    if (!(datos.Lector["idCategoria"] is DBNull))
                        aux.idCategoria = (int)datos.Lector["idCategoria"];


                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    if (!(datos.Lector["idProveedor"] is DBNull))
                    {
                        aux.idProveedor = (int)datos.Lector["idProveedor"];
                        aux.Proveedor = new Proveedor
                        {
                            Id = aux.idProveedor,
                            Nombre = (string)datos.Lector["ProveedorNombre"],
                            Cuit = (string)datos.Lector["ProveedorCuit"]

                        };
                    }
                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
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

        public void agregarArticulo(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {         
                datos.setearConsulta("INSERT INTO PRODUCTOS (Nombre, Precio, UrlImagen, Activo, idCategoria, idProveedor, descripcion) VALUES(@nombre, @precio, @img, @activo, @idCategoria, @idProveedor, @descripcion)");
                datos.setearParametro("@nombre", art.Nombre);
                datos.setearParametro("@precio", art.Precio);
                datos.setearParametro("@activo", art.Activo);
                if (string.IsNullOrEmpty(art.UrlImagen))
                    datos.setearParametro("@img", DBNull.Value);
                else
                    datos.setearParametro("@img", art.UrlImagen);
                datos.setearParametro("@descripcion", art.Descripcion);
                datos.setearParametro("@idCategoria", art.idCategoria);
                datos.setearParametro("@idProveedor", art.idProveedor);

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
        public void eliminar(int Id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from PRODUCTOS where Id = @id");
                datos.setearParametro("@id", Id);
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

        public void eliminarLogico(int id, bool activo = false)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update PRODUCTOS set Activo = @activo where id = @id");
                datos.setearParametro("@id", id);
                datos.setearParametro("@activo", activo);
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


        public void modificarArticulo(Articulo art)
        {
           AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("UPDATE PRODUCTOS SET Nombre=@nombre, Precio=@precio, UrlImagen=@img, Activo=@activo, idCategoria=@idCategoria, idProveedor=@idProveedor, descripcion=@descripcion  WHERE Id=@id");
                datos.setearParametro("@nombre", art.Nombre);
                datos.setearParametro("@precio", art.Precio);
                datos.setearParametro("@id", art.Id);
                datos.setearParametro("@descripcion", art.Descripcion);
                datos.setearParametro("@activo", art.Activo);
                if (string.IsNullOrEmpty(art.UrlImagen))
                    datos.setearParametro("@img", DBNull.Value);
                else
                    datos.setearParametro("@img",art.UrlImagen);
                datos.setearParametro("@idCategoria", art.idCategoria);
                datos.setearParametro("@idProveedor", art.idProveedor);

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
