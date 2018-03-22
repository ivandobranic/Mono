using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Project.DAL;
using Project.Repository.Common;

namespace Project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VehicleContext context;

        public UnitOfWork(VehicleContext _context)
        {
            this.context = _context;
            MakeRepository = new VehicleMakeRepository(_context);
            ModelRepository = new VehicleModelRepository(_context);
        }
        public IMakeRepository MakeRepository { get; private set; }
        public IModelRepository ModelRepository { get; private set; }
        public async Task<int> CommitAsync()
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await context.SaveChangesAsync();
                scope.Complete();
            }
            return result;
        }

        public void Dispose()
        {
            context.Dispose();
        }

    }
}
