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


namespace WindowsFormsApplication1.ABM_Visibilidad
{
    public partial class Form2 : Form
    {

        SqlConnection con;
        SqlDataReader data;
        SqlCommand ult, visi, existe;

        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

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

            if (esNumero(ingresado,true))
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


                con.Open();
                existe = new SqlCommand("PERSISTIENDO.existeVisibilidad", con);
                existe.CommandType = CommandType.StoredProcedure;
                existe.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox1.Text;
                var resultado = existe.Parameters.Add("@Valor", SqlDbType.Int);
                resultado.Direction = ParameterDirection.ReturnValue;
                data = existe.ExecuteReader();
                var existeR = resultado.Value;
                data.Close();
                con.Close();

                if ((int)existeR == 1)
                {
                    String mensaje = "La visibilidad ya existe, ingrese otro nombre";
                    String caption = "Error al crear la visibilidad";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                }
                else
                    crearVisibilidad();

            }
        }


        private void crearVisibilidad()
        {
            con.Open();
            ult = new SqlCommand("PERSISTIENDO.ultimaVisibilidad", con);

            ult.CommandType = CommandType.StoredProcedure;
            var up = ult.Parameters.Add("@Cantidad", SqlDbType.Float);
            up.Direction = ParameterDirection.ReturnValue;
            data = ult.ExecuteReader();
            var codigo = up.Value;
            data.Close();
            
            visi= new SqlCommand("PERSISTIENDO.newVisibilidad", con);
            double p = double.Parse(textBox4.Text);
            double porcentaje = p / 100;

            visi.CommandType = CommandType.StoredProcedure;
            visi.Parameters.Add("@cod", SqlDbType.Float).Value = (float.Parse(codigo.ToString(), CultureInfo.InvariantCulture.NumberFormat) + 1);
            visi.Parameters.Add("@desc", SqlDbType.VarChar).Value = textBox1.Text;
            visi.Parameters.Add("@precio", SqlDbType.Float).Value = textBox3.Text;
            visi.Parameters.Add("@porc", SqlDbType.Float).Value = porcentaje;
            visi.Parameters.Add("@envio", SqlDbType.Float).Value = textBox5.Text;
            visi.ExecuteNonQuery();


            String mensaje = "La Visibilidad ha sido creada correctamente";
                String caption = "Visibilidad creada";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

                textBox1.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";

                ABM_Visibilidad.Form1 f1 = new ABM_Visibilidad.Form1();
                f1.Show();
                this.Close();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            ABM_Visibilidad.Form1 f1 =  new ABM_Visibilidad.Form1();
            f1.Show();
            this.Close();
        }


        }

    }

