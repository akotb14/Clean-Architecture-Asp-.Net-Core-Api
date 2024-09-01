namespace CleanArchitecture.Application.ResultHandler
{
    public class PaginatedResult<T>
    {


        public List<T> Data { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public int TotalCount { get; set; }

        public object Meta { get; set; }

        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public string Messages { get; set; }

        public bool Succeeded { get; set; }
    }
}
