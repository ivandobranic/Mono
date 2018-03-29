using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using Project.DAL.Entities;
using AutoMapper;
using Project.Model;
using Project.Model.Common;
using Project.Repository.Common;

namespace Project.Repository
{
    public class VehicleMakeRepository : IMakeRepository
    {
      
        private readonly IRepository<VehicleMakeEntity> Repository;
        public VehicleMakeRepository (IRepository<VehicleMakeEntity> repository)
        {
            this.Repository = repository;
           
        }

        public async Task<IVehicleMake> GetByIdAsync(int id)
        {
            var entity = await Repository.GetByIdAsync(id);
            return Mapper.Map<VehicleMakeEntity, IVehicleMake>(entity);
           
        }

        public async Task<int> InsertAsync(IVehicleMake domainModel)
        {
            var entity = Mapper.Map<IVehicleMake, VehicleMakeEntity>(domainModel);
            return await Repository.InsertAsync(entity);

        }

        public async Task<int> UpdateAsync(IVehicleMake domainModel)
        {
            var entity = Mapper.Map<IVehicleMake, VehicleMakeEntity>(domainModel);
            return await Repository.UpdateAsync(entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await Repository.DeleteAsync(id);
        }
        public async Task<IPagedList<IVehicleMake>>GetPagedMake(IFilter filter)
        {
            var query = Repository.Get();
        
            Mapper.AssertConfigurationIsValid();
            query = filter.IsAscending ==  false ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
            if (!string.IsNullOrEmpty(filter.Search))
            {
                filter.TotalCount = await query.Where(x => x.Name == filter.Search).CountAsync();
                query = query.Where(x => x.Name == filter.Search).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            else
            {
                filter.TotalCount = await query.CountAsync();
                query = query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            var enumerableQuery = query.AsEnumerable();
            var mappedQuery = Mapper.Map<IEnumerable<VehicleMakeEntity>, IEnumerable<IVehicleMake>>(enumerableQuery);
            return new StaticPagedList<IVehicleMake>(mappedQuery, filter.PageNumber, filter.PageSize, filter.TotalCount);
        }
    }
}
