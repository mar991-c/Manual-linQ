using GestionUsuariosEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuariosDatos
{
    public static class GeneroDatos
    {
        public static List<GeneroEntidades> DevolverListaGenero()
        {
			try
			{
                List<GeneroEntidades> lista = new List<GeneroEntidades>();
                SqlConnection conexion = new SqlConnection(Properties.Settings.Default.ConexionBD);
                conexion.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT [Id]
                                  ,[Nombre]
                                   FROM [dbo].[Genero]";
                using (var dr = cmd.ExecuteReader())//para llamar datos
                {
                    while (dr.Read())
                    {
                        GeneroEntidades genero = new GeneroEntidades();
                        genero.Id = Convert.ToInt32(dr["Id"].ToString());
                        genero.Nombre = dr["Nombre"].ToString();
                        lista.Add(genero);


                    }
                }
                conexion.Close();
                return lista;
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
