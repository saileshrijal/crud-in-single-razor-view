using System.ComponentModel.DataAnnotations;
using Models;

namespace ViewModels
{
    public class TransactionVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Account Number")]
        public string? AccountNumber { get; set; }

        [Required]
        [Display(Name = "Beneficiary Name")]
        public string? BeneficiaryName { get; set; }

        [Required]
        [Display(Name = "Bank Name")]
        public string? BankName { get; set; }

        [Required]
        [Display(Name = "SWIFT Code")]
        public string? SWIFTCode { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public List<TransactionModel>? Transactions { get; set; }
    }
}