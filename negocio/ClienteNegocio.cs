using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> storeListarClientes()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storeListarClientes ");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente cliente = new Cliente();

                    cliente.Id = (int)datos.Lector["Id"];
                    cliente.Nombre = (string)datos.Lector["Cliente"];

                    if (!(datos.Lector["Telefono"] is DBNull))
                        cliente.Telefono = (string)datos.Lector["Telefono"];
                    if (!(datos.Lector["Cuit"] is DBNull))
                        cliente.Cuit = (string)datos.Lector["Cuit"];
                    if (!(datos.Lector["Correo"] is DBNull))
                        cliente.Email = (string)datos.Lector["Correo"];
                    if (!(datos.Lector["Dirección"] is DBNull))
                        cliente.Direccion = (string)datos.Lector["Dirección"];

                    lista.Add(cliente);
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

        public List<Cliente> storeListarClientes(string nombre) 
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("storeListarClientes");
                datos.setearParametro("@Nombre", nombre);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente cliente = new Cliente();

                    cliente.Id = (int)datos.Lector["Id"];
                    cliente.Nombre = (string)datos.Lector["Cliente"];

                    if (!(datos.Lector["Telefono"] is DBNull))
                        cliente.Telefono = (string)datos.Lector["Telefono"];
                    if (!(datos.Lector["Cuit"] is DBNull))
                        cliente.Cuit = (string)datos.Lector["Cuit"];
                    if (!(datos.Lector["Correo"] is DBNull))
                        cliente.Email = (string)datos.Lector["Correo"];
                    if (!(datos.Lector["Dirección"] is DBNull))
                        cliente.Direccion = (string)datos.Lector["Dirección"];

                    lista.Add(cliente);

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

        public void agregarCliente(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO clientes values " +
                    "(@nombre, @telefono, @cuit, @email, @direccion)");

                datos.setearParametro("@nombre", cliente.Nombre);
                datos.setearParametro("@telefono", cliente.Telefono);
                datos.setearParametro("@cuit", cliente.Cuit);
                datos.setearParametro("@email", cliente.Email);
                datos.setearParametro("@direccion", cliente.Direccion);
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
