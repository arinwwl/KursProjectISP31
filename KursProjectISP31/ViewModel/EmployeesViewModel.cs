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
    public class EmployeesViewModel : ViewModelBase
    {
        private ObservableCollection<Employee> _employees = new();

        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }

       
        public EmployeesViewModel()
        {
            
        }
    }
}
