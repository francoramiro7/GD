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
    public partial class Form1 : Form
    {

        bool envia = false;
        String tipoVisibilidad = "";
        bool preguntas = true;


        SqlConnection coneccion;
        SqlCommand nombresVisibilidad,rubros, precioPorNombreVisibilidad, envioPorNombreVisibilidad, envioHabilitado,publicar,
            codigoRubro, codigoTipo, codigoVisibilidad, codigoEstado,ultimaPublicacion;
        SqlDataReader data;
        
        public Form1()
        {
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();

            nombresVisibilidad = new SqlCommand("PERSISTIENDO.nombreVisibilidades", coneccion);

            nombresVisibilidad.CommandType = CommandType.StoredProcedure;
            
            SqlDataAdapter adapter = new SqlDataAdapter(nombresVisibilidad);
            DataTable tablavisiblidades = new DataTable();

            adapter.Fill(tablavisiblidades);
            comboBox4.Text = "";
            comboBox4.DataSource = tablavisiblidades;
            comboBox4.DisplayMember = "Visibilidad_descripcion";

            rubros = new SqlCommand("PERSISTIENDO.listarRubros", coneccion);
            rubros.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapterRubro = new SqlDataAdapter(rubros);
            DataTable tablaRubros = new DataTable();
            adapterRubro.Fill(tablaRubros);
            comboBox5.DataSource = tablaRubros;
            comboBox5.DisplayMember = "Rubro_Descripcion";
            comboBox5.Text = "Seleccione rubro";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("No"))
            {
                preguntas = false;
            }
            else {
                preguntas = true;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoVisibilidad = comboBox4.Text;
            actualizar_costo();
        }

        private void actualizar_costo(){

            float costo =0;
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

            costoTotal = (costo + envio).ToString();

            
            textBox6.Text = "$" + costoTotal;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.Text.Equals("No"))
            {
                envia = false;
            }
            else {
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

            var envioHabi= preEnvio.Value;
            data.Close();

            if ((int)envioHabi == 1)
            {
                comboBox6.Enabled = true;
            }
            else {
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

        private void button2_Click(object sender, EventArgs e)
        {


            if (true)
            {

                ultimaPublicacion = new SqlCommand("PERSISTIENDO.ultimaPublicacion", coneccion);

                ultimaPublicacion.CommandType = CommandType.StoredProcedure;
                var up = ultimaPublicacion.Parameters.Add("@Cantidad", SqlDbType.Float);
                up.Direction = ParameterDirection.ReturnValue;
                data = ultimaPublicacion.ExecuteReader();
                var codPublicacion = up.Value;
                data.Close();


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










                publicar = new SqlCommand("PERSISTIENDO.crearPublicacion", coneccion);

                publicar.CommandType = CommandType.StoredProcedure;
                publicar.Parameters.Add("@CodigoPublicacion", SqlDbType.Float).Value = (float.Parse(codPublicacion.ToString(), CultureInfo.InvariantCulture.NumberFormat)+1);
                publicar.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = textBox1.Text;
                publicar.Parameters.Add("@Stock", SqlDbType.Int).Value = textBox2.Text;
                publicar.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Properties.Settings.Default.fecha;
                publicar.Parameters.Add("@Venci", SqlDbType.DateTime).Value = Properties.Settings.Default.fecha;
                publicar.Parameters.Add("@Precio", SqlDbType.Float).Value = textBox5.Text;
                publicar.Parameters.Add("@Rubro", SqlDbType.Int).Value = (int)codRubro;
                publicar.Parameters.Add("@Envio", SqlDbType.Bit).Value = envia;
                publicar.Parameters.Add("@Vendedor", SqlDbType.VarChar).Value = usuario.username;
                publicar.Parameters.Add("@Tipo", SqlDbType.Int).Value = (int)codTipo;
                publicar.Parameters.Add("@Preguntas", SqlDbType.Bit).Value = preguntas;
                publicar.Parameters.Add("@Visibilidad", SqlDbType.Int).Value = (int)codVisibilidad;
                publicar.Parameters.Add("@Estado", SqlDbType.Int).Value = (int)codEstado;

                publicar.ExecuteNonQuery();
               
            }
        }

    }
}
