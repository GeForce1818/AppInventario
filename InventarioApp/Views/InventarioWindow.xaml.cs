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
    /// Lógica de interacción para InventarioWindow.xaml
    /// </summary>
    public partial class InventarioWindow : Window
    {
        public InventarioWindow()
        {
            InitializeComponent();
        }

		private StockmanagerdbContext db = new StockmanagerdbContext();
		public InventarioDetailModel model;

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (model != null && !string.IsNullOrEmpty(model.inventario_id))
			{
				txtInventarioId.Text = model.inventario_id;
				txtInventarioNombre.Text = model.inventario_nombre;
				txtInventarioId.IsReadOnly = true;
			}
		}

		private void btnGuardar_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtInventarioId.Text) || string.IsNullOrWhiteSpace(txtInventarioNombre.Text))
			{
				MessageBox.Show("Debe completar todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Validar que el ID del inventario sea alfanumérico
			if (txtInventarioId.Text.Any(c => !Char.IsLetterOrDigit(c)))
			{
				MessageBox.Show("El ID del inventario debe ser alfanumérico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (model != null && !string.IsNullOrEmpty(model.inventario_id))
			{
				// Verificar si el inventario existe en la base de datos
				var inventarioExistente = db.Inventarios.FirstOrDefault(x => x.InventarioId == model.inventario_id);
				if (inventarioExistente == null)
				{
					MessageBox.Show("El inventario con el ID especificado no existe en la base de datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
				// Actualizar inventario
				try
				{
					Inventario inventario = db.Inventarios.Find(model.inventario_id);
					if (inventario != null)
					{
						inventario.InventarioNombre = txtInventarioNombre.Text;
						db.SaveChanges();
						MessageBox.Show("El inventario fue actualizado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
						LimpiarCampos();
						txtInventarioId.IsReadOnly = false;
					}
				}
				catch (Exception)
				{
					MessageBox.Show("El inventario no pudo ser actualizado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				// Validar si el ID del inventario ya existe en la base de datos al intentar guardar uno nuevo
				var inventarioExistente = db.Inventarios.FirstOrDefault(x => x.InventarioId == txtInventarioId.Text);
				if (inventarioExistente != null)
				{
					MessageBox.Show("Ya existe un inventario con el mismo ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
				// Guardar nuevo inventario
				try
				{
					Inventario inventario = new Inventario();
					inventario.InventarioId = txtInventarioId.Text;
					inventario.InventarioNombre = txtInventarioNombre.Text;
					db.Inventarios.Add(inventario);
					db.SaveChanges();
					MessageBox.Show("El inventario fue guardado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
					LimpiarCampos();
				}
				catch (Exception)
				{
					MessageBox.Show("El inventario no pudo ser almacenado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void LimpiarCampos()
		{
			txtInventarioId.Clear();
			txtInventarioNombre.Clear();
		}

		private void btnCerrar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
