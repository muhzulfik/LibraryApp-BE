namespace library_be.Helper
{
    public class Paginated<T>
    {
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}
