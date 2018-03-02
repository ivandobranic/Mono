using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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