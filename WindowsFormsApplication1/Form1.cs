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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        SqlConnection coneccion;
        SqlCommand cmd;
        SqlDataReader data;

        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
                coneccion.Open();
                cmd = new SqlCommand("PERSISTIENDO.ValidarUsuario", coneccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;

                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = textBox2.Text;
                var resultado = cmd.Parameters.Add("@Valor", SqlDbType.Int);
                resultado.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteReader();
                var resultado2 = resultado.Value;
                coneccion.Close();
                if ((int)resultado2 == 1)
                {
                    abrirFormulario2();
                }
                else
                {
                    String mensaje = "username o Password incorrectos, intetelo de nuevo";
                    String caption = "Error en iniciar sesion";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                }

            }
           
          
            
            

        }




        private void abrirFormulario2()
        {
            Form2 form2 = new Form2();
            //this.Close();
            form2.Show();
            
        }
        private Boolean validarCampos()
        {
            if (string.IsNullOrEmpty(textBox1.Text) | string.IsNullOrEmpty(textBox2.Text))
            {
                String mensaje = "Los campos Username y Password son obligatorios";
                String caption = "Ingrese Username y Password";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                return false;
                


            }
            else
            {

                return true;
            }

        }
    }
}