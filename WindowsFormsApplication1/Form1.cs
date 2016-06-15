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
using System.Configuration;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
       

        SqlConnection coneccion;
        SqlCommand validarUsuario, validarContra, cantidadRoles, validarIntentos, 
            actualizarIntentos, resetearIntentos, bloquearUsuario, validarBloqueo, esAdmin, roles;
        SqlDataReader data;
        String username;

        
        public Form1()
        {
            

            InitializeComponent();
            coneccion = new SqlConnection(@"Data Source=localhost\SQLSERVER2012;Initial Catalog=GD1C2016;Persist Security Info=True;User ID=gd;Password=gd2016");
            coneccion.Open();
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
            if (validarCampos())
            {
                
              
               
                validarUsuario = new SqlCommand("PERSISTIENDO.ValidarUsuario", coneccion);
               
                validarUsuario.CommandType = CommandType.StoredProcedure;
                validarUsuario.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
               
                var resultado = validarUsuario.Parameters.Add("@Valor", SqlDbType.Int);
                resultado.Direction = ParameterDirection.ReturnValue;
              data = validarUsuario.ExecuteReader();
              data.Close();

                var resultado2 = resultado.Value;
                
               // validarbloquead;
                 validarBloqueo = new SqlCommand("PERSISTIENDO.estaBloqueado", coneccion);
               
                validarBloqueo.CommandType = CommandType.StoredProcedure;
                validarBloqueo.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
               
                var bloq = validarBloqueo.Parameters.Add("@Valor", SqlDbType.Int);
                bloq.Direction = ParameterDirection.ReturnValue;
              data =  validarBloqueo.ExecuteReader();
              data.Close();
            

                var bloqueado = bloq.Value;
                if ((int)resultado2 == 1){
                    if((int)bloqueado == 1){
                                 

                        validarIntentos= new SqlCommand("PERSISTIENDO.intentosFallidos", coneccion);

                        validarIntentos.CommandType = CommandType.StoredProcedure;
                        validarIntentos.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
                    

                        var resultadoIntentos = validarIntentos.Parameters.Add("@Valor", SqlDbType.Int);
                        resultadoIntentos.Direction = ParameterDirection.ReturnValue;
                        data = validarIntentos.ExecuteReader();
                       
                            
                            
                        var resultadoIntentos2= resultadoIntentos.Value;

                        data.Close();
                        if (((int)resultadoIntentos2) < 3)
                        {

                            validarContra = new SqlCommand("PERSISTIENDO.ValidarContra", coneccion);

                            validarContra.CommandType = CommandType.StoredProcedure;
                            validarContra.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
                            validarContra.Parameters.Add("@Password", SqlDbType.VarChar).Value = textBox2.Text;

                            var resultadoC = validarContra.Parameters.Add("@Valor", SqlDbType.Int);
                            resultadoC.Direction = ParameterDirection.ReturnValue;
                            data = validarContra.ExecuteReader();
                            data.Close();
                            var resultadoContra = resultadoC.Value;



                            if ((int)resultadoContra == 1)
                            {
                                resetearIntentos = new SqlCommand("PERSISTIENDO.resetearIntentoFallidos", coneccion);

                                resetearIntentos.CommandType = CommandType.StoredProcedure;
                                resetearIntentos.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;

                                resetearIntentos.ExecuteNonQuery();
                               
                                encontrarRoles();
                                usuario.username = textBox1.Text;

                            }
                            else
                            {

                                actualizarIntentos = new SqlCommand("PERSISTIENDO.agregarIntentoFallidos", coneccion);

                                actualizarIntentos.CommandType = CommandType.StoredProcedure;
                                actualizarIntentos.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;
                            
                                actualizarIntentos.ExecuteNonQuery();

                                if ((((int)resultadoIntentos2) + 1) > 2)
                                {

                                   bloquearUsuario = new SqlCommand("PERSISTIENDO.bloquearUsuario", coneccion);

                                    bloquearUsuario.CommandType = CommandType.StoredProcedure;
                                    bloquearUsuario.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;

                                   bloquearUsuario.ExecuteNonQuery();
                                }

                            
                                String mensaje = "Password incorrecto, ha perdido un intento";
                                String caption = "Error en iniciar sesion";
                                textBox1.Clear();
                                textBox2.Clear();
                                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                            }
                        }
                    }
                    else
                    {
                        String mensaje = "El usuario esta bloqueado, contactar administrador 0810-999-admin";
                        String caption = "Error en iniciar sesion";
                        textBox1.Clear();
                        textBox2.Clear();
                        MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                    }
                }

                else
                {
                    String mensaje = "Username incorrecto, intetelo de nuevo";
                    String caption = "Error en iniciar sesion";
                    textBox1.Clear();
                    textBox2.Clear();
                    MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                }
            

            }
           
          
            
            

        }




        
        private Boolean validarCampos()
        {
            if (string.IsNullOrEmpty(textBox1.Text) | string.IsNullOrEmpty(textBox2.Text))
            {
                String mensaje = "Los campos Username y Password son obligatorios";
                String caption = "Ingrese Username y Password";
                MessageBox.Show(mensaje, caption, MessageBoxButtons.OK);
                return false;
                


   }
            else
            {

                return true;
            }

        }

        private void encontrarRoles()
        {

            
            esAdmin = new SqlCommand("PERSISTIENDO.esAdministrador", coneccion);

            esAdmin.CommandType = CommandType.StoredProcedure;
            esAdmin.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;


            var resultadoIntentos = esAdmin.Parameters.Add("@Valor", SqlDbType.Int);
            resultadoIntentos.Direction = ParameterDirection.ReturnValue;
            data = esAdmin.ExecuteReader();
            var resultadoIntentos2 = resultadoIntentos.Value;
            data.Close();

            roles = new SqlCommand("PERSISTIENDO.Nombreroles", coneccion);

            roles.CommandType = CommandType.StoredProcedure;
            roles.Parameters.Add("@Username", SqlDbType.VarChar).Value = textBox1.Text;

            SqlDataAdapter adapter = new SqlDataAdapter(roles);
            DataTable tablaRoles = new DataTable();
            
            adapter.Fill(tablaRoles);
            SqlDataReader reader = roles.ExecuteReader();
            List<string> roleslist = new List<string>();
           
            comboBox1.DataSource = tablaRoles;
            comboBox1.DisplayMember = "Rol_nombre";


            if (((int)resultadoIntentos2) == 1)
            {
                DataRow dr = tablaRoles.NewRow();
                dr["Rol_nombre"] = "Administrador";
                
                roleslist.Add("Administrador");

                tablaRoles.Rows.InsertAt(dr, 0);
              }

            if (comboBox1.Items.Count == 1)
            {


                String rol;
                if (roleslist.Count != 0)
                {
                    rol = roleslist.First();
                }
                else
                {
                    rol = (tablaRoles.Rows[0]["Rol_nombre"]).ToString();
                }

                
                usuario.Rol = rol;

                Form2 form = new Form2();
                form.Show();
                this.Hide();
            }
            else
            {
                button1.Visible = false;
                label9.Visible = true;
                comboBox1.Visible = true;
                button2.Visible = true;
            }



           
 



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            
            

            usuario.Rol = comboBox1.Text;
            Form2 form = new Form2();
            form.Show();
            this.Hide();
            
        }

        private void label10_Click(object sender, EventArgs e)
        {
           
        }



    }
}