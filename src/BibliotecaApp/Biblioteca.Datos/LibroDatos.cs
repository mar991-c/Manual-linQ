using System.Collections.Generic;
using System.Linq;
using Biblioteca.Entidades;

namespace Biblioteca.Datos
{
    public static class LibroDatos
    {
        // READ: Obtener todos los libros disponibles ordenados por titulo
        public static List<LibroEntidad> ObtenerLibrosDisponibles()
        {
            using (var contexto = new BibliotecaContext())
            {
                return contexto.Libros
                    .Where(l => l.Disponible == true)
                    .OrderBy(l => l.Titulo)
                    .Select(l => new LibroEntidad
                    {
                        ID = l.ID,
                        Titulo = l.Titulo,
                        Autor = l.Autor,
                        Genero = l.Genero,
                        AnioPub = l.AnioPub,
                        Disponible = l.Disponible
                    })
                    .ToList();
            }
        }

        // READ: Buscar libro por ID con FirstOrDefault
        public static LibroEntidad BuscarPorId(int id)
        {
            using (var contexto = new BibliotecaContext())
            {
                var libro = contexto.Libros
                    .FirstOrDefault(l => l.ID == id);

                if (libro == null) return null;

                return new LibroEntidad
                {
                    ID = libro.ID,
                    Titulo = libro.Titulo,
                    Autor = libro.Autor,
                    Genero = libro.Genero,
                    AnioPub = libro.AnioPub,
                    Disponible = libro.Disponible
                };
            }
        }

        // CREATE: Insertar nuevo libro
        public static void InsertarLibro(LibroEntidad nuevoLibro)
        {
            using (var contexto = new BibliotecaContext())
            {
                var libroDb = new Libro
                {
                    Titulo = nuevoLibro.Titulo,
                    Autor = nuevoLibro.Autor,
                    Genero = nuevoLibro.Genero,
                    AnioPub = nuevoLibro.AnioPub,
                    Disponible = true
                };
                contexto.Libros.Add(libroDb);
                contexto.SaveChanges();
            }
        }

        // DELETE: Eliminar libro por ID
        public static bool EliminarLibro(int id)
        {
            using (var contexto = new BibliotecaContext())
            {
                var libro = contexto.Libros.FirstOrDefault(l => l.ID == id);
                if (libro == null) return false;

                contexto.Libros.Remove(libro);
                contexto.SaveChanges();
                return true;
            }
        }
    }
}
