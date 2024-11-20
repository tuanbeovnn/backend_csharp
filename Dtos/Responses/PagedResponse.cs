namespace Dtos.Responses
{
    public class PagedResponse<T> : SuccessResponse<List<T>> where T : class
    {
        public long Total { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }

        public int TotalPages => Size < 1 ? 0 : (int)Math.Ceiling((decimal)Total / Size);
    }
}