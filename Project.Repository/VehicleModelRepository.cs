using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using Project.Model;
using Project.Repository.Common;

namespace Project.Repository
{
    public class VehicleModelRepository : IModelRepository
    {

       
        private readonly IRepository<VehicleModel> Repository;
        public VehicleModelRepository(IRepository<VehicleModel> _Repository)
        {
            this.Repository =_Repository;
            
        }

        public async Task<VehicleModel> GetByIdAsync(int Id)
        {

            return await Repository.GetByIdAsync(Id);
        }

        public Task<int> InsertAsync(VehicleModel entity)
        {

            return Repository.InsertAsync(entity);

        }

        public Task<int> UpdateAsync(VehicleModel entity)
        {
            return Repository.UpdateAsync(entity);
        }

        public Task<int> DeleteAsync(int id)
        {

            return Repository.DeleteAsync(id);
        }

        public async Task<IPagedList<VehicleModel>> GetPagedModel(IFilter filter)
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

            return new StaticPagedList<VehicleModel>(query, filter.PageNumber, filter.PageSize, filter.TotalCount);
        }
    }
}
