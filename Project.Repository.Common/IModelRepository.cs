using System.Threading.Tasks;
using PagedList;
using Project.Model.Common;

namespace Project.Repository.Common
{
    public interface IModelRepository 
    {
        Task<IVehicleModel> GetByIdAsync(int id);
        Task<int> InsertAsync(IVehicleModel entity);
        Task<int> UpdateAsync(IVehicleModel entity);
        Task<int> DeleteAsync(int id);
        Task<IPagedList<IVehicleModel>> GetPagedModel(IFilter filter);
    }
}
