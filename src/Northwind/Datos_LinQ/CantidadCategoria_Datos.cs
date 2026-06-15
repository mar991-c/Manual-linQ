using Northwind_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos_LinQ
{
    public static class CantidadCategoria_Datos
    {
        public static List<CantidadCategoria> ObtenerCantidadPorCategoria()
        {
            using (DataClasses1DataContext contexto= new DataClasses1DataContext())
            {
                var consulta = contexto.vw_GestionOrdenesPorEmpleados
                    .GroupBy(v => v.CategoryName)
                    .Select(grupo => new CantidadCategoria
                    {
                        NombreCategoria = grupo.Key,
                        NumeroOrdenes = grupo.Count()
                    })
                    .OrderByDescending(x => x.NumeroOrdenes)
                    .ToList();

                return consulta;
            }
        }
    }
}
