using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionUsuariosEntidades;
using GestionUsuariosLogicaNegocio;
namespace GestionUsuariosPresentacion
{
    public partial class Paciente : Form
    {
        //crear objeto paciente
        PacienteEntidades paciente=new PacienteEntidades();
        public Paciente()
        {
            InitializeComponent();
        }

        private void Paciente_Load(object sender, EventArgs e)
        {
            CargarListadoPacienteEnDataGridView();
            CargarComboGenero();
            InicializarValores();

        }

        private void InicializarValores()
        {
            paciente.ListaEnfermedades = new List<PacienteEnfermedadEntidad>();
        }

        private void CargarComboGenero()
        {
            comboBox_Genero.DataSource=GeneroNegocio.DevolverListaGenero();
            comboBox_Genero.DisplayMember = "Nombre";
            comboBox_Genero.ValueMember= "Id";
        }

        private void CargarListadoPacienteEnDataGridView()
        {
            dataGridView_Pacientes.DataSource = PacienteLogicaNegocio.DevolverListaPaciente();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GuardarPaciente();
        }

        private void GuardarPaciente()
        {
            
                //TODO: Validar posteriormente
                //paciente.ID = Convert.ToInt32(textBoxID.Text);
                paciente.Nombre = textBoxNombre.Text.ToUpper();
                paciente.Apellido = textBoxApellido.Text.ToUpper();
                paciente.Cedula = textBoxCedula.Text;
                paciente.Direccion = textBoxDireccion.Text;
                paciente.Telefono = textBoxTelefono.Text;
                paciente.FechaNacimiento = dateTimePickerFechaNacimiento.Value;
                paciente.Afiliado = checkBox_Afiliado.Checked;
                paciente.CodigoIESS = textBox_CodigoIESS.Text;
                paciente.Id_Genero = (int)comboBox_Genero.SelectedValue;

                paciente = PacienteLogicaNegocio.GuardarPaciente(paciente);
            if (paciente.error!="")
            {
                MessageBox.Show(paciente.error, "error",MessageBoxButtons.OK);
                return;
            }

            if (paciente != null)
                {
                    textBoxID.Text = paciente.ID.ToString();
                    MessageBox.Show("Datos almacenados correctamente");
                    CargarListadoPacienteEnDataGridView();
                }
            
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(dataGridView_Pacientes.Rows[e.RowIndex].Cells["id"].Value.ToString());
                CargarValoresPacientePorId(id);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error en seleccion de elemento" + ex.Message);
            }
        }

        private void CargarValoresPacientePorId(int id)
        {
            paciente = PacienteLogicaNegocio.CargarPacientePorId(id);
            textBoxID.Text = paciente.ID.ToString();
            comboBox_Genero.SelectedValue= paciente.Id_Genero;
            textBoxNombre.Text = paciente.Nombre;
            textBoxApellido.Text = paciente.Apellido;
            textBoxCedula.Text = paciente.Cedula;
            textBoxTelefono.Text = paciente.Telefono;
            dateTimePickerFechaNacimiento.Value = paciente.FechaNacimiento;
            textBoxDireccion.Text = paciente.Direccion;
            textBox_CodigoIESS.Text= paciente.CodigoIESS;
            checkBox_Afiliado.Checked= paciente.Afiliado;
        }

        private void button_Nuevo_Click(object sender, EventArgs e)
        {
            EncerarCamposParaNuevoRegistro();
        }

        private void EncerarCamposParaNuevoRegistro()
        {
            paciente = new PacienteEntidades();
            textBoxID.Text = paciente.ID.ToString();
            textBoxNombre.Text = paciente.Nombre;
            textBoxApellido.Text = paciente.Apellido;
            textBoxCedula.Text = paciente.Cedula;
            textBoxTelefono.Text = paciente.Telefono;
            dateTimePickerFechaNacimiento.Value = DateTime.Now;
            textBoxDireccion.Text = paciente.Direccion;
        }

        private void button_Eliminar_Click(object sender, EventArgs e)
        {
            EliminarPaciente();
        }

        private void EliminarPaciente()
        {
            if (MessageBox.Show("Está Usted seguro de eliminar permanentemente este registro?",
               "Eliminar Registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //Llamar a Negocio
                if (PacienteLogicaNegocio.EliminarPacientePorID(paciente.ID))
                {
                    MessageBox.Show("El registro fue eliminado",
                     "Registro Eliminado", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    //Actualizar el DataGridView de Pacientes
                    CargarListadoPacienteEnDataGridView();
                }
            }
        }

        private void comboBox_Genero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView_Pacientes.SelectedRows.Count==0)
            {
                MessageBox.Show("Seleccione un paciente ", "error de seleccion", MessageBoxButtons.OK);
                return;
            }
            int id = Convert.ToInt32(dataGridView_Pacientes.SelectedRows[0].Cells["id"].Value);
            string nombre = dataGridView_Pacientes.SelectedRows[0].Cells["nombre"].Value.ToString();
            string apellido = dataGridView_Pacientes.SelectedRows[0].Cells["apellido"].Value.ToString();

            EnfermedadesPacientes pacienteEnfermedades = new EnfermedadesPacientes(id, nombre + " " + apellido);
            pacienteEnfermedades.ShowDialog();


        }
    }
}
