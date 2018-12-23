namespace EaShop.Api.ViewModels
{
    public class SearchWithPagination : Pagination
    {
        public string Name { get; set; }

        public int? CategoryId { get; set; }
    }
}