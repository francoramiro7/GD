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

namespace WindowsFormsApplication1.ComprarOfertar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
        }
        
        private SqlCommand command { get; set; }
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        public Object SelectedItem { get; set; }


        SqlConnection coneccion;
        SqlCommand rubros,getPublicaciones;
        SqlDataReader data;

        int pag = 0;
        int pagTotal;
        int totalRows = 0;


        DataTable tablaTemporal;
        int totalPaginas;
        int totalPublicaciones;
        int publicacionesPorPagina = 10;
        int paginaActual;
        int ini;
        int fin;

                
        private void Form1_Load(object sender, EventArgs e)
        {            
            CargarRubros();            
        }

        private void CargarRubros()
        {

            rubros = new SqlCommand("PERSISTIENDO.listarRubros", coneccion);
            rubros.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapterRubro = new SqlDataAdapter(rubros);
            DataTable tablaRubros = new DataTable();
            adapterRubro.Fill(tablaRubros);
            comboBoxRubro.DataSource = tablaRubros;
            comboBoxRubro.DisplayMember = "Rubro_Descripcion";
            comboBoxRubro.SelectedIndex = comboBoxRubro.Items.IndexOf("New");

        }

        private void botonBuscar_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            //parametros = new List<SqlParameter>();
            //parametros.Clear();
            //parametros.Add(new SqlParameter("@usuario", idUsuarioActual));

            DataTable busquedaTemporal = new DataTable();

            getPublicaciones = new SqlCommand("PERSISTIENDO.getPublicaciones", coneccion);
            getPublicaciones.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(getPublicaciones);
            adapter.Fill(busquedaTemporal);

            int cantFilas = busquedaTemporal.Rows.Count;
            if (cantFilas == 0)
            {
                MessageBox.Show("No hay resultados");
                return;
            }
            else
            {
                tablaTemporal = busquedaTemporal;
                calcularPaginas();
                ini = 0;
                if (totalPublicaciones > 9)
                {
                    fin = 9;
                }
                else
                {
                    fin = totalPublicaciones;
                }
                calcularPaginas();
                dataGridView1.DataSource = paginarDataGridView(ini, fin);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                mostrarNrosPaginas(ini);
            }
            AgregarBotonVerPublicacion();
        }

        private void calcularPaginas()
        {
            totalPublicaciones = tablaTemporal.Rows.Count - 1;
            totalPaginas = totalPublicaciones / publicacionesPorPagina;
            if ((totalPublicaciones / publicacionesPorPagina) > 0)
            {
                totalPaginas += 1;
            }
        }

        private DataTable paginarDataGridView(int ini, int fin)
        {
            DataTable publicacionesDeUnaPagina = new DataTable();
            publicacionesDeUnaPagina = tablaTemporal.Clone();
            for (int i = ini; i <= fin; i++)
            {
                publicacionesDeUnaPagina.ImportRow(tablaTemporal.Rows[i]);
            }
            return publicacionesDeUnaPagina;
        }

        private void mostrarNrosPaginas(int ini)
        {
            paginaActual = (ini / publicacionesPorPagina) + 1;
            labelNrosPagina.Text = "Pagina " + paginaActual + "/" + totalPaginas;
        }        

        private bool VerificarSiSeBusco()
        {
            if (totalPaginas == 0)
            {
                MessageBox.Show("Aun no buscaste nada");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool sePuedeRetrocederPaginas()
        {
            if (VerificarSiSeBusco() == false)
            {
                return false;
            }
            else
            {
                if (paginaActual == 1)
                {
                    MessageBox.Show("Ya estas en la 1º pagina");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void botonPrimeraPagina_Click(object sender, EventArgs e)
        {
            if (sePuedeRetrocederPaginas())
            {
                ini = 0;
                fin = 9;
                dataGridView1.DataSource = paginarDataGridView(ini, fin);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                mostrarNrosPaginas(ini);
            }
        }

        private void botonPaginaAnterior_Click(object sender, EventArgs e)
        {
            if (sePuedeRetrocederPaginas())
            {
                ini -= publicacionesPorPagina;
                if (fin != totalPublicaciones)
                {
                    fin -= publicacionesPorPagina;
                }
                else
                {
                    fin = ini + 9;
                }

                dataGridView1.DataSource = paginarDataGridView(ini, fin);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                mostrarNrosPaginas(ini);
            }
        }

        private bool sePuedeAvanzarPaginas()
        {
            if (VerificarSiSeBusco() == false)
            {
                return false;
            }
            else
            {
                if (paginaActual == totalPaginas)
                {
                    MessageBox.Show("Ya estas en la ultima pagina");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void botonPaginaSiguiente_Click(object sender, EventArgs e)
        {
            if (sePuedeAvanzarPaginas())
            {
                ini += publicacionesPorPagina;
                if ((fin + publicacionesPorPagina) < totalPublicaciones)          
                
                {
                    fin += publicacionesPorPagina;                    
                }
                else
                {
                    fin = totalPublicaciones;
                }                
                dataGridView1.DataSource = paginarDataGridView(ini, fin);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                mostrarNrosPaginas(ini);
            }
        }


        private void botonUltimaPagina_Click(object sender, EventArgs e)
        {
            if (sePuedeAvanzarPaginas())
            {
                ini = (totalPaginas - 1) * publicacionesPorPagina;
                fin = totalPublicaciones;
                dataGridView1.DataSource = paginarDataGridView(ini, fin);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                mostrarNrosPaginas(ini);
            }
        }

        private void AgregarBotonVerPublicacion()
        {
            if (dataGridView1.Columns.Contains("Ver Publicacion"))
                dataGridView1.Columns.Remove("Ver Publicacion");
            DataGridViewButtonColumn buttons = new DataGridViewButtonColumn();
            {
                buttons.HeaderText = "Ver Publicacion";
                buttons.Text = "Ver Publicacion";
                buttons.Name = "Ver Publicacion";
                buttons.UseColumnTextForButtonValue = true;
                buttons.AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.AllCells;
                buttons.FlatStyle = FlatStyle.Standard;
                buttons.CellTemplate.Style.BackColor = Color.Honeydew;
                dataGridView1.CellClick +=
                    new DataGridViewCellEventHandler(dataGridView1_CellClick);
            }

            dataGridView1.Columns.Add(buttons);


        }        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 5)
            {
                string cod = "";
                string desc = "";
                string precio = "";
                string rub = "";
                string tip = "";

                
                cod = tablaTemporal.Rows[e.RowIndex][0].ToString();
                desc = tablaTemporal.Rows[e.RowIndex][1].ToString();
                precio = tablaTemporal.Rows[e.RowIndex][2].ToString();
                rub = tablaTemporal.Rows[e.RowIndex][3].ToString();
                tip = tablaTemporal.Rows[e.RowIndex][4].ToString();
                
                ComprarOfertar.Form2 form2 = new ComprarOfertar.Form2(cod, desc, precio, rub, tip);
                form2.Show();
                this.Close();
            }
        }

        private void botonLimpiar_Click(object sender, EventArgs e)
        {
            textBoxDescripcion.Clear();
            comboBoxRubro.SelectedIndex = -1;
            labelNrosPagina.Text = "";
            dataGridView1.DataSource = null;
            if (dataGridView1.Columns.Contains("Ver Publicacion"))
                dataGridView1.Columns.Remove("Ver Publicacion");
        }

        private void botonVolver_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.Form2 form2 = new WindowsFormsApplication1.Form2();
            form2.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
