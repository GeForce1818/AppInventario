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
using System.Windows.Shapes;

namespace InventarioApp.Views
{
	/// <summary>
	/// Lógica de interacción para StockWindow.xaml
	/// </summary>
	public partial class StockWindow : Window
	{
		public StockWindow()
		{
			InitializeComponent();
		}

		private StockmanagerdbContext db = new StockmanagerdbContext();
		public StockDetailModel model;

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			cmbStockEstado.ItemsSource = db.EstadoStocks.OrderBy(x => x.EstadoId).ToList();
			cmbStockEstado.DisplayMemberPath = "EstadoNombre";
			cmbStockEstado.SelectedValuePath = "EstadoId";
			cmbStockEstado.SelectedIndex = -1;
			cmbLoteId.ItemsSource = db.Lotes.OrderBy(x => x.LoteId).ToList();
			cmbLoteId.DisplayMemberPath = "LoteId";
			cmbLoteId.SelectedValuePath = "LoteId";
			cmbLoteId.SelectedIndex = -1;

			if (model != null && model.stock_id != " ")
			{
				txtStockId.Text = model.stock_id.ToString();
				cmbLoteId.SelectedValue = model.lote_id;
				txtCantidadDisponible.Text = model.cantidad_disponible.ToString();
				cmbStockEstado.SelectedValue = model.stock_estado;
				txtStockId.IsReadOnly = true;
			}
		}

		private void btnGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (cmbLoteId.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtCantidadDisponible.Text) ||
				cmbStockEstado.SelectedItem == null)
			{
				MessageBox.Show("Debe completar todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			if (string.IsNullOrWhiteSpace(txtStockId.Text))
			{
				MessageBox.Show("El ID de stock no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar que StockId sea alfanumérico usando una expresión regular
			if (!System.Text.RegularExpressions.Regex.IsMatch(txtStockId.Text, @"^[a-zA-Z0-9]+$"))
			{
				MessageBox.Show("El ID de stock debe ser alfanumérico (solo letras y números).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar longitud de StockId (ajustar la longitud según sea necesario)
			if (txtStockId.Text.Length < 5 || txtStockId.Text.Length > 20) // ejemplo de longitud
			{
				MessageBox.Show("El ID de stock debe tener entre 5 y 20 caracteres.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Verificar si el StockId ya existe en la base de datos
			if (db.Stocks.Any(x => x.StockId == txtStockId.Text))
			{
				MessageBox.Show("El ID de stock ya existe.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			// Validar que CantidadDisponible no esté vacío y sea un número válido
			if (string.IsNullOrWhiteSpace(txtCantidadDisponible.Text))
			{
				MessageBox.Show("La cantidad disponible no puede estar vacía.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			int cantidadDisponible;
			if (!int.TryParse(txtCantidadDisponible.Text, out cantidadDisponible))
			{
				MessageBox.Show("La cantidad disponible debe ser un número entero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar que la cantidad sea positiva
			if (cantidadDisponible <= 0)
			{
				MessageBox.Show("La cantidad disponible debe ser un número positivo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			// Validar que se haya seleccionado un LoteId
			if (cmbLoteId.SelectedIndex == -1)
			{
				MessageBox.Show("Debe seleccionar un lote.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			// Validar que se haya seleccionado un estado de stock
			if (cmbStockEstado.SelectedIndex == -1 || cmbStockEstado.SelectedItem == null)
			{
				MessageBox.Show("Debe seleccionar un estado para el stock.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			try
			{
				if (model != null && model.stock_id != " ")
				{
					// Actualizar stock
					Stock stock = db.Stocks.Find(model.stock_id);
					stock.LoteId = Convert.ToString(cmbLoteId.SelectedValue);
					stock.CantidadDisponible = int.Parse(txtCantidadDisponible.Text);
					stock.StockEstado = Convert.ToInt32(cmbStockEstado.SelectedValue);
					db.SaveChanges();
					MessageBox.Show("El stock fue actualizado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCampos();
					txtStockId.IsReadOnly = false;
				}
				else
				{
					// Guardar nuevo stock
					Stock stock = new Stock();
					stock.StockId = Convert.ToString(txtStockId.Text);
					stock.LoteId = Convert.ToString(cmbLoteId.SelectedValue);
					stock.CantidadDisponible = int.Parse(txtCantidadDisponible.Text);
					stock.StockEstado = Convert.ToInt32(cmbStockEstado.SelectedValue);
					db.Stocks.Add(stock);
					db.SaveChanges();
					MessageBox.Show("El stock fue guardado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCampos();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("No se pudo guardar el stock. Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void LimpiarCampos()
		{
			txtStockId.Clear();
			cmbLoteId.SelectedIndex = -1;
			txtCantidadDisponible.Clear();
			cmbStockEstado.SelectedIndex = -1;
		}

		private void btnCerrar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
