using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using GestionUsuariosDatos;
using GestionUsuariosEntidades;
//using GestionUsuarios_DatosLinq;
using GestionUsuariosDatosEF;
using System.Activities.Statements;
using System.Transactions;
using TransactionScope = System.Transactions.TransactionScope;

namespace GestionUsuariosLogicaNegocio
{
    public static class PacienteLogicaNegocio
    {
        public static PacienteEntidades CargarPacientePorId(int id)
        {
            try
            {
            
            using (TransactionScope scope = new TransactionScope())
            {
                return PacienteDatos.CargarPacientePorId(id);
                scope.Complete();
            } 
        
        }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static List<PacienteEntidades> DevolverListaPaciente()
        {
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {
                    return PacienteDatos.DevolverListaPaciente();
                    scope.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
                
        }

        public static bool EliminarPacientePorID(int id)
        {
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {
                    return PacienteDatos.EliminarPacientePorID(id);
                    scope.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }


        }

        public static PacienteEntidades GuardarPaciente(PacienteEntidades paciente)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (paciente.ID == 0)//cuando ID es 0 es un nuevo paciente
                    {
                        return PacienteDatos.Nuevo(paciente);
                    }
                    else //cuando ID != 0 el paciente ya existe, hay que actualizar el paciente
                    {
                        return PacienteDatos.Actualizar(paciente);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
