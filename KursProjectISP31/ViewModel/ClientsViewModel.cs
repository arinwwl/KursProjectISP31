using KursProjectISP31.Model;
using KursProjectISP31.Utills;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjectISP31.ViewModel
{
    public class ClientsViewModel : ViewModelBase
    {
        private ObservableCollection<Client> _clients = new();

        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }
        public ClientsViewModel()
        {

        }

        
    }
}
