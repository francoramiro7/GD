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
        SqlCommand rubros, existeCuit, existeRazon;
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

            con.Open();
            rubros = new SqlCommand("PERSISTIENDO.listarRubros", con);
            rubros.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(rubros);
            DataTable tablaRubros = new DataTable();
            adapter.Fill(tablaRubros);
            comboBox3.DataSource = tablaRubros;
            comboBox3.DisplayMember = "Rubro_Descripcion";
            comboBox3.Text = rubro;
            con.Close();




        }

        private void Form5_Load(object sender, EventArgs e)
        {

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

            if (textBox12.Text.Length > 255)
                textBox12.Text = "Ha superado el limite de caracteres";
            else
                validacionEmpresa++;

            if (textBox13.Text.Length > 50)
                textBox13.Text = "Ha superado el limite de caracteres";
            else
                validacionEmpresa++;


            if (textBox3.Text.Length > 18)
            {
                textBox3.Text = "Ha superado el limite de caracteres";
            }
            else if (textBox3.Text.Any(char.IsLetter))
            {
                textBox3.Text = "No se permiten caracteres alfanumericos";
            }
            else
                validacionEmpresa++;


            if (textBox4.Text.Length > 50)
            {
                textBox4.Text = "Ha superado el limite de caracteres";
            }
            else if (textBox4.Text.Any(char.IsLetter))
            {
                textBox4.Text = "No se permiten caracteres alfanumericos";
            }
            else
                validacionEmpresa++;

            if (textBox7.Text.Length > 100)
                textBox7.Text = "Ha superado el limite de caracteres";
            else
                validacionEmpresa++;

            if (textBox8.Text.Length > 18)
            {
                textBox8.Text = "Ha superado el limite de caracteres";
            }
            else if (textBox8.Text.Any(char.IsLetter))
            {
                textBox8.Text = "No se permiten caracteres alfanumericos";
            }
            else
                validacionEmpresa++;

            if (textBox9.Text.Length > 18)
            {
                textBox9.Text = "Ha superado el limite de caracteres";
            }
            else if (textBox9.Text.Any(char.IsLetter))
            {
                textBox9.Text = "No se permiten caracteres alfanumericos";
            }
            else
                validacionEmpresa++;

            if (textBox11.Text.Length > 50)
                textBox11.Text = "Ha superado el limite de caracteres";
            else
                validacionEmpresa++;

            if (textBox5.Text.Length > 255)
                textBox5.Text = "Ha superado el limite de caracteres";
            else
                validacionEmpresa++;

            if (textBox6.Text.Length > 255)
                textBox6.Text = "Ha superado el limite de caracteres";
            else
                validacionEmpresa++;

            if (textBox13.Text.Length > 255)
                textBox13.Text = "Ha superado el limite de caracteres";
            else
                validacionEmpresa++;

            if (validacionEmpresa == 12)
            {

            }
            else
            {

                String mensaje = "Por favor, corrija los campos indicados";
                String caption = "Error al crear usuario";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                validacionEmpresa = 0;

            }


            
        }
    }
}
