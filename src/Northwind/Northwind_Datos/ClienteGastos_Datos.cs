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
    public static class ClienteGastos_Datos
    {
        public static List<ClienteGasto> ObtenerTop10Clientes()
        {
            List<ClienteGasto> lista = new List<ClienteGasto>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection= conexion;
                cmd.CommandType=CommandType.Text;
                cmd.CommandText= @"SELECT TOP 10
                              cu.CompanyName AS NombreCliente,
                              cu.Country AS Pais,
                              SUM(od.Quantity * od.UnitPrice) AS TotalGastado,
                              COUNT(DISTINCT o.OrderID) AS NumeroOrdenes
                           FROM Customers cu
                           INNER JOIN Orders o ON cu.CustomerID = o.CustomerID
                           INNER JOIN [Order Details] od ON o.OrderID = od.OrderID
                           GROUP BY cu.CompanyName, cu.Country
                           ORDER BY TotalGastado DESC";
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ClienteGasto
                        {
                            NombreCliente = dr["NombreCliente"].ToString(),
                            Pais = dr["Pais"].ToString(),
                            TotalGastado = (decimal)dr["TotalGastado"],
                            NumeroOrdenes = (int)dr["NumeroOrdenes"]
                        });
                    }
                }

                   
            }
            return lista;
        }
    }
}
