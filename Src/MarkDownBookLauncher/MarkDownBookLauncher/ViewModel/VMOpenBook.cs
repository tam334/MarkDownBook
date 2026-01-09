using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace MarkDownBookLauncher.ViewModel
{
    internal class VMOpenBook : INotifyPropertyChanged
    {

        class ExeInfo
        {
            public string path = "";
            public string tooltip = "";
            public string arguments = "";
        };

        ExeInfo exeFirefox = new();
        ExeInfo exeVsCode = new();

        string _openFile = "";
        public string OpenFile
        {
            get => _openFile;
            set
            {
                _openFile = value;
                OnPropertyChanged(nameof(OpenFile));
            }
        }

        public RelayCommand CommandOpen { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public VMOpenBook(string mdxPath)
        {
            this.OpenFile = mdxPath;
            LoadSettings();

            OpenFirefoxCommand = new OpenFirefoxCommandImpl(this);
            OpenVisualStudioCodeCommand = new OpenVisualStudioCodeCommandImpl(this);

            CommandOpen = new RelayCommand(SelectProj, () => true);
        }

        private void LoadSettings()
        {
            string iniPath = AppContext.BaseDirectory + "\\Script\\Win\\";
            string iniFirefox = "";
            try
            {
                iniFirefox = File.ReadAllText(iniPath + "Firefox.ini");
            }
            catch (Exception)
            {
                MessageBox.Show("設定ファイル" + iniPath + "Firefox.ini" + "が開けません",
                    "エラー",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            string iniVsCode = "";
            try
            {
                iniVsCode = File.ReadAllText(iniPath + "VisualStudioCode.ini");
            }
            catch (Exception)
            {
                MessageBox.Show("設定ファイル" + iniPath + "VisualStudioCode.ini" + "が開けません",
                    "エラー",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }

            string sectionName = "Settings";
            {
                string path = iniPath + "Firefox.ini";
                exeFirefox = new ExeInfo()
                {
                    path = GetIniValue(path, sectionName, "Exe"),
                    tooltip = GetIniValue(path, sectionName, "Tooltip"),
                    arguments = GetIniValue(path, sectionName, "Arguments")
                };
                if (exeFirefox.path == "")
                {
                    MessageBox.Show("設定ファイル" + path + "の内容にエラーがあります。Exeの値が指定されていません",
                        "エラー",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            {
                string path = iniPath + "VisualStudioCode.ini";
                exeVsCode = new ExeInfo()
                {
                    path = GetIniValue(path, sectionName, "Exe"),
                    tooltip = GetIniValue(path, sectionName, "Tooltip"),
                    arguments = GetIniValue(path, sectionName, "Arguments")
                };
                if (exeVsCode.path == "")
                {
                    MessageBox.Show("設定ファイル" + path + "の内容にエラーがあります。Exeの値が指定されていません",
                        "エラー",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        string SubDirPath()
        {
            string subDirPath = GetIniValue(OpenFile, "Project", "ProjectDir");
            if (subDirPath == "")
            {
                MessageBox.Show("プロジェクトファイル" + OpenFile + "が開けません",
                    "エラー",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return "";
            }
            return System.IO.Path.GetDirectoryName(OpenFile) + "\\" + subDirPath;
        }

        string SubFiles()
        {
            string[] mdFiles = Directory.GetFiles(SubDirPath(), "*.md");
            string files = "";
            foreach (string mdFile in mdFiles)
            {
                files += mdFile;
                files += " ";
            }
            files.Remove(files.Length - 1, 1);
            return files;
        }

        public ICommand OpenFirefoxCommand
        {
            get; private set;
        }

        class OpenFirefoxCommandImpl : ICommand
        {
            VMOpenBook parent;
            public OpenFirefoxCommandImpl(VMOpenBook parent)
            {
                this.parent = parent;
            }

            event EventHandler? ICommand.CanExecuteChanged
            {
                add
                {
                }

                remove
                {
                }
            }

            bool ICommand.CanExecute(object? parameter)
            {
                return true;
            }

            void ICommand.Execute(object? parameter)
            {
                Process process = new Process();
                process.StartInfo.FileName = parent.exeFirefox.path;
                process.StartInfo.Arguments = parent.ParseArgument(parent.exeFirefox.arguments);
                process.Start();
            }
        }

        public ICommand OpenVisualStudioCodeCommand
        {
            get; private set;
        }

        class OpenVisualStudioCodeCommandImpl : ICommand
        {
            VMOpenBook parent;
            public OpenVisualStudioCodeCommandImpl(VMOpenBook parent)
            {
                this.parent = parent;
            }

            event EventHandler? ICommand.CanExecuteChanged
            {
                add
                {
                }

                remove
                {
                }
            }

            public void Execute(object? _)
            {
                Process process = new Process();
                process.StartInfo.FileName = parent.exeVsCode.path;
                process.StartInfo.Arguments = parent.ParseArgument(parent.exeVsCode.arguments);
                process.Start();
            }

            bool ICommand.CanExecute(object? parameter)
            {
                return true;
            }
            
        }

        internal string ParseArgument(string argument)
        {
            string tmp = argument.Replace("(dir)", SubDirPath());
            tmp = tmp.Replace("(files)", SubFiles());
            return tmp;
        }

        void SelectProj()
        {
            var ofd = new OpenFileDialog()
            {
                Filter = "MarkdownBook|*.mdx",
                Multiselect = false
            };
            bool? result = ofd.ShowDialog();
            if (result is bool r)
            {
                if (r)
                {
                    OpenFile = ofd.FileName;
                }
            }
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
