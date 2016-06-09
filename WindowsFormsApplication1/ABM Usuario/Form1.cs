using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.ABM_Usuario
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
            


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            crearCamposUser();
        }

        private void crearCamposUser()
        {
            Label labelnombre = new Label();
            this.Controls.Add(labelnombre);
            labelnombre.Text = "(*) Nombre";
           labelnombre.Font = new Font("Tahoma", 14);
           labelnombre.AutoSize = true;
        labelnombre.Location = new System.Drawing.Point(85, 150);

           this.Controls.Add(labelnombre);

           Label labelApellido = new Label();
           this.Controls.Add(labelApellido);
           labelApellido.Text = "(*) Apellido";
           labelApellido.Font = new Font("Tahoma", 14);
           labelApellido.AutoSize = true;
           labelApellido.Location = new System.Drawing.Point(85, 180);

           Label labelDNI = new Label();
           this.Controls.Add(labelDNI);
           labelDNI.Text = "(*) DNI";
           labelDNI.Font = new Font("Tahoma", 14);
           labelDNI.AutoSize = true;
           labelDNI.Location = new System.Drawing.Point(85, 210);

           Label labeltipo = new Label();
           this.Controls.Add(labeltipo);
           labeltipo.Text = "(*) Tipo de Documento";
           labeltipo.Font = new Font("Tahoma", 14);
           labeltipo.AutoSize = true;
           labeltipo.Location = new System.Drawing.Point(85, 240);

           Label labelMail = new Label();
           this.Controls.Add(labelMail);
           labelMail.Text = "(*) Mail";
           labelMail.Font = new Font("Tahoma", 14);
           labelMail.AutoSize = true;
           labelMail.Location = new System.Drawing.Point(85, 270);

           Label labelTelef = new Label();
           this.Controls.Add(labelTelef);
           labelTelef.Text = "(*) Mail";
           labelTelef.Font = new Font("Tahoma", 14);
           labelTelef.AutoSize = true;
           labelTelef.Location = new System.Drawing.Point(85, 300);

           Label labelCalle = new Label();
           this.Controls.Add(labelCalle);
           labelCalle.Text = "(*) Mail";
           labelCalle.Font = new Font("Tahoma", 14);
           labelCalle.AutoSize = true;
           labelCalle.Location = new System.Drawing.Point(85, 330);

       
            
        }
    }
}
