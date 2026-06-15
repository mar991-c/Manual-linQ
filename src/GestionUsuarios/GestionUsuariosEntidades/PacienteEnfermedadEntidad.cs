using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuariosEntidades
{
    public class PacienteEnfermedadEntidad
    {
        public int id { get; set; }
        public int id_Paciente { get; set; }

        public int id_Enfermedad { get; set; }

        //public EnfermedadEntidades Enfermedad { get; set; }
        public DateTime fechaEnfermedad { get; set; }
        public string observacion { get; set; }

        public PacienteEnfermedadEntidad()
        {
            
        }

        public PacienteEnfermedadEntidad(int id, int id_Paciente, int id_Enfermedad, DateTime fechaEnfermedad, string observacion)
        {
            this.id = id;
            this.id_Paciente = id_Paciente;
            this.id_Enfermedad = id_Enfermedad;
            this.fechaEnfermedad = fechaEnfermedad;
            this.observacion = observacion;
        }
    }
}
