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
	/// Lógica de interacción para DetallePedidoWindow.xaml
	/// </summary>
	public partial class DetallePedidoWindow : Window

	{
		StockmanagerdbContext db = new StockmanagerdbContext();
		List<ProductoDetailModel> listaProductos = new List<ProductoDetailModel>();
		List<LoteDetailModel> listaLotes = new List<LoteDetailModel>();
		List<DetallePedidoDetailModel> listaDetallePedido = new List<DetallePedidoDetailModel>();
		public PedidoDetailModel model;
		bool productoEncontrado = false;
		public DetallePedidoWindow()
        {
            InitializeComponent();
        }
		

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			cmbTipopago.ItemsSource = db.TipoPagos.ToList();
			cmbTipopago.DisplayMemberPath = "Descripcion";
			cmbTipopago.SelectedValuePath = "TipoPagoId";
			cmbTipopago.SelectedIndex = -1;
			cmbProveedor.ItemsSource = db.Proveedors.ToList();
			cmbProveedor.DisplayMemberPath = "ProveedorNombre";
			cmbProveedor.SelectedValuePath = "ProveedorId";
			cmbProveedor.SelectedIndex = -1;
			cmbProductoId.ItemsSource = db.Productos.ToList();
			cmbProductoId.DisplayMemberPath = "Nombre";
			cmbProductoId.SelectedValuePath = "ProductoId";
			cmbProductoId.SelectedIndex = -1;
			borAgregarPedido.IsEnabled = true;
			borAgregarProducto.IsEnabled = false;
			if (model != null && model.pedido_id != null)
			{
				txtPedidoId.Text = model.pedido_id.ToString();
				cmbProveedor.SelectedValue = model.proveedor_id;
				cmbTipopago.SelectedValue = model.tipo_pago_id;
				calFechaPedido.SelectedDate = model.pedido_fecha.HasValue ? model.pedido_fecha.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null;
				calFechaEntrega.SelectedDate = model.fecha_entrega.HasValue ? model.fecha_entrega.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null;
				txtPedidoId.IsReadOnly = true;


				FillDatagrid();
			}
			else
			{
				listaProductos = db.Productos.Include(x => x.Categoria).Include(x => x.Inventario).Select(x => new ProductoDetailModel
				{
					producto_id = x.ProductoId,
					nombre = x.Nombre,
					descripcion = x.Descripcion,
					preciounitario = x.Preciounitario,
					categoria_id = x.CategoriaId,
					categoria_nombre = x.Categoria.CategoriaNombre,
					inventario_id = x.InventarioId,
					inventario_nombre = x.Inventario.InventarioNombre,
				}).ToList();
			}
		}

		void FillDatagrid()
		{
			listaDetallePedido = db.DetallePedidos.Where(x => x.PedidoId == Convert.ToString(txtPedidoId.Text)).Select(x => new DetallePedidoDetailModel
			{
				pedido_id = x.PedidoId,
				producto_id = x.ProductoId,
				nombre = x.Producto.Nombre,
				cantidad_solicitada = x.CantidadSolicitada,
				precio_unitario = x.Producto.Preciounitario,
				lote_id = x.LoteId,
				fecha_fabricacion = x.FechaFabricacion,
				fecha_vencimiento = x.FechaVencimiento,
			}).ToList();
			listaProductos = db.Productos.Include(x => x.Categoria).Include(x => x.Inventario).Select(x => new ProductoDetailModel
			{
				producto_id = x.ProductoId,
				nombre = x.Nombre,
				descripcion = x.Descripcion,
				preciounitario = x.Preciounitario,
				categoria_id = x.CategoriaId,
				categoria_nombre = x.Categoria.CategoriaNombre,
				inventario_id = x.InventarioId,
				inventario_nombre = x.Inventario.InventarioNombre,
			}).ToList();
			gridDetallePedido.ItemsSource = listaDetallePedido;
			decimal totalPedido = listaDetallePedido.Sum(x => x.Subtotal);
			lblTotalPedido.Content = totalPedido.ToString("C2");
		}

		private void btnGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (!Regex.IsMatch(txtPedidoId.Text, @"^[A-Za-z0-9]+$"))
			{
				MessageBox.Show("El ID del Pedido debe contener solo caracteres alfanuméricos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			if (calFechaEntrega.SelectedDate.HasValue && calFechaPedido.SelectedDate.HasValue)
			{
				if (calFechaEntrega.SelectedDate < calFechaPedido.SelectedDate)
				{
					MessageBox.Show("La fecha de entrega no puede ser anterior a la fecha de pedido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
			}
			if (cmbProveedor.SelectedIndex == -1 || cmbTipopago.SelectedIndex == -1 || calFechaPedido.SelectedDate == null)
			{
				MessageBox.Show("Debe completar todos los campos requeridos para el pedido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			else
			{
				if (model != null && model.pedido_id != null)
				{
					//Actualizar pedido
					try
					{
						Pedido pedido = db.Pedidos.Find(model.pedido_id);
						pedido.ProveedorId = Convert.ToString(cmbProveedor.SelectedValue);
						pedido.TipoPagoId = Convert.ToString(cmbTipopago.SelectedValue);
						pedido.PedidoFecha = calFechaPedido.SelectedDate.HasValue ? DateOnly.FromDateTime(calFechaPedido.SelectedDate.Value) : (DateOnly?)null;
						pedido.FechaEntrega = calFechaEntrega.SelectedDate.HasValue ? DateOnly.FromDateTime(calFechaEntrega.SelectedDate.Value) : (DateOnly?)null;
						db.SaveChanges();
						borAgregarPedido.IsEnabled = false;
						borAgregarProducto.IsEnabled = true;
						txtPedidoId.Text = pedido.PedidoId.ToString();
						cmbProductoId.Focus();
						txtPedidoId.IsReadOnly= false;
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
						Pedido pedido = new Pedido();
						pedido.PedidoId = Convert.ToString(txtPedidoId.Text);
						pedido.ProveedorId = Convert.ToString(cmbProveedor.SelectedValue);
						pedido.TipoPagoId = Convert.ToString(cmbTipopago.SelectedValue);
						pedido.PedidoFecha = calFechaPedido.SelectedDate.HasValue ? DateOnly.FromDateTime(calFechaPedido.SelectedDate.Value) : (DateOnly?)null;
						pedido.FechaEntrega = calFechaEntrega.SelectedDate.HasValue ? DateOnly.FromDateTime(calFechaEntrega.SelectedDate.Value) : (DateOnly?)null;
						db.Pedidos.Add(pedido);
						db.SaveChanges();
						borAgregarPedido.IsEnabled = false;
						borAgregarProducto.IsEnabled = true;
						txtPedidoId.Text = pedido.PedidoId.ToString();
						cmbProductoId.Focus();
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
			if (cmbProductoId.SelectedIndex != -1)
			{
				ProductoDetailModel productoBuscado = listaProductos.FirstOrDefault(x => x.producto_id == Convert.ToString(cmbProductoId.SelectedValue));
				if (productoBuscado != null)
				{
					txtPreciouni.Text = productoBuscado.preciounitario.ToString();
					productoEncontrado = true;
				}
				else
				{
					MessageBox.Show($"No existe el producto de Id {cmbProductoId.Text}", "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					productoEncontrado = false;
					txtPreciouni.Clear();
					cmbProductoId.Focus();
				}
			}

		}
		private void btnAgregar_Click(object sender, RoutedEventArgs e)
		{
			// Validación de campos antes de agregar detalle del pedido
			if (string.IsNullOrWhiteSpace(txtCantidadSolicitada.Text) || string.IsNullOrWhiteSpace(txtPreciouni.Text))
			{
				MessageBox.Show("Debe completar los datos requeridos.", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (!Regex.IsMatch(txtIDLote.Text, @"^[A-Za-z0-9]+$"))
			{
				MessageBox.Show("El ID del Lote debe contener solo caracteres alfanuméricos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (!int.TryParse(txtCantidadSolicitada.Text, out _))
			{
				MessageBox.Show("La cantidad solicitada debe ser un número entero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (!decimal.TryParse(txtPreciouni.Text, out _))
			{
				MessageBox.Show("El precio unitario debe ser un número válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (calFechaFabricacion.SelectedDate.HasValue && calFechaVenci.SelectedDate.HasValue && calFechaFabricacion.SelectedDate > calFechaVenci.SelectedDate)
			{
				MessageBox.Show("La fecha de fabricación no puede ser posterior a la fecha de vencimiento.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (productoEncontrado && txtCantidadSolicitada.Text.Trim() != "" && txtPreciouni.Text.Trim() != "")
			{
				try
				{

						DetallePedido detalle = new DetallePedido();
						detalle.PedidoId = Convert.ToString(txtPedidoId.Text);
						detalle.ProductoId = Convert.ToString(cmbProductoId.SelectedValue);
						detalle.CantidadSolicitada = Convert.ToInt32(txtCantidadSolicitada.Text);
						detalle.FechaFabricacion = calFechaFabricacion.SelectedDate.HasValue ? DateOnly.FromDateTime(calFechaFabricacion.SelectedDate.Value) : (DateOnly?)null;
						detalle.FechaVencimiento = calFechaVenci.SelectedDate.HasValue ? DateOnly.FromDateTime(calFechaVenci.SelectedDate.Value) : (DateOnly?)null;
					    detalle.LoteId = Convert.ToString(txtIDLote.Text);
						detalle.PrecioUnitario = Convert.ToDecimal(txtPreciouni.Text);
						db.DetallePedidos.Add(detalle);
						Producto producto = db.Productos.Find(detalle.ProductoId);
						db.SaveChanges();
						FillDatagrid();
						productoEncontrado = false;
						cmbProductoId.SelectedIndex=-1;
						txtPreciouni.Clear();
						txtCantidadSolicitada.Clear();
						cmbProductoId.Focus();
				}
				catch (Exception)
				{
					MessageBox.Show($"El producto Id {cmbProductoId.Text} no pudo ser almacenado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

		private void btnQuitar_Click(object sender, RoutedEventArgs e)
		{
			if (gridDetallePedido.SelectedItem != null)
			{
				DetallePedidoDetailModel model = (DetallePedidoDetailModel)gridDetallePedido.SelectedItem;
				if (model != null && (model.pedido_id != "" || model.producto_id != ""))
				{
					if (MessageBox.Show($"¿Esta seguro de eliminar el producto Id {model.producto_id} de este pedido?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
					{
						try
						{
							DetallePedido detalle = db.DetallePedidos.SingleOrDefault(x => x.PedidoId == model.pedido_id && x.ProductoId == model.producto_id);
							Producto producto = db.Productos.Find(detalle.ProductoId);
							db.DetallePedidos.Remove(detalle);
							db.SaveChanges();
							MessageBox.Show($"El producto de Id {model.producto_id} fue eliminado del pedido.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
							FillDatagrid();
						}
						catch (Exception)
						{
							MessageBox.Show($"El producto Id {model.producto_id} no pudo ser eliminado del pedido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
