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
        string defaultPath = "_mkbook/";
        string _createPath = "";

        public RelayCommand CommandRevert { get; set;  }
        public RelayCommand CommandCreate { get; set;  }

        public VMCreateBook()
        {
            CreatePath = DefaultPath();
            CommandRevert = new RelayCommand(OnClickRevert, () => true);
            CommandCreate = new RelayCommand(OnClickCreate, () => true);
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        

        private void OnClickRevert()
        {
            CreatePath = DefaultPath();
        }

        private void OnClickCreate()
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
