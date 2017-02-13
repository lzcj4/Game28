using Game28UI.ViewModel;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Game28UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (obj, sender) =>
            {
                this.DataContext = new MainViewModel();
            };

        }

        //[Import]
        public MainViewModel ViewModel { get { return this.DataContext as MainViewModel; } }
    }
}
