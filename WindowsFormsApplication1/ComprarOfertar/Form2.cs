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
using System.Globalization;


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
        float oferta = 0;
        String estado;
        bool envia;
        String vendedor;
        String visibilidad;

        SqlConnection coneccion;
        SqlCommand datosPublicacion,ultimaFactura,facturar, modificarMontoFactura, cuantasPorCalificar, stockPublicacion, newCompra, modificarStockEstadoPublicacion, ultimaOferta, facturaPorPublicacion, cantidadOfertas, ofertar, itemFactura, porVisibilidad;
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
                label9.Visible = true;
                label2.Visible=true;
                label8.Text = "Ultima Oferta:";
                label2.Text = "Oferta:";
                label5.Visible=false;
                textBox1.Visible=false;
                botonTerminar.Text = "Ofertar";
                this.Text = "Ofertar por Publicación";
                textBox1.Text = "1";
                textBox1.Enabled = false;
                textBox2.Visible = true;
                label9.Text = "Precio sugerido: $" + precio.ToString();


                cantidadOfertas = new SqlCommand("PERSISTIENDO.cantidadOfertas", coneccion);
                cantidadOfertas.CommandType = CommandType.StoredProcedure;
                cantidadOfertas.Parameters.Add("@Codigo", SqlDbType.Float).Value = codigo;
                var co = cantidadOfertas.Parameters.Add("@Cantidad", SqlDbType.Int);
                co.Direction = ParameterDirection.ReturnValue;
                data = cantidadOfertas.ExecuteReader();
                var varCofertas = co.Value;
                data.Close();




                if (((int)varCofertas)>0)
                {

                    ultimaOferta = new SqlCommand("PERSISTIENDO.ultimaOferta", coneccion);
                    ultimaOferta.CommandType = CommandType.StoredProcedure;
                    ultimaOferta.Parameters.Add("@Codigo", SqlDbType.Float).Value = codigo;
                    SqlDataAdapter adapter3 = new SqlDataAdapter(ultimaOferta);
                    DataTable table2 = new DataTable();
                    adapter3.Fill(table2);

                    oferta = (float)Double.Parse(table2.Rows[0][0].ToString());
                }

                label6.Text = "$" + oferta.ToString();
            
            }else{
                label2.Visible=true;
                label9.Visible = false;
                label8.Text = "Precio";
                label2.Text = "Cantidad:";
                label5.Visible=true;
                textBox1.Visible=true;
                botonTerminar.Text = "Comprar";
                this.Text = "Comprar";
                textBox2.Visible = false;
                textBox1.Enabled = true;
                textBox1.Text = "1";
            }

            datosPublicacion = new SqlCommand("PERSISTIENDO.datosPublicacion", coneccion);
            datosPublicacion.CommandType = CommandType.StoredProcedure;
            datosPublicacion.Parameters.Add("@Codigo", SqlDbType.Float).Value = codigo;

            SqlDataAdapter adapter2 = new SqlDataAdapter(datosPublicacion);
            DataTable table = new DataTable();
            adapter2.Fill(table);


            estado = table.Rows[0][5].ToString();
            vendedor = table.Rows[0][6].ToString();
            visibilidad = table.Rows[0][3].ToString();

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

            if (!usuario.username.Equals(vendedor))
            {

                if (!usuario.Rol.Equals("Administrador"))
                {

                    if (estado.Equals("Pausada"))
                    {
                        String mensaje = "La publicación esta pausada, intentelo más tarde";
                        String caption = "Imposible realizar la compra/oferta";
                        MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(comboBox1.Text))
                        {
                            cuantasPorCalificar = new SqlCommand("PERSISTIENDO.cuantasPorCalificar", coneccion);
                            cuantasPorCalificar.CommandType = CommandType.StoredProcedure;
                            cuantasPorCalificar.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = usuario.username;
                            SqlDataAdapter adapter7 = new SqlDataAdapter(cuantasPorCalificar);
                            DataTable table7 = new DataTable();
                            adapter7.Fill(table7);

                            float cantCali = (float)Double.Parse(table7.Rows[0][0].ToString());

                            if (cantCali < 4) { 

                            if (tipo.Equals("Subasta"))
                            {

                                if (!String.IsNullOrEmpty(textBox2.Text))
                                {
                                    if (((float)Double.Parse(textBox2.Text)) > oferta)
                                    {

                                        ofertar = new SqlCommand("PERSISTIENDO.ofertar", coneccion);

                                        ofertar.CommandType = CommandType.StoredProcedure;
                                        ofertar.Parameters.Add("@CodigoPublicacion", SqlDbType.Float).Value = (float.Parse(codigo.ToString(), CultureInfo.InvariantCulture.NumberFormat));
                                        ofertar.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Properties.Settings.Default.fecha;
                                        ofertar.Parameters.Add("@Monto", SqlDbType.Float).Value = textBox2.Text;
                                        ofertar.Parameters.Add("@Envio", SqlDbType.Bit).Value = envia;
                                        ofertar.Parameters.Add("@Ofertante", SqlDbType.VarChar).Value = usuario.username;

                                        ofertar.ExecuteNonQuery();

                                        ComprarOfertar.Form1 form1 = new ComprarOfertar.Form1();
                                        form1.Show();
                                        this.Close();

                                    }
                                    else
                                    {
                                        String mensaje = "La oferta debe ser mayor al ultimo monto ofertado";
                                        String caption = "Imposible realizar la oferta";
                                        MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                                    }
                                }
                                else
                                {
                                    String mensaje = "Debe completar el campo oferta";
                                    String caption = "Imposible realizar la oferta";
                                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

                                }

                            }
                            else
                            {
                                if (!String.IsNullOrEmpty(textBox1.Text))
                                {
                                    //Busco cod ultima factura
                                    ultimaFactura = new SqlCommand("PERSISTIENDO.ultimaFactura", coneccion);

                                    ultimaFactura.CommandType = CommandType.StoredProcedure;
                                    var uf = ultimaFactura.Parameters.Add("@Cantidad", SqlDbType.Float);
                                    uf.Direction = ParameterDirection.ReturnValue;
                                    data = ultimaFactura.ExecuteReader();
                                    var codFactura = uf.Value;
                                    data.Close();

                                    float nFac= ((float) Double.Parse(codFactura.ToString()))+1;

                                    //Generar factura
                                    facturar = new SqlCommand("PERSISTIENDO.facturarPublicacion", coneccion);

                                    facturar.CommandType = CommandType.StoredProcedure;

                                    facturar.Parameters.Add("@CodigoPublicacion", SqlDbType.Float).Value = codigo;
                                    facturar.Parameters.Add("@CodigoFactura", SqlDbType.Float).Value = nFac;
                                    facturar.Parameters.Add("@Precio", SqlDbType.Float).Value = 0;
                                    facturar.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Properties.Settings.Default.fecha;
                                    facturar.Parameters.Add("@Pago", SqlDbType.VarChar).Value = "Efectivo";

                                    facturar.ExecuteNonQuery();


                                    //Obtener % visibilidad
                                    porVisibilidad = new SqlCommand("PERSISTIENDO.porcentajeVisibilidad", coneccion);
                                    porVisibilidad.CommandType = CommandType.StoredProcedure;
                                    porVisibilidad.Parameters.Add("@Visibilidad", SqlDbType.VarChar).Value = visibilidad;
                                    SqlDataAdapter adapter4 = new SqlDataAdapter(porVisibilidad);
                                    DataTable table3 = new DataTable();
                                    adapter4.Fill(table3);

                                    float porcentaje = (float)Double.Parse(table3.Rows[0][0].ToString());
                                    float precioEnvio = (float)Double.Parse(table3.Rows[0][1].ToString());

                                    float total = ((float)Double.Parse(precio) * (Int32.Parse(textBox1.Text)))*porcentaje;
                                    

                                    //generar item_factura
                                    itemFactura = new SqlCommand("PERSISTIENDO.itemFactura", coneccion);
                                    itemFactura.CommandType = CommandType.StoredProcedure;
                                    itemFactura.Parameters.Add("@CodigoFactura", SqlDbType.Float).Value = nFac;
                                    itemFactura.Parameters.Add("@Precio", SqlDbType.Float).Value = total;
                                    itemFactura.Parameters.Add("@Detalle", SqlDbType.VarChar).Value = ("Comision por venta: " + visibilidad);
                                    itemFactura.Parameters.Add("@Cantidad", SqlDbType.Int).Value = (Int32.Parse(textBox1.Text)) ;

                                    itemFactura.ExecuteNonQuery();

                                    modificarMontoFactura = new SqlCommand("PERSISTIENDO.modificarMontoFactura", coneccion);
                                    modificarMontoFactura.CommandType = CommandType.StoredProcedure;
                                    modificarMontoFactura.Parameters.Add("@Numero", SqlDbType.Float).Value = nFac;
                                    modificarMontoFactura.Parameters.Add("@Monto", SqlDbType.Float).Value = total;

                                    modificarMontoFactura.ExecuteNonQuery();

                                    if (comboBox1.Text.Equals("Si"))
                                    {

                                        itemFactura = new SqlCommand("PERSISTIENDO.itemFactura", coneccion);
                                        itemFactura.CommandType = CommandType.StoredProcedure;
                                        itemFactura.Parameters.Add("@CodigoFactura", SqlDbType.Float).Value = nFac;
                                        itemFactura.Parameters.Add("@Precio", SqlDbType.Float).Value = precioEnvio;
                                        itemFactura.Parameters.Add("@Detalle", SqlDbType.VarChar).Value = ("Envio: " + visibilidad);
                                        itemFactura.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1;

                                        itemFactura.ExecuteNonQuery();

                                        modificarMontoFactura = new SqlCommand("PERSISTIENDO.modificarMontoFactura", coneccion);
                                        modificarMontoFactura.CommandType = CommandType.StoredProcedure;
                                        modificarMontoFactura.Parameters.Add("@Numero", SqlDbType.Float).Value = nFac;
                                        modificarMontoFactura.Parameters.Add("@Monto", SqlDbType.Float).Value = precioEnvio;

                                        modificarMontoFactura.ExecuteNonQuery();

                                    }
                                    
                                    //actualizar stock
                                    //si el stock queda en 0 finalizar publicacion

                                    modificarStockEstadoPublicacion = new SqlCommand("PERSISTIENDO.modificarStockEstadoPublicacion", coneccion);
                                    modificarStockEstadoPublicacion.CommandType = CommandType.StoredProcedure;
                                    modificarStockEstadoPublicacion.Parameters.Add("@CodigoPublicacion", SqlDbType.Float).Value = (float.Parse(codigo.ToString(), CultureInfo.InvariantCulture.NumberFormat));

                                    int stockFinal = stock - (Int32.Parse(textBox1.Text));

                                    modificarStockEstadoPublicacion.Parameters.Add("@Stock", SqlDbType.Float).Value = stockFinal;
                                    if (stockFinal == 0)
                                    {
                                        modificarStockEstadoPublicacion.Parameters.Add("@Estado", SqlDbType.Int).Value = 4;
                                    }
                                    else
                                    {
                                        modificarStockEstadoPublicacion.Parameters.Add("@Estado", SqlDbType.Int).Value = 1;
                                    }

                                    modificarStockEstadoPublicacion.ExecuteNonQuery();

                                    //generarCompra
                                    newCompra = new SqlCommand("PERSISTIENDO.newCompra", coneccion);
                                    newCompra.CommandType = CommandType.StoredProcedure;
                                    newCompra.Parameters.Add("@Codigo", SqlDbType.Float).Value = codigo;
                                    newCompra.Parameters.Add("@Comprador", SqlDbType.VarChar).Value = usuario.username;
                                    newCompra.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Properties.Settings.Default.fecha;
                                    newCompra.Parameters.Add("@Cant", SqlDbType.Float).Value = (float) Double.Parse(textBox1.Text);

                                    newCompra.ExecuteNonQuery();
                                    

                                    ComprarOfertar.Form1 form1 = new ComprarOfertar.Form1();
                                    form1.Show();
                                    this.Close();
                                }
                                else
                                {
                                    String mensaje = "Debe completar el campo cantidad";
                                    String caption = "Imposible realizar la compra";
                                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                                }
                            }
                        }else{
                            String mensaje = "Por favor califica antes de comprar/ofertar";
                            String caption = "Imposible realizar la compra/oferta";
                            MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

                        }
                        }
                        else
                        {
                            String mensaje = "Debe completar el campo envio";
                            String caption = "Imposible realizar la compra/oferta";
                            MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                        }
                    }
                }
                else
                {
                    String mensaje = "No tiene permisos para comprar/ofertar";
                    String caption = "Error de permisos";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                }
            }
            else
            {
                String mensaje = "No puede comprar/ofertar por una publicación propia";
                String caption = "Imposible realizar la compra/oferta";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            String ingresado = ((TextBox)sender).Text;

            if (esNumero(ingresado, true))
            {
                if (!textBox2.Text.Equals(""))
                {
                }
            }
            else
            {
                String mensaje = "No puede ingresar letras";
                String caption = "Error al ingresar datos";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                textBox2.Text = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("No"))
            {
                envia = false;
            }
            else
            {
                envia = true;
            }
        }
    }
}
