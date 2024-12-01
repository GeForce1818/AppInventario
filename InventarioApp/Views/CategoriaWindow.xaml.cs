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
    /// Lógica de interacción para CategoriaWindow.xaml
    /// </summary>
    public partial class CategoriaWindow : Window
    {
        public CategoriaWindow()
        {
            InitializeComponent();
        }

		StockmanagerdbContext db = new StockmanagerdbContext();
		public CategoriaDetailModel model;

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (model != null && model.categoria_id != 0)
			{
				txtCategoriaId.Text = model.categoria_id.ToString();
				txtCategoriaNombre.Text = model.categoria_nombre;
				txtDescripcion.Text = model.descripcion;
				txtCategoriaId.IsReadOnly = true;
			}
		}

		private void btnGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (!ValidarCamposFormulario())
				return;
			if (txtCategoriaNombre.Text.Trim() == "" || txtDescripcion.Text.Trim() == "")
			{
				MessageBox.Show("Debe completar todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				if (model != null && model.categoria_id != 0)
				{
					// Actualizar categoría
					try
					{
						Categorium categoria = db.Categoria.Find(model.categoria_id);
						categoria.CategoriaNombre = txtCategoriaNombre.Text;
						categoria.Descripcion = txtDescripcion.Text;
						db.SaveChanges();
						MessageBox.Show("La categoría fue actualizada correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
						LimpiarCampos();
						txtCategoriaId.IsReadOnly = false;
					}
					catch (Exception)
					{
						MessageBox.Show("La categoría no pudo ser actualizada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
				else
				{
					// Guardar nueva categoría
					try
					{
						Categorium categoria = new Categorium();
						categoria.CategoriaId = Convert.ToInt32(txtCategoriaId.Text);
						categoria.CategoriaNombre = txtCategoriaNombre.Text;
						categoria.Descripcion = txtDescripcion.Text;
						db.Categoria.Add(categoria);
						db.SaveChanges();
						MessageBox.Show("La categoría fue guardada correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
						LimpiarCampos();
					}
					catch (Exception)
					{
						MessageBox.Show("La categoría no pudo ser almacenada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
			}
		}

		private void LimpiarCampos()
		{
			txtCategoriaId.Clear();
			txtCategoriaNombre.Clear();
			txtDescripcion.Clear();
		}

		private void btnCerrar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private bool ValidarCategoriaId()
		{
			if (string.IsNullOrWhiteSpace(txtCategoriaId.Text))
				return true; // Campo opcional, si está vacío es válido.

			if (!int.TryParse(txtCategoriaId.Text, out int id) || id <= 0)
			{
				MessageBox.Show("Ingrese un ID de categoría válido (número entero positivo).", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}
			return true;
		}

		private bool ValidarCategoriaNombre()
		{
			if (string.IsNullOrWhiteSpace(txtCategoriaNombre.Text))
			{
				MessageBox.Show("El nombre de la categoría no puede estar vacío.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}

			if (txtCategoriaNombre.Text.Length > 50)
			{
				MessageBox.Show("El nombre de la categoría no puede exceder los 50 caracteres.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}
			return true;
		}

		private bool ValidarDescripcion()
		{
			if (txtDescripcion.Text.Length > 200)
			{
				MessageBox.Show("La descripción no puede exceder los 200 caracteres.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}
			return true;
		}

		private bool ValidarCamposFormulario()
		{
			return ValidarCategoriaId() && ValidarCategoriaNombre() && ValidarDescripcion();
		}

	}
}
