using KursProjectISP31.Utills;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace KursProjectISP31.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public string SystemName => "CAR RENTAL SYSTEM";

        // Для примера - данные которые можно обновлять
        private int _availableCars = 24;
        public string AvailableCars => $"Доступно автомобилей: {_availableCars}";

        public void UpdateCarCount(int count)
        {
            _availableCars = count;
            OnPropertyChanged(nameof(AvailableCars));
        }
    }   
}