using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;

namespace MarkDownBookLauncher.ViewModel
{
    internal class VMCreateBook : INotifyPropertyChanged
    {
        string defaultPath = "_mkbook\\";
        string _createPath = "";

        public RelayCommand CommandRevert { get; set;  }
        public RelayCommand CommandCreate { get; set;  }
        public RelayCommand CommandSelectDir { get; set; }

        public VMCreateBook()
        {
            ProjRelPath = DefaultPath();
            CreatePath = "C:\\";
            CommandRevert = new RelayCommand(OnClickRevert, () => true);
            CommandCreate = new RelayCommand(OnClickCreate, () => true);
            CommandSelectDir = new RelayCommand(OnClickSelectDir, () => true);
        }

        public string CreatePath
        {
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

        string _projRelPath = "";
        public string ProjRelPath
        {
            get => _projRelPath;
            set
            {
                _projRelPath = value;
                OnPropertyChanged(nameof(ProjRelPath));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        

        private void OnClickRevert()
        {
            ProjRelPath = DefaultPath();
        }

        private void OnClickCreate()
        {
            string path = CreatePath + "\\" + ProjRelPath;
            Directory.CreateDirectory(path);
            //プロジェクトファイル作成
            string projFile = CreatePath + "\\" + ProjName + ".mdx";
            File.WriteAllText(projFile,
                string.Format("[Project]\r\nProjectDir={0}\n", ProjRelPath));
            File.Copy(AppContext.BaseDirectory + "\\Template\\Template.md", path + "\\Template.md");
            Environment.Exit(0);
        }

        private string DefaultPath()
        {
            return defaultPath + ProjName;
        }

        private void OnClickSelectDir()
        {
            var ofd = new OpenFolderDialog()
            {
                Multiselect = false
            };
            bool? result = ofd.ShowDialog();
            if (result is bool r)
            {
                if (r)
                {
                    CreatePath = ofd.FolderName;
                }
            }
        }
    }
}
