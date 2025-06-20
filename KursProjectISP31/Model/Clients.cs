using KursProjectISP31.Utills;
using System;
using System.ComponentModel;

namespace KursProjectISP31.Model
{
    public class Clients : ViewModelBase
    {
        private int clientID;
        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; OnPropertyChanged(nameof(ClientID)); }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; OnPropertyChanged(nameof(FullName)); }
        }

        private string gender;
        public string Gender
        {
            get { return gender; }
            set { gender = value; OnPropertyChanged(nameof(Gender)); }
        }

        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; OnPropertyChanged(nameof(BirthDate)); }
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
    }
}