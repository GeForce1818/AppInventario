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
	/// Lógica de interacción para ProductoNuevoWindow.xaml
	/// </summary>
	public partial class ProductoNuevoWindow : Window
	{
		public ProductoNuevoWindow()
		{
			InitializeComponent();
		}

		StockmanagerdbContext db = new StockmanagerdbContext();
		public ProductoNuevoDetailModel model;

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

			cmbCategoria.ItemsSource = db.Categoria.OrderBy(x => x.CategoriaNombre).ToList();
			cmbCategoria.DisplayMemberPath = "CategoriaNombre";
			cmbCategoria.SelectedValuePath = "CategoriaId";
			cmbCategoria.SelectedIndex = -1;

			cmbRegistro.ItemsSource = db.Registros.OrderBy(x => x.RegistroId).ToList();
			cmbRegistro.DisplayMemberPath = "Estado";
			cmbRegistro.SelectedValuePath = "RegistroId";
			cmbRegistro.SelectedIndex = -1;

			if (model != null && !string.IsNullOrWhiteSpace(model.producto_nuevo_id))
			{
				txtProductoNuevoId.Text = model.producto_nuevo_id;
				txtNombre.Text = model.nombre;
				txtDescripcion.Text = model.descripcion;
				txtPrecioEstimado.Text = model.precio_estimado.ToString();
				cmbCategoria.SelectedValue = model.categoria_id;
				cmbRegistro.SelectedValue = model.estado_registro;
				txtProductoNuevoId.IsReadOnly = true;
			}
		}

		private void btnGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
				string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
				string.IsNullOrWhiteSpace(txtPrecioEstimado.Text) ||
				cmbCategoria.SelectedIndex == -1 ||
				cmbRegistro.SelectedIndex == -1)
			{
				MessageBox.Show("Debe completar todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			// Validación de ProductoNuevoId (alfanumérico)
			if (string.IsNullOrWhiteSpace(txtProductoNuevoId.Text) || !System.Text.RegularExpressions.Regex.IsMatch(txtProductoNuevoId.Text, "^[a-zA-Z0-9]+$"))
			{
				MessageBox.Show("El ID del producto debe ser alfanumérico y no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			else if (model == null && db.ProductoNuevos.Any(p => p.ProductoNuevoId == txtProductoNuevoId.Text))
			{
				MessageBox.Show("El ID del producto ya existe. Ingrese uno diferente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
			if (!decimal.TryParse(txtPrecioEstimado.Text, out decimal precio) || precio < 0 || precio > 10000)
			{
				MessageBox.Show("El precio estimado debe ser un valor numérico entre 0 y 10,000.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validación de Categoría
			if (cmbCategoria.SelectedIndex == -1)
			{
				MessageBox.Show("Debe seleccionar una categoría para el producto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validación de Estado de Registro
			if (cmbRegistro.SelectedIndex == -1)
			{
				MessageBox.Show("Debe seleccionar un estado de registro para el producto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			try
			{
				if (model != null && !string.IsNullOrWhiteSpace(model.producto_nuevo_id))
				{
					// Actualizar producto
					ProductoNuevo producto = db.ProductoNuevos.Find(model.producto_nuevo_id);
					producto.Nombre = txtNombre.Text;
					producto.Descripcion = txtDescripcion.Text;
					producto.PrecioEstimado = decimal.Parse(txtPrecioEstimado.Text);
					producto.CategoriaId = (int)cmbCategoria.SelectedValue;
					producto.EstadoRegistro = (int)cmbRegistro.SelectedValue;
					db.SaveChanges();
					MessageBox.Show("El producto fue actualizado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCampos();
					txtProductoNuevoId.IsReadOnly = false;
				}
				else
				{
					// Guardar nuevo producto
					ProductoNuevo producto = new ProductoNuevo();
					producto.ProductoNuevoId = txtProductoNuevoId.Text;
					producto.Nombre = txtNombre.Text;
					producto.Descripcion = txtDescripcion.Text;
					producto.PrecioEstimado = decimal.Parse(txtPrecioEstimado.Text);
					producto.CategoriaId = (int)cmbCategoria.SelectedValue;
					producto.EstadoRegistro = (int)cmbRegistro.SelectedValue;
					db.ProductoNuevos.Add(producto);
					db.SaveChanges();
					MessageBox.Show("El producto fue guardado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCampos();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("No se pudo guardar el producto. Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void LimpiarCampos()
		{
			txtProductoNuevoId.Clear();
			txtNombre.Clear();
			txtDescripcion.Clear();
			txtPrecioEstimado.Clear();
			cmbCategoria.SelectedIndex = -1;
			cmbRegistro.SelectedIndex = -1;
		}

		private void btnCerrar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
