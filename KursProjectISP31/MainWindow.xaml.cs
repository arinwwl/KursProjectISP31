using System.Windows;

namespace CarRentalSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
            this.MinHeight = 600;
            this.MinWidth = 900;

           
            this.StateChanged += (sender, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.BorderThickness = new Thickness(7);
                }
                else
                {
                    this.BorderThickness = new Thickness(0);
                }
            };
        }
    }
}