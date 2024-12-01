using InventarioApp.DB;
using InventarioApp.Models;
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
    /// Lógica de interacción para ProductoWindow.xaml
    /// </summary>
    public partial class ProductoWindow : Window
    {
        public ProductoWindow()
        {
            InitializeComponent();
        }

		StockmanagerdbContext db = new StockmanagerdbContext();
		public ProductoDetailModel model;

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			cmbCategoria.ItemsSource = db.Categoria.ToList();
			cmbCategoria.DisplayMemberPath = "CategoriaNombre";
			cmbCategoria.SelectedValuePath = "CategoriaId";
			cmbCategoria.SelectedIndex = -1;
			cmbInventario.ItemsSource = db.Inventarios.ToList();
			cmbInventario.DisplayMemberPath = "InventarioNombre";
			cmbInventario.SelectedValuePath = "InventarioId";
			cmbInventario.SelectedIndex = -1;
			if (model != null && model.producto_id != "")
			{
				txtProductoId.Text = model.producto_id;
				txtProductoId.IsReadOnly = true;
				txtNombre.Text = model.nombre;
				txtDescripcion.Text = model.descripcion;
				txtPreciouni.Text = model.preciounitario.ToString();
				cmbCategoria.SelectedValue = model.categoria_id;
				cmbInventario.SelectedValue = model.inventario_id;
				txtNombre.Focus();
			}
		}

		private void btnGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (txtProductoId.Text.Trim() == "" || txtNombre.Text.Trim() == "" || txtPreciouni.Text.Trim() == "" || txtDescripcion.Text.Trim() == "" || cmbCategoria.SelectedIndex == -1 || cmbInventario.SelectedIndex == -1)
			{
				MessageBox.Show("Debe completar todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			// Validación de ProductoId (alfanumérico)
			if (string.IsNullOrWhiteSpace(txtProductoId.Text) || !Regex.IsMatch(txtProductoId.Text, "^[a-zA-Z0-9]+$"))
			{
				MessageBox.Show("El ID del producto debe ser alfanumérico y no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validación del Nombre
			if (string.IsNullOrWhiteSpace(txtNombre.Text) || txtNombre.Text.Length < 3 || txtNombre.Text.Length > 70)
			{
				MessageBox.Show("El nombre del producto debe tener entre 3 y 50 caracteres.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validación de Descripción
			if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || txtDescripcion.Text.Length > 200)
			{
				MessageBox.Show("La descripción del producto es obligatoria y debe tener un máximo de 200 caracteres.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validación de Precio Estimado
			if (!decimal.TryParse(txtPreciouni.Text, out decimal precio) || precio < 0 || precio > 10000)
			{
				MessageBox.Show("El precio estimado debe ser un valor numérico entre 0 y 10,000.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validación de Categoría e Inventario
			if (cmbCategoria.SelectedIndex == -1)
			{
				MessageBox.Show("Debe seleccionar una categoría para el producto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (cmbInventario.SelectedIndex == -1)
			{
				MessageBox.Show("Debe seleccionar un inventario para el producto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			else
			{
				if (model != null && model.producto_id != "")
				{
					//Actualizar producto
					try
					{
						Producto producto = db.Productos.Find(model.producto_id);
						producto.ProductoId = txtProductoId.Text.Trim();
						producto.Nombre = txtNombre.Text.Trim();
						producto.Descripcion = txtDescripcion.Text.Trim();
						producto.Preciounitario = Convert.ToDecimal(txtPreciouni.Text.Trim());
						producto.CategoriaId = Convert.ToInt32(cmbCategoria.SelectedValue);
						producto.InventarioId = Convert.ToString(cmbInventario.SelectedValue);
						db.SaveChanges();
						MessageBox.Show("El producto fue actualizado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
						txtProductoId.Clear();
						txtNombre.Clear();
						txtPreciouni.Clear();
						txtDescripcion.Clear();
						cmbCategoria.SelectedIndex = -1;
						cmbInventario.SelectedIndex = -1;
					}
					catch (Exception)
					{
						MessageBox.Show($"El producto Id {txtProductoId.Text} no pudo ser actualizado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
			}
		}

		private void btnCerrar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
