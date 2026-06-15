using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_Datos
{
    public static class Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(Northwind_Datos.Properties.Settings.Default.ConexionBD);
            conexion.Open();
            return conexion;

        }
    }
}
