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
            if (usuario.Rol.Equals("Administrador"))
            {
                cargarPublicaciones();
            }else{
                cargarPublicacionesPorUsuario();
            }
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

        private void cargarPublicacionesPorUsuario(){
        
            cargar = new SqlCommand("PERSISTIENDO.cargarPublicacionesPorUsuario", coneccion);
            cargar.CommandType = CommandType.StoredProcedure;
            cargar.Parameters.Add("@Username", SqlDbType.VarChar).Value = usuario.username;
            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter(cargar);
            DataTable table;
            table = new DataTable("PERSISTIENDO.Publicacion");
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cod ="";
            string desc ="";
            string precio = "";
            string stock = "";
            string estado = "";

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                cod = row.Cells[0].Value.ToString();
                desc = row.Cells[1].Value.ToString();
                precio = row.Cells[2].Value.ToString();
                stock = row.Cells[3].Value.ToString();
                estado = row.Cells[4].Value.ToString();
            }

            Generar_Publicación.Form3 form3 = new Generar_Publicación.Form3(cod,desc,precio,stock,estado);
            form3.Show();
            this.Close();
        }
    }
}
