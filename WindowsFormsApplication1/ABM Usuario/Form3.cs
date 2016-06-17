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
using System.Globalization;

namespace WindowsFormsApplication1.ABM_Usuario
{
    public partial class Form3 : Form
    {
        SqlDataReader data;
        SqlConnection coneccion;
        SqlCommand cargar, cargar2, cargarDatos, filtrar, cliUser, cliTipo, cliCalle, cliNro, cliPiso, cliDpto, cliCp, clipFecha, cliLocalidad;
        public Form3()
        {
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
           
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
            button1.Visible = true;
            button2.Visible = true;
            
            if (comboBox1.Text == "Cliente")
            {
                
                cargarClientes();
                dataGridView1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label1.Visible = false;
                label6.Visible = false;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
               
            }else{

          
                cargarEmpresas();
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = false;
                label1.Visible = true;
                label6.Visible = true;
                label5.Visible = true;
                label2.Visible =false;
                label3.Visible =false;
                label4.Visible = false;
               
                dataGridView1.Visible = true;
            }
        }

        private void cargarClientes() {
            coneccion.Open();
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
            coneccion.Close();




        }

        private void cargarEmpresas()
        {
            coneccion.Open();
            cargar2 = new SqlCommand("PERSISTIENDO.cargarEmpresas", coneccion);
            cargar2.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter(cargar2);
            DataTable table;
            table = new DataTable("PERSISTIENDO.Empresa");
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            coneccion.Close();


           
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text.Equals("Cliente")){

                String nombre = "";
                String apellido = "";
                String mail = "";
                float dni = 0;
                String calle = "";
                String tipo = "";
                String depto= "";
                float nro = 0;
                float piso = 0;
                String localidad = "";
                String cp = "";
                DateTime fecha = new DateTime();
                String username = "";

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dni = float.Parse(row.Cells[0].Value.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    apellido = row.Cells[1].Value.ToString();
                    nombre = row.Cells[2].Value.ToString();
                    mail = row.Cells[3].Value.ToString();
                    }

                coneccion.Open();

                cargarDatos = new SqlCommand("PERSISTIENDO.datosString", coneccion);
                cargarDatos.Parameters.Add("@dni", SqlDbType.Float).Value = dni;
                cargarDatos.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter;
                adapter = new SqlDataAdapter(cargarDatos);
                DataTable table;
                table = new DataTable();
                adapter.Fill(table);
                DataGridView datos = new DataGridView();
                datos.DataSource = table;
                dataGridView1.DataSource = table;
                datos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                int cant = dataGridView1.Rows.Count;
                
                coneccion.Close();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                   
                    cp = row.Cells[0].Value.ToString();
                    depto = row.Cells[1].Value.ToString();
                    calle = row.Cells[2].Value.ToString();
                    fecha = DateTime.Parse(row.Cells[3].Value.ToString());
                    localidad = row.Cells[4].Value.ToString();
                    nro = float.Parse(row.Cells[5].Value.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    piso = float.Parse(row.Cells[6].Value.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    tipo = row.Cells[7].Value.ToString();
                    username = row.Cells[8].Value.ToString();
                }

                
                
                ABM_Usuario.Form4 form4 = new Form4(username, nombre, apellido, dni, tipo, mail, calle,
                  nro, piso, depto, cp, fecha,localidad);

                form4.Show();
                this.Close();
                
            }

            else {
                ABM_Usuario.Form5 form5 = new Form5();
                form5.Show();
                this.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ABM_Usuario.Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filtrar = new SqlCommand("PERSISTIENDO.filter", coneccion);
            filtrar.CommandType = CommandType.StoredProcedure;

            filtrar.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox1.Text;
            filtrar.Parameters.Add("@apellido", SqlDbType.VarChar).Value = textBox2.Text;
            filtrar.Parameters.Add("@mail", SqlDbType.VarChar).Value = textBox3.Text;

            float vardni = -1;

            if (!String.IsNullOrEmpty(textBox4.Text))
            {
                vardni=float.Parse(textBox4.Text, CultureInfo.InvariantCulture.NumberFormat);

            }

            filtrar.Parameters.Add("@dni", SqlDbType.Float).Value = vardni;

            SqlDataAdapter adapter;

            adapter = new SqlDataAdapter(filtrar);
            DataTable table;
            table = new DataTable("PERSISTIENDO.U");
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


    }
}