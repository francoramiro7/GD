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

namespace WindowsFormsApplication1.ABM_Visibilidad
{
    public partial class Form3 : Form
    {

          SqlDataReader data;
        SqlConnection coneccion;
        SqlCommand cargar, nombresVisibilidad;

        public Form3()
        {
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            coneccion.Open();

            nombresVisibilidad = new SqlCommand("PERSISTIENDO.nombreVisibilidades", coneccion);

            nombresVisibilidad.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(nombresVisibilidad);
            DataTable tablavisiblidades = new DataTable();

            adapter.Fill(tablavisiblidades);
            comboBox2.DataSource = tablavisiblidades;
            comboBox2.DisplayMember = "Visibilidad_descripcion";
            coneccion.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            textBox1.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;

            String nombre = comboBox2.Text.ToString();
            coneccion.Open();

            cargar = new SqlCommand("PERSISTIENDO.datosVisibilidad", coneccion);
            cargar.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;

            cargar.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cargar);
            DataTable tablaDATOS = new DataTable();

            adapter.Fill(tablaDATOS);

            string cod = tablaDATOS.Rows[0][0].ToString();
            string des = tablaDATOS.Rows[0][1].ToString();
            float porc = float.Parse(tablaDATOS.Rows[0][2].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            string pre = tablaDATOS.Rows[0][3].ToString();
            string envio = tablaDATOS.Rows[0][4].ToString();

            textBox1.Text = des;
            textBox3.Text = pre;
            textBox4.Text = porc.ToString();
            textBox5.Text = envio;


        }
    }
}
