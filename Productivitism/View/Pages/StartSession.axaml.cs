using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Productivitism.View;


namespace Productivitism;

public partial class StartSession : UserControl
{
    public StartSession()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}