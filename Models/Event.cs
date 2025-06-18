using System.ComponentModel.DataAnnotations;

namespace HareKrishnaNamaHattaWebApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Location { get; set; }
        public string? ImagePath { get; set; }
        public string? EventType { get; set; } = "Special";

    }
}