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
using System.Globalization;


namespace WindowsFormsApplication1.ABM_Rol
{
    public partial class Form4 : Form
    {
        SqlConnection coneccion;
        SqlDataReader data;
        SqlCommand cargarRoles, eliminar;

        public Form4()
        {
            InitializeComponent();

            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
            cargarRoles = new SqlCommand("PERSISTIENDO.cargarRoles", coneccion);

            cargarRoles.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cargarRoles);
            DataTable tablaRoles = new DataTable();

            coneccion.Close();
            adapter.Fill(tablaRoles);
            comboBox2.DataSource = tablaRoles;
            comboBox2.DisplayMember = "Rol_nombre";
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            label4.Visible = true;
            label4.Text = comboBox2.Text.ToString();
            button2.Visible = true;
            button3.Visible = true;

            

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //coneccion.Open();
            //eliminar = new SqlCommand("PERSISTIENDO.eliminarRol", coneccion);
            //eliminar.CommandType = CommandType.StoredProcedure;
            //eliminar.Parameters.Add("@nombre", SqlDbType.VarChar).Value = comboBox2.Text.ToString();
            
            //eliminar.ExecuteNonQuery();
            //coneccion.Close();

            //ELIMINAR PRIMERO FUNCIONALIDADES Y LUEGO ROL


            String mensaje = "El rol se ha eliminado exitosamente";
            String caption = "Rol eliminado";
            MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ABM_Rol.Form1 form1 = new ABM_Rol.Form1();
            this.Close();
            form1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            label4.Visible = false;
            label4.Text = string.Empty;
            button2.Visible = false;
            button3.Visible = false;
        }
    }
}
