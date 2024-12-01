using InventarioApp.DB;
using InventarioApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InventarioApp.Views
{
    /// <summary>
    /// Lógica de interacción para PedidoListView.xaml
    /// </summary>
    public partial class PedidoListView : UserControl
    {
        StockmanagerdbContext db = new StockmanagerdbContext();
        List<PedidoDetailModel> listaPedido = new List<PedidoDetailModel>();

        public PedidoListView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDatagrid();
        }

        void FillDatagrid()
        {

            cmbTipoPago.ItemsSource = db.TipoPagos.OrderBy(x => x.TipoPagoId).ToList();
            cmbTipoPago.DisplayMemberPath = "Descripcion";
            cmbTipoPago.SelectedValuePath = "TipoPagoId";
            cmbTipoPago.SelectedIndex = -1;
			cmbProveedor.ItemsSource = db.Proveedors.OrderBy(x => x.ProveedorId).ToList();
			cmbProveedor.DisplayMemberPath = "ProveedorNombre";
			cmbProveedor.SelectedValuePath = "ProveedorId";
			cmbProveedor.SelectedIndex = -1;
			listaPedido = db.Pedidos.Include(x => x.Proveedor).Include(x => x.TipoPago)
            .Select(x => new PedidoDetailModel
                {
                    pedido_id = x.PedidoId,
                    pedido_fecha = x.PedidoFecha,
                    proveedor_id = x.ProveedorId,
                    proveedor_nombre = x.Proveedor.ProveedorNombre,
                    fecha_entrega = x.FechaEntrega,
                    tipo_pago_id = x.TipoPagoId,
                    descripcion = x.TipoPago.Descripcion
                }).ToList();
            gridPedidos.ItemsSource = listaPedido;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            List<PedidoDetailModel> searchList = listaPedido;

			if (!string.IsNullOrWhiteSpace(txtPedidoId.Text))
			{
				if (txtPedidoId.Text.All(char.IsLetterOrDigit))
				{
					searchList = searchList.Where(x => x.pedido_id == txtPedidoId.Text).ToList();
				}
				else
				{
					MessageBox.Show("El ID del Pedido solo debe contener caracteres alfanuméricos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
			}
			if (calFechaPedido.SelectedDate.HasValue)
            {
                var selectedPedidoFecha = DateOnly.FromDateTime(calFechaPedido.SelectedDate.Value);
                searchList = searchList.Where(x => x.pedido_fecha == selectedPedidoFecha).ToList();
            }

			if (cmbProveedor.SelectedIndex != -1)
				searchList = searchList.Where(x => x.proveedor_id == Convert.ToString(cmbProveedor.SelectedValue)).ToList();
			// Validación de la Fecha de Entrega
			if (calFechaEntrega.SelectedDate.HasValue)
			{
				var selectedFechaEntrega = DateOnly.FromDateTime(calFechaEntrega.SelectedDate.Value);

				// Validación de Fecha de Entrega mayor o igual a Fecha del Pedido
				if (calFechaPedido.SelectedDate.HasValue && selectedFechaEntrega < DateOnly.FromDateTime(calFechaPedido.SelectedDate.Value))
				{
					MessageBox.Show("La Fecha de Entrega no puede ser anterior a la Fecha del Pedido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}

				searchList = searchList.Where(x => x.fecha_entrega == selectedFechaEntrega).ToList();
			}

			if (cmbTipoPago.SelectedIndex != -1)
                searchList = searchList.Where(x => x.tipo_pago_id == Convert.ToString(cmbTipoPago.SelectedValue)).ToList();

            gridPedidos.ItemsSource = searchList;
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtPedidoId.Clear();
            calFechaPedido.SelectedDate = null;
            cmbProveedor.SelectedIndex = -1;
            calFechaEntrega.SelectedDate = null;
            cmbTipoPago.SelectedIndex = -1;
			gridPedidos.ItemsSource = listaPedido;
        }

		private void btnNuevo_Click(object sender, RoutedEventArgs e)
		{
			DetallePedidoWindow window = new DetallePedidoWindow();
			window.ShowDialog();
			FillDatagrid();
		}

		private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (gridPedidos.SelectedItem != null)
            {
                PedidoDetailModel model = (PedidoDetailModel)gridPedidos.SelectedItem;
                DetallePedidoWindow window = new DetallePedidoWindow();
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
            if (gridPedidos.SelectedItem != null)
            {
                PedidoDetailModel model = (PedidoDetailModel)gridPedidos.SelectedItem;
                if (model != null && model.pedido_id != null)
                {
                    if (MessageBox.Show($"¿Está seguro de eliminar el pedido Id {model.pedido_id}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            Pedido pedido = db.Pedidos.Find(model.pedido_id)!;
                            db.Pedidos.Remove(pedido);
                            db.SaveChanges();
                            MessageBox.Show($"El pedido de Id {model.pedido_id} fue eliminado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                            FillDatagrid();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show($"El pedido Id {model.pedido_id} no pudo ser eliminado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
