using InventarioApp.DB;
using InventarioApp.Models;
using Microsoft.EntityFrameworkCore;
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
    /// Lógica de interacción para RegistroProductoListView.xaml
    /// </summary>
    public partial class RegistroProductoListView : UserControl
    {
		StockmanagerdbContext db = new StockmanagerdbContext();
		List<ProductoDetailModel> listaProductos = new List<ProductoDetailModel>();
		List<LoteDetailModel> listaLotes = new List<LoteDetailModel>();
		List<StockDetailModel> listaStocks = new List<StockDetailModel>();

		public RegistroProductoListView()
        {
            InitializeComponent();
        }

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			FillDatagrids();
		}

		void FillDatagrids()
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
				inventario_nombre = x.Inventario.InventarioNombre
			}).ToList();
			gridProducto.ItemsSource = listaProductos;

			cmbEstadoLote.ItemsSource = db.EstadoLotes.OrderBy(x => x.EstadoNombre).ToList();
			cmbEstadoLote.DisplayMemberPath = "EstadoNombre";
			cmbEstadoLote.SelectedValuePath = "EstadoId";
			cmbEstadoLote.SelectedIndex = -1;
			listaLotes = db.Lotes.Include(x => x.LoteEstadoNavigation)
			.Select(x => new LoteDetailModel
			{
				lote_id = x.LoteId,
				producto_id = x.ProductoId,
				fecha_fabricacion = x.FechaFabricacion,
				fecha_vencimiento = x.FechaVencimiento,
				lote_ubicacion = x.LoteUbicacion,
				lote_estado = x.LoteEstado,
				estado_nombre = x.LoteEstadoNavigation.EstadoNombre
			}).ToList();
			gridLote.ItemsSource = listaLotes;

			cmbEstadoStock.ItemsSource = db.EstadoStocks.OrderBy(x => x.EstadoNombre).ToList();
			cmbEstadoStock.DisplayMemberPath = "EstadoNombre";
			cmbEstadoStock.SelectedValuePath = "EstadoId";
			cmbEstadoStock.SelectedIndex = -1;
			listaStocks = db.Stocks.Include(x => x.StockEstadoNavigation)
				.Select(x => new StockDetailModel
				{
					stock_id = x.StockId,
					lote_id = x.LoteId,
					cantidad_disponible = x.CantidadDisponible,
					stock_estado = x.StockEstado,
					estado_nombre = x.StockEstadoNavigation.EstadoNombre
				}).ToList();
			gridStock.ItemsSource = listaStocks;
		}

		private void btnAgregar_Click(object sender, RoutedEventArgs e)
		{
			// Validar ProductoId
			if (string.IsNullOrWhiteSpace(txtProductoId.Text))
			{
				MessageBox.Show("El Producto ID no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar Nombre
			if (string.IsNullOrWhiteSpace(txtNombre.Text))
			{
				MessageBox.Show("El nombre no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar Descripcion
			if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
			{
				MessageBox.Show("La descripción no puede estar vacía.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar Precio Unitario
			if (string.IsNullOrWhiteSpace(txtPrecioUnitario.Text))
			{
				MessageBox.Show("Por favor ingrese un precio unitario válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar CategoriaId
			if (cmbCategoria.SelectedIndex == -1)
			{
				MessageBox.Show("Debe seleccionar una categoría.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar InventarioId
			if (cmbInventario.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cmbInventario.SelectedValue?.ToString()))
			{
				MessageBox.Show("Debe seleccionar un inventario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar LoteId
			if (string.IsNullOrWhiteSpace(txtLoteId.Text))
			{
				MessageBox.Show("El Lote ID no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar Fecha de Fabricación
			if (!calFechaFabricacion.SelectedDate.HasValue)
			{
				MessageBox.Show("Debe seleccionar una fecha de fabricación.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}


			// Validar Fecha de Vencimiento
			if (!calFechaVencimiento.SelectedDate.HasValue)
			{
				MessageBox.Show("Debe seleccionar una fecha de vencimiento.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}


			// Validar Ubicación del Lote
			if (string.IsNullOrWhiteSpace(txtUbicacion.Text))
			{
				MessageBox.Show("La ubicación del lote no puede estar vacía.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar Estado del Lote
			if (cmbEstadoLote.SelectedIndex == -1)
			{
				MessageBox.Show("Debe seleccionar un estado para el lote.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar Stock ID
			if (string.IsNullOrWhiteSpace(txtStockId.Text))
			{
				MessageBox.Show("El Stock ID no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar Cantidad Disponible
			if (string.IsNullOrWhiteSpace(txtCantidadDisponible.Text))
			{
				MessageBox.Show("La cantidad disponible debe ser un número válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar Estado del Stock
			if (cmbEstadoStock.SelectedIndex == -1)
			{
				MessageBox.Show("Debe seleccionar un estado para el stock.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Obteniendo valores desde la UI
			string productoId = txtProductoId.Text;
			string nombre = txtNombre.Text;
			string descripcion = txtDescripcion.Text;
			decimal precioUnitario = Convert.ToDecimal(txtPrecioUnitario.Text);
			int categoriaId = Convert.ToInt32(cmbCategoria.SelectedValue);
			string inventarioId = Convert.ToString(cmbInventario.SelectedValue);
			string loteId = txtLoteId.Text;
			DateTime fechaFabricacionDateTime = calFechaFabricacion.SelectedDate.Value;
			DateOnly fechaFabricacion = DateOnly.FromDateTime(fechaFabricacionDateTime);
			DateTime fechaVencimientoDateTime = calFechaVencimiento.SelectedDate.Value;
			DateOnly fechaVencimiento = DateOnly.FromDateTime(fechaVencimientoDateTime);
			string loteUbicacion = txtUbicacion.Text;
			int loteEstado = Convert.ToInt32(cmbEstadoLote.SelectedValue);
			string stockId = txtStockId.Text;
			int cantidadDisponible = Convert.ToInt32(txtCantidadDisponible.Text);
			int stockEstado = Convert.ToInt32(cmbEstadoStock.SelectedValue);

			if (string.IsNullOrWhiteSpace(productoId) || !Regex.IsMatch(productoId, @"^[a-zA-Z0-9]+$"))
			{
				MessageBox.Show("El Producto ID debe ser alfanumérico.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (string.IsNullOrWhiteSpace(loteId) || !Regex.IsMatch(loteId, @"^[a-zA-Z0-9]+$"))
			{
				MessageBox.Show("El Lote ID debe ser alfanumérico.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (string.IsNullOrWhiteSpace(nombre))
			{
				MessageBox.Show("El nombre del producto es obligatorio.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (string.IsNullOrWhiteSpace(descripcion))
			{
				MessageBox.Show("La descripción del producto es obligatoria.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (!decimal.TryParse(txtPrecioUnitario.Text, out precioUnitario))
			{
				MessageBox.Show("El precio unitario debe ser un número válido.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (!int.TryParse(cmbCategoria.SelectedValue?.ToString(), out categoriaId))
			{
				MessageBox.Show("Debe seleccionar una categoría válida.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (cmbInventario.SelectedIndex == -1)
			{
				MessageBox.Show("Debe seleccionar un inventario válido.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (!DateTime.TryParse(calFechaFabricacion.SelectedDate?.ToString(), out fechaFabricacionDateTime))
			{
				MessageBox.Show("La fecha de fabricación es inválida.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (!DateTime.TryParse(calFechaVencimiento.SelectedDate?.ToString(), out fechaVencimientoDateTime))
			{
				MessageBox.Show("La fecha de vencimiento es inválida.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (string.IsNullOrWhiteSpace(loteUbicacion))
			{
				MessageBox.Show("La ubicación del lote es obligatoria.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (!int.TryParse(cmbEstadoLote.SelectedValue?.ToString(), out loteEstado))
			{
				MessageBox.Show("Debe seleccionar un estado de lote válido.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (string.IsNullOrWhiteSpace(stockId))
			{
				MessageBox.Show("El ID del stock es obligatorio.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (!int.TryParse(txtCantidadDisponible.Text, out cantidadDisponible) || cantidadDisponible < 0)
			{
				MessageBox.Show("La cantidad disponible debe ser un número válido y mayor o igual a 0.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (!int.TryParse(cmbEstadoStock.SelectedValue?.ToString(), out stockEstado))
			{
				MessageBox.Show("Debe seleccionar un estado de stock válido.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			try
			{
				// Verificar si el producto ya existe en la base de datos
				var productoExistente = db.Productos.FirstOrDefault(p => p.ProductoId == productoId);

				if (productoExistente != null)
				{
					// Actualizar producto existente
					db.Database.ExecuteSqlInterpolated($@"
                CALL actualizar_producto(
                    {productoId}, {nombre}, {descripcion}, {precioUnitario}, {categoriaId}, 
                    {inventarioId}, {loteId}, {fechaFabricacion}, {fechaVencimiento}, 
                    {loteUbicacion}, {loteEstado}, {stockId}, {cantidadDisponible}, {stockEstado}
                )");

					MessageBox.Show("Producto actualizado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
				}
				else
				{
					// Insertar nuevo producto
					db.Database.ExecuteSqlInterpolated($@"
                CALL agregar_producto(
                    {productoId}, {nombre}, {descripcion}, {precioUnitario}, {categoriaId}, 
                    {inventarioId}, {loteId}, {fechaFabricacion}, {fechaVencimiento}, 
                    {loteUbicacion}, {loteEstado}, {stockId}, {cantidadDisponible}, {stockEstado}
                )");

					MessageBox.Show("Producto agregado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
				}
				FillDatagrids();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error al agregar o actualizar el producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnQuitar_Click(object sender, RoutedEventArgs e)
		{
			string productoId = txtProductoId.Text;
			var productoExistente = db.Productos.FirstOrDefault(p => p.ProductoId == productoId); // Asumiendo que el modelo contiene esta propiedad

			if (productoExistente != null)
			{

				try
				{
					// Llamar al procedimiento para eliminar el producto y sus lotes y stocks
					db.Database.ExecuteSqlInterpolated($"CALL EliminarProductoCompleto({productoId})");
					MessageBox.Show("Producto, sus lotes y stocks eliminados correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
					FillDatagrids(); // Recargar los datagrids
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void gridProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (gridProducto.SelectedItem is ProductoDetailModel selectedProducto)
			{
				txtProductoId.Text = selectedProducto.producto_id.ToString();
				txtNombre.Text = selectedProducto.nombre;
				txtDescripcion.Text = selectedProducto.descripcion;
				txtPrecioUnitario.Text = selectedProducto.preciounitario.ToString();
				cmbCategoria.SelectedValue = selectedProducto.categoria_id;
				cmbInventario.SelectedValue = selectedProducto.inventario_id;
			}
		}

		private void gridLote_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (gridLote.SelectedItem is LoteDetailModel selectedLote)
			{
				txtLoteId.Text = selectedLote.lote_id.ToString();
				txtUbicacion.Text = selectedLote.lote_ubicacion;
				calFechaFabricacion.SelectedDate = selectedLote.fecha_fabricacion?.ToDateTime(TimeOnly.MinValue);
				calFechaVencimiento.SelectedDate = selectedLote.fecha_vencimiento?.ToDateTime(TimeOnly.MinValue);
				cmbEstadoLote.SelectedValue = selectedLote.lote_estado;
			}
		}

		private void gridStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (gridStock.SelectedItem is StockDetailModel selectedStock)
			{
				txtStockId.Text = selectedStock.stock_id.ToString();
				txtCantidadDisponible.Text = selectedStock.cantidad_disponible.ToString();
				cmbEstadoStock.SelectedValue = selectedStock.stock_estado;
			}
		}


	}
}
