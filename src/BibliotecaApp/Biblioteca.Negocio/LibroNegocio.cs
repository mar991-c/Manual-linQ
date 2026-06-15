using System.Collections.Generic;
using System.Linq;
using Biblioteca.Entidades;

namespace Biblioteca.Negocio
{
    public static class LibroNegocio
    {
        // LINQ to Objects: Filtrar por genero en memoria
        public static List<LibroEntidad> FiltrarPorGenero(string genero)
        {
            List<LibroEntidad> todosLosLibros = LibroDatos.ObtenerLibrosDisponibles();

            // En la Capa de Negocio, los datos ya estan en memoria (List<T>)
            // Por eso aqui se usa LINQ to Objects, no LINQ to Entities
            return todosLosLibros
                .Where(l => l.Genero.ToLower() == genero.ToLower())
                .ToList();
        }

        // LINQ to Objects: Contar libros por genero (agregado en memoria)
        public static Dictionary<string, int> ContarPorGenero()
        {
            List<LibroEntidad> todosLosLibros = LibroDatos.ObtenerLibrosDisponibles();

            return todosLosLibros
                .GroupBy(l => l.Genero)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        // Validacion de negocio antes de insertar
        public static string InsertarConValidacion(LibroEntidad libro)
        {
            if (string.IsNullOrWhiteSpace(libro.Titulo))
                return "Error: El titulo no puede estar vacio.";

            if (libro.AnioPub < 1450 || libro.AnioPub > 2026)
                return "Error: El anio de publicacion no es valido.";

            LibroDatos.InsertarLibro(libro);
            return "Libro registrado correctamente.";
        }
    }
}
