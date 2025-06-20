using CarRentalSystem.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace CarRentalSystem.ViewModel
{
    public class ServiceViewModel : BaseViewModel
    {
        private AdditionalService _currentService = new AdditionalService();
        private AdditionalService _selectedService;
        private string _filterText;
        private ICollectionView _servicesView;

        public ObservableCollection<AdditionalService> Services { get; }

        public AdditionalService CurrentService
        {
            get => _currentService;
            set
            {
                _currentService = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsServiceSelected));
            }
        }

        public AdditionalService SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                CurrentService = value != null ? new AdditionalService
                {
                    ServiceID = value.ServiceID,
                    ServiceName = value.ServiceName,
                    ServicePrice = value.ServicePrice,
                    Description = value.Description
                } : new AdditionalService();
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsServiceSelected));
            }
        }

        public bool IsServiceSelected => SelectedService != null;

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged();
                _servicesView.Refresh();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand GeneratePriceListCommand { get; }

        public ServiceViewModel()
        {
            Services = new ObservableCollection<AdditionalService>();

            InitializeCommands();
            LoadInitialData();
            SetupView();
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddService, CanAddService);
            SaveCommand = new RelayCommand(UpdateService, CanUpdateService);
            DeleteCommand = new RelayCommand(DeleteService, CanDeleteService);
            ClearCommand = new RelayCommand(ClearForm);
            GeneratePriceListCommand = new RelayCommand(GeneratePriceList);
        }

        private void LoadInitialData()
        {
            // Загрузка тестовых данных (в реальном приложении - из базы)
            Services.Add(new AdditionalService
            {
                ServiceID = 1,
                ServiceName = "Полная страховка",
                ServicePrice = 1500,
                Description = "Полное страхование без франшизы"
            });

            Services.Add(new AdditionalService
            {
                ServiceID = 2,
                ServiceName = "Детское кресло",
                ServicePrice = 500,
                Description = "Детское автомобильное кресло группы 1/2/3"
            });

            Services.Add(new AdditionalService
            {
                ServiceID = 3,
                ServiceName = "Навигатор",
                ServicePrice = 300,
                Description = "GPS навигатор с актуальными картами"
            });
        }

        private void SetupView()
        {
            _servicesView = CollectionViewSource.GetDefaultView(Services);
            _servicesView.Filter = FilterServices;
        }

        private bool FilterServices(object obj)
        {
            if (string.IsNullOrWhiteSpace(FilterText)) return true;

            if (obj is AdditionalService service)
            {
                return service.ServiceName.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                       service.Description.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                       service.ServicePrice.ToString().Contains(FilterText);
            }
            return false;
        }

        private void AddService()
        {
            var newService = new AdditionalService
            {
                ServiceName = CurrentService.ServiceName,
                ServicePrice = CurrentService.ServicePrice,
                Description = CurrentService.Description
            };

            newService.ServiceID = Services.Count > 0 ? Services.Max(s => s.ServiceID) + 1 : 1;
            Services.Add(newService);

            ClearForm();
        }

        private void UpdateService()
        {
            if (SelectedService != null)
            {
                SelectedService.ServiceName = CurrentService.ServiceName;
                SelectedService.ServicePrice = CurrentService.ServicePrice;
                SelectedService.Description = CurrentService.Description;

                var index = Services.IndexOf(SelectedService);
                Services[index] = SelectedService;
            }
        }

        private void DeleteService()
        {
            if (SelectedService != null)
            {
                Services.Remove(SelectedService);
                ClearForm();
            }
        }

        private void ClearForm()
        {
            CurrentService = new AdditionalService();
            SelectedService = null;
        }

        private void GeneratePriceList()
        {
            // Логика генерации прайс-листа услуг
            // Можно реализовать через PDF-генератор или отчеты
        }

        private bool CanAddService()
        {
            return !string.IsNullOrWhiteSpace(CurrentService.ServiceName) &&
                   CurrentService.ServicePrice >= 0;
        }

        private bool CanUpdateService()
        {
            return SelectedService != null && CanAddService();
        }

        private bool CanDeleteService()
        {
            return SelectedService != null;
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