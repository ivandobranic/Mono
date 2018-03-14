using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Service.Common
{
    public interface IVehicleMakeService
    {
        IEnumerable<VehicleMake> GetAll();
        Task<VehicleMake> GetById(int? id);
        Task<VehicleMake> Create(VehicleMake VehicleMake);
        Task<int> Update (VehicleMake VehicleMake);
        Task<int> Delete(VehicleMake VehicleMake);
        Task<int> GetVehicleMakeCount(string search);
        Task<IEnumerable<VehicleMake>> Sorting(string sortOrder);
        Task<IEnumerable<VehicleMake>> Filtering(string search);
        Task<List<VehicleMake>> PagedList(string sortOrder, string search, int pageNumber, int pageSize);
    }
}
