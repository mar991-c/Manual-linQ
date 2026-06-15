//using Northwind_Datos;
using Northwind_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos_LinQ;

namespace Northwind_Logica
{
    public static class VentaPais_Logica
    {
        //Datos normales
        //public static List<VentaPais> ObtenerVentasPorPais()
        //{
        //    return VentaPais_Datos.ObtenerVentasPorPais();
        //}
        //Datos LinQ

        public static List<VentaPais> ObtenerVentasPorPais()
        {
            return VentaPais_Datos.ObtenerVentasPorPais();
        }
    }
}
