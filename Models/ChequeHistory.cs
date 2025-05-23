namespace ChequePrintWebApp.Models
{
    public class ChequeHistory
    {
        public int Id { get; set; }
        public string PersonalNumber { get; set; }
        public string Name { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string AmountInWords { get; set; }
        public DateTime Date { get; set; }
    }

}
