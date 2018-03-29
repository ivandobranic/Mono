using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using Project.DAL;
using Project.Repository.Common;

namespace Project.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
     where TEntity : class
    {

        private readonly VehicleContext Context;
        private DbSet<TEntity> Entities;
        public GenericRepository(VehicleContext context)
        {
            this.Context = context;
            Entities = Context.Set<TEntity>();

        }


        public IQueryable<TEntity> Get()
        {
            return Entities;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {

            return await Entities.FindAsync(id);

        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentException("entity");
                }
                Entities.Add(entity);
               return await Context.SaveChangesAsync();
               
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
                Context.Entry(entity).State = EntityState.Modified;
                return await Context.SaveChangesAsync();
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

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                var entity = await Entities.FindAsync(id);
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                Context.Entry(entity).State = EntityState.Deleted;
                return await Context.SaveChangesAsync();
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
