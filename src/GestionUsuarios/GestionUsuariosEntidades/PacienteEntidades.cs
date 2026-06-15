using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuariosEntidades
{
    public class PacienteEntidades
    {
       

        public int ID { get; set; }
        public int Id_Genero { get; set; }
        public string  Genero { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Afiliado { get; set; }
        public string CodigoIESS { get; set; }
        public List<PacienteEnfermedadEntidad> ListaEnfermedades { get; set; }

        public string error { get; set; }

        
        public PacienteEntidades(int iD, int id_Genero, string genero, string nombre, string apellido, string cedula, string direccion, string telefono, DateTime fechaNacimiento, bool afiliado, string codigoIESS)
        {
            ID = iD;
            Id_Genero = id_Genero;
            Genero = genero;
            Nombre = nombre;
            Apellido = apellido;
            Cedula = cedula;
            Direccion = direccion;
            Telefono = telefono;
            FechaNacimiento = fechaNacimiento;
            Afiliado = afiliado;
            CodigoIESS = codigoIESS;
        }
        public PacienteEntidades()
        {
            
        }


    }
}
