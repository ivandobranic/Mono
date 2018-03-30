using System.Threading.Tasks;
using PagedList;
using Project.Model.Common;
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


        public async Task<IVehicleModel> GetByIdAsync(int id)
        {

            return await VehicleModelRepository.GetByIdAsync(id);

        }

        public async Task<int> CreateAsync(IVehicleModel vehicleMake)
        {
            return await VehicleModelRepository.InsertAsync(vehicleMake);

        }

        public async Task<int> UpdateAsync(IVehicleModel vehicleMake)
        {

            return await VehicleModelRepository.UpdateAsync(vehicleMake);

        }

        public async Task<int> DeleteAsync(int id)
        {
            return await VehicleModelRepository.DeleteAsync(id);

        }

        public async Task<IPagedList<IVehicleModel>> PagedList(IFilter filter)
        {
            return await VehicleModelRepository.GetPagedModel(filter);
        }
    }
}
