using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hashing.CustomControls
{
    public partial class HashDisplayControl : UserControl, INotifyPropertyChanged
    {
        public HashDisplayControl()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private string hashFunctionName;

        public string HashFunctionName
        {
            get { return hashFunctionName; }
            set
            {
                hashFunctionName = value;
                OnPropertyChanged();
            }
        }

        private string calculatedHash;

        public string CalculatedHash
        {
            get { return calculatedHash; }
            set
            {
                calculatedHash = value;
                OnPropertyChanged();
            }
        }

        private Brush hashColor = Brushes.Black;

        public Brush HashColor
        {
            get { return hashColor; }
            set
            {
                hashColor = value;
                OnPropertyChanged();
            }
        }

    }
}
