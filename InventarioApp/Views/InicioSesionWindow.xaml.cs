using InventarioApp.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InventarioApp.Views
{
	/// <summary>
	/// Lógica de interacción para InicioSesionWindow.xaml
	/// </summary>
	public partial class InicioSesionWindow : Window
	{
		StockmanagerdbContext db = new StockmanagerdbContext();

		public InicioSesionWindow()
		{
			InitializeComponent();
		}

		private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
		{
			string nombreUsuario = txtUsuario.Text.Trim().ToLower();
			string contrasena = txtContrasena.Password.Trim();

			// Consulta para verificar el usuario
			var usuario = db.Usuarios
				.FirstOrDefault(u => u.Nombre.ToLower() == nombreUsuario && u.Contrasena == contrasena);

			if (usuario != null)
			{
				// Iniciar sesión exitosamente
				MessageBox.Show("Inicio de sesión exitoso.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

				// Abrir ventana principal
				MainWindow mainWindow = new MainWindow();
				mainWindow.Show();

				// Cerrar ventana de inicio de sesión
				this.Close();
			}
			else
			{
				// Mostrar mensaje de error
				MessageBox.Show("Usuario o contraseña incorrectos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
				txtContrasena.Clear();
			}
		}

		private void btnCancelar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
