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

namespace WindowsFormsApplication1.ABM_Usuario
{

    public partial class Form4 : Form
    {
        SqlConnection con;
        SqlDataReader data;
        SqlCommand existeDni, modificar, habilitado, bloquearUsuario, desbloquearUsuario;
        int validacionCliente = 0;
        float docu = 0;
        String user = "";


        public Form4(String username, String nombre, String apellido, float dni, String tipo, String mail, String calle,
                  float nro, String piso, String depto, String cp, DateTime fecha, String localidad)
        {

            con = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            user = username;
            docu = dni;
            InitializeComponent();
            textBox1.Text = username;
            textBox12.Text = nombre;
            textBox2.Text = apellido;
            textBox3.Text = mail;
            textBox4.Text = ((int)dni).ToString();
            comboBox1.Text = tipo;
            textBox7.Text = calle;
            textBox8.Text = nro.ToString();
            textBox9.Text = piso;
            textBox10.Text = depto;
            textBox11.Text = cp;
            textBox6.Text = localidad;
            dateTimePicker1.Value = fecha;
            int habilitado = estaHabilitado(user);
            if (habilitado == 0)
                button3.Visible = true;
            

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private int estaHabilitado(String username)
        {
            con.Open();
            habilitado = new SqlCommand("PERSISTIENDO.estaBloqueado", con);
            habilitado.CommandType = CommandType.StoredProcedure;
            habilitado.Parameters.Add("@Username", SqlDbType.VarChar).Value = username;
            var resultado = habilitado.Parameters.Add("@Valor", SqlDbType.Bit);
            resultado.Direction = ParameterDirection.ReturnValue;
            data = habilitado.ExecuteReader();
            var habi = resultado.Value;
            int respuesta = (int)habi;
            con.Close();
            data.Close();
            return respuesta;
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
        private void button4_Click(object sender, EventArgs e)
        {
            
            textBox12.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            comboBox1.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox10.Text = string.Empty;
            textBox11.Text = string.Empty;
            textBox6.Text = string.Empty;
            dateTimePicker1.Value = dateTimePicker1.MaxDate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            validarVaciosCliente();
        }


        private void validarVaciosCliente()
        {
            if (string.IsNullOrEmpty(textBox2.Text) | string.IsNullOrEmpty(textBox3.Text) |
              string.IsNullOrEmpty(textBox4.Text) | string.IsNullOrEmpty(textBox11.Text) | string.IsNullOrEmpty(textBox12.Text) |
                string.IsNullOrEmpty(textBox7.Text) | string.IsNullOrEmpty(textBox8.Text) |
                comboBox1.SelectedIndex == -1 | dateTimePicker1.Text == "")
            {

                String mensaje = "Los campos (*) son obligatorios";
                String caption = "Error al crear usuario";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
            else
            {

                validarCamposCliente();
            }


        }


        private void modificarCliente()
        {
            con.Open();
            modificar = new SqlCommand("PERSISTIENDO.updateCliente", con);
            modificar.CommandType = CommandType.StoredProcedure;

            modificar.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
            modificar.Parameters.Add("@apellido", SqlDbType.VarChar).Value = textBox2.Text;
            modificar.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox12.Text;
            modificar.Parameters.Add("@mail", SqlDbType.VarChar).Value = textBox3.Text;
            modificar.Parameters.Add("@calle", SqlDbType.VarChar).Value = textBox7.Text;
            modificar.Parameters.Add("@cp", SqlDbType.VarChar).Value = textBox11.Text;
            modificar.Parameters.Add("@depto", SqlDbType.VarChar).Value = textBox10.Text;
            if (String.IsNullOrEmpty(textBox9.Text))
                modificar.Parameters.Add("@piso", SqlDbType.Float).Value = DBNull.Value;
            else
                modificar.Parameters.Add("@piso", SqlDbType.Float).Value = float.Parse(textBox9.Text, CultureInfo.InvariantCulture.NumberFormat);
            
            modificar.Parameters.Add("@dni", SqlDbType.Float).Value = float.Parse(textBox4.Text, CultureInfo.InvariantCulture.NumberFormat);
            modificar.Parameters.Add("@tipo", SqlDbType.VarChar).Value = comboBox1.SelectedItem.ToString();
            modificar.Parameters.Add("@nro", SqlDbType.Float).Value = float.Parse(textBox8.Text, CultureInfo.InvariantCulture.NumberFormat);
            modificar.Parameters.Add("@fechanac", SqlDbType.DateTime).Value = dateTimePicker1.Value;
            modificar.Parameters.Add("@localidad", SqlDbType.VarChar).Value = textBox6.Text;
            modificar.ExecuteNonQuery();
            con.Close();

            String mensaje = "El usuario se ha modificado correctamente";
            String caption = "Se ha modificado el usuario";
            MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            ABM_Usuario.Form3 form3 = new ABM_Usuario.Form3();
            this.Close();
            form3.Show();

            ABM_Usuario.Form7 form7 = new ABM_Usuario.Form7(user,0);
            form7.Close();

            
        }



        private void validarCamposCliente()
        {



            con.Open();
            existeDni = new SqlCommand("PERSISTIENDO.existeDni", con);
            existeDni.CommandType = CommandType.StoredProcedure;
            existeDni.Parameters.Add("@Dni", SqlDbType.Float).Value = textBox4.Text;
            var result = existeDni.Parameters.Add("@Valor", SqlDbType.Int);
            result.Direction = ParameterDirection.ReturnValue;
            data = existeDni.ExecuteReader();
            var existeDoc = result.Value;
            data.Close();
            con.Close();


            if ((int)existeDoc == 1 && (docu != (float)Convert.ToDouble(textBox4.Text)))
            {

                String mensaje = "El dni ya ha sido registrado";
                String caption = "Error al ingresar datos";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                textBox4.Text = "";

                
            }



            else {

                modificarCliente();
            }
               


        

}

        private void button2_Click(object sender, EventArgs e)
        {
            ABM_Usuario.Form3 form3 = new ABM_Usuario.Form3();
            this.Close();
            form3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
                
                                      

                  DialogResult dialogResult = MessageBox.Show("El usuario se encuentra inhabilitado, deseea desbloquearlo?", "Habilitar usuario", MessageBoxButtons.YesNo);
                  if (dialogResult == DialogResult.Yes)
                  {
                      con.Open();
                      desbloquearUsuario = new SqlCommand("PERSISTIENDO.desbloquearUsuario", con);

                      desbloquearUsuario.CommandType = CommandType.StoredProcedure;
                      desbloquearUsuario.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;

                      desbloquearUsuario.ExecuteNonQuery();
                      con.Close();
                  }



              
                
               
                
                }

        private void button5_Click(object sender, EventArgs e)
        {
            ABM_Usuario.Form7 form7 = new ABM_Usuario.Form7(user,1);
            form7.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

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

        private void textBox8_TextChanged(object sender, EventArgs e)
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
                textBox8.Text = "";
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
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
                textBox9.Text = "";
            }
        }



    }
    }

