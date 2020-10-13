using System.Windows;

namespace Example2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SongViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = base.DataContext as SongViewModel;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ArtistName = "中孝介";
        }
    }
}
