using Project.Repository.Common;

namespace Project.Repository
{
    public class Filter : IFilter
    {
        private int pageSize = 3;
        public int PageNumber { get; set; }
        public int PageSize { get { return pageSize; } }
        public bool IsAscending { get; set; }
        public string Search { get; set; }
        public int TotalCount { get; set; }
    }
}
