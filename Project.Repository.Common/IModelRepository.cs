using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Project.Model;

namespace Project.Repository.Common
{
    public interface IModelRepository 
    {
        Task<VehicleModel> GetByIdAsync(int id);
        Task<int> InsertAsync(VehicleModel entity);
        Task<int> UpdateAsync(VehicleModel entity);
        Task<int> DeleteAsync(VehicleModel entity);
        Task<IPagedList<VehicleModel>> GetPagedModel(IFilter filter);
    }
}
