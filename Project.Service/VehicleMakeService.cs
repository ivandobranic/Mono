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

        IMakeRepository VehicleMakeRepository;
        public VehicleMakeService(IMakeRepository vehicleMakeRepository)
        {
            this.VehicleMakeRepository = vehicleMakeRepository;
        }


        public async Task<VehicleMake> GetByIdAsync(int id)
        {

            return await VehicleMakeRepository.GetByIdAsync(id);
               
        }

        public async Task<int> CreateAsync(VehicleMake VehicleMake)
        {
            return await VehicleMakeRepository.InsertAsync(VehicleMake);
         
        }

        public async Task<int> UpdateAsync(VehicleMake VehicleMake)
        {
            
          return await VehicleMakeRepository.UpdateAsync(VehicleMake);
            
        }

        public async Task<int> DeleteAsync(VehicleMake VehicleMake)
        {
           return await VehicleMakeRepository.DeleteAsync(VehicleMake);
           
        }

        public async Task<IPagedList<VehicleMake>> PagedList(IFilter filter)
        {
            return await VehicleMakeRepository.GetPagedMake(filter);
        }
    }
}
