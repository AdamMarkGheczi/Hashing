using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace Hashing.CustomControls
{
    public partial class FileSelector : UserControl, INotifyPropertyChanged
    {
        public FileSelector()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void OnPropertyChanged( [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private Visibility placeholderVisibility;

        public Visibility PlaceholderVisibility
        {
            get { return placeholderVisibility; }
            set
            {
                placeholderVisibility = value;
                OnPropertyChanged();
            }
        }


        private bool pathIsValid;

        public bool PathIsValid
        {
            get { return pathIsValid; }
            set
            {
                pathIsValid = value;
            }
        }

        private string path;

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                pathIsValid = File.Exists(path);
                OnPropertyChanged();
            }
        }


        private void PathTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PathTB.Text == "")
                PlaceholderVisibility = Visibility.Visible;
            else
                PlaceholderVisibility = Visibility.Hidden;

        }

        private void BrowseBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            try
            {
                openFileDialog.CheckFileExists = true;
                openFileDialog.Title = "Select File";
                openFileDialog.ShowDialog();
                if(openFileDialog.FileName != "") Path = openFileDialog.FileName;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
