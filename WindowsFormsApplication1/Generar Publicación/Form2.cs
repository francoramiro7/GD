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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication1.Form2 form2 = new WindowsFormsApplication1.Form2();
            form2.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Generar_Publicación.Form1 generar = new Generar_Publicación.Form1();
            generar.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Generar_Publicación.Form4 form4 = new Generar_Publicación.Form4();
            form4.Show();
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
