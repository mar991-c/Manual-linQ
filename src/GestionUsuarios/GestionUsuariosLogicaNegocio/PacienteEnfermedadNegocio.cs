using GestionUsuariosEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionUsuariosDatosEF;

namespace GestionUsuariosLogicaNegocio
{
    public static class PacienteEnfermedadNegocio
    {
        public static PacienteEnfermedadEntidad InsertarEnfermedad(PacienteEnfermedadEntidad pacienteEnfermedad)
        {
            return PacienteEnfermedadesDatos.InsertarEnfermedad(pacienteEnfermedad);
        }
        public static List<PacienteEnfermedadEntidad> DevolverListaEnfermedadesPorPaciente(int id_paciente)
        {
            return PacienteEnfermedadesDatos.DevolverListaEnfermedadesPorPaciente(id_paciente);
        }
        public static bool Eliminar(int id)
        {
            return PacienteEnfermedadesDatos.Eliminar(id);
        }
    }
}
