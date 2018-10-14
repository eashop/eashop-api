using System.ComponentModel.DataAnnotations;

namespace EaShop.Api.ViewModels
{
    public class Pagination
    {
        [Range(0, 100)]
        public int? PageSize { get; set; }
 
        [Range(0, int.MaxValue)]
        public int? PageNumber { get; set; }
    }
}
