using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Project.Model;
using Project.Repository.Common;

namespace Project.Service.Common
{
    public interface IVehicleModelService
    {
        Task<VehicleModel> GetById(int id);
        Task<int> Create(VehicleModel vehicleModel);
        Task<int> Update(VehicleModel vehicleModel);
        Task<int> Delete(int id);
        Task<IPagedList<VehicleModel>> PagedList(IFilter filter);
    }
}
