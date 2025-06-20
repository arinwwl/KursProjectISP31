using KursProjectISP31.Utills;
using System;
using System.ComponentModel;

namespace KursProjectISP31.Model
{
    public class Positions : ViewModelBase
    {
        private int positionID;
        public int PositionID
        {
            get { return positionID; }
            set { positionID = value; OnPropertyChanged(nameof(PositionID)); }
        }

        private string positionName;
        public string PositionName
        {
            get { return positionName; }
            set { positionName = value; OnPropertyChanged(nameof(PositionName)); }
        }

        private decimal salary;
        public decimal Salary
        {
            get { return salary; }
            set { salary = value; OnPropertyChanged(nameof(Salary)); }
        }

        private string responsibilities;
        public string Responsibilities
        {
            get { return responsibilities; }
            set { responsibilities = value; OnPropertyChanged(nameof(Responsibilities)); }
        }

        private string requirements;
        public string Requirements
        {
            get { return requirements; }
            set { requirements = value; OnPropertyChanged(nameof(Requirements)); }
        }
    }
}