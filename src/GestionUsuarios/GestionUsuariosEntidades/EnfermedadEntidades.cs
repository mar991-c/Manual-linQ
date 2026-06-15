using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuariosEntidades
{
    public class EnfermedadEntidades
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public EnfermedadEntidades()
        {
            
        }

        public EnfermedadEntidades(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
