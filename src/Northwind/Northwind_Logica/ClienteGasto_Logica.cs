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
    public static class ClienteGasto_Logica
    {
        //Con datos normal
        //public static List<ClienteGasto> ObtenerTop10Clientes()
        //{
        //    return ClienteGastos_Datos.ObtenerTop10Clientes();
        //}
        public static List<ClienteGasto> ObtenerTop10Clientes()
        {
            return ClienteGastos_Datos.ObtenerTop10Clientes();
        }


    }
}
