using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Project.Repository.Common
{
    public interface IFilter
    {
        int pageNumber { get; set; }
        int pageSize { get; set; }
        string sortOrder { get; set; }
        string search { get; set; }
        int totalCount { get; set; }
    }
}
