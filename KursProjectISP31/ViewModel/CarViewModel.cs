using KursProjectISP31.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KursProjectISP31.ViewModel
{
    public class CarViewModel : BaseViewModel
    {
        private Cars _currentCar = new Cars();
        public Cars CurrentCar
        {
            get => _currentCar;
            set { _currentCar = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Cars> _cars = new ObservableCollection<Car>();
        public ObservableCollection<Cars> Cars
        {
            get => _cars;
            set { _cars = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public CarViewModel()
        {
            SaveCommand = new RelayCommand(SaveCar);
            DeleteCommand = new RelayCommand(DeleteCar);
            LoadCars();
        }

        private void LoadCars()
        {
            // Загрузка данных из БД
            Cars.Add(new Cars { CarID = 1, RegistrationNumber = "ABC123" });
        }

        private void SaveCar()
        {
            // Логика сохранения
        }

        private void DeleteCar()
        {
            // Логика удаления
        }

        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            public event EventHandler CanExecuteChanged;

            public RelayCommand(Action execute) => _execute = execute;
            public bool CanExecute(object parameter) => true;
            public void Execute(object parameter) => _execute();
        }
    }
}