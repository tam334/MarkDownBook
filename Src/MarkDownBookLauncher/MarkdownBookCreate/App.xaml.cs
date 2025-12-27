using System.Configuration;
using System.Data;
using System.Windows;

namespace MarkdownBookCreate
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
                MessageBox.Show("ディレクトリのパスが指定されていません",
                   "エラー",
                   MessageBoxButton.OK,
                   MessageBoxImage.Error);
                Environment.Exit(1);
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
