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

namespace WindowsFormsApplication1.Generar_Publicación
{
    public partial class Form4 : Form
    {

        SqlConnection coneccion;
        SqlCommand cargar;

        public Form4()
        {
            InitializeComponent();

            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            cargarPublicaciones();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Generar_Publicación.Form2 form2 = new Generar_Publicación.Form2();
            form2.Show();
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void cargarPublicaciones()
        {

            cargar = new SqlCommand("PERSISTIENDO.cargarPublicaciones", coneccion);
            cargar.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter(cargar);
            DataTable table;
            table = new DataTable("PERSISTIENDO.Publicacion");
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;



            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
