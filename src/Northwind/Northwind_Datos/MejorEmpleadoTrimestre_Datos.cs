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
    public static class MejorEmpleadoTrimestre_Datos
    {
        public static List<MejorEmpleadoTrimestre> ObtenerTop3EmpleadosTrimestre()
        {
            List<MejorEmpleadoTrimestre> lista = new List<MejorEmpleadoTrimestre>();

            SqlConnection conexion = Conexion.ObtenerConexion();
            using (conexion)
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT TOP 3
                           NombreEmpleado,
                           SUM(montoReacudado) AS MontoTotalOrden
                        FROM vw_GestionOrdenesPorEmpleados
                        WHERE añoOrden IN (1997, 1998)
                        AND MesOrden IN (10, 11, 12)
                        AND CategoryName NOT IN ('Condiments', 'Confections')
                        GROUP BY NombreEmpleado
                        ORDER BY MontoTotalOrden DESC";

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new MejorEmpleadoTrimestre
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
