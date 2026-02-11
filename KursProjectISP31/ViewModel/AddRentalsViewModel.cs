using KursProjectISP31.Model;
using KursProjectISP31.Services;
using KursProjectISP31.Utills;
using System;
using System.Collections.ObjectModel;

namespace KursProjectISP31.ViewModel
{
    public class AddRentalsViewModel : ViewModelBase
    {
        // Поля
        private Client? _selectedClient;
        private Car? _selectedCar;
        private DateTime? _issueDate;
        private DateTime? _returnDate;

        // Свойства
        public Client? SelectedClient
        {
            get => _selectedClient;
            set { _selectedClient = value; OnPropertyChanged(); UpdateSummary(); }
        }

        public Car? SelectedCar
        {
            get => _selectedCar;
            set { _selectedCar = value; OnPropertyChanged(); UpdateSummary(); }
        }

        public DateTime? IssueDate
        {
            get => _issueDate;
            set { _issueDate = value; OnPropertyChanged(); UpdateSummary(); }
        }

        public DateTime? ReturnDate
        {
            get => _returnDate;
            set { _returnDate = value; OnPropertyChanged(); UpdateSummary(); }
        }

        public ObservableCollection<Client> Clients { get; } = new();
        public ObservableCollection<Car> Cars { get; } = new();

        private RentalDraft _draft = new();
        public RentalDraft Draft
        {
            get => _draft;
            set { _draft = value; OnPropertyChanged(); }
        }

        // Методы
        private void UpdateSummary()
        {
            Draft = new RentalDraft
            {
                SelectedClient = SelectedClient,
                SelectedCar = SelectedCar,
                IssueDate = IssueDate,
                ReturnDate = ReturnDate
            };
        }

        private void LoadDataFromDb()
        {
            try
            {
                var clientService = new ClientService();
                var carService = new CarService();

                var clients = clientService.GetAll();
                var cars = carService.GetAll();

                foreach (var client in clients)
                    Clients.Add(client);

                foreach (var car in cars)
                    Cars.Add(car);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки из БД: {ex.Message}");
                // Опционально: можно показать MessageBox
            }
        }

        // Конструктор
        public AddRentalsViewModel()
        {
            LoadDataFromDb(); // ← вместо LoadMockData()
            UpdateSummary();
        }

        // 👇👇👇 КОМАНДЫ ВСТАВЛЯЕМ СЮДА (в конец класса) 👇👇👇
        public RelayCommand AddCommand => new RelayCommand(() =>
        {
            if (SelectedClient == null || SelectedCar == null || IssueDate == null)
                return;

            var rental = new Rental
            {
                ClientID = SelectedClient.ClientID,
                CarID = SelectedCar.CarID,
                IssueDate = IssueDate.Value,
                ReturnDate = ReturnDate,
                RentalPrice = 5000m,
                IsPaid = false,
                EmployeeID = 1 // ← можно заменить позже на текущего пользователя
            };

            var service = new RentalService();
            if (service.Add(rental))
            {
                System.Diagnostics.Debug.WriteLine("Аренда сохранена!");
                // Опционально: обновить список аренд или очистить форму
            }
        });

        public RelayCommand CancelCommand => new RelayCommand(() =>
        {
            SelectedClient = null;
            SelectedCar = null;
            IssueDate = null;
            ReturnDate = null;
        });
    }
}