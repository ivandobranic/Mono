using System.Threading.Tasks;
using PagedList;
using Project.Model.Common;
using Project.Repository.Common;

namespace Project.Service.Common
{
    public interface IVehicleModelService
    {
        Task<IVehicleModel> GetByIdAsync(int id);
        Task<int> CreateAsync(IVehicleModel vehicleModel);
        Task<int> UpdateAsync(IVehicleModel vehicleModel);
        Task<int> DeleteAsync(int id);
        Task<IPagedList<IVehicleModel>> PagedList(IFilter filter);
    }
}
