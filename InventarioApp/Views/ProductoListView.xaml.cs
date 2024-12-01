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
    /// Lógica de interacción para ProductoListView.xaml
    /// </summary>
    public partial class ProductoListView : UserControl
    {

		StockmanagerdbContext db = new StockmanagerdbContext();
		List<ProductoDetailModel> listaProductos = new List<ProductoDetailModel>();

		public ProductoListView()
        {
            InitializeComponent();
        }

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			FillDatagrid();
		}

		void FillDatagrid()
		{
			cmbCategoria.ItemsSource = db.Categoria.OrderBy(x => x.CategoriaNombre).ToList();
			cmbCategoria.DisplayMemberPath = "CategoriaNombre";
			cmbCategoria.SelectedValuePath = "CategoriaId";
			cmbCategoria.SelectedIndex = -1;
			cmbInventario.ItemsSource = db.Inventarios.OrderBy(x => x.InventarioNombre).ToList();
			cmbInventario.DisplayMemberPath = "InventarioNombre";
			cmbInventario.SelectedValuePath = "InventarioId";
			cmbInventario.SelectedIndex = -1;
			listaProductos = db.Productos.Include(x => x.Categoria).Include(x => x.Inventario)
			.Select(x => new ProductoDetailModel
			{
				producto_id = x.ProductoId,
				nombre = x.Nombre,
				descripcion = x.Descripcion,
				preciounitario = x.Preciounitario,
				categoria_id = x.CategoriaId,
				categoria_nombre = x.Categoria.CategoriaNombre,
				inventario_id = x.InventarioId,
				inventario_nombre = x.Inventario.InventarioNombre,

				// Sumar la cantidad disponible desde la tabla Stock
				cantidad_total = db.Stocks
								.Where(s => db.Lotes
											   .Where(l => l.ProductoId == x.ProductoId)
											   .Select(l => l.LoteId)
											   .Contains(s.LoteId))
								.Sum(s => (int?)s.CantidadDisponible) ?? 0 // Manejar null
			}).ToList();
			gridProductos.ItemsSource = listaProductos;
		}

		private void btnBuscar_Click(object sender, RoutedEventArgs e)
		{
			List<ProductoDetailModel> searchList = listaProductos;
			// Validación para txtProductoId: verificar si es alfanumérico
			if (!string.IsNullOrWhiteSpace(txtProductoId.Text))
			{
				string productoId = txtProductoId.Text.Trim();
				if (!System.Text.RegularExpressions.Regex.IsMatch(productoId, @"^[a-zA-Z0-9]+$"))
				{
					MessageBox.Show("El ID de producto debe ser alfanumérico (solo letras y números).", "Error de validación", MessageBoxButton.OK, MessageBoxImage.Warning);
					return; // Salimos del método si el ID no es alfanumérico
				}
				searchList = searchList.Where(x => x.producto_id.Equals(productoId, StringComparison.OrdinalIgnoreCase)).ToList();
			}
			// Validación para txtNombre: verificar si es alfanumérico
			if (!string.IsNullOrWhiteSpace(txtNombre.Text))
			{
				string nombre = txtNombre.Text.Trim();
				if (!System.Text.RegularExpressions.Regex.IsMatch(nombre, @"^[a-zA-Z0-9\s]+$"))
				{
					MessageBox.Show("El nombre debe ser alfanumérico (solo letras, números y espacios).", "Error de validación", MessageBoxButton.OK, MessageBoxImage.Warning);
					return; // Salimos del método si el nombre no es alfanumérico
				}
				searchList = searchList.Where(x => x.nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList();
			}
			if (cmbCategoria.SelectedIndex != -1)
				searchList = searchList.Where(x => x.categoria_id == Convert.ToInt32(cmbCategoria.SelectedValue)).ToList();
			if (cmbInventario.SelectedIndex != -1)
				searchList = searchList.Where(x => x.inventario_id == Convert.ToString(cmbInventario.SelectedValue)).ToList();
			gridProductos.ItemsSource = searchList;
		}

		private void btnLimpiar_Click(object sender, RoutedEventArgs e)
		{
			txtProductoId.Clear();
			txtNombre.Clear();
			cmbCategoria.SelectedIndex = -1;
			cmbInventario.SelectedIndex = -1;
			gridProductos.ItemsSource = listaProductos;
		}

		private void btnActualizar_Click(object sender, RoutedEventArgs e)
		{
			if (gridProductos.SelectedItem != null)
			{
				ProductoDetailModel model = (ProductoDetailModel)gridProductos.SelectedItem;
				ProductoWindow window = new ProductoWindow();
				window.model = model;
				window.ShowDialog();
				FillDatagrid();
			}
			else
			{
				MessageBox.Show("Seleccione un producto para ser actulizado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnEliminar_Click(object sender, RoutedEventArgs e)
		{
			if (gridProductos.SelectedItem != null)
			{
				ProductoDetailModel model = (ProductoDetailModel)gridProductos.SelectedItem;
				if (model != null && model.producto_id != "")
				{
					if (MessageBox.Show($"Esta seguro de eliminar el producto Id {model.producto_id}", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
					{
						try
						{
							Producto producto = db.Productos.Find(model.producto_id)!;
							db.Productos.Remove(producto);
							db.SaveChanges();
							MessageBox.Show($"El producto de Id {model.producto_id} fue eliminado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
							FillDatagrid();
						}
						catch (Exception)
						{
							MessageBox.Show($"El producto Id {model.producto_id} no pudo ser eliminado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
