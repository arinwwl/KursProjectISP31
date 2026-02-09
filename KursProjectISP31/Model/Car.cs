// Model/Car.cs
namespace KursProjectISP31.Model
{
    public class Car
    {
        public int CarID { get; set; }
        public int BrandID { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public string VIN { get; set; } = string.Empty;
        public string EngineNumber { get; set; } = string.Empty;
        public string ManufactureYear { get; set; } = string.Empty; // ← STRING!
        public string Mileage { get; set; } = string.Empty;         // ← STRING!
        public decimal CarPrice { get; set; }
        public decimal DailyRentalPrice { get; set; }
        public DateTime? LastInspectionDate { get; set; }           // ← Nullable!
        public int EmployeeID { get; set; }                        // ← ПРАВИЛЬНОЕ ИМЯ!
        public string SpecialNotes { get; set; } = string.Empty;
        public bool IsReturned { get; set; }
    }
}