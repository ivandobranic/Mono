using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Project.Model;

namespace Project.Repository.Common
{
    public interface IMakeRepository : IRepository<VehicleMake>
    {
        Task<IPagedList<VehicleMake>> GetPagedMake(IFilter filter);
    }
}
