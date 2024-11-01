using Negocio.EntitiesDTO;
using Negocio.Management;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gimnasio.Views
{
    public partial class Login : Form
    {
        

        public Login()
        {
            InitializeComponent();
        }

        private void bAtras_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void bBorrar_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
            textBox2.ResetText();
            lError.ResetText(); 
        }

        
        private void bAceptar_Click(object sender, EventArgs e)
        {

            string usuario = textBox1.Text;
            string contrasena = textBox2.Text;

            UsuariosDTO usuComprobar= new UsuariosManagement().ExisteUsuarioEmail(usuario);

            

            if (usuComprobar.GMAIL != null)
            {
                // Añadir hash en la comprobación de contraseña
                // contrasena == usuComprobar.PASS


                if ( Hasher.VerifyPassword(contrasena, usuComprobar.PASS))
                {
                   
                    this.Close();
                    

                }
                else
                {
                     lError.Text = "Contrasena incorrecta";
                }

            }
            else
            {
                lError.Text = "Usuario no existe";
            }
        }

        private void bVisualizarContra_Click(object sender, EventArgs e)
        {
            
            if (textBox2.PasswordChar == '*')
            {
                textBox2.PasswordChar = '\0';
                bVisualizarContra.Image = Properties.Resources.invisible;

            } else
            {
                textBox2.PasswordChar = '*';
                bVisualizarContra.Image = Properties.Resources.ojo1;
            }
        }
        
    }
}
