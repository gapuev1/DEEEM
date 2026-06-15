using System.Windows;
using MovieSearchApp.ViewModels;

namespace MovieSearchApp.Views
{
    public partial class MovieDetailWindow : Window
    {
        public MovieDetailWindow(int movieId)
        {
            InitializeComponent();
            DataContext = new MovieDetailViewModel(movieId);
            Loaded += async (s, e) => await ((MovieDetailViewModel)DataContext).LoadDetailsAsync();
        }
    }
}