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
    /// Lógica de interacción para PedidoNuevoListView.xaml
    /// </summary>
    public partial class PedidoNuevoListView : UserControl
    {
		StockmanagerdbContext db = new StockmanagerdbContext();
		List<PedidoNuevoDetailModel> listaPedido = new List<PedidoNuevoDetailModel>();

		public PedidoNuevoListView()
        {
            InitializeComponent();
        }

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			FillDatagrid();
		}

		void FillDatagrid()
		{

			cmbNTipoPago.ItemsSource = db.TipoPagos.OrderBy(x => x.TipoPagoId).ToList();
			cmbNTipoPago.DisplayMemberPath = "Descripcion";
			cmbNTipoPago.SelectedValuePath = "TipoPagoId";
			cmbNTipoPago.SelectedIndex = -1;
			cmbNProveedor.ItemsSource = db.Proveedors.OrderBy(x => x.ProveedorId).ToList();
			cmbNProveedor.DisplayMemberPath = "ProveedorNombre";
			cmbNProveedor.SelectedValuePath = "ProveedorId";
			cmbNProveedor.SelectedIndex = -1;
			listaPedido = db.PedidoNuevos.Include(x => x.Proveedor).Include(x => x.TipoPago)
			.Select(x => new PedidoNuevoDetailModel
			{
				pedido_nuevo_id = x.PedidoNuevoId,
				pedido_nuevo_fecha = x.PedidoNuevoFecha,
				proveedor_id = x.ProveedorId,
				proveedor_nombre = x.Proveedor.ProveedorNombre,
				fecha_entrega = x.FechaEntrega,
				tipo_pago_id = x.TipoPagoId,
				descripcion = x.TipoPago.Descripcion
			}).ToList();
			gridPedidoNuevo.ItemsSource = listaPedido;
		}

		private void btnBuscar_Click(object sender, RoutedEventArgs e)
		{
			List<PedidoNuevoDetailModel> searchList = listaPedido;


			// Validación para txtNPedidoId: verificar si el campo es alfanumérico y no está vacío
			if (!string.IsNullOrWhiteSpace(txtNPedidoId.Text))
			{
				string pedidoId = txtNPedidoId.Text.Trim();

				// Validación de caracteres alfanuméricos
				if (!System.Text.RegularExpressions.Regex.IsMatch(pedidoId, @"^[a-zA-Z0-9]+$"))
				{
					MessageBox.Show("El ID de pedido debe ser alfanumérico (solo letras y números).", "Error de validación", MessageBoxButton.OK, MessageBoxImage.Warning);
					return; // Salimos del método si el ID no es alfanumérico
				}

				searchList = searchList.Where(x => x.pedido_nuevo_id.Equals(pedidoId, StringComparison.OrdinalIgnoreCase)).ToList();
			}

			if (calNFechaPedido.SelectedDate.HasValue)
			{
				var selectedPedidoFecha = DateOnly.FromDateTime(calNFechaPedido.SelectedDate.Value);
				searchList = searchList.Where(x => x.pedido_nuevo_fecha == selectedPedidoFecha).ToList();
			}

			if (cmbNProveedor.SelectedIndex != -1)
				searchList = searchList.Where(x => x.proveedor_id == Convert.ToString(cmbNProveedor.SelectedValue)).ToList();
			if (calNFechaEntrega.SelectedDate.HasValue)
			{
				var selectedFechaEntrega = DateOnly.FromDateTime(calNFechaEntrega.SelectedDate.Value);
				searchList = searchList.Where(x => x.fecha_entrega == selectedFechaEntrega).ToList();
			}
			if (cmbNTipoPago.SelectedIndex != -1)
				searchList = searchList.Where(x => x.tipo_pago_id == Convert.ToString(cmbNTipoPago.SelectedValue)).ToList();

			gridPedidoNuevo.ItemsSource = searchList;
		}

		private void btnLimpiar_Click(object sender, RoutedEventArgs e)
		{
			txtNPedidoId.Clear();
			calNFechaPedido.SelectedDate = null;
			cmbNProveedor.SelectedIndex = -1;
			calNFechaEntrega.SelectedDate = null;
			cmbNTipoPago.SelectedIndex = -1;
			gridPedidoNuevo.ItemsSource = listaPedido;
		}

		private void btnNuevo_Click(object sender, RoutedEventArgs e)
		{
			DetalleNuevoPedidoWindow window = new DetalleNuevoPedidoWindow();
			window.ShowDialog();
			FillDatagrid();
		}

		private void btnActualizar_Click(object sender, RoutedEventArgs e)
		{
			if (gridPedidoNuevo.SelectedItem != null)
			{
				PedidoNuevoDetailModel model = (PedidoNuevoDetailModel)gridPedidoNuevo.SelectedItem;
				DetalleNuevoPedidoWindow window = new DetalleNuevoPedidoWindow();
				window.model = model;
				window.ShowDialog();
				FillDatagrid();
			}
			else
			{
				MessageBox.Show("Seleccione un pedido para ser actualizado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnEliminar_Click(object sender, RoutedEventArgs e)
		{
			if (gridPedidoNuevo.SelectedItem != null)
			{
				PedidoNuevoDetailModel model = (PedidoNuevoDetailModel)gridPedidoNuevo.SelectedItem;
				if (model != null && model.pedido_nuevo_id != null)
				{
					if (MessageBox.Show($"¿Está seguro de eliminar el pedido Id {model.pedido_nuevo_id}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
					{
						try
						{
							PedidoNuevo pedido = db.PedidoNuevos.Find(model.pedido_nuevo_id)!;
							db.PedidoNuevos.Remove(pedido);
							db.SaveChanges();
							MessageBox.Show($"El pedido de Id {model.pedido_nuevo_id} fue eliminado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
							FillDatagrid();
						}
						catch (Exception)
						{
							MessageBox.Show($"El pedido Id {model.pedido_nuevo_id} no pudo ser eliminado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Seleccione el pedido que desea eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
