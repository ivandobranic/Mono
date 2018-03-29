using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Project.Model;
using Project.Model.Common;
using Project.Repository.Common;

namespace Project.Service.Common
{
    public interface IVehicleMakeService
    {
        Task<IVehicleMake> GetByIdAsync(int id);
        Task<int> CreateAsync(IVehicleMake vehicleMake);
        Task<int> UpdateAsync (IVehicleMake vehicleMake);
        Task<int> DeleteAsync(int id);
        Task<IPagedList<IVehicleMake>> PagedList(IFilter filter);
    }
}
