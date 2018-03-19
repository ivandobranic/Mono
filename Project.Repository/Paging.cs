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
   public class Paging : IPaging
    {
        private DbSet<VehicleMake> makeEntity;
        private readonly VehicleContext context;
        public Paging(VehicleContext _context)
        {
            this.context = _context;
            this.makeEntity = context.Set<VehicleMake>();
     
        }
        public async Task<StaticPagedList<VehicleMake>> GetPagedResultMake(string sortOrder, string search, int pageNumber, int pageSize)
        {
            var query = makeEntity.AsQueryable();
            int rowCount = 0;

            query = sortOrder == "name_desc" ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
            if (search != null)
            {
               rowCount = await query.Where(x => x.Name == search).CountAsync();
               query = query.Where(x => x.Name.ToLower() == search.ToLower()).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
            else
            {
              rowCount = await query.CountAsync();
              query =  query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
     
            StaticPagedList<VehicleMake> pagedMake = new StaticPagedList<VehicleMake>(query, pageNumber, pageSize, rowCount);
            return pagedMake;
         }   
        
    }
}
