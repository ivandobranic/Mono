using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Project.DAL;
using Project.Model;
using Project.Repository.Common;

namespace Project.Repository
{
    public class VehicleModelRepository : IModelRepository
    {

        private readonly VehicleContext context;
        private readonly IRepository<VehicleModel> repository;
        public VehicleModelRepository(VehicleContext _context, IRepository<VehicleModel> _repository)
        {
            this.repository = _repository;
            this.context = _context;
        }


        public async Task<IPagedList<VehicleModel>> GetPagedModel(IFilter filter)
        {
            var query = context.Set<VehicleModel>().AsQueryable();
            query = filter.IsAscending == false ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
            if (filter.Search != null)
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
