using InventarioApp.DB;
using InventarioApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
	/// Lógica de interacción para ProveedorWindow.xaml
	/// </summary>
	public partial class ProveedorWindow : Window
	{
		public ProveedorWindow()
		{
			InitializeComponent();
		}

		StockmanagerdbContext db = new StockmanagerdbContext();
		public ProveedorDetailModel model;

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (model != null && model.proveedor_id != " ")
			{
				txtProveedorId.Text = model.proveedor_id.ToString();
				txtProveedorNombre.Text = model.proveedor_nombre;
				txtNumeroTelefono.Text = model.numerotelefono;
				txtEmail.Text = model.email;
				txtDireccion.Text = model.direccion;
				txtProveedorId.IsReadOnly = true;
			}
		}

		private void btnGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtProveedorId.Text) || string.IsNullOrWhiteSpace(txtProveedorNombre.Text) ||
				string.IsNullOrWhiteSpace(txtNumeroTelefono.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) ||
				string.IsNullOrWhiteSpace(txtDireccion.Text))
			{
				MessageBox.Show("Debe completar todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			// Validación para ProveedorId (alfanumérico)
			if (!Regex.IsMatch(txtProveedorId.Text, @"^[a-zA-Z0-9]+$"))
			{
				MessageBox.Show("El 'Proveedor Id' debe ser alfanumérico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validación para NumeroTelefono (solo números)
			if (!Regex.IsMatch(txtNumeroTelefono.Text, @"^\d+$"))
			{
				MessageBox.Show("El 'Número de Teléfono' debe contener solo números.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validación para Email (formato correcto)
			if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
			{
				MessageBox.Show("El 'Email' no tiene un formato válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			try
			{
				if (model != null && model.proveedor_id != " ")
				{
					// Actualizar proveedor
					Proveedor proveedor = db.Proveedors.Find(model.proveedor_id);
					proveedor.ProveedorNombre = txtProveedorNombre.Text;
					proveedor.Numerotelefono = txtNumeroTelefono.Text;
					proveedor.Email = txtEmail.Text;
					proveedor.Direccion = txtDireccion.Text;
					db.SaveChanges();
					MessageBox.Show("El proveedor fue actualizado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCampos();
					txtProveedorId.IsReadOnly = false;
				}
				else
				{
					// Guardar nuevo proveedor
					Proveedor proveedor = new Proveedor();
					proveedor.ProveedorId = txtProveedorId.Text;
					proveedor.ProveedorNombre = txtProveedorNombre.Text;
					proveedor.Numerotelefono = txtNumeroTelefono.Text;
					proveedor.Email = txtEmail.Text;
					proveedor.Direccion = txtDireccion.Text;
					db.Proveedors.Add(proveedor);
					db.SaveChanges();
					MessageBox.Show("El proveedor fue guardado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCampos();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("No se pudo guardar el proveedor. Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void LimpiarCampos()
		{
			txtProveedorId.Clear();
			txtProveedorNombre.Clear();
			txtNumeroTelefono.Clear();
			txtEmail.Clear();
			txtDireccion.Clear();
		}

		private void btnCerrar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
