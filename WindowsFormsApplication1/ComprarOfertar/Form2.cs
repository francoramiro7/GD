using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1.ComprarOfertar
{
    public partial class Form2 : Form
    {
        String codigo;
        String descripcion;
        String precio;
        String rubro;
        String tipo;
        int stock;
        float oferta;

        SqlConnection coneccion;
        SqlCommand datosPublicacion, stockPublicacion, ultimaOferta;
        SqlDataReader data;



        public Form2(string cod, string desc,string prec , string rub,string tip)
        {
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
            codigo = cod;
            descripcion = desc;
            precio = prec;
            rubro = rub;
            tipo = tip;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            labelDescripcion.Text = descripcion;            
            label6.Text = "$" + precio;
            label7.Text = rubro;
            textBox1.Text = "1";

            if(tipo.Equals("Subasta")){
                label2.Visible=true;
                label8.Text = "Ultima Oferta:";
                label2.Text = "Oferta:";
                label5.Visible=false;
                textBox1.Visible=false;
                botonTerminar.Text = "Ofertar";
                this.Text = "Ofertar por Publicación";
                textBox1.Text = "1";
                textBox1.Enabled = false;
                if (false)
                {

                    ultimaOferta = new SqlCommand("PERSISTIENDO.ultimaOferta", coneccion);
                    ultimaOferta.CommandType = CommandType.StoredProcedure;
                    ultimaOferta.Parameters.Add("@Codigo", SqlDbType.Float).Value = codigo;
                    var uo = ultimaOferta.Parameters.Add("@Cantidad", SqlDbType.Float);
                    uo.Direction = ParameterDirection.ReturnValue;
                    data = ultimaOferta.ExecuteReader();
                    var varOferta = uo.Value;
                    data.Close();

                    oferta = (float)varOferta;

                    label6.Text = "$" + oferta.ToString();
                }


            
            }else{
                label2.Visible=true;
                label8.Text = "Precio";
                label2.Text = "Cantidad:";
                label5.Visible=true;
                textBox1.Visible=true;
                botonTerminar.Text = "Comprar";
                this.Text = "Comprar";
                textBox1.Enabled = true;
            }

            datosPublicacion = new SqlCommand("PERSISTIENDO.datosPublicacion", coneccion);
            datosPublicacion.CommandType = CommandType.StoredProcedure;
            datosPublicacion.Parameters.Add("@Codigo", SqlDbType.Float).Value = codigo;

            SqlDataAdapter adapter2 = new SqlDataAdapter(datosPublicacion);
            DataTable table = new DataTable();
            adapter2.Fill(table);


            if (table.Rows[0][0].ToString().Equals("True"))
            {
                botonPreguntar.Visible = true;
            }
            else
            {
                botonPreguntar.Visible = false;
            }

            if (table.Rows[0][1].ToString().Equals("True"))
            {
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox1.DisplayMember = "No";
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf("New");
                comboBox1.Text = "No";
            }

            stockPublicacion = new SqlCommand("PERSISTIENDO.stockPublicacion", coneccion);
            stockPublicacion.CommandType = CommandType.StoredProcedure;
            stockPublicacion.Parameters.Add("@Codigo", SqlDbType.Float).Value = codigo;
            var st = stockPublicacion.Parameters.Add("@Cantidad", SqlDbType.Int);
            st.Direction = ParameterDirection.ReturnValue;
            data = stockPublicacion.ExecuteReader();
            var varStock = st.Value;
            data.Close();

            stock = (int)varStock;

            label5.Text = "/ " + varStock.ToString();


        }

        private void botonVolver_Click(object sender, EventArgs e)
        {
            ComprarOfertar.Form1 form1 = new ComprarOfertar.Form1();
            form1.Show();
            this.Close();
        }

        private void labelDescripcion_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String ingresado = ((TextBox)sender).Text;

            if (esNumero(ingresado, false))
            {
                if(!textBox1.Text.Equals("")){
                    if ((Int32.Parse(textBox1.Text) > stock))
                    {
                        String mensaje = "No puede ingresar valores superiores a la cantidad disponible";
                        String caption = "Error al ingresar datos";
                        MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                        textBox1.Text = "";
                    }else if(Int32.Parse(textBox1.Text) == 0)
                    {
                        String mensaje = "No puede ingresar 0 como cantidad a comprar";
                        String caption = "Error al ingresar datos";
                        MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                        textBox1.Text = "";
                    }

                }

            }
            else
            {
                String mensaje = "No puede ingresar letras";
                String caption = "Error al ingresar datos";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                textBox1.Text = "";
            }
        }

        private void botonTerminar_Click(object sender, EventArgs e)
        {
           
        }

        private bool esNumero(String ingresado, bool tieneComa)
        {

            char[] ingre = ingresado.ToCharArray();
            int comas = 0;
            for (int i = 0; i < ingresado.Length; i++)
            {
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
    }
}
