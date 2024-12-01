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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventarioApp.Views
{
	/// <summary>
	/// Lógica de interacción para ProveedorListView.xaml
	/// </summary>
	public partial class ProveedorListView : UserControl
	{
		private StockmanagerdbContext db = new StockmanagerdbContext();
		private List<ProveedorDetailModel> listaProveedores = new List<ProveedorDetailModel>();

		public ProveedorListView()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			FillDatagrid();
		}

		private void FillDatagrid()
		{
			listaProveedores = db.Proveedors
				.Select(x => new ProveedorDetailModel
				{
					proveedor_id = x.ProveedorId,
					proveedor_nombre = x.ProveedorNombre,
					numerotelefono = x.Numerotelefono,
					email = x.Email,
					direccion = x.Direccion
				}).ToList();
			gridProveedores.ItemsSource = listaProveedores;
		}

		private void btnBuscar_Click(object sender, RoutedEventArgs e)
		{
			// Validación de que al menos un campo de búsqueda esté lleno
			if (string.IsNullOrWhiteSpace(txtProveedorId.Text) &&
				string.IsNullOrWhiteSpace(txtProveedorNombre.Text) &&
				string.IsNullOrWhiteSpace(txtNumeroTelefono.Text) &&
				string.IsNullOrWhiteSpace(txtEmail.Text) &&
				string.IsNullOrWhiteSpace(txtDireccion.Text))
			{
				MessageBox.Show("Debe ingresar al menos un campo para realizar la búsqueda.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			// Validación para ProveedorId (alfanumérico)
			if (!string.IsNullOrWhiteSpace(txtProveedorId.Text) && !Regex.IsMatch(txtProveedorId.Text, @"^[a-zA-Z0-9]+$"))
			{
				MessageBox.Show("El 'Proveedor Id' debe ser alfanumérico.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			List<ProveedorDetailModel> searchList = listaProveedores;
			if (!string.IsNullOrWhiteSpace(txtProveedorId.Text))
				searchList = searchList.Where(x => x.proveedor_id == Convert.ToString(txtProveedorId.Text)).ToList();
			if (!string.IsNullOrWhiteSpace(txtProveedorNombre.Text))
				searchList = searchList.Where(x => x.proveedor_nombre.Contains(txtProveedorNombre.Text, StringComparison.OrdinalIgnoreCase)).ToList();
			if (!string.IsNullOrWhiteSpace(txtNumeroTelefono.Text))
				searchList = searchList.Where(x => x.numerotelefono.Contains(txtNumeroTelefono.Text)).ToList();
			if (!string.IsNullOrWhiteSpace(txtEmail.Text))
				searchList = searchList.Where(x => x.email.Contains(txtEmail.Text, StringComparison.OrdinalIgnoreCase)).ToList();
			if (!string.IsNullOrWhiteSpace(txtDireccion.Text))
				searchList = searchList.Where(x => x.direccion.Contains(txtDireccion.Text, StringComparison.OrdinalIgnoreCase)).ToList();
			gridProveedores.ItemsSource = searchList;
		}

		private void btnLimpiar_Click(object sender, RoutedEventArgs e)
		{
			txtProveedorId.Clear();
			txtProveedorNombre.Clear();
			txtNumeroTelefono.Clear();
			txtEmail.Clear();
			txtDireccion.Clear();
			gridProveedores.ItemsSource = listaProveedores;
		}

		private void btnNuevo_Click(object sender, RoutedEventArgs e)
		{
			ProveedorWindow window = new ProveedorWindow();
			window.ShowDialog();
			FillDatagrid();
		}

		private void btnActualizar_Click(object sender, RoutedEventArgs e)
		{
			if (gridProveedores.SelectedItem != null)
			{
				ProveedorDetailModel model = (ProveedorDetailModel)gridProveedores.SelectedItem;
				ProveedorWindow window = new ProveedorWindow();
				window.model = model;
				window.ShowDialog();
				FillDatagrid();
			}
			else
			{
				MessageBox.Show("Seleccione un proveedor para actualizar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnEliminar_Click(object sender, RoutedEventArgs e)
		{
			if (gridProveedores.SelectedItem != null)
			{
				ProveedorDetailModel model = (ProveedorDetailModel)gridProveedores.SelectedItem;
				if (model != null && model.proveedor_id != "")
				{
					if (MessageBox.Show($"¿Está seguro de eliminar el proveedor con ID {model.proveedor_id}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
					{
						try
						{
							Proveedor proveedor = db.Proveedors.Find(model.proveedor_id);
							db.Proveedors.Remove(proveedor);
							db.SaveChanges();
							MessageBox.Show($"El proveedor con ID {model.proveedor_id} fue eliminado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
							FillDatagrid();
						}
						catch (Exception)
						{
							MessageBox.Show($"El proveedor con ID {model.proveedor_id} no pudo ser eliminado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Seleccione el proveedor que desea eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
