using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using KursProjectISP31.Utills;

namespace KursProjectISP31.Model
{
    public class AdditionalServices : ViewModelBase
    {
        private int serviceID;
        public int ServiceID
        {
            get { return serviceID; }
            set { serviceID = value; OnPropertyChanged(nameof(ServiceID)); }
        }

        private string serviceName;
        public string ServiceName
        {
            get { return serviceName; }
            set { serviceName = value; OnPropertyChanged(nameof(ServiceName)); }
        }

        private decimal servicePrice;
        public decimal ServicePrice
        {
            get { return servicePrice; }
            set { servicePrice = value; OnPropertyChanged(nameof(ServicePrice)); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }
    }
}