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
    public static class VentaPais_Datos
    {
        public static List<VentaPais> ObtenerVentasPorPais()
        {
            List<VentaPais> lista = new List<VentaPais>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT 
                              cu.Country AS Pais,
                              SUM(od.Quantity * od.UnitPrice) AS TotalVentas,
                              COUNT(DISTINCT cu.CustomerID) AS NumeroClientes
                           FROM Customers cu
                           INNER JOIN Orders o ON cu.CustomerID = o.CustomerID
                           INNER JOIN [Order Details] od ON o.OrderID = od.OrderID
                           GROUP BY cu.Country
                           ORDER BY TotalVentas DESC";
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new VentaPais
                        {
                            Pais = dr["Pais"].ToString(),
                            TotalVentas = (decimal)dr["TotalVentas"],
                            NumeroClientes = (int)dr["NumeroClientes"]
                        });
                    }
                }
            }
            return lista;
        }
    }
}
