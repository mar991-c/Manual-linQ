using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GestionUsuariosEntidades;
using System.Data;
using System.Data.SqlClient;

namespace GestionUsuariosDatos
{
    public static class PacienteDatos
    {
        public static PacienteEntidades Nuevo(PacienteEntidades paciente)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(Properties.Settings.Default.ConexionBD);
                conexion.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType= CommandType.Text;
                cmd.CommandText = @"INSERT INTO [dbo].[Pacientes]
                                               ([Id_Genero]
                                               ,[apellido]
                                               ,[nombres]
                                               ,[cedula]
                                               ,[telefono]
                                               ,[fechaNacimiento]
                                               ,[direccion]
                                               ,[CodigoIESS]
                                               ,[Afiliado])
                                                 VALUES
                                                       (@Id_Genero,@apellido, @nombres, @cedula, @telefono,
                                                         @fechaNacimiento, @direccion, @CodigoIESS, @Afiliado);
                                                        SELECT SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@Id_Genero", paciente.Id_Genero);
                cmd.Parameters.AddWithValue("@apellido", paciente.Apellido);
                cmd.Parameters.AddWithValue("@nombres", paciente.Nombre);
                cmd.Parameters.AddWithValue("@cedula", paciente.Cedula);
                cmd.Parameters.AddWithValue("@telefono", paciente.Telefono);
                cmd.Parameters.AddWithValue("@fechaNacimiento", paciente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@direccion", paciente.Direccion);
                cmd.Parameters.AddWithValue("@CodigoIESS", paciente.CodigoIESS);
                cmd.Parameters.AddWithValue("@Afiliado", paciente.Afiliado);

                var id_paciente=Convert.ToInt32(cmd.ExecuteScalar());
                paciente.ID = id_paciente;
                conexion.Close();
                return paciente;
            }
            catch (Exception e)
            {
                var error=e.Message;
                return null;
            }
        }
        public static PacienteEntidades Actualizar(PacienteEntidades paciente)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(GestionUsuariosDatos.Properties.Settings.Default.ConexionBD);
                conexion.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE [dbo].[Pacientes]
                                   SET [Id_Genero] = @id_genero
                                       ,[apellido] = @apellido
                                      ,[nombres] = @nombres
                                      ,[cedula] = @cedula
                                      ,[telefono] =@telefono
                                      ,[fechaNacimiento] =@fechaNacimiento
                                      ,[direccion] =@direccion
                                      ,[CodigoIESS]=@CodigoIESS
                                      ,[Afiliado]=@Afiliado
                                 WHERE 
                                    id =@id ";
                cmd.Parameters.AddWithValue("@id_genero", paciente.Id_Genero);
                cmd.Parameters.AddWithValue("@nombres", paciente.Nombre);
                cmd.Parameters.AddWithValue("@apellido", paciente.Apellido);
                cmd.Parameters.AddWithValue("@cedula", paciente.Cedula);
                cmd.Parameters.AddWithValue("@fechaNacimiento", paciente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@telefono", paciente.Telefono);
                cmd.Parameters.AddWithValue("@direccion", paciente.Direccion);
                cmd.Parameters.AddWithValue("@CodigoIESS", paciente.CodigoIESS);
                cmd.Parameters.AddWithValue("@Afiliado", paciente.Afiliado);
                cmd.Parameters.AddWithValue("@id", paciente.ID);
                
                cmd.ExecuteNonQuery();//Elemto si se guardo o no 
                conexion.Close();
                return paciente;
            }catch(Exception e)
            {
                string error = e.Message;
                return null;
            }
        }

        public static List<PacienteEntidades> DevolverListaPaciente()
        {
            try
            {
                
                List<PacienteEntidades> listaPacientes = new List<PacienteEntidades>();
                SqlConnection conexion = new SqlConnection(GestionUsuariosDatos.Properties.Settings.Default.ConexionBD);
                conexion.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT p.[id]
                                      ,p.[Id_Genero]
                                      ,g.[nombre] as genero
                                      ,p.[apellido]
                                      ,p.[nombres]
                                      ,p.[cedula]
                                      ,p.[telefono]
                                      ,p.[fechaNacimiento]
                                      ,p.[direccion]
                                      ,p.[CodigoIESS]
                                      ,p.[Afiliado]
                                  FROM [dbo].[Pacientes]p
                                INNER JOIN Genero g ON p.Id_Genero=g.id; ";

                using (var dr = cmd.ExecuteReader())//para llamar datos
                {
                    while (dr.Read())
                    {
                        PacienteEntidades paciente = new PacienteEntidades();
                        paciente.ID = Convert.ToInt32(dr["id"].ToString());
                        paciente.Id_Genero = Convert.ToInt32(dr["Id_Genero"].ToString());
                        paciente.Genero = dr["genero"].ToString();
                        paciente.Nombre = dr["nombres"].ToString();
                        paciente.Apellido = dr["apellido"].ToString();
                        paciente.Cedula = dr["cedula"].ToString();
                        paciente.FechaNacimiento = Convert.ToDateTime(dr["fechaNacimiento"].ToString());
                        paciente.Telefono = dr["telefono"].ToString();
                        paciente.Direccion = dr["direccion"].ToString();
                        paciente.Afiliado = Convert.ToBoolean(dr["Afiliado"].ToString());
                        paciente.CodigoIESS = dr["CodigoIESS"].ToString();

                        listaPacientes.Add(paciente);


                    }
                }
                conexion.Close();
                return listaPacientes;

            }
            catch (Exception e)
            {
                string error = e.Message;
                return null;
            }
        }

        public static PacienteEntidades CargarPacientePorId(int id)
        {
            try
            {
                
                SqlConnection conexion = new SqlConnection(GestionUsuariosDatos.Properties.Settings.Default.ConexionBD);
                conexion.Open();
                PacienteEntidades paciente = null;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT p.[id]
                                      ,p.[Id_Genero]
                                      ,g.[nombre] as genero
                                      ,p.[apellido]
                                      ,p.[nombres]
                                      ,p.[cedula]
                                      ,p.[telefono]
                                      ,p.[fechaNacimiento]
                                      ,p.[direccion]
                                      ,p.[CodigoIESS]
                                      ,p.[Afiliado]
                                  FROM [dbo].[Pacientes]p
                                INNER JOIN Genero g ON p.Id_Genero=g.id 
                                    WHERE p.id = @id";

                cmd.Parameters.AddWithValue("@id", id);
                
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        paciente=new PacienteEntidades();
                        paciente.ID = Convert.ToInt32(dr["id"].ToString());
                        paciente.Nombre = dr["nombres"].ToString();
                        paciente.Id_Genero = Convert.ToInt32(dr["Id_Genero"].ToString());
                        paciente.Genero=dr["genero"].ToString();
                        paciente.Apellido = dr["apellido"].ToString();
                        paciente.Cedula = dr["cedula"].ToString();
                        paciente.FechaNacimiento = Convert.ToDateTime(dr["fechaNacimiento"].ToString());
                        paciente.Telefono = dr["telefono"].ToString();
                        paciente.Direccion = dr["direccion"].ToString();
                        paciente.CodigoIESS = dr["CodigoIESS"].ToString();
                        paciente.Afiliado = Convert.ToBoolean(dr["Afiliado"]);

                    }
                }
                conexion.Close();
                return paciente;
            }
            catch (Exception e)
            {

                string error = e.Message;
                return null;
            }
        }
        public static bool EliminarPacientePorID(int id)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(GestionUsuariosDatos.Properties.Settings.Default.ConexionBD);
                conexion.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"DELETE FROM [dbo].[Paciente]
                                     WHERE id=@id";

                cmd.Parameters.AddWithValue("@id", id);
                var numeroFilasAfectadas = cmd.ExecuteNonQuery();
                if (numeroFilasAfectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                return false;
            }
        }

    }
}
