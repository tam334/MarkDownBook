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

namespace MarkdownBookCreate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string defaultPath = "_mkbook/";

        public MainWindow(string dirPath)
        {
            InitializeComponent();
            tbPath.Text = DefaultPath();
        }

        private void OnClickRevert(object sender, RoutedEventArgs e)
        {
            this.tbPath.Text = DefaultPath();
        }

        private string DefaultPath()
        {
            return defaultPath + tbProjName.Text + "/";
        }
    }
}