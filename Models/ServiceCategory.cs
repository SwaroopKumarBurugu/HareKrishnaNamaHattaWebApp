﻿using System.ComponentModel.DataAnnotations;

namespace HareKrishnaNamaHattaWebApp.Models
{
    public class ServiceCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; }

        public ICollection<VolunteerService> Services { get; set; }
    }
}
