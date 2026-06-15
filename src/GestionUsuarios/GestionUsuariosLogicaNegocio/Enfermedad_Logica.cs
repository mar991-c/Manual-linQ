using GestionUsuariosEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionUsuariosDatosEF;

namespace GestionUsuariosLogicaNegocio
{
    public static class Enfermedad_Logica
    {
        public static EnfermedadEntidades InsertarEnfermedad(EnfermedadEntidades enfermedad)
        {
            return EnfermedadesDatos.InsertarEnfermedad(enfermedad);
        }

        public static List<EnfermedadEntidades> ObtenerEnfermedades()
        {
            return EnfermedadesDatos.ObtenerEnfermedades();
        }


    }
}
