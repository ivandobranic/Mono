using System;
using System.Data.Entity;
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

        private readonly VehicleContext context;
        private DbSet<TEntity> entities;
        public GenericRepository(VehicleContext _context)
        {
            this.context = _context;
            entities = context.Set<TEntity>();

        }

        public virtual void SetEntityStateModified(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void SetEntityStateDeleted(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public IQueryable<TEntity> Get()
        {
            return entities;
        }

        public async Task<TEntity> GetByIdAsync(int Id)
        {

            return await entities.FindAsync(Id);

        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentException("entity");
                }
                entities.Add(entity);
                await context.SaveChangesAsync();
                return entity;
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

        public async Task<int> UpdateAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentException("entity");
                }
                SetEntityStateModified(entity);
                return await context.SaveChangesAsync();
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

        public async Task<int> DeleteAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                SetEntityStateDeleted(entity);
                return await context.SaveChangesAsync();
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
