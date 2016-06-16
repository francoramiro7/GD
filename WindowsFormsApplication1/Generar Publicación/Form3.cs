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
        String descripcion;
        String precio;
        String stock;
        bool envia = false;
        String tipoVisibilidad = "";
        bool preguntas = true;
        SqlConnection coneccion;
        SqlCommand nombresVisibilidad, rubros, precioPorNombreVisibilidad, envioPorNombreVisibilidad, envioHabilitado, publicar,
            codigoRubro, codigoTipo, codigoVisibilidad, codigoEstado, ultimaPublicacion;
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
                publicar.Parameters.Add("@CodigoPublicacion", SqlDbType.Float).Value = (float.Parse(codPublicacion.ToString(), CultureInfo.InvariantCulture.NumberFormat) + 1);
                publicar.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = textBox1.Text;
                publicar.Parameters.Add("@Stock", SqlDbType.Int).Value = textBox2.Text;
                publicar.Parameters.Add("@Precio", SqlDbType.Float).Value = textBox5.Text;
                publicar.Parameters.Add("@Rubro", SqlDbType.Int).Value = (int)codRubro;
                publicar.Parameters.Add("@Envio", SqlDbType.Bit).Value = envia;
                publicar.Parameters.Add("@Tipo", SqlDbType.Int).Value = (int)codTipo;
                publicar.Parameters.Add("@Preguntas", SqlDbType.Bit).Value = preguntas;
                publicar.Parameters.Add("@Visibilidad", SqlDbType.Int).Value = (int)codVisibilidad;
                publicar.Parameters.Add("@Estado", SqlDbType.Int).Value = (int)codEstado;

                publicar.ExecuteNonQuery();

                Generar_Publicación.Form2 form2 = new Generar_Publicación.Form2();
                form2.Show();
                this.Close();

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

            comboBox5.DisplayMember = "Rubro_Descripcion"; //CAMBIARR!!!
            comboBox5.SelectedIndex = comboBox5.Items.IndexOf("New");

            comboBox4.DisplayMember = "Visibilidad_descripcion"; //CAMBIARR!!!
            comboBox4.SelectedIndex = comboBox4.Items.IndexOf("New");

            comboBox2.DisplayMember = "Visibilidad_descripcion"; //CAMBIARR!!!
            comboBox2.SelectedIndex = comboBox2.Items.IndexOf("New");

            comboBox1.DisplayMember = "Visibilidad_descripcion"; //CAMBIARR!!!
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf("New");

            comboBox6.DisplayMember = "Visibilidad_descripcion"; //CAMBIARR!!!
            comboBox6.SelectedIndex = comboBox6.Items.IndexOf("New");


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

            float costo = 0;
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

            if ((int)envioHabi == 1)
            {
                comboBox6.Enabled = true;
            }
            else
            {
                comboBox6.Text = "No";
                comboBox6.Enabled = false;
            }

            if (comboBox2.Text.Equals("Subasta"))
            {
                textBox2.Text = "1";
                textBox2.Enabled = false;
                label5.Text = "Precio Inicial";
            }
            else
            {
                textBox2.Text = "";
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
