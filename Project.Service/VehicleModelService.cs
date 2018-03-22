using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Repository.Common;
using Project.Service.Common;

namespace Project.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        IRepository<VehicleModel> modelRepository;
        public VehicleModelService(IRepository<VehicleModel> _modelRepository)
        {
            this.modelRepository = _modelRepository;
        }


        public async Task<VehicleModel> GetById(int id)
        {
            return await modelRepository.GetByIdAsync(id);
        }

        public async Task<int> Create(VehicleModel vehicleModel)
        {
            return await modelRepository.InsertAsync(vehicleModel);
        }

        public async Task<int> Update(VehicleModel vehicleModel)
        {
           return await modelRepository.UpdateAsync(vehicleModel);
        }

        public async Task<int> Delete(VehicleModel vehicleModel)
        {
           return await modelRepository.DeleteAsync(vehicleModel);
        }

        public async Task<int> GetVehicleModelCount(string search)
        {
            
            if (search != null)
            {
                return await modelRepository.Get().Where(x => x.Name == search).CountAsync();
            }
            else
            {
                return await modelRepository.Get().CountAsync();
            }

        }

        public async Task<IEnumerable<VehicleModel>> Sorting(string sortOrder)
        {
            var query = modelRepository.Get();
            query = sortOrder == "name_desc" ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<VehicleModel>> Filtering(string search)
        {
            return await modelRepository.Get().Where(x => x.Name.ToLower() == search.ToLower()).ToListAsync();
        }

        public async Task<List<VehicleModel>> PagedList(string sortOrder, string search, int pageNumber, int pageSize)
        {

            var query = modelRepository.Get();
            query = sortOrder == "name_desc" ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
            if (search != null)
            {
                return await query.Where(x => x.Name.ToLower() == search.ToLower()).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }
    }
}
