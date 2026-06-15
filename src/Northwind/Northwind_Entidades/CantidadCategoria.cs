using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_Entidades
{
    public class CantidadCategoria
    {


        public string NombreCategoria { get; set; }
        public int NumeroOrdenes { get; set; }
        
        public CantidadCategoria()
        {
            
        }

        public CantidadCategoria(string nombreCategoria, int numeroOrdenes)
        {
            NombreCategoria = nombreCategoria;
            NumeroOrdenes = numeroOrdenes;
        }
    }
}
