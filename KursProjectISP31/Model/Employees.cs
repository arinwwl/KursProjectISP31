using KursProjectISP31.Utills;
using System;
using System.ComponentModel;

namespace KursProjectISP31.Model
{
    public class Employees : ViewModelBase
    {
        private int employeeID;
        public int EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; OnPropertyChanged(nameof(EmployeeID)); }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; OnPropertyChanged(nameof(FullName)); }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; OnPropertyChanged(nameof(Age)); }
        }

        private string gender;
        public string Gender
        {
            get { return gender; }
            set { gender = value; OnPropertyChanged(nameof(Gender)); }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(nameof(Address)); }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged(nameof(Phone)); }
        }

        private string passportData;
        public string PassportData
        {
            get { return passportData; }
            set { passportData = value; OnPropertyChanged(nameof(PassportData)); }
        }

        private int positionID;
        public int PositionID
        {
            get { return positionID; }
            set { positionID = value; OnPropertyChanged(nameof(PositionID)); }
        }
    }
}