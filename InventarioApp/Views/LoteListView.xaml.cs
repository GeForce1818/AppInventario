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
	/// Lógica de interacción para LoteListView.xaml
	/// </summary>
	public partial class LoteListView : UserControl
	{
		private StockmanagerdbContext db = new StockmanagerdbContext();
		private List<LoteDetailModel> listaLotes = new List<LoteDetailModel>();

		public LoteListView()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			FillDatagrid();
		}

		private void FillDatagrid()
		{
			cmbLoteEstado.ItemsSource = db.EstadoLotes.OrderBy(x => x.EstadoNombre).ToList();
			cmbLoteEstado.DisplayMemberPath = "EstadoNombre";
			cmbLoteEstado.SelectedValuePath = "EstadoId";
			cmbLoteEstado.SelectedIndex = -1;
			cmbProductoId.ItemsSource = db.Productos.OrderBy(x => x.Nombre).ToList();
			cmbProductoId.DisplayMemberPath = "Nombre";
			cmbProductoId.SelectedValuePath = "ProductoId";
			cmbProductoId.SelectedIndex = -1;
			listaLotes = db.Lotes.Include(x => x.LoteEstadoNavigation)
				.Select(x => new LoteDetailModel
				{
					lote_id = x.LoteId,
					producto_id = x.ProductoId,
					nombre = x.Producto.Nombre,
					fecha_fabricacion = x.FechaFabricacion,
					fecha_vencimiento = x.FechaVencimiento,
					lote_ubicacion = x.LoteUbicacion,
					lote_estado = x.LoteEstado,
					estado_nombre = x.LoteEstadoNavigation.EstadoNombre
				}).ToList();
			gridLotes.ItemsSource = listaLotes;
		}

		private void btnBuscar_Click(object sender, RoutedEventArgs e)
		{
			List<LoteDetailModel> searchList = listaLotes;
			if (!string.IsNullOrWhiteSpace(txtLoteId.Text))
			{
				// Comprobar si el ID de lote es alfanumérico
				if (!System.Text.RegularExpressions.Regex.IsMatch(txtLoteId.Text, @"^[a-zA-Z0-9]+$"))
				{
					MessageBox.Show("El ID de lote debe ser alfanumérico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
				// Filtrar por ID de lote
				searchList = searchList.Where(x => x.lote_id.Equals(txtLoteId.Text, StringComparison.OrdinalIgnoreCase)).ToList();
			}

			if (cmbProductoId.SelectedIndex != -1)
				searchList = searchList.Where(x => x.producto_id == Convert.ToString(cmbProductoId.SelectedValue)).ToList();
			if (cmbLoteEstado.SelectedIndex != -1)
				searchList = searchList.Where(x => x.lote_estado == Convert.ToInt32(cmbLoteEstado.SelectedValue)).ToList();
			if (dpFechaFabricacion.SelectedDate != null)
			{
				var fechaFabricacion = DateOnly.FromDateTime(dpFechaFabricacion.SelectedDate.Value);
				searchList = searchList.Where(x => x.fecha_fabricacion == fechaFabricacion).ToList();
			}

			if (!string.IsNullOrWhiteSpace(txtLoteUbicacion.Text))
			{
				// Filtrar por ID de lote
				searchList = searchList.Where(x => x.lote_ubicacion.Contains(txtLoteUbicacion.Text, StringComparison.OrdinalIgnoreCase)).ToList();
			}

			if (dpFechaVencimiento.SelectedDate != null)
			{
				var fechaVencimiento = DateOnly.FromDateTime(dpFechaVencimiento.SelectedDate.Value);

				// Asegúrate de que la fecha de vencimiento no sea anterior a la de fabricación
				if (dpFechaFabricacion.SelectedDate != null && fechaVencimiento < DateOnly.FromDateTime(dpFechaFabricacion.SelectedDate.Value))
				{
					MessageBox.Show("La fecha de vencimiento no puede ser anterior a la fecha de fabricación.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}

				searchList = searchList.Where(x => x.fecha_vencimiento == fechaVencimiento).ToList();
			}
			gridLotes.ItemsSource = searchList;
		}

		// Evento para el botón de limpiar campos
		private void btnLimpiar_Click(object sender, RoutedEventArgs e)
		{
			txtLoteId.Clear();
			cmbProductoId.SelectedIndex = -1;
			txtLoteUbicacion.Clear();
			dpFechaFabricacion.SelectedDate = null;
			dpFechaVencimiento.SelectedDate = null;
			cmbLoteEstado.SelectedIndex = -1;
			gridLotes.ItemsSource = listaLotes;
		}

		// Evento para el botón de nuevo lote
		private void btnNuevo_Click(object sender, RoutedEventArgs e)
		{
			LoteWindow window = new LoteWindow();
			window.ShowDialog();
			FillDatagrid();
		}

		// Evento para el botón de actualización de lote
		private void btnActualizar_Click(object sender, RoutedEventArgs e)
		{
			if (gridLotes.SelectedItem != null)
			{
				LoteDetailModel model = (LoteDetailModel)gridLotes.SelectedItem;
				LoteWindow window = new LoteWindow();
				window.model = model;
				window.ShowDialog();
				FillDatagrid();
			}
			else
			{
				MessageBox.Show("Seleccione un lote para actualizar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		// Evento para el botón de eliminar lote
		private void btnEliminar_Click(object sender, RoutedEventArgs e)
		{
			if (gridLotes.SelectedItem != null)
			{
				LoteDetailModel model = (LoteDetailModel)gridLotes.SelectedItem;
				if (model != null && model.lote_id != "")
				{
					if (MessageBox.Show($"¿Está seguro de eliminar el lote con ID {model.lote_id}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
					{
						try
						{
							Lote lote = db.Lotes.Find(model.lote_id);
							db.Lotes.Remove(lote);
							db.SaveChanges();
							MessageBox.Show($"El lote con ID {model.lote_id} fue eliminado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
							FillDatagrid();
						}
						catch (Exception)
						{
							MessageBox.Show($"El lote con ID {model.lote_id} no pudo ser eliminado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Seleccione el lote que desea eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

	}
}
