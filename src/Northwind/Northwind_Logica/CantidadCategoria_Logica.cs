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
    public static class CantidadCategoria_Logica
    {
        //Con datos normal
        //public static List<CantidadCategoria> ObtenerCantidadPorCategoria()
        //{
        //    return CantidadCategoria_Datos.ObtenerCantidadPorCategoria();
        //}

        //Con datos LinQ
        public static List<CantidadCategoria> ObtenerCantidadPorCategoria()
        {
            return CantidadCategoria_Datos.ObtenerCantidadPorCategoria();
        }
    }
}
