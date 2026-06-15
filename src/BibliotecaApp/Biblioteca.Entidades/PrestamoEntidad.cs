using System;

namespace Biblioteca.Entidades
{
    public class PrestamoEntidad
    {
        public int ID { get; set; }
        public int IdLibro { get; set; }
        public string TituloLibro { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public bool Devuelto { get; set; }
    }
}
