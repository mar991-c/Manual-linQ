using GestionUsuariosEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuariosDatosEF
{
    public static class EnfermedadesDatos
    {
        public static EnfermedadEntidades InsertarEnfermedad(EnfermedadEntidades enfermedad)
        {
            try
            {
                Enfermedad _pacienteEF = new Enfermedad();
                _pacienteEF.id = enfermedad.Id;
                _pacienteEF.nombre = enfermedad.Nombre;
                

                using (Programacion_avanzadaEntities contexto = new Programacion_avanzadaEntities())
                {
                    contexto.Enfermedad.Add(_pacienteEF);
                    contexto.SaveChanges();
                }
                enfermedad.Id = _pacienteEF.id;

                return enfermedad;
            }

            catch (Exception)
            {

                throw;
            }

        }
        public static List<EnfermedadEntidades> ObtenerEnfermedades()
        {
            try
            {
                List<EnfermedadEntidades> listaEnfermedades = new List<EnfermedadEntidades>();
                using (Programacion_avanzadaEntities contexto = new Programacion_avanzadaEntities())
                {
                    var lista = contexto
                                .Enfermedad
                                .ToList();
                    foreach (var item in lista)
                    {
                        listaEnfermedades.Add(new EnfermedadEntidades(
                            item.id, item.nombre
                            ));
                    }
                    return listaEnfermedades;
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
