// Model/RentalDraft.cs
namespace KursProjectISP31.Model
{
    public class RentalDraft
    {
        public Client? SelectedClient { get; set; }
        public Car? SelectedCar { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Добавляем Summary — но защищённый от null
        public string Summary
        {
            get
            {
                var client = SelectedClient?.FullName ?? "—";
                var car = SelectedCar?.RegistrationNumber ?? "—";
                var issue = IssueDate?.ToString("dd.MM.yyyy") ?? "—";
                var ret = ReturnDate?.ToString("dd.MM.yyyy") ?? "—";
                return $"{client} | {car} | {issue} → {ret}";
            }
        }
    }
}