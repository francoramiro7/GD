﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.ABM_Visibilidad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ABM_Visibilidad.Form2 f2 = new ABM_Visibilidad.Form2();
            this.Close();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ABM_Visibilidad.Form3 f3 = new ABM_Visibilidad.Form3();
            this.Close();
            f3.Show();
        }
    }
}
