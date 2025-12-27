using System.ComponentModel;
using System.IO;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        string defaultPath = "_mkbook/";
        string _createPath = "";
        public string CreatePath {
            get => _createPath;
            set
            {
                _createPath = value;
                OnPropertyChanged(nameof(CreatePath));
            }
        }

        string _projName = "Project";
        public string ProjName
        {
            get => _projName;
            set
            {
                _projName = value;
                OnPropertyChanged(nameof(ProjName));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            tbPath.DataContext = this;
            CreatePath = DefaultPath();

            tbProjName.DataContext = this;
        }

        private void OnClickRevert(object sender, RoutedEventArgs e)
        {
            CreatePath = DefaultPath();
        }

        private void OnClickCreate(object sender, RoutedEventArgs e)
        {
            string path = System.Environment.CurrentDirectory + "\\" + CreatePath;
            Directory.CreateDirectory(path);
            Environment.Exit(0);
        }

        private string DefaultPath()
        {
            return defaultPath + ProjName + "/";
        }
    }
}