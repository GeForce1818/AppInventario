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
	/// Lógica de interacción para CategoriaListView.xaml
	/// </summary>
	public partial class CategoriaListView : UserControl
	{
		public CategoriaListView()
		{
			InitializeComponent();
		}

		StockmanagerdbContext db = new StockmanagerdbContext();
		List<CategoriaDetailModel> listaCategorias = new List<CategoriaDetailModel>();

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			FillDatagrid();
		}

		void FillDatagrid()
		{
			listaCategorias = db.Categoria
				.Select(x => new CategoriaDetailModel
				{
					categoria_id = x.CategoriaId,
					categoria_nombre = x.CategoriaNombre,
					descripcion = x.Descripcion
				}).ToList();
			gridCategorias.ItemsSource = listaCategorias;
		}

		private void btnBuscar_Click(object sender, RoutedEventArgs e)
		{
			if (!ValidarCamposBusqueda())
				return;
			List<CategoriaDetailModel> searchList = listaCategorias;
			if (txtCategoriaId.Text.Trim() != "")
				searchList = searchList.Where(x => x.categoria_id == Convert.ToInt32(txtCategoriaId.Text)).ToList();
			if (txtCategoriaNombre.Text.Trim() != "")
				searchList = searchList.Where(x => x.categoria_nombre.Contains(txtCategoriaNombre.Text)).ToList();
			gridCategorias.ItemsSource = searchList;
		}

		private void btnLimpiar_Click(object sender, RoutedEventArgs e)
		{
			txtCategoriaId.Clear();
			txtCategoriaNombre.Clear();
			txtDescripcion.Clear();
			gridCategorias.ItemsSource = listaCategorias;
		}

		private void btnNuevo_Click(object sender, RoutedEventArgs e)
		{
			CategoriaWindow window = new CategoriaWindow();
			window.ShowDialog();
			FillDatagrid();
		}

		private void btnActualizar_Click(object sender, RoutedEventArgs e)
		{
			if (gridCategorias.SelectedItem != null)
			{
				CategoriaDetailModel model = (CategoriaDetailModel)gridCategorias.SelectedItem;
				CategoriaWindow window = new CategoriaWindow();
				window.model = model;
				window.ShowDialog();
				FillDatagrid();
			}
			else
			{
				MessageBox.Show("Seleccione una categoría para actualizar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnEliminar_Click(object sender, RoutedEventArgs e)
		{
			if (gridCategorias.SelectedItem != null)
			{
				CategoriaDetailModel model = (CategoriaDetailModel)gridCategorias.SelectedItem;
				if (model != null && model.categoria_id != 0)
				{
					if (MessageBox.Show($"¿Está seguro de eliminar la categoría con ID {model.categoria_id}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
					{
						try
						{
							Categorium categoria = db.Categoria.Find(model.categoria_id);
							db.Categoria.Remove(categoria);
							db.SaveChanges();
							MessageBox.Show($"La categoría con ID {model.categoria_id} fue eliminada.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
							FillDatagrid();
						}
						catch (Exception)
						{
							MessageBox.Show($"La categoría con ID {model.categoria_id} no pudo ser eliminada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Seleccione la categoría que desea eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private bool ValidarCategoriaId()
		{
			if (string.IsNullOrWhiteSpace(txtCategoriaId.Text))
				return true; // Campo opcional, si está vacío es válido.

			if (!int.TryParse(txtCategoriaId.Text, out int id) || id <= 0)
			{
				MessageBox.Show("Ingrese un ID de categoría válido (número entero positivo).", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}
			return true;
		}

		private bool ValidarCategoriaNombre()
		{
			if (string.IsNullOrWhiteSpace(txtCategoriaNombre.Text))
			{
				MessageBox.Show("El nombre de la categoría no puede estar vacío.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}

			if (txtCategoriaNombre.Text.Length > 50)
			{
				MessageBox.Show("El nombre de la categoría no puede exceder los 50 caracteres.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}
			return true;
		}

		private bool ValidarDescripcion()
		{
			if (txtDescripcion.Text.Length > 200)
			{
				MessageBox.Show("La descripción no puede exceder los 200 caracteres.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}
			return true;
		}

		private bool ValidarCamposBusqueda()
		{
			return ValidarCategoriaId() && ValidarCategoriaNombre();
		}
	}
}
