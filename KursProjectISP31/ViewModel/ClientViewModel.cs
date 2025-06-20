using KursProjectISP31.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KursProjectISP31.ViewModel
{
    public class ClientViewModel : BaseViewModel
    {
        private Clients _currentClient = new Clients();
        private ObservableCollection<Clients> _clients;
        private Clients _selectedClient;

        public Clients CurrentClient
        {
            get => _currentClient;
            set
            {
                _currentClient = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Clients> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }

        public Clients SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                CurrentClient = value != null ? new Clients
                {
                    ClientID = value.ClientID,
                    FullName = value.FullName,
                    Gender = value.Gender,
                    BirthDate = value.BirthDate,
                    Address = value.Address,
                    Phone = value.Phone,
                    PassportData = value.PassportData
                } : new Clients();
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public ClientViewModel()
        {
            Clients = new ObservableCollection<Clients>();
            LoadClients();

            AddCommand = new RelayCommand(AddClient);
            SaveCommand = new RelayCommand(SaveClient, CanSaveClient);
            DeleteCommand = new RelayCommand(DeleteClient, CanDeleteClient);
            ClearCommand = new RelayCommand(ClearForm);
        }

        private void LoadClients()
        {
            // Здесь должна быть логика загрузки клиентов из базы данных
            // Пример тестовых данных:
            Clients.Add(new Clients
            {
                ClientID = 1,
                FullName = "Иванов Иван Иванович",
                Gender = "Мужской",
                BirthDate = new DateTime(1985, 5, 15),
                Address = "г. Москва, ул. Ленина, 10",
                Phone = "+7 (123) 456-7890",
                PassportData = "4510 123456"
            });
        }

        private void AddClient()
        {
            var newClient = new Clients
            {
                FullName = CurrentClient.FullName,
                Gender = CurrentClient.Gender,
                BirthDate = CurrentClient.BirthDate,
                Address = CurrentClient.Address,
                Phone = CurrentClient.Phone,
                PassportData = CurrentClient.PassportData
            };

            // Здесь должна быть логика сохранения в базу данных
            Clients.Add(newClient);
            ClearForm();
        }

        private void SaveClient()
        {
            if (SelectedClient != null)
            {
                SelectedClient.FullName = CurrentClient.FullName;
                SelectedClient.Gender = CurrentClient.Gender;
                SelectedClient.BirthDate = CurrentClient.BirthDate;
                SelectedClient.Address = CurrentClient.Address;
                SelectedClient.Phone = CurrentClient.Phone;
                SelectedClient.PassportData = CurrentClient.PassportData;

                // Здесь должна быть логика обновления в базе данных
            }
        }

        private void DeleteClient()
        {
            if (SelectedClient != null)
            {
                // Здесь должна быть логика удаления из базы данных
                Clients.Remove(SelectedClient);
                ClearForm();
            }
        }

        private void ClearForm()
        {
            CurrentClient = new Clients();
            SelectedClient = null;
        }

        private bool CanSaveClient()
        {
            return SelectedClient != null &&
                   !string.IsNullOrWhiteSpace(CurrentClient.FullName) &&
                   !string.IsNullOrWhiteSpace(CurrentClient.PassportData);
        }

        private bool CanDeleteClient()
        {
            return SelectedClient != null;
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

            public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

            public void Execute(object parameter) => _execute();

            public void RaiseCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}