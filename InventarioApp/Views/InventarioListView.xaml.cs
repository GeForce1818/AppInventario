using InventarioApp.DB;
using InventarioApp.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventarioApp.Views
{
    /// <summary>
    /// Lógica de interacción para InventarioListView.xaml
    /// </summary>
    public partial class InventarioListView : UserControl
    {
        public InventarioListView()
        {
            InitializeComponent();
        }

		StockmanagerdbContext db = new StockmanagerdbContext();
		List<InventarioDetailModel> listaInventarios = new List<InventarioDetailModel>();

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			FillDatagrid();
		}

		void FillDatagrid()
		{
			listaInventarios = db.Inventarios
				.Select(x => new InventarioDetailModel
				{
					inventario_id = x.InventarioId,
					inventario_nombre = x.InventarioNombre
				}).ToList();
			gridInventarios.ItemsSource = listaInventarios;
		}

		private void btnBuscar_Click(object sender, RoutedEventArgs e)
		{
			// Validar que no se estén dejando ambos campos vacíos
			if (string.IsNullOrWhiteSpace(txtInventarioId.Text.Trim()) && string.IsNullOrWhiteSpace(txtInventarioNombre.Text.Trim()))
			{
				MessageBox.Show("Por favor, ingrese al menos un criterio de búsqueda.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			List<InventarioDetailModel> searchList = listaInventarios;
			if (!string.IsNullOrWhiteSpace(txtInventarioId.Text.Trim()))
			{
				string inventarioId = txtInventarioId.Text.Trim();
				// Asegurarse de que el ID de inventario esté en el formato adecuado
				if (inventarioId.Any(c => !Char.IsLetterOrDigit(c))) // Validar que solo contenga letras y números
				{
					MessageBox.Show("El ID del inventario debe ser alfanumérico.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}

				searchList = searchList.Where(x => x.inventario_id == inventarioId).ToList();
			}

			if (!string.IsNullOrWhiteSpace(txtInventarioNombre.Text.Trim()))
			{
				string inventarioNombre = txtInventarioNombre.Text.Trim();
				// Asegurarse de que el nombre no esté vacío (aunque ya se haya hecho antes)
				if (inventarioNombre.Any(c => !Char.IsLetterOrDigit(c))) // No permitir simbolos
				{
					MessageBox.Show("El nombre del inventario no debe contener simbolos", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}

				searchList = searchList.Where(x => x.inventario_nombre.Contains(inventarioNombre, StringComparison.OrdinalIgnoreCase)).ToList();
			}
			gridInventarios.ItemsSource = searchList;
		}

		private void btnLimpiar_Click(object sender, RoutedEventArgs e)
		{
			txtInventarioId.Clear();
			txtInventarioNombre.Clear();
			gridInventarios.ItemsSource = listaInventarios;
		}

		private void btnNuevo_Click(object sender, RoutedEventArgs e)
		{
			InventarioWindow window = new InventarioWindow();
			window.ShowDialog();
			FillDatagrid();
		}

		private void btnActualizar_Click(object sender, RoutedEventArgs e)
		{
			if (gridInventarios.SelectedItem != null)
			{
				InventarioDetailModel model = (InventarioDetailModel)gridInventarios.SelectedItem;
				InventarioWindow window = new InventarioWindow();
				window.model = model;
				window.ShowDialog();
				FillDatagrid();
			}
			else
			{
				MessageBox.Show("Seleccione un inventario para actualizar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnEliminar_Click(object sender, RoutedEventArgs e)
		{
			if (gridInventarios.SelectedItem != null)
			{
				InventarioDetailModel model = (InventarioDetailModel)gridInventarios.SelectedItem;
				if (model != null && model.inventario_id != "")
				{
					if (MessageBox.Show($"¿Está seguro de eliminar el inventario con ID {model.inventario_id}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
					{
						try
						{
							Inventario inventario = db.Inventarios.Find(model.inventario_id);
							db.Inventarios.Remove(inventario);
							db.SaveChanges();
							MessageBox.Show($"El inventario con ID {model.inventario_id} fue eliminado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
							FillDatagrid();
						}
						catch (Exception)
						{
							MessageBox.Show($"El inventario con ID {model.inventario_id} no pudo ser eliminado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Seleccione el inventario que desea eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
