using Northwind_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos_LinQ
{
    public static class ClienteGastos_Datos
    {
        public static List<ClienteGasto> ObtenerTop10Clientes()
        {
            using (DataClasses1DataContext contexto=new DataClasses1DataContext())
            {
                var consulta = contexto.vw_GestionOrdenesPorEmpleados
                    .GroupBy(v => new { v.CompanyName, v.Country })
                    .Select(grupo => new ClienteGasto
                    {
                        NombreCliente = grupo.Key.CompanyName,
                        Pais = grupo.Key.Country,
                        TotalGastado = grupo.Sum(v => v.montoReacudado) ?? 0,
                        NumeroOrdenes = grupo.Count()
                    })
                    .OrderByDescending(x => x.TotalGastado)
                    .Take(10)
                    .ToList();

                return consulta;
            }
        }
    }
}
