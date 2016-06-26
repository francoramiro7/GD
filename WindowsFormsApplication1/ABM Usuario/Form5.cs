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
    public partial class Form5 : Form
    {
        int validacionEmpresa = 0;
        SqlConnection con;
        SqlDataReader data;
        SqlCommand rubros, existeCuit, existeRazon, cod, modificar, habilitado, bloquearUsuario, desbloquearUsuario;
        String user;
        String razonSocial;
        String cuitG;
        public Form5(String username, String razon, String mail, String telefono, String cuit, String calle, String nro, String piso,
                String depto, String cp, String loca, String nombre, String rubro, String ciudad)
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            user = username;
            cuitG = cuit;
            razonSocial = razon;
            textBox1.Text = username;
            textBox12.Text = razon;
            textBox2.Text = mail;
            textBox3.Text = telefono;
            textBox4.Text = cuit;
            textBox7.Text = calle;
            textBox8.Text = nro;
            textBox9.Text = piso;
            textBox10.Text = depto;
            textBox11.Text = cp;
            textBox6.Text = loca;
            textBox5.Text = ciudad;
            textBox13.Text = nombre;
            int habilitado = estaHabilitado(user);
            if (habilitado == 0)
                button3.Visible = true;

            con.Open();
            rubros = new SqlCommand("PERSISTIENDO.listarRubros", con);
            rubros.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(rubros);
            DataTable tablaRubros = new DataTable();
            adapter.Fill(tablaRubros);
            comboBox3.DataSource = tablaRubros;
            comboBox3.DisplayMember = "Rubro_Descripcion";
            comboBox3.SelectedIndex = comboBox3.Items.IndexOf("New");
            comboBox3.Text = "";
            if (!(string.IsNullOrEmpty(rubro)))
            {
                comboBox3.Text = rubro;
            }
           
            con.Close();
            
           



        }

        private void Form5_Load(object sender, EventArgs e)
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


        private bool esCuit(String ingresado, bool tieneComa)
        {

            char[] ingre = ingresado.ToCharArray();
            int comas = 0;
            for (int i = 0; i < ingresado.Length; i++)
            {

                if (ingre[0].Equals('-'))
                {
                    return false;
                }

                if (!char.IsNumber(ingre[i]))
                {
                    if (tieneComa)
                    {
                        if ((!ingre[i].Equals('-')) || (comas > 1))
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
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox10.Text = string.Empty;
            textBox11.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox13.Text = string.Empty;
            comboBox3.Text = "Seleccione Rubro";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ABM_Usuario.Form3 form3 = new ABM_Usuario.Form3();
            this.Close();
            form3.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            validarVaciosEmpresa();
        }

        private void validarVaciosEmpresa()
        {
            if (string.IsNullOrEmpty(textBox2.Text) | string.IsNullOrEmpty(textBox12.Text) |
              string.IsNullOrEmpty(textBox3.Text) | string.IsNullOrEmpty(textBox4.Text) | string.IsNullOrEmpty(textBox7.Text) |
                string.IsNullOrEmpty(textBox8.Text) | string.IsNullOrEmpty(textBox11.Text) | string.IsNullOrEmpty(textBox6.Text) |
                string.IsNullOrEmpty(textBox5.Text) | string.IsNullOrEmpty(textBox13.Text) | comboBox3.Text.Equals("Seleccione Rubro"))
            {

                String mensaje = "Los campos (*) son obligatorios";
                String caption = "Error al crear usuario";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
            else
            {

                validarCamposEmpresa();
            }
        }

        private void validarCamposEmpresa()
        {

            con.Open();
            existeCuit = new SqlCommand("PERSISTIENDO.existeCuit", con);
            existeCuit.CommandType = CommandType.StoredProcedure;
            existeCuit.Parameters.Add("@Cuit", SqlDbType.VarChar).Value = textBox4.Text;
            var resultado = existeCuit.Parameters.Add("@Valor", SqlDbType.Int);
            resultado.Direction = ParameterDirection.ReturnValue;
            data = existeCuit.ExecuteReader();
            var existeC = resultado.Value;
            data.Close();

            existeRazon = new SqlCommand("PERSISTIENDO.existeRazon", con);
            existeRazon.CommandType = CommandType.StoredProcedure;
            existeRazon.Parameters.Add("@Razon", SqlDbType.VarChar).Value = textBox12.Text;
            var resul = existeRazon.Parameters.Add("@Valor", SqlDbType.Int);
            resul.Direction = ParameterDirection.ReturnValue;
            data = existeRazon.ExecuteReader();
            var existeR = resul.Value;
            data.Close();
            con.Close();

            if ((int)existeC == 1 && cuitG != textBox4.Text)
            {

                String mensaje = "El cuit ha ya sido registrado";
                String caption = "Error al modificar";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);


            }
            else if ((int)existeR == 1 && razonSocial != textBox12.Text)
            {

                String mensaje = "La razon social ya ha sido registrada";
                String caption = "Error al modificar";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
            else

                modificarEmpresa();

       
            
        }



        private void modificarEmpresa()
        {
            con.Open();
            cod = new SqlCommand("PERSISTIENDO.codigoRubro", con);
            cod.CommandType = CommandType.StoredProcedure;
            cod.Parameters.Add("@Rubro", SqlDbType.VarChar).Value = comboBox3.Text.ToString();

            var result = cod.Parameters.Add("@Valor", SqlDbType.Int);
            result.Direction = ParameterDirection.ReturnValue;
            data = cod.ExecuteReader();
            data.Close();
            var codigoRubro = result.Value;

            modificar = new SqlCommand("PERSISTIENDO.updateEmpresa", con);
            modificar.CommandType = CommandType.StoredProcedure;

            modificar.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
            modificar.Parameters.Add("@razon", SqlDbType.VarChar).Value = textBox12.Text;
            modificar.Parameters.Add("@cuil", SqlDbType.VarChar).Value = textBox4.Text;
            modificar.Parameters.Add("@mail", SqlDbType.VarChar).Value = textBox2.Text;
            modificar.Parameters.Add("@calle", SqlDbType.VarChar).Value = textBox7.Text;
            modificar.Parameters.Add("@cp", SqlDbType.VarChar).Value = textBox11.Text;
            modificar.Parameters.Add("@depto", SqlDbType.VarChar).Value = textBox10.Text;
            if (String.IsNullOrEmpty(textBox9.Text))
                modificar.Parameters.Add("@piso", SqlDbType.Float).Value = DBNull.Value;
            else
                modificar.Parameters.Add("@piso", SqlDbType.Float).Value = float.Parse(textBox9.Text, CultureInfo.InvariantCulture.NumberFormat);

            modificar.Parameters.Add("@rubro", SqlDbType.Int).Value = (int)codigoRubro;
            modificar.Parameters.Add("@nro", SqlDbType.Float).Value = float.Parse(textBox8.Text, CultureInfo.InvariantCulture.NumberFormat);
            modificar.Parameters.Add("@localidad", SqlDbType.VarChar).Value = textBox6.Text;
            modificar.Parameters.Add("@ciudad", SqlDbType.VarChar).Value = textBox5.Text;
            modificar.Parameters.Add("@tel", SqlDbType.Float).Value = float.Parse(textBox3.Text, CultureInfo.InvariantCulture.NumberFormat);
            modificar.Parameters.Add("@contacto", SqlDbType.VarChar).Value = textBox13.Text;
            con.Close();

            String mensaje = "La empresa se ha modificado correctamente";
            String caption = "Empresa modificada";
            MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            ABM_Usuario.Form2 frm2 = new ABM_Usuario.Form2();
            frm2.Show();
            this.Close();






        }

        private void textBox3_TextChanged(object sender, EventArgs e)
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
                textBox3.Text = "";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            String ingresado = ((TextBox)sender).Text;
            if (esCuit(ingresado, true))
            {
            }
            else
            {
                String mensaje = "Solo se pueden ingresar numeros y hasta 3 '-' en este campos";
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
            ABM_Usuario.Form7 frm7 = new ABM_Usuario.Form7(user,2);
            frm7.Show();
        }
    }
}
