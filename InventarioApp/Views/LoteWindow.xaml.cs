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
using System.Windows.Shapes;

namespace InventarioApp.Views
{
    /// <summary>
    /// Lógica de interacción para LoteWindow.xaml
    /// </summary>
    public partial class LoteWindow : Window
    {
        public LoteWindow()
        {
            InitializeComponent();
        }

		StockmanagerdbContext db = new StockmanagerdbContext();
		public LoteDetailModel model;

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			cmbLoteEstado.ItemsSource = db.EstadoLotes.OrderBy(x => x.EstadoId).ToList();
			cmbLoteEstado.DisplayMemberPath = "EstadoNombre";
			cmbLoteEstado.SelectedValuePath = "EstadoId";
			cmbLoteEstado.SelectedIndex = -1;
			cmbProductoId.ItemsSource = db.Productos.OrderBy(x => x.Nombre).ToList();
			cmbProductoId.DisplayMemberPath = "Nombre";
			cmbProductoId.SelectedValuePath = "ProductoId";
			cmbProductoId.SelectedIndex = -1;
			if (model != null && model.lote_id != " ")
			{
				txtLoteId.Text = model.lote_id.ToString();
				cmbProductoId.SelectedValue = model.producto_id;
				dpFechaFabricacion.SelectedDate = model.fecha_fabricacion.HasValue ? model.fecha_fabricacion.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null;
				dpFechaVencimiento.SelectedDate = model.fecha_vencimiento.HasValue ? model.fecha_vencimiento.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null;
				txtLoteUbicacion.Text = model.lote_ubicacion;
				cmbLoteEstado.SelectedValue = model.lote_estado;
				txtLoteId.IsReadOnly = true;
			}
		}

		private void btnGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (cmbProductoId.SelectedIndex == -1 || dpFechaFabricacion.SelectedDate == null ||
				dpFechaVencimiento.SelectedDate == null || string.IsNullOrWhiteSpace(txtLoteUbicacion.Text) ||
				cmbLoteEstado.SelectedItem == null)
			{
				MessageBox.Show("Debe completar todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			// Validación de ID de lote alfanumérico
			if (!System.Text.RegularExpressions.Regex.IsMatch(txtLoteId.Text, @"^[a-zA-Z0-9]+$"))
			{
				MessageBox.Show("El ID de lote debe ser alfanumérico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validación de fechas
			if (dpFechaVencimiento.SelectedDate < dpFechaFabricacion.SelectedDate)
			{
				MessageBox.Show("La fecha de vencimiento no puede ser anterior a la fecha de fabricación.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			try
			{
				if (model != null && model.lote_id != " ")
				{
					// Actualizar lote
					Lote lote = db.Lotes.Find(model.lote_id);
					lote.ProductoId = Convert.ToString(cmbProductoId.SelectedValue);
					lote.FechaFabricacion = dpFechaFabricacion.SelectedDate.HasValue ? DateOnly.FromDateTime(dpFechaFabricacion.SelectedDate.Value) : (DateOnly?)null;
					lote.FechaVencimiento = dpFechaVencimiento.SelectedDate.HasValue ? DateOnly.FromDateTime(dpFechaVencimiento.SelectedDate.Value) : (DateOnly?)null;
					lote.LoteUbicacion = txtLoteUbicacion.Text;
					lote.LoteEstado = Convert.ToInt32(cmbLoteEstado.SelectedValue);
					db.SaveChanges();
					MessageBox.Show("El lote fue actualizado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCampos();
					txtLoteId.IsReadOnly = false;
				}
				else
				{
					// Guardar nuevo lote
					Lote lote = new Lote();
					lote.LoteId = Convert.ToString(txtLoteId.Text);
					lote.ProductoId = Convert.ToString(cmbProductoId.SelectedValue);
					lote.FechaFabricacion = dpFechaFabricacion.SelectedDate.HasValue ? DateOnly.FromDateTime(dpFechaFabricacion.SelectedDate.Value) : (DateOnly?)null;
					lote.FechaVencimiento = dpFechaVencimiento.SelectedDate.HasValue ? DateOnly.FromDateTime(dpFechaVencimiento.SelectedDate.Value) : (DateOnly?)null;
					lote.LoteUbicacion = txtLoteUbicacion.Text;
					lote.LoteEstado = Convert.ToInt32(cmbLoteEstado.SelectedValue);
					db.Lotes.Add(lote);
					db.SaveChanges();
					MessageBox.Show("El lote fue guardado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCampos();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("No se pudo guardar el lote. Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void LimpiarCampos()
		{
			txtLoteId.Clear();
			cmbProductoId.SelectedIndex = -1;
			dpFechaFabricacion.SelectedDate = null;
			dpFechaVencimiento.SelectedDate = null;
			txtLoteUbicacion.Clear();
			cmbLoteEstado.SelectedIndex = -1;
		}

		private void btnCerrar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
