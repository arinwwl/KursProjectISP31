// Model/Rental.cs
namespace KursProjectISP31.Model
{
    public class Rental
    {
        public int RentalID { get; set; }
        public DateTime IssueDate { get; set; }
        public int RentalPeriod { get; set; } 
        public DateTime? ReturnDate { get; set; }
        public int CarID { get; set; }
        public int ClientID { get; set; }
        public string Services { get; set; } = string.Empty;
        public decimal RentalPrice { get; set; }
        public bool IsPaid { get; set; }
        public int EmployeeID { get; set; }
    }
}