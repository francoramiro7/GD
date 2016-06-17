using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Generar_Publicación
{
    public partial class Form5 : Form
    {
        public Form5(string det, string visi ,string precio,DateTime fech, DateTime venci, string cod)
        {
            InitializeComponent();

            label7.Text = usuario.username;
            label8.Text = det;
            label9.Text = visi;
            label10.Text = "$"+precio;
            label11.Text = fech.ToShortDateString();
            label12.Text = venci.ToShortDateString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
