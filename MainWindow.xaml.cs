using Hashing.CustomControls;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows;
using System.Media;
using System.Windows.Media;

namespace Hashing
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool calculateButtonEnabled = true;

        public bool CalculateButtonEnabled
        {
            get { return calculateButtonEnabled; }
            set
            {
                calculateButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private string status = "No calculations yet";

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }



        List<HashAlgorithm> algorithms = new List<HashAlgorithm>()
        {
            MD5.Create(),
            SHA1.Create(),
            SHA256.Create(),
            SHA384.Create(),
            SHA512.Create()
        };


        private async void CalculateBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!FileSelectorInstance.PathIsValid) MessageBox.Show("Invalid file path!");
            else
            {
                CalculateButtonEnabled = false;
                Status = "Computing...";
                    foreach(var algorithm in algorithms)
                    {
                        using (FileStream inputStream = new FileStream(FileSelectorInstance.Path, FileMode.Open))
                        {
                            string displayControlName = algorithm.GetType().ReflectedType.Name + "Display";

                            HashDisplayControl control = FindName(displayControlName) as HashDisplayControl;

                            control.HashColor = Brushes.Gray;

                            byte[] hash = await algorithm.ComputeHashAsync(inputStream);
                            string formattedHash = string.Join("", hash.Select(b => b.ToString("x2")));


                            control.CalculatedHash = formattedHash;
                            control.HashColor = Brushes.Black;
                        }

                    }
                CalculateButtonEnabled = true;
                Status = "Done!";
            }
        }
    }
}