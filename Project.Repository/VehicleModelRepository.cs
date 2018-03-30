using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using AutoMapper;
using Project.Repository.Common;
using Project.DAL.Entities;
using Project.Model.Common;
using System.Collections.Generic;

namespace Project.Repository
{
    public class VehicleModelRepository : IModelRepository
    {


        private readonly IRepository<VehicleModelEntity> Repository;
        public VehicleModelRepository(IRepository<VehicleModelEntity> repository)
        {
            this.Repository = repository;

        }

        public async Task<IVehicleModel> GetByIdAsync(int id)
        {
            var entity = await Repository.GetByIdAsync(id);
            return Mapper.Map<VehicleModelEntity, IVehicleModel>(entity);

        }

        public async Task<int> InsertAsync(IVehicleModel domainModel)
        {
            var entity = Mapper.Map<IVehicleModel, VehicleModelEntity>(domainModel);
            return await Repository.InsertAsync(entity);

        }

        public async Task<int> UpdateAsync(IVehicleModel domainModel)
        {
            var entity = Mapper.Map<IVehicleModel, VehicleModelEntity>(domainModel);
            return await Repository.UpdateAsync(entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await Repository.DeleteAsync(id);
        }
        public async Task<IPagedList<IVehicleModel>> GetPagedModel(IFilter filter)
        {
            var query = Repository.Get();

            query = filter.IsAscending == false ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
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
            var mappedQuery = Mapper.Map<IEnumerable<VehicleModelEntity>, IEnumerable<IVehicleModel>>(enumerableQuery);
            return new StaticPagedList<IVehicleModel>(mappedQuery, filter.PageNumber, filter.PageSize, filter.TotalCount);
        }
    }
}

