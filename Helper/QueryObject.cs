namespace library_be.Helper
{
    public class QueryObject
    {
        public string? SearchAll { get; set; } = null;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
