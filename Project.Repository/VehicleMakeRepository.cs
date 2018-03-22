using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Project.DAL;
using Project.Model;
using Project.Repository.Common;

namespace Project.Repository
{
    public class VehicleMakeRepository : IMakeRepository
    {
        private readonly VehicleContext Context;
        private readonly IRepository<VehicleMake> Repository;
        public VehicleMakeRepository (VehicleContext context, IRepository<VehicleMake> repository)
        {
            this.Repository = repository;
            this.Context = context;
        }

        public async Task<VehicleMake> GetByIdAsync(int Id)
        {

            return await Context.Set<VehicleMake>().FindAsync(Id);
        }

        public Task<int> InsertAsync(VehicleMake entity)
        {
            
                if (entity == null)
                {
                    throw new ArgumentException("entity");
                }

                DbEntityEntry dbEntityEntry = Context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    Context.Set<VehicleMake>().Add(entity);
                }
                return Task.FromResult(1);

        }

        public Task<int> UpdateAsync(VehicleMake entity)
        {
            if (entity == null)
                {
                    throw new ArgumentException("entity");
                }

                DbEntityEntry dbEntityEntry = Context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    Context.Set<VehicleMake>().Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
                return Task.FromResult(1);
         
        }

        public Task<int> DeleteAsync(VehicleMake entity)
        {
            
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                DbEntityEntry dbEntityEntry = Context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    Context.Set<VehicleMake>().Attach(entity);
                    Context.Set<VehicleMake>().Remove(entity);
                }
                return Task.FromResult(1);
         
        }
        public async Task<IPagedList<VehicleMake>>GetPagedMake(IFilter filter)
        {
            var query = Context.Set<VehicleMake>().AsQueryable();
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
