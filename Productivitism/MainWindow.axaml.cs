using Avalonia.Controls;
using Productivitism.View;

namespace Productivitism
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}