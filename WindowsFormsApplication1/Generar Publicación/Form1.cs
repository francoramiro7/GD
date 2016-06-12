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
    public partial class Form1 : Form
    {

        SqlConnection coneccion;
        SqlCommand nombresVisibilidad,costoPorNombre;
        SqlDataReader data;
        
        public Form1()
        {
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();

            nombresVisibilidad = new SqlCommand("PERSISTIENDO.nombreVisibilidades", coneccion);

            nombresVisibilidad.CommandType = CommandType.StoredProcedure;
            
            SqlDataAdapter adapter = new SqlDataAdapter(nombresVisibilidad);
            DataTable tablavisiblidades = new DataTable();

            adapter.Fill(tablavisiblidades);

            comboBox4.DataSource = tablavisiblidades;
            comboBox4.DisplayMember = "Visibilidad_descripcion";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualizar_costo(true, "Oro");
        }

        private void actualizar_costo(bool envia, String visibilidad){

            int costo;
            int envio = 0;
            String costoTotal;

            costoPorNombre = new SqlCommand("PERSISTIENDO.costoVisibilidad", coneccion);

            costoPorNombre.CommandType = CommandType.StoredProcedure;
            costoPorNombre.Parameters.Add("@Detalle", SqlDbType.VarChar).Value = visibilidad;

            SqlDataAdapter adapter = new SqlDataAdapter(costoPorNombre);
            DataTable tablaVisiblidadesCosto = new DataTable();

            adapter.Fill(tablaVisiblidadesCosto);

            costo = Int32.Parse(tablaVisiblidadesCosto.Rows[0]["Visibilidad_precio"].ToString());

            if (envia)
            {
                envio = Int32.Parse(tablaVisiblidadesCosto.Rows[0]["Visibilidad_precio_envio"].ToString());
            }

            costoTotal = (costo + envio).ToString();

            
            textBox6.Text = "$" + costoTotal;

        }

    }
}
