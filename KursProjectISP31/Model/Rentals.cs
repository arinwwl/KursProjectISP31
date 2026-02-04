using KursProjectISP31.Utills;
using System;
using System.ComponentModel;

namespace KursProjectISP31.Model
{
    public class Rentals : ViewModelBase
    {
        private int rentalID;
        public int RentalID
        {
            get => rentalID;
            set { rentalID = value; OnPropertyChanged(nameof(RentalID)); }
        }

        private DateTime issueDate;
        public DateTime IssueDate
        {
            get => issueDate;
            set { issueDate = value; OnPropertyChanged(nameof(IssueDate)); }
        }

        private int rentalPeriod;
        public int RentalPeriod
        {
            get => rentalPeriod;
            set { rentalPeriod = value; OnPropertyChanged(nameof(RentalPeriod)); }
        }

        private DateTime returnDate;
        public DateTime ReturnDate
        {
            get => returnDate;
            set { returnDate = value; OnPropertyChanged(nameof(ReturnDate)); }
        }

        private int carID;
        public int CarID
        {
            get => carID;
            set { carID = value; OnPropertyChanged(nameof(CarID)); }
        }

        private int clientID;
        public int ClientID
        {
            get => clientID;
            set { clientID = value; OnPropertyChanged(nameof(ClientID)); }
        }

        private string? services;
        public string? Services
        {
            get => services;
            set { Services = value; OnPropertyChanged(nameof(Services)); }
        }

        private decimal rentalPrice;
        public decimal RentalPrice
        {
            get => rentalPrice;
            set { rentalPrice = value; OnPropertyChanged(nameof(RentalPrice)); }
        }

        private bool isPaid;
        public bool IsPaid
        {
            get => isPaid;
            set { isPaid = value; OnPropertyChanged(nameof(IsPaid)); }
        }

        private int employeeID;
        public int EmployeeID
        {
            get => employeeID;
            set { employeeID = value; OnPropertyChanged(nameof(EmployeeID)); }
        }
    }
}