using GestionUsuariosEntidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuariosDatosEF
{
    public static class PacienteDatos
    {
        public static PacienteEntidades Nuevo(PacienteEntidades paciente)
        {
            try
            {
                Pacientes _pacienteEF = new Pacientes();
                _pacienteEF.id = paciente.ID;
                _pacienteEF.Id_Genero = paciente.Id_Genero;
                _pacienteEF.cedula = paciente.Cedula;
                _pacienteEF.nombres = paciente.Nombre;
                _pacienteEF.apellido = paciente.Apellido;
                _pacienteEF.telefono = paciente.Telefono;
                _pacienteEF.direccion = paciente.Direccion;
                _pacienteEF.fechaNacimiento = paciente.FechaNacimiento;
                _pacienteEF.Afiliado = paciente.Afiliado;
                _pacienteEF.CodigoIESS = paciente.CodigoIESS;

                using (Programacion_avanzadaEntities contexto = new Programacion_avanzadaEntities())
                {
                    contexto.Pacientes.Add(_pacienteEF);
                    contexto.SaveChanges();
                }
                paciente.ID = _pacienteEF.id;
                paciente.error = "";

                return paciente;
            }
            catch (Exception e)
            {
                paciente.error =e.Message;
                ;
                return paciente;

            }

        }

        public static PacienteEntidades Actualizar(PacienteEntidades paciente)
        {
            try
            {
                Pacientes _pacienteEF = new Pacientes();
                _pacienteEF.id = paciente.ID;
                _pacienteEF.Id_Genero = paciente.Id_Genero;
                _pacienteEF.cedula = paciente.Cedula;
                _pacienteEF.nombres = paciente.Nombre;
                _pacienteEF.apellido = paciente.Apellido;
                _pacienteEF.telefono = paciente.Telefono;
                _pacienteEF.direccion = paciente.Direccion;
                _pacienteEF.fechaNacimiento = paciente.FechaNacimiento;
                _pacienteEF.Afiliado = paciente.Afiliado;
                _pacienteEF.CodigoIESS = paciente.CodigoIESS;
                using (Programacion_avanzadaEntities contexto = new Programacion_avanzadaEntities())
                {
                    contexto.Pacientes.AddOrUpdate(_pacienteEF);
                    contexto.SaveChanges();
                }
                paciente.error = "";
                return paciente;

            }
            catch (Exception e)
            {
                paciente.error = e.Message;
                return paciente;
            }
        }

        public static List<PacienteEntidades> DevolverListaPaciente()
        {
            try
            {
                List<PacienteEntidades> listaPacienteEntidades = new List<PacienteEntidades>();
                using (Programacion_avanzadaEntities contexto = new Programacion_avanzadaEntities())
                {
                    //Query personalizado nos ayuda a multiples busquedas 
                    //estudiar el like %_
                    var lista = contexto
                        .Pacientes
                        .Include("Genero")
                        .ToList();
                    foreach (var item in lista)
                    {
                        listaPacienteEntidades.Add(new PacienteEntidades(
                            item.id, item.Id_Genero??0, item.Genero.Nombre,
                            item.nombres, item.apellido, item.cedula, item.direccion,
                            item.telefono, item.fechaNacimiento, item.Afiliado??false, item.CodigoIESS??""
                            ));
                    }
     

                    return listaPacienteEntidades;
                }
            }
            catch (Exception e)
            {
                
                throw;
            }
            



        }

        public static PacienteEntidades CargarPacientePorId(int id)
        {
            PacienteEntidades paciente = new PacienteEntidades();
            try
            {
                
                using (Programacion_avanzadaEntities contexto= new Programacion_avanzadaEntities())
                {
                    var _pacienteEF=contexto.Pacientes
                                 .Include("Genero")
                                 .FirstOrDefault(p=>p.id==id);
                    paciente.ID = _pacienteEF.id;
                    paciente.Id_Genero = _pacienteEF.Id_Genero??0;
                    paciente.Cedula = _pacienteEF.cedula;
                    paciente.Nombre = _pacienteEF.nombres;
                    paciente.Apellido = _pacienteEF.apellido;
                    paciente.Telefono = _pacienteEF.telefono;
                    paciente.Direccion = _pacienteEF.direccion;
                    paciente.FechaNacimiento = _pacienteEF.fechaNacimiento;
                    paciente.Afiliado = _pacienteEF.Afiliado??false;
                    paciente.CodigoIESS = _pacienteEF.CodigoIESS??"";
                    paciente.error = "";
                    return paciente;
                }
            }
            catch (Exception e)
            {
                paciente.error = e.Message;
                throw;
            }
        }

        public static bool EliminarPacientePorID(int id)
        {
            try
            {
                using (Programacion_avanzadaEntities contexto = new Programacion_avanzadaEntities())
                {
                    var _pacienteEF = contexto.Pacientes
                                .FirstOrDefault(p => p.id == id);
                    contexto.Pacientes.Remove(_pacienteEF);
                    contexto.SaveChanges();
                    return true;


                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


    }
}
