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

    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataReader data;
        SqlCommand rubros, cod, existeUsuario, existeDni, existeRazon, existeCuit, crearUsuario, crearCliente, crearEmpresa, crearRol;
        int validacionCliente = 0;
        int validacionEmpresa = 0;
        Boolean ingresonum = false;
        Boolean ingresonumNombre = false;
        Boolean ingresoLetraDni = false;
        Boolean ingresoletraNro = false;
        Boolean ingresoletraPiso = false;
        Boolean ingresoLetraCp = false;
        Boolean cambioFecha = true;
        
       


        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            con.Open();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }




        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

       

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Cliente")
            {
                crearCamposCliente();
            }

            if (comboBox2.Text == "Empresa")
            {
                crearCamposEmp();
            }

        }

        private void crearCamposCliente()
        {
            label3.Visible = false;
            comboBox2.Visible = false;
            button1.Visible = false;
            label4.Visible = true;
            label5.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            label20.Visible = true;
            textBox18.Visible = true;
            button4.Visible = true;
            dateTimePicker1.Text = "";
            

            textBox3.Visible = true; //nommbre
            textBox4.Visible = true; //apellido
            textBox5.Visible = true; //dni
            textBox6.Visible = true; //localida
            textBox7.Visible = true; //calle
            textBox8.Visible = true; //nro
            textBox9.Visible = true; //piso
            textBox10.Visible = true; //dpt
            textBox11.Visible = true; //cp
            comboBox1.Visible = true;
            comboBox1.Text = "Seleccione Tipo de Documento";
            dateTimePicker1.Visible = true;
            button3.Visible = true;
           

        }

        private void crearCamposEmp()
        {

            button4.Visible = true;
            label3.Visible = false;
            comboBox2.Visible = false;
            button1.Visible = false;
            label16.Visible = true;
            label17.Visible = true;
            label18.Visible = true;
            label19.Visible = true;
            label19.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label13.Visible = true;
            label12.Visible = true;
            label14.Visible = true;
            label21.Visible = true;
            label23.Visible = true;

            textBox12.Visible = true; //razon social
            textBox13.Visible = true; //mail
            textBox14.Visible = true; //tel
            textBox15.Visible = true; //cuit
            textBox7.Visible = true;
            textBox8.Visible = true;
            textBox4.Visible = true;
            textBox9.Visible = true;
            textBox10.Visible = true;
            textBox11.Visible = true;
            textBox6.Visible = true;
            textBox16.Visible = true;
            label9.Visible = true;
            comboBox3.Visible = true;
            label22.Visible = true;
            button3.Visible = true;
            textBox17.Visible = true;
            

            //cargar rubros

            rubros = new SqlCommand("PERSISTIENDO.listarRubros", con);
            rubros.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(rubros);
            DataTable tablaRubros = new DataTable();
            adapter.Fill(tablaRubros);
            comboBox3.DataSource = tablaRubros;
            comboBox3.DisplayMember = "Rubro_Descripcion";
            comboBox3.Text = "Seleccione Rubro";

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ABM_Usuario.Form2 form2 = new Form2();
            form2.Show();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
         
                    if(comboBox2.Text.Equals("Cliente")){
                     
                        validarVaciosCliente();

                      

                    }else{
                        validarVaciosEmpresa();

                        

                    }
           

            

        }


        private void nuevaEmpresa()
        {

            crearUsuario = new SqlCommand("PERSISTIENDO.crearUsuario", con);
            crearUsuario.CommandType = CommandType.StoredProcedure;
            crearUsuario.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
            crearUsuario.Parameters.Add("@Password", SqlDbType.VarChar).Value = textBox2.Text;
            crearUsuario.ExecuteNonQuery();


            cod = new SqlCommand("PERSISTIENDO.codigoRubro", con);
            cod.CommandType = CommandType.StoredProcedure;
            cod.Parameters.Add("@Rubro", SqlDbType.VarChar).Value = comboBox3.Text.ToString();

            var result= cod.Parameters.Add("@Valor", SqlDbType.Int);
            result.Direction = ParameterDirection.ReturnValue;
            data = cod.ExecuteReader();
            data.Close();
            var codigoRubro = result.Value;

            crearEmpresa = new SqlCommand("PERSISTIENDO.crearEmpresa", con);
            crearEmpresa.CommandType = CommandType.StoredProcedure;

            crearEmpresa.Parameters.Add("@user", SqlDbType.VarChar).Value = textBox1.Text;
            crearEmpresa.Parameters.Add("@razon", SqlDbType.VarChar).Value = textBox12.Text;
            crearEmpresa.Parameters.Add("@cuit", SqlDbType.VarChar).Value = textBox15.Text;
            crearEmpresa.Parameters.Add("@creacion", SqlDbType.DateTime).Value = Properties.Settings.Default.fecha;
            crearEmpresa.Parameters.Add("@mail", SqlDbType.VarChar).Value = textBox13.Text;
            crearEmpresa.Parameters.Add("@calle", SqlDbType.VarChar).Value = textBox7.Text;
            crearEmpresa.Parameters.Add("@num", SqlDbType.Float).Value = float.Parse(textBox8.Text, CultureInfo.InvariantCulture.NumberFormat);
            if (String.IsNullOrEmpty(textBox9.Text))
                crearEmpresa.Parameters.Add("@piso", SqlDbType.Float).Value = DBNull.Value;
            else
                crearEmpresa.Parameters.Add("@piso", SqlDbType.Float).Value = float.Parse(textBox9.Text, CultureInfo.InvariantCulture.NumberFormat);

            crearEmpresa.Parameters.Add("@depto", SqlDbType.VarChar).Value = textBox10.Text;
            crearEmpresa.Parameters.Add("@cp", SqlDbType.VarChar).Value = textBox11.Text;
            crearEmpresa.Parameters.Add("@rubro", SqlDbType.Int).Value = (int)codigoRubro;
            crearEmpresa.Parameters.Add("@contacto", SqlDbType.VarChar).Value = textBox16.Text;
            crearEmpresa.Parameters.Add("@localidad", SqlDbType.VarChar).Value = textBox6.Text;
            crearEmpresa.Parameters.Add("@ciudad", SqlDbType.VarChar).Value = textBox17.Text;
            crearEmpresa.Parameters.Add("@tel", SqlDbType.Float).Value = float.Parse(textBox14.Text, CultureInfo.InvariantCulture.NumberFormat);
            crearEmpresa.ExecuteNonQuery();


            crearRol = new SqlCommand("PERSISTIENDO.crearRol", con);
            crearRol.CommandType = CommandType.StoredProcedure;
            crearRol.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
            crearRol.Parameters.Add("@Rol", SqlDbType.Int).Value = 2;

            crearRol.ExecuteNonQuery();

            String mensaje = "El usuario se ha creado correctamente";
                        String caption = "Usuario creado";
                        MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                        ABM_Usuario.Form2 form2 = new Form2();
                        form2.Show();
                        this.Close();



        }



        private void nuevoCliente (){
        
             crearUsuario = new SqlCommand("PERSISTIENDO.crearUsuario", con);
                        crearUsuario.CommandType = CommandType.StoredProcedure;

                        crearUsuario.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
                        crearUsuario.Parameters.Add("@Password", SqlDbType.VarChar).Value = textBox2.Text;
                        crearUsuario.ExecuteNonQuery();

                        crearCliente = new SqlCommand("PERSISTIENDO.crearCliente", con);
                        crearCliente.CommandType = CommandType.StoredProcedure;

                        crearCliente.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
                        crearCliente.Parameters.Add("@apellido", SqlDbType.VarChar).Value = textBox4.Text;
                        crearCliente.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox3.Text;
                        crearCliente.Parameters.Add("@mail", SqlDbType.VarChar).Value = textBox18.Text;
                        crearCliente.Parameters.Add("@calle", SqlDbType.VarChar).Value = textBox7.Text;
                        crearCliente.Parameters.Add("@cp", SqlDbType.Float).Value = float.Parse(textBox11.Text, CultureInfo.InvariantCulture.NumberFormat);
                        crearCliente.Parameters.Add("@depto", SqlDbType.VarChar).Value = textBox10.Text;
                        if (String.IsNullOrEmpty(textBox9.Text))
                            crearCliente.Parameters.Add("@piso", SqlDbType.Float).Value = DBNull.Value;
                        else
                            crearCliente.Parameters.Add("@piso", SqlDbType.Float).Value = float.Parse(textBox9.Text, CultureInfo.InvariantCulture.NumberFormat);

                        crearCliente.Parameters.Add("@dni", SqlDbType.Float).Value = float.Parse(textBox5.Text, CultureInfo.InvariantCulture.NumberFormat);
                        crearCliente.Parameters.Add("@tipo", SqlDbType.VarChar).Value = comboBox1.SelectedItem.ToString();
                        crearCliente.Parameters.Add("@nro", SqlDbType.Float).Value = float.Parse(textBox8.Text, CultureInfo.InvariantCulture.NumberFormat);
                        crearCliente.Parameters.Add("@fechanac", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                        crearCliente.Parameters.Add("@fechacreac", SqlDbType.DateTime).Value = Properties.Settings.Default.fecha;
                        crearCliente.Parameters.Add("@localidad", SqlDbType.VarChar).Value = textBox6.Text;
                        crearCliente.ExecuteNonQuery();

                        crearRol = new SqlCommand("PERSISTIENDO.crearRol", con);
                        crearRol.CommandType = CommandType.StoredProcedure;
                        crearRol.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
                        crearRol.Parameters.Add("@Rol", SqlDbType.Int).Value = 1;

                        crearRol.ExecuteNonQuery();


                        String mensaje = "El usuario se ha creado correctamente";
                        String caption = "Usuario creado";
                        MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                        ABM_Usuario.Form2 form2 = new Form2();
                        form2.Show();
                        this.Close();
        
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void validarCamposEmpresa() {

            existeCuit = new SqlCommand("PERSISTIENDO.existeCuit", con);
            existeCuit.CommandType = CommandType.StoredProcedure;
            existeCuit.Parameters.Add("@Cuit", SqlDbType.VarChar).Value = textBox15.Text;
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

            existeUsuario = new SqlCommand("PERSISTIENDO.existeUsuario", con);
            existeUsuario.CommandType = CommandType.StoredProcedure;
            existeUsuario.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
            var resultado2 = existeUsuario.Parameters.Add("@Valor", SqlDbType.Int);
            resultado2.Direction = ParameterDirection.ReturnValue;
            data = existeUsuario.ExecuteReader();
            var existeU = resultado2.Value;
            data.Close();

            if ((int)existeU == 1)
            {

                 String mensaje = "El usuario ya existe";
                String caption = "Error al crear usuario";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
            else if ((int)existeC == 1)
            {

                String mensaje = "El CUIT ingresado ya ha sido registrado";
                String caption = "Error al crear usuario";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
            else if ((int)existeR == 1)
            {
                String mensaje = "La razon social ingresada ya esta registrada";
                String caption = "Error al crear usuario";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
            else
            {
                nuevaEmpresa();
            }
           

        
        }

        private void validarCamposCliente()
        {
           
            //se fija si existe el usuario
            existeUsuario = new SqlCommand("PERSISTIENDO.existeUsuario", con);
            existeUsuario.CommandType = CommandType.StoredProcedure;
            existeUsuario.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
            var resultado = existeUsuario.Parameters.Add("@Valor", SqlDbType.Int);
            resultado.Direction = ParameterDirection.ReturnValue;
            data = existeUsuario.ExecuteReader();
            var existe = resultado.Value;
            data.Close();


            existeDni = new SqlCommand("PERSISTIENDO.existeDni", con);
            existeDni.CommandType = CommandType.StoredProcedure;
            existeDni.Parameters.Add("@Dni", SqlDbType.Float).Value = textBox5.Text;
            var result= existeDni.Parameters.Add("@Valor", SqlDbType.Int);
            result.Direction = ParameterDirection.ReturnValue;
            data = existeDni.ExecuteReader();
            var existeDoc = result.Value;
            data.Close();


            if ((int)existe == 1)
            {

                String mensaje = "El usuario ya existe";
                String caption = "Error al crear usuario";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
            }
            else if ((int)existeDoc == 1)
            {
                String mensaje = "El DNI ingresado ya se ha registrado en el sistema";
                String caption = "Error al crear usuario";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
            }
            else {

                nuevoCliente();
            
            }

            
            

            

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }


        private void validarVaciosCliente()
        {
            if (string.IsNullOrEmpty(textBox1.Text) | string.IsNullOrEmpty(textBox2.Text) | string.IsNullOrEmpty(textBox3.Text) |
              string.IsNullOrEmpty(textBox4.Text) | string.IsNullOrEmpty(textBox5.Text) | string.IsNullOrEmpty(textBox5.Text) |
                string.IsNullOrEmpty(textBox7.Text) | string.IsNullOrEmpty(textBox8.Text) | string.IsNullOrEmpty(textBox11.Text) |
                string.IsNullOrEmpty(textBox6.Text) | string.IsNullOrEmpty(textBox18.Text) |
                comboBox1.SelectedIndex == -1 | (cambioFecha))
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

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
           
        }

        private void validarVaciosEmpresa()
        {
            if (string.IsNullOrEmpty(textBox1.Text) | string.IsNullOrEmpty(textBox2.Text) | string.IsNullOrEmpty(textBox12.Text) |
              string.IsNullOrEmpty(textBox13.Text) | string.IsNullOrEmpty(textBox14.Text) | string.IsNullOrEmpty(textBox15.Text) |
                string.IsNullOrEmpty(textBox7.Text) | string.IsNullOrEmpty(textBox8.Text) | string.IsNullOrEmpty(textBox11.Text) |
                string.IsNullOrEmpty(textBox6.Text) | string.IsNullOrEmpty(textBox16.Text) | string.IsNullOrEmpty(textBox17.Text) |
                comboBox3.Text.Equals("Seleccione Rubro")) 
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

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
         

        }

        private void textBox14_LocationChanged(object sender, EventArgs e)
        {

        }

        

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)))
            {
            }
            else
            {
                ingresoLetraCp = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
        
            if (!(char.IsNumber(e.KeyChar)))
            {
            }
            else
            {
                ingresoLetraDni = true;
            }

        
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
            }
            else
            {
                ingresonumNombre = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)))
            {
            }
            else
            {
                ingresoletraNro = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)))
            {
            }
            else
            {
                ingresoletraPiso = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (comboBox2.Text.Equals("Cliente"))
            {

                limpiarCliente();
            }
            else
            {
                limpiarEmpresa();
            }




        }


        private void limpiarCliente()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox18.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void limpiarEmpresa()
        {

            textBox1.Clear();
            textBox2.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox4.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox6.Clear();
            textBox16.Clear();
            textBox17.Clear();
            comboBox3.SelectedIndex = -1;


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            cambioFecha = false;
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            cambioFecha = false;
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            cambioFecha = false;
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


        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
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
                textBox5.Text = "";
            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
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
                textBox14.Text = "";
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

        private void textBox15_TextChanged(object sender, EventArgs e)
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
                textBox15.Text = "";

            }
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
       {
            
           


        }



       

        
        

       

        


    }

}
