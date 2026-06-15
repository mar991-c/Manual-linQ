using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuariosEntidades
{
    public class GeneroEntidades
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public GeneroEntidades()
        {
            
        }

        public GeneroEntidades(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
