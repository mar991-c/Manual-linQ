using GestionUsuariosEntidades;
using GestionUsuariosLogicaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionUsuariosPresentacion
{
    public partial class EnfermedadesPacientes : Form
    {
        private int _idPaciente;
        public EnfermedadesPacientes(int id_paciente, string nombre_paciente)
        {
            InitializeComponent();
            _idPaciente = id_paciente;
            label_Paciente.Text = "Paciente: " + nombre_paciente;
        }
        
        private void Enfermedades_Load(object sender, EventArgs e)
        {
            CargarDataEnfermedades();
        }

        private void CargarDataEnfermedades()
        {
            dataGridView1.DataSource = PacienteEnfermedadNegocio.DevolverListaEnfermedadesPorPaciente(_idPaciente);
        }

        private void button_agregar_Click(object sender, EventArgs e)
        {
            Enfermedades frm = new Enfermedades();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PacienteEnfermedadEntidad pe = new PacienteEnfermedadEntidad();
                pe.id_Paciente = _idPaciente;
                pe.id_Enfermedad = frm.EnfermedadSeleccionada.Id;
                pe.fechaEnfermedad = frm.FechaDiagnostico;
                pe.observacion = frm.Observacion;

                PacienteEnfermedadNegocio.InsertarEnfermedad(pe);

                CargarDataEnfermedades();
            }
        }
    }
}
