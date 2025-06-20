using CarRentalSystem.Model;
using KursProjectISP31.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace CarRentalSystem.ViewModel
{
    public class BrandViewModel : BaseViewModel
    {
        private CarBrands _currentBrand = new CarBrands();
        private CarBrands _selectedBrand;
        private string _filterText;
        private ICollectionView _brandsView;

        public ObservableCollection<CarBrands> Brands { get; }

        public CarBrands CurrentBrand
        {
            get => _currentBrand;
            set
            {
                _currentBrand = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsBrandSelected));
            }
        }

        public CarBrands SelectedBrand
        {
            get => _selectedBrand;
            set
            {
                _selectedBrand = value;
                CurrentBrand = value != null ? new CarBrands
                {
                    BrandID = value.BrandID,
                    BrandName = value.BrandName,
                    TechnicalSpecs = value.TechnicalSpecs,
                    Description = value.Description
                } : new CarBrands();
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsBrandSelected));
            }
        }

        public bool IsBrandSelected => SelectedBrand != null;

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged();
                _brandsView?.Refresh();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand GenerateCatalogCommand { get; }

        public BrandViewModel()
        {
            Brands = new ObservableCollection<CarBrands>();
            InitializeCommands();
            LoadInitialData();
            SetupView();
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddBrand, CanAddBrand);
            SaveCommand = new RelayCommand(UpdateBrand, CanUpdateBrand);
            DeleteCommand = new RelayCommand(DeleteBrand, CanDeleteBrand);
            ClearCommand = new RelayCommand(ClearForm);
            GenerateCatalogCommand = new RelayCommand(GenerateCatalog);
        }

        private void LoadInitialData()
        {
            // Загрузка тестовых данных
            Brands.Add(new CarBrands
            {
                BrandID = 1,
                BrandName = "Toyota",
                TechnicalSpecs = "Двигатель: 2.0L, Мощность: 150 л.с., Расход: 7.5L/100km",
                Description = "Надежные автомобили японского производства"
            });

            Brands.Add(new CarBrands
            {
                BrandID = 2,
                BrandName = "BMW",
                TechnicalSpecs = "Двигатель: 3.0L Turbo, Мощность: 250 л.с., Расход: 9.2L/100km",
                Description = "Немецкие автомобили класса премиум"
            });

            Brands.Add(new CarBrands
            {
                BrandID = 3,
                BrandName = "Ford",
                TechnicalSpecs = "Двигатель: 2.5L, Мощность: 180 л.с., Расход: 8.1L/100km",
                Description = "Американские автомобили с хорошей ремонтопригодностью"
            });
        }

        private void SetupView()
        {
            _brandsView = CollectionViewSource.GetDefaultView(Brands);
            _brandsView.Filter = FilterBrands;
        }

        private bool FilterBrands(object obj)
        {
            if (string.IsNullOrWhiteSpace(FilterText)) return true;

            return obj is CarBrands brand &&
                   (brand.BrandName.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                    brand.Description.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                    brand.TechnicalSpecs.Contains(FilterText, StringComparison.OrdinalIgnoreCase));
        }

        private void AddBrand()
        {
            var newBrand = new CarBrands
            {
                BrandID = Brands.Count > 0 ? Brands.Max(b => b.BrandID) + 1 : 1,
                BrandName = CurrentBrand.BrandName,
                TechnicalSpecs = CurrentBrand.TechnicalSpecs,
                Description = CurrentBrand.Description
            };

            Brands.Add(newBrand);
            ClearForm();
        }

        private void UpdateBrand()
        {
            if (SelectedBrand == null) return;

            SelectedBrand.BrandName = CurrentBrand.BrandName;
            SelectedBrand.TechnicalSpecs = CurrentBrand.TechnicalSpecs;
            SelectedBrand.Description = CurrentBrand.Description;

            var index = Brands.IndexOf(SelectedBrand);
            Brands[index] = SelectedBrand;
        }

        private void DeleteBrand()
        {
            if (SelectedBrand != null)
            {
                Brands.Remove(SelectedBrand);
                ClearForm();
            }
        }

        private void ClearForm()
        {
            CurrentBrand = new CarBrands();
            SelectedBrand = null;
        }

        private void GenerateCatalog()
        {
            // Логика генерации каталога
        }

        private bool CanAddBrand() =>
            !string.IsNullOrWhiteSpace(CurrentBrand.BrandName) &&
            !string.IsNullOrWhiteSpace(CurrentBrand.TechnicalSpecs);

        private bool CanUpdateBrand() =>
            SelectedBrand != null && CanAddBrand();

        private bool CanDeleteBrand() =>
            SelectedBrand != null;

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