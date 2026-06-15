namespace Presentacion_Northwind
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tapMontoEmpleados = new System.Windows.Forms.TabPage();
            this.dataGridView_Monto_Empleados = new System.Windows.Forms.DataGridView();
            this.chart_Monto_Empleado = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.TabCategoria = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView_Categorias = new System.Windows.Forms.DataGridView();
            this.chart_Categorias = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabEmpleados = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.chart_Empleados = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridView_Empleados = new System.Windows.Forms.DataGridView();
            this.tabClientes = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.chart_Clientes = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridView_Clientes = new System.Windows.Forms.DataGridView();
            this.tabPais = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.chart_Pais = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridView_Pais = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tapMontoEmpleados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Monto_Empleados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Monto_Empleado)).BeginInit();
            this.TabCategoria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Categorias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Categorias)).BeginInit();
            this.tabEmpleados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Empleados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Empleados)).BeginInit();
            this.tabClientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Clientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Clientes)).BeginInit();
            this.tabPais.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Pais)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Pais)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tapMontoEmpleados);
            this.tabControl1.Controls.Add(this.TabCategoria);
            this.tabControl1.Controls.Add(this.tabEmpleados);
            this.tabControl1.Controls.Add(this.tabClientes);
            this.tabControl1.Controls.Add(this.tabPais);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1094, 715);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tapMontoEmpleados
            // 
            this.tapMontoEmpleados.Controls.Add(this.dataGridView_Monto_Empleados);
            this.tapMontoEmpleados.Controls.Add(this.chart_Monto_Empleado);
            this.tapMontoEmpleados.Controls.Add(this.label1);
            this.tapMontoEmpleados.Location = new System.Drawing.Point(4, 25);
            this.tapMontoEmpleados.Name = "tapMontoEmpleados";
            this.tapMontoEmpleados.Padding = new System.Windows.Forms.Padding(3);
            this.tapMontoEmpleados.Size = new System.Drawing.Size(1086, 686);
            this.tapMontoEmpleados.TabIndex = 0;
            this.tapMontoEmpleados.Text = "Monto Empleados";
            this.tapMontoEmpleados.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Monto_Empleados
            // 
            this.dataGridView_Monto_Empleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Monto_Empleados.Location = new System.Drawing.Point(523, 113);
            this.dataGridView_Monto_Empleados.Name = "dataGridView_Monto_Empleados";
            this.dataGridView_Monto_Empleados.RowHeadersWidth = 51;
            this.dataGridView_Monto_Empleados.RowTemplate.Height = 24;
            this.dataGridView_Monto_Empleados.Size = new System.Drawing.Size(538, 486);
            this.dataGridView_Monto_Empleados.TabIndex = 2;
            // 
            // chart_Monto_Empleado
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_Monto_Empleado.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_Monto_Empleado.Legends.Add(legend1);
            this.chart_Monto_Empleado.Location = new System.Drawing.Point(35, 95);
            this.chart_Monto_Empleado.Name = "chart_Monto_Empleado";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_Monto_Empleado.Series.Add(series1);
            this.chart_Monto_Empleado.Size = new System.Drawing.Size(468, 483);
            this.chart_Monto_Empleado.TabIndex = 1;
            this.chart_Monto_Empleado.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(312, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(467, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Monto total recaudado por cada empleado";
            // 
            // TabCategoria
            // 
            this.TabCategoria.Controls.Add(this.label2);
            this.TabCategoria.Controls.Add(this.dataGridView_Categorias);
            this.TabCategoria.Controls.Add(this.chart_Categorias);
            this.TabCategoria.Location = new System.Drawing.Point(4, 25);
            this.TabCategoria.Name = "TabCategoria";
            this.TabCategoria.Padding = new System.Windows.Forms.Padding(3);
            this.TabCategoria.Size = new System.Drawing.Size(1086, 686);
            this.TabCategoria.TabIndex = 1;
            this.TabCategoria.Text = "Categoria";
            this.TabCategoria.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(247, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(624, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Cantidad de órdenes vendidas por categoría de producto";
            // 
            // dataGridView_Categorias
            // 
            this.dataGridView_Categorias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Categorias.Location = new System.Drawing.Point(525, 122);
            this.dataGridView_Categorias.Name = "dataGridView_Categorias";
            this.dataGridView_Categorias.RowHeadersWidth = 51;
            this.dataGridView_Categorias.RowTemplate.Height = 24;
            this.dataGridView_Categorias.Size = new System.Drawing.Size(538, 486);
            this.dataGridView_Categorias.TabIndex = 3;
            // 
            // chart_Categorias
            // 
            chartArea2.Name = "ChartArea1";
            this.chart_Categorias.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart_Categorias.Legends.Add(legend2);
            this.chart_Categorias.Location = new System.Drawing.Point(23, 122);
            this.chart_Categorias.Name = "chart_Categorias";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart_Categorias.Series.Add(series2);
            this.chart_Categorias.Size = new System.Drawing.Size(468, 483);
            this.chart_Categorias.TabIndex = 2;
            this.chart_Categorias.Text = "chart2";
            // 
            // tabEmpleados
            // 
            this.tabEmpleados.Controls.Add(this.label3);
            this.tabEmpleados.Controls.Add(this.chart_Empleados);
            this.tabEmpleados.Controls.Add(this.dataGridView_Empleados);
            this.tabEmpleados.Location = new System.Drawing.Point(4, 25);
            this.tabEmpleados.Name = "tabEmpleados";
            this.tabEmpleados.Padding = new System.Windows.Forms.Padding(3);
            this.tabEmpleados.Size = new System.Drawing.Size(1086, 686);
            this.tabEmpleados.TabIndex = 2;
            this.tabEmpleados.Text = "Mejores Empleados";
            this.tabEmpleados.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(54, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(993, 29);
            this.label3.TabIndex = 5;
            this.label3.Text = "Top 3 empleados del último trimestre en 1997 y 1998 excluyendo Condiments y Confe" +
    "ctions";
            // 
            // chart_Empleados
            // 
            chartArea3.Name = "ChartArea1";
            this.chart_Empleados.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart_Empleados.Legends.Add(legend3);
            this.chart_Empleados.Location = new System.Drawing.Point(24, 110);
            this.chart_Empleados.Name = "chart_Empleados";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart_Empleados.Series.Add(series3);
            this.chart_Empleados.Size = new System.Drawing.Size(468, 483);
            this.chart_Empleados.TabIndex = 4;
            this.chart_Empleados.Text = "chart3";
            // 
            // dataGridView_Empleados
            // 
            this.dataGridView_Empleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Empleados.Location = new System.Drawing.Point(528, 110);
            this.dataGridView_Empleados.Name = "dataGridView_Empleados";
            this.dataGridView_Empleados.RowHeadersWidth = 51;
            this.dataGridView_Empleados.RowTemplate.Height = 24;
            this.dataGridView_Empleados.Size = new System.Drawing.Size(538, 486);
            this.dataGridView_Empleados.TabIndex = 3;
            // 
            // tabClientes
            // 
            this.tabClientes.Controls.Add(this.label4);
            this.tabClientes.Controls.Add(this.chart_Clientes);
            this.tabClientes.Controls.Add(this.dataGridView_Clientes);
            this.tabClientes.Location = new System.Drawing.Point(4, 25);
            this.tabClientes.Name = "tabClientes";
            this.tabClientes.Padding = new System.Windows.Forms.Padding(3);
            this.tabClientes.Size = new System.Drawing.Size(1086, 686);
            this.tabClientes.TabIndex = 3;
            this.tabClientes.Text = "Clientes";
            this.tabClientes.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(264, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(539, 29);
            this.label4.TabIndex = 5;
            this.label4.Text = "Top 10 clientes con mayor gasto total en órdenes";
            // 
            // chart_Clientes
            // 
            chartArea4.Name = "ChartArea1";
            this.chart_Clientes.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart_Clientes.Legends.Add(legend4);
            this.chart_Clientes.Location = new System.Drawing.Point(17, 105);
            this.chart_Clientes.Name = "chart_Clientes";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chart_Clientes.Series.Add(series4);
            this.chart_Clientes.Size = new System.Drawing.Size(483, 498);
            this.chart_Clientes.TabIndex = 4;
            this.chart_Clientes.Text = "chart4";
            // 
            // dataGridView_Clientes
            // 
            this.dataGridView_Clientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Clientes.Location = new System.Drawing.Point(521, 105);
            this.dataGridView_Clientes.Name = "dataGridView_Clientes";
            this.dataGridView_Clientes.RowHeadersWidth = 51;
            this.dataGridView_Clientes.RowTemplate.Height = 24;
            this.dataGridView_Clientes.Size = new System.Drawing.Size(543, 501);
            this.dataGridView_Clientes.TabIndex = 3;
            // 
            // tabPais
            // 
            this.tabPais.Controls.Add(this.label5);
            this.tabPais.Controls.Add(this.chart_Pais);
            this.tabPais.Controls.Add(this.dataGridView_Pais);
            this.tabPais.Location = new System.Drawing.Point(4, 25);
            this.tabPais.Name = "tabPais";
            this.tabPais.Padding = new System.Windows.Forms.Padding(3);
            this.tabPais.Size = new System.Drawing.Size(1086, 686);
            this.tabPais.TabIndex = 4;
            this.tabPais.Text = "Pais";
            this.tabPais.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(298, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(493, 29);
            this.label5.TabIndex = 5;
            this.label5.Text = "Ventas totales agrupadas por país del cliente";
            // 
            // chart_Pais
            // 
            chartArea5.Name = "ChartArea1";
            this.chart_Pais.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chart_Pais.Legends.Add(legend5);
            this.chart_Pais.Location = new System.Drawing.Point(25, 107);
            this.chart_Pais.Name = "chart_Pais";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chart_Pais.Series.Add(series5);
            this.chart_Pais.Size = new System.Drawing.Size(468, 483);
            this.chart_Pais.TabIndex = 4;
            this.chart_Pais.Text = "chart5";
            // 
            // dataGridView_Pais
            // 
            this.dataGridView_Pais.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Pais.Location = new System.Drawing.Point(542, 107);
            this.dataGridView_Pais.Name = "dataGridView_Pais";
            this.dataGridView_Pais.RowHeadersWidth = 51;
            this.dataGridView_Pais.RowTemplate.Height = 24;
            this.dataGridView_Pais.Size = new System.Drawing.Size(538, 486);
            this.dataGridView_Pais.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 739);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tapMontoEmpleados.ResumeLayout(false);
            this.tapMontoEmpleados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Monto_Empleados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Monto_Empleado)).EndInit();
            this.TabCategoria.ResumeLayout(false);
            this.TabCategoria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Categorias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Categorias)).EndInit();
            this.tabEmpleados.ResumeLayout(false);
            this.tabEmpleados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Empleados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Empleados)).EndInit();
            this.tabClientes.ResumeLayout(false);
            this.tabClientes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Clientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Clientes)).EndInit();
            this.tabPais.ResumeLayout(false);
            this.tabPais.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Pais)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Pais)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tapMontoEmpleados;
        private System.Windows.Forms.TabPage TabCategoria;
        private System.Windows.Forms.TabPage tabEmpleados;
        private System.Windows.Forms.TabPage tabClientes;
        private System.Windows.Forms.TabPage tabPais;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView_Monto_Empleados;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Monto_Empleado;
        private System.Windows.Forms.DataGridView dataGridView_Categorias;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Categorias;
        private System.Windows.Forms.DataGridView dataGridView_Empleados;
        private System.Windows.Forms.DataGridView dataGridView_Clientes;
        private System.Windows.Forms.DataGridView dataGridView_Pais;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Empleados;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Clientes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Pais;
    }
}

