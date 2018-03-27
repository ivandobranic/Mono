using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using Project.Common.Caching;
using Project.Model;
using Project.Repository.Common;
using Project.Service.Common;

namespace Project.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {

        IUnitOfWork UnitOfWork;
        public VehicleMakeService(IUnitOfWork _UnitOfWork)
        {
            this.UnitOfWork = _UnitOfWork; 
        }


        public async Task<VehicleMake> GetByIdAsync(int id)
        {

            return await UnitOfWork.MakeRepository.GetByIdAsync(id);
               
        }

        public async Task<int> CreateAsync(VehicleMake VehicleMake)
        {
            await UnitOfWork.MakeRepository.InsertAsync(VehicleMake);
            return await UnitOfWork.CommitAsync();
        }

        public async Task<int> UpdateAsync(VehicleMake VehicleMake)
        {
            
            await UnitOfWork.MakeRepository.UpdateAsync(VehicleMake);
            return await UnitOfWork.CommitAsync();
        }

        public async Task<int> DeleteAsync(VehicleMake VehicleMake)
        {
           await UnitOfWork.MakeRepository.DeleteAsync(VehicleMake);
           return await UnitOfWork.CommitAsync();
        }

        public async Task<IPagedList<VehicleMake>> PagedList(IFilter filter)
        {
            return await UnitOfWork.MakeRepository.GetPagedMake(filter);
        }
    }
}
