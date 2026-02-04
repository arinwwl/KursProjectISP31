// Model/Client.cs
namespace KursProjectISP31.Model
{
    public class Client
    {
        public int ClientID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; } 
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PassportData { get; set; } = string.Empty;
    }
}