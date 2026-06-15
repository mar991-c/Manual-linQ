using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionUsuariosEntidades;

namespace GestionUsuariosDatosEF
{
    public static class GeneroDatos
    {
        public static List<GeneroEntidades> DevolverListaGeneros()
        {
			try
			{
                List<GeneroEntidades> listagenero = new List<GeneroEntidades>();
                using (Programacion_avanzadaEntities contexto = new Programacion_avanzadaEntities())
                {
                    
                    var lista = contexto
                        .Genero
                        .ToList();
                    foreach (var item in lista)
                    {
                        listagenero.Add(new GeneroEntidades(
                            item.Id, item.Nombre
                            ));
                    }
                    return listagenero;
                }
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
