using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsApplication1.ABM_Usuario
{
    public partial class Form3 : Form
    {

        SqlConnection coneccion;
        SqlCommand cargar, cargar2;
        public Form3()
        {
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {


            
            
            

           comboBox1.Text = "Tipo de Usuario";


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
            if (comboBox1.Text == "Cliente")
            {
                cargarClientes();
                dataGridView1.Visible = true;
               
            }else{

          
                cargarEmpresas();
               
                dataGridView1.Visible = true;
            }
        }

        private void cargarClientes() {

            cargar = new SqlCommand("PERSISTIENDO.cargarClientes", coneccion);
            cargar.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter(cargar);
            DataTable table;
            table = new DataTable("PERSISTIENDO.Usuario");
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;



            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;




        }

        private void cargarEmpresas()
        {
            cargar2 = new SqlCommand("PERSISTIENDO.cargarEmpresas", coneccion);
            cargar2.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter(cargar2);
            DataTable table;
            table = new DataTable("PERSISTIENDO.Empresa");
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


           
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }


       


    }
}