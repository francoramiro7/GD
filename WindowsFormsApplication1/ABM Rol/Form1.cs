﻿using System;
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
        ABM_Rol.Form2 crearRol = new ABM_Rol.Form2();
        ABM_Rol.Form3 modificarRol = new ABM_Rol.Form3();
        ABM_Rol.Form4 eliminarRol = new ABM_Rol.Form4();

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            crearRol.Show();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            modificarRol.Show();
            this.Close();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            eliminarRol.Show();
            this.Close();
        }
    }
}
