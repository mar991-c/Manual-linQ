using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_Entidades
{
    public class ClienteGasto
    {
        public string NombreCliente { get; set; }
        public string Pais { get; set; }
        public decimal TotalGastado { get; set; }
        public int NumeroOrdenes { get; set; }
    }
}
