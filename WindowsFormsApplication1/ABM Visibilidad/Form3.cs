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
        SqlCommand cargar, nombresVisibilidad, existe, visi;
        string nom = "";
        string cod = "";

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
            nom = comboBox2.Text.ToString();

            cargar = new SqlCommand("PERSISTIENDO.datosVisibilidad", coneccion);
            cargar.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;

            cargar.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cargar);
            DataTable tablaDATOS = new DataTable();

            adapter.Fill(tablaDATOS);

            string codigo = tablaDATOS.Rows[0][0].ToString();
            string des = tablaDATOS.Rows[0][1].ToString();
            double porc = double.Parse(tablaDATOS.Rows[0][2].ToString());
            string pre = tablaDATOS.Rows[0][3].ToString();
            string envio = tablaDATOS.Rows[0][4].ToString();

            cod = codigo;
            
            

            textBox1.Text = des;
            textBox3.Text = pre;
            textBox4.Text = (porc * 100).ToString();
            textBox5.Text = envio;
            coneccion.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            validarCampos();
            
        }

        private void validarCampos()
        {
            if (string.IsNullOrEmpty(textBox1.Text) | string.IsNullOrEmpty(textBox5.Text) | string.IsNullOrEmpty(textBox3.Text) |
              string.IsNullOrEmpty(textBox4.Text))
            {


                String mensaje = "Todos los campos son obligatorios";
                String caption = "Error al crear visibilidad";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
            else
            {


                coneccion.Open();
                existe = new SqlCommand("PERSISTIENDO.existeVisibilidad", coneccion);
                existe.CommandType = CommandType.StoredProcedure;
                existe.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox1.Text;
                var resultado = existe.Parameters.Add("@Valor", SqlDbType.Int);
                resultado.Direction = ParameterDirection.ReturnValue;
                data = existe.ExecuteReader();
                var existeR = resultado.Value;
                data.Close();
                coneccion.Close();

                if ((int)existeR == 1 & !(nom.Equals(textBox1.Text)))
                {
                    String mensaje = "La visibilidad ya existe, ingrese otro nombre";
                    String caption = "Error al crear la visibilidad";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                }
                else
                {
                    modificarVisibilidad();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ABM_Visibilidad.Form1 f1 = new ABM_Visibilidad.Form1();
            f1.Show();
            this.Close();
        }
        private void modificarVisibilidad()
        {
            coneccion.Open();
            visi = new SqlCommand("PERSISTIENDO.updateVisibilidad", coneccion);
            double p = double.Parse(textBox4.Text);
            double porcentaje = p / 100;

            visi.CommandType = CommandType.StoredProcedure;
            visi.Parameters.Add("@codigo", SqlDbType.Float).Value = float.Parse(cod.ToString(), CultureInfo.InvariantCulture.NumberFormat);
            visi.Parameters.Add("@desc", SqlDbType.VarChar).Value = textBox1.Text;
            visi.Parameters.Add("@precio", SqlDbType.Float).Value = textBox3.Text;
            visi.Parameters.Add("@porc", SqlDbType.Float).Value = porcentaje;
            visi.Parameters.Add("@envio", SqlDbType.Float).Value = textBox5.Text;
            visi.ExecuteNonQuery();
            coneccion.Close();

            String mensaje = "La visibilidad se ha modificado correctamente";
            String caption = "Se modifico la visibilidad";
            MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
            ABM_Visibilidad.Form1 f1 = new ABM_Visibilidad.Form1();
            f1.Show();
            this.Close();

        }
        private bool esNumero(String ingresado, bool tieneComa)
        {

            char[] ingre = ingresado.ToCharArray();
            int comas = 0;
            for (int i = 0; i < ingresado.Length; i++)
            {

                if (ingre[0].Equals(','))
                {
                    return false;
                }

                if (!char.IsNumber(ingre[i]))
                {
                    if (tieneComa)
                    {
                        if ((!ingre[i].Equals(',')) || (comas > 0))
                        {
                            return false;
                        }
                        else
                        {
                            comas++;
                        }
                    }
                    else
                    {
                        return false;
                    }


                }
            }
            return true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            String ingresado = ((TextBox)sender).Text;

            if (esNumero(ingresado, true))
            {
            }
            else
            {
                String mensaje = "Solo se pueden ingresar numeros y ',' en este campo";
                String caption = "Error al ingresar datos";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                textBox3.Text = "";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            String ingresado = ((TextBox)sender).Text;

            if (esNumero(ingresado, false))
            {
            }
            else
            {
                String mensaje = "Solo se pueden ingresar numeros en este campo";
                String caption = "Error al ingresar datos";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                textBox4.Text = "";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            String ingresado = ((TextBox)sender).Text;

            if (esNumero(ingresado, true))
            {
            }
            else
            {
                String mensaje = "Solo se pueden ingresar numeros y ',' en este campo";
                String caption = "Error al ingresar datos";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                textBox5.Text = "";
            }
        }

    }
}
