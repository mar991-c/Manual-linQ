using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionUsuariosEntidades;

namespace GestionUsuariosDatosEF
{
    public static class PacienteEnfermedadesDatos
    {
        public static PacienteEnfermedadEntidad InsertarEnfermedad(PacienteEnfermedadEntidad pacienteEnfermedad)
        {
            try
            {
                Paciente_Enfermedad _pacienteEF = new Paciente_Enfermedad();
                _pacienteEF.id = pacienteEnfermedad.id;
                _pacienteEF.id_Paciente = pacienteEnfermedad.id_Paciente;
                _pacienteEF.id_Enfermedad = pacienteEnfermedad.id_Enfermedad;
                _pacienteEF.fechaEnfermedad = pacienteEnfermedad.fechaEnfermedad;
                _pacienteEF.observacion=pacienteEnfermedad.observacion;


                using (Programacion_avanzadaEntities contexto = new Programacion_avanzadaEntities())
                {
                    contexto.Paciente_Enfermedad.Add(_pacienteEF);
                    contexto.SaveChanges();
                }
                pacienteEnfermedad.id = _pacienteEF.id;

                return pacienteEnfermedad;
            }
            
            catch (Exception)
            {

                throw;
            }

        }
        public static List<PacienteEnfermedadEntidad> DevolverListaEnfermedadesPorPaciente(int id_paciente)
        {
			try
			{
				List<PacienteEnfermedadEntidad> listaPacienteEnfermedad= new List<PacienteEnfermedadEntidad> ();
                using (Programacion_avanzadaEntities contexto= new Programacion_avanzadaEntities())
                {
                    var lista = contexto
                                .Paciente_Enfermedad
                                .Include("Enfermedad")
                                .Where(p => p.id_Paciente == id_paciente)
                                .ToList();
                    foreach (var item in lista)
                    {
                        listaPacienteEnfermedad.Add(new PacienteEnfermedadEntidad(
                            item.id, (int)item.id_Paciente,(int)item.id_Enfermedad,(DateTime)item.fechaEnfermedad,item.observacion
                            ));
                    }
                    return listaPacienteEnfermedad;
                }


            }
			catch (Exception)
			{

				throw;
			}
        }

        public static bool Eliminar(int id)
        {
            try
            {
                using (Programacion_avanzadaEntities contexto= new Programacion_avanzadaEntities())
                {
                    var _pacienteEF= contexto.Paciente_Enfermedad.FirstOrDefault(p=>p.id==id);
                    if (_pacienteEF == null) return false;
                    contexto.Paciente_Enfermedad.Remove(_pacienteEF);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }


    }
}
