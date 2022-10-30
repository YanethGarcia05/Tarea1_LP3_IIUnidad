using Datos;

namespace Tarea1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void AceptarButton_Click(object sender, EventArgs e)
        {
            if (CorreoTextBox.Text == String.Empty)
            {
                errorProvider1.SetError(CorreoTextBox, "Ingrese el correo electronico del usuario");
                CorreoTextBox.Focus();
                return;
            }
            errorProvider1.Clear();
            if (ContraseñaTextBox.Text == String.Empty)
            {
                errorProvider1.SetError(ContraseñaTextBox, "Ingrese una contraseña");
                ContraseñaTextBox.Focus();
                return;
            }
            errorProvider1.Clear();
            UsuarioDatos userDatos = new UsuarioDatos();

            bool valido = await userDatos.LoginAsync(CorreoTextBox.Text, ContraseñaTextBox.Text);
            if (valido)
            {
                RespuestaCorrecta formulario = new RespuestaCorrecta();
                Hide();
                formulario.Show();
            }
            else
            {
                MessageBox.Show("Datos de usuario incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}