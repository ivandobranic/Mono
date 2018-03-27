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
        private IRepository<VehicleMake> makeRepository;
        private IRepository<VehicleModel> modelRepository;
        public UnitOfWork(VehicleContext context)
        {
            this.Context = context;
            this.makeRepository = new GenericRepository<VehicleMake>(Context);
            this.modelRepository = new GenericRepository<VehicleModel>(Context);
            MakeRepository = new VehicleMakeRepository(makeRepository);
            ModelRepository = new VehicleModelRepository(modelRepository);

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
