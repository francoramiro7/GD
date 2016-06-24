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

namespace WindowsFormsApplication1.ABM_Visibilidad
{
    public partial class Form4 : Form
    {
        double codVisibilidad = 0;
        SqlConnection con;
        SqlDataReader data;
        SqlCommand visi, existe, codigo, borrar;
        public Form4()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ABM_Visibilidad.Form1 f1 = new ABM_Visibilidad.Form1();
            f1.Show();
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            con.Open();

            visi = new SqlCommand("PERSISTIENDO.nombreVisibilidades", con);

            visi.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(visi);
            DataTable tablavisiblidades = new DataTable();

            adapter.Fill(tablavisiblidades);
            comboBox2.DataSource = tablavisiblidades;
            comboBox2.DisplayMember = "Visibilidad_descripcion";
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = comboBox2.Text.ToString();

            con.Open();
            codigo = new SqlCommand("PERSISTIENDO.codigoVisibilidad", con);
            codigo.CommandType = CommandType.StoredProcedure;
            codigo.Parameters.Add("@Visibilidad", SqlDbType.VarChar).Value = nombre;
            var resultado = codigo.Parameters.Add("@Valor", SqlDbType.Int);
            resultado.Direction = ParameterDirection.ReturnValue;
            data = codigo.ExecuteReader();
            var cod = resultado.Value;
            data.Close();
            con.Close();

            double cod2 = double.Parse(cod.ToString());
            codVisibilidad = cod2;

            con.Open();
            existe = new SqlCommand("PERSISTIENDO.hayPublicacionConVisibilidad", con);
            existe.CommandType = CommandType.StoredProcedure;
            existe.Parameters.Add("@codigo", SqlDbType.VarChar).Value = cod2;
            var resultado2 = existe.Parameters.Add("@Valor", SqlDbType.Int);
            resultado2.Direction = ParameterDirection.ReturnValue;
            data = existe.ExecuteReader();
            var exi = resultado2.Value;
            data.Close();
            con.Close();


            if ((int)exi != 0)
            {
                String mensaje = "Hay publicaciones creadas con esta visibilidad, por lo tanto no puede eliminarse";
                String caption = "Error al eliminar ";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
            else
            {
                eliminarVisibilidad();
            }


        }


        public void eliminarVisibilidad()
        {

            borrar= new SqlCommand("PERSISTIENDO.eliminarVisibilidad", con);
            con.Open();
           borrar.CommandType = CommandType.StoredProcedure;
            borrar.Parameters.Add("@codigo", SqlDbType.Float).Value = codVisibilidad;
            borrar.ExecuteNonQuery();
            con.Close();
            String mensaje = "La visibilidad se ha eliminado";
            String caption = "Eliminar Visibilidad";
            MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            ABM_Visibilidad.Form1 f1 = new ABM_Visibilidad.Form1();
            f1.Show();
            this.Close();

        
        
        
        }


    }
}
