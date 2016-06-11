using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.ABM_Rol
{
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); 
            ABM_Rol.Form1 accionesRol = new ABM_Rol.Form1();
            accionesRol.Show();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            validarCampoRol();
        }
        private void validarCampoRol()
        {

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                String mensaje = "Ingrese el nombre del Rol a crear";
                String caption = "Nombre de Rol obligatorio";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
            }
        }
    }
}