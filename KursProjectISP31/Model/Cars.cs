using KursProjectISP31.Utills;
using System;
using System.ComponentModel;

namespace KursProjectISP31.Model
{
    public class Cars : ViewModelBase
    {
        private int carID;
        public int CarID
        {
            get { return carID; }
            set { carID = value; OnPropertyChanged(nameof(CarID)); }
        }

        private int brandID;
        public int BrandID
        {
            get { return brandID; }
            set { brandID = value; OnPropertyChanged(nameof(BrandID)); }
        }

        private string registrationNumber;
        public string RegistrationNumber
        {
            get { return registrationNumber; }
            set { registrationNumber = value; OnPropertyChanged(nameof(RegistrationNumber)); }
        }

        private string vin;
        public string VIN
        {
            get { return vin; }
            set { vin = value; OnPropertyChanged(nameof(VIN)); }
        }

        private string engineNumber;
        public string EngineNumber
        {
            get { return engineNumber; }
            set { engineNumber = value; OnPropertyChanged(nameof(EngineNumber)); }
        }

        private int manufactureYear;
        public int ManufactureYear
        {
            get { return manufactureYear; }
            set { manufactureYear = value; OnPropertyChanged(nameof(ManufactureYear)); }
        }

        private int mileage;
        public int Mileage
        {
            get { return mileage; }
            set { mileage = value; OnPropertyChanged(nameof(Mileage)); }
        }

        private decimal carPrice;
        public decimal CarPrice
        {
            get { return carPrice; }
            set { carPrice = value; OnPropertyChanged(nameof(CarPrice)); }
        }

        private decimal dailyRentalPrice;
        public decimal DailyRentalPrice
        {
            get { return dailyRentalPrice; }
            set { dailyRentalPrice = value; OnPropertyChanged(nameof(DailyRentalPrice)); }
        }

        private DateTime lastInspectionDate;
        public DateTime LastInspectionDate
        {
            get { return lastInspectionDate; }
            set { lastInspectionDate = value; OnPropertyChanged(nameof(LastInspectionDate)); }
        }

        private int employedD;
        public int EmployedD
        {
            get { return employedD; }
            set { employedD = value; OnPropertyChanged(nameof(EmployedD)); }
        }

        private string specialNotes;
        public string SpecialNotes
        {
            get { return specialNotes; }
            set { specialNotes = value; OnPropertyChanged(nameof(SpecialNotes)); }
        }

        private bool isReturned;
        public bool IsReturned
        {
            get { return isReturned; }
            set { isReturned = value; OnPropertyChanged(nameof(IsReturned)); }
        }
    }
}