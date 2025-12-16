using System.Diagnostics;
using System.IO;
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
        string dir = "C:\\Users\\ytsuk\\Documents\\MyWork\\MarkdownBook\\Doc\\Plan\\";
        string files = "";

        public MainWindow()
        {
            InitializeComponent();
            LoadSettings();
            files += dir + "00_概要.md";
            files += " ";
            files += dir + "01_日報.md";
        }

        private void LoadSettings()
        {
            string relPath = "Script\\Win\\";
            string iniFirefox = File.ReadAllText(relPath + "Firefox.ini");
            string iniVsCode = File.ReadAllText(relPath + "VisualStudioCode.ini");
        }

        private void OnClickFirefox(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Program Files\\Mozilla Firefox\\firefox.exe";
            process.StartInfo.Arguments = files;
            process.Start();
        }

        private void OnClickVisualStudioCode(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Users\\ytsuk\\AppData\\Local\\Programs\\Microsoft VS Code\\Code.exe";
            process.StartInfo.Arguments = dir;
            process.Start();
        }
    }
}