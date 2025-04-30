namespace Wasted_Food.Core.Pagination
{
    public class PaginatedResult<T>
    {
        public PaginatedResult(List<T> data)
        {
            Data = data;
        }
        public List<T> Data { get; set; }
        internal PaginatedResult(bool succeeded, List<T> data = default, List<string> message = null, int count = 0, int page = 0, int pagesize = 10)
        {
            Data = data;
            CurrentPage = page;
            Succeeded = succeeded;
            PageSize = pagesize;
            TotalPages = (int)Math.Ceiling(count / (double)pagesize);
            TotalCount = count;
        }
        public static PaginatedResult<T> Success(List<T> data, int count, int page, int pageSize)
        {
            return new(true, data, null, count, page, pageSize);
        }
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public object Meta { get; set; }

        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public List<string> Messages { get; set; } = new();

        public bool Succeeded { get; set; }
    }
}
