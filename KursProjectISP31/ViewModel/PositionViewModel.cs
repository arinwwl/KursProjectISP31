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
    public class PositionViewModel : BaseViewModel
    {
        private Positions _currentPosition = new Positions();
        private Positions _selectedPosition;
        private string _filterText;
        private ICollectionView _positionsView;

        public ObservableCollection<Positions> Position { get; }

        public Positions CurrentPosition
        {
            get => _currentPosition;
            set
            {
                _currentPosition = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsPositionSelected));
            }
        }

        public Positions SelectedPosition
        {
            get => _selectedPosition;
            set
            {
                _selectedPosition = value;
                CurrentPosition = value != null ? new Positions
                {
                    PositionID = value.PositionID,
                    PositionName = value.PositionName,
                    Salary = value.Salary,
                    Responsibilities = value.Responsibilities,
                    Requirements = value.Requirements
                } : new Positions();
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsPositionSelected));
            }
        }

        public bool IsPositionSelected => SelectedPosition != null;

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged();
                _positionsView.Refresh();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand GenerateReportCommand { get; }

        public PositionViewModel()
        {
            Position = new ObservableCollection<Positions>();

            InitializeCommands();
            LoadInitialData();
            SetupView();
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddPosition, CanAddPosition);
            SaveCommand = new RelayCommand(UpdatePosition, CanUpdatePosition);
            DeleteCommand = new RelayCommand(DeletePosition, CanDeletePosition);
            ClearCommand = new RelayCommand(ClearForm);
            GenerateReportCommand = new RelayCommand(GenerateReport);
        }

        private void LoadInitialData()
        {
            // Загрузка тестовых данных (в реальном приложении - из базы)
            Positions.Add(new Positions
            {
                PositionID = 1,
                PositionName = "Менеджер по аренде",
                Salary = 50000,
                Responsibilities = "Оформление договоров аренды, работа с клиентами",
                Requirements = "Высшее образование, опыт работы от 1 года"
            });

            Positions.Add(new Positions
            {
                PositionID = 2,
                PositionName = "Автомеханик",
                Salary = 45000,
                Responsibilities = "Техническое обслуживание автомобилей, ремонт",
                Requirements = "Среднее специальное образование, опыт работы с автомобилями"
            });

            Positions.Add(new Positions
            {
                PositionID = 3,
                PositionName = "Администратор",
                Salary = 40000,
                Responsibilities = "Встреча клиентов, оформление документации",
                Requirements = "Опыт работы в сфере обслуживания, грамотная речь"
            });
        }

        private void SetupView()
        {
            _positionsView = CollectionViewSource.GetDefaultView(Positions);
            _positionsView.Filter = FilterPositions;
        }

        private bool FilterPositions(object obj)
        {
            if (string.IsNullOrWhiteSpace(FilterText)) return true;

            if (obj is Positions position)
            {
                return position.PositionName.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                       position.Responsibilities.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                       position.Salary.ToString().Contains(FilterText);
            }
            return false;
        }

        private void AddPosition()
        {
            var newPosition = new Positions          
            {
                PositionName = CurrentPosition.PositionName,
                Salary = CurrentPosition.Salary,
                Responsibilities = CurrentPosition.Responsibilities,
                Requirements = CurrentPosition.Requirements
            };

            newPosition.PositionID = Positions.Count > 0 ? Positions.Max(p => p.PositionID) + 1 : 1;
            Positions.Add(newPosition);

            ClearForm();
        }

        private void UpdatePosition()
        {
            if (SelectedPosition != null)
            {
                SelectedPosition.PositionName = CurrentPosition.PositionName;
                SelectedPosition.Salary = CurrentPosition.Salary;
                SelectedPosition.Responsibilities = CurrentPosition.Responsibilities;
                SelectedPosition.Requirements = CurrentPosition.Requirements;

                var index = Positions.IndexOf(SelectedPosition);
                Position[index] = SelectedPosition;
            }
        }

        private void DeletePosition()
        {
            if (SelectedPosition != null)
            {
                Position.Remove(SelectedPosition);
                ClearForm();
            }
        }

        private void ClearForm()
        {
            CurrentPosition = new Positions();
            SelectedPosition = null;
        }

        private void GenerateReport()
        {
            // Логика генерации отчета по должностям
            // Можно реализовать через Microsoft Reporting или другой механизм отчетов
        }

        private bool CanAddPosition()
        {
            return !string.IsNullOrWhiteSpace(CurrentPosition.PositionName) &&
                   CurrentPosition.Salary > 0;
        }

        private bool CanUpdatePosition()
        {
            return SelectedPosition != null && CanAddPosition();
        }

        private bool CanDeletePosition()
        {
            return SelectedPosition != null;
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