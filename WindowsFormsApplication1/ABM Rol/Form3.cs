﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace WindowsFormsApplication1.ABM_Rol
{
    public partial class Form3 : Form
    {

        SqlConnection coneccion;
        SqlDataReader data;
        SqlCommand cargarRoles, cargarFunc, fpr, existeRol, cambiarN, eliminar, crearFunc, codigoRol, codigoFunc;
        List<String> funcion = new List<String>();
        List<String> funcionesViejas = new List<String>();
        
        string rol;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
            cargarRoles = new SqlCommand("PERSISTIENDO.cargarRoles", coneccion);

            cargarRoles.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cargarRoles);
            DataTable tablaRoles = new DataTable();

            coneccion.Close();
            adapter.Fill(tablaRoles);
            comboBox2.DataSource = tablaRoles;
            comboBox2.DisplayMember = "Rol_nombre";

            cargarFuncionalidades();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rol = comboBox2.Text.ToString();
            label3.Visible = true;
            label6.Visible = true;
            textBox1.Visible = true;
            listBox1.Visible = true;
            listBox2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            textBox1.Text = rol;
            button2.Visible = true;

            cargarFuncionalidadesPorRol(rol);

   
        }

       
        
        private void cargarFuncionalidadesPorRol(String rol) {

            List<String> funcionalidades = new List<string>();
            listBox2.Items.Clear();
            funcionesViejas.Clear();
            funcionalidades.Clear();
            funcion.Clear();
            coneccion.Open();
            fpr = new SqlCommand("PERSISTIENDO.FuncionalidadesPorRol", coneccion);

            fpr.CommandType = CommandType.StoredProcedure;
            fpr.Parameters.Add("@Rol", SqlDbType.VarChar).Value = rol;

            SqlDataAdapter adapter = new SqlDataAdapter(fpr);
            SqlDataReader reader = fpr.ExecuteReader();
            
                while (reader.Read())
                {
                    funcionalidades.Add(reader.GetString(0)); //Specify column index 
                }

                
           
                listBox2.Items.AddRange(funcionalidades.ToArray());
                reader.Close();

            listBox2.DisplayMember = "Func_nombre";
            coneccion.Close();

            for (int i = 0; i < listBox2.Items.Count; i++)
            {

                string text = listBox2.GetItemText(listBox2.Items[i]);

                funcion.Add(text);
                funcionesViejas.Add(text);
            }

        }

        private void validarCampos()
        {

          if (string.IsNullOrEmpty(textBox1.Text) || (int)listBox2.Items.Count == 0)
           {
                String mensaje = "Los campos nombre y funcionalidades son obligatorios";
                String caption = "Error al modificar el rol";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
            
            else
            {
                coneccion.Open();
                existeRol = new SqlCommand("PERSISTIENDO.existeRol", coneccion);
                existeRol.CommandType = CommandType.StoredProcedure;
                existeRol.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox1.Text;
                var resultado = existeRol.Parameters.Add("@Valor", SqlDbType.Int);
                resultado.Direction = ParameterDirection.ReturnValue;
                data = existeRol.ExecuteReader();
                var existeR = resultado.Value;
                data.Close();
                coneccion.Close();

                if ((int)existeR == 1 && !(rol.Equals(textBox1.Text)))
                {
                    String mensaje = "El rol ya existe, ingrese otro nombre";
                    String caption = "Error al modificar el rol";
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                }
                else
                    modificarRol();

            }

        }

        private void modificarRol() {

                coneccion.Open();
                cambiarN = new SqlCommand("PERSISTIENDO.modificarRol", coneccion);
                cambiarN.CommandType = CommandType.StoredProcedure;
                cambiarN.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox1.Text;
                cambiarN.Parameters.Add("@anterior", SqlDbType.VarChar).Value = rol;
                cambiarN.ExecuteNonQuery();


                codigoRol = new SqlCommand("PERSISTIENDO.codigoRol", coneccion);
                codigoRol.CommandType = CommandType.StoredProcedure;
                codigoRol.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox1.Text;
                var resultado = codigoRol.Parameters.Add("@Valor", SqlDbType.Int);
                resultado.Direction = ParameterDirection.ReturnValue;
                data = codigoRol.ExecuteReader();
               

                var codi = resultado.Value;
                int codigo = (int)codi;
                data.Close();

                eliminar = new SqlCommand("PERSISTIENDO.eliminarFuncionalidades", coneccion);
                eliminar.CommandType = CommandType.StoredProcedure;
                eliminar.Parameters.Add("@rol", SqlDbType.Int).Value = codigo;
                eliminar.ExecuteNonQuery();
                coneccion.Close();

                List<int> codigos = new List<int>();


                for (int i = 0; i < funcion.Count(); i++)
                {
                    coneccion.Open();
                    codigoFunc = new SqlCommand("PERSISTIENDO.codigoFuncionalidad", coneccion);
                    codigoFunc.CommandType = CommandType.StoredProcedure;
                    codigoFunc.Parameters.Add("@nombre", SqlDbType.VarChar).Value = funcion.ElementAt(i).ToString();
                    var resultado2 = codigoFunc.Parameters.Add("@Valor", SqlDbType.Int);
                    resultado2.Direction = ParameterDirection.ReturnValue;
                    data = codigoFunc.ExecuteReader();
                    var codigo2 = resultado2.Value;
                    int aniadir = (int)codigo2;
                    codigos.Add(aniadir);
                    data.Close();
                    coneccion.Close();
                                    }


                for (int i = 0; i < codigos.Count(); i++)
                {

                    coneccion.Open();
                    crearFunc = new SqlCommand("PERSISTIENDO.crearFuncionalidad", coneccion);
                    crearFunc.CommandType = CommandType.StoredProcedure;
                    crearFunc.Parameters.Add("@codigoRol", SqlDbType.VarChar).Value = codigo;
                    crearFunc.Parameters.Add("@codigoFunc", SqlDbType.Int).Value = codigos.ElementAt(i);
                    crearFunc.ExecuteNonQuery();
                    coneccion.Close();

                }

                String mensaje = "El rol se ha modificado correctamente";
                String caption = "Rol modificado";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);


                
                

            

          
        }



        
        
        
        private void cargarFuncionalidades()
        {

            coneccion.Open();
            cargarFunc = new SqlCommand("PERSISTIENDO.listarFuncionalidades", coneccion);

            cargarFunc.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cargarFunc);
            DataTable tablaRoles = new DataTable();

            adapter.Fill(tablaRoles);
            SqlDataReader reader = cargarFunc.ExecuteReader();

            listBox1.DataSource = tablaRoles;
            listBox1.DisplayMember = "Func_nombre";
            coneccion.Close();

            




        }

        private void button3_Click(object sender, EventArgs e)
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);

            if (funcion.Contains(text))
            {

                String mensaje = "Esta funcionalidad ya ha sido ingresada";
                String caption = "Funcionalidad duplicada";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);

            }
            else
            {

                listBox2.DisplayMember = "Func_nombre";
                listBox2.Items.Add((DataRowView)listBox1.SelectedItem);

                funcion.Add(text);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = listBox2.GetItemText(listBox2.SelectedItem);
            listBox2.Items.Remove(listBox2.SelectedItem);

            funcion.Remove(text);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            ABM_Rol.Form1 accionesRol = new ABM_Rol.Form1();
            accionesRol.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            validarCampos();
        }
    }
}
