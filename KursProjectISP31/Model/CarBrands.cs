using KursProjectISP31.Utills;
using System;
using System.ComponentModel;

namespace KursProjectISP31.Model
{
    public class CarBrands : ViewModelBase
    {
        private int brandID;
        public int BrandID
        {
            get { return brandID; }
            set { brandID = value; OnPropertyChanged(nameof(BrandID)); }
        }

        private string brandName;
        public string BrandName
        {
            get { return brandName; }
            set { brandName = value; OnPropertyChanged(nameof(BrandName)); }
        }

        private string technicalSpecs;
        public string TechnicalSpecs
        {
            get { return technicalSpecs; }
            set { technicalSpecs = value; OnPropertyChanged(nameof(TechnicalSpecs)); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }
    }
}