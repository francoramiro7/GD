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

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        string rol;

        SqlConnection coneccion;
        SqlCommand cargarfun;
        public Form2()
        {
            rol = usuario.Rol;
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            this.Text = rol;
            cargarfun = new SqlCommand("PERSISTIENDO.FuncionalidadesPorRol", coneccion);
            cargarfun.Parameters.Add("@Rol", SqlDbType.VarChar).Value = rol;
            cargarfun.CommandType = CommandType.StoredProcedure;
            
            SqlDataReader reader = cargarfun.ExecuteReader();
         List<string> funcionalidades = new List<string>();
                    int i=0;
                         while (reader.Read())
                         {
                                funcionalidades.Add( reader.GetValue(0).ToString() );
                          }
                      reader.Close();

                     // crearBotones(funcionalidades);


        
        }



        private void crearBotones(List<String>func)
        {
            List<Button> botones = new List<Button>();
            botones.Add(button10);
            botones.Add(button11);
            botones.Add(button12);
            botones.Add(button13);

            int size = func.Count();
            for (int i = 0; i < size; i++)
            {

                String nombreBoton = func[i];
                botones[i].Text = nombreBoton;


            }

        }








        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ABM_Usuario.Form2 abmUser = new ABM_Usuario.Form2();
            abmUser.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ABM_Rol.Form1 abmRol = new ABM_Rol.Form1();
            abmRol.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Generar_Publicación.Form2 generar_Publicacion = new Generar_Publicación.Form2();
            generar_Publicacion.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
