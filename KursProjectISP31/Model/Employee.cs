// Model/Employee.cs
namespace KursProjectISP31.Model
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PassportData { get; set; } = string.Empty;
        public int PositionID { get; set; }
    }
}