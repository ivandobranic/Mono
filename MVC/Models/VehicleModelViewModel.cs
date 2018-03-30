using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class VehicleModelViewModel
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        [Required]
        [StringLength(maximumLength: 20)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 20)]
        public string Abrv { get; set; }
    }
}
