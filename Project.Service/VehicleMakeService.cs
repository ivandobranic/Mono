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

        IUnitOfWork unitOfWork;
        public VehicleMakeService(IUnitOfWork _unitOfWork, IMakeRepository _makeRepository)
        {
            this.unitOfWork = _unitOfWork; 
        }


        public async Task<VehicleMake> GetByIdAsync(int id)
        {

            return await unitOfWork.MakeRepository.GetByIdAsync(id);
               
        }

        public async Task<int> CreateAsync(VehicleMake VehicleMake)
        {
            await unitOfWork.MakeRepository.InsertAsync(VehicleMake);
            return await unitOfWork.CommitAsync();
        }

        public async Task<int> UpdateAsync(VehicleMake VehicleMake)
        {
            
            await unitOfWork.MakeRepository.UpdateAsync(VehicleMake);
            return await unitOfWork.CommitAsync();
        }

        public async Task<int> DeleteAsync(VehicleMake VehicleMake)
        {
           await unitOfWork.MakeRepository.DeleteAsync(VehicleMake);
           return await unitOfWork.CommitAsync();
        }

        public async Task<IPagedList<VehicleMake>> PagedList(IFilter filter)
        {
            return await unitOfWork.MakeRepository.GetPagedMake(filter);
        }
    }
}
