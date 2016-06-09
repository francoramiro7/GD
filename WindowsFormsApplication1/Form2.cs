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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            IsMdiContainer = true;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ABM_Usuario.Form1 abmUser = new ABM_Usuario.Form1();
            abmUser.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ABM_Rol.Form1 abmRol = new ABM_Rol.Form1();
            abmRol.Show();
            this.Hide();
        }
    }
}
