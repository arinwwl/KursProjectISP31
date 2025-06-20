using CarRentalSystem.View;
using System.Windows;
using System.Windows.Input;

namespace CarRentalSystem.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand NavigateToCarsCommand { get; }
        public ICommand NavigateToEmployeesCommand { get; }
        public ICommand NavigateToClientsCommand { get; }
        public ICommand NavigateToRentalsCommand { get; }
        public ICommand NavigateToPositionsCommand { get; }
        public ICommand NavigateToBrandsCommand { get; }
        public ICommand NavigateToServicesCommand { get; }
        public ICommand ExitCommand { get; }

        public MainViewModel()
        {
            // Инициализация команд
            NavigateToCarsCommand = new RelayCommand(() => CurrentView = new CarView());
            NavigateToEmployeesCommand = new RelayCommand(() => CurrentView = new EmployeeView());
            NavigateToClientsCommand = new RelayCommand(() => CurrentView = new ClientView());
            NavigateToRentalsCommand = new RelayCommand(() => CurrentView = new RentalView());
            NavigateToPositionsCommand = new RelayCommand(() => CurrentView = new PositionView());
            NavigateToBrandsCommand = new RelayCommand(() => CurrentView = new CertBrandView());
            NavigateToServicesCommand = new RelayCommand(() => CurrentView = new AdditionalServiceView());
            ExitCommand = new RelayCommand(() => Application.Current.Shutdown());

            // Установка начального View
            CurrentView = new CarView();
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();
    }
}