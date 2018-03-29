using System.Collections.Generic;

namespace Project.Model.Common
{
    public interface IVehicleMake
    {
        int Id { get; set; }

        string Name { get; set; }

        string Abrv { get; set; }
        ICollection<IVehicleModel> VehicleModel { get; set; }
    }
}

