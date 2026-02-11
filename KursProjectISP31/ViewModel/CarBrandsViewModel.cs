
using KursProjectISP31.Model;
using KursProjectISP31.Utills;
using System.Collections.ObjectModel;

namespace KursProjectISP31.ViewModel
{
    public class CarBrandsViewModel : ViewModelBase
    {
        private ObservableCollection<CarBrand> _brands = new();

        public ObservableCollection<CarBrand> Brands
        {
            get => _brands;
            set
            {
                _brands = value;
                OnPropertyChanged();
            }
        }

        public CarBrandsViewModel()
        {
            // Данные будут загружены позже из сервиса
        }
    }
}