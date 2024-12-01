using InventarioApp.DB;
using InventarioApp.Models;
using Microsoft.EntityFrameworkCore;
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
	/// Lógica de interacción para ProductoNuevoListView.xaml
	/// </summary>
	public partial class ProductoNuevoListView : UserControl
	{
		private StockmanagerdbContext db = new StockmanagerdbContext();
		private List<ProductoNuevoDetailModel> listaProductos = new List<ProductoNuevoDetailModel>();

		public ProductoNuevoListView()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			FillDatagrid();
		}

		private void FillDatagrid()
		{
			cmbEstadoRegistro.ItemsSource = db.Registros.OrderBy(x => x.Estado).ToList();
			cmbEstadoRegistro.DisplayMemberPath = "Estado";
			cmbEstadoRegistro.SelectedValuePath = "RegistroId";
			cmbEstadoRegistro.SelectedIndex = -1;

			cmbCategoriaNombre.ItemsSource = db.Categoria.OrderBy(x => x.CategoriaNombre).ToList();
			cmbCategoriaNombre.DisplayMemberPath = "CategoriaNombre";
			cmbCategoriaNombre.SelectedValuePath = "CategoriaId";
			cmbCategoriaNombre.SelectedIndex = -1;

			listaProductos = db.ProductoNuevos.Include(x => x.Categoria).Include(x => x.EstadoRegistroNavigation)
				.Select(x => new ProductoNuevoDetailModel
				{
					producto_nuevo_id = x.ProductoNuevoId,
					nombre = x.Nombre,
					categoria_id = x.CategoriaId,
					categoria_nombre = x.Categoria.CategoriaNombre,
					descripcion = x.Descripcion,
					precio_estimado = x.PrecioEstimado,
					estado_registro = x.EstadoRegistro,
					estado = x.EstadoRegistroNavigation.Estado
				}).ToList();
			gridProductos.ItemsSource = listaProductos;
		}

		private void btnBuscar_Click(object sender, RoutedEventArgs e)
		{
			List<ProductoNuevoDetailModel> searchList = listaProductos;
			// Validación de ID de producto
			if (!string.IsNullOrWhiteSpace(txtProductoId.Text))
			{
				if (txtProductoId.Text.All(char.IsLetterOrDigit))
					searchList = searchList.Where(x => x.producto_nuevo_id == txtProductoId.Text.Trim()).ToList();
				else
					MessageBox.Show("El ID de producto debe ser alfanumérico.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			// Validación de nombre de producto
			if (!string.IsNullOrWhiteSpace(txtnombre.Text))
			{
				if (txtnombre.Text.All(char.IsLetterOrDigit))
					searchList = searchList.Where(x => x.nombre.Contains(txtnombre.Text.Trim())).ToList();
				else
					MessageBox.Show("El nombre del producto debe ser alfanumérico.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			if (cmbCategoriaNombre.SelectedIndex != -1)
				searchList = searchList.Where(x => x.categoria_id == Convert.ToInt32(cmbCategoriaNombre.SelectedValue)).ToList();
			if (cmbEstadoRegistro.SelectedIndex != -1)
				searchList = searchList.Where(x => x.estado_registro == Convert.ToInt32(cmbEstadoRegistro.SelectedValue)).ToList();

			gridProductos.ItemsSource = searchList;
		}

		private void btnLimpiar_Click(object sender, RoutedEventArgs e)
		{
			txtProductoId.Clear();
			txtnombre.Clear();
			cmbCategoriaNombre.SelectedIndex = -1;
			txtDescripcion.Clear();
			txtPrecioEstimado.Clear();
			cmbEstadoRegistro.SelectedIndex = -1;
			gridProductos.ItemsSource = listaProductos;
		}

		private void btnNuevo_Click(object sender, RoutedEventArgs e)
		{
			ProductoNuevoWindow window = new ProductoNuevoWindow();
			window.ShowDialog();
			FillDatagrid();
		}

		private void btnActualizar_Click(object sender, RoutedEventArgs e)
		{
			if (gridProductos.SelectedItem != null)
			{
				ProductoNuevoDetailModel model = (ProductoNuevoDetailModel)gridProductos.SelectedItem;
				ProductoNuevoWindow window = new ProductoNuevoWindow();
				window.model = model;
				window.ShowDialog();
				FillDatagrid();
			}
			else
			{
				MessageBox.Show("Seleccione un producto para actualizar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnEliminar_Click(object sender, RoutedEventArgs e)
		{
			if (gridProductos.SelectedItem != null)
			{
				ProductoNuevoDetailModel model = (ProductoNuevoDetailModel)gridProductos.SelectedItem;
				if (model != null && model.producto_nuevo_id != "")
				{
					if (MessageBox.Show($"¿Está seguro de eliminar el producto con ID {model.producto_nuevo_id}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
					{
						try
						{
							ProductoNuevo producto = db.ProductoNuevos.Find(model.producto_nuevo_id);
							db.ProductoNuevos.Remove(producto);
							db.SaveChanges();
							MessageBox.Show($"El producto con ID {model.producto_nuevo_id} fue eliminado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
							FillDatagrid();
						}
						catch (Exception)
						{
							MessageBox.Show($"El producto con ID {model.producto_nuevo_id} no pudo ser eliminado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Seleccione el producto que desea eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
