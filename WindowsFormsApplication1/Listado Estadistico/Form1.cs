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

namespace WindowsFormsApplication1.Listado_Estadistico
{
    public partial class Form1 : Form
    {
        SqlConnection coneccion;
        SqlCommand nombresVisibilidad, rubros ;
        SqlDataReader data;
        string t1;
        string t2;

        public Form1()
        {
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
             
//Vendedores con mayor cantidad de productos no vendidos
//Clientes con mayor cantidad de productos comprados
//Vendedores con mayor cantidad de facturas
//Vendedores con mayor monto facturado
            DataTable table = new DataTable();
            string query = "";

            if (validar())
            {
                if(comboBox2.Text.Equals("Vendedores con mayor monto facturado")){

                  query =  "select top 5 Usuario.Usuario_username,sum(Factura_total) as total"+
" from PERSISTIENDO.Usuario"+
" Join PERSISTIENDO.Publicacion on Publicacion_vendedor = Usuario_username"+
" Join PERSISTIENDO.Factura on Factura_codigo_publicacion = Publicacion_codigo"+
" where year(Factura_fecha) = "+textBox1.Text+" and month(Factura_fecha) between "+t1+" and "+t2+
" group by Usuario_username"+
" order by total desc";

                }
                else if (comboBox2.Text.Equals("Vendedores con mayor cantidad de facturas"))
                {

                    query = "select top 5 Usuario.Usuario_username,count(Factura_numero) as total" +
  " from PERSISTIENDO.Usuario" +
  " Join PERSISTIENDO.Publicacion on Publicacion_vendedor = Usuario_username" +
  " Join PERSISTIENDO.Factura on Factura_codigo_publicacion = Publicacion_codigo" +
  " where year(Factura_fecha) = " + textBox1.Text + " and month(Factura_fecha) between " + t1 + " and " + t2 +
  " group by Usuario_username" +
  " order by total desc";

                }
                else if (comboBox2.Text.Equals("Clientes con mayor cantidad de productos comprados"))
                {
                    string parametroRubro="";
                    if (!String.IsNullOrEmpty(comboBox3.Text))
                    {
                        parametroRubro = " and Rubro_descripcion like '" + comboBox3.Text +"'";
                    }


                    query = "select top 5 Compra_comprador,sum(Compra_cantidad) as total" +
" from PERSISTIENDO.Compra" +
" JOIN PERSISTIENDO.Publicacion on Publicacion_codigo= Compra_codigo_publicacion"+
" Join PERSISTIENDO.Rubro on Publicacion_rubro = Rubro_codigo"+
" where year(Compra_fecha)= " + textBox1.Text + " and month(Compra_fecha) between "+ t1 +" and "+ t2 +parametroRubro+
" group by Compra_comprador" +
" order by total desc";


                }
                else if (comboBox2.Text.Equals("Vendedores con mayor cantidad de productos no vendidos"))
                {
                    string parametroVisi = "";
                    if (!String.IsNullOrEmpty(comboBox3.Text))
                    {
                        parametroVisi = " and Visibilidad_descripcion like '" + comboBox3.Text + "'";

                    }

                    query = "select top 5 Publicacion_vendedor,sum(Publicacion_stock) as total"+
" from PERSISTIENDO.Publicacion"+
" JOIN PERSISTIENDO.Visibilidad on Publicacion_visibilidad  = Visibilidad_cod"+
" where year(Publicacion_fecha) = "+textBox1.Text+" and month(Publicacion_fecha) between "+t1+ " and "+t2+parametroVisi+
" group by Publicacion_vendedor"+
" order by total desc";


                }
                SqlCommand comando = new SqlCommand(query, coneccion);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = comando;
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;



            }



        }
        private bool validar()
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(comboBox1.Text) || String.IsNullOrEmpty(comboBox2.Text))
            {


                String mensaje = "Los campos año, trimestre y Estadistica no pueden estar vacios";
                String caption = "Error al ingresar datos";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.Form2 form2 = new WindowsFormsApplication1.Form2();
            form2.Show();
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text.Equals("Vendedores con mayor cantidad de productos no vendidos"))
            {
                label5.Visible = true;
                label5.Text = "Visibilidad:";
                nombresVisibilidad = new SqlCommand("PERSISTIENDO.nombreVisibilidades", coneccion);

                nombresVisibilidad.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(nombresVisibilidad);
                DataTable tablavisiblidades = new DataTable();

                adapter.Fill(tablavisiblidades);
                comboBox3.DataSource = tablavisiblidades;
                comboBox3.DisplayMember = "Visibilidad_descripcion";
                comboBox3.SelectedIndex = comboBox3.Items.IndexOf("New");
                comboBox3.Visible = true;
            }
            else if (comboBox2.Text.Equals("Clientes con mayor cantidad de productos comprados"))
            {
                label5.Visible = true;
                label5.Text = "Rubro:";
                comboBox3.Visible = true;
                rubros = new SqlCommand("PERSISTIENDO.listarRubros", coneccion);
                rubros.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapterRubro = new SqlDataAdapter(rubros);
                DataTable tablaRubros = new DataTable();
                adapterRubro.Fill(tablaRubros);
                comboBox3.DataSource = tablaRubros;
                comboBox3.DisplayMember = "Rubro_Descripcion";
                comboBox3.SelectedIndex = comboBox3.Items.IndexOf("New");
            }
            else
            {
                label5.Visible = false;
                comboBox3.Visible = false;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("1"))
            {
                t1 = "1";
                t2 = "3";
            }
            else if (comboBox1.Text.Equals("2"))
            {
                t1 = "4";
                t2 = "6";
            }
            else if (comboBox1.Text.Equals("3"))
            {
                t1 = "7";
                t2 = "8";
            }
            else
            {
                t1 = "9";
                t2 = "12";
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
