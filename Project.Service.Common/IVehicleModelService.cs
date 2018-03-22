using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Service.Common
{
    public interface IVehicleModelService
    {
        Task<VehicleModel> GetById(int id);
        Task<int> Create(VehicleModel vehicleModel);
        Task<int> Update(VehicleModel vehicleModel);
        Task<int> Delete(VehicleModel vehicleModel);
        Task<int> GetVehicleModelCount(string search);
        Task<IEnumerable<VehicleModel>> Sorting(string sortOrder);
        Task<IEnumerable<VehicleModel>> Filtering(string search);
        Task<List<VehicleModel>> PagedList(string sortOrder, string search, int pageNumber, int pageSize);
    }
}
