using MarkDownBookLauncher.ViewModel;
using System;
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
        VMOpenBook vmOpenBook;
        VMCreateBook vmCreateBook = new();

        public MainWindow(string mdxPath)
        {
            InitializeComponent();
            vmOpenBook = new VMOpenBook(mdxPath);
            btFirefox.DataContext = vmOpenBook;
            btVSCode.DataContext = vmOpenBook;

            tbCreatePath.DataContext = vmCreateBook;
            tbProjName.DataContext = vmCreateBook;
            tbRelPath.DataContext = vmCreateBook;
            btRevert.DataContext = vmCreateBook;
            btCreate.DataContext = vmCreateBook;
            btCreateSelectDir.DataContext = vmCreateBook;
        }

    }
}