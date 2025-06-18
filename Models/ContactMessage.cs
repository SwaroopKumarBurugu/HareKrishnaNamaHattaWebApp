using System.ComponentModel.DataAnnotations;

namespace HareKrishnaNamaHattaWebApp.Models
{
    public class ContactMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime DateSent { get; set; } = DateTime.Now;
        public string? AdminReply { get; set; }
    }
}