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

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        SqlConnection coneccion;
        SqlCommand cambiar;
        public Form3()
        {
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.Form2 f2 = new WindowsFormsApplication1.Form2();
            f2.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            coneccion.Open();
            cambiar = new SqlCommand("PERSISTIENDO.actualizarContra", coneccion);
           cambiar.CommandType = CommandType.StoredProcedure;
           cambiar.Parameters.Add("@user", SqlDbType.VarChar).Value = usuario.username;
           cambiar.Parameters.Add("@pass", SqlDbType.VarChar).Value = textBox2.Text;
           cambiar.ExecuteNonQuery();

           String mensaje = "La password se ha cambiado correctamente";
           String caption = "Password cambiada";
           MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

           WindowsFormsApplication1.Form2 f2 = new WindowsFormsApplication1.Form2();
           f2.Show();
           this.Close();
        }
    }
}
