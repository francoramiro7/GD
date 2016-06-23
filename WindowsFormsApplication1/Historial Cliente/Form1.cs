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

namespace WindowsFormsApplication1.Historial_Cliente
{
    public partial class Form1 : Form
    {

        int pag = 0;
        int pagTotal;
        int totalRows=0;

        SqlConnection coneccion;
        SqlCommand ofertarYCompras, cuantasPorCalificar, cantidadDeEstrellas, comprasRealizadas;
        SqlDataReader data;
        DataTable tabla = new DataTable();


        public Form1()
        {
            InitializeComponent();

            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();

            //Compras/ofertas concretadas

            comprasRealizadas = new SqlCommand("PERSISTIENDO.comprasRealizadas", coneccion);
            comprasRealizadas.CommandType = CommandType.StoredProcedure;
            comprasRealizadas.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = usuario.username;
            var cr = comprasRealizadas.Parameters.Add("@Cantidad", SqlDbType.Int);
            cr.Direction = ParameterDirection.ReturnValue;
            data = comprasRealizadas.ExecuteReader();
            var comReal = cr.Value;
            data.Close();

            label6.Text = comReal.ToString();





            //CantidadDeEstrelllas

            cantidadDeEstrellas = new SqlCommand("PERSISTIENDO.cantidadDeEstrellas", coneccion);
            cantidadDeEstrellas.CommandType = CommandType.StoredProcedure;
            cantidadDeEstrellas.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = usuario.username;
            var ce = cantidadDeEstrellas.Parameters.Add("@Cantidad", SqlDbType.Int);
            ce.Direction = ParameterDirection.ReturnValue;
            data = cantidadDeEstrellas.ExecuteReader();
            var cantEstrellas = ce.Value;
            data.Close();

            label4.Text = cantEstrellas.ToString();

            //Por calificar
            cuantasPorCalificar = new SqlCommand("PERSISTIENDO.cuantasPorCalificar", coneccion);
            cuantasPorCalificar.CommandType = CommandType.StoredProcedure;
            cuantasPorCalificar.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = usuario.username;
            SqlDataAdapter adapter7 = new SqlDataAdapter(cuantasPorCalificar);
            DataTable table7 = new DataTable();
            adapter7.Fill(table7);

            String cantCali = table7.Rows[0][0].ToString();

            label3.Text = cantCali;


            //Cargar Tabla
            ofertarYCompras = new SqlCommand("PERSISTIENDO.ofertarYCompras", coneccion);
            ofertarYCompras.CommandType = CommandType.StoredProcedure;
            ofertarYCompras.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = usuario.username;
            SqlDataAdapter adapter = new SqlDataAdapter(ofertarYCompras);
            adapter.Fill(tabla);

            totalRows = tabla.Rows.Count;
            float cant = (tabla.Rows.Count / 10);
            
            pagTotal = (int) cant;

            if ((cant - ((int)cant)) != 0)
            {
                pagTotal++;
            }

            actualizarTabla();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.Form2 form2 = new WindowsFormsApplication1.Form2();
            form2.Show();
            this.Close();
        }

        private void actualizarTabla()
        {
            int ini = pag * 10;

            int n = 0;

            DataTable temporal = tabla.Clone();

            
            dataGridView1.ClearSelection();
            temporal.Clear();
           
            while(ini<totalRows && n<10){
 

                DataRow row;
                row = temporal.NewRow();
                row.ItemArray = tabla.Rows[ini].ItemArray;

                DataRow row2 = tabla.Rows[ini];     
                temporal.Rows.Add(row);
                ini++;
                n++;

            }
            dataGridView1.DataSource = temporal;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            label1.Text = "Pagina " + pag.ToString() + " de " + pagTotal.ToString();

        }

        private void botonPrimeraPagina_Click(object sender, EventArgs e)
        {
            pag = 0;
            actualizarTabla();
        }

        private void botonPaginaAnterior_Click(object sender, EventArgs e)
        {
            if (pag > 0)
            {
                pag=pag-1;
                actualizarTabla();

            }
        }

        private void botonPaginaSiguiente_Click(object sender, EventArgs e)
        {
            if (pag != pagTotal)
            {
                pag++;
                actualizarTabla();

            }
        }

        private void botonUltimaPagina_Click(object sender, EventArgs e)
        {
            pag = pagTotal;
            actualizarTabla();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
