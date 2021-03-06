﻿using System.Threading.Tasks;
using PagedList;
using Project.Model.Common;

namespace Project.Repository.Common
{
    public interface IMakeRepository
    {
        Task<IVehicleMake> GetByIdAsync(int id);
        Task<int> InsertAsync(IVehicleMake domainModel);
        Task<int> UpdateAsync(IVehicleMake domainModel);
        Task<int> DeleteAsync(int id);
        Task<IPagedList<IVehicleMake>> GetPagedMake(IFilter filter);
    }
}
