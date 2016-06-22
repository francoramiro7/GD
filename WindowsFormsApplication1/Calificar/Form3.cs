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

namespace WindowsFormsApplication1.Calificar
{
    public partial class Form3 : Form
    {
        SqlConnection coneccion;
        SqlCommand calificarCompra, ultimaCalificacion;
        SqlDataReader data;
        int cod_compra;

        public Form3(String compra,String detalle)
        {
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
            cod_compra = Int32.Parse(compra);
            labelDescripcion.Text = detalle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Calificar.Form2 form2 = new Calificar.Form2();
            form2.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(comboBox1.Text))
            {
                string comentario ="";

                if (!String.IsNullOrEmpty(textBox1.Text))
                {
                    comentario = textBox1.Text;
                }

                if(textBox1.Text.Length > 255)
                {
                    String mensaje = "El comentario es demasiado largo";
                    String caption = "Imposible calificar";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

                }else{

                    ultimaCalificacion = new SqlCommand("PERSISTIENDO.ultimaCalificacion", coneccion);
                    ultimaCalificacion.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(ultimaCalificacion);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    float cod_cali = (float)Double.Parse(table.Rows[0][0].ToString());

                    calificarCompra = new SqlCommand("PERSISTIENDO.calificarCompra", coneccion);

                    calificarCompra.CommandType = CommandType.StoredProcedure;
                    calificarCompra.Parameters.Add("@Codigo", SqlDbType.Float).Value = (cod_cali + 1);
                    calificarCompra.Parameters.Add("@Compra", SqlDbType.Int).Value = cod_compra;
                    calificarCompra.Parameters.Add("@Estrellas", SqlDbType.Float).Value = comboBox1.Text;
                    calificarCompra.Parameters.Add("@Comentario", SqlDbType.VarChar).Value = comentario;

                    calificarCompra.ExecuteNonQuery();

                    Calificar.Form2 form2 = new Calificar.Form2();
                    form2.Show();
                    this.Close();

                }

            }
            else
            {
                String mensaje = "Por favor ingrese la cantidad de estrellas";
                String caption = "Imposible calificar";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
            }




        }
    }
}
