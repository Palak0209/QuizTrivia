using System.Configuration;
using System.Data;
using System.Windows;

namespace QuizGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException
            (object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Unhandled exception: {e.Exception.Message}", "ERROR",
                MessageBoxButton.OK, MessageBoxImage.Error);

            e.Handled = true;
        }
    }

}
