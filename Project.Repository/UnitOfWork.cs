using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Project.DAL;
using Project.Model;
using Project.Repository.Common;

namespace Project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VehicleContext Context;
        private IRepository<VehicleMake> VehicleMakeRepository;
        private IRepository<VehicleModel> VehicleModelRepository;
        public UnitOfWork(VehicleContext context, IRepository<VehicleMake> vehicleMakeRepository,
            IRepository<VehicleModel> vehicleModelRepository)
        {
            this.Context = context;
            this.VehicleMakeRepository = vehicleMakeRepository;
            this.VehicleModelRepository = vehicleModelRepository;
            MakeRepository = new VehicleMakeRepository(Context, VehicleMakeRepository);
            ModelRepository = new VehicleModelRepository(Context, VehicleModelRepository);

        }

        public IMakeRepository MakeRepository { get; private set; }
        public IModelRepository ModelRepository { get; private set; }
        public async Task<int> CommitAsync()
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await Context.SaveChangesAsync();
                scope.Complete();
            }
            return result;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
