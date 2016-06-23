﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1.ComprarOfertar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
        }
        
        private SqlCommand command { get; set; }
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        public Object SelectedItem { get; set; }


        SqlConnection coneccion;
        SqlCommand rubros,getPublicaciones;
        SqlDataReader data;

        int pag = 0;
        int pagTotal=0;
        int totalRows = 0;

        bool abrio = true;


        DataTable tabla;

        

                
        private void Form1_Load(object sender, EventArgs e)
        {            
            CargarRubros();            
        }

        private void CargarRubros()
        {

            rubros = new SqlCommand("PERSISTIENDO.listarRubros", coneccion);
            rubros.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapterRubro = new SqlDataAdapter(rubros);
            DataTable tablaRubros = new DataTable();
            adapterRubro.Fill(tablaRubros);
            comboBoxRubro.DataSource = tablaRubros;
            comboBoxRubro.DisplayMember = "Rubro_Descripcion";
            comboBoxRubro.SelectedIndex = comboBoxRubro.Items.IndexOf("New");

        }

        private void botonBuscar_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            //parametros = new List<SqlParameter>();
            //parametros.Clear();
            //parametros.Add(new SqlParameter("@usuario", idUsuarioActual));

            DataTable busquedaTemporal = new DataTable();

            getPublicaciones = new SqlCommand("PERSISTIENDO.getPublicaciones", coneccion);
            getPublicaciones.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(getPublicaciones);
            adapter.Fill(busquedaTemporal);

            int cantFilas = busquedaTemporal.Rows.Count;
            if (cantFilas == 0)
            {
                MessageBox.Show("No hay resultados");
                return;
            }
            else
            {
                tabla = busquedaTemporal;

                totalRows = tabla.Rows.Count;
                float cant = (tabla.Rows.Count / 10);

                pagTotal = (int)cant;

                if ((cant - ((int)cant)) != 0)
                {
                    pagTotal++;
                }

                actualizarTabla();
            }
            AgregarBotonVerPublicacion();
        }


        private void botonPrimeraPagina_Click(object sender, EventArgs e)
        {
            if (totalRows != 0)
            {
                pag = 0;
                actualizarTabla();
            }
        }

        private void botonPaginaAnterior_Click(object sender, EventArgs e)
        {
            if (pag > 0)
            {
                pag = pag - 1;
                actualizarTabla();

            }
        }

        private void botonPaginaSiguiente_Click(object sender, EventArgs e)
        {
            if (pag != pagTotal && totalRows!=0)
            {
                pag++;
                actualizarTabla();

            }
        }

        private void botonUltimaPagina_Click(object sender, EventArgs e)
        {
            if (totalRows != 0)
            {
                pag = pagTotal;
                actualizarTabla();
            }
            
        }

        private void AgregarBotonVerPublicacion()
        {
            if (dataGridView1.Columns.Contains("Ver Publicacion"))
                dataGridView1.Columns.Remove("Ver Publicacion");
            DataGridViewButtonColumn buttons = new DataGridViewButtonColumn();
            {
                buttons.HeaderText = "Ver Publicacion";
                buttons.Text = "Ver Publicacion";
                buttons.Name = "Ver Publicacion";
                buttons.UseColumnTextForButtonValue = true;
                buttons.AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
                buttons.FlatStyle = FlatStyle.Standard;
                buttons.CellTemplate.Style.BackColor = Color.Honeydew;
                dataGridView1.CellClick +=
                    new DataGridViewCellEventHandler(dataGridView1_CellClick);
            }

            dataGridView1.Columns.Add(buttons);


        }        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 5 && abrio)
            {
                string cod = "";
                string desc = "";
                string precio = "";
                string rub = "";
                string tip = "";

                
                cod = tabla.Rows[(e.RowIndex)+(pag*10)][0].ToString();
                desc = tabla.Rows[(e.RowIndex) + (pag * 10)][1].ToString();
                precio = tabla.Rows[(e.RowIndex) + (pag * 10)][2].ToString();
                rub = tabla.Rows[(e.RowIndex) + (pag * 10)][3].ToString();
                tip = tabla.Rows[(e.RowIndex) + (pag * 10)][4].ToString();
                
                ComprarOfertar.Form2 form2 = new ComprarOfertar.Form2(cod, desc, precio, rub, tip);
                form2.Show();
                abrio = false;
                this.Close();
            }
        }

        private void botonLimpiar_Click(object sender, EventArgs e)
        {
            textBoxDescripcion.Clear();
            comboBoxRubro.SelectedIndex = -1;
            labelNrosPagina.Text = "";
            totalRows = 0;
            pagTotal = 0;
            dataGridView1.DataSource = null;
            if (dataGridView1.Columns.Contains("Ver Publicacion"))
                dataGridView1.Columns.Remove("Ver Publicacion");
        }

        private void botonVolver_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.Form2 form2 = new WindowsFormsApplication1.Form2();
            form2.Show();
            this.Close();
        }



        private void actualizarTabla()
        {
            int ini = pag * 10;

            int n = 0;

            DataTable temporal = tabla.Clone();


            dataGridView1.ClearSelection();
            temporal.Clear();

            while (ini < totalRows && n < 10)
            {

                DataRow row;
                row = temporal.NewRow();
                row.ItemArray = tabla.Rows[ini].ItemArray;
                temporal.Rows.Add(row);
                ini++;
                n++;

            }
            dataGridView1.DataSource = temporal;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            AgregarBotonVerPublicacion();

            labelNrosPagina.Text = "Pagina " + pag.ToString() + " de " + pagTotal.ToString();

        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
