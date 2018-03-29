using Project.Model.Common;

namespace Project.Model
{
    public class VehicleModel : IVehicleModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MakeId { get; set; }

        public string Abrv { get; set; }

        public virtual IVehicleMake VehicleMake { get; set; }
    }
}
