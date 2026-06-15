namespace Biblioteca.Entidades
{
    public class LibroEntidad
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public int AnioPub { get; set; }
        public bool Disponible { get; set; }
    }
}
