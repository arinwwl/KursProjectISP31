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
    public class EmployeeViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Positions> _positions;
        private Employees _currentEmployee = new Employees();
        private Employees _selectedEmployee;
        private string _filterText;
        private ICollectionView _employeesView;

        public ObservableCollection<Employees> Employees { get; }

        public Employees CurrentEmployee
        {
            get => _currentEmployee;
            set
            {
                _currentEmployee = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEmployeeSelected));
            }
        }

        public Employees SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                CurrentEmployee = value != null ? new Employees
                {
                    EmployeeID = value.EmployeeID,
                    FullName = value.FullName,
                    Age = value.Age,
                    Gender = value.Gender,
                    Address = value.Address,
                    Phone = value.Phone,
                    PassportData = value.PassportData,
                    PositionID = value.PositionID
                } : new Employees();
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEmployeeSelected));
            }
        }

        public bool IsEmployeeSelected => SelectedEmployee != null;

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged();
                _employeesView.Refresh();
            }
        }

        public ObservableCollection<Positions> Positions => _positions;

        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand GenerateReportCommand { get; }

        public EmployeeViewModel()
        {
            Employees = new ObservableCollection<Employees>();
            _positions = new ObservableCollection<Positions>();

            InitializeCommands();
            LoadData();
            SetupView();
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddEmployee, CanAddEmployee);
            SaveCommand = new RelayCommand(UpdateEmployee, CanUpdateEmployee);
            DeleteCommand = new RelayCommand(DeleteEmployee, CanDeleteEmployee);
            ClearCommand = new RelayCommand(ClearForm);
            GenerateReportCommand = new RelayCommand(GenerateReport);
        }

        private void LoadData()
        {
            // Загрузка сотрудников (в реальном приложении - из базы данных)
            Employees.Add(new Employees
            {
                EmployeeID = 1,
                FullName = "Иванов Петр Сергеевич",
                Age = 32,
                Gender = "Мужской",
                Address = "г. Москва, ул. Ленина, 15",
                Phone = "+7 (495) 123-4567",
                PassportData = "4510 654321",
                PositionID = 1
            });

            // Загрузка должностей
            _positions.Add(new Positions { PositionID = 1, PositionName = "Менеджер", Salary = 50000 });
            _positions.Add(new Positions { PositionID = 2, PositionName = "Механик", Salary = 40000 });
            _positions.Add(new Positions { PositionID = 3, PositionName = "Администратор", Salary = 45000 });
        }

        private void SetupView()
        {
            _employeesView = CollectionViewSource.GetDefaultView(Employees);
            _employeesView.Filter = FilterEmployees;
        }

        private bool FilterEmployees(object obj)
        {
            if (string.IsNullOrWhiteSpace(FilterText)) return true;

            if (obj is Employees employee)
            {
                return employee.FullName.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                       employee.Phone.Contains(FilterText) ||
                       employee.PassportData.Contains(FilterText);
            }
            return false;
        }

        private void AddEmployee()
        {
            var newEmployee = new Employees
            {
                FullName = CurrentEmployee.FullName,
                Age = CurrentEmployee.Age,
                Gender = CurrentEmployee.Gender,
                Address = CurrentEmployee.Address,
                Phone = CurrentEmployee.Phone,
                PassportData = CurrentEmployee.PassportData,
                PositionID = CurrentEmployee.PositionID
            };

            // В реальном приложении: сохранение в базу данных
            newEmployee.EmployeeID = Employees.Count > 0 ? Employees.Max(e => e.EmployeeID) + 1 : 1;
            Employees.Add(newEmployee);

            ClearForm();
        }

        private void UpdateEmployee()
        {
            if (SelectedEmployee != null)
            {
                SelectedEmployee.FullName = CurrentEmployee.FullName;
                SelectedEmployee.Age = CurrentEmployee.Age;
                SelectedEmployee.Gender = CurrentEmployee.Gender;
                SelectedEmployee.Address = CurrentEmployee.Address;
                SelectedEmployee.Phone = CurrentEmployee.Phone;
                SelectedEmployee.PassportData = CurrentEmployee.PassportData;
                SelectedEmployee.PositionID = CurrentEmployee.PositionID;

                // В реальном приложении: обновление в базе данных

                // Обновляем отображение
                var index = Employees.IndexOf(SelectedEmployee);
                Employees[index] = SelectedEmployee;
            }
        }

        private void DeleteEmployee()
        {
            if (SelectedEmployee != null)
            {
                // В реальном приложении: удаление из базы данных
                Employees.Remove(SelectedEmployee);
                ClearForm();
            }
        }

        private void ClearForm()
        {
            CurrentEmployee = new Employees();
            SelectedEmployee = null;
        }

        private void GenerateReport()
        {
            // Логика генерации отчета по сотрудникам
            // Можно реализовать через Microsoft Reporting или другой механизм отчетов
        }

        private bool CanAddEmployee()
        {
            return !string.IsNullOrWhiteSpace(CurrentEmployee.FullName) &&
                   !string.IsNullOrWhiteSpace(CurrentEmployee.PassportData) &&
                   CurrentEmployee.PositionID > 0;
        }

        private bool CanUpdateEmployee()
        {
            return SelectedEmployee != null && CanAddEmployee();
        }

        private bool CanDeleteEmployee()
        {
            return SelectedEmployee != null;
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