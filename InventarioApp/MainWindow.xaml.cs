using InventarioApp.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventarioApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void btnProductos_Click(object sender, RoutedEventArgs e)
		{
			lblWindowName.Content = "Lista de Productos";
			DataContext = new ProductoViewModel();
		}

		private void btnRegistroProductos_Click(object sender, RoutedEventArgs e)
		{
			lblWindowName.Content = "Registro de Productos Nuevos";
			DataContext = new RegistroProductoViewModel();
		}

		private void btnExtraerProductos_Click(object sender, RoutedEventArgs e)
		{
			lblWindowName.Content = "Extracción de Productos";
			DataContext = new ExtraccionViewModel();
		}

		private void btnPedidoReposicion_Click(object sender, RoutedEventArgs e)
		{
			lblWindowName.Content = "Pedido de Reposición";
			DataContext = new PedidoViewModel();
		}

		private void btnPedidoNuevo_Click(object sender, RoutedEventArgs e)
		{
			lblWindowName.Content = "Pedido de Productos Nuevos";
			DataContext = new PedidoNuevoViewModel();
		}

		private void btnCategoria_Click(object sender, RoutedEventArgs e)
		{
			lblWindowName.Content = "Listado de Categorías";
			DataContext = new CategoriaViewModel();
		}

		private void btnInventario_Click(object sender, RoutedEventArgs e)
		{
			lblWindowName.Content = "Listado de Inventarios";
			DataContext = new InventarioViewModel();
		}

		private void btnLote_Click(object sender, RoutedEventArgs e)
		{
			lblWindowName.Content = "Listado de Lotes";
			DataContext = new LoteViewModel();
		}

		private void btnStock_Click(object sender, RoutedEventArgs e)
		{
			lblWindowName.Content = "Listado de Stock";
			DataContext = new StockViewModel();
		}

		private void btnProductoNuevo_Click(object sender, RoutedEventArgs e)
		{
			lblWindowName.Content = "Listado de Productos Nuevos a Pedir";
			DataContext = new ProductoNuevoViewModel();
		}

		private void btnProveedor_Click(object sender, RoutedEventArgs e)
		{
			lblWindowName.Content = "Listado de Proveedores";
			DataContext = new ProveedorViewModel();
		}

		private void lnkCerrar_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

	}
}