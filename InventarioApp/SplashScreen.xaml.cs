using Microsoft.EntityFrameworkCore;
using InventarioApp.DB;
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
using InventarioApp.Views;

namespace InventarioApp
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            Loaded += SplashScreen_Loaded;
        }

        private async void SplashScreen_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => InitializeApplication());
        }

        private async Task InitializeApplication()
        {
            try
            {
                using (var db = new StockmanagerdbContext())
                {
                    // Realizar una consulta temprana para calentar el contexto
                    var warmup = await db.Productos.FirstOrDefaultAsync();

                    // Simulación de una tarea para retrasar la pantalla de inicio
                    await Task.Delay(2000);
                }

                // Abrir la ventana principal en el hilo de la interfaz de usuario
                Dispatcher.Invoke(() =>
                {
                    var iniciosesion = new InicioSesionWindow();
                    iniciosesion.Show();
                    this.Close();
                });
            }
            catch (Exception ex)
            {
                // Manejo de errores de conexión a la base de datos
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"Error al conectar a la base de datos: {ex.Message}");
                    Application.Current.Shutdown();
                });
            }
        }
    }

}
