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
	/// Lógica de interacción para StockListView.xaml
	/// </summary>
	public partial class StockListView : UserControl
	{
		private StockmanagerdbContext db = new StockmanagerdbContext();
		private List<StockDetailModel> listaStock = new List<StockDetailModel>();

		public StockListView()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			FillDatagrid();
		}

		private void FillDatagrid()
		{
			cmbStockEstado.ItemsSource = db.EstadoStocks.OrderBy(x => x.EstadoNombre).ToList();
			cmbStockEstado.DisplayMemberPath = "EstadoNombre";
			cmbStockEstado.SelectedValuePath = "EstadoId";
			cmbStockEstado.SelectedIndex = -1;
			cmbLoteId.ItemsSource = db.Lotes.OrderBy(x => x.LoteId).ToList();
			cmbLoteId.DisplayMemberPath = "LoteId";
			cmbLoteId.SelectedValuePath = "LoteId";
			cmbLoteId.SelectedIndex = -1;
			listaStock = db.Stocks.Include(x => x.StockEstadoNavigation)
				.Select(x => new StockDetailModel
				{
					stock_id = x.StockId,
					lote_id = x.LoteId,
					cantidad_disponible = x.CantidadDisponible,
					stock_estado = x.StockEstado,
					estado_nombre = x.StockEstadoNavigation.EstadoNombre
				}).ToList();
			gridStock.ItemsSource = listaStock;
		}

		private void btnBuscar_Click(object sender, RoutedEventArgs e)
		{
			List<StockDetailModel> searchList = listaStock;
			if (!string.IsNullOrWhiteSpace(txtStockId.Text))
			{
				if (!Regex.IsMatch(txtStockId.Text, @"^[a-zA-Z0-9]+$"))
				{
					MessageBox.Show("El Stock ID debe ser alfanumérico.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}
				searchList = searchList.Where(x => x.stock_id == txtStockId.Text).ToList();
			}
			if (cmbLoteId.SelectedIndex != -1)
				searchList = searchList.Where(x => x.lote_id == Convert.ToString(cmbLoteId.SelectedValue)).ToList();
			if (cmbStockEstado.SelectedIndex != -1)
				searchList = searchList.Where(x => x.stock_estado == Convert.ToInt32(cmbStockEstado.SelectedValue)).ToList();
			// Si no se encontraron resultados, mostrar mensaje
			if (!searchList.Any())
			{
				MessageBox.Show("No se encontraron registros que coincidan con los criterios de búsqueda.", "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			gridStock.ItemsSource = searchList;
		}

		private void btnLimpiar_Click(object sender, RoutedEventArgs e)
		{
			txtStockId.Clear();
			cmbLoteId.SelectedIndex = -1;
			txtCantidadDisponible.Clear();
			cmbStockEstado.SelectedIndex = -1;
			gridStock.ItemsSource = listaStock;
		}

		private void btnNuevo_Click(object sender, RoutedEventArgs e)
		{
			StockWindow window = new StockWindow();
			window.ShowDialog();
			FillDatagrid();
		}

		private void btnActualizar_Click(object sender, RoutedEventArgs e)
		{
			if (gridStock.SelectedItem != null)
			{
				StockDetailModel model = (StockDetailModel)gridStock.SelectedItem;
				StockWindow window = new StockWindow();
				window.model = model;
				window.ShowDialog();
				FillDatagrid();
			}
			else
			{
				MessageBox.Show("Seleccione un registro de stock para actualizar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnEliminar_Click(object sender, RoutedEventArgs e)
		{
			if (gridStock.SelectedItem != null)
			{
				StockDetailModel model = (StockDetailModel)gridStock.SelectedItem;
				if (model != null && model.stock_id != "")
				{
					if (MessageBox.Show($"¿Está seguro de eliminar el registro de stock con ID {model.stock_id}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
					{
						try
						{
							Stock stock = db.Stocks.Find(model.stock_id);
							db.Stocks.Remove(stock);
							db.SaveChanges();
							MessageBox.Show($"El registro de stock con ID {model.stock_id} fue eliminado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
							FillDatagrid();
						}
						catch (Exception)
						{
							MessageBox.Show($"El registro de stock con ID {model.stock_id} no pudo ser eliminado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Seleccione el registro de stock que desea eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
