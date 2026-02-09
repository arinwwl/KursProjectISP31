// ViewModel/CarsViewModel.cs
using KursProjectISP31.Model;
using KursProjectISP31.Utills;
using System.Collections.ObjectModel;

namespace KursProjectISP31.ViewModel
{
    public class CarsViewModel : ViewModelBase
    {
        private ObservableCollection<Car> _cars = new();

        public ObservableCollection<Car> Cars
        {
            get => _cars;
            set
            {
                _cars = value;
                OnPropertyChanged();
            }
        }

        public CarsViewModel()
        {
            // Пока пусто — данные будут загружены из БД позже
        }
    }
}