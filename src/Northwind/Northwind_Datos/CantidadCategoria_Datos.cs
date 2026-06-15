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
    public static class CantidadCategoria_Datos
    {
        public static List<CantidadCategoria> ObtenerCantidadPorCategoria()
        {
            List<CantidadCategoria> lista = new List<CantidadCategoria>();

            SqlConnection conexion = Conexion.ObtenerConexion();
            using (conexion)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT 
                           CategoryName AS NombreCategoria,
                           COUNT(montoReacudado) AS NumeroOrdenes
                        FROM vw_GestionOrdenesPorEmpleados
                        GROUP BY CategoryName
                        ORDER BY NumeroOrdenes DESC";

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new CantidadCategoria
                        {
                            NombreCategoria = dr["NombreCategoria"].ToString(),
                            NumeroOrdenes = Convert.ToInt32(dr["NumeroOrdenes"])
                        });
                    }
                }
            }
            return lista;
        }
    }
}
