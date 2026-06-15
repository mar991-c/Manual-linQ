namespace GestionUsuariosPresentacion
{
    partial class EnfermedadesPacientes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label_Paciente = new System.Windows.Forms.Label();
            this.button_agregar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(24, 123);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(735, 443);
            this.dataGridView1.TabIndex = 0;
            // 
            // label_Paciente
            // 
            this.label_Paciente.AutoSize = true;
            this.label_Paciente.Location = new System.Drawing.Point(34, 23);
            this.label_Paciente.Name = "label_Paciente";
            this.label_Paciente.Size = new System.Drawing.Size(66, 16);
            this.label_Paciente.TabIndex = 1;
            this.label_Paciente.Text = "Paciente: ";
            // 
            // button_agregar
            // 
            this.button_agregar.Location = new System.Drawing.Point(37, 69);
            this.button_agregar.Name = "button_agregar";
            this.button_agregar.Size = new System.Drawing.Size(162, 23);
            this.button_agregar.TabIndex = 2;
            this.button_agregar.Text = "Agregar Enfermedad";
            this.button_agregar.UseVisualStyleBackColor = true;
            this.button_agregar.Click += new System.EventHandler(this.button_agregar_Click);
            // 
            // EnfermedadesPacientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 606);
            this.Controls.Add(this.button_agregar);
            this.Controls.Add(this.label_Paciente);
            this.Controls.Add(this.dataGridView1);
            this.Name = "EnfermedadesPacientes";
            this.Text = "Enfermedades";
            this.Load += new System.EventHandler(this.Enfermedades_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label_Paciente;
        private System.Windows.Forms.Button button_agregar;
    }
}