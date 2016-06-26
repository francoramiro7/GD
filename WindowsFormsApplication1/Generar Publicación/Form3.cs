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

namespace WindowsFormsApplication1.Generar_Publicación
{
    public partial class Form3 : Form
    {

        String estado;
        float codigo;
        float costo = 0;
        String descripcion;
        String precio;
        String stock;
        String Rubro;
        String Pregunta;
        String envi;
        String Visibilidad;
        String Tipo;
        bool envia = false;
        String tipoVisibilidad = "";
        bool preguntas = true;
        SqlConnection coneccion;
        SqlCommand nombresVisibilidad, rubros, precioPorNombreVisibilidad, envioPorNombreVisibilidad, envioHabilitado, publicar,
            codigoRubro, codigoTipo, codigoVisibilidad, codigoEstado, rubroPorPublicacion, datosPublicacion,
            tieneFactura, ultimaFactura, facturar, itemFactura;
        SqlDataReader data;


        public Form3(String cod,String desc,String prec,String stock,String state)
        {
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
            codigo = float.Parse(cod, CultureInfo.InvariantCulture.NumberFormat);
            descripcion = desc;
            precio = prec;
            this.stock = stock;
            estado = state;

            label13.Text = cod;


            nombresVisibilidad = new SqlCommand("PERSISTIENDO.nombreVisibilidades", coneccion);

            nombresVisibilidad.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(nombresVisibilidad);
            DataTable tablavisiblidades = new DataTable();

            adapter.Fill(tablavisiblidades);
            comboBox4.DataSource = tablavisiblidades;

            rubros = new SqlCommand("PERSISTIENDO.listarRubros", coneccion);
            rubros.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapterRubro = new SqlDataAdapter(rubros);
            DataTable tablaRubros = new DataTable();
            adapterRubro.Fill(tablaRubros);
            comboBox5.DataSource = tablaRubros;

            datosPublicacion = new SqlCommand("PERSISTIENDO.datosPublicacion", coneccion);
            datosPublicacion.CommandType = CommandType.StoredProcedure;
            datosPublicacion.Parameters.Add("@Codigo", SqlDbType.Float).Value = codigo;
           
            SqlDataAdapter adapter2 = new SqlDataAdapter(datosPublicacion);
            DataTable table = new DataTable();
            adapter2.Fill(table);

           
            if (table.Rows[0][0].ToString().Equals("True"))
            {
                Pregunta = "Si";
            }
            else
            {
                Pregunta = "No";
            }

            if (table.Rows[0][1].ToString().Equals("True"))
            {
                envi = "Si";
            }
            else
            {
                envi = "No";
            }

            Rubro = table.Rows[0][2].ToString();
            Visibilidad = table.Rows[0][3].ToString();
            Tipo = table.Rows[0][4].ToString();
            


            data.Close();


            seteo();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Generar_Publicación.Form4 form4 = new Generar_Publicación.Form4();
            form4.Show();
            this.Close();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!usuario.Rol.Equals("Administrador"))
            {
                modificarPublicacion();

                Generar_Publicación.Form2 form2 = new Generar_Publicación.Form2();
                form2.Show();
                this.Close();
            }
            else
            {
                String mensaje = "No tiene permisos para modificar";
                String caption = "Error al modificar publicación";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
            }
            
        }

        public void modificarPublicacion()
        {

            if (validarCampos())
            {


                var codPublicacion = label13.Text;

                codigoRubro = new SqlCommand("PERSISTIENDO.codigoRubro", coneccion);

                codigoRubro.CommandType = CommandType.StoredProcedure;
                codigoRubro.Parameters.Add("@Rubro", SqlDbType.VarChar).Value = comboBox5.Text;
                var cr = codigoRubro.Parameters.Add("@Cantidad", SqlDbType.Int);
                cr.Direction = ParameterDirection.ReturnValue;
                data = codigoRubro.ExecuteReader();
                var codRubro = cr.Value;
                data.Close();

                codigoEstado = new SqlCommand("PERSISTIENDO.codigoEstado", coneccion);

                codigoEstado.CommandType = CommandType.StoredProcedure;
                codigoEstado.Parameters.Add("@Estado", SqlDbType.VarChar).Value = comboBox3.Text;
                var ce = codigoEstado.Parameters.Add("@Cantidad", SqlDbType.Int);
                ce.Direction = ParameterDirection.ReturnValue;
                data = codigoEstado.ExecuteReader();
                var codEstado = ce.Value;
                data.Close();

                codigoTipo = new SqlCommand("PERSISTIENDO.codigoTipoPublicacion", coneccion);

                codigoTipo.CommandType = CommandType.StoredProcedure;
                codigoTipo.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = comboBox2.Text;
                var ct = codigoTipo.Parameters.Add("@Cantidad", SqlDbType.Int);
                ct.Direction = ParameterDirection.ReturnValue;
                data = codigoTipo.ExecuteReader();
                var codTipo = ct.Value;
                data.Close();

                codigoVisibilidad = new SqlCommand("PERSISTIENDO.codigoVisibilidad", coneccion);

                codigoVisibilidad.CommandType = CommandType.StoredProcedure;
                codigoVisibilidad.Parameters.Add("@Visibilidad", SqlDbType.VarChar).Value = comboBox4.Text;
                var cv = codigoVisibilidad.Parameters.Add("@Cantidad", SqlDbType.Int);
                cv.Direction = ParameterDirection.ReturnValue;
                data = codigoVisibilidad.ExecuteReader();
                var codVisibilidad = cv.Value;
                data.Close();



                publicar = new SqlCommand("PERSISTIENDO.modificarPublicacion", coneccion);

                publicar.CommandType = CommandType.StoredProcedure;
                publicar.Parameters.Add("@CodigoPublicacion", SqlDbType.Float).Value =codigo;
                publicar.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = textBox1.Text;
                publicar.Parameters.Add("@Stock", SqlDbType.Int).Value = textBox2.Text;
                publicar.Parameters.Add("@Precio", SqlDbType.Float).Value = (float) Convert.ToDouble(textBox5.Text);
                publicar.Parameters.Add("@Rubro", SqlDbType.Int).Value = (int)codRubro;
                publicar.Parameters.Add("@Envio", SqlDbType.Bit).Value = envia;
                publicar.Parameters.Add("@Tipo", SqlDbType.Int).Value = (int)codTipo;
                publicar.Parameters.Add("@Preguntas", SqlDbType.Bit).Value = preguntas;
                publicar.Parameters.Add("@Visibilidad", SqlDbType.Int).Value = (int)codVisibilidad;
                publicar.Parameters.Add("@Estado", SqlDbType.Int).Value = (int)codEstado;

                publicar.ExecuteNonQuery();



                tieneFactura = new SqlCommand("PERSISTIENDO.tieneFactura", coneccion);

                tieneFactura.CommandType = CommandType.StoredProcedure;
                tieneFactura.Parameters.Add("@CodigoPublicacion", SqlDbType.Float).Value = codigo;
                var tf = tieneFactura.Parameters.Add("@Cantidad", SqlDbType.Int);
                tf.Direction = ParameterDirection.ReturnValue;
                data = tieneFactura.ExecuteReader();
                var facturasTiene = tf.Value;
                data.Close();


                if (comboBox3.Text.Equals("Activa")  && ((int)facturasTiene==0))
                {
                    ultimaFactura = new SqlCommand("PERSISTIENDO.ultimaFactura", coneccion);

                    ultimaFactura.CommandType = CommandType.StoredProcedure;
                    var uf = ultimaFactura.Parameters.Add("@Cantidad", SqlDbType.Float);
                    uf.Direction = ParameterDirection.ReturnValue;
                    data = ultimaFactura.ExecuteReader();
                    var codFactura = uf.Value;
                    data.Close();

                    facturar = new SqlCommand("PERSISTIENDO.facturarPublicacion", coneccion);

                    facturar.CommandType = CommandType.StoredProcedure;

                    facturar.Parameters.Add("@CodigoPublicacion", SqlDbType.Float).Value = codigo;
                    facturar.Parameters.Add("@CodigoFactura", SqlDbType.Float).Value = (float.Parse(codFactura.ToString(), CultureInfo.InvariantCulture.NumberFormat) + 1);
                    facturar.Parameters.Add("@Precio", SqlDbType.Float).Value = costo;
                    facturar.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Properties.Settings.Default.fecha;
                    facturar.Parameters.Add("@Pago", SqlDbType.VarChar).Value = "Efectivo";

                    facturar.ExecuteNonQuery();

                    itemFactura = new SqlCommand("PERSISTIENDO.newItemFactura", coneccion);

                    itemFactura.CommandType = CommandType.StoredProcedure;

                    itemFactura.Parameters.Add("@CodigoFactura", SqlDbType.Float).Value = (float.Parse(codFactura.ToString(), CultureInfo.InvariantCulture.NumberFormat) + 1);
                    itemFactura.Parameters.Add("@Precio", SqlDbType.Float).Value = costo;
                    itemFactura.Parameters.Add("@Detalle", SqlDbType.VarChar).Value = ("Costo de publicacion: " + comboBox4.Text);

                    itemFactura.ExecuteNonQuery();

                    string query2 = "update PERSISTIENDO.Usuario set Usuario_nuevo = 0 where Usuario_username like '" + usuario.username + "'";
                    SqlCommand comando2 = new SqlCommand(query2, coneccion);
                    comando2.ExecuteNonQuery();


                    Generar_Publicación.Form5 form5 = new Generar_Publicación.Form5(textBox1.Text,
                        comboBox4.Text, (costo.ToString()),
                        Properties.Settings.Default.fecha,
                        ((DateTime)Properties.Settings.Default.fecha).AddDays(7),
                        codPublicacion.ToString());
                    form5.Show();
                }



            }



        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void seteo()
        {

            textBox1.Text = descripcion;
            textBox2.Text = stock;
            textBox5.Text = precio;

            if (estado.Equals("Activa"))
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox5.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
                comboBox3.Enabled = true;
                comboBox3.Items.Add("Pausada");
                comboBox3.Items.Add("Finalizada");

            }
            else if (estado.Equals("Pausada"))
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox5.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
                comboBox3.Enabled = true;
                comboBox3.Items.Add("Activa");
                comboBox3.Items.Add("Finalizada");
            }
            else if (estado.Equals("Finalizada"))
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox5.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
                comboBox3.Enabled = false;
                comboBox3.Items.Add("Finalizada");
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox5.Enabled = true;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox4.Enabled = true;
                comboBox5.Enabled = true;
                comboBox6.Enabled = true;
                comboBox3.Enabled = true;
                comboBox3.Items.Add("Pausada");
                comboBox3.Items.Add("Activa");
                comboBox3.Items.Add("Borrador");
                comboBox3.Items.Add("Finalizada");
            }

            comboBox5.DisplayMember = "Rubro_Descripcion";
            comboBox5.SelectedIndex = comboBox5.Items.IndexOf("New");
            comboBox5.Text = Rubro;

            comboBox4.DisplayMember = "Visibilidad_descripcion";
            comboBox4.SelectedIndex = comboBox4.Items.IndexOf("New");
            comboBox4.Text = Visibilidad;

            comboBox2.DisplayMember = Tipo;
            comboBox2.SelectedIndex = comboBox2.Items.IndexOf("New");
            comboBox2.Text = Tipo;

            comboBox1.DisplayMember = Pregunta;
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf("New");
            comboBox1.Text = Pregunta;

            comboBox6.DisplayMember = envi;
            comboBox6.SelectedIndex = comboBox6.Items.IndexOf("New");
            comboBox6.Text = envi;

            comboBox3.SelectedIndex = -1;
            comboBox3.Text = estado;
            comboBox3.DisplayMember = estado;

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

            if (esNumero(ingresado, false))
            {
            }
            else
            {
                String mensaje = "Solo se pueden ingresar numeros en este campo";
                String caption = "Error al ingresar datos";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                textBox2.Text = "";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            String ingresado = ((TextBox)sender).Text;

            if (esNumero(ingresado, true))
            {
            }
            else
            {
                String mensaje = "Solo se pueden ingresar numeros y , en este campo";
                String caption = "Error al ingresar datos";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                textBox5.Text = "";
            }
        }

        private bool validarCampos()
        {
            if (string.IsNullOrEmpty(textBox1.Text) | string.IsNullOrEmpty(textBox2.Text) | string.IsNullOrEmpty(textBox5.Text) |
              string.IsNullOrEmpty(comboBox1.Text) | string.IsNullOrEmpty(comboBox2.Text) | string.IsNullOrEmpty(comboBox3.Text) |
                string.IsNullOrEmpty(comboBox4.Text) | string.IsNullOrEmpty(comboBox5.Text) | string.IsNullOrEmpty(comboBox6.Text))
            {


                String mensaje = "Todos los campos son obligatorios";
                String caption = "Error al modificar publicación";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                return false;

            }
            else
            {

                int tDescripcion = textBox1.Text.Length;
                int tStock = textBox2.Text.Length;
                int tPrecio = textBox5.Text.Length;


                if (tDescripcion > 256)
                {
                    String mensaje = "El campo Descripcion tiene más de 255 caracteres";
                    String caption = "Error al crear publicación";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                    return false;

                }
                else if (tStock > 19)
                {

                    String mensaje = "El campo Stock tiene más de 18 digitos";
                    String caption = "Error al modificar publicación";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                    return false;

                }
                else if (tPrecio > 22)
                {

                    String mensaje = "El campo Stock tiene más de 18 digitos";
                    String caption = "Error al modificar publicación";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                    return false;

                }
                else
                {

                    return true;

                }

            }

        }

        private void actualizar_costo()
        {

            float envio = 0;
            String costoTotal;

            precioPorNombreVisibilidad = new SqlCommand("PERSISTIENDO.precioPorVisibilidad", coneccion);

            precioPorNombreVisibilidad.CommandType = CommandType.StoredProcedure;
            precioPorNombreVisibilidad.Parameters.Add("@Detalle", SqlDbType.VarChar).Value = tipoVisibilidad;
            var prePrecio = precioPorNombreVisibilidad.Parameters.Add("@Cantidad", SqlDbType.Float);
            prePrecio.Direction = ParameterDirection.ReturnValue;
            data = precioPorNombreVisibilidad.ExecuteReader();

            var precio = prePrecio.Value;
            data.Close();

            //costo = Double.Parse(precio.ToString());
            costo = float.Parse(precio.ToString(), CultureInfo.InvariantCulture.NumberFormat);

            string query = "select Usuario_nuevo from PERSISTIENDO.Usuario where Usuario_username like '" + usuario.username + "'";



            SqlCommand comando = new SqlCommand(query, coneccion);
            DataTable tabla7 = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = comando;
            adapter.Fill(tabla7);


            if (tabla7.Rows[0][0].ToString().Equals("True"))
            {

                costo = 0;

            }




            if (envia)
            {

                envioPorNombreVisibilidad = new SqlCommand("PERSISTIENDO.envioPorVisibilidad", coneccion);

                envioPorNombreVisibilidad.CommandType = CommandType.StoredProcedure;
                envioPorNombreVisibilidad.Parameters.Add("@Detalle", SqlDbType.VarChar).Value = tipoVisibilidad;
                var preEnvio = envioPorNombreVisibilidad.Parameters.Add("@Valor", SqlDbType.Int);
                preEnvio.Direction = ParameterDirection.ReturnValue;
                data = envioPorNombreVisibilidad.ExecuteReader();

                var precioEnvio = preEnvio.Value;
                data.Close();


                envio = float.Parse(precioEnvio.ToString(), CultureInfo.InvariantCulture.NumberFormat);


            }

            costoTotal = (costo).ToString();


            label4.Text = "$" + costoTotal;

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.Text.Equals("No"))
            {
                envia = false;
            }
            else
            {
                envia = true;
            }
            actualizar_costo();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            envioHabilitado = new SqlCommand("PERSISTIENDO.envioValido", coneccion);

            envioHabilitado.CommandType = CommandType.StoredProcedure;
            envioHabilitado.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = comboBox2.Text;
            var preEnvio = envioHabilitado.Parameters.Add("@Valor", SqlDbType.Bit);
            preEnvio.Direction = ParameterDirection.ReturnValue;
            data = envioHabilitado.ExecuteReader();

            var envioHabi = preEnvio.Value;
            data.Close();

            if (((int)envioHabi == 1) && estado.Equals("Borrador")) 
            {
                comboBox6.Enabled = true;
            }
            else
            {
                comboBox6.Text = "No";
                comboBox6.Enabled = false;
            }

            if (comboBox2.Text.Equals("Subasta") && estado.Equals("Borrador"))
            {
                textBox2.Text = "1";
                textBox2.Enabled = false;
                label5.Text = "Precio Inicial";
            }
            else if(!comboBox2.Text.Equals("Subasta") && estado.Equals("Borrador"))
            {
                textBox2.Enabled = true;
                label5.Text = "Precio";
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoVisibilidad = comboBox4.Text;
            actualizar_costo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("No"))
            {
                preguntas = false;
            }
            else
            {
                preguntas = true;
            }
        }

    }
}
