using Northwind_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos_LinQ
{
    public static class MontoEmpleado_Datos
    {
        public static List<MontoEmpleado> ObtenerMontoPorEmpleado()
        {
            using (DataClasses1DataContext contexto = new DataClasses1DataContext())
            {
                var consulta = contexto.vw_GestionOrdenesPorEmpleados
                    .GroupBy(v => v.NombreEmpleado)
                    .Select(grupo => new MontoEmpleado
                    {
                        NombreEmpleado = grupo.Key,
                        MontoTotalOrden = grupo.Sum(v => v.montoReacudado) ?? 0
                    })
                    .OrderBy(x => x.MontoTotalOrden)
                    .ToList();

                return consulta;
            }
        }
    }
}
