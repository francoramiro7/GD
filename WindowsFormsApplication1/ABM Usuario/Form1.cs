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

namespace WindowsFormsApplication1.ABM_Usuario
{

    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataReader data;
        SqlCommand rubros, existeUsuario;
        int valido;


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
            label20.Visible = true;
            label21.Visible = true;

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
            

            //cargar rubros

            rubros = new SqlCommand("PERSISTIENDO.listarRubros", con);
            rubros.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(rubros);
            DataTable tablaRubros = new DataTable();
            adapter.Fill(tablaRubros);
            comboBox3.DataSource = tablaRubros;
            comboBox3.DisplayMember = "Rubro_Descripcion";
            comboBox3.Text = "Seleccione rubro";

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
         
            //verificar que no exista usuario

            existeUsuario = new SqlCommand("PERSISTIENDO.existeUsuario", con);
            existeUsuario.CommandType = CommandType.StoredProcedure;
            existeUsuario.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
            var resultado = existeUsuario.Parameters.Add("@Valor", SqlDbType.Int);
            resultado.Direction = ParameterDirection.ReturnValue;
            data = existeUsuario.ExecuteReader();

            var existe = resultado.Value;
            data.Close();

            if ((int)existe == 1)
            {
                textBox1.Text = "Usuario existente, intente nuevamente";
            }
            //verificar campos obligatorios
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Text = "Este campo es obligatorio";
            }
        }


      

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        

       

        


    }

}
