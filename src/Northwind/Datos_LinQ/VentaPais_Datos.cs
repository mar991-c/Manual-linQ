using Northwind_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos_LinQ
{
    public static class VentaPais_Datos
    {
        public static List<VentaPais> ObtenerVentasPorPais()
        {
            using (DataClasses1DataContext contexto = new DataClasses1DataContext())
            {
                return contexto.vw_GestionOrdenesPorEmpleados
                    .GroupBy(v => v.Country)
                    .Select(grupo => new VentaPais
                    {
                        Pais = grupo.Key,
                        TotalVentas = grupo.Sum(v => v.montoReacudado) ?? 0,
                        NumeroClientes = grupo.Select(v => v.CompanyName)
                                              .Distinct()
                                              .Count()
                    })
                    .OrderByDescending(x => x.TotalVentas)
                    .ToList();
            }
        }
    }
}
