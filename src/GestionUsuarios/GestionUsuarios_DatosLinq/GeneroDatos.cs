using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionUsuariosEntidades;

namespace GestionUsuarios_DatosLinq
{
    public static class GeneroDatos
    {
        public static List<GeneroEntidades> DevolverListaGeneros()
        {
			try
			{
                List<GeneroEntidades> listageneroEntidades = new List<GeneroEntidades>();
                List<Genero> listaGenero= new List<Genero>();

                using (Programacion_avanzadaDataContext contexto= new Programacion_avanzadaDataContext())
                {
                    var resultado = from g in contexto.Genero
                                    select g;
                    listaGenero= resultado.ToList(); //convierte a una lista 

                }
                foreach (var item in listaGenero)
                {
                    listageneroEntidades.Add(new GeneroEntidades(item.Id, item.Nombre));
                }

                return listageneroEntidades;

            }
			catch (Exception)
			{

				throw;
			}
        }

        public static string DevolverNombreGeneroPorId(int id)
        {
            try
            {
                using (Programacion_avanzadaDataContext contexto= new Programacion_avanzadaDataContext())
                {
                    var resultado= contexto.Genero.FirstOrDefault(g => g.Id == id);
                    return resultado.Nombre;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
