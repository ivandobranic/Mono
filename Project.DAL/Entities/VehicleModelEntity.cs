using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DAL.Entities
{
    [Table("VehicleModel")]
    public class VehicleModelEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MakeId { get; set; }

        public string Abrv { get; set; }

        public virtual VehicleMakeEntity VehicleMakeEntity { get; set; }
    }
}
