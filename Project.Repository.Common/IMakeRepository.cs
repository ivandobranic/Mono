using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Project.Model;

namespace Project.Repository.Common
{
    public interface IMakeRepository
    {
        Task<VehicleMake> GetByIdAsync(int id);
        Task<int> InsertAsync(VehicleMake entity);
        Task<int> UpdateAsync(VehicleMake entity);
        Task<int> DeleteAsync(VehicleMake entity);
        Task<IPagedList<VehicleMake>> GetPagedMake(IFilter filter);
    }
}
