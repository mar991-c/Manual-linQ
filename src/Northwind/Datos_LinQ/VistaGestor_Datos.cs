using Northwind_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos_LinQ
{
    public static class VistaGestor_Datos
    {
        public static List<VistaGestor> ObtenerTodos()
        {
            using (DataClasses1DataContext contexto = new DataClasses1DataContext())
            {

                var lista = contexto.vw_GestionOrdenesPorEmpleados.Select(v => new VistaGestor
                {
                    AñoOrden = Convert.ToInt32(v.añoOrden),
                    MesOrden = Convert.ToInt32(v.MesOrden),
                    NombreEmpleado = v.NombreEmpleado,
                    CompanyName = v.CompanyName,
                    Country = v.Country,
                    CategoryName = v.CategoryName,
                    MontoReacudado = Convert.ToDecimal(v.montoReacudado)
                }).ToList();

                return lista;
            }
        }
    }
}
