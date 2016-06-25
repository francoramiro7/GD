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

namespace WindowsFormsApplication1.Facturas
{
    public partial class Form1 : Form
    {
        int pag = 0;
        int pagTotal;
        int totalRows = 0;

        SqlConnection coneccion;
        SqlCommand comprasRealizadas;
        SqlDataReader data;
        DataTable tabla = new DataTable();


        public Form1()
        {
            InitializeComponent();

            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();

            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.Form2 form2 = new WindowsFormsApplication1.Form2();
            form2.Show();
            this.Close();
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
                pag = pag - 1;
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
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buscar();
        }

        private void actualizarTabla()
        {
            int ini = pag * 10;

            int n = 0;

            DataTable temporal = tabla.Clone();


            dataGridView1.ClearSelection();
            temporal.Clear();

            while (ini < totalRows && n < 10)
            {


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

            label7.Text = "Pagina " + pag.ToString() + " de " + pagTotal.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            buscar();  
        }


        public bool validar()
        {

            if (String.IsNullOrEmpty(textBox3.Text))
            {
                if (!String.IsNullOrEmpty(textBox4.Text))
                {
                    String mensaje = "No puede haber solo un importe vacio";
                    String caption = "Error al ingresar datos";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                    return false;
                }
            }
            else
            {
                if (String.IsNullOrEmpty(textBox4.Text))
                {
                    String mensaje = "No puede haber solo un importe vacio";
                    String caption = "Error al ingresar datos";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                    return false;
                }
            }


            if ((!String.IsNullOrEmpty(textBox4.Text)) && (!String.IsNullOrEmpty(textBox3.Text)))
            {
                float imp1= (float) Double.Parse(textBox3.Text);
                float imp2 =(float)Double.Parse(textBox4.Text);
                if (imp2 < imp1)
                {
                    String mensaje = "El segundo importe debe ser mayor o igual al primero";
                    String caption = "Error al ingresar datos";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                    return false;
                }
            }
            return true;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            String ingresado = ((TextBox)sender).Text;

            if (esNumero(ingresado, true))
            {
            }
            else
            {
                String mensaje = "Solo se pueden ingresar numeros y ',' en este campo";
                String caption = "Error al ingresar datos";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                textBox3.Text = "";
            }
        }

        private bool esNumero(String ingresado, bool tieneComa)
        {

            char[] ingre = ingresado.ToCharArray();
            int comas = 0;
            for (int i = 0; i < ingresado.Length; i++)
            {

                if (ingre[0].Equals(','))
                {
                    return false;
                }

                if (!char.IsNumber(ingre[i]))
                {
                    if (tieneComa)
                    {
                        if ((!ingre[i].Equals(',')) || (comas > 0))
                        {
                            return false;
                        }
                        else
                        {
                            comas++;
                        }
                    }
                    else
                    {
                        return false;
                    }


                }
            }
            return true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            String ingresado = ((TextBox)sender).Text;

            if (esNumero(ingresado, true))
            {
            }
            else
            {
                String mensaje = "Solo se pueden ingresar numeros y ',' en este campo";
                String caption = "Error al ingresar datos";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                textBox4.Text = "";
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker1.Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                String mensaje = "Debe seleccionar una fecha mayor a la inicial";
                String caption = "Error al ingresar datos";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                label6.Visible = true;

            }
            else
            {
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                label6.Visible = false;
            }
        }
    
        private void buscar()
        {
            string parametroImporte = "";
            string parametroUsuario = "";
            string parametroDetalle = "";
            string parametroFechas = "";

            if (validar())
            {
                if ((!String.IsNullOrEmpty(textBox4.Text)) && (!String.IsNullOrEmpty(textBox3.Text)))
                {
                    float imp1 = (float)Double.Parse(textBox3.Text);
                    float imp2 = (float)Double.Parse(textBox4.Text);

                    parametroImporte = " and Item_Factura_monto between " + imp1.ToString() + " and " + imp2.ToString();

                }

                if (!String.IsNullOrEmpty(textBox1.Text))
                {
                    parametroUsuario = " and Publicacion_vendedor = '" + textBox1.Text + "'";

                }

                if (!String.IsNullOrEmpty(textBox2.Text))
                {
                    parametroDetalle = " and Item_Factura_detalle like '%"+textBox2.Text+"%'";

                }

                if (checkBox1.Checked)
                {
                    parametroFechas = " and Factura_fecha between '" + dateTimePicker1.Value.ToString() + "' and '" + dateTimePicker2.Value.ToString()+"'";
                }

                String query = "select Item_Factura_codigo,Item_Factura_detalle,Item_Factura_monto,Factura_fecha" +
" from PERSISTIENDO.Publicacion" +
" Join PERSISTIENDO.Factura on Factura_codigo_publicacion = Publicacion_codigo" +
" Join PERSISTIENDO.Item_Factura On Factura_numero = Item_Factura_codigo_factura" +
" where Item_Factura_codigo > 0"+parametroUsuario+parametroDetalle+parametroImporte+parametroFechas+
" order by Item_Factura_codigo desc";



                SqlCommand comando = new SqlCommand(query, coneccion);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = comando;
                tabla.Clear();
                adapter.Fill(tabla);

                totalRows = tabla.Rows.Count;
                float cant = (tabla.Rows.Count / 10);

                pagTotal = (int)cant;

                if ((cant - ((int)cant)) != 0)
                {
                    pagTotal++;
                }

                actualizarTabla();
            }

        }
    }
}
