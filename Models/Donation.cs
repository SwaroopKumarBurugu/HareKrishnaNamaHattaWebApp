using System;
using System.ComponentModel.DataAnnotations;

namespace HareKrishnaNamaHattaWebApp.Models
{
    public class Donation
    {
        [Key] // Ensure this line is present
        public int Id { get; set; }

        [Required]
        public string DonorName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal Amount { get; set; }

        public string Message { get; set; }

        public DateTime DonationDate { get; set; } = DateTime.UtcNow;
        // ← new flag
        public bool ReceiptSent { get; set; } = false;

    }
}
