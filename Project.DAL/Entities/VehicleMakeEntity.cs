using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Entities
{
    [Table("VehicleMake")]
   public class VehicleMakeEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public virtual ICollection<VehicleModelEntity> VehicleModelEntity { get; set; }
    }
}
