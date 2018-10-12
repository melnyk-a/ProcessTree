using ProcessTree.Presentation.Wpf.ViewModels;
using System.Windows;

namespace ProcessTree.Presentation.Wpf.Views
{
    internal partial class MainView : Window
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}