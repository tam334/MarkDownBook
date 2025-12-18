using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
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

        class ExeInfo
        {
            public string path = "";
            public string tooltip = "";
            public string arguments = "";
        };

        ExeInfo exeFirefox = new();
        ExeInfo exeVsCode = new();

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

            string sectionName = "Settings";
            {
                string path = relPath + "Firefox.ini";
                exeFirefox = new ExeInfo()
                {
                    path = GetIniValue(path, sectionName, "Exe"),
                    tooltip = GetIniValue(path, sectionName, "Tooltip"),
                    arguments = GetIniValue(path, sectionName, "Arguments")
                };
            }

            {
                string path = relPath + "VisualStudioCode.ini";
                exeVsCode = new ExeInfo()
                {
                    path = GetIniValue(path, sectionName, "Exe"),
                    tooltip = GetIniValue(path, sectionName, "Tooltip"),
                    arguments = GetIniValue(path, sectionName, "Arguments")
                };
            }
        }

        private void OnClickFirefox(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = exeFirefox.path;
            process.StartInfo.Arguments = ParseArgument(exeFirefox.arguments);
            process.Start();
        }

        private void OnClickVisualStudioCode(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = exeVsCode.path;
            process.StartInfo.Arguments = ParseArgument(exeVsCode.arguments);
            process.Start();
        }

        private string ParseArgument(string argument)
        {
            string tmp = argument.Replace("(dir)", dir);
            tmp = tmp.Replace("(files)", files);
            return tmp;
        }

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
            string lpApplicationName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedstring,
            int nSize,
            string lpFileName);

        public static string GetIniValue(string path, string section, string key)
        {
            StringBuilder sb = new StringBuilder(256);
            GetPrivateProfileString(section, key, string.Empty, sb, sb.Capacity, path);
            return sb.ToString();
        }
    }
}