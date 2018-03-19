using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Project.Model;

namespace Project.Repository.Common
{
    public interface IPaging
    {
       Task<StaticPagedList<VehicleMake>> GetPagedResultMake(string sortOrder, string search, int pageNumber, int pageSize);
        
    }
}
