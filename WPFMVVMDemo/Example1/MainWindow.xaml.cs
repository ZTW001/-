using System.Windows;

namespace Example1
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
            //_viewModel = new SongViewModel();
            //base.DataContext = _viewModel;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            //界面不会更新
            _viewModel.ArtistName = "中孝介";
        }
    }
}
