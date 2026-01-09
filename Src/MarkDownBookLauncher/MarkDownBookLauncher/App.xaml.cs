using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace MarkDownBookLauncher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string mdxPath = "";
            if (e.Args.Length == 0)
            {
            }
            else
            {
                mdxPath = e.Args[0];
            }
            MainWindow mw = new MainWindow(mdxPath);
            mw.Show();
        }
    }

}
