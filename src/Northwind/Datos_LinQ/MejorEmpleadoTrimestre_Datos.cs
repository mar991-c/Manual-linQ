using Northwind_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos_LinQ
{
    public static class MejorEmpleadoTrimestre_Datos
    {
        public static List<MejorEmpleadoTrimestre> ObtenerTop3EmpleadosTrimestre()
        {
            using (DataClasses1DataContext contexto= new DataClasses1DataContext())
            {
                int[] añosPermitidos = { 1997, 1998 };
                int[] mesesTrimestre = { 10, 11, 12 };
                string[] categoriasExcluidas = { "Condiments", "Confections" };

                var consulta = contexto.vw_GestionOrdenesPorEmpleados
                    .Where(v => añosPermitidos.Contains(v.añoOrden ?? 0) &&
                                mesesTrimestre.Contains(v.MesOrden ?? 0) &&
                                !categoriasExcluidas.Contains(v.CategoryName))
                    .GroupBy(v => v.NombreEmpleado)
                    .Select(grupo => new MejorEmpleadoTrimestre
                    {
                        NombreEmpleado = grupo.Key,
                        MontoTotalOrden = grupo.Sum(v => v.montoReacudado) ?? 0
                    })
                    .OrderByDescending(x => x.MontoTotalOrden)
                    .Take(3)
                    .ToList();

                return consulta;
            }
        }
    }
}
