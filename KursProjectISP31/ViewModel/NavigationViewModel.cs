using KursProjectISP31.Utills;
using System.Diagnostics;
using System.Windows.Input;

namespace KursProjectISP31.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
                Debug.WriteLine($"Переключено на: {_currentView?.GetType().Name}");
            }
        }

        public ICommand HomeCommand { get; }
        public ICommand ClientsCommand { get; }
        public ICommand EmployeesCommand { get; }
        public ICommand CarsCommand { get; }
        public ICommand CarBrandsCommand { get; }
        public ICommand RentalsCommand { get; }
        public ICommand AddRentalsCommand { get; }

        public NavigationViewModel()
        {
            HomeCommand = new RelayCommand(() => ShowView(new HomeViewModel()));
            ClientsCommand = new RelayCommand(() => ShowView(new ClientsViewModel()));
            EmployeesCommand = new RelayCommand(() => ShowView(new EmployeesViewModel()));
            CarsCommand = new RelayCommand(() => ShowView(new CarsViewModel()));
            CarBrandsCommand = new RelayCommand(() => ShowView(new CarBrandsViewModel()));
            RentalsCommand = new RelayCommand(() => ShowView(new RentalsViewModel()));
            AddRentalsCommand = new RelayCommand(() => CurrentView = new AddRentalsViewModel());


            // Стартовая страница
            CurrentView = new HomeViewModel();
        }

        private void ShowView(ViewModelBase viewModel)
        {
            CurrentView = viewModel;
            Debug.WriteLine($"Показан ViewModel: {viewModel.GetType().Name}");
        }
    }
}
