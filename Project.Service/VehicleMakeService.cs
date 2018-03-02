using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Project.Common.Caching;
using Project.Model;
using Project.Repository.Common;
using Project.Service.Common;

namespace Project.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {

        IRepository<VehicleMake> makeRepository;
       
        private readonly string[] MasterCacheKeyArray = { "VehicleMakeCache" };
        public VehicleMakeService(IRepository<VehicleMake> _makeRepository)
        {
            this.makeRepository = _makeRepository;
          
        }

        public IEnumerable<VehicleMake> GetAll()
        {
          
            return makeRepository.Get();
        }

        public async Task<VehicleMake> GetById(int? id)
        {

            return await makeRepository.GetByIdAsync(id);
               
        }

        public async Task<VehicleMake> Create(VehicleMake VehicleMake)
        {
            
            return await makeRepository.InsertAsync(VehicleMake);
        }

        public async Task<int> Update(VehicleMake VehicleMake)
        {
            
            return await makeRepository.UpdateAsync(VehicleMake);
        }

        public async Task<int> Delete(VehicleMake VehicleMake)
        {
           return await makeRepository.DeleteAsync(VehicleMake);
        }

        public int GetVehicleMakeCount(string search)
        {

            int rowCount = 0;
            if (search != null)
            {
                return rowCount = makeRepository.Get().Where(x => x.Name.ToLower() == search.ToLower()).Count();
            }
            else
            {
                return rowCount = makeRepository.Get().Count();
            }

        }

        public async Task<IEnumerable<VehicleMake>> Sorting(string sortOrder)
        {   
            var query = makeRepository.Get();
            query = sortOrder == "name_desc" ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<VehicleMake>> Filtering(string search)
        {
            return await makeRepository.Get().Where(x => x.Name.ToLower() == search.ToLower()).ToListAsync();
        }

        public async Task<List<VehicleMake>> PagedList(string sortOrder, string search, int pageNumber, int pageSize)
        {
           
            var query = makeRepository.Get();
           
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
