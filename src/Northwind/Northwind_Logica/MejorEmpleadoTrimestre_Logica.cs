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
    public static class MejorEmpleadoTrimestre_Logica
    {
        //Con datos normales
        //public static List<MejorEmpleadoTrimestre> ObtenerTop3EmpleadosTrimestre()
        //{
        //    return MejorEmpleadoTrimestre_Datos.ObtenerTop3EmpleadosTrimestre();
        //}

        //LinQ
        public static List<MejorEmpleadoTrimestre> ObtenerTop3EmpleadosTrimestre()
        {
            return MejorEmpleadoTrimestre_Datos.ObtenerTop3EmpleadosTrimestre();
        }
    }
}
