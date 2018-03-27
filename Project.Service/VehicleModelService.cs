using System.Threading.Tasks;
using PagedList;
using Project.Model;
using Project.Repository.Common;
using Project.Service.Common;

namespace Project.Service
{
    public class VehicleModelService : IVehicleModelService
    {
       IModelRepository VehicleModelRepository;
        public VehicleModelService(IModelRepository vehicleModelRepository)
        {
            this.VehicleModelRepository = vehicleModelRepository;
        }


        public async Task<VehicleModel> GetById(int id)
        {
            return await VehicleModelRepository.GetByIdAsync(id);
        }

        public async Task<int> Create(VehicleModel vehicleModel)
        {
            return await VehicleModelRepository.InsertAsync(vehicleModel);
        }

        public async Task<int> Update(VehicleModel vehicleModel)
        {
           return await VehicleModelRepository.UpdateAsync(vehicleModel);
        }

        public async Task<int> Delete(VehicleModel vehicleModel)
        {
           return await VehicleModelRepository.DeleteAsync(vehicleModel);
        }


        public async Task<IPagedList<VehicleModel>> PagedList(IFilter filter)
        {
            return await VehicleModelRepository.GetPagedModel(filter);
        }
    }
}
