using GestionUsuariosEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionUsuariosLogicaNegocio;

namespace GestionUsuariosPresentacion
{
    public partial class Enfermedades : Form
    {
        public EnfermedadEntidades EnfermedadSeleccionada { get; private set; }
        public DateTime FechaDiagnostico { get; set; }
        public string Observacion { get; set; }
        public Enfermedades()
        {
            InitializeComponent();
        }

        private void Enfermedades_Load(object sender, EventArgs e)
        {
            CargarEnfermedades();
        }

        private void CargarEnfermedades()
        {
            dataGridView1.DataSource = Enfermedad_Logica.ObtenerEnfermedades();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void Enfermedades_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex < 0) return;

            EnfermedadSeleccionada = new EnfermedadEntidades(
                Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value),
                dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString()
            );
            FechaDiagnostico = dateTimePicker1.Value;
            Observacion = textBox2.Text.Trim();

            this.DialogResult = DialogResult.OK;
            
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
