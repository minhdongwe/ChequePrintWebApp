using System.ComponentModel.DataAnnotations;

namespace ChequePrintWebApp.Models
{
    public class ChequeViewModel
    {
        [Required(ErrorMessage ="Personal Number is Required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage ="Personal Number Must Be Exactly 6 Digits")]
        public string PersonalNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string BankAccountNumber { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage ="Amount must be greater than 0")]
        public decimal Amount { get; set; }
        [Required]
        public string AmountInWords { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;  // DateTime instead of string
    }
}
