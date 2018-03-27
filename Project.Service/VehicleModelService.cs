using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Project.Model;
using Project.Repository.Common;
using Project.Service.Common;

namespace Project.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        IUnitOfWork UnitOfWork;
        public VehicleModelService(IUnitOfWork _UnitOfWork)
        {
            this.UnitOfWork = _UnitOfWork;
        }


        public async Task<VehicleModel> GetById(int id)
        {
            return await UnitOfWork.ModelRepository.GetByIdAsync(id);
        }

        public async Task<int> Create(VehicleModel vehicleModel)
        {
            await UnitOfWork.ModelRepository.InsertAsync(vehicleModel);
            return await UnitOfWork.CommitAsync();
        }

        public async Task<int> Update(VehicleModel vehicleModel)
        {
           await UnitOfWork.ModelRepository.UpdateAsync(vehicleModel);
           return await UnitOfWork.CommitAsync();
        }

        public async Task<int> Delete(VehicleModel vehicleModel)
        {
           await UnitOfWork.ModelRepository.DeleteAsync(vehicleModel);
           return await UnitOfWork.CommitAsync();
        }



        public async Task<IPagedList<VehicleModel>> PagedList(IFilter filter)
        {
            return await UnitOfWork.ModelRepository.GetPagedModel(filter);
        }
    }
}
