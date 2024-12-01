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
using System.Windows.Shapes;

namespace InventarioApp.Views
{
	/// <summary>
	/// Lógica de interacción para DetalleNuevoPedidoWindow.xaml
	/// </summary>
	public partial class DetalleNuevoPedidoWindow : Window
	{
		StockmanagerdbContext db = new StockmanagerdbContext();
		List<ProductoNuevoDetailModel> listaProductos = new List<ProductoNuevoDetailModel>();
		List<LoteDetailModel> listaLotes = new List<LoteDetailModel>();
		List<DetallePedidoNuevoDetailModel> listaDetallePedido = new List<DetallePedidoNuevoDetailModel>();
		public PedidoNuevoDetailModel model;
		bool productoEncontrado = false;

		public DetalleNuevoPedidoWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			cmbNTipopago.ItemsSource = db.TipoPagos.ToList();
			cmbNTipopago.DisplayMemberPath = "Descripcion";
			cmbNTipopago.SelectedValuePath = "TipoPagoId";
			cmbNTipopago.SelectedIndex = -1;
			cmbNProveedor.ItemsSource = db.Proveedors.ToList();
			cmbNProveedor.DisplayMemberPath = "ProveedorNombre";
			cmbNProveedor.SelectedValuePath = "ProveedorId";
			cmbNProveedor.SelectedIndex = -1;
			cmbNProductoId.ItemsSource = db.ProductoNuevos.ToList();
			cmbNProductoId.DisplayMemberPath = "Nombre";
			cmbNProductoId.SelectedValuePath = "ProductoNuevoId";
			cmbNProductoId.SelectedIndex = -1;
			borAgregarPedido.IsEnabled = true;
			borAgregarProducto.IsEnabled = false;
			if (model != null && model.pedido_nuevo_id != null)
			{
				txtPedidoId.Text = model.pedido_nuevo_id.ToString();
				cmbNProveedor.SelectedValue = model.proveedor_id;
				cmbNTipopago.SelectedValue = model.tipo_pago_id;
				calNFechaPedido.SelectedDate = model.pedido_nuevo_fecha.ToDateTime(TimeOnly.MinValue);
				calNFechaEntrega.SelectedDate = model.fecha_entrega.HasValue ? model.fecha_entrega.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null;
				txtPedidoId.IsReadOnly = true;

				FillDatagrid();
			}
			else
			{
				listaProductos = db.ProductoNuevos.Include(x => x.Categoria).Select(x => new ProductoNuevoDetailModel
				{
					producto_nuevo_id = x.ProductoNuevoId,
					nombre = x.Nombre,
					descripcion = x.Descripcion,
					precio_estimado = x.PrecioEstimado,
					categoria_id = x.CategoriaId,
					categoria_nombre = x.Categoria.CategoriaNombre,
					estado_registro = x.EstadoRegistro,
					estado = x.EstadoRegistroNavigation.Estado
				}).ToList();
			}
		}

		void FillDatagrid()
		{
			listaDetallePedido = db.DetallePedidoNuevos.Where(x => x.PedidoNuevoId == Convert.ToString(txtPedidoId.Text)).Select(x => new DetallePedidoNuevoDetailModel
			{
				pedido_nuevo_id = x.PedidoNuevoId,
				producto_nuevo_id = x.ProductoNuevoId,
				nombre = x.ProductoNuevo.Nombre,
				cantidad_solicitada = x.CantidadSolicitada,
				precio_unitario = x.ProductoNuevo.PrecioEstimado,
				lote_id = x.LoteId,
				fecha_fabricacion = x.FechaFabricacion,
				fecha_vencimiento = x.FechaVencimiento
			}).ToList();
			listaProductos = db.ProductoNuevos.Include(x => x.Categoria).Select(x => new ProductoNuevoDetailModel
			{
				producto_nuevo_id = x.ProductoNuevoId,
				nombre = x.Nombre,
				descripcion = x.Descripcion,
				precio_estimado = x.PrecioEstimado,
				categoria_id = x.CategoriaId,
				categoria_nombre = x.Categoria.CategoriaNombre,
				estado_registro = x.EstadoRegistro,
				estado = x.EstadoRegistroNavigation.Estado
			}).ToList();
			gridDetallePedido.ItemsSource = listaDetallePedido;
			decimal totalPedido = listaDetallePedido.Sum(x => x.Subtotal);
			lblTotalPedido.Content = totalPedido.ToString("C2");
		}

		private void btnGuardar_Click(object sender, RoutedEventArgs e)
		{
			// Validación de campos obligatorios

			// Validación de txtPedidoId (alfanumérico)
			if (!Regex.IsMatch(txtPedidoId.Text, "^[a-zA-Z0-9]+$"))
			{
				MessageBox.Show("El ID del pedido debe contener solo caracteres alfanuméricos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				txtPedidoId.Focus();
				return;
			}

			// Validación de fecha de entrega posterior a fecha de pedido
			if (calNFechaEntrega.SelectedDate.HasValue && calNFechaEntrega.SelectedDate < calNFechaPedido.SelectedDate)
			{
				MessageBox.Show("La fecha de entrega debe ser posterior a la fecha del pedido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				calNFechaEntrega.Focus();
				return;
			}
			if (cmbNProveedor.SelectedIndex == -1 || cmbNTipopago.SelectedIndex == -1 || calNFechaPedido.SelectedDate == null)
			{
				MessageBox.Show("Debe completar todos los campos requeridos para el pedido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			else
			{
				if (model != null && model.pedido_nuevo_id != null)
				{
					//Actualizar pedido
					try
					{
						PedidoNuevo pedido = db.PedidoNuevos.Find(model.pedido_nuevo_id);
						pedido.ProveedorId = Convert.ToString(cmbNProveedor.SelectedValue);
						pedido.TipoPagoId = Convert.ToString(cmbNTipopago.SelectedValue);
						pedido.PedidoNuevoFecha = calNFechaPedido.SelectedDate.HasValue ? DateOnly.FromDateTime(calNFechaPedido.SelectedDate.Value) : DateOnly.MinValue;
						pedido.FechaEntrega = calNFechaEntrega.SelectedDate.HasValue ? DateOnly.FromDateTime(calNFechaEntrega.SelectedDate.Value) : (DateOnly?)null;
						db.SaveChanges();
						borAgregarPedido.IsEnabled = false;
						borAgregarProducto.IsEnabled = true;
						txtPedidoId.Text = pedido.PedidoNuevoId.ToString();
						cmbNProductoId.Focus();
						txtPedidoId.IsReadOnly = false;
					}
					catch (Exception)
					{
						MessageBox.Show($"El pedido no pudo ser almacenado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
				else
				{
					//Guardar Nuevo pedido
					try
					{
						PedidoNuevo pedido = new PedidoNuevo();
						pedido.PedidoNuevoId = Convert.ToString(txtPedidoId.Text);
						pedido.ProveedorId = Convert.ToString(cmbNProveedor.SelectedValue);
						pedido.TipoPagoId = Convert.ToString(cmbNTipopago.SelectedValue);
						pedido.PedidoNuevoFecha = calNFechaPedido.SelectedDate.HasValue ? DateOnly.FromDateTime(calNFechaPedido.SelectedDate.Value) : DateOnly.MinValue;
						pedido.FechaEntrega = calNFechaEntrega.SelectedDate.HasValue ? DateOnly.FromDateTime(calNFechaEntrega.SelectedDate.Value) : (DateOnly?)null;
						db.PedidoNuevos.Add(pedido);
						db.SaveChanges();
						borAgregarPedido.IsEnabled = false;
						borAgregarProducto.IsEnabled = true;
						txtPedidoId.Text = pedido.PedidoNuevoId.ToString();
						cmbNProductoId.Focus();
					}
					catch (Exception)
					{
						MessageBox.Show($"El pedido no pudo ser almacenado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
			}
		}

		private void btnBuscar_Click(object sender, RoutedEventArgs e)
		{
			if (cmbNProductoId.SelectedIndex != -1)
			{
				ProductoNuevoDetailModel productoBuscado = listaProductos.FirstOrDefault(x => x.producto_nuevo_id == Convert.ToString(cmbNProductoId.SelectedValue));
				if (productoBuscado != null)
				{
					txtNPreciouni.Text = productoBuscado.precio_estimado.ToString();
					productoEncontrado = true;
				}
				else
				{
					MessageBox.Show($"No existe el producto de Id {cmbNProductoId.Text}", "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					productoEncontrado = false;
					txtNPreciouni.Clear();
					cmbNProductoId.Focus();
				}
			}

		}
		private void btnAgregar_Click(object sender, RoutedEventArgs e)
		{
			if (cmbNProductoId.SelectedIndex == -1)
			{
				MessageBox.Show("Debe seleccionar un producto.", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
				cmbNProductoId.Focus();
				return;
			}

			// Validación de cantidad solicitada (números enteros positivos)
			if (!int.TryParse(txtNCantidadSolicitada.Text, out int cantidad) || cantidad <= 0)
			{
				MessageBox.Show("La cantidad solicitada debe ser un número entero positivo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				txtNCantidadSolicitada.Focus();
				return;
			}

			// Validación de precio unitario (decimal positivo)
			if (!decimal.TryParse(txtNPreciouni.Text, out decimal precio) || precio <= 0)
			{
				MessageBox.Show("El precio unitario debe ser un número decimal positivo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				txtNPreciouni.Focus();
				return;
			}

			// Validación de fecha de vencimiento posterior a fecha de fabricación
			if (calNFechaVenci.SelectedDate.HasValue && calNFechaFabricacion.SelectedDate.HasValue && calNFechaVenci.SelectedDate <= calNFechaFabricacion.SelectedDate)
			{
				MessageBox.Show("La fecha de vencimiento debe ser posterior a la fecha de fabricación.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				calNFechaVenci.Focus();
				return;
			}

			if (productoEncontrado && txtNCantidadSolicitada.Text.Trim() != "" && txtNPreciouni.Text.Trim() != "")
			{
				try
				{
						DetallePedidoNuevo detalle = new DetallePedidoNuevo();
						detalle.PedidoNuevoId = Convert.ToString(txtPedidoId.Text);
						detalle.ProductoNuevoId = Convert.ToString(cmbNProductoId.SelectedValue);
						detalle.CantidadSolicitada = Convert.ToInt32(txtNCantidadSolicitada.Text);
						detalle.PrecioUnitario = Convert.ToDecimal(txtNPreciouni.Text);
						detalle.FechaFabricacion = calNFechaFabricacion.SelectedDate.HasValue ? DateOnly.FromDateTime(calNFechaFabricacion.SelectedDate.Value) : (DateOnly?)null;
						detalle.FechaVencimiento = calNFechaVenci.SelectedDate.HasValue ? DateOnly.FromDateTime(calNFechaVenci.SelectedDate.Value) : (DateOnly?)null;
						detalle.LoteId = Convert.ToString(txtNIdLote.Text);
						db.DetallePedidoNuevos.Add(detalle);
						ProductoNuevo producto = db.ProductoNuevos.Find(detalle.ProductoNuevoId);
						db.SaveChanges();
						FillDatagrid();
						productoEncontrado = false;
						cmbNProductoId.SelectedIndex = -1;
						txtNPreciouni.Clear();
						txtNCantidadSolicitada.Clear();
						cmbNProductoId.Focus();
				}
				catch (Exception)
				{
					MessageBox.Show($"El producto Id {cmbNProductoId.Text} no pudo ser almacenado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				MessageBox.Show($"Debe completar los datos requeridos.", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void txtCantidadPedido_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
		}

		private void txtNPreciouni_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			// Validación de entrada de solo números y un punto decimal para el precio unitario
			e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]*(?:\.[0-9]*)?$");
		}

		private void btnQuitar_Click(object sender, RoutedEventArgs e)
		{
			if (gridDetallePedido.SelectedItem != null)
			{
				DetallePedidoNuevoDetailModel model = (DetallePedidoNuevoDetailModel)gridDetallePedido.SelectedItem;
				if (model != null && (model.pedido_nuevo_id != "" || model.producto_nuevo_id != ""))
				{
					if (MessageBox.Show($"¿Esta seguro de eliminar el producto Id {model.producto_nuevo_id} de este pedido?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
					{
						try
						{
							DetallePedidoNuevo detalle = db.DetallePedidoNuevos.SingleOrDefault(x => x.PedidoNuevoId == model.pedido_nuevo_id && x.ProductoNuevoId == model.producto_nuevo_id);
							ProductoNuevo producto = db.ProductoNuevos.Find(detalle.ProductoNuevoId);
							db.DetallePedidoNuevos.Remove(detalle);
							db.SaveChanges();
							MessageBox.Show($"El producto de Id {model.producto_nuevo_id} fue eliminado del pedido.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
							FillDatagrid();
						}
						catch (Exception)
						{
							MessageBox.Show($"El producto Id {model.producto_nuevo_id} no pudo ser eliminado del pedido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Seleccione el producto que desea aliminar del pedido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btnCerrar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
