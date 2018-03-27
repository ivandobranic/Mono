using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using Project.Model;
using Project.Repository.Common;

namespace Project.Repository
{
    public class VehicleMakeRepository : IMakeRepository
    {
      
        private readonly IRepository<VehicleMake> Repository;
        public VehicleMakeRepository (IRepository<VehicleMake> _Repository)
        {
            this.Repository = _Repository;
           
        }

        public async Task<VehicleMake> GetByIdAsync(int Id)
        {

            return await Repository.GetByIdAsync(Id);
        }

        public Task<int> InsertAsync(VehicleMake entity)
        {

            return Repository.InsertAsync(entity);

        }

        public Task<int> UpdateAsync(VehicleMake entity)
        {
            return Repository.UpdateAsync(entity);
        }

        public Task<int> DeleteAsync(VehicleMake entity)
        {

            return Repository.DeleteAsync(entity);
        }
        public async Task<IPagedList<VehicleMake>>GetPagedMake(IFilter filter)
        {
            var query = Repository.Get();
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
          
            return new StaticPagedList<VehicleMake>(query, filter.PageNumber, filter.PageSize, filter.TotalCount);
        }
    }
}
