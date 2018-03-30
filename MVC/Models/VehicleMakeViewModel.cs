using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class VehicleMakeViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 15)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 15)]
        public string Abrv { get; set; }
    }
}