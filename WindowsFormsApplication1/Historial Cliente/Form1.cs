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
        SqlCommand getPublicaciones;
        SqlDataReader data;
        DataTable tabla = new DataTable();


        public Form1()
        {
            InitializeComponent();

            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();



            getPublicaciones = new SqlCommand("PERSISTIENDO.getPublicaciones", coneccion);
            getPublicaciones.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(getPublicaciones);
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
