using HareKrishnaNamaHattaWebApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HareKrishnaNamaHattaWebApp.Models
{
    public class VolunteerDevotees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Service Type")]
        public int VolunteerServiceId { get; set; }

        [ValidateNever]
        [ForeignKey("VolunteerServiceId")]
        public VolunteerService VolunteerService { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Preferred Date")]
        public DateTime? PreferredDate { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Preferred Time")]
        public TimeSpan? PreferredTime { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        public string AdditionalNotes { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime SubmittedOn { get; set; } = DateTime.Now;
    }

}