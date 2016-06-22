using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Globalization;
using System.Data.SqlClient;

namespace WindowsFormsApplication1.ABM_Usuario
{
    public partial class Form8 : Form
    {

        SqlDataReader data;
       
        SqlConnection coneccion;
        SqlCommand cargar, cargar2, username, eliminar1, eliminar2, eliminar3;

        public Form8()
        {
            InitializeComponent();

            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "Tipo de Usuario";
        }


        private void cargarClientes()
        {
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

            }
            else
            {


                cargarEmpresas();
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = false;
                label1.Visible = true;
                label6.Visible = true;
                label5.Visible = true;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;

                dataGridView1.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ABM_Usuario.Form2 frm2 = new ABM_Usuario.Form2();
            this.Close();
            frm2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Cliente")
            {
                eliminarCliente();
            }
            else
            {

                eliminarEmpresa();

            }
        }

        private void eliminarCliente(){

            float dni = 0 ;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dni = float.Parse(row.Cells[0].Value.ToString(), CultureInfo.InvariantCulture.NumberFormat);
            }

            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar el usuario?", "Eliminar usuario", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                coneccion.Open();
                username = new SqlCommand("PERSISTIENDO.usernameCliente", coneccion);
                username.CommandType = CommandType.StoredProcedure;
                username.Parameters.Add("@dni", SqlDbType.Float).Value = dni;
                SqlDataAdapter adapter = new SqlDataAdapter(username);
                DataTable table = new DataTable();
                adapter.Fill(table);

                String user = table.Rows[0][0].ToString();

                eliminar1 = new SqlCommand("PERSISTIENDO.eliminarRPU", coneccion);
                eliminar1.CommandType = CommandType.StoredProcedure;
                eliminar1.Parameters.Add("@Username", SqlDbType.VarChar).Value = user;
                eliminar1.ExecuteNonQuery();

                eliminar2 = new SqlCommand("PERSISTIENDO.eliminarCliente", coneccion);
                eliminar2.CommandType = CommandType.StoredProcedure;
                eliminar2.Parameters.Add("@Username", SqlDbType.VarChar).Value = user;
                eliminar2.ExecuteNonQuery();

                eliminar3 = new SqlCommand("PERSISTIENDO.eliminarUsuario", coneccion);
                eliminar3.CommandType = CommandType.StoredProcedure;
                eliminar3.Parameters.Add("@Username", SqlDbType.VarChar).Value = user;
                eliminar3.ExecuteNonQuery();
                coneccion.Close();

                String mensaje = "El usuario se ha eliminado correctamente";
                String caption = "Usuario borrado";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                
}


        
        }

        private void eliminarEmpresa() {

            String cuit = "";

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                cuit = (row.Cells[0].Value.ToString());
            }

            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar el usuario?", "Eliminar usuario", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                coneccion.Open();
                username = new SqlCommand("PERSISTIENDO.usernameEmpresa", coneccion);
                username.CommandType = CommandType.StoredProcedure;
                username.Parameters.Add("@cuit", SqlDbType.VarChar).Value = cuit;
                SqlDataAdapter adapter = new SqlDataAdapter(username);
                DataTable table = new DataTable();
                adapter.Fill(table);

                String user = table.Rows[0][0].ToString();

                eliminar1 = new SqlCommand("PERSISTIENDO.eliminarRPU", coneccion);
                eliminar1.CommandType = CommandType.StoredProcedure;
                eliminar1.Parameters.Add("@Username", SqlDbType.VarChar).Value = user;
                eliminar1.ExecuteNonQuery();

                eliminar2 = new SqlCommand("PERSISTIENDO.eliminarEmpresa", coneccion);
                eliminar2.CommandType = CommandType.StoredProcedure;
                eliminar2.Parameters.Add("@Username", SqlDbType.VarChar).Value = user;
                eliminar2.ExecuteNonQuery();

                eliminar3 = new SqlCommand("PERSISTIENDO.eliminarUsuario", coneccion);
                eliminar3.CommandType = CommandType.StoredProcedure;
                eliminar3.Parameters.Add("@Username", SqlDbType.VarChar).Value = user;
                eliminar3.ExecuteNonQuery();
                coneccion.Close();

                String mensaje = "El usuario se ha eliminado correctamente";
                String caption = "Usuario borrado";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);




            }
        }
    }
}
