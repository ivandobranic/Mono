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
        int PageNumber { get; set; }
        int PageSize { get; set; }
        bool IsAscending { get; set; }
        string Search { get; set; }
        int TotalCount { get; set; }
    }
}
