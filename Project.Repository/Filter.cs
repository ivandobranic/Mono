using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Project.Repository.Common;

namespace Project.Repository
{
    public class Filter : IFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool IsAscending { get; set; }
        public string Search { get; set; }
        public int TotalCount { get; set; }
    }
}
