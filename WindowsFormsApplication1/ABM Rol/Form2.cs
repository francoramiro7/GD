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


namespace WindowsFormsApplication1.ABM_Rol
{
    public partial class Form2 : Form
    {
        SqlConnection coneccion;
        SqlCommand cargarFunc, crearRol, codigoRol, crearFunc, existeRol;
        List<String> funcion = new List<String>();
        SqlDataReader data;

        public Form2()
        {
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cargarFunc = new SqlCommand("PERSISTIENDO.listarFuncionalidades", coneccion);

            cargarFunc.CommandType = CommandType.StoredProcedure;
            
            SqlDataAdapter adapter = new SqlDataAdapter(cargarFunc);
            DataTable tablaRoles = new DataTable();

            adapter.Fill(tablaRoles);
            SqlDataReader reader = cargarFunc.ExecuteReader();

            listBox1.DataSource = tablaRoles;
            listBox1.DisplayMember = "Func_nombre";
            coneccion.Close();
           
            
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); 
            ABM_Rol.Form1 accionesRol = new ABM_Rol.Form1();
            accionesRol.Show();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void validarCampos()
        {
            if (string.IsNullOrEmpty(textBox1.Text) || (int)listBox2.Items.Count == 0)
            {
                String mensaje = "Los campos nombre y funcionalidades son obligatorios";
                String caption = "Error al crear el rol";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
               else
            {
                coneccion.Open();
                existeRol = new SqlCommand("PERSISTIENDO.existeRol", coneccion);
                existeRol.CommandType = CommandType.StoredProcedure;
                existeRol.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox1.Text;
                var resultado = existeRol.Parameters.Add("@Valor", SqlDbType.Int);
                resultado.Direction = ParameterDirection.ReturnValue;
                data = existeRol.ExecuteReader();
                var existeR = resultado.Value;
                data.Close();
                coneccion.Close();

                if ((int)existeR == 1)
                {
                    String mensaje = "El rol ya exisste, ingrese otro nombre";
                    String caption = "Error al crear el rol";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                }
                else
                    crearNuevoRol();
            }


                
             }
            

        private void button2_Click(object sender, EventArgs e)
        {
            validarCampos();
        }
                      
                  

        private void crearNuevoRol(){
        
                coneccion.Open();
                crearRol = new SqlCommand("PERSISTIENDO.crearRolNuevo", coneccion);
                crearRol.CommandType = CommandType.StoredProcedure;
                crearRol.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox1.Text;
                crearRol.ExecuteNonQuery();
                

                codigoRol = new SqlCommand("PERSISTIENDO.codigoRol", coneccion);
                codigoRol.CommandType = CommandType.StoredProcedure;
                codigoRol.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox1.Text;
                var resultado = codigoRol.Parameters.Add("@Valor", SqlDbType.Int);
                resultado.Direction = ParameterDirection.ReturnValue;
                data = codigoRol.ExecuteReader();
                coneccion.Close();
                
                var codi = resultado.Value;
                int rol = (int)codi;

                crearFuncionalidades();
        
        }

        private void crearFuncionalidades()
        {

        }


      

        private void button3_Click(object sender, EventArgs e)
        {
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.Remove(listBox2.SelectedItem);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);

            if (funcion.Contains(text))
            {

                String mensaje = "Esta funcionalidad ya ha sido ingresada";
                String caption = "Funcionalidad duplicada";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
            else
            {

                listBox2.DisplayMember = "Func_nombre";
                listBox2.Items.Add((DataRowView)listBox1.SelectedItem);

                funcion.Add(text);

            }
        }


    

}
}
