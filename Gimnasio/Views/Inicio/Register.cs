using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gimnasio.Views
{
    public partial class Register : Form
    {
        
        public Register()
        {
            InitializeComponent();
			this.KeyPreview = true; // Puesto para que pueda capturar eventos del teclado
		}

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txApellidos_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            BorrarCampos();
            lblError.ResetText();

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

		private void btnRegister_Click(object sender, EventArgs e)
        {
            if (compruebaCampos() == false)
            {

            }
            else if (new Negocio.Management.UsuariosManagement().ExisteUsuarioEmail(tbEmail.Text).GMAIL != null)
            {
                lblError.Text = "El email ya esta registrado";
            }
            else if (new Negocio.Management.UsuariosManagement().ExisteUsuarioDNI(tbDNI.Text).DNI != null)
            {
                lblError.Text = "El DNI ya esta registrado";
            }
            else
            {
                registro();
                lblError.ForeColor = Color.FromArgb(0, 143, 57);

                lblError.Text = "Registro exitoso";
                // BorrarCampos();

				MessageBox.Show("Se ha completado el registro con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Login l = new Login();
				l.Show();
				this.Close();

			}
		}

        private void registro()
        {
            Negocio.EntitiesDTO.UsuariosDTO user = new Negocio.EntitiesDTO.UsuariosDTO();
            user.NOMBRE = tbNombre.Text;
            user.APELLIDOS = tbApellidos.Text;

			user.TELEFONO = 0;
			if (tbTelefono.TextLength != 0) { user.TELEFONO = int.Parse(tbTelefono.Text); }

			user.DIRECCION = tbDireccion.Text;
            user.DNI = tbDNI.Text;
            user.GMAIL = tbEmail.Text;
            user.PASS = tbRepiteContraseña.Text;
            user.CCC = tbCuentaCorriente.Text;
            user.PRIVILEGIO = "USUARIO";
            new Negocio.Management.UsuariosManagement().AltaUsuario(user);
        }

        private void BorrarCampos()
        {
            tbNombre.ResetText();
            tbApellidos.ResetText();
            tbTelefono.ResetText();
            tbDireccion.ResetText();
            tbDNI.ResetText();
            tbEmail.ResetText();
            tbCuentaCorriente.ResetText();
            tbRepiteContraseña.ResetText();
            tbContraseña.ResetText();
            
        }

        private Boolean compruebaCampos()
        {
            lblError.ForeColor = Color.FromArgb(255, 0, 0);
            if (tbNombre.TextLength != 0)
            {
                if (tbApellidos.TextLength != 0)
                {
                    if (Regex.IsMatch(tbTelefono.Text, @"^\d{9}$") || tbTelefono.TextLength == 0)
                    {
                        if (Regex.IsMatch(tbDNI.Text, @"^\d{8}[A-Za-z]$"))
                        {
                            if (Regex.IsMatch(tbEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                            {
                                if (Regex.IsMatch(tbContraseña.Text, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[%&$/\*])[A-Za-z\d%&$/\*]{8,}$"))
                                {
                                    if (Regex.IsMatch(tbCuentaCorriente.Text, @"^ES\d{2}\d{20}$"))
                                    {
                                        if (tbContraseña.Text == tbRepiteContraseña.Text)
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            lblError.Text = "Las contraseñas no coinciden";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        lblError.Text = "Cuenta corriente no válida";
                                        return false;
                                    }
                                }
                                else
                                {
                                    lblError.Text = "Contraseña no válida, debe contener al menos una mayúscula, una minúscula, un número , 8 caracteres y un carácter especial de este tipo: % & $ / *";
                                    return false;
                                }
                            }
                            else
                            {
                                lblError.Text = "Email no válido";
                                return false;
                            }
                        }
                        else
                        {
                            lblError.Text = "DNI incorrecto";
                            return false;
                        }

                    }
                    else
                    {
                        lblError.Text = "Número de teléfono no válido";
                        return false;
                    }
                }
                else
                {
                    lblError.Text = "El apellido no debe de estar vacio";
                    return false;
                }
            }
            else
            {
                lblError.Text = "El nombre no debe de estar vacío";
                return false;
            }
        }

		private Boolean verificarBotonRegistroEnabled()
		{
            if (tbNombre.TextLength != 0 &&
                tbApellidos.TextLength != 0 &&
                Regex.IsMatch(tbDNI.Text, @"^\d{8}[A-Za-z]$") &&
                Regex.IsMatch(tbEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") &&
                Regex.IsMatch(tbContraseña.Text, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[%&$/\*])[A-Za-z\d%&$/\*]{8,}$") &&
                Regex.IsMatch(tbCuentaCorriente.Text, @"^ES\d{2}\d{20}$") && Regex.IsMatch(tbRepiteContraseña.Text, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[%&$/\*])[A-Za-z\d%&$/\*]{8,}$")) {

                return true;
            }

            return false;
		}

		private void btnMostrarOcultar_Click(object sender, EventArgs e)
        {
            if (tbContraseña.PasswordChar == '*')
            {
                tbContraseña.PasswordChar = '\0';
                tbRepiteContraseña.PasswordChar = '\0';
                btnMostrarOcultar.Image = Properties.Resources.invisible;

            }
            else
            {
                tbContraseña.PasswordChar = '*';
                tbRepiteContraseña.PasswordChar = '*';
                btnMostrarOcultar.Image = Properties.Resources.ojo1;
            }
        }

		private void Register_KeyUp(object sender, KeyEventArgs e)
		{
            if(verificarBotonRegistroEnabled())
            {
                btnRegister.Enabled = true;
            } else
            {
                btnRegister.Enabled = false;
            }
		}
	}
}
