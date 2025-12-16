using System.Diagnostics;
using System.Numerics;
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

namespace MarkDownBookLauncher
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

        private void OnClickFirefox(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Program Files\\Mozilla Firefox\\firefox.exe";
            string path = "C:\\Users\\ytsuk\\Documents\\MyWork\\MarkdownBook\\Doc\\Plan\\";
            process.StartInfo.Arguments = path + "00_概要.md";
            process.StartInfo.Arguments += " ";
            process.StartInfo.Arguments += path + "01_日報.md";
            process.Start();
        }

        private void OnClickVisualStudioCode(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Users\\ytsuk\\AppData\\Local\\Programs\\Microsoft VS Code\\Code.exe";
            string path = "C:\\Users\\ytsuk\\Documents\\MyWork\\MarkdownBook\\Doc\\Plan\\";
            process.StartInfo.Arguments = path;
            process.Start();
        }
    }
}