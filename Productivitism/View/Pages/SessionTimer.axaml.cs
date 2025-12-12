using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Productivitism.View;


namespace Productivitism;

public partial class SessionTimer : UserControl
{
    public SessionTimer()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}