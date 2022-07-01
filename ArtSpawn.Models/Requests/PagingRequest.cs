using ArtSpawn.Models.Constants;

namespace ArtSpawn.Models.Requests
{
    public class PagingRequest
    {
        private readonly int maxPageSize = PaginationConstans.maxPageSize;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
}
