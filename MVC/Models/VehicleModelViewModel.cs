using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
