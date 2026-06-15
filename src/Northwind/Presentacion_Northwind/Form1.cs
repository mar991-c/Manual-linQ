using Northwind_Entidades;
using Northwind_Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Presentacion_Northwind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CargarMontoEmpleado();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0: CargarMontoEmpleado(); break;
                case 1: CargarCantidadCategoria(); break;
                case 2: CargarMejorTrimestre(); break;
                case 3: CargarClientes(); break;
                case 4: CargarPaises(); break;
            }
        }

        private void CargarMejorTrimestre()
        {
            try
            {
                List<MejorEmpleadoTrimestre> lista = MejorEmpleadoTrimestre_Logica.ObtenerTop3EmpleadosTrimestre();

                dataGridView_Empleados.DataSource = lista;
                dataGridView_Empleados.Columns["NombreEmpleado"].HeaderText = "Empleado";
                dataGridView_Empleados.Columns["MontoTotalOrden"].HeaderText = "Monto Total ($)";

                chart_Empleados.Series.Clear();
                Series s = new Series("Monto");
                s.ChartType = SeriesChartType.Column;
                foreach (var e in lista)
                    s.Points.AddXY(e.NombreEmpleado, (double)e.MontoTotalOrden);
                chart_Empleados.Series.Add(s);
                chart_Empleados.Titles.Clear();
                chart_Empleados.Titles.Add("Top 3 Empleados Último Trimestre");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar trimestre: " + ex.Message);
            }
        }

        private void CargarCantidadCategoria()
        {
            try
            {
                List<CantidadCategoria> lista = CantidadCategoria_Logica.ObtenerCantidadPorCategoria();

                dataGridView_Categorias.DataSource = lista;
                dataGridView_Categorias.Columns["NombreCategoria"].HeaderText = "Categoría";
                dataGridView_Categorias.Columns["NumeroOrdenes"].HeaderText = "N° Órdenes";

                chart_Categorias.Series.Clear();
                Series s = new Series("Órdenes");
                s.ChartType = SeriesChartType.Column;
                foreach (var c in lista)
                    s.Points.AddXY(c.NombreCategoria, c.NumeroOrdenes);
                chart_Categorias.Series.Add(s);
                chart_Categorias.Titles.Clear();
                chart_Categorias.Titles.Add("Órdenes por Categoría");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message);
            }
        }

        private void CargarMontoEmpleado()
        {
            try
            {
                List<MontoEmpleado> lista = MontoEmpleado_Logica.ObtenerMontoPorEmpleado();

                dataGridView_Monto_Empleados.DataSource = lista;
                dataGridView_Monto_Empleados.Columns["NombreEmpleado"].HeaderText = "Empleado";
                dataGridView_Monto_Empleados.Columns["MontoTotalOrden"].HeaderText = "Monto Total ($)";

                chart_Monto_Empleado.Series.Clear();
                Series s = new Series("Monto");
                s.ChartType = SeriesChartType.Bar;
                foreach (var e in lista)
                    s.Points.AddXY(e.NombreEmpleado, (double)e.MontoTotalOrden);
                chart_Monto_Empleado.Series.Add(s);
                chart_Monto_Empleado.Titles.Clear();
                chart_Monto_Empleado.Titles.Add("Monto Total por Empleado");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar monto empleado: " + ex.Message);
            }
        }

        private void CargarPaises()
        {
            try
            {
                List<VentaPais> lista = VentaPais_Logica.ObtenerVentasPorPais();

                dataGridView_Pais.DataSource = lista;
                dataGridView_Pais.Columns["Pais"].HeaderText = "País";
                dataGridView_Pais.Columns["TotalVentas"].HeaderText = "Ventas ($)";
                dataGridView_Pais.Columns["NumeroClientes"].HeaderText = "N° Clientes";

                chart_Pais.Series.Clear();
                Series s = new Series("Ventas");
                s.ChartType = SeriesChartType.Column;
                foreach (var p in lista)
                    s.Points.AddXY(p.Pais, (double)p.TotalVentas);
                chart_Pais.Series.Add(s);
                chart_Pais.Titles.Clear();
                chart_Pais.Titles.Add("Ventas Totales por País");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar países: " + ex.Message);
            }
        }

        private void CargarClientes()
        {
            try
            {
                List<ClienteGasto> lista = ClienteGasto_Logica.ObtenerTop10Clientes();

                dataGridView_Clientes.DataSource = lista;
                dataGridView_Clientes.Columns["NombreCliente"].HeaderText = "Cliente";
                dataGridView_Clientes.Columns["Pais"].HeaderText = "País";
                dataGridView_Clientes.Columns["TotalGastado"].HeaderText = "Total ($)";
                dataGridView_Clientes.Columns["NumeroOrdenes"].HeaderText = "N° Órdenes";

                chart_Clientes.Series.Clear();
                Series s = new Series("Gasto");
                s.ChartType = SeriesChartType.Bar;
                foreach (var c in lista)
                    s.Points.AddXY(c.NombreCliente, (double)c.TotalGastado);
                chart_Clientes.Series.Add(s);
                chart_Clientes.Titles.Clear();
                chart_Clientes.Titles.Add("Top 10 Clientes por Gasto");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message);
            }
        }

        
    }
}
