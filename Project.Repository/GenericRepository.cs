using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using Project.Common.Logging;
using Project.DAL;
using Project.Repository.Common;

namespace Project.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
     where TEntity : class
    {

        protected VehicleContext context;
        private DbSet<TEntity> entities;
        public GenericRepository(VehicleContext _context)
        {
            this.context = _context;
            entities = context.Set<TEntity>();

        }

        public IQueryable<TEntity> Get()
        {
            return entities;
        }

        public async Task<TEntity> GetByIdAsync(int Id)
        {

            return await entities.FindAsync(Id);

        }

        public Task<int> InsertAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentException("entity");
                }

                DbEntityEntry dbEntityEntry = context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    entities.Add(entity);
                }
                return Task.FromResult(1);

            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);
                throw fail;

            }

        }

        public Task<int> UpdateAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentException("entity");
                }
              
                DbEntityEntry dbEntityEntry = context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    entities.Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
                return Task.FromResult(1);
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public Task<int> DeleteAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                DbEntityEntry dbEntityEntry = context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    entities.Attach(entity);
                    entities.Remove(entity);
                }
                return Task.FromResult(1);
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

         
    }
}
