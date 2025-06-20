using CarRentalSystem.ViewModel;
using KursProjectISP31.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace KursProjectISP31.ViewModel
{
    public class RentalViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Cars> _availableCars;
        private readonly ObservableCollection<Clients> _clients;
        private readonly ObservableCollection<Employees> _employees;
        private readonly ObservableCollection<AdditionalService> _services;

        private Rentals _currentRental = new Rentals();
        private Rentals _selectedRental;
        private string _filterText;
        private ICollectionView _rentalsView;

        public ObservableCollection<Rentals> Rental { get; }
        public ObservableCollection<Cars> AvailableCar => _availableCars;
        public ObservableCollection<Clients> Client => _clients;
        public ObservableCollection<Employees> Employee => _employees;
        public ObservableCollection<AdditionalService> Service => _services;

        public Rental CurrentRental
        {
            get => _currentRental;
            set
            {
                _currentRental = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsRentalSelected));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public Rentals SelectedRental
        {
            get => _selectedRental;
            set
            {
                _selectedRental = value;
                CurrentRental = value != null ? new Rentals
                {
                    RentalID = value.RentalID,
                    IssueDate = value.IssueDate,
                    RentalPeriod = value.RentalPeriod,
                    ReturnDate = value.ReturnDate,
                    CarID = value.CarID,
                    ClientID = value.ClientID,
                    EmployeeID = value.EmployeeID,
                    Service1ID = value.Service1ID,
                    Service2ID = value.Service2ID,
                    Service3ID = value.Service3ID,
                    RentalPrice = value.RentalPrice,
                    IsPaid = value.IsPaid
                } : new Rentals();
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsRentalSelected));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public bool IsRentalSelected => SelectedRental != null;

        public decimal TotalPrice
        {
            get
            {
                decimal price = CurrentRental.RentalPrice;
                if (CurrentRental.Service1 != null) price += CurrentRental.Service1.ServicePrice;
                if (CurrentRental.Service2 != null) price += CurrentRental.Service2.ServicePrice;
                if (CurrentRental.Service3 != null) price += CurrentRental.Service3.ServicePrice;
                return price;
            }
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged();
                _rentalsView.Refresh();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand CalculatePriceCommand { get; }
        public ICommand PrintContractCommand { get; }

        public RentalViewModel()
        {
            Rental = new ObservableCollection<Rentals>();
            _availableCars = new ObservableCollection<Cars>();
            _clients = new ObservableCollection<Clients>();
            _employees = new ObservableCollection<Employees>();
            _services = new ObservableCollection<AdditionalService>();

            InitializeCommands();
            LoadInitialData();
            SetupView();
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddRental, CanAddRental);
            SaveCommand = new RelayCommand(UpdateRental, CanUpdateRental);
            DeleteCommand = new RelayCommand(DeleteRental, CanDeleteRental);
            ClearCommand = new RelayCommand(ClearForm);
            CalculatePriceCommand = new RelayCommand(CalculatePrice);
            PrintContractCommand = new RelayCommand(PrintContract);
        }

        private void LoadInitialData()
        {
            // Загрузка тестовых данных (в реальном приложении - из базы)
            _availableCars.Add(new Cars
            {
                CarID = 1,
                RegistrationNumber = "A123BC",
                DailyRentalPrice = 2500,
                CarBrands = new CarBrands { BrandName = "Toyota Camry" }
            });

            _clients.Add(new Clients
            {
                ClientID = 1,
                FullName = "Иванов Иван Иванович",
                PassportData = "4510 123456"
            });

            _employees.Add(new Employee
            {
                EmployeeID = 1,
                FullName = "Петрова Мария Сергеевна",
                Position = new Position { PositionName = "Менеджер" }
            });

            _services.Add(new AdditionalService
            {
                ServiceID = 1,
                ServiceName = "Страховка",
                ServicePrice = 1000
            });

            Rentals.Add(new Rental
            {
                RentalID = 1,
                IssueDate = DateTime.Now,
                RentalPeriod = 3,
                CarID = 1,
                ClientID = 1,
                EmployeeID = 1,
                RentalPrice = 7500,
                IsPaid = true
            });
        }

        private void SetupView()
        {
            _rentalsView = CollectionViewSource.GetDefaultView(Rentals);
            _rentalsView.Filter = FilterRentals;
        }

        private bool FilterRentals(object obj)
        {
            if (string.IsNullOrWhiteSpace(FilterText)) return true;

            if (obj is Rental rental)
            {
                return rental.Client?.FullName.Contains(FilterText, StringComparison.OrdinalIgnoreCase) == true ||
                       rental.Car?.RegistrationNumber.Contains(FilterText, StringComparison.OrdinalIgnoreCase) == true ||
                       rental.RentalID.ToString().Contains(FilterText);
            }
            return false;
        }

        private void AddRental()
        {
            var newRental = new Rental
            {
                IssueDate = CurrentRental.IssueDate,
                RentalPeriod = CurrentRental.RentalPeriod,
                ReturnDate = CurrentRental.ReturnDate,
                CarID = CurrentRental.CarID,
                ClientID = CurrentRental.ClientID,
                EmployeeID = CurrentRental.EmployeeID,
                Service1ID = CurrentRental.Service1ID,
                Service2ID = CurrentRental.Service2ID,
                Service3ID = CurrentRental.Service3ID,
                RentalPrice = CurrentRental.RentalPrice,
                IsPaid = CurrentRental.IsPaid
            };

            newRental.RentalID = Rentals.Count > 0 ? Rentals.Max(r => r.RentalID) + 1 : 1;
            Rentals.Add(newRental);

            ClearForm();
        }

        private void UpdateRental()
        {
            if (SelectedRental != null)
            {
                SelectedRental.IssueDate = CurrentRental.IssueDate;
                SelectedRental.RentalPeriod = CurrentRental.RentalPeriod;
                SelectedRental.ReturnDate = CurrentRental.ReturnDate;
                SelectedRental.CarID = CurrentRental.CarID;
                SelectedRental.ClientID = CurrentRental.ClientID;
                SelectedRental.EmployeeID = CurrentRental.EmployeeID;
                SelectedRental.Service1ID = CurrentRental.Service1ID;
                SelectedRental.Service2ID = CurrentRental.Service2ID;
                SelectedRental.Service3ID = CurrentRental.Service3ID;
                SelectedRental.RentalPrice = CurrentRental.RentalPrice;
                SelectedRental.IsPaid = CurrentRental.IsPaid;

                var index = Rentals.IndexOf(SelectedRental);
                Rentals[index] = SelectedRental;
            }
        }

        private void DeleteRental()
        {
            if (SelectedRental != null)
            {
                Rentals.Remove(SelectedRental);
                ClearForm();
            }
        }

        private void ClearForm()
        {
            CurrentRental = new Rental { IssueDate = DateTime.Today };
            SelectedRental = null;
        }

        private void CalculatePrice()
        {
            if (CurrentRental.CarID > 0 && CurrentRental.RentalPeriod > 0)
            {
                var car = AvailableCars.FirstOrDefault(c => c.CarID == CurrentRental.CarID);
                if (car != null)
                {
                    CurrentRental.RentalPrice = car.DailyRentalPrice * CurrentRental.RentalPeriod;
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        private void PrintContract()
        {
            // Логика печати договора аренды
            // Можно использовать PDF-генератор или отчеты
        }

        private bool CanAddRental()
        {
            return CurrentRental.CarID > 0 &&
                   CurrentRental.ClientID > 0 &&
                   CurrentRental.EmployeeID > 0 &&
                   CurrentRental.RentalPeriod > 0;
        }

        private bool CanUpdateRental()
        {
            return SelectedRental != null && CanAddRental();
        }

        private bool CanDeleteRental()
        {
            return SelectedRental != null;
        }

        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool> _canExecute;

            public event EventHandler CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value;
                remove => CommandManager.RequerySuggested -= value;
            }

            public RelayCommand(Action execute, Func<bool> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

            public void Execute(object parameter) => _execute();
        }
    }
}