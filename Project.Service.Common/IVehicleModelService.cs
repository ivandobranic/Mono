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

        IEnumerable<VehicleModel> GetAll();
        Task<VehicleModel> GetById(int? id);
        Task<VehicleModel> Create(VehicleModel vehicleModel);
        Task<int> Update(VehicleModel vehicleModel);
        Task<int> Delete(VehicleModel vehicleModel);
        int GetVehicleModelCount(string search);
        Task<IEnumerable<VehicleModel>> Sorting(string sortOrder);
        Task<IEnumerable<VehicleModel>> Filtering(string search);
        Task<List<VehicleModel>> PagedList(string sortOrder, string search, int pageNumber, int pageSize);
    }
}
