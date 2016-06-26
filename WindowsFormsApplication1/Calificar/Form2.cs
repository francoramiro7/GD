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

namespace WindowsFormsApplication1.Calificar
{
    public partial class Form2 : Form
    {

        SqlConnection coneccion;
        SqlCommand calificadas, porCalificar;
        SqlDataReader data;
        
        
        public Form2()
        {
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
            
            InitializeComponent();

            porCalificar = new SqlCommand("PERSISTIENDO.porCalificar", coneccion);
            porCalificar.CommandType = CommandType.StoredProcedure;
            porCalificar.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = usuario.username;

            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter(porCalificar);
            DataTable table;
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (table.Rows.Count == 0)
            {
                button2.Visible = false;
            }
            else
            {
                button2.Visible = true;
            }

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            calificadas = new SqlCommand("PERSISTIENDO.calificadas", coneccion);
            calificadas.CommandType = CommandType.StoredProcedure;
            calificadas.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = usuario.username;

            SqlDataAdapter adapter2;
            adapter2 = new SqlDataAdapter(calificadas);
            DataTable table2;
            table2 = new DataTable();
            adapter2.Fill(table2);
            dataGridView2.DataSource = table2;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;



            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.Form2 form2 = new WindowsFormsApplication1.Form2();
            form2.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string cod ="";
            string detalle="";

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                cod = row.Cells[0].Value.ToString();
                detalle = row.Cells[1].Value.ToString();
            }

            Calificar.Form3 form3 = new Calificar.Form3(cod,detalle);
            form3.Show();
            this.Close();
        }
    }
}
