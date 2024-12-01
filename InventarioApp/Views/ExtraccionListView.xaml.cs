using InventarioApp.DB;
using InventarioApp.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
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
	/// Lógica de interacción para ExtraccionListView.xaml
	/// </summary>
	public partial class ExtraccionListView : UserControl
	{

		StockmanagerdbContext db = new StockmanagerdbContext();
		List<ExtraccionDetailModel> listaExtraccion = new List<ExtraccionDetailModel>();

		public ExtraccionListView()
		{
			InitializeComponent();
			cmbProducto.SelectionChanged += cmbProducto_SelectionChanged;
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			FillDatagrid();
		}

		void FillDatagrid()
		{
			cmbProducto.ItemsSource = db.Productos.OrderBy(x => x.Nombre).ToList();
			cmbProducto.DisplayMemberPath = "Nombre";
			cmbProducto.SelectedValuePath = "ProductoId";
			cmbProducto.SelectedIndex = -1;

			listaExtraccion = db.ExtraccionProductos.Include(x => x.Producto).Include(x => x.Lote)
			.Select(x => new ExtraccionDetailModel
			{
				extraccion_id = x.ExtraccionId,
				producto_id = x.ProductoId,
				nombre = x.Producto.Nombre,
				lote_id = x.LoteId,
				cantidad_extraida = x.CantidadExtraida,
				fecha_extraccion = x.FechaExtraccion,
			}).ToList();
			gridExtraccion.ItemsSource = listaExtraccion;

			cmbProducto.SelectionChanged += cmbProducto_SelectionChanged;
		}

		void cmbProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (cmbProducto.SelectedValue != null)
			{
				string productoId = cmbProducto.SelectedValue.ToString();

				// Filtrar lotes pertenecientes al producto seleccionado (ProductoId es string)
				var lotesFiltrados = db.Lotes
					.Where(lote => lote.ProductoId == productoId)
					.OrderBy(lote => lote.LoteId)
					.ToList();

				cmbLoteId.ItemsSource = lotesFiltrados;
				cmbLoteId.DisplayMemberPath = "LoteId";
				cmbLoteId.SelectedValuePath = "LoteId";
				cmbLoteId.SelectedIndex = -1;
			}
			else
			{
				// Si no hay producto seleccionado, limpiar los lotes
				cmbLoteId.ItemsSource = null;
			}
		}

		private void btnExtraer_Click(object sender, RoutedEventArgs e)
		{
			if (cmbProducto.SelectedValue == null || cmbLoteId.SelectedValue == null || string.IsNullOrEmpty(txtCantidadId.Text) || string.IsNullOrEmpty(txtExtraccionId.Text))
			{
				MessageBox.Show("Por favor, complete todos los campos antes de extraer.");
				return;
			}

			// Obtener el ProductoId y LoteId seleccionados
			string productoId = cmbProducto.SelectedValue.ToString();
			string loteId = cmbLoteId.SelectedValue.ToString();
			string extraccionId = txtExtraccionId.Text;

			if (string.IsNullOrEmpty(extraccionId))
			{
				MessageBox.Show("Por favor, ingrese un ID de extracción.");
				return;
			}

			// Validar que el ID de extracción tenga un formato adecuado (por ejemplo, alfanumérico o cualquier otro patrón)
			if (!System.Text.RegularExpressions.Regex.IsMatch(extraccionId, @"^[a-zA-Z0-9]+$"))
			{
				MessageBox.Show("El ID de extracción no es válido. Debe ser alfanumérico.");
				return;
			}


			// Obtener la cantidad a extraer
			if (!int.TryParse(txtCantidadId.Text, out int cantidadExtraer))
			{
				MessageBox.Show("La cantidad a extraer debe ser un número válido.");
				return;
			}

			// Obtener la fecha de extracción
			DateOnly fechaExtraccion = calFechaExtraccion.SelectedDate.HasValue
			? DateOnly.FromDateTime(calFechaExtraccion.SelectedDate.Value)
			: DateOnly.FromDateTime(DateTime.Now);

			// Obtener el stock del lote correspondiente
			var stock = db.Stocks.FirstOrDefault(s => s.LoteId == loteId);

			// Verificar que el lote tenga stock disponible
			if (stock == null)
			{
				MessageBox.Show("El lote seleccionado no tiene stock registrado.");
				return;
			}

			// Verificar si hay suficiente stock disponible
			if (stock.CantidadDisponible < cantidadExtraer)
			{
				MessageBox.Show("La cantidad a extraer excede la cantidad disponible en el stock.");
				return;
			}

			// Actualizar el stock disponible
			stock.CantidadDisponible -= cantidadExtraer;

			// Verificar y actualizar el estado del stock (ej. si queda en 0, actualizar a 'agotado')
			if (stock.CantidadDisponible == 0)
			{
				stock.StockEstado = 2;
			}

			// Registrar la extracción en la tabla de extracciones
			var nuevaExtraccion = new ExtraccionProducto
			{
				ExtraccionId = extraccionId,
				ProductoId = productoId,
				LoteId = loteId,
				CantidadExtraida = cantidadExtraer,
				FechaExtraccion = fechaExtraccion
			};
			db.ExtraccionProductos.Add(nuevaExtraccion);

			// Guardar cambios en la base de datos
			try
			{
				db.SaveChanges();
				MessageBox.Show("Extracción realizada exitosamente.");

				// Actualizar los controles de la interfaz si es necesario
				txtCantidadId.Clear();
				cmbProducto.SelectedIndex = -1;
				cmbLoteId.ItemsSource = null;
				calFechaExtraccion.SelectedDate = null;

				// Actualizar la vista de la grilla de extracciones
				FillDatagrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ocurrió un error al realizar la extracción: {ex.Message}");
			}

		}

		private void btnLimpiar_Click(object sender, RoutedEventArgs e)
		{
			txtCantidadId.Clear();
			txtExtraccionId.Clear();
			cmbProducto.SelectedIndex = -1;
			cmbLoteId.ItemsSource = null;
			calFechaExtraccion.SelectedDate = null;
		}

		private void btnActualizar_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnEliminar_Click(object sender, RoutedEventArgs e)
		{
			if (gridExtraccion.SelectedItem is ExtraccionDetailModel extraccionSeleccionada)
			{
				// Confirmación de eliminación
				var confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar esta extracción?", "Confirmar eliminación", MessageBoxButton.YesNo);
				if (confirmacion == MessageBoxResult.No)
				{
					return;
				}

				// Buscar la extracción en la base de datos por su ExtraccionId
				var extraccion = db.ExtraccionProductos.FirstOrDefault(x => x.ExtraccionId == extraccionSeleccionada.extraccion_id);
				if (extraccion != null)
				{
					// Eliminar la extracción de la base de datos
					db.ExtraccionProductos.Remove(extraccion);

					try
					{
						// Guardar cambios
						db.SaveChanges();
						MessageBox.Show("Extracción eliminada exitosamente.");

						// Actualizar el DataGrid
						FillDatagrid();
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Error al eliminar la extracción: {ex.Message}");
					}
				}
				else
				{
					MessageBox.Show("La extracción seleccionada no se encontró en la base de datos.");
				}
			}
			else
			{
				MessageBox.Show("Seleccione una extracción para eliminar.");
			}

		}

		private void btnVerificar_Click(object sender, RoutedEventArgs e)
		{
			// Validar que se hayan seleccionado valores en los ComboBox
			if (cmbProducto.SelectedValue == null || cmbLoteId.SelectedValue == null)
			{
				MessageBox.Show("Por favor, seleccione un Producto y un Lote antes de verificar.");
				return;
			}

			try
			{
				// Obtener los valores seleccionados en los ComboBox
				string productoId = cmbProducto.SelectedValue.ToString();
				string loteId = cmbLoteId.SelectedValue.ToString();

				// Verificar si el producto existe en la base de datos
				var productoExistente = db.Productos.FirstOrDefault(p => p.ProductoId == productoId);


				if (productoExistente != null)
				{
					// Acceder a la cadena de conexión desde app.config
					string connectionString = ConfigurationManager.ConnectionStrings["myConStr"].ConnectionString;

					// Capturar los mensajes de NOTICE
					using (var connection = new NpgsqlConnection(connectionString))
					{
						connection.Open();

						// Suscribirse a los mensajes de NOTICE
						connection.Notice += (sender, e) =>
						{
							// Mostrar cada mensaje de NOTICE en un MessageBox
							MessageBox.Show(e.Notice.MessageText, "Información del servidor", MessageBoxButton.OK, MessageBoxImage.Information);
						};

						// Llamar al procedimiento almacenado para verificar el stock bajo
						using (var cmd = new NpgsqlCommand($@"
                    CALL verificar_stock_bajo(
                        @productoId, @loteId
                    )", connection))
						{
							cmd.Parameters.AddWithValue("productoId", productoId);
							cmd.Parameters.AddWithValue("loteId", loteId);
							cmd.ExecuteNonQuery();
						}

						MessageBox.Show("Verificación completada. Si el stock está bajo, se ha generado una alerta.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
					}
				}
				else
				{
					MessageBox.Show("Producto no encontrado en la base de datos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
			catch (Exception ex)
			{
				// Manejo de excepciones en caso de errores
				MessageBox.Show($"Error al verificar el stock: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
