using Northwind_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_Datos
{
    public static class MontoEmpleado_Datos
    {
        public static List<MontoEmpleado> ObtenerMontoPorEmpleado()
        {
                List<MontoEmpleado> lista = new List<MontoEmpleado>();

                SqlConnection conexion = Conexion.ObtenerConexion();
            using (conexion)
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT 
                           NombreEmpleado,
                           SUM(montoReacudado) AS MontoTotalOrden
                        FROM vw_GestionOrdenesPorEmpleados
                        GROUP BY NombreEmpleado
                        ORDER BY MontoTotalOrden";

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new MontoEmpleado
                        {
                            NombreEmpleado = dr["NombreEmpleado"].ToString(),
                            MontoTotalOrden = Convert.ToDecimal(dr["MontoTotalOrden"])
                        });
                    }
                }
            }
                return lista;
            }
    }
}

