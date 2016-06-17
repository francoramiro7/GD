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
    public partial class Form7 : Form
    {

         SqlConnection coneccion;
        SqlDataReader data;
        SqlCommand cargarRoles, rolDeUsuario, borrarRoles, agregarRoles, codigosRol;
        String usuario;
        List<String> funcionalidades = new List<String>();

        public Form7(String username)
            
            
        {
            usuario = username;
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
            cargarRoles = new SqlCommand("PERSISTIENDO.cargarRoles", coneccion);

            cargarRoles.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cargarRoles);
            SqlDataReader dat = cargarRoles.ExecuteReader();
            List<String> roles = new List<String>();

            while (dat.Read())
            {
                roles.Add(dat.GetString(0));
            }

            roles.Remove("Empresa");
            comboBox1.DataSource = roles;
            dat.Close();
            comboBox1.DisplayMember = "Rol_nombre";
            

            rolDeUsuario = new SqlCommand("PERSISTIENDO.rolPorUsuario", coneccion);
            rolDeUsuario.Parameters.Add("@Username", SqlDbType.VarChar).Value = usuario;

            rolDeUsuario.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter2 = new SqlDataAdapter(rolDeUsuario);
            SqlDataReader reader = rolDeUsuario.ExecuteReader();



            while (reader.Read())
            {
                funcionalidades.Add(reader.GetString(0));  
            }



            listBox1.Items.AddRange(funcionalidades.ToArray());
            reader.Close();

            listBox1.DisplayMember = "Rol_nombre";



            coneccion.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string text = comboBox1.Text.ToString();

            if (funcionalidades.Contains(text))
            {

                String mensaje = "Esta funcionalidad ya ha sido ingresada";
                String caption = "Funcionalidad duplicada";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
            else
            {

                
                listBox1.Items.Add(text);

                funcionalidades.Add(text);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);
            listBox1.Items.Remove(listBox1.SelectedItem);

            funcionalidades.Remove(text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (funcionalidades.Count == 0)
            {
                coneccion.Open();
                borrarRoles = new SqlCommand("PERSISTIENDO.borrarRoles", coneccion);
                borrarRoles.Parameters.Add("@Username", SqlDbType.VarChar).Value = usuario;
                borrarRoles.CommandType = CommandType.StoredProcedure;
                borrarRoles.ExecuteNonQuery();
                coneccion.Close();



            }
            else
            {
                coneccion.Open();
                borrarRoles = new SqlCommand("PERSISTIENDO.borrarRoles", coneccion);
                borrarRoles.Parameters.Add("@Username", SqlDbType.VarChar).Value = usuario;
                borrarRoles.CommandType = CommandType.StoredProcedure;
                borrarRoles.ExecuteNonQuery();

                List<int> codigos = new List<int>();

                for (int i = 0; i < funcionalidades.Count(); i++)
                {
                    coneccion.Open();
                    codigosRol = new SqlCommand("PERSISTIENDO.codigoRol", coneccion);
                    codigosRol.CommandType = CommandType.StoredProcedure;
                    codigosRol.Parameters.Add("@nombre", SqlDbType.VarChar).Value = funcionalidades.ElementAt(i).ToString();
                    var resultado = codigosRol.Parameters.Add("@Valor", SqlDbType.Int);
                    resultado.Direction = ParameterDirection.ReturnValue;
                    data = codigosRol.ExecuteReader();
                    var codigo = resultado.Value;
                    int aniadir = (int)codigo;
                    codigos.Add(aniadir);
                    data.Close();
                    coneccion.Close();

                }

                //ESTABAMOS POR ACA


                
            }




        }
    }
}
