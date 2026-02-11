// ViewModel/RentalsViewModel.cs
using KursProjectISP31.Model;
using KursProjectISP31.Utills;
using System.Collections.ObjectModel;

namespace KursProjectISP31.ViewModel
{
    public class RentalsViewModel : ViewModelBase
    {
        private ObservableCollection<Rental> _rentals = new();

        public ObservableCollection<Rental> Rentals
        {
            get => _rentals;
            set
            {
                _rentals = value;
                OnPropertyChanged();
            }
        }

        public RentalsViewModel()
        {
            // Данные загрузятся позже через сервис
        }
    }
}