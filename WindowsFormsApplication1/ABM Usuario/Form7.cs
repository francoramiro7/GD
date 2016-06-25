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
        SqlCommand cargarRoles, rolDeUsuario, borrarRoles, agregarRoles, codigosRol, habilitado;
        String usuario;
        List<String> funcionalidades = new List<String>();

        public Form7(String username, int tipo)
            
            
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


            if (tipo == 1)
            {
                roles.Remove("Empresa");
            }

            if (tipo == 2) {
                roles.Remove("Cliente");
            }
            
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
            int habilitado = estaHabilitado(text);
            if (funcionalidades.Contains(text))
            {

                String mensaje = "Este rol ya ha sido ingresado";
                String caption = "Rol duplicada";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
            else if (habilitado == 0) {
                String mensaje = "No es posible agregar un rol inhabilitado a un usuario";
                String caption = "Rol dinhabilitad";
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


        private int estaHabilitado(String rol)
        {
            coneccion.Open();
            habilitado = new SqlCommand("PERSISTIENDO.rolHabilitado", coneccion);
            habilitado.CommandType = CommandType.StoredProcedure;
            habilitado.Parameters.Add("@nombre", SqlDbType.VarChar).Value = rol;
            var resultado = habilitado.Parameters.Add("@Valor", SqlDbType.Bit);
            resultado.Direction = ParameterDirection.ReturnValue;
            data = habilitado.ExecuteReader();
            var habi = resultado.Value;
            int respuesta = (int)habi;
            coneccion.Close();
            data.Close();
            return respuesta;
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
                coneccion.Close();

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
                    int cod = (int)codigo;
                    codigos.Add(cod);
                    data.Close();
                    coneccion.Close();

                }

                for (int i = 0; i < codigos.Count(); i++)
                {

                    coneccion.Open();
                    agregarRoles = new SqlCommand("PERSISTIENDO.crearRol", coneccion);
                    agregarRoles.CommandType = CommandType.StoredProcedure;
                    agregarRoles.Parameters.Add("@Username", SqlDbType.VarChar).Value = usuario;
                    agregarRoles.Parameters.Add("@rol", SqlDbType.Int).Value = codigos.ElementAt(i);
                    agregarRoles.ExecuteNonQuery();
                    coneccion.Close();

                }

                String mensaje = "Los roles del usuario se han modificado correctamente";
                String caption = "Roles modificados";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

                this.Close();

                


                
            }




        }
    }
}
