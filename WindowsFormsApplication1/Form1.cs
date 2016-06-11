using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        Form2 form2 = new Form2();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            validarCampos();

        }

        private void validarCampos()
        {
            if (string.IsNullOrEmpty(textBox1.Text) | string.IsNullOrEmpty(textBox2.Text))
            {
                String mensaje = "Los campos Username y Password son obligatorios";
                String caption = "Ingrese Username y Password";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);


            }
            else
            {
                form2.Show();

            }

        }
    }
}