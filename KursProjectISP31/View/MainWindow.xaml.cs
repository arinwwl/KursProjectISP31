using KursProjectISP31.ViewModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace KursProjectISP31;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = new NavigationViewModel();
        Debug.WriteLine($"DataContext is {(this.DataContext == null ? "NULL" : "set")}");
    }

    private void CloseApp_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}