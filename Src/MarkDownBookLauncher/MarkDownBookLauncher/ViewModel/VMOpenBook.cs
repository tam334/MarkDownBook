using System;
using System.Collections.Generic;
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
    internal class VMOpenBook
    {
        string mdxPath = "";
        string dir = "";
        string files = "";

        class ExeInfo
        {
            public string path = "";
            public string tooltip = "";
            public string arguments = "";
        };

        ExeInfo exeFirefox = new();
        ExeInfo exeVsCode = new();

        public VMOpenBook(string mdxPath)
        {
            this.mdxPath = mdxPath;
            LoadSettings();

            string[] mdFiles = Directory.GetFiles(dir, "*.md");
            foreach (string mdFile in mdFiles)
            {
                files += mdFile;
                files += " ";
            }
            files.Remove(files.Length - 1, 1);

            OpenFirefoxCommand = new OpenFirefoxCommandImpl(this);
            OpenVisualStudioCodeCommand = new OpenVisualStudioCodeCommandImpl(this);
        }

        private void LoadSettings()
        {
            string subDirPath = GetIniValue(mdxPath, "Project", "ProjectDir");
            if (subDirPath == "")
            {
                MessageBox.Show("プロジェクトファイル" + mdxPath + "が開けません",
                    "エラー",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dir = System.IO.Path.GetDirectoryName(mdxPath) + "\\" + subDirPath;
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
