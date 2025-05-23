using System.ComponentModel.DataAnnotations;

namespace ChequePrintWebApp.Models
{
    public class PFViewModel
    {
        [Required]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Enter 6-digit Personal Number")]
        public string PersonalNumber { get; set; }

        public string EmployeeName { get; set; }
        public decimal PFMemberContribution { get; set; }
        public decimal PFEmployerContribution { get; set; }

        public decimal Total => PFMemberContribution + PFEmployerContribution;

        [Range(0, double.MaxValue, ErrorMessage = "Enter valid amount")]
        public decimal PayrollDeduction { get; set; }

        public decimal GrandTotal => Total - PayrollDeduction;
    }

}
