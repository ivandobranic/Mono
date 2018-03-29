using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using Project.Common.Caching;
using Project.Model;
using Project.Model.Common;
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


        public async Task<IVehicleMake> GetByIdAsync(int id)
        {

            return await VehicleMakeRepository.GetByIdAsync(id);
               
        }

        public async Task<int> CreateAsync(IVehicleMake vehicleMake)
        {
            return await VehicleMakeRepository.InsertAsync(vehicleMake);
         
        }

        public async Task<int> UpdateAsync(IVehicleMake vehicleMake)
        {
            
          return await VehicleMakeRepository.UpdateAsync(vehicleMake);
            
        }

        public async Task<int> DeleteAsync(int id)
        {
           return await VehicleMakeRepository.DeleteAsync(id);
           
        }

        public async Task<IPagedList<IVehicleMake>> PagedList(IFilter filter)
        {
            return await VehicleMakeRepository.GetPagedMake(filter);
        }
    }
}
