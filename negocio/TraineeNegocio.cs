using dominio;
using System;

namespace negocio
{
    public class TraineeNegocio
    {
        public int insertarNuevo(Trainee nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("insertarNuevo");
                datos.setearParametro("@Usuario", nuevo.Usuario);
                datos.setearParametro("@pass", nuevo.Pass);
                datos.setearParametro("@Email", nuevo.Email);
                return datos.ejecutarAccionScalar();

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

        public void actualizar(Trainee user)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update Usuario set ImagenUrl = @imagen, Nombre = @nombre, Apellido = @apellido, FechaNacimiento = @fechaNacimiento, Usuario = @usuario where Id = @id");
                //             datos.setearParametro("@imagen", user.ImagenPerfilUrl != null ? user.ImagenPerfilUrl : (object)DBNull.Value);
                // Uso de operador null Coalescing
                datos.setearParametro("@imagen", (object)user.ImagenPerfilUrl ?? DBNull.Value);

                datos.setearParametro("@nombre", user.Nombre);
                datos.setearParametro("@apellido", user.Apellido);
                datos.setearParametro("@fechaNacimiento", user.FechaNacimiento);
                datos.setearParametro("@usuario", user.Usuario);
                datos.setearParametro("@id", user.Id);
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

        public bool Login(Trainee trainee)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select Id, email, pass, TipoUser, imagenUrl, nombre, apellido, fechaNacimiento, Usuario from USUARIO Where email = @email and pass = @pass");
                datos.setearParametro("@email", trainee.Email);
                datos.setearParametro("@pass", trainee.Pass);
                datos.ejecutarLectura();


                if (datos.Lector.Read())
                {
                    trainee.Id = (int)datos.Lector["Id"];
                    trainee.Admin = (bool)datos.Lector["TipoUser"];

                    if (!(datos.Lector["imagenUrl"] is DBNull))
                        trainee.ImagenPerfilUrl = (string)datos.Lector["imagenUrl"];
                    if (!(datos.Lector["Nombre"] is DBNull))
                        trainee.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["Apellido"] is DBNull))
                        trainee.Apellido = (string)datos.Lector["Apellido"];
                    if (!(datos.Lector["Usuario"] is DBNull))
                        trainee.Usuario = (string)datos.Lector["Usuario"];
                    if (!(datos.Lector["fechaNacimiento"] is DBNull))
                        trainee.FechaNacimiento = DateTime.Parse(datos.Lector["fechaNacimiento"].ToString());


                    //  trainee.Admin = Convert.ToBoolean(datos.Lector["TipoUser"]);
                    return true;
                }
                return false;
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
