using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Project.Model;

namespace Project.Service.Common
{
    public interface IVehicleMakeService
    {
        Task<VehicleMake> GetByIdAsync(int id);
        Task<int> CreateAsync(VehicleMake VehicleMake);
        Task<int> UpdateAsync (VehicleMake VehicleMake);
        Task<int> DeleteAsync(VehicleMake VehicleMake);
        Task<StaticPagedList<VehicleMake>> PagedList(string sortOrder, string search, int pageNumber, int pageSize);
    }
}
