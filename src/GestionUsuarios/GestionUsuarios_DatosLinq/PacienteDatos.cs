using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionUsuariosEntidades;

namespace GestionUsuarios_DatosLinq
{
    public static class PacienteDatos
    {
        public static PacienteEntidades Nuevo(PacienteEntidades paciente)
        {
			try
			{

				Pacientes _pacienteLinQ= new Pacientes();
                _pacienteLinQ.id=paciente.ID;
                _pacienteLinQ.Id_Genero = paciente.Id_Genero;
                _pacienteLinQ.cedula = paciente.Cedula;
                _pacienteLinQ.nombres = paciente.Nombre;
                _pacienteLinQ.apellido = paciente.Apellido;
                _pacienteLinQ.telefono = paciente.Telefono;
                _pacienteLinQ.direccion = paciente.Direccion;
                _pacienteLinQ.fechaNacimiento = paciente.FechaNacimiento;
                _pacienteLinQ.Afiliado = paciente.Afiliado;
                _pacienteLinQ.CodigoIESS = paciente.CodigoIESS;
                using (Programacion_avanzadaDataContext contexto= new Programacion_avanzadaDataContext())
                {
                    contexto.Pacientes.InsertOnSubmit(_pacienteLinQ);
                    contexto.SubmitChanges(); //confirmar el dato 
                }
                _pacienteLinQ.id = paciente.ID  ;
                return paciente;


            }
			catch (Exception)
			{

				throw;
			}
        }

        public static PacienteEntidades Actualizar(PacienteEntidades paciente)
        {
            try
            {
                using (Programacion_avanzadaDataContext contexto = new Programacion_avanzadaDataContext())
                {
                    //Identificar el elemento que debemos actualizar

                    Pacientes _pacienteLinQ= contexto.Pacientes.FirstOrDefault(p=>p.id==paciente.ID);
                    _pacienteLinQ.id = paciente.ID;
                    _pacienteLinQ.Id_Genero = paciente.Id_Genero;
                    _pacienteLinQ.cedula = paciente.Cedula;
                    _pacienteLinQ.nombres = paciente.Nombre;
                    _pacienteLinQ.apellido = paciente.Apellido;
                    _pacienteLinQ.telefono = paciente.Telefono;
                    _pacienteLinQ.direccion = paciente.Direccion;
                    _pacienteLinQ.fechaNacimiento = paciente.FechaNacimiento;
                    _pacienteLinQ.Afiliado = paciente.Afiliado;
                    _pacienteLinQ.CodigoIESS = paciente.CodigoIESS;
                    contexto.SubmitChanges();
                    return paciente;

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
                List<PacienteEntidades> listaPacienteEntidades = new List<PacienteEntidades>();
                List<Pacientes> listaPaciente= new List<Pacientes>();

                using (Programacion_avanzadaDataContext contexto = new Programacion_avanzadaDataContext())
                {
                    var resultado = from p in contexto.Pacientes
                                    select p;
                    listaPaciente= resultado.ToList();
                }
                foreach (var item in listaPaciente)
                {
                    listaPacienteEntidades.Add(new PacienteEntidades(item.id,
                        item.Id_Genero??0,
                        GeneroDatos.DevolverNombreGeneroPorId(item.Id_Genero??0),
                        item.nombres,item.apellido,
                        item.cedula,item.direccion,
                        item.telefono,item.fechaNacimiento,
                        item.Afiliado?? false,item.CodigoIESS));
                }
                return listaPacienteEntidades;

            }
            catch (Exception ex)
            {
                var error= ex.Message;

                throw;
            }
        }
        public static PacienteEntidades CargarPacientePorId(int id)
        {
            try
            {
                PacienteEntidades paciente= new PacienteEntidades();
                using (Programacion_avanzadaDataContext contexto = new Programacion_avanzadaDataContext())
                {
                    Pacientes _pacienteLinQ= contexto.Pacientes.FirstOrDefault(p=>p.id==id);
                    paciente.ID=_pacienteLinQ.id;
                    paciente.Id_Genero=_pacienteLinQ.Id_Genero??0;
                    paciente.Cedula = _pacienteLinQ.cedula  ;
                    paciente.Nombre = _pacienteLinQ.nombres;
                    paciente.Apellido = _pacienteLinQ.apellido;
                    paciente.Telefono = _pacienteLinQ.telefono  ;
                    paciente.Direccion = _pacienteLinQ.direccion;
                    paciente.FechaNacimiento = _pacienteLinQ.fechaNacimiento;
                    paciente.Afiliado = _pacienteLinQ.Afiliado??false;
                    paciente.CodigoIESS = _pacienteLinQ.CodigoIESS;
                    contexto.SubmitChanges();
                    return paciente;
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
                using (Programacion_avanzadaDataContext contexto= new Programacion_avanzadaDataContext())
                {
                    Pacientes pacienteLinQ = contexto.Pacientes.FirstOrDefault(p => p.id == id);
                    contexto.Pacientes.DeleteOnSubmit(pacienteLinQ);
                    contexto.SubmitChanges();
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
