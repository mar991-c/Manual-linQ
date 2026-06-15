//using GestionUsuariosDatos;
using GestionUsuariosEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using GestionUsuarios_DatosLinq;
using GestionUsuariosDatosEF;
namespace GestionUsuariosLogicaNegocio
{
    public static class GeneroNegocio
    {
        public static List<GeneroEntidades> DevolverListaGenero()
        {
            return GeneroDatos.DevolverListaGeneros();
        }

    }
}
