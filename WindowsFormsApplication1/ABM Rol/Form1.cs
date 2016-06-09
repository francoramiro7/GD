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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ABM_Rol.Form2 crear = new ABM_Rol.Form2();
            crear.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ABM_Rol.Form3 modificar= new ABM_Rol.Form3();
            modificar.Show();
            this.Hide();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ABM_Rol.Form4 eliminar = new ABM_Rol.Form4();
            eliminar.Show();
            this.Hide();
        }
    }
}
