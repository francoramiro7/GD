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
        SqlCommand cargarfun, cargaradmin;
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

            if (rol.Equals("Administrador"))
            {


                cargaradmin = new SqlCommand("PERSISTIENDO.listarFuncionalidades", coneccion);

                cargaradmin.CommandType = CommandType.StoredProcedure;



                SqlDataReader reader = cargaradmin.ExecuteReader();
                List<string> funcionalidades = new List<string>();

                

                while (reader.Read())
                {
                    funcionalidades.Add(reader.GetValue(0).ToString());
                }
                reader.Close();

                crearBotones(funcionalidades);
            }
            else
            {

                cargarfun = new SqlCommand("PERSISTIENDO.FuncionalidadesPorRol", coneccion);
                cargarfun.Parameters.Add("@Rol", SqlDbType.VarChar).Value = rol;
                cargarfun.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cargarfun.ExecuteReader();
                List<string> funcionalidades = new List<string>();

               

                while (reader.Read())
                {
                    funcionalidades.Add(reader.GetValue(0).ToString());
                }
                reader.Close();

                crearBotones(funcionalidades);

            }
        
        }



        private void crearBotones(List<String>func)
        {
            List<Button> botones = new List<Button>();
            botones.Add(button10);
            botones.Add(button11);
            botones.Add(button12);
            botones.Add(button13);
            botones.Add(button14);
            botones.Add(button15);
            botones.Add(button16);
            botones.Add(button17);
            botones.Add(button18);
            botones.Add(button19);



            int size = func.Count();
            for (int i = 0; i < size; i++)
            {

                String nombreBoton = func[i];
                botones[i].Text = nombreBoton;
                botones[i].Visible = true;
                
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

        private void realizarAccion(String nombre)
        {
            if (nombre.Equals("ABM ROL"))
            {
                ABM_Rol.Form1 formRol = new ABM_Rol.Form1();
                formRol.Show();
                this.Close();
                
            }

            if (nombre.Equals("ABM USUARIO"))
            {
                ABM_Usuario.Form2 formUser = new ABM_Usuario.Form2();
                formUser.Show();
                this.Close();

            }

            if (nombre.Equals("ABM RUBRO"))
            {
                ABM_Rubro.Form1 formRubro = new ABM_Rubro.Form1();
                formRubro.Show();
                this.Close();

            }

            if (nombre.Equals("ABM VISIBILIDAD DE PUBLICACION"))
            {
                ABM_Visibilidad.Form1 formVisibilidad = new ABM_Visibilidad.Form1();
                formVisibilidad.Show();
                this.Close();


            }

            if (nombre.Equals("PUBLICACIONES"))
            {
                Generar_Publicación.Form2 formPublicacion = new Generar_Publicación.Form2();
                formPublicacion.Show();
                this.Close();

            }

            if (nombre.Equals("COMPRAR/OFERTAR"))
            {
                ComprarOfertar.Form1 formComprar = new ComprarOfertar.Form1();
                formComprar.Show();
                this.Close();

            }

            if (nombre.Equals("HISTORIAL DE CLIENTES"))
            {
                Historial_Cliente.Form1 formHist = new Historial_Cliente.Form1();
                formHist.Show();
                this.Close();

            }

            if (nombre.Equals("CALIFICAR AL VENDEDOR"))
            {
                Calificar.Form2 formCal = new Calificar.Form2();
                formCal.Show();
                this.Close();

            }

            if (nombre.Equals("CONSULTA DE FACTURAS"))
            {
                Facturas.Form1 formFac = new Facturas.Form1();
                formFac.Show();
                this.Close();

            }

            if (nombre.Equals("LISTADO ESTADISTICO"))
            {
                
                Listado_Estadistico.Form1 formList = new Listado_Estadistico.Form1();
                formList.Show();
                this.Close();

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            realizarAccion(button10.Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            realizarAccion(button11.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            realizarAccion(button12.Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            realizarAccion(button13.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            realizarAccion(button14.Text);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            realizarAccion(button15.Text);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            realizarAccion(button16.Text);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            realizarAccion(button17.Text);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            realizarAccion(button18.Text);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            WindowsFormsApplication1.Form3 frm3 = new WindowsFormsApplication1.Form3();
            frm3.Show();
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            usuario.username = "";
            usuario.Rol = "";
            WindowsFormsApplication1.Form1 f1 = new WindowsFormsApplication1.Form1();
            this.Close();
            f1.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            realizarAccion(button19.Text);
        }

    }
}
