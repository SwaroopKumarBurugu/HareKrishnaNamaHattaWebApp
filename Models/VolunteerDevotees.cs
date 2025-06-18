// ViewModels/VolunteerService.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HareKrishnaNamaHattaWebApp.Models
{
    public class VolunteerDevotees
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Preferred Date")]
        public DateTime PreferredDate { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public string AdditionalNotes { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime SubmittedOn { get; set; } = DateTime.Now;
    }
}
