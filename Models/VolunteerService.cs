using HareKrishnaNamaHattaWebApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HareKrishnaNamaHattaWebApp.Models
{
    public class VolunteerService
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Service name is required")]
        public string Name { get; set; }

        [Required]
        public int ServiceCategoryId { get; set; }

        [ForeignKey(nameof(ServiceCategoryId))]
        public ServiceCategory Category { get; set; }

        public ICollection<VolunteerDevotees> VolunteerDevotees { get; set; }
    }
}