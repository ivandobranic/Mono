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
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string sortOrder { get; set; }
        public string search { get; set; }
        public int totalCount { get; set; }
    }
}
